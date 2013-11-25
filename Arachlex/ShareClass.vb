Imports Arachlex.DefinitionClass
Public Class ShareClass

    Public MyOpenList As Generic.List(Of MyShareList) '自身の公開リスト
    Public YourOpenList As Generic.List(Of FileInfoListViewItem) '相手のリスト
    Private NowShowView As String = "" '現在自身が相手に表示しているパス。空の場合ROOT
    Private SelectItemName As String = "" '自身が前に表示していたフォルダ名
    Public Share_Path As String = "" '自身がパスに表示するためのテキスト
    Private Const SHARE_DIR As String = "<DIR>" 'フォルダ時の表記
    Private mainform As Form1
    Private ownerData As AccountData

    ''' <summary>
    ''' 自身が相手に公開する情報を保持する構造体です
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MyShareList

        Public FullPath As String

        Public Attribute As Arachlex_Item

        Public ID As Integer

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="filepath">ファイル名</param>
        ''' <param name="fileAttribute">アイテムの種類</param>
        ''' <param name="ItemID">アイテムのID</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal filepath As String, ByVal fileAttribute As Arachlex_Item, ByVal ItemID As Integer)
            FullPath = filepath
            Attribute = fileAttribute
            ID = ItemID
        End Sub
    End Class

    ''' <summary>
    ''' どのようなリストを取得するか
    ''' </summary>
    ''' <remarks></remarks>
    Enum ShareKind
        ROOT
        TOP
        Refresh
        NORMAL
    End Enum

    ''' <summary>
    ''' フォルダリストを取得し返します
    ''' </summary>
    ''' <param name="GetKind">フォルダの種類</param>
    ''' <param name="id">ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOpenList(ByVal GetKind As ShareKind, ByVal id As Integer) As String
        Dim retStr As String = ""
        Dim retFolderName As String = ""
        Dim retOldFolderName As String = ""
        Dim retRoot As Boolean = False

        Select Case GetKind
            Case ShareKind.NORMAL '普通のパス
                Dim sInfo As MyShareList = GetMyOpenInfoFromID(id)
                If sInfo IsNot Nothing AndAlso IO.Directory.Exists(sInfo.FullPath) Then
                    retStr = GetListFromPath(sInfo.FullPath)
                    retFolderName = ShareGetFileName(sInfo.FullPath, True)
                End If
            Case ShareKind.Refresh '更新
                retStr = GetListFromPath(NowShowView)
                If NowShowView Is Nothing OrElse NowShowView.Length = 0 Then
                    retFolderName = "ROOT"
                Else
                    retFolderName = ShareGetFileName(NowShowView, True)
                End If
            Case ShareKind.ROOT 'ルートパス
                retStr = GetRootList()
                retFolderName = "ROOT"
                retRoot = True
            Case ShareKind.TOP '上へ移動
                retOldFolderName = ShareGetFileName(NowShowView, False)
                If NowShowView Is Nothing OrElse NowShowView.Length = 0 OrElse ownerData.GetExitsSharedList.IndexOf(NowShowView) >= 0 Then
                    retStr = GetRootList()
                    retFolderName = "ROOT"
                    retRoot = True
                Else
                    retStr = GetListFromPath(IO.Path.GetDirectoryName(NowShowView))
                    retFolderName = ShareGetFileName(NowShowView, True)
                End If
        End Select

        '必要な情報を付加する
        '{フォルダ名} + {前回表示していたフォルダ名} + {retStr}
        Return retFolderName & vbCrLf & retOldFolderName & vbCrLf & retStr
    End Function

    ''' <summary>
    ''' リストビューにリストビューアイテムを追加します
    ''' </summary>
    ''' <param name="AddList">対象の情報</param>
    ''' <remarks></remarks>
    Public Sub AddListView(ByVal AddList As Generic.List(Of FileInfoListViewItem))
        'リストをクリア
        mainform.Share_ListView.Items.Clear()

        If AddList IsNot Nothing AndAlso AddList.Count > 0 Then
            'アイテムをリストビューに追加
            mainform.Share_ListView.Items.AddRange(AddList.ToArray)

            '選択されているアイテムまでスクロールする
            If mainform.Share_ListView.SelectedItems.Count > 0 Then
                mainform.Share_ListView.SelectedItems(0).EnsureVisible()
            End If
        End If
    End Sub

    ''' <summary>
    ''' リストを構築します
    ''' </summary>
    ''' <param name="item">アイテム</param>
    ''' <remarks></remarks>
    Public Sub ShowListItem(ByVal item As String)
        Dim ms() As String = Split(item, vbCrLf, 3) '解析

        Dim boo_View As Boolean = mainform.SelectUserAccount IsNot Nothing AndAlso mainform.SelectUserAccount.ShareInfo Is Me

        '受け取ったリストのフォルダ名を入れる
        If boo_View Then
            mainform.Share_PathText.Text = ms(0)
            mainform.Share_PathText.SelectionStart = mainform.Share_PathText.TextLength
            mainform.Share_PathText.ScrollToCaret()
        End If
        Share_Path = ms(0)
        '選択すべきフォルダ名を保持
        SelectItemName = ms(1)

        'リストを空にする
        YourOpenList.Clear()

        'アイテム単位に分割
        Dim items() As String = Split(ms(2), vbCrLf)
        '空なら抜ける
        If items IsNot Nothing AndAlso items.Length > 0 AndAlso items(0).Length > 0 Then
            For i As Integer = 0 To items.Length - 2
                Dim spm() As String = Split(items(i), vbCr, 5) '{ファイル名} + {ID} + {DIR or File} + {FileSize} + {更新日時}

                '保持情報を生成
                If CType(CInt(spm(2)), Arachlex_Item) = Arachlex_Item.Folder Then
                    'ディレクトリの場合
                    Dim nsListViewItem As New FileInfoListViewItem(CLng(spm(3)), spm(0), Arachlex_Item.Folder, Date.Parse(spm(4)), CInt(spm(1)))
                    nsListViewItem.Text = nsListViewItem.FileName
                    nsListViewItem.SubItems.Add(SHARE_DIR)
                    nsListViewItem.SubItems.Add(SHARE_DIR)
                    nsListViewItem.SubItems.Add(nsListViewItem.TimeStamp.ToString)
                    nsListViewItem.ImageKey = "folder"
                    YourOpenList.Add(nsListViewItem)
                    '選択されているアイテムか
                    If nsListViewItem.FileName.Equals(SelectItemName, StringComparison.OrdinalIgnoreCase) Then
                        nsListViewItem.Selected = True
                    End If
                Else
                    'ファイルの場合
                    Dim nsListViewItem As New FileInfoListViewItem(CLng(spm(3)), spm(0), Arachlex_Item.File, Date.Parse(spm(4)), CInt(spm(1)))
                    nsListViewItem.Text = nsListViewItem.FileName
                    nsListViewItem.SubItems.Add(IO.Path.GetExtension(nsListViewItem.FileName))
                    nsListViewItem.SubItems.Add(RetFileSize(nsListViewItem.FileSize))
                    nsListViewItem.SubItems.Add(nsListViewItem.TimeStamp.ToString)
                    nsListViewItem.ImageKey = mainform.AddIconToImageList(IO.Path.GetExtension(nsListViewItem.FileName))
                    YourOpenList.Add(nsListViewItem)
                End If
            Next
        End If

        If boo_View Then
            AddListView(YourOpenList)
        End If
    End Sub

    ''' <summary>
    ''' 指定IDから自身の公開している情報を取得します
    ''' </summary>
    ''' <param name="id">対象のID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMyOpenInfoFromID(ByVal id As Integer) As MyShareList
        If MyOpenList IsNot Nothing AndAlso MyOpenList.Count > 0 AndAlso id > -1 Then
            'IDとArray配列の番号が一緒だった場合
            If MyOpenList.Count - 1 >= id AndAlso MyOpenList(id).ID = id Then
                Return MyOpenList(id)
            Else
                For i As Integer = 0 To MyOpenList.Count - 1
                    If MyOpenList(i).ID = id Then
                        Return MyOpenList(i)
                    End If
                Next
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' 指定したパスのファイル名を取得します
    ''' </summary>
    ''' <param name="nm">対象パス</param>
    ''' <param name="remotePath">リモート用のパスを取得するか</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShareGetFileName(ByVal nm As String, ByVal remotePath As Boolean) As String
        Dim drvname As IO.DriveInfo() = IO.DriveInfo.GetDrives
        For i As Integer = 0 To drvname.Length - 1
            If drvname(i).Name.Equals(nm, StringComparison.OrdinalIgnoreCase) Then
                If remotePath Then
                    Return "ROOT\" & nm
                Else
                    Return "Drive - " & nm
                End If
            End If
        Next

        If remotePath Then
            'リモートパスの取得
            Dim shareList As Generic.List(Of String) = ownerData.Share_OpenFolder
            If (shareList IsNot Nothing AndAlso shareList.Count > 0) OrElse (nm IsNot Nothing AndAlso nm.Length > 0) Then
                For i As Integer = 0 To shareList.Count - 1
                    If nm.IndexOf(shareList(i)) >= 0 Then
                        Dim rep As String = Replace(nm, IO.Path.GetDirectoryName(shareList(i)), "", 1, -1, CompareMethod.Text)
                        If Not rep.StartsWith("\") Then
                            rep = "\" & rep
                        End If
                        Return "ROOT" & rep
                    End If
                Next
            End If
            Return "ROOT"
        Else
            Return IO.Path.GetFileName(nm)
        End If
    End Function

    ''' <summary>
    ''' 指定したパスのリストを取得します
    ''' </summary>
    ''' <param name="path">対象のパス</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetListFromPath(ByVal path As String) As String
        'フォルダが存在するか
        If path IsNot Nothing AndAlso path.Length > 0 AndAlso IO.Directory.Exists(path) Then
            '自身が公開しているフォルダパスを変更
            NowShowView = path
            MyOpenList.Clear()

            '公開するフォルダ内のフォルダ
            Dim allDIR As String() = Nothing
            '公開するフォルダ内のファイル
            Dim allFiles As String() = Nothing
            Try 'アクセス権があるか
                allDIR = IO.Directory.GetDirectories(path)
                allFiles = IO.Directory.GetFiles(path)
            Catch
                Return ""
            End Try

            Dim sb As New System.Text.StringBuilder
            Dim i As Integer = 0
            'ディレクトリのリストを作成
            If allDIR IsNot Nothing AndAlso allDIR.Length > 0 Then
                Do
                    sb.Append(ShareGetFileName(allDIR(i), False) & vbCr & i & vbCr & Arachlex_Item.Folder & vbCr & 0 & vbCr & System.IO.File.GetLastWriteTime(allDIR(i)).ToString & vbCrLf)
                    MyOpenList.Add(New MyShareList(allDIR(i), Arachlex_Item.Folder, i))
                    i += 1
                Loop Until i = allDIR.Length
            End If

            'ファイルのリストを作成
            If allFiles IsNot Nothing AndAlso allFiles.Length > 0 Then
                Dim f As Integer = 0 'allFilesのインデックス
                Do
                    Dim fl As New IO.FileInfo(allFiles(f))
                    sb.Append(ShareGetFileName(allFiles(f), False) & vbCr & i & vbCr & Arachlex_Item.File & vbCr & fl.Length & vbCr & System.IO.File.GetLastWriteTime(allFiles(f)).ToString & vbCrLf)
                    MyOpenList.Add(New MyShareList(allFiles(f), Arachlex_Item.File, i))
                    i += 1
                    f += 1
                Loop Until f = allFiles.Length
            End If
            Return sb.ToString
        Else
            Return GetRootList()
        End If
    End Function
    ''' <summary>
    ''' ルートリストを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRootList() As String
        '自身の公開フォルダを更新
        NowShowView = ""
        'リストの取得
        Dim openFolder As Generic.List(Of String) = ownerData.GetExitsSharedList
        MyOpenList.Clear()
        'リストが存在するか
        If openFolder IsNot Nothing AndAlso openFolder.Count > 0 Then
            Dim sb As New System.Text.StringBuilder
            For i As Integer = 0 To openFolder.Count - 1
                sb.Append(ShareGetFileName(openFolder(i), False) & vbCr & i & vbCr & Arachlex_Item.Folder & vbCr & 0 & vbCr & System.IO.File.GetLastWriteTime(openFolder(i)).ToString & vbCrLf)
                MyOpenList.Add(New MyShareList(openFolder(i), Arachlex_Item.Folder, i))
            Next
            Return sb.ToString
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal fm As Form1, ByVal owData As AccountData)
        mainform = fm
        ownerData = owData
        MyOpenList = New Generic.List(Of MyShareList)
        YourOpenList = New Generic.List(Of FileInfoListViewItem)
        NowShowView = ""
        SelectItemName = ""
    End Sub
End Class