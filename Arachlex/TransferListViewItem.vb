Imports Arachlex.DefinitionClass
''' <summary>
''' �]���A�C�e���̏���ێ�����N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class TransferListViewItem
    Inherits ListViewItem

    Public Enum ItemStatus
        ErrorDone
        Done
        Approvaled
        NotApproval
    End Enum

    ''' <summary>
    ''' ���X�g�r���[�A�C�e����o�^���܂�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RegisterListViewItem()
        If OwnerAccount.ControledForm.InvokeRequired Then
            Dim n As New RegisterListViewItemDelegate(AddressOf RegisterListViewItemCallBack)
            OwnerAccount.ControledForm.BeginInvoke(n, New Object() {})
        Else
            RegisterListViewItemCallBack()
        End If
    End Sub
    Private Delegate Sub RegisterListViewItemDelegate()
    Private Sub RegisterListViewItemCallBack()
        '�O���[�v�ɕ�����
        If _ItemState = Arachlex_Transport.Download Then
            Me.Group = _OwnerAccount.ControledForm.Transfer_ListView.Groups("GTransfer_Download")
        ElseIf _ItemState = Arachlex_Transport.Upload Then
            Me.Group = _OwnerAccount.ControledForm.Transfer_ListView.Groups("GTransfer_Upload")
        End If

        OwnerAccount.ControledForm.Transfer_ListView.Items.Add(Me)
    End Sub

    ''' <summary>
    ''' �X�e�[�^�X�ɕ`�悷�ׂ���������擾���܂�
    ''' </summary>
    ''' <param name="lang">����N���X</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStatusStr(ByVal lang As LanguageClass) As String
        Dim str As String = ""
        If TransportStatus = TransferListViewItem.ItemStatus.NotApproval Then
            str = lang.MainForm_Drawing_Transport_NotApproval
        ElseIf TransportingDone Then
            If TransportStatus = TransferListViewItem.ItemStatus.ErrorDone Then
                str = lang.MainForm_Drawing_Transport_ErrorDone
            ElseIf TransportStatus = TransferListViewItem.ItemStatus.Done Then
                str = lang.MainForm_Drawing_Transport_Done
            End If
        ElseIf DownloadStop Then
            str = lang.MainForm_Drawing_Transport_DownloadStop
        ElseIf UploadStop Then
            str = lang.MainForm_Drawing_Transport_UploadStop
        Else
            Dim tItem_highest As TransferListViewItem = OwnerAccount.GetMostPriorityQueueItem(ItemState)
            If tItem_highest Is Me Then
                If ItemAttribute = Arachlex_Item.Folder Then
                    str = Replace(FileCachePath, FilePath, "", 1)
                Else
                    str = Format(Math.Round(FileSeek / FileSize, 3) * 100, "#0.0") & "%"
                End If
            Else
                str = lang.MainForm_Drawing_Transport_QueueStop
            End If
        End If
        Return str
    End Function

    ''' <summary>
    ''' �t�@�C���X�g���[���𐶐����܂�
    ''' </summary>
    ''' <param name="fpath">�t�@�C���p�X</param>
    ''' <param name="errorMSG">�G���[���b�Z�[�W</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateStream(ByVal fpath As String, ByVal errorMSG As Boolean) As Boolean
        Me.FileStream = Nothing
        Try
            If ItemState = Arachlex_Transport.Download Then
                Me.FileStream = New System.IO.FileStream(fpath, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.Read, 32768, IO.FileOptions.SequentialScan)
                Return True
            ElseIf ItemState = Arachlex_Transport.Upload Then
                Me.FileStream = New System.IO.FileStream(fpath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read, 32768, IO.FileOptions.SequentialScan)
                Return True
            End If
        Catch ex As Exception
            If errorMSG Then MessageBox.Show(ex.Message, SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' �]�����ꎞ��~���܂�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopTransport()
        If ItemState = Arachlex_Transport.Download Then
            _UploadStop = True
        ElseIf ItemState = Arachlex_Transport.Upload Then
            _DownloadStop = True
        End If
    End Sub

    ''' <summary>
    ''' �]�����ĊJ���܂�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RestartTransport()
        If ItemState = Arachlex_Transport.Download Then
            _UploadStop = False
        ElseIf ItemState = Arachlex_Transport.Upload Then
            Dim nDB As Boolean = OwnerAccount.GetSendItems()
            _DownloadStop = False
            If Not nDB Then
                OwnerAccount.SendFileData()
            End If
        End If
    End Sub

    Protected _OwnerAccount As AccountListViewItem
    ''' <summary>
    ''' ���̃A�C�e����]�����Ă���I�[�i�[�̏��ł�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OwnerAccount() As AccountListViewItem
        Get
            Return _OwnerAccount
        End Get
        Set(ByVal value As AccountListViewItem)
            _OwnerAccount = value
        End Set
    End Property

    Protected _FileName As String
    ''' <summary>
    ''' �t�@�C����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileName() As String
        Get
            Return _FileName
        End Get
    End Property

    Protected _FileCachePath As String
    ''' <summary>
    ''' �_�E�����[�h�ꎞ�ۑ��p�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FileCachePath() As String
        Get
            Return _FileCachePath
        End Get
        Set(ByVal value As String)
            _FileCachePath = value
        End Set
    End Property

    Protected _FilePath As String
    ''' <summary>
    ''' �t�@�C���t���p�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilePath() As String
        Get
            Return _FilePath
        End Get
        Set(ByVal value As String)
            _FilePath = value
        End Set
    End Property

    Protected _FileSize As Long
    ''' <summary>
    ''' �t�@�C���T�C�Y
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FileSize() As Long
        Get
            Return _FileSize
        End Get
        Set(ByVal value As Long)
            _FileSize = value
        End Set
    End Property

    Protected _FileSeek As Long
    ''' <summary>
    ''' �t�@�C���V�[�N
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FileSeek() As Long
        Get
            Return _FileSeek
        End Get
        Set(ByVal value As Long)
            If value >= 0 AndAlso value <= FileSize Then
                _FileSeek = value
            End If
        End Set
    End Property

    Protected _FileStream As IO.FileStream
    ''' <summary>
    ''' �t�@�C���X�g���[��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FileStream() As IO.FileStream
        Get
            Return _FileStream
        End Get
        Set(ByVal value As IO.FileStream)
            SyncLock OwnerAccount.sync_obj
                If value Is Nothing Then
                    Try
                        If _FileStream IsNot Nothing Then
                            _FileStream.Flush()
                            _FileStream.Close()
                            _FileStream.Dispose()
                        End If
                    Catch
                    Finally
                        _FileStream = Nothing
                    End Try
                Else
                    _FileStream = value
                End If
            End SyncLock
        End Set
    End Property

    Protected _TransportID As UShort
    ''' <summary>
    ''' �]��ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransportID() As UShort
        Get
            Return _TransportID
        End Get
    End Property

    Protected _TransportQueue As UShort
    ''' <summary>
    ''' �L���[�C���O�ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransportQueue() As UShort
        Get
            Return _TransportQueue
        End Get
        Set(ByVal value As UShort)
            _TransportQueue = value
        End Set
    End Property

    Protected _TransportStatus As ItemStatus
    ''' <summary>
    ''' �A�C�e���̓]����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransportStatus() As ItemStatus
        Get
            Return _TransportStatus
        End Get
        Set(ByVal value As ItemStatus)
            _TransportStatus = value

            If _TransportStatus = ItemStatus.Done OrElse _TransportStatus = ItemStatus.ErrorDone Then
                FileStream = Nothing
                OwnerAccount.RemoveQueue(Me)
            End If
        End Set
    End Property

    ''' <summary>
    ''' �]�����̃A�C�e����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Transporting() As Boolean
        Get
            Return TransportStatus = ItemStatus.Approvaled AndAlso Not UploadStop AndAlso Not DownloadStop
        End Get
    End Property

    ''' <summary>
    ''' �t�H���_�]���̏󋵂��܂߂āA�]������
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property NowTransporting() As Boolean
        Get
            Return Transporting AndAlso (ItemAttribute <> Arachlex_Item.Folder OrElse (ItemAttribute = Arachlex_Item.Folder AndAlso FolderSetedInItem))
        End Get
    End Property

    ''' <summary>
    ''' �A�C�e���̓]�����I�����Ă��邩
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransportingDone() As Boolean
        Get
            Return TransportStatus = ItemStatus.Done OrElse TransportStatus = ItemStatus.ErrorDone
        End Get
    End Property

    Protected _UploadStop As Boolean
    ''' <summary>
    ''' �A�b�v���[�h��~����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UploadStop() As Boolean
        Get
            Return _UploadStop
        End Get
    End Property

    Protected _DownloadStop As Boolean
    ''' <summary>
    ''' �_�E�����[�h��~����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DownloadStop() As Boolean
        Get
            Return _DownloadStop
        End Get
    End Property

    Protected _ItemAttribute As Arachlex_Item
    ''' <summary>
    ''' �A�C�e���̎��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ItemAttribute() As Arachlex_Item
        Get
            Return _ItemAttribute
        End Get
    End Property

    Protected _ItemState As Arachlex_Transport
    ''' <summary>
    ''' �A�C�e���̓]�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ItemState() As Arachlex_Transport
        Get
            Return _ItemState
        End Get
    End Property

    Protected _FolderSetedInItem As Boolean
    ''' <summary>
    ''' �t�H���_�̒��Ɋ܂܂��t�@�C�����Z�b�g���Ă��邩�BFalse�ł��Ă��Ȃ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FolderSetedInItem() As Boolean
        Get
            Return _FolderSetedInItem
        End Get
        Set(ByVal value As Boolean)
            _FolderSetedInItem = value
        End Set
    End Property

    Protected _FolderDoneFiles As Integer
    ''' <summary>
    ''' �t�H���_�]���ŏI�������t�@�C����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FolderDoneFiles() As Integer
        Get
            Return _FolderDoneFiles
        End Get
        Set(ByVal value As Integer)
            _FolderDoneFiles = value
        End Set
    End Property

    Protected _FolderAllFiles As Integer
    ''' <summary>
    ''' �t�H���_�]���œ]������S�Ẵt�@�C����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FolderAllFiles() As Integer
        Get
            Return _FolderAllFiles
        End Get
        Set(ByVal value As Integer)
            _FolderAllFiles = value
        End Set
    End Property

    Protected _FolderFiles As Generic.List(Of String)
    ''' <summary>
    ''' �t�H���_�A�b�v���[�h���̓]������t�H���_���̃t�@�C��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FolderFiles() As Generic.List(Of String)
        Get
            Return _FolderFiles
        End Get
        Set(ByVal value As Generic.List(Of String))
            _FolderFiles = value
        End Set
    End Property

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <param name="AddListView">���X�g�r���[�A�C�e�������X�g�r���[�ɒǉ����邩�ǂ���</param>
    ''' <param name="cID">ID</param>
    ''' <param name="cFileSize">�t�@�C���T�C�Y</param>
    ''' <param name="cFilePath">�t�@�C���p�X</param>
    ''' <param name="cFileName">�t�@�C����</param>
    ''' <param name="cFolder">�A�C�e���̎��</param>
    ''' <param name="cTransporter">�A�C�e���̓]�����</param>
    ''' <param name="owAccount">�I�[�i�[�A�J�E���g</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal owAccount As AccountListViewItem, ByVal AddListView As Boolean, ByVal cID As UShort, _
    ByVal cQueue As UShort, ByVal cFileSize As Long, ByVal cFilePath As String, ByVal cFileName As String, _
    ByVal cFolder As Arachlex_Item, ByVal cTransporter As Arachlex_Transport)
        _OwnerAccount = owAccount
        _FileName = cFileName
        _FileCachePath = ""
        _FilePath = cFilePath
        _FileSize = cFileSize
        _FileSeek = 0
        _FileStream = Nothing
        _TransportID = cID
        _TransportQueue = cQueue
        _TransportStatus = ItemStatus.NotApproval
        _UploadStop = False
        _DownloadStop = False
        _ItemAttribute = cFolder
        _ItemState = cTransporter
        _FolderSetedInItem = False
        _FolderDoneFiles = 0
        _FolderAllFiles = 0
        _FolderFiles = New Generic.List(Of String)

        '���X�g�r���[�𐶐�����
        If AddListView Then
            RegisterListViewItem()
        End If
    End Sub
End Class