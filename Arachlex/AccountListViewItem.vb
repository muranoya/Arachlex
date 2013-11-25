Imports Arachlex.DefinitionClass
Imports Arachlex.TCPNetworkClass
''' <summary>
''' 接続しているユーザーの情報を保持するためのクラスです
''' </summary>
''' <remarks></remarks>
Public Class AccountListViewItem
    Inherits ListViewItem

    Public ControledForm As Form1
    Public sync_obj As Object = New Object
    Public _bgConnect As System.Threading.Thread = Nothing
    Public _bgMSGRec As System.Threading.Thread = Nothing

#Region "プロトコル処理"
    Private Delegate Sub InvokeProtocolDelegate(ByVal lth As TCP_Header, ByVal id As UShort, ByVal msg As String)
    Private Sub InvokeProtocol(ByVal lth As TCP_Header, ByVal id As UShort, ByVal msg As String)
        Select Case lth
            Case TCP_Header.ChatMSG 'チャットメッセージを受け取った
                PRC_ChatMSG(msg)
            Case TCP_Header.ItemUpload 'アイテムがアップロードされた
                PRC_ItemUpload(id, msg)
            Case TCP_Header.ItemApp 'アイテムを承認する
                PRC_ItemApp(id, msg)
            Case TCP_Header.ItemStop 'アイテムを一時停止する
                PRC_ItemStop(id)
            Case TCP_Header.ItemReStart 'アイテムの転送を再開する
                PRC_ItemRestart(id)
            Case TCP_Header.ItemDelete 'アイテムを削除する
                PRC_ItemDelete(id)
            Case TCP_Header.ItemChangeQueue 'アイテムの優先順位を変更する
                PRC_ItemChangeQueue(id, msg)
            Case TCP_Header.MakeFolder 'フォルダーを作成します
                PRC_MakeFolder(id, msg)
            Case TCP_Header.NextFolderFile '次の送信ファイルのセット
                PRC_NextFolderFile(id, msg)
            Case TCP_Header.NextFolderFileOk 'フォルダー転送でセットしていたファイルの転送が完了した
                PRC_NextFolderFileOk(id)
            Case TCP_Header.FolderDone 'フォルダの転送が完了した
                PRC_FolderDone(id)
            Case TCP_Header.SetSeek 'アップロードファイルのシーク位置を調整します
                PRC_SetSeek(id, msg)
            Case TCP_Header.SendFileDone 'ファイルの送信が完了した
                PRC_SendFileDone(id)
            Case TCP_Header.SyncItem 'アイテムの同期処理
                PRC_SyncItem(msg)
            Case TCP_Header.MyVersion 'バージョンチェック
                PRC_MyVersion(msg)
            Case TCP_Header.ChangeComment 'コメントの変更
                PRC_ChangeComment(msg)
            Case TCP_Header.ChangeAccountName 'アカウント名の変更
                PRC_ChangeAccountName(msg)
            Case TCP_Header.ChangePort 'ポート番号の変更
                PRC_ChangePort(msg)
            Case TCP_Header.Share_GetList 'ファイル共有リストを取得する
                PRC_Share_GetList(msg)
            Case TCP_Header.Share_Download '共有リストからアイテムを選択しダウンロードする
                PRC_Share_Download(msg)
            Case TCP_Header.Share_List '共有ファイルのリスト
                ShareInfo.ShowListItem(msg)
        End Select
    End Sub

    ''' <summary>
    ''' チャットメッセージを受信する
    ''' </summary>
    ''' <param name="msg">チャットのメッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_ChatMSG(ByVal msg As String)
        'メッセージの解析
        Dim c_msg() As String = Split(msg, vbCrLf, 2)
        'チャットに追加
        ApendChat(c_msg(1), c_msg(0), Now, False)
        '通知
        If ControledForm.appSettings.Notify_ReceiveMSG AndAlso Form.ActiveForm Is Nothing Then
            ControledForm.DoNotify(ControledForm.lang.AccountClass_NotifyMSG_Chat(IndividualData.AccountName), c_msg(1))
        End If
    End Sub

    ''' <summary>
    ''' アイテムのアップロード
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="msg">メッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_ItemUpload(ByVal ID As UShort, ByVal msg As String)
        Dim nInfo As TransferListViewItem = Nothing
        SyncLock sync_obj
            Dim fname As String() = Split(msg, vbCrLf, 4)

            'ファイルかフォルダか
            Dim li As Arachlex_Item = CType(fname(0), Arachlex_Item)
            Dim que_number As UShort = CType(fname(1), UShort)
            Dim boo_View As Boolean = (ControledForm.SelectUserAccount Is Nothing) OrElse (ControledForm.SelectUserAccount Is Me)
            If li = Arachlex_Item.File Then
                '新しいインスタンスの作成
                nInfo = New TransferListViewItem(Me, boo_View, ID, que_number, CLng(fname(3)), "", fname(2), li, Arachlex_Transport.Download)
            ElseIf li = Arachlex_Item.Folder Then
                '新しいインスタンスの作成
                nInfo = New TransferListViewItem(Me, boo_View, ID, que_number, 0, "", fname(2), li, Arachlex_Transport.Download)
                nInfo.FolderAllFiles = CInt(fname(3))
            End If
            AddTransportInfoArray(nInfo)
            '通知する
            If ControledForm.appSettings.Notify_Upload Then
                ControledForm.DoNotify(ControledForm.lang.AccountClass_NotifyMSG_Upload(IndividualData.AccountName), fname(2))
            End If
        End SyncLock
        '自動で承認する場合
        If IndividualData.DownloadAutoSave Then
            UploadApporoval(nInfo, IndividualData.DownloadAutoSavePath)
        End If
    End Sub

    ''' <summary>
    ''' アイテムの転送を承認します
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="msg">メッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_ItemApp(ByVal ID As UShort, ByVal msg As String)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Upload Then
                'ファイルの承認
                If tInfo.ItemAttribute = Arachlex_Item.File Then
                    If tInfo.FileSize > CLng(msg) Then
                        Dim GSIb As Boolean = GetSendItems()
                        tInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled
                        tInfo.FileSeek = CLng(msg)
                        If Not GSIb Then
                            SendFileData()
                        End If
                    Else
                        DeleteTransportInfoArray(tInfo)
                        TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
                    End If
                ElseIf tInfo.ItemAttribute = Arachlex_Item.Folder Then
                    tInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled
                    '相手にフォルダ作成を指示する
                    Dim subFolders As String() = System.IO.Directory.GetDirectories(tInfo.FilePath, "*", System.IO.SearchOption.AllDirectories)
                    If subFolders IsNot Nothing AndAlso subFolders.Length > 0 Then
                        Dim sb As New System.Text.StringBuilder
                        For i As Integer = 0 To subFolders.Length - 1
                            sb.Append(Replace(subFolders(i), tInfo.FilePath, "") & vbCrLf)
                        Next
                        TCPConnect.SendPacket(sb.ToString, TCP_Header.MakeFolder, ID)
                    End If
                    NextFolderFile(tInfo)
                End If
            Else
                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' アイテムの転送を一時停止します
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <remarks></remarks>
    Private Sub PRC_ItemStop(ByVal ID As UShort)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing Then
                tInfo.StopTransport()
            Else
                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' アイテムの転送を再開します
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <remarks></remarks>
    Private Sub PRC_ItemRestart(ByVal ID As UShort)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing Then
                tInfo.RestartTransport()
            Else
                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' アイテムを削除します
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <remarks></remarks>
    Private Sub PRC_ItemDelete(ByVal ID As UShort)
        DeleteTransportInfoArray(GetInfoFromID(ID))
    End Sub

    ''' <summary>
    ''' アイテムの優先順位を変更します
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="msg">メッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_ItemChangeQueue(ByVal ID As UShort, ByVal msg As String)
        Dim tItem As TransferListViewItem = Me.GetInfoFromID(ID)
        If tItem IsNot Nothing Then
            SetQueue(tItem, CUShort(msg))
        End If
    End Sub

    ''' <summary>
    ''' フォルダを作成します
    ''' </summary>
    ''' <param name="ID">対象のID</param>
    ''' <param name="msg">メッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_MakeFolder(ByVal ID As UShort, ByVal msg As String)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Download AndAlso tInfo.ItemAttribute = Arachlex_Item.Folder Then
                'フォルダを作る
                Dim sb() As String = Split(msg, vbCrLf)
                If sb IsNot Nothing AndAlso sb.Length > 0 Then
                    Dim downFolderPath As String = tInfo.FilePath
                    For i As Integer = 0 To sb.Length - 2
                        IO.Directory.CreateDirectory(downFolderPath & sb(i))
                    Next
                End If
            Else
                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' 次のフォルダー内のファイルのセット
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="msg">メッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_NextFolderFile(ByVal ID As UShort, ByVal msg As String)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Download AndAlso tInfo.ItemAttribute = Arachlex_Item.Folder Then

                'メッセージ解析
                Dim dmsg() As String = Split(msg, vbCrLf, 3)

                'ファイルストリームの開放
                tInfo.FileStream = Nothing
                tInfo.FileCachePath = tInfo.FilePath & dmsg(0)

                '配置場所のディレクトリが存在しなければ作る
                If Not IO.Directory.Exists(IO.Path.GetDirectoryName(tInfo.FileCachePath)) Then
                    IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(tInfo.FileCachePath))
                End If

                'ファイルストリームを作成する
                If tInfo.CreateStream(tInfo.FileCachePath, False) Then
                    '新しいファイルのサイズ
                    Dim fi As New IO.FileInfo(tInfo.FileCachePath)
                    '転送する必要があるか確かめる
                    If fi.Length < CLng(dmsg(1)) Then
                        'シーク位置を調整する必要があるか
                        If fi.Length <> 0 Then
                            TCPConnect.SendPacket(CStr(fi.Length), TCP_Header.SetSeek, tInfo.TransportID)
                        End If
                        tInfo.FolderSetedInItem = True
                        tInfo.FileSize = CLng(dmsg(1))
                        tInfo.FileSeek = fi.Length
                        tInfo.FolderDoneFiles = CInt(dmsg(2))
                        TCPConnect.SendPacket(String.Empty, TCP_Header.NextFolderFileOk, ID)
                    Else
                        tInfo.FileStream = Nothing
                        TCPConnect.SendPacket(String.Empty, TCP_Header.SendFileDone, ID)
                    End If
                Else
                    TCPConnect.SendPacket(String.Empty, TCP_Header.SendFileDone, ID)
                End If
            Else
                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' フォルダ送信時の次のファイルをセットします
    ''' </summary>
    ''' <param name="ID">対象のID</param>
    ''' <remarks></remarks>
    Private Sub PRC_NextFolderFileOk(ByVal ID As UShort)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Upload AndAlso tInfo.ItemAttribute = Arachlex_Item.Folder Then
                Dim nDB As Boolean = GetSendItems()
                tInfo.FolderSetedInItem = True
                If Not nDB Then
                    SendFileData()
                End If
            Else
                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' フォルダ転送が終了
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <remarks></remarks>
    Private Sub PRC_FolderDone(ByVal ID As UShort)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Download AndAlso tInfo.ItemAttribute = Arachlex_Item.Folder Then
                tInfo.TransportStatus = TransferListViewItem.ItemStatus.Done
            Else
                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' アップロードアイテムのシーク位置を調整します
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="msg">メッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_SetSeek(ByVal ID As UShort, ByVal msg As String)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Upload Then
                tInfo.FileSeek = CLng(msg) 'シークの調整
            Else
                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' ファイルデータの受信
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="bytes">データ</param>
    ''' <remarks></remarks>
    Private Sub PRC_SendFile(ByVal ID As UShort, ByVal bytes() As Byte)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            Try
                If tInfo Is Nothing OrElse tInfo.ItemState <> Arachlex_Transport.Download Then
                    'アイテムが存在し、ダウンロード方向のアイテムであるか調べる
                    Throw New Exception("Item does not exist or have different forward direction")
                ElseIf Not tInfo.NowTransporting Then '転送中のアイテムか調べる
                    Return
                End If

                '転送データのシークポジションを読み取る
                Dim SeekPosi As Long = BitConverter.ToInt64(bytes, 0)
                If SeekPosi <> tInfo.FileSeek Then
                    'シーク位置がずれている場合、相手のシーク位置を調整
                    TCPConnect.SendPacket(CStr(tInfo.FileSeek), TCP_Header.SetSeek, tInfo.TransportID)
                    Return
                End If

                'シーク位置の調整
                tInfo.FileStream.Position = tInfo.FileSeek
                '書き込む
                tInfo.FileStream.Write(bytes, 8, bytes.Length - 8) '8Byte引くのは、先頭8バイトはデータとは無関係な転送時に使うヘッダだから
                '次のシーク位置を記憶
                tInfo.FileSeek += (bytes.Length - 8)
                tInfo.FileStream.Flush()
                '完了したか調べる
                If tInfo.FileSeek = tInfo.FileSize Then
                    If tInfo.ItemAttribute = Arachlex_Item.Folder Then
                        tInfo.FileStream = Nothing
                        tInfo.FolderSetedInItem = False
                    Else
                        tInfo.TransportStatus = TransferListViewItem.ItemStatus.Done
                        '一時ファイルから通常ファイルに名前変更
                        IO.File.Move(tInfo.FileCachePath, GetRepetitionFileName(tInfo.FilePath))
                    End If
                    '相手に完了を通知
                    TCPConnect.SendPacket(String.Empty, TCP_Header.SendFileDone, ID)
                End If
            Catch ex As Exception
                If tInfo IsNot Nothing Then
                    tInfo.TransportStatus = TransferListViewItem.ItemStatus.ErrorDone
                End If
                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
            Finally
                TCPConnect.SendPacket(String.Empty, TCP_Header.SendFileOk, ID)
            End Try
        End SyncLock
    End Sub

    ''' <summary>
    ''' アイテム送信が終了
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <remarks></remarks>
    Private Sub PRC_SendFileDone(ByVal ID As UShort)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Upload Then
                If tInfo.ItemAttribute = Arachlex_Item.Folder Then
                    tInfo.FolderSetedInItem = False
                    NextFolderFile(tInfo)
                Else
                    tInfo.TransportStatus = TransferListViewItem.ItemStatus.Done
                End If
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' アイテムを同期します
    ''' </summary>
    ''' <param name="msg">メッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_SyncItem(ByVal msg As String)
        SyncLock sync_obj
            '相手のアップロードリスト
            Dim uStr As New Generic.List(Of String)
            uStr.AddRange(Split(msg, vbCrLf))

            If "nothing".Equals(msg, StringComparison.OrdinalIgnoreCase) OrElse uStr.Count = 0 Then
                '相手のアップロードリストに何もない場合
                If TransportItem IsNot Nothing AndAlso TransportItem.Count > 0 Then
                    Dim i As Integer = 0
                    Do
                        If TransportItem(i).TransportingDone OrElse TransportItem(i).ItemState = Arachlex_Transport.Upload Then
                            i += 1
                        Else
                            DeleteTransportInfoArray(TransportItem(i))
                        End If
                    Loop Until TransportItem Is Nothing OrElse i = TransportItem.Count
                End If
            ElseIf TransportItem Is Nothing OrElse TransportItem.Count = 0 Then
                '自身の転送リストリストが空の場合、相手に全てのアイテムの削除を要請
                For i As Integer = 0 To uStr.Count - 1
                    If uStr(i) IsNot Nothing Then
                        Dim ustr_id() As String = Split(uStr(i), vbCr, 2)
                        If ustr_id.Length = 2 Then
                            TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, CUShort(ustr_id(0)))
                        End If
                    End If
                Next
            Else '自身のリストと照合
                Dim MachItem As New Generic.List(Of TransferListViewItem) '適合リスト
                For i As Integer = 0 To uStr.Count - 1
                    If uStr(i) IsNot Nothing Then
                        Dim ustr_id() As String = Split(uStr(i), vbCr, 2)
                        If ustr_id.Length = 2 Then
                            'アイテムが存在するか調べる
                            Dim tInfo As TransferListViewItem = GetInfoFromID(CUShort(ustr_id(0)))
                            If tInfo IsNot Nothing AndAlso tInfo.FileSize = CLng(ustr_id(1)) Then
                                '相手のアイテムと一致すれば、アイテムを適合リストに追加する
                                MachItem.Add(tInfo)
                            Else
                                '一致しなかった場合
                                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, CUShort(ustr_id(0)))
                            End If
                        End If
                    End If
                Next

                '適合リストと自身のリストを比較する
                If MachItem.Count = 0 Then
                    '適合リストが空の場合、自身のダウンロード中アイテム全てを削除する
                    Dim h As Integer = 0
                    Do
                        If Not TransportItem(h).TransportingDone AndAlso TransportItem(h).ItemState = Arachlex_Transport.Download Then
                            DeleteTransportInfoArray(TransportItem(h))
                        Else
                            h += 1
                        End If
                    Loop Until h = TransportItem.Count
                Else
                    '適合リストに存在しない自身のダウンロード中のアイテムを全て削除する
                    Dim h As Integer = 0
                    Do
                        If TransportItem(h).ItemState = Arachlex_Transport.Download AndAlso _
                        Not TransportItem(h).TransportingDone AndAlso (Not MachItem.IndexOf(TransportItem(h)) >= 0) Then
                            DeleteTransportInfoArray(TransportItem(h))
                        Else
                            h += 1
                        End If
                    Loop Until h = TransportItem.Count
                End If
                TCPConnect.SendPacket(String.Empty, TCP_Header.SendFileOk, 0) '終了
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' バージョンチェック
    ''' </summary>
    ''' <param name="msg">メッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_MyVersion(ByVal msg As String)
        'バージョン交換
        Dim ppv() As String = Split(msg, vbCrLf, 2) '0{ソフトフルネーム},1{プロトコルバージョン}
        TCPConnect.SoftVersion = ppv(0)
        TCPConnect.SoftProtocol = CInt(ppv(1))
    End Sub

    ''' <summary>
    ''' コメントの更新
    ''' </summary>
    ''' <param name="msg">コメント</param>
    ''' <remarks></remarks>
    Private Sub PRC_ChangeComment(ByVal msg As String)
        '更新を通知する
        IndividualData.UserComment = msg

        'アカウントリストの更新
        ControledForm.AccountListViewRedraw(Me.Index)
    End Sub

    ''' <summary>
    ''' アカウント名の更新
    ''' </summary>
    ''' <param name="msg">アカウント名</param>
    ''' <remarks></remarks>
    Private Sub PRC_ChangeAccountName(ByVal msg As String)
        If Not ControledForm.ExistAccountName(msg) Then
            IndividualData.AccountName = msg
            ControledForm.AccountListViewRedraw(Me.Index)
        End If
    End Sub

    ''' <summary>
    ''' ポートの変更
    ''' </summary>
    ''' <param name="msg">ポート番号</param>
    ''' <remarks></remarks>
    Private Sub PRC_ChangePort(ByVal msg As String)
        Try
            IndividualData.ConnectPort = CType(msg, UShort)
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' 指定されたパスの内容を取得します
    ''' </summary>
    ''' <param name="msg">メッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_Share_GetList(ByVal msg As String)
        If IndividualData.Share_Use Then
            Dim m() As String = Split(msg, vbCrLf, 2)
            Dim shareID As Integer = -1
            If m.Length > 1 Then
                shareID = CInt(m(1))
            End If
            TCPConnect.SendPacket(ShareInfo.GetOpenList(CType(m(0), ShareClass.ShareKind), shareID), TCP_Header.Share_List, UShort.MinValue)
        End If
    End Sub

    ''' <summary>
    ''' ファイル共有リストの指定パスからアイテムをダウンロードします
    ''' </summary>
    ''' <param name="msg">メッセージ</param>
    ''' <remarks></remarks>
    Private Sub PRC_Share_Download(ByVal msg As String)
        If IndividualData.Share_Use Then
            Dim fp As ShareClass.MyShareList = ShareInfo.GetMyOpenInfoFromID(CInt(msg))
            If fp IsNot Nothing Then
                UploadItem(fp.FullPath, False)
            End If
        End If
    End Sub
#End Region

#Region "スレッド"
    ''' <summary>
    ''' bgConnectスレッドを開始します。既にスレッドが動いている場合、何もしません
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Start_bgConnect()
        SyncLock sync_obj
            If _bgConnect Is Nothing Then
                _bgConnect = New System.Threading.Thread(AddressOf bgConnect)
                _bgConnect.IsBackground = True
                _bgConnect.Start()
            End If
        End SyncLock
    End Sub
    ''' <summary>
    ''' bgConnectスレッドを停止させます
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Stop_bgConnect()
        SyncLock sync_obj
            If _bgConnect IsNot Nothing Then
                _bgConnect.Abort()
                _bgConnect = Nothing
            End If
        End SyncLock
    End Sub
    ''' <summary>
    ''' 接続を試行するためのスレッド用メソッドです
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub bgConnect()
        Try
            Do
                If TCPConnect.AceptInfo Then
                    Exit Do
                Else
                    TCPConnect.ConnectionNetwork(IndividualData.ConnectIP, IndividualData.ConnectPort, ControledForm.appSettings.Account_AccountName, False)
                End If

                '認証する
                If TCPConnect.Login(IndividualData.LoginPassword, LoginServiceClass.ServiceMode.Client, Me.IndividualData.UseEncryptNetwork) Then
                    ConnectMethod()
                    Stop_bgConnect()
                    Exit Do
                Else
                    TCPConnect.NetworkClose()
                    System.Threading.Thread.Sleep(30000)
                End If
            Loop
        Catch ex As Threading.ThreadAbortException
            _bgConnect = Nothing
            Exit Sub
        End Try
    End Sub

    ''' <summary>
    ''' bgMSGRecスレッドを開始します。既にスレッドが動いている場合、何もしません
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Start_bgMSGRec()
        SyncLock sync_obj
            If _bgMSGRec Is Nothing Then
                _bgMSGRec = New System.Threading.Thread(AddressOf bgMSGRec)
                _bgMSGRec.IsBackground = True
                _bgMSGRec.Start()
            End If
        End SyncLock
    End Sub
    ''' <summary>
    ''' bgMSGRecスレッドを停止させます
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Stop_bgMSGRec()
        SyncLock sync_obj
            If _bgMSGRec IsNot Nothing Then
                _bgMSGRec.Abort()
                _bgMSGRec = Nothing
            End If
        End SyncLock
    End Sub
    ''' <summary>
    ''' メッセージを受信するためのスレッド用メソッドです
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub bgMSGRec()
        Try
            Dim p_Byte As Byte() = Nothing
            Dim p_id As UShort = 0
            Dim p_head As TCP_Header
            Do
                If Not TCPConnect.ReceiveData(p_Byte, p_id, p_head) Then
                    CloseMethod()
                    Exit Do
                End If

                Select Case p_head
                    Case TCP_Header.SendFile 'ファイルのデータである場合
                        PRC_SendFile(p_id, p_Byte)
                    Case TCP_Header.SendFileOk '送信したファイルが届いた合図
                        Dim msg As String = enc.GetString(p_Byte)
                        'バッファ制御
                        Dim n As Integer = (Environment.TickCount - LastSendTime)
                        If n = 0 Then
                            MainSendBuffer *= 2
                        Else
                            MainSendBuffer = CInt(MainSendBuffer * (1000 / n))
                        End If
                        SendFileData()
                    Case TCP_Header.JUNK 'ジャンクデータ
                        'このヘッダは通信を確認するためのモノなので、応答するコードは存在しない。
                    Case Else
                        Dim newInvokeProtocol As New InvokeProtocolDelegate(AddressOf InvokeProtocol)
                        ControledForm.BeginInvoke(newInvokeProtocol, New Object() {p_head, p_id, enc.GetString(p_Byte)})
                End Select
            Loop
        Catch ex As Threading.ThreadAbortException
            _bgMSGRec = Nothing
            Exit Sub
        End Try
    End Sub
#End Region

#Region "スレッドセーフ関数"
    ''' <summary>
    ''' 接続がとじたときにすること
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseMethod()
        If ControledForm.InvokeRequired Then
            Dim n As New CloseMethodDelegate(AddressOf CloseMethodCallBack)
            ControledForm.BeginInvoke(n, New Object() {})
        Else
            CloseMethodCallBack()
        End If
    End Sub
    Private Delegate Sub CloseMethodDelegate()
    Private Sub CloseMethodCallBack()
        SyncLock sync_obj
            TCPConnect.NetworkClose()

            ControledForm.AccountListViewRedraw(Me.Index)
            ControledForm.ChangeEnabled()

            Stop_bgMSGRec()
            Start_bgConnect()
        End SyncLock
    End Sub

    ''' <summary>
    ''' 接続完了時にすること
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConnectMethod()
        If ControledForm.InvokeRequired Then
            Dim n As New ConnectMethodDelegate(AddressOf ConnectMethodCallBack)
            ControledForm.BeginInvoke(n, New Object() {})
        Else
            ConnectMethodCallBack()
        End If
    End Sub
    Private Delegate Sub ConnectMethodDelegate()
    Private Sub ConnectMethodCallBack()
        Stop_bgConnect()
        Start_bgMSGRec()

        ControledForm.AccountListViewRedraw(Me.Index)

        If ControledForm.appSettings.Notify_Connect Then
            ControledForm.DoNotify(ControledForm.lang.AccountClass_NotifyTitle_Connect, ControledForm.lang.AccountClass_NotifyMSG_Connect(IndividualData.AccountName))
        End If

        'メインフォームのEnabledを更新
        ControledForm.ChangeEnabled()

        '相手に自身のバージョンを通知
        TCPConnect.SendPacket(SoftwareVersion & vbCrLf & ProtocolVersion, TCP_Header.MyVersion, UShort.MinValue)

        '自身の共有リストの送信
        If IndividualData.Share_Use Then
            TCPConnect.SendPacket(ShareInfo.GetOpenList(ShareClass.ShareKind.ROOT, -1), TCP_Header.Share_List, UShort.MinValue)
        End If

        '自身のポート番号を送信
        TCPConnect.SendPacket(CStr(ControledForm.appSettings.Connect_ListenPort), TCP_Header.ChangePort, UShort.MinValue)

        '自身のコメントを送信
        TCPConnect.SendPacket(ControledForm.appSettings.Account_Comment, TCP_Header.ChangeComment, UShort.MinValue)

        '再接続時にするお互いのアップロードリストとダウンロードリストの照合
        If TransportItem IsNot Nothing AndAlso TransportItem.Count > 0 Then
            SyncLock sync_obj
                Dim sb As New System.Text.StringBuilder
                For i As Integer = 0 To TransportItem.Count - 1
                    If Not TransportItem(i).TransportingDone AndAlso TransportItem(i).ItemState = Arachlex_Transport.Upload Then
                        If sb.Length = 0 Then
                            sb.Append(TransportItem(i).TransportID & vbCr & TransportItem(i).FileSize)
                        Else
                            sb.Append(vbCrLf & TransportItem(i).TransportID & vbCr & TransportItem(i).FileSize)
                        End If
                    End If
                Next
                If sb.Length = 0 Then
                    TCPConnect.SendPacket("nothing", TCP_Header.SyncItem, 0)
                Else
                    TCPConnect.SendPacket(sb.ToString, TCP_Header.SyncItem, 0)
                End If
            End SyncLock
        Else
            TCPConnect.SendPacket("nothing", TCP_Header.SyncItem, 0)
        End If
    End Sub

    ''' <summary>
    ''' リストビューへ登録します
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddToListView()
        If ControledForm.InvokeRequired Then
            Dim n As New AddToListViewDelegate(AddressOf AddToListViewCallBack)
            ControledForm.Invoke(n, New Object() {})
        Else
            AddToListViewCallBack()
        End If
    End Sub
    Private Delegate Sub AddToListViewDelegate()
    Private Sub AddToListViewCallBack()
        ControledForm.Account_ListView.Items.Add(Me)
    End Sub

    ''' <summary>
    ''' 空いているファイルアップロードIDを調べます
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUpEmptyItemID() As UShort
        SyncLock sync_obj
            If TransportItem IsNot Nothing AndAlso TransportItem.Count > 0 Then
                For rID As Integer = UShort.MinValue + 1 To UShort.MaxValue - 1
                    Dim i As Integer = 0
                    Do
                        If Not TransportItem(i).TransportingDone AndAlso TransportItem(i).TransportID = rID Then
                            Exit Do
                        End If
                        i += 1
                        If i = TransportItem.Count Then
                            Return CUShort(rID)
                        End If
                    Loop
                Next
                Return UShort.MinValue
            Else
                Return UShort.MinValue + 1
            End If
        End SyncLock
    End Function

    ''' <summary>
    ''' 次に使えるフリーのキューイング番号を取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNextQueue() As UShort
        SyncLock sync_obj
            If TransportItem IsNot Nothing AndAlso TransportItem.Count > 0 Then
                Dim rQueue As UShort = 0
                For i As Integer = 0 To TransportItem.Count - 1
                    Dim tInfo As TransferListViewItem = TransportItem(i)
                    If Not tInfo.TransportingDone AndAlso tInfo.ItemState = Arachlex_Transport.Upload AndAlso tInfo.TransportQueue >= rQueue Then
                        rQueue = CUShort(rQueue + 1)
                    End If
                Next
                Return rQueue
            Else
                Return 0
            End If
        End SyncLock
    End Function

    ''' <summary>
    ''' キューイングリストから指定したアイテムを除外する。
    ''' </summary>
    ''' <param name="tItem">キューイングからRemoveされたアイテム</param>
    ''' <remarks></remarks>
    Public Sub RemoveQueue(ByVal tItem As TransferListViewItem)
        SyncLock sync_obj
            If tItem IsNot Nothing AndAlso TransportItem IsNot Nothing AndAlso TransportItem.Count > 0 Then
                For i As Integer = 0 To TransportItem.Count - 1
                    If tItem IsNot TransportItem(i) AndAlso tItem.TransportQueue < TransportItem(i).TransportQueue AndAlso tItem.ItemState = TransportItem(i).ItemState Then
                        TransportItem(i).TransportQueue = CUShort(TransportItem(i).TransportQueue - 1)
                    End If
                Next
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' 優先順位を任意の値にセットしなおします
    ''' </summary>
    ''' <param name="tItem">対象の転送アイテム</param>
    ''' <param name="afterQueue">変更後の優先順位</param>
    ''' <remarks></remarks>
    Public Sub SetQueue(ByVal tItem As TransferListViewItem, ByVal afterQueue As UShort)
        SyncLock sync_obj
            If tItem IsNot Nothing AndAlso Not tItem.TransportingDone AndAlso Me.TransportItem IsNot Nothing AndAlso Me.TransportItem.Count > 0 Then
                If afterQueue > tItem.TransportQueue Then

                    Dim n As Integer = 0 '変更前の優先順位と変更後の優先順位の間に何件、ヒットしたか。
                    For i As Integer = 0 To TransportItem.Count - 1
                        Dim tInfo As TransferListViewItem = TransportItem(i)
                        If tItem.ItemState = tInfo.ItemState AndAlso Not tInfo.TransportingDone AndAlso _
                        tItem.TransportQueue < tInfo.TransportQueue AndAlso afterQueue >= tInfo.TransportQueue Then

                            '変更後と変更前の優先順位の間にいる(After >= Queue > Before)優先順位を１つ下に移動。
                            tInfo.TransportQueue = CUShort(tInfo.TransportQueue - 1)
                            n += 1
                        End If
                    Next

                    tItem.TransportQueue = CUShort(tItem.TransportQueue + n)

                ElseIf afterQueue < tItem.TransportQueue Then

                    Dim n As Integer = 0 '変更前の優先順位と変更後の優先順位の間に何件、ヒットしたか。
                    For i As Integer = 0 To TransportItem.Count - 1
                        Dim tInfo As TransferListViewItem = TransportItem(i)
                        If tItem.ItemState = tInfo.ItemState AndAlso Not tInfo.TransportingDone AndAlso _
                        tItem.TransportQueue > tInfo.TransportQueue AndAlso afterQueue <= tInfo.TransportQueue Then

                            '変更後と変更前の優先順位の間にいる(Before > Queue => After)優先順位を１つ上に移動。
                            tInfo.TransportQueue = CUShort(tInfo.TransportQueue + 1)
                            n += 1
                        End If
                    Next

                    tItem.TransportQueue = CUShort(tItem.TransportQueue - n)

                End If
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' 送信中のアイテムがあるか調べます。Trueならある
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSendItems() As Boolean
        If TransportItem IsNot Nothing AndAlso TransportItem.Count > 0 Then
            For i As Integer = 0 To TransportItem.Count - 1
                If TransportItem(i).ItemState = Arachlex_Transport.Upload AndAlso TransportItem(i).NowTransporting Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    ''' <summary>
    ''' アイテム配列から指定IDのダウン情報を取得します。見つからない場合、-1を返します。
    ''' </summary>
    ''' <param name="id">ID番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetInfoFromID(ByVal id As UShort) As TransferListViewItem
        If TransportItem IsNot Nothing AndAlso TransportItem.Count > 0 AndAlso id > UShort.MinValue Then
            For i As Integer = 0 To TransportItem.Count - 1
                If TransportItem(i).TransportID = id AndAlso Not TransportItem(i).TransportingDone Then
                    Return TransportItem(i)
                End If
            Next
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' 最も優先順位の高い転送アイテムを取得する
    ''' </summary>
    ''' <param name="itemState">アイテムの状態</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMostPriorityQueueItem(ByVal itemState As Arachlex_Transport) As TransferListViewItem
        If TransportItem IsNot Nothing AndAlso TransportItem.Count > 0 Then
            Dim retItem As TransferListViewItem = Nothing
            Dim mostpriorityqueue As UShort = UShort.MaxValue
            For n As Integer = 0 To TransportItem.Count - 1
                If mostpriorityqueue > TransportItem(n).TransportQueue AndAlso TransportItem(n).NowTransporting AndAlso TransportItem(n).ItemState = itemState Then
                    retItem = TransportItem(n)
                    If retItem.TransportQueue = 0 Then
                        Return retItem
                    End If
                    mostpriorityqueue = retItem.TransportQueue
                End If
            Next
            Return retItem
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' データを送信します
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SendFileData()
        SyncLock sync_obj
            Dim tItem As TransferListViewItem = GetMostPriorityQueueItem(Arachlex_Transport.Upload)
            If tItem IsNot Nothing Then
                Dim recBytes(MainSendBuffer + 8) As Byte '読み取りバッファ
                '送信データにシーク位置を書き込む
                Array.Copy(BitConverter.GetBytes(tItem.FileSeek), 0, recBytes, 0, 8)
                Try
                    'シーク位置の調整
                    tItem.FileStream.Position = tItem.FileSeek
                    '読み取る
                    Dim recSize As Integer = tItem.FileStream.Read(recBytes, 8, recBytes.Length - 8)
                    If recSize < recBytes.Length - 8 Then
                        Array.Resize(recBytes, recSize + 8)
                    End If
                    tItem.FileSeek += recSize '次のシーク位置を記憶
                    TCPConnect.SendPacket(recBytes, TCP_Header.SendFile, tItem.TransportID) '送信する
                    LastSendTime = Environment.TickCount
                    Return
                Catch
                    tItem.TransportStatus = TransferListViewItem.ItemStatus.ErrorDone
                    TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, tItem.TransportID)
                End Try
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' 次のファイルを相手にセットします
    ''' </summary>
    ''' <param name="fInfo">アイテム情報</param>
    ''' <remarks></remarks>
    Private Sub NextFolderFile(ByVal fInfo As TransferListViewItem)
        SyncLock sync_obj
            If fInfo IsNot Nothing AndAlso fInfo.ItemState = Arachlex_Transport.Upload AndAlso fInfo.ItemAttribute = Arachlex_Item.Folder Then
                fInfo.FolderSetedInItem = False
                fInfo.FileStream = Nothing
                Do
                    If fInfo.FolderFiles Is Nothing OrElse fInfo.FolderFiles.Count = 0 Then
                        '転送完了
                        fInfo.TransportStatus = TransferListViewItem.ItemStatus.Done
                        TCPConnect.SendPacket(String.Empty, TCP_Header.FolderDone, fInfo.TransportID)
                        Return
                    ElseIf fInfo.CreateStream(fInfo.FolderFiles(0), False) Then
                        '転送アイテムをセットする
                        'ファイルサイズ
                        Dim fi As New IO.FileInfo(fInfo.FolderFiles(0))
                        fInfo.FileSize = fi.Length
                        fInfo.FileSeek = 0
                        fInfo.FolderDoneFiles += 1
                        fInfo.FileCachePath = fInfo.FolderFiles(0)
                        '相手にデータを送信する
                        TCPConnect.SendPacket(Replace(fInfo.FolderFiles(0), fInfo.FilePath, "") & vbCrLf & _
                        fi.Length & vbCrLf & _
                        fInfo.FolderDoneFiles _
                        , TCP_Header.NextFolderFile, fInfo.TransportID)
                        '転送残りファイルの整理
                        fInfo.FolderFiles.RemoveAt(0)
                        Return
                    Else 'ストリームを取得できなかったとき
                        fInfo.FolderDoneFiles += 1
                        fInfo.FolderFiles.RemoveAt(0)
                    End If
                Loop Until Not TCPConnect.AceptInfo
            End If
        End SyncLock
    End Sub
#End Region

#Region "関数"
    ''' <summary>
    ''' アイテムを転送リストから削除します
    ''' </summary>
    ''' <param name="tInfo">対象の転送アイテム</param>
    ''' <remarks></remarks>
    Public Sub DeleteTransportInfoArray(ByVal tInfo As TransferListViewItem)
        SyncLock sync_obj
            If tInfo IsNot Nothing Then
                If Not tInfo.TransportingDone Then
                    RemoveQueue(tInfo)
                End If
                tInfo.FileStream = Nothing
                tInfo.Remove()
                TransportItem.Remove(tInfo)
                tInfo = Nothing
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' アイテムを転送リストに追加します
    ''' </summary>
    ''' <param name="tinfo">対象のアイテム</param>
    ''' <remarks></remarks>
    Public Sub AddTransportInfoArray(ByVal tinfo As TransferListViewItem)
        SyncLock sync_obj
            If tinfo IsNot Nothing AndAlso TransportItem.IndexOf(tinfo) = -1 Then
                TransportItem.Add(tinfo)
                ControledForm.AddIconToImageList(IO.Path.GetExtension(tinfo.FileName))
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' 指定したアイテムをアップロードします。続行不可能な場合False
    ''' </summary>
    ''' <param name="fPath">アップロードするパス</param>
    ''' <param name="ShowError">エラーを表示するか</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UploadItem(ByVal fPath As String, ByVal ShowError As Boolean) As Boolean
        If fPath Is Nothing OrElse fPath.Length = 0 Then
            Return True
        End If

        If Not TCPConnect.AceptInfo Then
            Return False
        End If

        '残りIDチェック
        Dim nID As UShort = GetUpEmptyItemID()
        If nID = 0 Then
            If ShowError Then
                MessageBox.Show(ControledForm.lang.AccountClass_DialogMSG_MaxNumUpload, SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            Return False
        End If

        If IO.File.Exists(fPath) Then
            'ファイルサイズ
            Dim fi As New System.IO.FileInfo(fPath)

            '情報作成
            Dim boo_View As Boolean = (ControledForm.SelectUserAccount Is Nothing) OrElse (ControledForm.SelectUserAccount Is Me)
            Dim uInfo As New TransferListViewItem(Me, boo_View, nID, GetNextQueue, fi.Length, fPath, IO.Path.GetFileName(fPath), Arachlex_Item.File, Arachlex_Transport.Upload)
            AddTransportInfoArray(uInfo)

            'ストリーム作成
            If uInfo.CreateStream(fPath, ShowError) Then
                '相手にアップロードを通知
                TCPConnect.SendPacket(CInt(Arachlex_Item.File) & vbCrLf & _
                CStr(uInfo.TransportQueue) & vbCrLf & _
                IO.Path.GetFileName(fPath) & vbCrLf & _
                fi.Length _
                , TCP_Header.ItemUpload, nID)
            Else
                DeleteTransportInfoArray(uInfo)
                uInfo = Nothing
            End If
        ElseIf IO.Directory.Exists(fPath) Then
            '情報作成
            Dim boo_View As Boolean = (ControledForm.SelectUserAccount Is Nothing) OrElse (ControledForm.SelectUserAccount Is Me)
            Dim uInfo As New TransferListViewItem(Me, boo_View, nID, GetNextQueue, 0, fPath, IO.Path.GetFileName(fPath), Arachlex_Item.Folder, Arachlex_Transport.Upload)

            'フォルダ情報を収集
            Dim files As String() = System.IO.Directory.GetFiles(fPath, "*", System.IO.SearchOption.AllDirectories)
            uInfo.FolderFiles.AddRange(files)
            uInfo.FolderAllFiles = files.Length

            '情報を追加
            AddTransportInfoArray(uInfo)

            '相手にアップロードを通知
            TCPConnect.SendPacket(CInt(Arachlex_Item.Folder) & vbCrLf & _
            CStr(uInfo.TransportQueue) & vbCrLf & _
            IO.Path.GetFileName(fPath) & vbCrLf & _
            uInfo.FolderAllFiles, _
            TCP_Header.ItemUpload, nID)
        End If
        Return True
    End Function

    ''' <summary>
    ''' 指定したアイテムのダウンロードを承認します
    ''' </summary>
    ''' <param name="dInfo">承認するダウンロード情報</param>
    ''' <param name="savepath">ダウンロード先フォルダ</param>
    ''' <remarks></remarks>
    Public Sub UploadApporoval(ByVal dInfo As TransferListViewItem, ByVal savepath As String)
        If dInfo Is Nothing OrElse dInfo.ItemState = Arachlex_Transport.Upload _
        OrElse dInfo.TransportStatus <> TransferListViewItem.ItemStatus.NotApproval OrElse Not TCPConnect.AceptInfo Then
            Return
        End If

        If dInfo.ItemAttribute = Arachlex_Item.File Then 'ファイルの場合
            'ダウンロード場所を設定
            If savepath IsNot Nothing AndAlso IO.Directory.Exists(savepath) Then
                dInfo.FilePath = GetRepetitionFileName(IO.Path.Combine(savepath, dInfo.FileName))
            Else
                Dim sfd As New SaveFileDialog()
                sfd.AddExtension = True
                sfd.CheckPathExists = True
                sfd.FilterIndex = 0
                sfd.FileName = dInfo.FileName
                sfd.Filter = IO.Path.GetExtension(dInfo.FileName) & "Type|*" & IO.Path.GetExtension(dInfo.FileName) & "|File(*.*)|*.*"
                sfd.InitialDirectory = ControledForm.appSettings.SaveDialogPath
                sfd.OverwritePrompt = True
                sfd.Title = ControledForm.lang.AccountClass_DialogTitle_UploadApprovalSavePath(dInfo.FileName)
                sfd.RestoreDirectory = True

                'ダイアログの表示
                If sfd.ShowDialog() = DialogResult.OK Then
                    ControledForm.appSettings.SaveDialogPath = sfd.FileName
                    Try
                        IO.File.Delete(sfd.FileName)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End Try
                    dInfo.FilePath = sfd.FileName
                Else
                    Return
                End If
            End If

            SyncLock sync_obj
                'ダイアログを出している間に変更されてないかチェックする
                If dInfo IsNot Nothing AndAlso dInfo.TransportStatus = TransferListViewItem.ItemStatus.NotApproval Then
                    'キャッシュパスを取得
                    Dim streamPath As String = GetRepetitionFileName(dInfo.FilePath & CacheExt)
                    If dInfo.CreateStream(streamPath, True) Then
                        dInfo.FileCachePath = streamPath
                        dInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled
                        '相手に通知
                        TCPConnect.SendPacket("0", TCP_Header.ItemApp, dInfo.TransportID)
                    End If
                End If
            End SyncLock
        ElseIf dInfo.ItemAttribute = Arachlex_Item.Folder Then 'フォルダの場合
            '保存位置の設定
            If savepath IsNot Nothing AndAlso IO.Directory.Exists(savepath) Then
                dInfo.FilePath = GetRepetitionFolderName(IO.Path.Combine(savepath, dInfo.FileName))
            Else
                Dim fbd As New FolderBrowserDialog
                fbd.Description = ControledForm.lang.AccountClass_DialogTitle_UploadApprovalSavePath(dInfo.FileName)
                fbd.RootFolder = Environment.SpecialFolder.Desktop
                fbd.SelectedPath = ControledForm.appSettings.SaveDialogPath
                fbd.ShowNewFolderButton = True
                If fbd.ShowDialog(ControledForm) = DialogResult.OK Then
                    dInfo.FilePath = GetRepetitionFolderName(IO.Path.Combine(fbd.SelectedPath, dInfo.FileName))
                    ControledForm.appSettings.SaveDialogPath = fbd.SelectedPath
                Else
                    Return
                End If
            End If

            SyncLock sync_obj
                'ダイアログを出している間に変更されてないかチェックする
                If dInfo IsNot Nothing AndAlso dInfo.TransportStatus = TransferListViewItem.ItemStatus.NotApproval AndAlso Not dInfo.Transporting Then
                    dInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled
                    'トップディレクトリを作成
                    IO.Directory.CreateDirectory(dInfo.FilePath)
                    '相手に通知
                    TCPConnect.SendPacket(String.Empty, TCP_Header.ItemApp, dInfo.TransportID)
                End If
            End SyncLock
        End If
    End Sub

    ''' <summary>
    ''' レジューム承認します
    ''' </summary>
    ''' <param name="dInfo">対象のアイテム</param>
    ''' <param name="path">保存先</param>
    ''' <remarks></remarks>
    Public Sub ResumeApporoval(ByVal dInfo As TransferListViewItem, ByVal path As String)
        If dInfo Is Nothing OrElse Not TCPConnect.AceptInfo OrElse dInfo.ItemState = Arachlex_Transport.Upload _
        OrElse dInfo.TransportStatus <> TransferListViewItem.ItemStatus.NotApproval Then
            Return
        End If

        If dInfo.ItemAttribute = Arachlex_Item.File Then 'ファイルの場合
            Dim rFile As String = ""
            If path IsNot Nothing AndAlso IO.File.Exists(path) Then
                rFile = path
            Else
                Dim ofd As New OpenFileDialog()
                ofd.FileName = dInfo.FileName
                ofd.InitialDirectory = ControledForm.appSettings.SaveDialogPath
                ofd.Filter = "Temp|" & "*" & CacheExt & "|File(*.*)|*.*"
                ofd.Title = ControledForm.lang.AccountClass_DialogTitle_ResumeFile(dInfo.FileName)
                ofd.RestoreDirectory = True
                ofd.Multiselect = False
                If ofd.ShowDialog() = DialogResult.OK Then
                    rFile = ofd.FileName
                    ControledForm.appSettings.SaveDialogPath = rFile
                Else
                    Return
                End If
            End If

            Dim fl As New IO.FileInfo(rFile)
            If fl.Length >= dInfo.FileSize OrElse fl.Length = 0 Then
                MessageBox.Show(ControledForm.lang.AccountClass_DialogMSG_MustNotResume, SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Return
            End If

            SyncLock sync_obj
                'メッセージボックス表示中に操作で変更されてないかチェックする
                If dInfo IsNot Nothing AndAlso dInfo.TransportStatus = TransferListViewItem.ItemStatus.NotApproval Then
                    'ストリームが取得できるかチェック
                    If dInfo.CreateStream(rFile, True) Then
                        dInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled
                        If CacheExt.Equals(IO.Path.GetExtension(rFile), StringComparison.OrdinalIgnoreCase) Then
                            dInfo.FilePath = IO.Path.ChangeExtension(rFile, Nothing)
                        Else
                            dInfo.FilePath = rFile
                        End If
                        dInfo.FileCachePath = rFile
                        dInfo.FileSeek = fl.Length

                        '相手に通知する
                        TCPConnect.SendPacket(fl.Length.ToString, TCP_Header.ItemApp, dInfo.TransportID)
                    End If
                End If
            End SyncLock
        ElseIf dInfo.ItemAttribute = Arachlex_Item.Folder Then 'フォルダの場合
            If path IsNot Nothing AndAlso IO.Directory.Exists(path) Then
                dInfo.FilePath = path
            Else
                Dim fbd As New FolderBrowserDialog
                fbd.Description = ControledForm.lang.AccountClass_DialogTitle_ResumeFolder(dInfo.FileName)
                fbd.RootFolder = Environment.SpecialFolder.Desktop
                fbd.SelectedPath = ControledForm.appSettings.SaveDialogPath
                fbd.ShowNewFolderButton = True
                If fbd.ShowDialog(ControledForm) = DialogResult.OK Then
                    dInfo.FilePath = fbd.SelectedPath
                    ControledForm.appSettings.SaveDialogPath = dInfo.FilePath
                Else
                    Return
                End If
            End If

            SyncLock sync_obj
                'メッセージボックス表示中に操作で変更されてないかチェックする
                If dInfo IsNot Nothing AndAlso dInfo.TransportStatus = TransferListViewItem.ItemStatus.NotApproval Then
                    dInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled
                    TCPConnect.SendPacket(String.Empty, TCP_Header.ItemApp, dInfo.TransportID)
                End If
            End SyncLock
        End If
    End Sub

    ''' <summary>
    ''' チャットメッセージを追加します
    ''' </summary>
    ''' <param name="msg">投稿メッセージ</param>
    ''' <param name="raiseName">投稿者名</param>
    ''' <param name="postDate">投稿日時</param>
    ''' <param name="rais">自分が投稿者か</param>
    ''' <remarks></remarks>
    Public Sub ApendChat(ByVal msg As String, ByVal raiseName As String, ByVal postDate As Date, ByVal rais As Boolean)
        If msg.Length > 0 Then
            'Chatウィンドウのインスタンスがまだ確保されていない場合
            If ChatWindow Is Nothing Then
                Dim n As New Form_Chat(Me)
                n.Show()
                ChatWindow = n
            ElseIf Form.ActiveForm IsNot ChatWindow Then
                FlashWindow(ChatWindow.Handle, True)
            End If

            Dim h_name As String = raiseName & " [" & postDate.ToString("HH:mm:ss") & "]"
            If raiseName Is Nothing OrElse raiseName.Length = 0 Then
                h_name = ControledForm.lang.AccountClass_Chat_AppendMSG & SoftwareName & " [" & Now.ToString(ControledForm.lang.DateType) & "]"
            End If

            '現在のテキスト数を記録
            Dim bifLen As Integer = ChatWindow.Chat_View.TextLength
            ChatWindow.Chat_View.AppendText(h_name & vbCrLf & msg & vbCrLf)

            '色づけ
            ChatWindow.Chat_View.SelectionStart = bifLen
            ChatWindow.Chat_View.SelectionLength = h_name.Length
            If rais Then
                ChatWindow.Chat_View.SelectionColor = Color.Blue
            Else
                ChatWindow.Chat_View.SelectionColor = Color.Green
            End If
            'スクロールする
            ChatWindow.Chat_View.SelectionStart = ChatWindow.Chat_View.TextLength + 4
            ChatWindow.Chat_View.ScrollToCaret()
        End If
    End Sub
#End Region

#Region "プロパティ"
    Private _ChatWindow As Form_Chat
    ''' <summary>
    ''' チャットウィンドウ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChatWindow() As Form_Chat
        Get
            Return _ChatWindow
        End Get
        Set(ByVal value As Form_Chat)
            _ChatWindow = value
        End Set
    End Property

    Private _IndividualData As AccountData
    ''' <summary>
    ''' 個人データなど
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IndividualData() As AccountData
        Get
            Return _IndividualData
        End Get
        Set(ByVal value As AccountData)
            _IndividualData = value
        End Set
    End Property

    Private _UserChat As String
    ''' <summary>
    ''' ユーザーのチャット履歴
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserChat() As String
        Get
            Return _UserChat
        End Get
        Set(ByVal value As String)
            _UserChat = value
        End Set
    End Property

    Private _TransportItem As List(Of TransferListViewItem)
    ''' <summary>
    ''' このユーザーと転送しているアイテム
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransportItem() As List(Of TransferListViewItem)
        Get
            Return _TransportItem
        End Get
        Set(ByVal value As List(Of TransferListViewItem))
            _TransportItem = value
        End Set
    End Property

    Private _ShareInfo As ShareClass
    ''' <summary>
    ''' 共有情報を管理する
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShareInfo() As ShareClass
        Get
            Return _ShareInfo
        End Get
        Set(ByVal value As ShareClass)
            _ShareInfo = value
        End Set
    End Property

    Private _TCPConnect As TCPNetworkClass
    ''' <summary>
    ''' TCP接続情報
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TCPConnect() As TCPNetworkClass
        Get
            Return _TCPConnect
        End Get
        Set(ByVal value As TCPNetworkClass)
            _TCPConnect = value
        End Set
    End Property

    Private _LastSendTime As Integer
    ''' <summary>
    ''' ファイルデータ送信間隔
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property LastSendTime() As Integer
        Get
            Return _LastSendTime
        End Get
        Set(ByVal value As Integer)
            _LastSendTime = value
        End Set
    End Property

    Private _MainSendBuffer As Integer
    ''' <summary>
    ''' 送信バッファ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainSendBuffer() As Integer
        Get
            If _MainSendBuffer > Transport_SendBufferSize_Max Then
                _MainSendBuffer = Transport_SendBufferSize_Max
            ElseIf _MainSendBuffer < Transport_SendBufferSize_Min Then
                _MainSendBuffer = Transport_SendBufferSize_Min
            End If
            Return _MainSendBuffer
        End Get
        Set(ByVal value As Integer)
            _MainSendBuffer = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' コストラクタ
    ''' </summary>
    ''' <param name="AForm">オーナーフォーム</param>
    ''' <param name="StartTryConnection">インスタンス生成時にアカウントへの接続試行を開始するか</param>
    ''' <param name="AddListView">リストビューにアイテムを追加するか</param>
    ''' <param name="AAccountData">アカウント情報</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal AForm As Form1, ByVal StartTryConnection As Boolean, ByVal AddListView As Boolean, ByVal AAccountData As AccountData)
        ControledForm = AForm
        ChatWindow = Nothing

        '初期化
        _TransportItem = New List(Of TransferListViewItem)
        _ShareInfo = New ShareClass(ControledForm, AAccountData)
        _TCPConnect = New TCPNetworkClass(enc)
        _UserChat = ""
        _LastSendTime = 0
        _MainSendBuffer = Transport_SendBufferSize_Default
        _IndividualData = AAccountData

        'リストボックスアイテムのリストボックスへの追加
        If AddListView Then
            AddToListView()
        End If

        '暗号化オプションをセット
        TCPConnect.UseEncrypt = IndividualData.UseEncryptNetwork

        '接続試行開始
        If StartTryConnection Then
            Start_bgConnect()
        End If
    End Sub
End Class