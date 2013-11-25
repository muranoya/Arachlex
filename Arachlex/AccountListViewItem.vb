Imports Arachlex.DefinitionClass
Imports Arachlex.TCPNetworkClass
''' <summary>
''' �ڑ����Ă��郆�[�U�[�̏���ێ����邽�߂̃N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class AccountListViewItem
    Inherits ListViewItem

    Public ControledForm As Form1
    Public sync_obj As Object = New Object
    Public _bgConnect As System.Threading.Thread = Nothing
    Public _bgMSGRec As System.Threading.Thread = Nothing

#Region "�v���g�R������"
    Private Delegate Sub InvokeProtocolDelegate(ByVal lth As TCP_Header, ByVal id As UShort, ByVal msg As String)
    Private Sub InvokeProtocol(ByVal lth As TCP_Header, ByVal id As UShort, ByVal msg As String)
        Select Case lth
            Case TCP_Header.ChatMSG '�`���b�g���b�Z�[�W���󂯎����
                PRC_ChatMSG(msg)
            Case TCP_Header.ItemUpload '�A�C�e�����A�b�v���[�h���ꂽ
                PRC_ItemUpload(id, msg)
            Case TCP_Header.ItemApp '�A�C�e�������F����
                PRC_ItemApp(id, msg)
            Case TCP_Header.ItemStop '�A�C�e�����ꎞ��~����
                PRC_ItemStop(id)
            Case TCP_Header.ItemReStart '�A�C�e���̓]�����ĊJ����
                PRC_ItemRestart(id)
            Case TCP_Header.ItemDelete '�A�C�e�����폜����
                PRC_ItemDelete(id)
            Case TCP_Header.ItemChangeQueue '�A�C�e���̗D�揇�ʂ�ύX����
                PRC_ItemChangeQueue(id, msg)
            Case TCP_Header.MakeFolder '�t�H���_�[���쐬���܂�
                PRC_MakeFolder(id, msg)
            Case TCP_Header.NextFolderFile '���̑��M�t�@�C���̃Z�b�g
                PRC_NextFolderFile(id, msg)
            Case TCP_Header.NextFolderFileOk '�t�H���_�[�]���ŃZ�b�g���Ă����t�@�C���̓]������������
                PRC_NextFolderFileOk(id)
            Case TCP_Header.FolderDone '�t�H���_�̓]������������
                PRC_FolderDone(id)
            Case TCP_Header.SetSeek '�A�b�v���[�h�t�@�C���̃V�[�N�ʒu�𒲐����܂�
                PRC_SetSeek(id, msg)
            Case TCP_Header.SendFileDone '�t�@�C���̑��M����������
                PRC_SendFileDone(id)
            Case TCP_Header.SyncItem '�A�C�e���̓�������
                PRC_SyncItem(msg)
            Case TCP_Header.MyVersion '�o�[�W�����`�F�b�N
                PRC_MyVersion(msg)
            Case TCP_Header.ChangeComment '�R�����g�̕ύX
                PRC_ChangeComment(msg)
            Case TCP_Header.ChangeAccountName '�A�J�E���g���̕ύX
                PRC_ChangeAccountName(msg)
            Case TCP_Header.ChangePort '�|�[�g�ԍ��̕ύX
                PRC_ChangePort(msg)
            Case TCP_Header.Share_GetList '�t�@�C�����L���X�g���擾����
                PRC_Share_GetList(msg)
            Case TCP_Header.Share_Download '���L���X�g����A�C�e����I�����_�E�����[�h����
                PRC_Share_Download(msg)
            Case TCP_Header.Share_List '���L�t�@�C���̃��X�g
                ShareInfo.ShowListItem(msg)
        End Select
    End Sub

    ''' <summary>
    ''' �`���b�g���b�Z�[�W����M����
    ''' </summary>
    ''' <param name="msg">�`���b�g�̃��b�Z�[�W</param>
    ''' <remarks></remarks>
    Private Sub PRC_ChatMSG(ByVal msg As String)
        '���b�Z�[�W�̉��
        Dim c_msg() As String = Split(msg, vbCrLf, 2)
        '�`���b�g�ɒǉ�
        ApendChat(c_msg(1), c_msg(0), Now, False)
        '�ʒm
        If ControledForm.appSettings.Notify_ReceiveMSG AndAlso Form.ActiveForm Is Nothing Then
            ControledForm.DoNotify(ControledForm.lang.AccountClass_NotifyMSG_Chat(IndividualData.AccountName), c_msg(1))
        End If
    End Sub

    ''' <summary>
    ''' �A�C�e���̃A�b�v���[�h
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="msg">���b�Z�[�W</param>
    ''' <remarks></remarks>
    Private Sub PRC_ItemUpload(ByVal ID As UShort, ByVal msg As String)
        Dim nInfo As TransferListViewItem = Nothing
        SyncLock sync_obj
            Dim fname As String() = Split(msg, vbCrLf, 4)

            '�t�@�C�����t�H���_��
            Dim li As Arachlex_Item = CType(fname(0), Arachlex_Item)
            Dim que_number As UShort = CType(fname(1), UShort)
            Dim boo_View As Boolean = (ControledForm.SelectUserAccount Is Nothing) OrElse (ControledForm.SelectUserAccount Is Me)
            If li = Arachlex_Item.File Then
                '�V�����C���X�^���X�̍쐬
                nInfo = New TransferListViewItem(Me, boo_View, ID, que_number, CLng(fname(3)), "", fname(2), li, Arachlex_Transport.Download)
            ElseIf li = Arachlex_Item.Folder Then
                '�V�����C���X�^���X�̍쐬
                nInfo = New TransferListViewItem(Me, boo_View, ID, que_number, 0, "", fname(2), li, Arachlex_Transport.Download)
                nInfo.FolderAllFiles = CInt(fname(3))
            End If
            AddTransportInfoArray(nInfo)
            '�ʒm����
            If ControledForm.appSettings.Notify_Upload Then
                ControledForm.DoNotify(ControledForm.lang.AccountClass_NotifyMSG_Upload(IndividualData.AccountName), fname(2))
            End If
        End SyncLock
        '�����ŏ��F����ꍇ
        If IndividualData.DownloadAutoSave Then
            UploadApporoval(nInfo, IndividualData.DownloadAutoSavePath)
        End If
    End Sub

    ''' <summary>
    ''' �A�C�e���̓]�������F���܂�
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="msg">���b�Z�[�W</param>
    ''' <remarks></remarks>
    Private Sub PRC_ItemApp(ByVal ID As UShort, ByVal msg As String)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Upload Then
                '�t�@�C���̏��F
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
                    '����Ƀt�H���_�쐬���w������
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
    ''' �A�C�e���̓]�����ꎞ��~���܂�
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
    ''' �A�C�e���̓]�����ĊJ���܂�
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
    ''' �A�C�e�����폜���܂�
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <remarks></remarks>
    Private Sub PRC_ItemDelete(ByVal ID As UShort)
        DeleteTransportInfoArray(GetInfoFromID(ID))
    End Sub

    ''' <summary>
    ''' �A�C�e���̗D�揇�ʂ�ύX���܂�
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="msg">���b�Z�[�W</param>
    ''' <remarks></remarks>
    Private Sub PRC_ItemChangeQueue(ByVal ID As UShort, ByVal msg As String)
        Dim tItem As TransferListViewItem = Me.GetInfoFromID(ID)
        If tItem IsNot Nothing Then
            SetQueue(tItem, CUShort(msg))
        End If
    End Sub

    ''' <summary>
    ''' �t�H���_���쐬���܂�
    ''' </summary>
    ''' <param name="ID">�Ώۂ�ID</param>
    ''' <param name="msg">���b�Z�[�W</param>
    ''' <remarks></remarks>
    Private Sub PRC_MakeFolder(ByVal ID As UShort, ByVal msg As String)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Download AndAlso tInfo.ItemAttribute = Arachlex_Item.Folder Then
                '�t�H���_�����
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
    ''' ���̃t�H���_�[���̃t�@�C���̃Z�b�g
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="msg">���b�Z�[�W</param>
    ''' <remarks></remarks>
    Private Sub PRC_NextFolderFile(ByVal ID As UShort, ByVal msg As String)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Download AndAlso tInfo.ItemAttribute = Arachlex_Item.Folder Then

                '���b�Z�[�W���
                Dim dmsg() As String = Split(msg, vbCrLf, 3)

                '�t�@�C���X�g���[���̊J��
                tInfo.FileStream = Nothing
                tInfo.FileCachePath = tInfo.FilePath & dmsg(0)

                '�z�u�ꏊ�̃f�B���N�g�������݂��Ȃ���΍��
                If Not IO.Directory.Exists(IO.Path.GetDirectoryName(tInfo.FileCachePath)) Then
                    IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(tInfo.FileCachePath))
                End If

                '�t�@�C���X�g���[�����쐬����
                If tInfo.CreateStream(tInfo.FileCachePath, False) Then
                    '�V�����t�@�C���̃T�C�Y
                    Dim fi As New IO.FileInfo(tInfo.FileCachePath)
                    '�]������K�v�����邩�m���߂�
                    If fi.Length < CLng(dmsg(1)) Then
                        '�V�[�N�ʒu�𒲐�����K�v�����邩
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
    ''' �t�H���_���M���̎��̃t�@�C�����Z�b�g���܂�
    ''' </summary>
    ''' <param name="ID">�Ώۂ�ID</param>
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
    ''' �t�H���_�]�����I��
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
    ''' �A�b�v���[�h�A�C�e���̃V�[�N�ʒu�𒲐����܂�
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="msg">���b�Z�[�W</param>
    ''' <remarks></remarks>
    Private Sub PRC_SetSeek(ByVal ID As UShort, ByVal msg As String)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            If tInfo IsNot Nothing AndAlso tInfo.ItemState = Arachlex_Transport.Upload Then
                tInfo.FileSeek = CLng(msg) '�V�[�N�̒���
            Else
                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, ID)
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' �t�@�C���f�[�^�̎�M
    ''' </summary>
    ''' <param name="ID">ID</param>
    ''' <param name="bytes">�f�[�^</param>
    ''' <remarks></remarks>
    Private Sub PRC_SendFile(ByVal ID As UShort, ByVal bytes() As Byte)
        SyncLock sync_obj
            Dim tInfo As TransferListViewItem = GetInfoFromID(ID)
            Try
                If tInfo Is Nothing OrElse tInfo.ItemState <> Arachlex_Transport.Download Then
                    '�A�C�e�������݂��A�_�E�����[�h�����̃A�C�e���ł��邩���ׂ�
                    Throw New Exception("Item does not exist or have different forward direction")
                ElseIf Not tInfo.NowTransporting Then '�]�����̃A�C�e�������ׂ�
                    Return
                End If

                '�]���f�[�^�̃V�[�N�|�W�V������ǂݎ��
                Dim SeekPosi As Long = BitConverter.ToInt64(bytes, 0)
                If SeekPosi <> tInfo.FileSeek Then
                    '�V�[�N�ʒu������Ă���ꍇ�A����̃V�[�N�ʒu�𒲐�
                    TCPConnect.SendPacket(CStr(tInfo.FileSeek), TCP_Header.SetSeek, tInfo.TransportID)
                    Return
                End If

                '�V�[�N�ʒu�̒���
                tInfo.FileStream.Position = tInfo.FileSeek
                '��������
                tInfo.FileStream.Write(bytes, 8, bytes.Length - 8) '8Byte�����̂́A�擪8�o�C�g�̓f�[�^�Ƃ͖��֌W�ȓ]�����Ɏg���w�b�_������
                '���̃V�[�N�ʒu���L��
                tInfo.FileSeek += (bytes.Length - 8)
                tInfo.FileStream.Flush()
                '�������������ׂ�
                If tInfo.FileSeek = tInfo.FileSize Then
                    If tInfo.ItemAttribute = Arachlex_Item.Folder Then
                        tInfo.FileStream = Nothing
                        tInfo.FolderSetedInItem = False
                    Else
                        tInfo.TransportStatus = TransferListViewItem.ItemStatus.Done
                        '�ꎞ�t�@�C������ʏ�t�@�C���ɖ��O�ύX
                        IO.File.Move(tInfo.FileCachePath, GetRepetitionFileName(tInfo.FilePath))
                    End If
                    '����Ɋ�����ʒm
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
    ''' �A�C�e�����M���I��
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
    ''' �A�C�e���𓯊����܂�
    ''' </summary>
    ''' <param name="msg">���b�Z�[�W</param>
    ''' <remarks></remarks>
    Private Sub PRC_SyncItem(ByVal msg As String)
        SyncLock sync_obj
            '����̃A�b�v���[�h���X�g
            Dim uStr As New Generic.List(Of String)
            uStr.AddRange(Split(msg, vbCrLf))

            If "nothing".Equals(msg, StringComparison.OrdinalIgnoreCase) OrElse uStr.Count = 0 Then
                '����̃A�b�v���[�h���X�g�ɉ����Ȃ��ꍇ
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
                '���g�̓]�����X�g���X�g����̏ꍇ�A����ɑS�ẴA�C�e���̍폜��v��
                For i As Integer = 0 To uStr.Count - 1
                    If uStr(i) IsNot Nothing Then
                        Dim ustr_id() As String = Split(uStr(i), vbCr, 2)
                        If ustr_id.Length = 2 Then
                            TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, CUShort(ustr_id(0)))
                        End If
                    End If
                Next
            Else '���g�̃��X�g�Əƍ�
                Dim MachItem As New Generic.List(Of TransferListViewItem) '�K�����X�g
                For i As Integer = 0 To uStr.Count - 1
                    If uStr(i) IsNot Nothing Then
                        Dim ustr_id() As String = Split(uStr(i), vbCr, 2)
                        If ustr_id.Length = 2 Then
                            '�A�C�e�������݂��邩���ׂ�
                            Dim tInfo As TransferListViewItem = GetInfoFromID(CUShort(ustr_id(0)))
                            If tInfo IsNot Nothing AndAlso tInfo.FileSize = CLng(ustr_id(1)) Then
                                '����̃A�C�e���ƈ�v����΁A�A�C�e����K�����X�g�ɒǉ�����
                                MachItem.Add(tInfo)
                            Else
                                '��v���Ȃ������ꍇ
                                TCPConnect.SendPacket(String.Empty, TCP_Header.ItemDelete, CUShort(ustr_id(0)))
                            End If
                        End If
                    End If
                Next

                '�K�����X�g�Ǝ��g�̃��X�g���r����
                If MachItem.Count = 0 Then
                    '�K�����X�g����̏ꍇ�A���g�̃_�E�����[�h���A�C�e���S�Ă��폜����
                    Dim h As Integer = 0
                    Do
                        If Not TransportItem(h).TransportingDone AndAlso TransportItem(h).ItemState = Arachlex_Transport.Download Then
                            DeleteTransportInfoArray(TransportItem(h))
                        Else
                            h += 1
                        End If
                    Loop Until h = TransportItem.Count
                Else
                    '�K�����X�g�ɑ��݂��Ȃ����g�̃_�E�����[�h���̃A�C�e����S�č폜����
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
                TCPConnect.SendPacket(String.Empty, TCP_Header.SendFileOk, 0) '�I��
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' �o�[�W�����`�F�b�N
    ''' </summary>
    ''' <param name="msg">���b�Z�[�W</param>
    ''' <remarks></remarks>
    Private Sub PRC_MyVersion(ByVal msg As String)
        '�o�[�W��������
        Dim ppv() As String = Split(msg, vbCrLf, 2) '0{�\�t�g�t���l�[��},1{�v���g�R���o�[�W����}
        TCPConnect.SoftVersion = ppv(0)
        TCPConnect.SoftProtocol = CInt(ppv(1))
    End Sub

    ''' <summary>
    ''' �R�����g�̍X�V
    ''' </summary>
    ''' <param name="msg">�R�����g</param>
    ''' <remarks></remarks>
    Private Sub PRC_ChangeComment(ByVal msg As String)
        '�X�V��ʒm����
        IndividualData.UserComment = msg

        '�A�J�E���g���X�g�̍X�V
        ControledForm.AccountListViewRedraw(Me.Index)
    End Sub

    ''' <summary>
    ''' �A�J�E���g���̍X�V
    ''' </summary>
    ''' <param name="msg">�A�J�E���g��</param>
    ''' <remarks></remarks>
    Private Sub PRC_ChangeAccountName(ByVal msg As String)
        If Not ControledForm.ExistAccountName(msg) Then
            IndividualData.AccountName = msg
            ControledForm.AccountListViewRedraw(Me.Index)
        End If
    End Sub

    ''' <summary>
    ''' �|�[�g�̕ύX
    ''' </summary>
    ''' <param name="msg">�|�[�g�ԍ�</param>
    ''' <remarks></remarks>
    Private Sub PRC_ChangePort(ByVal msg As String)
        Try
            IndividualData.ConnectPort = CType(msg, UShort)
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' �w�肳�ꂽ�p�X�̓��e���擾���܂�
    ''' </summary>
    ''' <param name="msg">���b�Z�[�W</param>
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
    ''' �t�@�C�����L���X�g�̎w��p�X����A�C�e�����_�E�����[�h���܂�
    ''' </summary>
    ''' <param name="msg">���b�Z�[�W</param>
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

#Region "�X���b�h"
    ''' <summary>
    ''' bgConnect�X���b�h���J�n���܂��B���ɃX���b�h�������Ă���ꍇ�A�������܂���
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
    ''' bgConnect�X���b�h���~�����܂�
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
    ''' �ڑ������s���邽�߂̃X���b�h�p���\�b�h�ł�
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

                '�F�؂���
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
    ''' bgMSGRec�X���b�h���J�n���܂��B���ɃX���b�h�������Ă���ꍇ�A�������܂���
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
    ''' bgMSGRec�X���b�h���~�����܂�
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
    ''' ���b�Z�[�W����M���邽�߂̃X���b�h�p���\�b�h�ł�
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
                    Case TCP_Header.SendFile '�t�@�C���̃f�[�^�ł���ꍇ
                        PRC_SendFile(p_id, p_Byte)
                    Case TCP_Header.SendFileOk '���M�����t�@�C�����͂������}
                        Dim msg As String = enc.GetString(p_Byte)
                        '�o�b�t�@����
                        Dim n As Integer = (Environment.TickCount - LastSendTime)
                        If n = 0 Then
                            MainSendBuffer *= 2
                        Else
                            MainSendBuffer = CInt(MainSendBuffer * (1000 / n))
                        End If
                        SendFileData()
                    Case TCP_Header.JUNK '�W�����N�f�[�^
                        '���̃w�b�_�͒ʐM���m�F���邽�߂̃��m�Ȃ̂ŁA��������R�[�h�͑��݂��Ȃ��B
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

#Region "�X���b�h�Z�[�t�֐�"
    ''' <summary>
    ''' �ڑ����Ƃ����Ƃ��ɂ��邱��
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
    ''' �ڑ��������ɂ��邱��
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

        '���C���t�H�[����Enabled���X�V
        ControledForm.ChangeEnabled()

        '����Ɏ��g�̃o�[�W������ʒm
        TCPConnect.SendPacket(SoftwareVersion & vbCrLf & ProtocolVersion, TCP_Header.MyVersion, UShort.MinValue)

        '���g�̋��L���X�g�̑��M
        If IndividualData.Share_Use Then
            TCPConnect.SendPacket(ShareInfo.GetOpenList(ShareClass.ShareKind.ROOT, -1), TCP_Header.Share_List, UShort.MinValue)
        End If

        '���g�̃|�[�g�ԍ��𑗐M
        TCPConnect.SendPacket(CStr(ControledForm.appSettings.Connect_ListenPort), TCP_Header.ChangePort, UShort.MinValue)

        '���g�̃R�����g�𑗐M
        TCPConnect.SendPacket(ControledForm.appSettings.Account_Comment, TCP_Header.ChangeComment, UShort.MinValue)

        '�Đڑ����ɂ��邨�݂��̃A�b�v���[�h���X�g�ƃ_�E�����[�h���X�g�̏ƍ�
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
    ''' ���X�g�r���[�֓o�^���܂�
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
    ''' �󂢂Ă���t�@�C���A�b�v���[�hID�𒲂ׂ܂�
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
    ''' ���Ɏg����t���[�̃L���[�C���O�ԍ����擾���܂�
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
    ''' �L���[�C���O���X�g����w�肵���A�C�e�������O����B
    ''' </summary>
    ''' <param name="tItem">�L���[�C���O����Remove���ꂽ�A�C�e��</param>
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
    ''' �D�揇�ʂ�C�ӂ̒l�ɃZ�b�g���Ȃ����܂�
    ''' </summary>
    ''' <param name="tItem">�Ώۂ̓]���A�C�e��</param>
    ''' <param name="afterQueue">�ύX��̗D�揇��</param>
    ''' <remarks></remarks>
    Public Sub SetQueue(ByVal tItem As TransferListViewItem, ByVal afterQueue As UShort)
        SyncLock sync_obj
            If tItem IsNot Nothing AndAlso Not tItem.TransportingDone AndAlso Me.TransportItem IsNot Nothing AndAlso Me.TransportItem.Count > 0 Then
                If afterQueue > tItem.TransportQueue Then

                    Dim n As Integer = 0 '�ύX�O�̗D�揇�ʂƕύX��̗D�揇�ʂ̊Ԃɉ����A�q�b�g�������B
                    For i As Integer = 0 To TransportItem.Count - 1
                        Dim tInfo As TransferListViewItem = TransportItem(i)
                        If tItem.ItemState = tInfo.ItemState AndAlso Not tInfo.TransportingDone AndAlso _
                        tItem.TransportQueue < tInfo.TransportQueue AndAlso afterQueue >= tInfo.TransportQueue Then

                            '�ύX��ƕύX�O�̗D�揇�ʂ̊Ԃɂ���(After >= Queue > Before)�D�揇�ʂ��P���Ɉړ��B
                            tInfo.TransportQueue = CUShort(tInfo.TransportQueue - 1)
                            n += 1
                        End If
                    Next

                    tItem.TransportQueue = CUShort(tItem.TransportQueue + n)

                ElseIf afterQueue < tItem.TransportQueue Then

                    Dim n As Integer = 0 '�ύX�O�̗D�揇�ʂƕύX��̗D�揇�ʂ̊Ԃɉ����A�q�b�g�������B
                    For i As Integer = 0 To TransportItem.Count - 1
                        Dim tInfo As TransferListViewItem = TransportItem(i)
                        If tItem.ItemState = tInfo.ItemState AndAlso Not tInfo.TransportingDone AndAlso _
                        tItem.TransportQueue > tInfo.TransportQueue AndAlso afterQueue <= tInfo.TransportQueue Then

                            '�ύX��ƕύX�O�̗D�揇�ʂ̊Ԃɂ���(Before > Queue => After)�D�揇�ʂ��P��Ɉړ��B
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
    ''' ���M���̃A�C�e�������邩���ׂ܂��BTrue�Ȃ炠��
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
    ''' �A�C�e���z�񂩂�w��ID�̃_�E�������擾���܂��B������Ȃ��ꍇ�A-1��Ԃ��܂��B
    ''' </summary>
    ''' <param name="id">ID�ԍ�</param>
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
    ''' �ł��D�揇�ʂ̍����]���A�C�e�����擾����
    ''' </summary>
    ''' <param name="itemState">�A�C�e���̏��</param>
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
    ''' �f�[�^�𑗐M���܂�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SendFileData()
        SyncLock sync_obj
            Dim tItem As TransferListViewItem = GetMostPriorityQueueItem(Arachlex_Transport.Upload)
            If tItem IsNot Nothing Then
                Dim recBytes(MainSendBuffer + 8) As Byte '�ǂݎ��o�b�t�@
                '���M�f�[�^�ɃV�[�N�ʒu����������
                Array.Copy(BitConverter.GetBytes(tItem.FileSeek), 0, recBytes, 0, 8)
                Try
                    '�V�[�N�ʒu�̒���
                    tItem.FileStream.Position = tItem.FileSeek
                    '�ǂݎ��
                    Dim recSize As Integer = tItem.FileStream.Read(recBytes, 8, recBytes.Length - 8)
                    If recSize < recBytes.Length - 8 Then
                        Array.Resize(recBytes, recSize + 8)
                    End If
                    tItem.FileSeek += recSize '���̃V�[�N�ʒu���L��
                    TCPConnect.SendPacket(recBytes, TCP_Header.SendFile, tItem.TransportID) '���M����
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
    ''' ���̃t�@�C���𑊎�ɃZ�b�g���܂�
    ''' </summary>
    ''' <param name="fInfo">�A�C�e�����</param>
    ''' <remarks></remarks>
    Private Sub NextFolderFile(ByVal fInfo As TransferListViewItem)
        SyncLock sync_obj
            If fInfo IsNot Nothing AndAlso fInfo.ItemState = Arachlex_Transport.Upload AndAlso fInfo.ItemAttribute = Arachlex_Item.Folder Then
                fInfo.FolderSetedInItem = False
                fInfo.FileStream = Nothing
                Do
                    If fInfo.FolderFiles Is Nothing OrElse fInfo.FolderFiles.Count = 0 Then
                        '�]������
                        fInfo.TransportStatus = TransferListViewItem.ItemStatus.Done
                        TCPConnect.SendPacket(String.Empty, TCP_Header.FolderDone, fInfo.TransportID)
                        Return
                    ElseIf fInfo.CreateStream(fInfo.FolderFiles(0), False) Then
                        '�]���A�C�e�����Z�b�g����
                        '�t�@�C���T�C�Y
                        Dim fi As New IO.FileInfo(fInfo.FolderFiles(0))
                        fInfo.FileSize = fi.Length
                        fInfo.FileSeek = 0
                        fInfo.FolderDoneFiles += 1
                        fInfo.FileCachePath = fInfo.FolderFiles(0)
                        '����Ƀf�[�^�𑗐M����
                        TCPConnect.SendPacket(Replace(fInfo.FolderFiles(0), fInfo.FilePath, "") & vbCrLf & _
                        fi.Length & vbCrLf & _
                        fInfo.FolderDoneFiles _
                        , TCP_Header.NextFolderFile, fInfo.TransportID)
                        '�]���c��t�@�C���̐���
                        fInfo.FolderFiles.RemoveAt(0)
                        Return
                    Else '�X�g���[�����擾�ł��Ȃ������Ƃ�
                        fInfo.FolderDoneFiles += 1
                        fInfo.FolderFiles.RemoveAt(0)
                    End If
                Loop Until Not TCPConnect.AceptInfo
            End If
        End SyncLock
    End Sub
#End Region

#Region "�֐�"
    ''' <summary>
    ''' �A�C�e����]�����X�g����폜���܂�
    ''' </summary>
    ''' <param name="tInfo">�Ώۂ̓]���A�C�e��</param>
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
    ''' �A�C�e����]�����X�g�ɒǉ����܂�
    ''' </summary>
    ''' <param name="tinfo">�Ώۂ̃A�C�e��</param>
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
    ''' �w�肵���A�C�e�����A�b�v���[�h���܂��B���s�s�\�ȏꍇFalse
    ''' </summary>
    ''' <param name="fPath">�A�b�v���[�h����p�X</param>
    ''' <param name="ShowError">�G���[��\�����邩</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UploadItem(ByVal fPath As String, ByVal ShowError As Boolean) As Boolean
        If fPath Is Nothing OrElse fPath.Length = 0 Then
            Return True
        End If

        If Not TCPConnect.AceptInfo Then
            Return False
        End If

        '�c��ID�`�F�b�N
        Dim nID As UShort = GetUpEmptyItemID()
        If nID = 0 Then
            If ShowError Then
                MessageBox.Show(ControledForm.lang.AccountClass_DialogMSG_MaxNumUpload, SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            Return False
        End If

        If IO.File.Exists(fPath) Then
            '�t�@�C���T�C�Y
            Dim fi As New System.IO.FileInfo(fPath)

            '���쐬
            Dim boo_View As Boolean = (ControledForm.SelectUserAccount Is Nothing) OrElse (ControledForm.SelectUserAccount Is Me)
            Dim uInfo As New TransferListViewItem(Me, boo_View, nID, GetNextQueue, fi.Length, fPath, IO.Path.GetFileName(fPath), Arachlex_Item.File, Arachlex_Transport.Upload)
            AddTransportInfoArray(uInfo)

            '�X�g���[���쐬
            If uInfo.CreateStream(fPath, ShowError) Then
                '����ɃA�b�v���[�h��ʒm
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
            '���쐬
            Dim boo_View As Boolean = (ControledForm.SelectUserAccount Is Nothing) OrElse (ControledForm.SelectUserAccount Is Me)
            Dim uInfo As New TransferListViewItem(Me, boo_View, nID, GetNextQueue, 0, fPath, IO.Path.GetFileName(fPath), Arachlex_Item.Folder, Arachlex_Transport.Upload)

            '�t�H���_�������W
            Dim files As String() = System.IO.Directory.GetFiles(fPath, "*", System.IO.SearchOption.AllDirectories)
            uInfo.FolderFiles.AddRange(files)
            uInfo.FolderAllFiles = files.Length

            '����ǉ�
            AddTransportInfoArray(uInfo)

            '����ɃA�b�v���[�h��ʒm
            TCPConnect.SendPacket(CInt(Arachlex_Item.Folder) & vbCrLf & _
            CStr(uInfo.TransportQueue) & vbCrLf & _
            IO.Path.GetFileName(fPath) & vbCrLf & _
            uInfo.FolderAllFiles, _
            TCP_Header.ItemUpload, nID)
        End If
        Return True
    End Function

    ''' <summary>
    ''' �w�肵���A�C�e���̃_�E�����[�h�����F���܂�
    ''' </summary>
    ''' <param name="dInfo">���F����_�E�����[�h���</param>
    ''' <param name="savepath">�_�E�����[�h��t�H���_</param>
    ''' <remarks></remarks>
    Public Sub UploadApporoval(ByVal dInfo As TransferListViewItem, ByVal savepath As String)
        If dInfo Is Nothing OrElse dInfo.ItemState = Arachlex_Transport.Upload _
        OrElse dInfo.TransportStatus <> TransferListViewItem.ItemStatus.NotApproval OrElse Not TCPConnect.AceptInfo Then
            Return
        End If

        If dInfo.ItemAttribute = Arachlex_Item.File Then '�t�@�C���̏ꍇ
            '�_�E�����[�h�ꏊ��ݒ�
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

                '�_�C�A���O�̕\��
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
                '�_�C�A���O���o���Ă���ԂɕύX����ĂȂ����`�F�b�N����
                If dInfo IsNot Nothing AndAlso dInfo.TransportStatus = TransferListViewItem.ItemStatus.NotApproval Then
                    '�L���b�V���p�X���擾
                    Dim streamPath As String = GetRepetitionFileName(dInfo.FilePath & CacheExt)
                    If dInfo.CreateStream(streamPath, True) Then
                        dInfo.FileCachePath = streamPath
                        dInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled
                        '����ɒʒm
                        TCPConnect.SendPacket("0", TCP_Header.ItemApp, dInfo.TransportID)
                    End If
                End If
            End SyncLock
        ElseIf dInfo.ItemAttribute = Arachlex_Item.Folder Then '�t�H���_�̏ꍇ
            '�ۑ��ʒu�̐ݒ�
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
                '�_�C�A���O���o���Ă���ԂɕύX����ĂȂ����`�F�b�N����
                If dInfo IsNot Nothing AndAlso dInfo.TransportStatus = TransferListViewItem.ItemStatus.NotApproval AndAlso Not dInfo.Transporting Then
                    dInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled
                    '�g�b�v�f�B���N�g�����쐬
                    IO.Directory.CreateDirectory(dInfo.FilePath)
                    '����ɒʒm
                    TCPConnect.SendPacket(String.Empty, TCP_Header.ItemApp, dInfo.TransportID)
                End If
            End SyncLock
        End If
    End Sub

    ''' <summary>
    ''' ���W���[�����F���܂�
    ''' </summary>
    ''' <param name="dInfo">�Ώۂ̃A�C�e��</param>
    ''' <param name="path">�ۑ���</param>
    ''' <remarks></remarks>
    Public Sub ResumeApporoval(ByVal dInfo As TransferListViewItem, ByVal path As String)
        If dInfo Is Nothing OrElse Not TCPConnect.AceptInfo OrElse dInfo.ItemState = Arachlex_Transport.Upload _
        OrElse dInfo.TransportStatus <> TransferListViewItem.ItemStatus.NotApproval Then
            Return
        End If

        If dInfo.ItemAttribute = Arachlex_Item.File Then '�t�@�C���̏ꍇ
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
                '���b�Z�[�W�{�b�N�X�\�����ɑ���ŕύX����ĂȂ����`�F�b�N����
                If dInfo IsNot Nothing AndAlso dInfo.TransportStatus = TransferListViewItem.ItemStatus.NotApproval Then
                    '�X�g���[�����擾�ł��邩�`�F�b�N
                    If dInfo.CreateStream(rFile, True) Then
                        dInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled
                        If CacheExt.Equals(IO.Path.GetExtension(rFile), StringComparison.OrdinalIgnoreCase) Then
                            dInfo.FilePath = IO.Path.ChangeExtension(rFile, Nothing)
                        Else
                            dInfo.FilePath = rFile
                        End If
                        dInfo.FileCachePath = rFile
                        dInfo.FileSeek = fl.Length

                        '����ɒʒm����
                        TCPConnect.SendPacket(fl.Length.ToString, TCP_Header.ItemApp, dInfo.TransportID)
                    End If
                End If
            End SyncLock
        ElseIf dInfo.ItemAttribute = Arachlex_Item.Folder Then '�t�H���_�̏ꍇ
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
                '���b�Z�[�W�{�b�N�X�\�����ɑ���ŕύX����ĂȂ����`�F�b�N����
                If dInfo IsNot Nothing AndAlso dInfo.TransportStatus = TransferListViewItem.ItemStatus.NotApproval Then
                    dInfo.TransportStatus = TransferListViewItem.ItemStatus.Approvaled
                    TCPConnect.SendPacket(String.Empty, TCP_Header.ItemApp, dInfo.TransportID)
                End If
            End SyncLock
        End If
    End Sub

    ''' <summary>
    ''' �`���b�g���b�Z�[�W��ǉ����܂�
    ''' </summary>
    ''' <param name="msg">���e���b�Z�[�W</param>
    ''' <param name="raiseName">���e�Җ�</param>
    ''' <param name="postDate">���e����</param>
    ''' <param name="rais">���������e�҂�</param>
    ''' <remarks></remarks>
    Public Sub ApendChat(ByVal msg As String, ByVal raiseName As String, ByVal postDate As Date, ByVal rais As Boolean)
        If msg.Length > 0 Then
            'Chat�E�B���h�E�̃C���X�^���X���܂��m�ۂ���Ă��Ȃ��ꍇ
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

            '���݂̃e�L�X�g�����L�^
            Dim bifLen As Integer = ChatWindow.Chat_View.TextLength
            ChatWindow.Chat_View.AppendText(h_name & vbCrLf & msg & vbCrLf)

            '�F�Â�
            ChatWindow.Chat_View.SelectionStart = bifLen
            ChatWindow.Chat_View.SelectionLength = h_name.Length
            If rais Then
                ChatWindow.Chat_View.SelectionColor = Color.Blue
            Else
                ChatWindow.Chat_View.SelectionColor = Color.Green
            End If
            '�X�N���[������
            ChatWindow.Chat_View.SelectionStart = ChatWindow.Chat_View.TextLength + 4
            ChatWindow.Chat_View.ScrollToCaret()
        End If
    End Sub
#End Region

#Region "�v���p�e�B"
    Private _ChatWindow As Form_Chat
    ''' <summary>
    ''' �`���b�g�E�B���h�E
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
    ''' �l�f�[�^�Ȃ�
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
    ''' ���[�U�[�̃`���b�g����
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
    ''' ���̃��[�U�[�Ɠ]�����Ă���A�C�e��
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
    ''' ���L�����Ǘ�����
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
    ''' TCP�ڑ����
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
    ''' �t�@�C���f�[�^���M�Ԋu
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
    ''' ���M�o�b�t�@
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
    ''' �R�X�g���N�^
    ''' </summary>
    ''' <param name="AForm">�I�[�i�[�t�H�[��</param>
    ''' <param name="StartTryConnection">�C���X�^���X�������ɃA�J�E���g�ւ̐ڑ����s���J�n���邩</param>
    ''' <param name="AddListView">���X�g�r���[�ɃA�C�e����ǉ����邩</param>
    ''' <param name="AAccountData">�A�J�E���g���</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal AForm As Form1, ByVal StartTryConnection As Boolean, ByVal AddListView As Boolean, ByVal AAccountData As AccountData)
        ControledForm = AForm
        ChatWindow = Nothing

        '������
        _TransportItem = New List(Of TransferListViewItem)
        _ShareInfo = New ShareClass(ControledForm, AAccountData)
        _TCPConnect = New TCPNetworkClass(enc)
        _UserChat = ""
        _LastSendTime = 0
        _MainSendBuffer = Transport_SendBufferSize_Default
        _IndividualData = AAccountData

        '���X�g�{�b�N�X�A�C�e���̃��X�g�{�b�N�X�ւ̒ǉ�
        If AddListView Then
            AddToListView()
        End If

        '�Í����I�v�V�������Z�b�g
        TCPConnect.UseEncrypt = IndividualData.UseEncryptNetwork

        '�ڑ����s�J�n
        If StartTryConnection Then
            Start_bgConnect()
        End If
    End Sub
End Class