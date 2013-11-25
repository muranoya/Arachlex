Imports Arachlex.DefinitionClass
Imports Arachlex.TCPNetworkClass
Imports Arachlex.ListViewItemComparer
Public Class Form1
    '設定のインスタンス
    Public appSettings As Settings = Nothing
    'それぞれのリストのソート
    Private AccountViewSorter, TransportViewSorter, ShareViewSorter As ListViewItemComparer
    'ユーザー情報
    Public UserAccount As New Generic.List(Of AccountListViewItem)
    '選択中のアカウント
    Public SelectUserAccount As AccountListViewItem
    '接続待ち中のTCPConnect
    Public tcpconnect As TCPNetworkClass
    '言語クラス
    Public lang As LanguageClass
    '多重起動チェックのmutex
    Private mutex As New System.Threading.Mutex(False, "Arachlex_MultipleStartsCheck")
    Private checkAppExit As Boolean = False

#Region "スレッドセーフ関数"
    ''' <summary>
    ''' 通知します
    ''' </summary>
    ''' <param name="msg">メッセージ</param>
    ''' <param name="title">タイトル</param>
    ''' <remarks></remarks>
    Public Sub DoNotify(ByVal title As String, ByVal msg As String)
        If Me.InvokeRequired Then
            Dim n As New DoNotifyDelegate(AddressOf DoNotifyCallBack)
            Me.BeginInvoke(n, New Object() {title, msg})
        Else
            DoNotifyCallBack(title, msg)
        End If
    End Sub
    Private Delegate Sub DoNotifyDelegate(ByVal title As String, ByVal msg As String)
    Private Sub DoNotifyCallBack(ByVal title As String, ByVal msg As String)
        If title IsNot Nothing AndAlso msg IsNot Nothing Then
            Dim showtitle As String = title
            Dim showmsg As String = msg
            If title.Length > 20 Then
                showtitle = showtitle.Substring(0, 20)
            End If
            If msg.Length > 100 Then
                showmsg = msg.Substring(0, 100)
            End If
            NotifyIcon1.ShowBalloonTip(3000, showtitle, showmsg, ToolTipIcon.Info)
        End If
    End Sub

    ''' <summary>
    ''' アカウントリストボックスを再描画します
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AccountListViewRedraw(ByVal index As Integer)
        If index >= 0 Then
            If Me.InvokeRequired Then
                Dim n As New AccountListViewRedrawDelegate(AddressOf AccountListViewRedrawCallBack)
                Me.BeginInvoke(n, New Object() {index})
            Else
                AccountListViewRedrawCallBack(index)
            End If
        End If
    End Sub
    Private Delegate Sub AccountListViewRedrawDelegate(ByVal index As Integer)
    Private Sub AccountListViewRedrawCallBack(ByVal index As Integer)
        Account_ListView.RedrawItems(index, index, False)
    End Sub

    ''' <summary>
    ''' ユーザーアカウントを登録します
    ''' </summary>
    ''' <param name="info">追加する情報</param>
    ''' <remarks></remarks>
    Public Sub AddUserAccount(ByVal info As AccountListViewItem)
        If Me.InvokeRequired Then
            Dim n As New AddUserAccountDelegate(AddressOf AddUserAccountCallBack)
            Me.BeginInvoke(n, New Object() {info})
        Else
            AddUserAccountCallBack(info)
        End If
    End Sub
    Private Delegate Sub AddUserAccountDelegate(ByVal info As AccountListViewItem)
    Private Sub AddUserAccountCallBack(ByVal info As AccountListViewItem)
        If info IsNot Nothing Then
            If UserAccount.IndexOf(info) < 0 Then
                UserAccount.Add(info)
            End If
        End If
    End Sub

    ''' <summary>
    ''' ユーザーアカウントを削除します
    ''' </summary>
    ''' <param name="info">削除するアカウント情報</param>
    ''' <remarks></remarks>
    Public Sub DeleteUserAccount(ByVal info As AccountListViewItem)
        If Me.InvokeRequired Then
            Dim n As New DeleteUserAccountDelegate(AddressOf DeleteUserAccountCallBack)
            Me.BeginInvoke(n, New Object() {info})
        Else
            DeleteUserAccountCallBack(info)
        End If
    End Sub
    Private Delegate Sub DeleteUserAccountDelegate(ByVal info As AccountListViewItem)
    Private Sub DeleteUserAccountCallBack(ByVal info As AccountListViewItem)
        If info IsNot Nothing AndAlso UserAccount.IndexOf(info) > -1 Then

            '転送情報の削除
            If info.TransportItem IsNot Nothing AndAlso info.TransportItem.Count > 0 Then
                Do
                    info.DeleteTransportInfoArray(info.TransportItem(0))
                Loop Until info.TransportItem Is Nothing OrElse info.TransportItem.Count = 0
            End If

            'リストビューを削除します
            Account_ListView.Items.Remove(info)

            '通信を切断
            info.Stop_bgConnect()
            info.Stop_bgMSGRec()
            If info.TCPConnect IsNot Nothing AndAlso info.TCPConnect.AceptInfo Then
                info.TCPConnect.NetworkClose()
            End If

            'ユーザー情報の削除
            UserAccount.Remove(info)
            info = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 角丸四角形を描画するためのGraphicsPathを取得します
    ''' </summary>
    ''' <param name="rect">描画範囲</param>
    ''' <param name="radius">角の円の半径</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRoundRect(ByVal rect As Rectangle, ByVal radius As Integer) As Drawing2D.GraphicsPath
        Dim path As New Drawing2D.GraphicsPath

        path.StartFigure()

        '左上の角丸
        path.AddArc(rect.Left, rect.Top, radius * 2, radius * 2, 180, 90)
        '上の線
        path.AddLine(rect.Left + radius, rect.Top, rect.Right - radius, rect.Top)
        '右上の角丸
        path.AddArc(rect.Right - radius * 2, rect.Top, radius * 2, radius * 2, 270, 90)
        '右の線
        path.AddLine(rect.Right, rect.Top + radius, rect.Right, rect.Bottom - radius)
        '右下の角丸
        path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90)
        '下の線
        path.AddLine(rect.Right - radius, rect.Bottom, rect.Left + radius, rect.Bottom)
        '左下の角丸
        path.AddArc(rect.Left, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90)
        '左の線
        path.AddLine(rect.Left, rect.Bottom - radius, rect.Left, rect.Top + radius)

        path.CloseFigure()

        Return path
    End Function

    ''' <summary>
    ''' 選択されている転送リストビューの転送アイテムをすべて取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSelectTransferItem() As TransferListViewItem()
        If Transfer_ListView.SelectedItems.Count > 0 Then
            Dim retItem As New List(Of TransferListViewItem)
            For i As Integer = 0 To Transfer_ListView.SelectedItems.Count - 1
                retItem.Add(DirectCast(Transfer_ListView.SelectedItems(i), TransferListViewItem))
            Next
            Return retItem.ToArray
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' アカウント名からユーザーアカウント情報を取得します
    ''' </summary>
    ''' <param name="name">検索するアカウント名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountInfoFromAccountName(ByVal name As String) As AccountListViewItem
        If UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 Then
            For i As Integer = 0 To UserAccount.Count - 1
                If UserAccount(i).IndividualData.AccountName.Equals(name, StringComparison.OrdinalIgnoreCase) Then
                    Return UserAccount(i)
                End If
            Next
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' アカウント名がダブっているか調べます
    ''' </summary>
    ''' <param name="AccountName">調べるアカウント名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExistAccountName(ByVal AccountName As String) As Boolean
        If AccountName IsNot Nothing AndAlso UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 Then

            If appSettings.Account_AccountName.Equals(AccountName, StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If

            For i As Integer = 0 To UserAccount.Count - 1
                If UserAccount(i).IndividualData.AccountName.Equals(AccountName, StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            Next

        End If
        Return False
    End Function
#End Region

#Region "関数"
    ''' <summary>
    ''' アカウントリストのソートモードを変更します
    ''' </summary>
    ''' <param name="newColumn">新しいカラムインデックス</param>
    ''' <param name="DoSort">ソートするか</param>
    ''' <param name="ChangeSortOrder">ソートオーダーを変更するか</param>
    ''' <remarks></remarks>
    Private Sub ChangeAccountListSortMode(ByVal newColumn As Integer, ByVal DoSort As Boolean, ByVal ChangeSortOrder As Boolean)
        Context_Account_Sort_AccountName.Checked = False
        Context_Account_Sort_LoginState.Checked = False
        Select Case newColumn
            Case AColumn_Name.Index
                Context_Account_Sort_AccountName.Checked = True
            Case AColumn_Status.Index
                Context_Account_Sort_LoginState.Checked = True
        End Select

        'ソートオーダーを変更するか
        If ChangeSortOrder AndAlso appSettings.Sort_Account_Column = newColumn Then
            If appSettings.Sort_Account_Order = SortOrder.Ascending Then
                appSettings.Sort_Account_Order = SortOrder.Descending
            Else
                appSettings.Sort_Account_Order = SortOrder.Ascending
            End If
            AccountViewSorter.Order = appSettings.Sort_Account_Order
        ElseIf Not ChangeSortOrder Then
            If appSettings.Sort_Account_Order = SortOrder.None Then
                appSettings.Sort_Account_Order = SortOrder.Ascending
            End If
            AccountViewSorter.Order = appSettings.Sort_Account_Order
        End If

        appSettings.Sort_Account_Column = newColumn
        AccountViewSorter.Column = newColumn

        If DoSort Then
            Account_ListView.Sort()
        End If
    End Sub
    ''' <summary>
    ''' ファイル転送リストのソートモードを変更します
    ''' </summary>
    ''' <param name="newColumn">ソートするカラム</param>
    ''' <param name="DoSort">ソートするか</param>
    ''' <param name="ChangeSortOrder">ソートオーダーを変更するか</param>
    ''' <remarks></remarks>
    Private Sub ChangeTransferListSortMode(ByVal newColumn As Integer, ByVal DoSort As Boolean, ByVal ChangeSortOrder As Boolean)
        Context_Transfer_Sort_OtherPoint.Checked = False
        Context_Transfer_Sort_Queue.Checked = False
        Context_Transfer_Sort_FileName.Checked = False
        Context_Transfer_Sort_FullPath.Checked = False
        Context_Transfer_Sort_Size.Checked = False
        Context_Transfer_Sort_Status.Checked = False

        Select Case newColumn
            Case TColumn_Remote.Index
                Context_Transfer_Sort_OtherPoint.Checked = True
            Case TColumn_Priority.Index
                Context_Transfer_Sort_Queue.Checked = True
            Case TColumn_FileName.Index
                Context_Transfer_Sort_FileName.Checked = True
            Case TColumn_FullPath.Index
                Context_Transfer_Sort_FullPath.Checked = True
            Case TColumn_Size.Index
                Context_Transfer_Sort_Size.Checked = True
            Case TColumn_Status.Index
                Context_Transfer_Sort_Status.Checked = True
        End Select

        'ソートオーダーを変更するか
        If ChangeSortOrder AndAlso appSettings.Sort_Transfer_Column = newColumn Then
            If appSettings.Sort_Transfer_Order = SortOrder.Ascending Then
                appSettings.Sort_Transfer_Order = SortOrder.Descending
            Else
                appSettings.Sort_Transfer_Order = SortOrder.Ascending
            End If
            TransportViewSorter.Order = appSettings.Sort_Transfer_Order
        ElseIf Not ChangeSortOrder Then
            If appSettings.Sort_Transfer_Order = SortOrder.None Then
                appSettings.Sort_Transfer_Order = SortOrder.Ascending
            End If
            TransportViewSorter.Order = appSettings.Sort_Transfer_Order
        End If

        appSettings.Sort_Transfer_Column = newColumn
        TransportViewSorter.Column = newColumn

        If DoSort Then
            Transfer_ListView.Sort()
        End If
    End Sub
    ''' <summary>
    ''' ファイル共有リストのソートモードを変更します
    ''' </summary>
    ''' <param name="newColumn">新しいカラム</param>
    ''' <param name="DoSort">ソートするか</param>
    ''' <param name="ChangeSortOrder">ソートオーダーを変更するか</param>
    ''' <remarks></remarks>
    Private Sub ChangeShareListSortMode(ByVal newColumn As Integer, ByVal DoSort As Boolean, ByVal ChangeSortOrder As Boolean)
        Context_Share_Sort_FileName.Checked = False
        Context_Share_Sort_Extension.Checked = False
        Context_Share_Sort_Size.Checked = False
        Context_Share_Sort_TimeStamp.Checked = False
        Select Case newColumn
            Case SColumn_Filename.Index
                Context_Share_Sort_FileName.Checked = True
            Case SColumn_Extension.Index
                Context_Share_Sort_Extension.Checked = True
            Case SColumn_Size.Index
                Context_Share_Sort_Size.Checked = True
            Case SColumn_TimeStamp.Index
                Context_Share_Sort_TimeStamp.Checked = True
        End Select

        'ソートオーダーを変更するか
        If ChangeSortOrder AndAlso appSettings.Sort_Share_Column = newColumn Then
            If appSettings.Sort_Share_Order = SortOrder.Ascending Then
                appSettings.Sort_Share_Order = SortOrder.Descending
            Else
                appSettings.Sort_Share_Order = SortOrder.Ascending
            End If
            ShareViewSorter.Order = appSettings.Sort_Share_Order
        ElseIf Not ChangeSortOrder Then
            If appSettings.Sort_Share_Order = SortOrder.None Then
                appSettings.Sort_Share_Order = SortOrder.Ascending
            End If
            ShareViewSorter.Order = appSettings.Sort_Share_Order
        End If

        ShareViewSorter.Column = newColumn
        appSettings.Sort_Share_Column = newColumn

        If DoSort Then
            Share_ListView.Sort()
        End If
    End Sub

    ''' <summary>
    ''' Enabledを変更します
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ChangeEnabled()
        Dim connectBoo As Boolean = SelectUserAccount IsNot Nothing AndAlso SelectUserAccount.TCPConnect.AceptInfo
        Dim tconnectBoo As Boolean = connectBoo OrElse (SelectUserAccount Is Nothing AndAlso UserAccount IsNot Nothing AndAlso UserAccount.Count > 0)

        If tconnectBoo AndAlso Not connectBoo Then
            'オンラインがいるか調べる
            Dim n As Boolean = False
            For i As Integer = 0 To UserAccount.Count - 1
                If UserAccount(i).TCPConnect.AceptInfo Then
                    n = True
                    Exit For
                End If
            Next
            tconnectBoo = n
        End If

        Transfer_Upload_File.Enabled = tconnectBoo
        Transfer_Upload_Folder.Enabled = tconnectBoo
        Share_Up.Enabled = connectBoo
        Context_Share_Up.Enabled = connectBoo
        Share_Refresh.Enabled = connectBoo
        Context_Share_Refresh.Enabled = connectBoo
        Share_Root.Enabled = connectBoo
        Context_Share_Root.Enabled = connectBoo

        Dim Transfer_boo As Boolean = Transfer_ListView.SelectedItems.Count > 0 AndAlso tconnectBoo
        Transfer_Approval_Approval.Enabled = Transfer_boo
        Transfer_Approval_AllApproval.Enabled = Transfer_boo
        Transfer_Approval_Resume.Enabled = Transfer_boo
        Context_Transfer_QueueUp.Enabled = Transfer_boo
        Context_Transfer_QueueDown.Enabled = Transfer_boo
        Transfer_Stop.Enabled = Transfer_boo
        Context_Transfer_Stop.Enabled = Transfer_boo
        Transfer_Restart.Enabled = Transfer_boo
        Context_Transfer_ReStart.Enabled = Transfer_boo
        Transfer_Interruption.Enabled = Transfer_boo
        Context_Transfer_Interruption.Enabled = Transfer_boo

        Dim Transfer_NumBoo As Boolean = Transfer_ListView.SelectedItems.Count > 0
        Transfer_Interruption.Enabled = Transfer_NumBoo
        Context_Transfer_Interruption.Enabled = Transfer_NumBoo
        Transfer_Delete.Enabled = Transfer_NumBoo
        Context_Transfer_Delete.Enabled = Transfer_NumBoo

        Dim Share_boo As Boolean = Share_ListView.SelectedItems.Count > 0 AndAlso connectBoo
        Share_Download.Enabled = Share_boo
        Context_Share_Download.Enabled = Share_boo
    End Sub

    ''' <summary>
    ''' ファイルかフォルダをオープンします
    ''' </summary>
    ''' <param name="tInfo">オープンする情報を持っているアイテム</param>
    ''' <remarks></remarks>
    Private Sub OpenFileOrFolder(ByVal tInfo As TransferListViewItem)
        If tInfo Is Nothing Then
            Return
        End If

        Try
            If tInfo.ItemState = Arachlex_Transport.Download AndAlso tInfo.TransportStatus <> TransferListViewItem.ItemStatus.NotApproval Then
                If tInfo.ItemAttribute = Arachlex_Item.File Then
                    If tInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled AndAlso tInfo.FileCachePath IsNot Nothing AndAlso IO.File.Exists(tInfo.FileCachePath) Then
                        Process.Start(tInfo.FileCachePath)
                    ElseIf tInfo.FilePath IsNot Nothing AndAlso IO.File.Exists(tInfo.FilePath) Then
                        Process.Start(tInfo.FilePath)
                    End If
                ElseIf tInfo.ItemAttribute = Arachlex_Item.Folder AndAlso tInfo.FilePath IsNot Nothing AndAlso IO.Directory.Exists(tInfo.FilePath) Then
                    Process.Start(tInfo.FilePath)
                End If
            ElseIf tInfo.ItemState = Arachlex_Transport.Upload Then
                If tInfo.ItemAttribute = Arachlex_Item.File AndAlso tInfo.FilePath IsNot Nothing AndAlso IO.File.Exists(tInfo.FilePath) Then
                    Process.Start(tInfo.FilePath)
                ElseIf tInfo.ItemAttribute = Arachlex_Item.Folder AndAlso tInfo.FilePath IsNot Nothing AndAlso IO.Directory.Exists(tInfo.FilePath) Then
                    Process.Start(tInfo.FilePath)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 指定拡張子のアイコンをイメージリストに登録します
    ''' </summary>
    ''' <param name="keyname">取得する対象の拡張子</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddIconToImageList(ByVal keyname As String) As String
        If keyname Is Nothing OrElse keyname.Length = 0 Then
            Return "nothing"
        End If

        If Share_SmallImageList.Images(keyname) Is Nothing Then
            Dim addicon As Icon = GetExtensionIcon(Me.Handle, keyname)
            If addicon Is Nothing Then
                Return "nothing"
            Else
                Share_SmallImageList.Images.Add(keyname, addicon)
                Share_LargeImageList.Images.Add(keyname, addicon)
            End If
        End If

        Return keyname.ToLower
    End Function

    ''' <summary>
    ''' リストからアイテムを検索します
    ''' </summary>
    ''' <param name="searchStr">検索する文字列</param>
    ''' <remarks></remarks>
    Private Sub FindList(ByVal searchStr As String)
        If Panel_Share.Visible Then

            If SelectUserAccount Is Nothing Then
                Return
            End If

            Share_ListView.Items.Clear()
            With SelectUserAccount.ShareInfo
                If .YourOpenList Is Nothing OrElse .YourOpenList.Count <= 0 Then
                    Return
                End If

                Dim f As Integer = 0 'newListのインデックス
                Dim newList As New Generic.List(Of FileInfoListViewItem)
                For i As Integer = 0 To .YourOpenList.Count - 1
                    If .YourOpenList(i).FileName.IndexOf(searchStr, StringComparison.InvariantCultureIgnoreCase) > -1 Then
                        newList.Add(.YourOpenList(i))
                        f += 1
                    End If
                Next
                .AddListView(newList)
            End With
        ElseIf Panel_Transfer.Visible Then

            Transfer_ListView.Items.Clear()
            Dim ItemArray As New List(Of TransferListViewItem)
            If SelectUserAccount IsNot Nothing Then
                If SelectUserAccount.TransportItem IsNot Nothing AndAlso SelectUserAccount.TransportItem.Count > 0 Then
                    ItemArray.AddRange(SelectUserAccount.TransportItem)
                End If
            ElseIf UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 Then
                For i As Integer = 0 To UserAccount.Count - 1
                    If UserAccount(i).TransportItem IsNot Nothing AndAlso UserAccount(i).TransportItem.Count > 0 Then
                        ItemArray.AddRange(UserAccount(i).TransportItem)
                    End If
                Next
            End If

            If ItemArray.Count <= 0 Then
                Return
            End If
            For i As Integer = 0 To ItemArray.Count - 1
                If ItemArray(i).FilePath.IndexOf(searchStr, StringComparison.InvariantCultureIgnoreCase) > -1 OrElse ItemArray(i).FileName.IndexOf(searchStr, StringComparison.InvariantCultureIgnoreCase) > -1 Then
                    Transfer_ListView.Items.Add(ItemArray(i))
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' 指定したアカウントのチャットウィンドウを開きます
    ''' </summary>
    ''' <param name="ua">対象のアカウント</param>
    ''' <remarks></remarks>
    Private Sub ShowChatWindow(ByVal ua As AccountListViewItem)
        If ua.ChatWindow Is Nothing Then
            '存在しない場合
            Dim n As New Form_Chat(ua)
            n.Show()
            ua.ChatWindow = n
        Else
            '存在する場合
            ua.ChatWindow.Show()
        End If
    End Sub

    ''' <summary>
    ''' UIの言語表示をリセットします
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ResetUserInterfaceLanguage()
        With lang
            Context_Account_Connect.Text = .MainForm_CAccount_Connect
            Context_Account_Reconnect.Text = .MainForm_CAccount_Reconnect
            Context_Account_Chat.Text = .MainForm_CAccount_Chat
            Context_Account_Add.Text = .MainForm_CAccount_AddAccount
            Context_Account_Edit.Text = .MainForm_CAccount_EditAccount
            Context_Account_Delete.Text = .MainForm_CAccount_DeleteAccount
            Context_Account_Sort.Text = .MainForm_CAccount_Sort
            Context_Account_Sort_AccountName.Text = .MainForm_CAccount_Sort_AccountName
            Context_Account_Sort_LoginState.Text = .MainForm_CAccount_Sort_LoginStatus
            Context_Account_Setting.Text = .MainForm_CAccount_Setting

            Context_Transfer_Open.Text = .MainForm_CTransport_Open
            Context_Transfer_QueueUp.Text = .MainForm_CTransport_PriorityUp
            Context_Transfer_QueueDown.Text = .MainForm_CTransport_PriorityDown
            Context_Transfer_Stop.Text = .MainForm_CTransport_Pause
            Context_Transfer_ReStart.Text = .MainForm_CTransport_Restart
            Context_Transfer_Interruption.Text = .MainForm_CTransport_Stop
            Context_Transfer_Delete.Text = .MainForm_CTransport_Delete
            Context_Transfer_Sort.Text = .MainForm_CTransport_Sort
            Context_Transfer_Sort_OtherPoint.Text = .MainForm_CTransport_Sort_Remote
            Context_Transfer_Sort_Queue.Text = .MainForm_CTransport_Sort_Priority
            Context_Transfer_Sort_FileName.Text = .MainForm_CTransport_Sort_FileName
            Context_Transfer_Sort_FullPath.Text = .MainForm_CTransport_Sort_FullPath
            Context_Transfer_Sort_Size.Text = .MainForm_CTransport_Sort_Size
            Context_Transfer_Sort_Status.Text = .MainForm_CTransport_Sort_Status
            Context_Transfer_ListMode.Text = .MainForm_CTransport_ListMode
            Context_Transfer_FindBox.Text = .MainForm_CTransport_SearchBox
            Context_Transfer_AllSelect.Text = .MainForm_CTransport_AllSelect

            Context_Share_Download.Text = .MainForm_CShare_Download
            Context_Share_Up.Text = .MainForm_CShare_Up
            Context_Share_Refresh.Text = .MainForm_CShare_Refresh
            Context_Share_Root.Text = .MainForm_CShare_Root
            Context_Share_Sort.Text = .MainForm_CShare_Sort
            Context_Share_Sort_FileName.Text = .MainForm_CShare_Sort_FileName
            Context_Share_Sort_Extension.Text = .MainForm_CShare_Sort_Extension
            Context_Share_Sort_Size.Text = .MainForm_CShare_Sort_Size
            Context_Share_Sort_TimeStamp.Text = .MainForm_CShare_Sort_DateModified
            Context_Share_FindBox.Text = .MainForm_CShare_SearchBox
            Context_Share_AllSelect.Text = .MainForm_CShare_AllSelect

            Find_Button.Text = .MainForm_SearchBox_SearchButton
            Transfer_Approval.Text = .MainForm_Transport_DownloadMenu
            Transfer_Approval_Approval.Text = .MainForm_Transport_Download_Approval
            Transfer_Approval_AllApproval.Text = .MainForm_Transport_Download_AllApproval
            Transfer_Approval_Resume.Text = .MainForm_Transport_Download_Resume
            Transfer_Upload.Text = .MainForm_Transport_UploadMenu
            Transfer_Upload_File.Text = .MainForm_Transport_Upload_File
            Transfer_Upload_Folder.Text = .MainForm_Transport_Upload_Folder
            Transfer_Stop.Text = .MainForm_Transport_Pause
            Transfer_Restart.Text = .MainForm_Transport_Restart
            Transfer_Interruption.Text = .MainForm_Transport_Stop
            Transfer_Delete.Text = .MainForm_Transport_Delete
            Transfer_DoneDelete.Text = .MainForm_Transport_DoneDelete
            Transfer_GoShareList.Text = .MainForm_Transport_GoShareList

            TColumn_Remote.Text = .MainForm_Transport_Column_Remote
            TColumn_Priority.Text = .MainForm_Transport_Column_Priority
            TColumn_FileName.Text = .MainForm_Transport_Column_FileName
            TColumn_FullPath.Text = .MainForm_Transport_Column_FullPath
            TColumn_Size.Text = .MainForm_Transport_Column_Size
            TColumn_Status.Text = .MainForm_Transport_Column_Status

            Share_Download.Text = .MainForm_Share_Download
            Share_Up.Text = .MainForm_Share_Up
            Share_Refresh.Text = .MainForm_Share_Refresh
            Share_Root.Text = .MainForm_Share_Root
            Share_GoTransferList.Text = .MainForm_Share_GoTransportList

            SColumn_Filename.Text = .MainForm_Share_Column_FileName
            SColumn_Extension.Text = .MainForm_Share_Column_Extension
            SColumn_Size.Text = .MainForm_Share_Column_Size
            SColumn_TimeStamp.Text = .MainForm_Share_Column_DateModified
        End With
    End Sub

    ''' <summary>
    ''' 言語ファイルをロードします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadLanguageFile() As LanguageClass
        Dim fs2 As System.IO.FileStream = Nothing
        Try
            Dim ret As LanguageClass
            Dim bf2 As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
            fs2 = New System.IO.FileStream(LanguageLocation, System.IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            ret = CType(bf2.Deserialize(fs2), LanguageClass)
            fs2.Close()
            Return ret
        Catch
            appSettings = New Settings
            Return Nothing
        Finally
            If fs2 IsNot Nothing Then
                fs2.Close()
                fs2.Dispose()
            End If
        End Try
    End Function
#End Region

    Private Sub ApplicationError(ByVal sender As Object, ByVal e As System.Threading.ThreadExceptionEventArgs)
        Try
            If MessageBox.Show("Unexpected error has occurred." & vbCrLf & _
             "Please report to the developers." & vbCrLf & _
             vbCrLf & _
             "Software::" & SoftwareName & SoftwareVersion & vbCrLf & _
             "OS::" & Environment.OSVersion.VersionString & vbCrLf & _
             ".NET Framework::" & Environment.Version.ToString & vbCrLf & _
             "Content of happen::" & e.Exception.Message & vbCrLf & _
             "Content of Site::" & e.Exception.TargetSite.Name & vbCrLf & _
             "Content of detail::" & e.Exception.TargetSite.ToString _
             , SoftwareName, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = Windows.Forms.DialogResult.Cancel Then
                Application.Exit()
            End If
        Catch ex As Exception
            Try
                MessageBox.Show("Critical error has occurred" & vbCrLf & "Exit software...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Catch
                Application.Exit()
            End Try
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler Application.ThreadException, AddressOf ApplicationError
       
        '設定の読み出し
        Dim fs2 As System.IO.FileStream = Nothing
        Try
            Dim bf2 As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
            fs2 = New System.IO.FileStream(SaveConfigPath, System.IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            appSettings = CType(bf2.Deserialize(fs2), Settings)
            fs2.Close()
        Catch
            appSettings = New Settings
        Finally
            If fs2 IsNot Nothing Then
                fs2.Close()
                fs2.Dispose()
            End If
        End Try

        '言語ファイルの設定
        If appSettings.Language = LanguageClass.LanguageType.LanguageFile Then
            lang = LoadLanguageFile()
            If lang Is Nothing Then
                lang = New LanguageClass(LanguageClass.LanguageType.English)
            End If
        Else
            lang = New LanguageClass(appSettings.Language)
        End If
        ResetUserInterfaceLanguage()

        '一部設定の反映
        Me.Size = appSettings.Main_Size
        Me.Location = appSettings.Main_Location
        Me.Text = SoftwareName & " " & SoftwareVersion
        NotifyIcon1.Text = Me.Text

        'ウィンドウの位置が画面外でないか
        Dim sc As Screen = System.Windows.Forms.Screen.FromPoint(Me.Location)
        If Not sc.Bounds.Contains(Me.Location) Then
            Me.Location = sc.Bounds.Location
        End If

        'パネルの設定
        Panel_Transfer.Dock = DockStyle.Fill
        Panel_Share.Dock = DockStyle.Fill
        Panel_Share.Visible = False
        Panel_Find.Dock = DockStyle.Bottom

        '検索ボックスの設定
        Panel_Find.Visible = appSettings.ShowFindBox
        Context_Transfer_FindBox.Checked = appSettings.ShowFindBox
        Context_Share_FindBox.Checked = appSettings.ShowFindBox

        '多重起動のチェック
        If Not appSettings.Main_AllowMultipleStarts Then
            Try
                mutex = New System.Threading.Mutex(False, "Arachlex_MultipleStartsCheck")

                'ミューテックスの所有権を要求する
                If Not mutex.WaitOne(0, False) Then
                    'すでに起動していると判断して終了
                    checkAppExit = True

                    '既に起動しているウィンドウをアクティブにする
                    Dim prs As Process() = Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName)
                    For Each p As Process In prs
                        SetForegroundWindow(p.MainWindowHandle)
                    Next

                    'ソフトウェアを終了する
                    Application.Exit()
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        '設定の復元
        With appSettings
            Me.TopMost = .Main_TopMost

            Try
                MainSplitContainer.SplitterDistance = .Split_Main
            Catch ex As Exception
            End Try

            Context_Transfer_ListMode.Checked = .TransferList_ListMode
            If .TransferList_ListMode Then
                Transfer_ListView.View = View.Details
            Else
                Transfer_ListView.View = View.Tile
            End If

            If Me.WindowState = FormWindowState.Minimized AndAlso .Main_MiniHide Then
                Me.Visible = False
            End If

            '転送リストのカラムの調整
            Dim i As Integer = 0
            If .Column_Transfer_Width IsNot Nothing AndAlso .Column_Transfer_DisplayIndex IsNot Nothing Then
                For i = 0 To Transfer_ListView.Columns.Count - 1
                    If .Column_Transfer_Width.Count - 1 >= i AndAlso .Column_Transfer_DisplayIndex.Count - 1 >= i Then
                        Transfer_ListView.Columns(i).Width = .Column_Transfer_Width(i)
                        Transfer_ListView.Columns(i).DisplayIndex = .Column_Transfer_DisplayIndex(i)
                    End If
                Next
            End If

            'ファイル共有リストのカラムの調整
            If .Column_Share_DisplayIndex IsNot Nothing AndAlso .Column_Share_Width IsNot Nothing Then
                For i = 0 To Share_ListView.Columns.Count - 1
                    If .Column_Share_Width.Count - 1 >= i AndAlso .Column_Share_DisplayIndex.Count - 1 >= i Then
                        Share_ListView.Columns(i).Width = .Column_Share_Width(i)
                        Share_ListView.Columns(i).DisplayIndex = .Column_Share_DisplayIndex(i)
                    End If
                Next
            End If

            'コンタクトリストの復元
            If .Account_Data IsNot Nothing AndAlso .Account_Data.Count > 0 Then
                For i = 0 To appSettings.Account_Data.Count - 1
                    Dim newAccount As New AccountListViewItem(Me, True, True, .Account_Data(i))
                    AddUserAccount(newAccount)
                Next
            End If
        End With

        Transfer_ListView_SizeChanged(Nothing, Nothing)
        If Not bgServer.IsBusy Then
            bgServer.RunWorkerAsync()
        End If

        'アカウントリストのソートを設定
        AccountViewSorter = New ListViewItemComparer
        AccountViewSorter.ColumnModes = New ComparerMode() _
        {ComparerMode.Account_Name, ComparerMode.Account_Login}

        Account_ListView.ListViewItemSorter = AccountViewSorter
        ChangeAccountListSortMode(appSettings.Sort_Share_Column, False, False)

        '転送リストのソートを設定
        TransportViewSorter = New ListViewItemComparer()
        TransportViewSorter.ColumnModes = New ComparerMode() _
        {ComparerMode.Transfer_OtherPoint, ComparerMode.Transfer_Queue, ComparerMode.Transfer_FileName, ComparerMode.Transfer_FullPath, ComparerMode.Transfer_Size, ComparerMode.Transfer_Status}

        Transfer_ListView.ListViewItemSorter = TransportViewSorter
        ChangeTransferListSortMode(appSettings.Sort_Transfer_Column, False, False)

        'ファイル共有リストのソートを設定
        ShareViewSorter = New ListViewItemComparer()
        ShareViewSorter.ColumnModes = New ComparerMode() _
        {ComparerMode.FileInfo_String, ComparerMode.FileInfo_String, ComparerMode.FileInfo_Size, ComparerMode.FileInfo_Time}

        Share_ListView.ListViewItemSorter = ShareViewSorter
        ChangeShareListSortMode(appSettings.Sort_Share_Column, False, False)
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not checkAppExit Then
            With appSettings
                If Me.WindowState = FormWindowState.Normal Then
                    .Main_Location = Me.Location
                    .Main_Size = Me.Size
                Else
                    .Main_Location = Me.RestoreBounds.Location
                    .Main_Size = Me.RestoreBounds.Size
                End If

                .Split_Main = MainSplitContainer.SplitterDistance

                Dim i As Integer = 0

                .Column_Transfer_Width = New List(Of Integer)
                .Column_Transfer_DisplayIndex = New List(Of Integer)
                For i = 0 To Transfer_ListView.Columns.Count - 1
                    .Column_Transfer_Width.Add(Transfer_ListView.Columns(i).Width)
                    .Column_Transfer_DisplayIndex.Add(Transfer_ListView.Columns(i).DisplayIndex)
                Next

                .Column_Share_Width = New List(Of Integer)
                .Column_Share_DisplayIndex = New List(Of Integer)
                For i = 0 To Share_ListView.Columns.Count - 1
                    .Column_Share_Width.Add(Share_ListView.Columns(i).Width)
                    .Column_Share_DisplayIndex.Add(Share_ListView.Columns(i).DisplayIndex)
                Next

                'コンタクトリストの保存
                If UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 Then
                    Dim newAccountData As New List(Of AccountData)
                    For i = 0 To UserAccount.Count - 1
                        newAccountData.Add(UserAccount(i).IndividualData)
                    Next
                    appSettings.Account_Data = newAccountData
                End If
            End With

            '本当に終了するか確認する
            If e.CloseReason <> CloseReason.WindowsShutDown Then
                If MessageBox.Show(lang.MainForm_Dialog_Quit, SoftwareName, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True
                    Exit Sub
                End If
            End If
            '通信を切断する
            If UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 Then
               For i As Integer = 0 To UserAccount.Count - 1
                    UserAccount(i).TCPConnect.NetworkClose()
                Next
            End If

            '設定の保存
            Try
                Dim bf1 As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
                Dim fs1 As New System.IO.FileStream(SaveConfigPath, System.IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
                bf1.Serialize(fs1, appSettings)
                fs1.Close()
            Catch ex As Exception
            End Try

            'mutexの解法
            Try
                If mutex IsNot Nothing Then
                    mutex.ReleaseMutex()
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub Form1_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        If Me.WindowState = FormWindowState.Minimized AndAlso appSettings IsNot Nothing AndAlso appSettings.Main_MiniHide Then
            Me.Visible = False
        End If
    End Sub

    Private Sub NotifyIcon1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.Click
        Me.Visible = Not Me.Visible
        Me.WindowState = FormWindowState.Normal
        Me.Activate()
    End Sub
    Private Sub ConnectCheck_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectCheck.Tick
        If UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 Then
            For i As Integer = 0 To UserAccount.Count - 1
                If UserAccount(i).TCPConnect.AceptInfo Then
                    UserAccount(i).TCPConnect.SendPacket(String.Empty, TCP_Header.JUNK, 0)
                End If
            Next
        End If
    End Sub
    Private Sub RefreshStatus_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshStatus.Tick
        If UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 Then
            Dim allDownloadSize As Integer = 0
            Dim allUploadSize As Integer = 0

            For i As Integer = 0 To UserAccount.Count - 1
                Dim ua As AccountListViewItem = UserAccount(i)
                'Form1のスピードメーター更新用データを作る
                allDownloadSize += ua.TCPConnect.OneReceiveSize
                allUploadSize += ua.TCPConnect.OneSendSize

                'チャットウィンドウの情報を更新
                If ua.ChatWindow IsNot Nothing AndAlso ua.IndividualData.Chat_ShowInfo Then
                    If ua.TCPConnect.AceptInfo Then
                        With UserAccount(i).ChatWindow
                            .Info_ConnectStartTime.Text = lang.MainForm_Infomation_ConnectStartTime & ua.TCPConnect.StartAceptTime.ToString
                            .Info_ConnectTime.Text = lang.MainForm_Infomation_ConnectTime & ua.TCPConnect.AceptTime
                            .Info_AllReceiveSize.Text = lang.MainForm_Infomation_SumReceiveSize & RetFileSize(ua.TCPConnect.AllReceiveSize)
                            .Info_AllSendSize.Text = lang.MainForm_Infomation_SumSendSize & RetFileSize(ua.TCPConnect.AllSendSize)
                            .Info_SendTransferSpeed.Text = lang.MainForm_Infomation_SendSpeed & (ua.TCPConnect.OneSendSize \ 1024).ToString("#,##0KB")
                            .Info_RecTransferSpeed.Text = lang.MainForm_Infomation_ReceiveSpeed & (ua.TCPConnect.OneReceiveSize \ 1024).ToString("#,##0KB")

                            .Info_YourVersion.Text = lang.MainForm_Infomation_YourVersion & ua.TCPConnect.SoftVersion
                            .Info_YourProtocol.Text = lang.MainForm_Infomation_YourProtocolVersion & ua.TCPConnect.SoftProtocol
                            .Info_MyVersion.Text = lang.MainForm_Infomation_MyVersion & SoftwareVersion
                            .Info_MyProtocol.Text = lang.MainForm_Infomation_MyProtocolVersion & ProtocolVersion

                            .Info_YourIP.Text = lang.MainForm_Infomation_YourIPAddress & ua.TCPConnect.RemoteIp
                            .Info_YourPort.Text = lang.MainForm_Infomation_YourPort & ua.TCPConnect.RemotePort

                            .Info_ReceiveEncryptData.Text = lang.MainForm_Infomation_RecEncryptDataNum & ua.TCPConnect.ReceiveEncryptDataNum
                            .Info_SendEncryptData.Text = lang.MainForm_Infomation_SendEncryptDataNum & ua.TCPConnect.SendEncryptDataNum
                        End With
                    Else
                        With UserAccount(i).ChatWindow
                            Const nothingStr As String = "N/A"

                            .Info_ConnectStartTime.Text = lang.MainForm_Infomation_ConnectStartTime & nothingStr
                            .Info_ConnectTime.Text = lang.MainForm_Infomation_ConnectTime & nothingStr
                            .Info_AllReceiveSize.Text = lang.MainForm_Infomation_SumReceiveSize & nothingStr
                            .Info_AllSendSize.Text = lang.MainForm_Infomation_SumSendSize & nothingStr
                            .Info_SendTransferSpeed.Text = lang.MainForm_Infomation_SendSpeed & nothingStr
                            .Info_RecTransferSpeed.Text = lang.MainForm_Infomation_ReceiveSpeed & nothingStr

                            .Info_YourVersion.Text = lang.MainForm_Infomation_YourVersion & nothingStr
                            .Info_YourProtocol.Text = lang.MainForm_Infomation_YourProtocolVersion & nothingStr
                            .Info_MyVersion.Text = lang.MainForm_Infomation_MyVersion & SoftwareVersion
                            .Info_MyProtocol.Text = lang.MainForm_Infomation_MyProtocolVersion & ProtocolVersion

                            .Info_YourIP.Text = lang.MainForm_Infomation_YourIPAddress & nothingStr
                            .Info_YourPort.Text = lang.MainForm_Infomation_YourPort & nothingStr

                            .Info_ReceiveEncryptData.Text = lang.MainForm_Infomation_RecEncryptDataNum & nothingStr
                            .Info_SendEncryptData.Text = lang.MainForm_Infomation_SendEncryptDataNum & nothingStr
                        End With
                    End If
                End If

                'カウンタをリセット
                ua.TCPConnect.ResetOneSize()
            Next

            'スピードメーターを更新
            Info_Speedometer.Text = "Down：" & (allDownloadSize \ 1024).ToString("#,##0KB") & vbCrLf & _
            "Up：" & (allUploadSize \ 1024).ToString("#,##0KB")
        Else
            Info_Speedometer.Text = "Down：0KB" & vbCrLf & "Up：0KB"
        End If

        Transfer_ListView.Refresh()
    End Sub
    Private Sub Find_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Find_Button.Click
        FindList(Find_TextBox.Text)
    End Sub
    Private Sub Find_TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Find_TextBox.KeyDown
        If e.KeyData = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            FindList(Find_TextBox.Text)
        End If
    End Sub
    Private Sub Find_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Find_TextBox.TextChanged
        FindList(Find_TextBox.Text)
    End Sub
    Private Sub Context_FindBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_FindBox.Click, Context_Share_FindBox.Click
        appSettings.ShowFindBox = Not appSettings.ShowFindBox

        Context_Transfer_FindBox.Checked = appSettings.ShowFindBox
        Context_Share_FindBox.Checked = appSettings.ShowFindBox

        Panel_Find.Visible = appSettings.ShowFindBox
    End Sub

    Private Sub bgServer_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgServer.DoWork
        Do
            bgServer.ReportProgress(10, appSettings.Connect_ListenPort)

            Try
                tcpconnect = New TCPNetworkClass(enc)
                'ポートをリッスンして接続要求を待つ
                Dim accountname As String = ""
                Dim status As TCPNetworkClass.ListenStatus = tcpconnect.ListenPort(appSettings.Connect_ListenPort, accountname)

                If status = ListenStatus.ListenError Then
                    'ポートリッスンエラー
                    MessageBox.Show(lang.MainForm_PortListenErrorMSG, SoftwareName, MessageBoxButtons.OK)
                    e.Cancel = True
                    Throw New Exception("Don't listen port")

                ElseIf status = ListenStatus.Done Then
                    '接続されたら
                    Dim ua As AccountListViewItem = Me.GetAccountInfoFromAccountName(accountname)
                    If ua IsNot Nothing Then
                        '認証する
                        If tcpconnect.Login(ua.IndividualData.LoginPassword, LoginServiceClass.ServiceMode.Server, ua.IndividualData.UseEncryptNetwork) Then
                            ua.TCPConnect = tcpconnect
                            ua.ConnectMethod()
                        Else
                            Throw New Exception("Failed login")
                        End If
                    Else
                        Throw New Exception("Don't exist account")
                    End If

                ElseIf status = ListenStatus.Error Then
                    'エラーの時は何もしない。もう一度接続を待つ
                    Throw New Exception("Connection is not successful")

                ElseIf accountname Is Nothing OrElse accountname.Length = 0 Then
                    'アカウント名送信ステップで接続が切れたとき
                    Throw New Exception("Invalid accountName")

                End If
            Catch ex As Exception
                If tcpconnect IsNot Nothing Then
                    tcpconnect.NetworkClose()
                    tcpconnect = Nothing
                End If
            End Try
        Loop Until e.Cancel
    End Sub
    Private Sub bgServer_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgServer.ProgressChanged
        Select Case e.ProgressPercentage
            Case 10
                Info_Port.Text = "Port：" & CStr(DirectCast(e.UserState, Integer))
        End Select
    End Sub
    Private Sub bgServer_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgServer.RunWorkerCompleted
        If Not e.Cancelled AndAlso Not bgServer.IsBusy Then
            bgServer.RunWorkerAsync()
        Else
            Info_Port.Text = "Port：Not listen"
        End If
    End Sub

#Region "アカウントリスト"
    Private Sub Context_Account_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Account_Connect.Click
        If Account_ListView.SelectedItems.Count > 0 Then
            '選択しているアカウント
            Dim ua As AccountListViewItem = DirectCast(Account_ListView.SelectedItems(0), AccountListViewItem)

            If ua IsNot Nothing AndAlso Not ua.TCPConnect.AceptInfo Then
                ua.TCPConnect.ConnectionNetwork(ua.IndividualData.ConnectIP, ua.IndividualData.ConnectPort, appSettings.Account_AccountName, True)
                If ua.TCPConnect.Login(ua.IndividualData.LoginPassword, LoginServiceClass.ServiceMode.Client, ua.IndividualData.UseEncryptNetwork) Then
                    ua.ConnectMethod()
                End If
            End If
        End If
    End Sub
    Private Sub Context_Account_Reconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Account_Reconnect.Click
        If Account_ListView.SelectedItems.Count > 0 Then
            Dim ua As AccountListViewItem = DirectCast(Account_ListView.SelectedItems(0), AccountListViewItem)

            ua.TCPConnect.NetworkClose()
        End If
    End Sub
    Private Sub Context_Account_Chat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Account_Chat.Click
        ShowChatWindow(DirectCast(Account_ListView.SelectedItems(0), AccountListViewItem))
    End Sub
    Private Sub Context_Account_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Account_Add.Click
        Dim newAddForm As New Form_Account
        Dim ua As New AccountData()
        ua = newAddForm.AccountContactInfo(Me, False, ua)
        If ua IsNot Nothing Then
            Dim newAccount As New AccountListViewItem(Me, True, True, ua)
            AddUserAccount(newAccount)
        End If
    End Sub
    Private Sub Context_Account_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Account_Edit.Click
        If Account_ListView.SelectedItems.Count > 0 Then
            Dim editAccount As AccountListViewItem = DirectCast(Account_ListView.SelectedItems(0), AccountListViewItem)
            Dim newEditAccount As New Form_Account
            Dim ua As AccountData = newEditAccount.AccountContactInfo(Me, True, editAccount.IndividualData)
            If ua IsNot Nothing Then
                editAccount.IndividualData = ua
                editAccount.TCPConnect.UseEncrypt = ua.UseEncryptNetwork
                AccountListViewRedraw(editAccount.Index)
            End If
        End If
    End Sub
    Private Sub Context_Account_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Account_Delete.Click
        If Account_ListView.SelectedItems.Count > 0 Then
            Do
                DeleteUserAccount(DirectCast(Account_ListView.SelectedItems(0), AccountListViewItem))
            Loop Until Account_ListView.SelectedItems.Count = 0
        End If
    End Sub
    Private Sub Context_Account_Sort_AccountName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Account_Sort_AccountName.Click
        ChangeAccountListSortMode(AColumn_Name.Index, True, True)
    End Sub
    Private Sub Context_Account_Sort_LoginState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Account_Sort_LoginState.Click
        ChangeAccountListSortMode(AColumn_Status.Index, True, True)
    End Sub
    Private Sub Context_Account_Setting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Account_Setting.Click
        Dim newSettingForm As New Form_Setting(Me)
        newSettingForm.ShowDialog()
        newSettingForm.Dispose()
    End Sub

    Private Sub Account_ListView_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles Account_ListView.DrawItem
        If e.Item IsNot Nothing Then
            'バックカラーの描画
            '少し縮めないと、はみ出して、描画あとが残る場合がある
            Dim ua As AccountListViewItem = DirectCast(e.Item, AccountListViewItem)
            Dim drawingPath As Drawing2D.GraphicsPath = GetRoundRect(New Rectangle(e.Bounds.Left, e.Bounds.Top + 1, e.Bounds.Width, e.Bounds.Height - 2), 5)
            Dim g As Graphics = e.Graphics
            Dim NameColor, CommentColor As Brush
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            '描画のリセット。白で塗りつぶしなおす
            g.FillRectangle(Brushes.White, e.Bounds)

            '描画対象のアイテムの状態によって、描画する色を変える
            If ua.Selected Then
                '選択状態
                g.FillPath(Brushes.RoyalBlue, drawingPath)
                NameColor = Brushes.White
                CommentColor = Brushes.PaleTurquoise
            Else
                'それ以外(非選択時)
                g.FillPath(Brushes.White, drawingPath)
                NameColor = Brushes.Black
                CommentColor = Brushes.SlateGray
            End If

            '接続状態アイコンの描画
            Dim ei As Image = Nothing
            Dim drawleft As Integer = e.Bounds.Left
            Dim drawNameWidth As Integer = e.Bounds.Width
            Dim drawIconTop As Integer = 0
            If ua.TCPConnect.AceptInfo Then
                ei = AccountImageList.Images("online")
            Else
                ei = AccountImageList.Images("offline")
            End If
            drawIconTop = e.Bounds.Top + (e.Bounds.Height - ei.Height) \ 2 + 1

            g.DrawImage(ei, drawleft, drawIconTop, ei.Width, ei.Height)

            drawleft += ei.Width
            drawNameWidth -= ei.Width

            '暗号化状態アイコンの描画
            If ua.IndividualData.UseEncryptNetwork Then
                Dim drawItem As Image = AccountImageList.Images("encrypt")
                g.DrawImage(drawItem, drawleft, drawIconTop, drawItem.Width, drawItem.Height)

                drawleft += drawItem.Width
                drawNameWidth -= drawItem.Width
            End If

            '名前描画用の設定
            Dim dsf As New System.Drawing.StringFormat
            dsf.FormatFlags = StringFormatFlags.LineLimit
            dsf.LineAlignment = StringAlignment.Center
            dsf.Trimming = StringTrimming.EllipsisCharacter

            '名前の描画後のサイズ計測
            Dim nameSize As SizeF = g.MeasureString(ua.IndividualData.AccountName, Account_ListView.Font)

            '名前の描画
            g.DrawString(ua.IndividualData.AccountName, Account_ListView.Font, NameColor, New RectangleF(drawleft, e.Bounds.Top + 1, drawNameWidth, e.Bounds.Height), dsf)

            'コメントを描画するスペースが余っているか調べる
            If e.Bounds.Width > nameSize.Width + ei.Width Then
                'コメントの描画
                Dim drawCommentRect As RectangleF
                drawCommentRect = New RectangleF(drawleft + nameSize.Width, e.Bounds.Top + 1, drawNameWidth - nameSize.Width, e.Bounds.Height)

                g.DrawString(ua.IndividualData.UserComment, Account_ListView.Font, CommentColor, drawCommentRect, dsf)
            End If

            'リソース開放
            drawingPath.Dispose()
            ei.Dispose()
            dsf.Dispose()
        End If
    End Sub
    Private Sub Account_ListView_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Account_ListView.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left AndAlso Account_ListView.SelectedItems.Count > 0 Then
            'チャットウィンドウが既に存在するか確認
            ShowChatWindow(DirectCast(Account_ListView.SelectedItems(0), AccountListViewItem))
        End If
    End Sub
    Private Sub Account_ListView_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Account_ListView.SizeChanged
        AColumn_Name.Width = Account_ListView.ClientRectangle.Width - 5
        AColumn_Status.Width = 0
    End Sub
    Private Sub Account_ListView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Account_ListView.SelectedIndexChanged
        If Account_ListView.SelectedItems.Count > 0 Then
            '新しい選択項目の項目を表示する
            Dim ua As AccountListViewItem = DirectCast(Account_ListView.SelectedItems(0), AccountListViewItem)

            '転送状態の復元
            Transfer_ListView.Items.Clear()
            If ua.TransportItem.Count > 0 Then
                For i As Integer = 0 To ua.TransportItem.Count - 1
                    ua.TransportItem(i).RegisterListViewItem()
                Next
            End If

            '共有情報の復元
            ua.ShareInfo.AddListView(ua.ShareInfo.YourOpenList)
            Share_PathText.Text = ua.ShareInfo.Share_Path

            SelectUserAccount = ua
        Else
            '全く選択されていない場合、転送リストは全リストを表示する
            Transfer_ListView.Items.Clear()
            If UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 Then
                For i As Integer = 0 To UserAccount.Count - 1
                    If UserAccount(i).TransportItem IsNot Nothing AndAlso UserAccount(i).TransportItem.Count > 0 Then
                        For u As Integer = 0 To UserAccount(i).TransportItem.Count - 1
                            UserAccount(i).TransportItem(u).RegisterListViewItem()
                        Next
                    End If
                Next
            End If

            '共有はクリア
            Share_ListView.Items.Clear()
            Share_PathText.Text = ""

            SelectUserAccount = Nothing
        End If

        'Enabledの変更
        Dim boo As Boolean = Account_ListView.SelectedItems.Count > 0
        Context_Account_Connect.Enabled = boo
        Context_Account_Reconnect.Enabled = boo
        Context_Account_Chat.Enabled = boo
        Context_Account_Edit.Enabled = boo
        Context_Account_Delete.Enabled = boo

        ChangeEnabled()
    End Sub
#End Region

#Region "転送タブ"
    Private Sub Context_Transfer_Open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_Open.Click
        If Transfer_ListView.SelectedItems.Count > 0 Then
            Dim tItem As TransferListViewItem() = GetSelectTransferItem()
            If tItem IsNot Nothing AndAlso tItem.Length > 0 Then
                For i As Integer = 0 To tItem.Length - 1
                    OpenFileOrFolder(tItem(i))
                Next
            End If
        End If
    End Sub
    Private Sub Context_Transfer_Sort_OtherPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_Sort_OtherPoint.Click
        ChangeTransferListSortMode(TColumn_Remote.Index, True, True)
    End Sub
    Private Sub Context_Transfer_Sort_Queue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_Sort_Queue.Click
        ChangeTransferListSortMode(TColumn_Priority.Index, True, True)
    End Sub
    Private Sub Context_Transfer_Sort_FileName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_Sort_FileName.Click
        ChangeTransferListSortMode(TColumn_FileName.Index, True, True)
    End Sub
    Private Sub Context_Transfer_Sort_FullPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_Sort_FullPath.Click
        ChangeTransferListSortMode(TColumn_FullPath.Index, True, True)
    End Sub
    Private Sub Context_Transfer_Sort_Size_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_Sort_Size.Click
        ChangeTransferListSortMode(TColumn_Size.Index, True, True)
    End Sub
    Private Sub Context_Transfer_Sort_Status_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_Sort_Status.Click
        ChangeTransferListSortMode(TColumn_Status.Index, True, True)
    End Sub
    Private Sub Context_Transfer_ListMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_ListMode.Click
        Context_Transfer_ListMode.Checked = Not Context_Transfer_ListMode.Checked

        If Context_Transfer_ListMode.Checked Then
            Transfer_ListView.View = View.Details
        Else
            Transfer_ListView.View = View.Tile
        End If
        appSettings.TransferList_ListMode = Context_Transfer_ListMode.Checked
    End Sub
    Private Sub Context_Transfer_AllSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_AllSelect.Click
        If Transfer_ListView.Items.Count > 0 Then
            For i As Integer = 0 To Transfer_ListView.Items.Count - 1
                Transfer_ListView.Items(i).Selected = True
            Next
        End If
    End Sub

    Private Sub Transfer_Approval_Approval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Transfer_Approval_Approval.Click
        Dim tItem As TransferListViewItem() = GetSelectTransferItem()
        If tItem IsNot Nothing AndAlso tItem.Length > 0 Then
            For i As Integer = 0 To tItem.Length - 1
                If tItem(i).OwnerAccount.TCPConnect.AceptInfo AndAlso tItem(i).ItemState = Arachlex_Transport.Download Then
                    tItem(i).OwnerAccount.UploadApporoval(tItem(i), Nothing)
                End If
            Next
        End If
    End Sub
    Private Sub Transfer_Approval_AllApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Transfer_Approval_AllApproval.Click
        If Transfer_ListView.SelectedItems.Count > 0 Then
            Dim fbd As New FolderBrowserDialog
            fbd.Description = lang.MainForm_DialogTitle_AllApproval
            fbd.RootFolder = Environment.SpecialFolder.Desktop
            fbd.SelectedPath = appSettings.AllSaveDialogPath
            fbd.ShowNewFolderButton = True
            Dim spath As String = Nothing
            If fbd.ShowDialog() = DialogResult.OK Then
                spath = fbd.SelectedPath
                appSettings.AllSaveDialogPath = fbd.SelectedPath
                fbd.Dispose()
            Else
                fbd.Dispose()
                Exit Sub
            End If

            'ダイアログ表示中にアイテムが消される可能性
            Dim tItem As TransferListViewItem() = GetSelectTransferItem()
            If tItem IsNot Nothing AndAlso tItem.Length > 0 Then
                For i As Integer = 0 To tItem.Length - 1
                    If tItem(i).OwnerAccount.TCPConnect.AceptInfo AndAlso tItem(i).ItemState = Arachlex_Transport.Download Then
                        tItem(i).OwnerAccount.UploadApporoval(tItem(i), spath)
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub Transfer_Approval_Resume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Transfer_Approval_Resume.Click
        Dim tItem As TransferListViewItem() = GetSelectTransferItem()
        If tItem IsNot Nothing AndAlso tItem.Length > 0 Then
            Dim i As Integer = 0
            Do
                If tItem(i).OwnerAccount.TCPConnect.AceptInfo AndAlso tItem(i).ItemState = Arachlex_Transport.Download Then
                    tItem(i).OwnerAccount.ResumeApporoval(tItem(i), Nothing)
                End If
                i += 1
            Loop Until i >= tItem.Length
        End If
    End Sub
    Private Sub Transfer_Upload_File_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Transfer_Upload_File.Click
        Dim ofd As New OpenFileDialog()
        ofd.FileName = "*.*"
        ofd.InitialDirectory = appSettings.OpenDialogPath
        ofd.Filter = "File(*.*)|*.*"
        ofd.Title = lang.MainForm_DialogTitle_UploadFile
        ofd.RestoreDirectory = True
        ofd.CheckFileExists = True
        ofd.CheckPathExists = True
        ofd.Multiselect = True
        Dim f_name As String()
        If ofd.ShowDialog() = DialogResult.OK Then
            f_name = ofd.FileNames
            appSettings.OpenDialogPath = f_name(0)
            ofd.Dispose()
        Else
            ofd.Dispose()
            Exit Sub
        End If

        'リストに登録
        If SelectUserAccount Is Nothing Then
            If UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 AndAlso _
            MessageBox.Show(lang.MainForm_Dialog_UploadFileToAllUsers, SoftwareName, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                For i As Integer = 0 To UserAccount.Count - 1
                    If UserAccount(i).TCPConnect.AceptInfo Then
                        For u As Integer = 0 To f_name.Length - 1
                            If Not UserAccount(i).UploadItem(f_name(u), True) Then
                                Exit Sub
                            End If
                        Next
                    End If
                Next
            End If
        ElseIf SelectUserAccount.TCPConnect.AceptInfo Then
            For i As Integer = 0 To f_name.Length - 1
                If Not SelectUserAccount.UploadItem(f_name(i), True) Then
                    Exit For
                End If
            Next
        End If
    End Sub
    Private Sub Transfer_Upload_Folder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Transfer_Upload_Folder.Click
        Dim fbd As New FolderBrowserDialog
        fbd.Description = lang.MainForm_DialogTitle_UploadFolder
        fbd.RootFolder = Environment.SpecialFolder.Desktop
        fbd.SelectedPath = appSettings.OpenDialogPath
        fbd.ShowNewFolderButton = True
        If fbd.ShowDialog(Me) = DialogResult.OK Then
            If SelectUserAccount Is Nothing Then
                If UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 AndAlso _
                MessageBox.Show(lang.MainForm_Dialog_UploadFileToAllUsers, SoftwareName, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    For i As Integer = 0 To UserAccount.Count - 1
                        If UserAccount(i).TCPConnect.AceptInfo Then
                            UserAccount(i).UploadItem(fbd.SelectedPath, True)
                        End If
                    Next
                End If
            ElseIf SelectUserAccount.TCPConnect.AceptInfo Then
                SelectUserAccount.UploadItem(fbd.SelectedPath, True)
            End If
            appSettings.OpenDialogPath = fbd.SelectedPath
        End If
    End Sub
    Private Sub Context_Transfer_QueueUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_QueueUp.Click
        If Transfer_ListView.SelectedItems.Count > 0 Then
            For i As Integer = 0 To Transfer_ListView.SelectedItems.Count - 1
                Dim tItem As TransferListViewItem = DirectCast(Transfer_ListView.SelectedItems(i), TransferListViewItem)
                SyncLock tItem.OwnerAccount.sync_obj
                    If tItem.TransportQueue <> 0 Then
                        tItem.OwnerAccount.TCPConnect.SendPacket(CStr(tItem.TransportQueue - 1), TCP_Header.ItemChangeQueue, tItem.TransportID)
                        tItem.OwnerAccount.SetQueue(tItem, CUShort(tItem.TransportQueue - 1))
                    End If
                End SyncLock

            Next
        End If
    End Sub
    Private Sub Context_Transfer_QueueDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_QueueDown.Click
        If Transfer_ListView.SelectedItems.Count > 0 Then
            For i As Integer = 0 To Transfer_ListView.SelectedItems.Count - 1
                Dim tItem As TransferListViewItem = DirectCast(Transfer_ListView.SelectedItems(i), TransferListViewItem)
                SyncLock tItem.OwnerAccount.sync_obj
                    tItem.OwnerAccount.TCPConnect.SendPacket(CStr(tItem.TransportQueue + 1), TCP_Header.ItemChangeQueue, tItem.TransportID)
                    tItem.OwnerAccount.SetQueue(tItem, CUShort(tItem.TransportQueue + 1))
                End SyncLock
            Next
        End If
    End Sub
    Private Sub Transfer_Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_Stop.Click, Transfer_Stop.Click
        If Transfer_ListView.SelectedItems.Count > 0 Then
            Dim tItem As TransferListViewItem() = GetSelectTransferItem()
            If tItem IsNot Nothing AndAlso tItem.Length > 0 Then
                For i As Integer = 0 To tItem.Length - 1
                    SyncLock tItem(i).OwnerAccount.sync_obj
                        tItem(i).StopTransport()
                        tItem(i).OwnerAccount.TCPConnect.SendPacket(String.Empty, TCP_Header.ItemStop, tItem(i).TransportID)
                    End SyncLock
                Next
            End If
        End If
    End Sub
    Private Sub Transfer_Restart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_ReStart.Click, Transfer_Restart.Click
        If Transfer_ListView.SelectedItems.Count > 0 Then
            Dim tItem As TransferListViewItem() = GetSelectTransferItem()
            If tItem IsNot Nothing AndAlso tItem.Length > 0 Then
                For i As Integer = 0 To tItem.Length - 1
                    SyncLock tItem(i).OwnerAccount.sync_obj
                        tItem(i).RestartTransport()
                        tItem(i).OwnerAccount.TCPConnect.SendPacket(String.Empty, TCP_Header.ItemReStart, tItem(i).TransportID)
                    End SyncLock

                Next
            End If
        End If
    End Sub
    Private Sub Transfer_Interruption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Transfer_Interruption.Click, Transfer_Interruption.Click
        If Transfer_ListView.SelectedItems.Count > 0 Then
            If MessageBox.Show(lang.MainForm_Dialog_Stop, SoftwareName, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                Return
            End If

            Dim tItems As TransferListViewItem() = GetSelectTransferItem()
            If tItems IsNot Nothing AndAlso tItems.Length > 0 Then
                For i As Integer = 0 To tItems.Length - 1
                    Dim tItem As TransferListViewItem = tItems(i)

                    SyncLock tItem.OwnerAccount.sync_obj
                        If Not tItem.TransportingDone Then
                            tItem.OwnerAccount.TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, tItem.TransportID)
                        End If
                        tItem.OwnerAccount.DeleteTransportInfoArray(tItem)
                    End SyncLock
                Next
            End If
        End If
    End Sub
    Private Sub Transfer_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Transfer_Delete.Click, Context_Transfer_Delete.Click
        If Transfer_ListView.SelectedItems.Count > 0 Then
            If MessageBox.Show(lang.MainForm_Dialog_Delete, SoftwareName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                Dim tItems As TransferListViewItem() = GetSelectTransferItem()
                If tItems IsNot Nothing AndAlso tItems.Length > 0 Then
                    For i As Integer = 0 To tItems.Length - 1
                        Dim tItem As TransferListViewItem = tItems(i)
                        SyncLock tItem.OwnerAccount.sync_obj
                            'FileStreamのロックを解除しないと削除できません。
                            tItem.FileStream = Nothing

                            If tItem.ItemState = Arachlex_Transport.Download Then
                                If tItem.ItemAttribute = Arachlex_Item.File Then
                                    If tItem.TransportingDone Then
                                        If IO.File.Exists(tItem.FilePath) Then
                                            IO.File.Delete(tItem.FilePath)
                                        End If
                                    Else
                                        If IO.File.Exists(tItem.FileCachePath) Then
                                            IO.File.Delete(tItem.FileCachePath)
                                        End If
                                    End If
                                ElseIf tItem.ItemAttribute = Arachlex_Item.Folder Then
                                    If IO.Directory.Exists(tItem.FilePath) Then
                                        Dim di As New IO.DirectoryInfo(tItem.FilePath)
                                        di.Delete(True)
                                    End If
                                End If
                            End If

                            '相手に削除をコールするか
                            If Not tItem.TransportingDone Then
                                tItem.OwnerAccount.TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, tItem.TransportID)
                            End If
                            tItem.OwnerAccount.DeleteTransportInfoArray(tItem)
                        End SyncLock

                    Next
                End If
            End If
        End If
    End Sub
    Private Sub Transfer_DoneDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Transfer_DoneDelete.Click
        If SelectUserAccount IsNot Nothing Then
            If SelectUserAccount.TransportItem IsNot Nothing AndAlso SelectUserAccount.TransportItem.Count > 0 Then
                SyncLock SelectUserAccount.sync_obj
                    Dim i As Integer = 0
                    Do
                        If SelectUserAccount.TransportItem(i).TransportingDone Then
                            SelectUserAccount.DeleteTransportInfoArray(SelectUserAccount.TransportItem(i))
                        Else
                            i += 1
                        End If
                    Loop Until i = SelectUserAccount.TransportItem.Count
                End SyncLock
            End If
        Else
            If UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 Then
                Dim i As Integer = 0
                Do
                    Dim ua As AccountListViewItem = UserAccount(i)
                    SyncLock ua.sync_obj
                        If ua.TransportItem IsNot Nothing AndAlso ua.TransportItem.Count > 0 Then
                            Dim u As Integer = 0
                            Do
                                If ua.TransportItem(u).TransportingDone Then
                                    ua.DeleteTransportInfoArray(ua.TransportItem(u))
                                Else
                                    u += 1
                                End If
                            Loop Until u = ua.TransportItem.Count
                        End If
                    End SyncLock

                    i += 1
                Loop Until i = UserAccount.Count
            End If
        End If
    End Sub
    Private Sub Transfer_GoShareList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Transfer_GoShareList.Click
        Panel_Share.Visible = True
        Panel_Transfer.Visible = False
    End Sub

    Private Sub Transfer_ListView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles Transfer_ListView.ColumnClick
        ChangeTransferListSortMode(e.Column, True, True)
    End Sub
    Private Sub Transfer_ListView_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Transfer_ListView.DragDrop
        Dim fileName As String() = CType(e.Data.GetData(DataFormats.FileDrop, False), String())
        If SelectUserAccount IsNot Nothing Then
            For i As Integer = 0 To fileName.Length - 1
                If SelectUserAccount.TCPConnect.AceptInfo AndAlso Not SelectUserAccount.UploadItem(fileName(i), True) Then
                    Exit For
                End If
            Next
        ElseIf UserAccount IsNot Nothing AndAlso UserAccount.Count > 0 Then
            If MessageBox.Show(lang.MainForm_Dialog_UploadFileToAllUsers, SoftwareName, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                For i As Integer = 0 To UserAccount.Count - 1
                    If UserAccount(i).TCPConnect.AceptInfo Then
                        For u As Integer = 0 To fileName.Length - 1
                            If Not UserAccount(i).UploadItem(fileName(u), True) Then
                                Exit Sub
                            End If
                        Next
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub Transfer_ListView_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Transfer_ListView.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub Transfer_ListView_DrawColumnHeader(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewColumnHeaderEventArgs) Handles Transfer_ListView.DrawColumnHeader
        e.DrawDefault = True
    End Sub
    Private Sub Transfer_ListView_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles Transfer_ListView.DrawItem
        'バックカラーの描画
        '少し縮めないと、はみ出して、描画あとが残る場合がある
        Dim tItem As TransferListViewItem = DirectCast(e.Item, TransferListViewItem)
        Dim drawingPath As Drawing2D.GraphicsPath = GetRoundRect(New Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1), 5)
        Dim g As Graphics = e.Graphics
        Dim fnt As Font = tItem.Font

        '------------色------------
        Dim BaseBackColor As Brush = Brushes.White '描画リセット時の色

        Dim SelectedStringColor As Brush = Brushes.Black '選択時の文字色
        Dim UnSelectedStringColor As Brush = Brushes.Black '非選択時の文字色

        Dim SelectedFrameColor As Brush = Brushes.LightSteelBlue '選択時の枠の色
        Dim UnselectedFrameColor As Pen = Pens.CornflowerBlue '非選択時の枠の色

        Dim SelectedProgressFrameColor As Pen = Pens.MediumSeaGreen '選択時のプログレスバーの枠の色
        Dim UnselectedProgressFrameColor As Pen = Pens.MediumSeaGreen '非選択時のプログレスバーの枠の色
        Dim SelectedProgressColor As Brush = Brushes.MediumSeaGreen '選択時のプログレスバーの中の色
        Dim UnselectedProgressColor As Brush = Brushes.MediumSeaGreen '非選択時のプログレスバーの中の色

        Dim ProgressTextColor As Brush = Brushes.Black 'ステータスの文字色

        'Dim SelectedFullPathColor As Brush = Brushes.Black '選択時のフルパスの文字色
        'Dim UnselectedFullPathColor As Brush = Brushes.Black '非選択時のフルパスの文字色
        '------------色------------

        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        '描画面のリセット
        g.FillRectangle(BaseBackColor, e.Bounds)

        If appSettings.TransferList_ListMode Then
            'リストドローの場合

            '色の決定
            Dim strColor As Brush = UnSelectedStringColor
            If tItem.Selected Then
                strColor = SelectedStringColor
            End If

            '枠の描画
            If tItem.Selected Then
                g.FillPath(SelectedFrameColor, drawingPath)
            End If

            Dim left As Integer = e.Bounds.Left
            Dim sf As New StringFormat
            sf.Alignment = StringAlignment.Near
            sf.LineAlignment = StringAlignment.Center
            sf.Trimming = StringTrimming.EllipsisCharacter
            sf.FormatFlags = StringFormatFlags.LineLimit

            For i As Integer = 0 To Transfer_ListView.Columns.Count - 1
                Select Case i
                    Case TColumn_Remote.DisplayIndex
                        g.DrawString(tItem.OwnerAccount.IndividualData.AccountName, fnt, strColor, New RectangleF(left, e.Bounds.Top, TColumn_Remote.Width, e.Bounds.Height), sf)
                        left += TColumn_Remote.Width
                    Case TColumn_Priority.DisplayIndex
                        g.DrawString(CStr(tItem.TransportQueue), fnt, strColor, New RectangleF(left, e.Bounds.Top, TColumn_Priority.Width, e.Bounds.Height), sf)
                        left += TColumn_Priority.Width
                    Case TColumn_FileName.DisplayIndex
                        g.DrawString(tItem.FileName, fnt, strColor, New RectangleF(left, e.Bounds.Top, TColumn_FileName.Width, e.Bounds.Height), sf)
                        left += TColumn_FileName.Width
                    Case TColumn_FullPath.DisplayIndex
                        g.DrawString(tItem.FilePath, fnt, strColor, New RectangleF(left, e.Bounds.Top, TColumn_FullPath.Width, e.Bounds.Height), sf)
                        left += TColumn_FullPath.Width
                    Case TColumn_Size.DisplayIndex
                        g.DrawString(RetFileSize(tItem.FileSize), fnt, strColor, New RectangleF(left, e.Bounds.Top, TColumn_Size.Width, e.Bounds.Height), sf)
                        left += TColumn_Size.Width
                    Case TColumn_Status.DisplayIndex
                        If tItem.Transporting Then
                            'プログラスバーを描画する場合

                            '描画する色を決定する
                            Dim ProgressFrame As Pen
                            Dim ProgressColor As Brush
                            If e.Item.Selected Then
                                ProgressFrame = SelectedProgressFrameColor
                                ProgressColor = SelectedProgressColor
                            Else
                                ProgressFrame = UnselectedProgressFrameColor
                                ProgressColor = UnselectedProgressColor
                            End If

                            '描画面の大きさ
                            Dim height As Integer = e.Bounds.Height - 2
                            Dim width As Integer = TColumn_Status.Width - 2
                            Dim top As Integer = e.Bounds.Top + 1

                            '外枠を描画
                            g.DrawRectangle(ProgressFrame, New Rectangle(left, top, width, height))

                            '中身を描画
                            Dim w_size As Integer '進捗パーセントをプログレスバーの横幅換算したもの
                            If tItem.FileSize = 0 Then
                                w_size = width
                            Else
                                w_size = CInt(width * (tItem.FileSeek / tItem.FileSize))
                            End If

                            '実際に描画する
                            If tItem.ItemAttribute = Arachlex_Item.File Then
                                'ファイル時
                                g.FillRectangle(ProgressColor, New Rectangle(left, top, w_size, height))
                            Else
                                Dim w_folder As Integer
                                If tItem.FolderAllFiles = 0 Then
                                    w_folder = width
                                Else
                                    w_folder = CInt(width * (tItem.FolderDoneFiles / tItem.FolderAllFiles))
                                End If
                                'フォルダ時
                                g.FillRectangle(ProgressColor, New Rectangle(left, top, w_folder, height \ 2))
                                'フォルダのファイルの転送状況
                                g.FillRectangle(ProgressColor, New Rectangle(left, top + height \ 2, w_size, height \ 2))
                            End If
                        End If

                        'ステータスのテキストを描画する
                        sf.Alignment = StringAlignment.Center
                        g.DrawString(tItem.GetStatusStr(lang), fnt, strColor, New RectangleF(left, e.Bounds.Top, TColumn_Status.Width, e.Bounds.Height), sf)
                        sf.Alignment = StringAlignment.Near
                        left += TColumn_Status.Width
                End Select
            Next
        Else
            'ボックスドローの場合

            '枠の描画
            Dim StringColor As Brush
            If e.Item.Selected Then
                StringColor = SelectedStringColor
                g.FillPath(SelectedFrameColor, drawingPath)
            Else
                StringColor = UnSelectedStringColor
                g.DrawPath(UnselectedFrameColor, drawingPath)
            End If

            'アイコンイメージの描画
            Dim iconImage As Image = Nothing
            If tItem.ItemAttribute = Arachlex_Item.File Then
                iconImage = Share_LargeImageList.Images(AddIconToImageList(IO.Path.GetExtension(tItem.FileName)))
            Else
                iconImage = Share_LargeImageList.Images("folder")
            End If
            Dim IconDrawLocation As New Point(e.Bounds.Left + 5, e.Bounds.Top + (e.Bounds.Height - iconImage.Height) \ 2)
            g.DrawImage(iconImage, IconDrawLocation.X, IconDrawLocation.Y, iconImage.Width, iconImage.Height)

            '相手(テキスト)描画用の設定
            Dim dsf As New System.Drawing.StringFormat
            dsf.FormatFlags = StringFormatFlags.LineLimit
            dsf.LineAlignment = StringAlignment.Center
            dsf.Trimming = StringTrimming.EllipsisCharacter

            '描画するテキストの一時保存場所
            Dim str As String = ""

            '相手(テキスト)を描画する高さ
            Dim DrawHeight As Integer = e.Bounds.Height \ 3
            Dim DrawTop As Integer = e.Bounds.Top + 6

            '相手(テキスト)描画時の大きさを計測
            str = tItem.OwnerAccount.IndividualData.AccountName
            Dim MaxDrawTextSize As New SizeF((e.Bounds.Width - iconImage.Width) \ 3, DrawHeight)
            Dim stringSize As SizeF = g.MeasureString(str, fnt, MaxDrawTextSize, dsf)
            '相手(テキスト)の描画
            g.DrawString(str, fnt, StringColor, New RectangleF(e.Bounds.Left + iconImage.Width + 6, DrawTop, stringSize.Width, DrawHeight), dsf)

            '優先順位を描画
            If tItem.TransportingDone Then
                str = "-"
            Else
                str = CStr(tItem.TransportQueue)
            End If
            Dim QueueTextDrawSize As SizeF = g.MeasureString(str, fnt, MaxDrawTextSize)
            g.DrawString(str, fnt, Brushes.Red, e.Bounds.Left + iconImage.Width + stringSize.Width + 6, DrawTop)

            '座標定義
            Dim DrawLeft As Integer = CInt(e.Bounds.Left + iconImage.Width + QueueTextDrawSize.Width + stringSize.Width + 7)
            Dim DrawWidth As Integer = CInt(e.Bounds.Width - stringSize.Width - iconImage.Width - QueueTextDrawSize.Width - 13)
            DrawTop -= 1

            'ステータスの描画
            If tItem.Transporting Then
                'プログレスバーを伴なう場合

                '描画する色を決定する
                Dim ProgressFrame As Pen
                Dim ProgressColor As Brush
                If e.Item.Selected Then
                    ProgressFrame = SelectedProgressFrameColor
                    ProgressColor = SelectedProgressColor
                Else
                    ProgressFrame = UnselectedProgressFrameColor
                    ProgressColor = UnselectedProgressColor
                End If

                '外枠を描画
                g.DrawRectangle(ProgressFrame, New Rectangle(DrawLeft, DrawTop, DrawWidth, DrawHeight))

                '進捗状況を設定
                Dim w_size As Integer
                If tItem.FileSize = 0 Then
                    w_size = DrawWidth
                Else
                    w_size = CInt(DrawWidth * (tItem.FileSeek / tItem.FileSize))
                End If
                If tItem.ItemAttribute = Arachlex_Item.File Then
                    'ファイル時
                    g.FillRectangle(ProgressColor, New Rectangle(DrawLeft, DrawTop, w_size, DrawHeight))
                Else
                    Dim w_folder As Integer
                    If tItem.FolderAllFiles = 0 Then
                        w_folder = DrawWidth
                    Else
                        w_folder = CInt(DrawWidth * (tItem.FolderDoneFiles / tItem.FolderAllFiles))
                    End If
                    'フォルダ時
                    g.FillRectangle(ProgressColor, New Rectangle(DrawLeft, DrawTop, w_folder, DrawHeight \ 2))
                    'フォルダのファイルの転送状況
                    g.FillRectangle(ProgressColor, New Rectangle(DrawLeft, DrawTop + DrawHeight \ 2, w_size, DrawHeight \ 2))
                End If
            End If
            dsf.Alignment = StringAlignment.Center

            'ステータスを描画
            g.DrawString(tItem.GetStatusStr(lang), fnt, ProgressTextColor, New RectangleF(DrawLeft, DrawTop + 1, CSng(DrawWidth), DrawHeight), dsf)

            '幅
            Dim OtherWidth As Integer = e.Bounds.Width - iconImage.Width - 6
            Dim OtherLeft As Integer = e.Bounds.Left + iconImage.Width + 6

            'DrawTopの再定義
            DrawTop = CInt(DrawTop + stringSize.Height + 4)

            dsf.Alignment = StringAlignment.Far
            'ファイルサイズの描画
            If tItem.ItemAttribute = Arachlex_Item.Folder Then
                If tItem.TransportStatus = TransferListViewItem.ItemStatus.Approvaled Then
                    str = tItem.FolderDoneFiles & "/" & tItem.FolderAllFiles
                Else
                    str = lang.MainForm_Drawing_Transport_Folder
                End If
            ElseIf tItem.ItemAttribute = Arachlex_Item.File Then
                str = RetFileSize(tItem.FileSize)
            End If
            g.DrawString(str, fnt, StringColor, New RectangleF(OtherLeft, DrawTop, OtherWidth, DrawHeight), dsf)

            'ファイルサイズ描画後のサイズを計測
            Dim FileSizeSize As SizeF = g.MeasureString(str, fnt)
            'ファイル名の描画
            dsf.Alignment = StringAlignment.Near
            g.DrawString(tItem.FileName & " - " & tItem.FilePath, fnt, StringColor, New RectangleF(OtherLeft, DrawTop, OtherWidth - FileSizeSize.Width, DrawHeight), dsf)

            '開放
            drawingPath.Dispose()
            iconImage.Dispose()
            dsf.Dispose()
        End If
    End Sub
    Private Sub Transfer_ListView_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Transfer_ListView.MouseDoubleClick
        Dim tItem As TransferListViewItem() = GetSelectTransferItem()
        If tItem IsNot Nothing AndAlso tItem.Length > 0 Then
            For i As Integer = 0 To tItem.Length - 1
                If tItem(i).OwnerAccount.TCPConnect.AceptInfo AndAlso tItem(i).ItemState = Arachlex_Transport.Download AndAlso tItem(i).TransportStatus = TransferListViewItem.ItemStatus.NotApproval Then
                    tItem(i).OwnerAccount.UploadApporoval(tItem(i), Nothing)
                End If
            Next
        End If
    End Sub
    Private Sub Transfer_ListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Transfer_ListView.SelectedIndexChanged
        '選択アイテムと合計アイテムの更新
        Context_Transfer_Open.Enabled = Transfer_ListView.SelectedItems.Count > 0
        ChangeEnabled()
    End Sub
    Private Sub Transfer_ListView_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Transfer_ListView.SizeChanged
        If Transfer_ListView.ClientRectangle.Width - 5 > 0 Then
            Transfer_ListView.TileSize = New Size(Transfer_ListView.ClientRectangle.Width - 3, 43)
        End If
    End Sub
#End Region

#Region "共有タブ"
    Private Sub Context_Share_Sort_FileName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Share_Sort_FileName.Click
        ChangeShareListSortMode(SColumn_Filename.Index, True, True)
    End Sub
    Private Sub Context_Share_Sort_Extension_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Share_Sort_Extension.Click
        ChangeShareListSortMode(SColumn_Extension.Index, True, True)
    End Sub
    Private Sub Context_Share_Sort_Size_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Share_Sort_Size.Click
        ChangeShareListSortMode(SColumn_Size.Index, True, True)
    End Sub
    Private Sub Context_Share_Sort_TimeStamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Share_Sort_TimeStamp.Click
        ChangeShareListSortMode(SColumn_TimeStamp.Index, True, True)
    End Sub
    Private Sub Context_Share_AllSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Share_AllSelect.Click
        If Share_ListView.Items.Count > 0 Then
            For i As Integer = 0 To Share_ListView.Items.Count - 1
                Share_ListView.Items(i).Selected = True
            Next
        End If
    End Sub

    Private Sub Share_Download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Share_Download.Click, Share_Download.Click
        If SelectUserAccount IsNot Nothing AndAlso SelectUserAccount.TCPConnect.AceptInfo AndAlso Share_ListView.SelectedItems.Count > 0 Then
            For i As Integer = 0 To Share_ListView.SelectedItems.Count - 1
                Dim sInfo As FileInfoListViewItem = DirectCast(Share_ListView.SelectedItems(i), FileInfoListViewItem)
                SelectUserAccount.TCPConnect.SendPacket(CStr(sInfo.ID), TCP_Header.Share_Download, UShort.MinValue)
            Next
        End If
    End Sub
    Private Sub Share_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Share_Up.Click, Share_Up.Click
        If SelectUserAccount IsNot Nothing AndAlso SelectUserAccount.TCPConnect.AceptInfo Then
            SelectUserAccount.TCPConnect.SendPacket(CStr(CInt(ShareClass.ShareKind.TOP)), TCP_Header.Share_GetList, UShort.MinValue)
        End If
    End Sub
    Private Sub Share_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Share_Refresh.Click, Share_Refresh.Click
        If SelectUserAccount IsNot Nothing AndAlso SelectUserAccount.TCPConnect.AceptInfo Then
            SelectUserAccount.TCPConnect.SendPacket(CStr(CInt(ShareClass.ShareKind.Refresh)), TCP_Header.Share_GetList, UShort.MinValue)
        End If
    End Sub
    Private Sub Share_Root_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Share_Root.Click, Share_Root.Click
        If SelectUserAccount IsNot Nothing AndAlso SelectUserAccount.TCPConnect.AceptInfo Then
            SelectUserAccount.TCPConnect.SendPacket(CStr(CInt(ShareClass.ShareKind.ROOT)), TCP_Header.Share_GetList, UShort.MinValue)
        End If
    End Sub
    Private Sub Share_GoTransferList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Share_GoTransferList.Click
        Panel_Share.Visible = False
        Panel_Transfer.Visible = True
    End Sub

    Private Sub ShareToolStrip_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShareToolStrip.SizeChanged
        Share_PathText.Width = _
        ShareToolStrip.Width - (Share_Download.Width + Share_Up.Width + Share_Refresh.Width + Share_Root.Width + Share_GoTransferList.Width + 10)
    End Sub
    Private Sub Share_ListView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles Share_ListView.ColumnClick
        ChangeShareListSortMode(e.Column, True, True)
    End Sub
    Private Sub Share_ListView_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Share_ListView.MouseDoubleClick
        If Share_ListView.SelectedItems.Count > 0 AndAlso SelectUserAccount IsNot Nothing AndAlso SelectUserAccount.TCPConnect.AceptInfo Then
            Dim nlist As FileInfoListViewItem = DirectCast(Share_ListView.SelectedItems(0), FileInfoListViewItem)
            If nlist.Attribute = Arachlex_Item.Folder Then
                SelectUserAccount.TCPConnect.SendPacket(ShareClass.ShareKind.NORMAL & vbCrLf & nlist.ID, TCP_Header.Share_GetList, 0)
            Else
                SelectUserAccount.TCPConnect.SendPacket(CStr(nlist.ID), TCP_Header.Share_Download, 0)
            End If
        End If
    End Sub
    Private Sub Share_ListView_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Share_ListView.MouseDown
        If e.Button = Windows.Forms.MouseButtons.XButton1 Then
            Share_Up_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub Share_ListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Share_ListView.SelectedIndexChanged
        ChangeEnabled()
    End Sub
#End Region
End Class