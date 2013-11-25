Imports Arachlex.DefinitionClass
Public Class ShareClass

    Public MyOpenList As Generic.List(Of MyShareList) '���g�̌��J���X�g
    Public YourOpenList As Generic.List(Of FileInfoListViewItem) '����̃��X�g
    Private NowShowView As String = "" '���ݎ��g������ɕ\�����Ă���p�X�B��̏ꍇROOT
    Private SelectItemName As String = "" '���g���O�ɕ\�����Ă����t�H���_��
    Public Share_Path As String = "" '���g���p�X�ɕ\�����邽�߂̃e�L�X�g
    Private Const SHARE_DIR As String = "<DIR>" '�t�H���_���̕\�L
    Private mainform As Form1
    Private ownerData As AccountData

    ''' <summary>
    ''' ���g������Ɍ��J�������ێ�����\���̂ł�
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MyShareList

        Public FullPath As String

        Public Attribute As Arachlex_Item

        Public ID As Integer

        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <param name="filepath">�t�@�C����</param>
        ''' <param name="fileAttribute">�A�C�e���̎��</param>
        ''' <param name="ItemID">�A�C�e����ID</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal filepath As String, ByVal fileAttribute As Arachlex_Item, ByVal ItemID As Integer)
            FullPath = filepath
            Attribute = fileAttribute
            ID = ItemID
        End Sub
    End Class

    ''' <summary>
    ''' �ǂ̂悤�ȃ��X�g���擾���邩
    ''' </summary>
    ''' <remarks></remarks>
    Enum ShareKind
        ROOT
        TOP
        Refresh
        NORMAL
    End Enum

    ''' <summary>
    ''' �t�H���_���X�g���擾���Ԃ��܂�
    ''' </summary>
    ''' <param name="GetKind">�t�H���_�̎��</param>
    ''' <param name="id">ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOpenList(ByVal GetKind As ShareKind, ByVal id As Integer) As String
        Dim retStr As String = ""
        Dim retFolderName As String = ""
        Dim retOldFolderName As String = ""
        Dim retRoot As Boolean = False

        Select Case GetKind
            Case ShareKind.NORMAL '���ʂ̃p�X
                Dim sInfo As MyShareList = GetMyOpenInfoFromID(id)
                If sInfo IsNot Nothing AndAlso IO.Directory.Exists(sInfo.FullPath) Then
                    retStr = GetListFromPath(sInfo.FullPath)
                    retFolderName = ShareGetFileName(sInfo.FullPath, True)
                End If
            Case ShareKind.Refresh '�X�V
                retStr = GetListFromPath(NowShowView)
                If NowShowView Is Nothing OrElse NowShowView.Length = 0 Then
                    retFolderName = "ROOT"
                Else
                    retFolderName = ShareGetFileName(NowShowView, True)
                End If
            Case ShareKind.ROOT '���[�g�p�X
                retStr = GetRootList()
                retFolderName = "ROOT"
                retRoot = True
            Case ShareKind.TOP '��ֈړ�
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

        '�K�v�ȏ���t������
        '{�t�H���_��} + {�O��\�����Ă����t�H���_��} + {retStr}
        Return retFolderName & vbCrLf & retOldFolderName & vbCrLf & retStr
    End Function

    ''' <summary>
    ''' ���X�g�r���[�Ƀ��X�g�r���[�A�C�e����ǉ����܂�
    ''' </summary>
    ''' <param name="AddList">�Ώۂ̏��</param>
    ''' <remarks></remarks>
    Public Sub AddListView(ByVal AddList As Generic.List(Of FileInfoListViewItem))
        '���X�g���N���A
        mainform.Share_ListView.Items.Clear()

        If AddList IsNot Nothing AndAlso AddList.Count > 0 Then
            '�A�C�e�������X�g�r���[�ɒǉ�
            mainform.Share_ListView.Items.AddRange(AddList.ToArray)

            '�I������Ă���A�C�e���܂ŃX�N���[������
            If mainform.Share_ListView.SelectedItems.Count > 0 Then
                mainform.Share_ListView.SelectedItems(0).EnsureVisible()
            End If
        End If
    End Sub

    ''' <summary>
    ''' ���X�g���\�z���܂�
    ''' </summary>
    ''' <param name="item">�A�C�e��</param>
    ''' <remarks></remarks>
    Public Sub ShowListItem(ByVal item As String)
        Dim ms() As String = Split(item, vbCrLf, 3) '���

        Dim boo_View As Boolean = mainform.SelectUserAccount IsNot Nothing AndAlso mainform.SelectUserAccount.ShareInfo Is Me

        '�󂯎�������X�g�̃t�H���_��������
        If boo_View Then
            mainform.Share_PathText.Text = ms(0)
            mainform.Share_PathText.SelectionStart = mainform.Share_PathText.TextLength
            mainform.Share_PathText.ScrollToCaret()
        End If
        Share_Path = ms(0)
        '�I�����ׂ��t�H���_����ێ�
        SelectItemName = ms(1)

        '���X�g����ɂ���
        YourOpenList.Clear()

        '�A�C�e���P�ʂɕ���
        Dim items() As String = Split(ms(2), vbCrLf)
        '��Ȃ甲����
        If items IsNot Nothing AndAlso items.Length > 0 AndAlso items(0).Length > 0 Then
            For i As Integer = 0 To items.Length - 2
                Dim spm() As String = Split(items(i), vbCr, 5) '{�t�@�C����} + {ID} + {DIR or File} + {FileSize} + {�X�V����}

                '�ێ����𐶐�
                If CType(CInt(spm(2)), Arachlex_Item) = Arachlex_Item.Folder Then
                    '�f�B���N�g���̏ꍇ
                    Dim nsListViewItem As New FileInfoListViewItem(CLng(spm(3)), spm(0), Arachlex_Item.Folder, Date.Parse(spm(4)), CInt(spm(1)))
                    nsListViewItem.Text = nsListViewItem.FileName
                    nsListViewItem.SubItems.Add(SHARE_DIR)
                    nsListViewItem.SubItems.Add(SHARE_DIR)
                    nsListViewItem.SubItems.Add(nsListViewItem.TimeStamp.ToString)
                    nsListViewItem.ImageKey = "folder"
                    YourOpenList.Add(nsListViewItem)
                    '�I������Ă���A�C�e����
                    If nsListViewItem.FileName.Equals(SelectItemName, StringComparison.OrdinalIgnoreCase) Then
                        nsListViewItem.Selected = True
                    End If
                Else
                    '�t�@�C���̏ꍇ
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
    ''' �w��ID���玩�g�̌��J���Ă�������擾���܂�
    ''' </summary>
    ''' <param name="id">�Ώۂ�ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMyOpenInfoFromID(ByVal id As Integer) As MyShareList
        If MyOpenList IsNot Nothing AndAlso MyOpenList.Count > 0 AndAlso id > -1 Then
            'ID��Array�z��̔ԍ����ꏏ�������ꍇ
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
    ''' �w�肵���p�X�̃t�@�C�������擾���܂�
    ''' </summary>
    ''' <param name="nm">�Ώۃp�X</param>
    ''' <param name="remotePath">�����[�g�p�̃p�X���擾���邩</param>
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
            '�����[�g�p�X�̎擾
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
    ''' �w�肵���p�X�̃��X�g���擾���܂�
    ''' </summary>
    ''' <param name="path">�Ώۂ̃p�X</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetListFromPath(ByVal path As String) As String
        '�t�H���_�����݂��邩
        If path IsNot Nothing AndAlso path.Length > 0 AndAlso IO.Directory.Exists(path) Then
            '���g�����J���Ă���t�H���_�p�X��ύX
            NowShowView = path
            MyOpenList.Clear()

            '���J����t�H���_���̃t�H���_
            Dim allDIR As String() = Nothing
            '���J����t�H���_���̃t�@�C��
            Dim allFiles As String() = Nothing
            Try '�A�N�Z�X�������邩
                allDIR = IO.Directory.GetDirectories(path)
                allFiles = IO.Directory.GetFiles(path)
            Catch
                Return ""
            End Try

            Dim sb As New System.Text.StringBuilder
            Dim i As Integer = 0
            '�f�B���N�g���̃��X�g���쐬
            If allDIR IsNot Nothing AndAlso allDIR.Length > 0 Then
                Do
                    sb.Append(ShareGetFileName(allDIR(i), False) & vbCr & i & vbCr & Arachlex_Item.Folder & vbCr & 0 & vbCr & System.IO.File.GetLastWriteTime(allDIR(i)).ToString & vbCrLf)
                    MyOpenList.Add(New MyShareList(allDIR(i), Arachlex_Item.Folder, i))
                    i += 1
                Loop Until i = allDIR.Length
            End If

            '�t�@�C���̃��X�g���쐬
            If allFiles IsNot Nothing AndAlso allFiles.Length > 0 Then
                Dim f As Integer = 0 'allFiles�̃C���f�b�N�X
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
    ''' ���[�g���X�g���擾���܂�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRootList() As String
        '���g�̌��J�t�H���_���X�V
        NowShowView = ""
        '���X�g�̎擾
        Dim openFolder As Generic.List(Of String) = ownerData.GetExitsSharedList
        MyOpenList.Clear()
        '���X�g�����݂��邩
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
    ''' �R���X�g���N�^
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