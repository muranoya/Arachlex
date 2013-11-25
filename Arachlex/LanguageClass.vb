<Serializable()> Public Class LanguageClass

    Private a As New List(Of String)

    Public Enum LanguageType
        Japanese
        English
        LanguageFile
    End Enum

    Private _DateType As String
    ''' <summary>
    ''' DateType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DateType() As String
        Get
            Return _DateType
        End Get
        Set(ByVal value As String)
            _DateType = value
        End Set
    End Property

#Region "MainWindow CAccount"
    Private _MainForm_CAccount_Connect As String
    ''' <summary>
    ''' MainWindow Account ContextMenu Connect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CAccount_Connect() As String
        Get
            Return _MainForm_CAccount_Connect
        End Get
        Set(ByVal value As String)
            _MainForm_CAccount_Connect = value
        End Set
    End Property

    Private _MainForm_CAccount_Reconnect As String
    ''' <summary>
    ''' MainWindow Account ContextMenu Reconnect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CAccount_Reconnect() As String
        Get
            Return _MainForm_CAccount_Reconnect
        End Get
        Set(ByVal value As String)
            _MainForm_CAccount_Reconnect = value
        End Set
    End Property

    Private _MainForm_CAccount_Chat As String
    ''' <summary>
    ''' MainWindow Account ContextMenu Chat
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CAccount_Chat() As String
        Get
            Return _MainForm_CAccount_Chat
        End Get
        Set(ByVal value As String)
            _MainForm_CAccount_Chat = value
        End Set
    End Property

    Private _MainForm_CAccount_AddAccount As String
    ''' <summary>
    ''' MainWindow Account ContextMenu AddAccount
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CAccount_AddAccount() As String
        Get
            Return _MainForm_CAccount_AddAccount
        End Get
        Set(ByVal value As String)
            _MainForm_CAccount_AddAccount = value
        End Set
    End Property

    Private _MainForm_CAccount_EditAccount As String
    ''' <summary>
    ''' MainWindow Account ContextMenu EditAccount
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CAccount_EditAccount() As String
        Get
            Return _MainForm_CAccount_EditAccount
        End Get
        Set(ByVal value As String)
            _MainForm_CAccount_EditAccount = value
        End Set
    End Property

    Private _MainForm_CAccount_DeleteAccount As String
    ''' <summary>
    ''' MainWindow Account ContextMenu DeleteAccount
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CAccount_DeleteAccount() As String
        Get
            Return _MainForm_CAccount_DeleteAccount
        End Get
        Set(ByVal value As String)
            _MainForm_CAccount_DeleteAccount = value
        End Set
    End Property

    Private _MainForm_CAccount_Sort As String
    ''' <summary>
    ''' MainWindow Account ContextMenu Sort
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CAccount_Sort() As String
        Get
            Return _MainForm_CAccount_Sort
        End Get
        Set(ByVal value As String)
            _MainForm_CAccount_Sort = value
        End Set
    End Property

    Private _MainForm_CAccount_Sort_AccountName As String
    ''' <summary>
    ''' MainWindow Account ContextMenu Sort AccountName
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CAccount_Sort_AccountName() As String
        Get
            Return _MainForm_CAccount_Sort_AccountName
        End Get
        Set(ByVal value As String)
            _MainForm_CAccount_Sort_AccountName = value
        End Set
    End Property

    Private _MainForm_CAccount_Sort_LoginStatus As String
    ''' <summary>
    ''' MainWindow Account ContextMenu Sort LoginStatus
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CAccount_Sort_LoginStatus() As String
        Get
            Return _MainForm_CAccount_Sort_LoginStatus
        End Get
        Set(ByVal value As String)
            _MainForm_CAccount_Sort_LoginStatus = value
        End Set
    End Property

    Private _MainForm_CAccount_Setting As String
    ''' <summary>
    ''' MainWindow Account ContextMenu Setting
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CAccount_Setting() As String
        Get
            Return _MainForm_CAccount_Setting
        End Get
        Set(ByVal value As String)
            _MainForm_CAccount_Setting = value
        End Set
    End Property
#End Region

#Region "MainWindow CTransport"
    Private _MainForm_CTransport_Open As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Open
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Open() As String
        Get
            Return _MainForm_CTransport_Open
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Open = value
        End Set
    End Property

    Private _MainForm_CTransport_PriorityUp As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu PriorityUp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_PriorityUp() As String
        Get
            Return _MainForm_CTransport_PriorityUp
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_PriorityUp = value
        End Set
    End Property

    Private _MainForm_CTransport_PriorityDown As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu PriorityDown
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_PriorityDown() As String
        Get
            Return _MainForm_CTransport_PriorityDown
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_PriorityDown = value
        End Set
    End Property

    Private _MainForm_CTransport_Pause As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Pause
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Pause() As String
        Get
            Return _MainForm_CTransport_Pause
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Pause = value
        End Set
    End Property

    Private _MainForm_CTransport_Restart As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Restart
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Restart() As String
        Get
            Return _MainForm_CTransport_Restart
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Restart = value
        End Set
    End Property

    Private _MainForm_CTransport_Stop As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Stop
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Stop() As String
        Get
            Return _MainForm_CTransport_Stop
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Stop = value
        End Set
    End Property

    Private _MainForm_CTransport_Delete As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Delete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Delete() As String
        Get
            Return _MainForm_CTransport_Delete
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Delete = value
        End Set
    End Property

    Private _MainForm_CTransport_Sort As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Sort
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Sort() As String
        Get
            Return _MainForm_CTransport_Sort
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Sort = value
        End Set
    End Property

    Private _MainForm_CTransport_Sort_Remote As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Sort Remote
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Sort_Remote() As String
        Get
            Return _MainForm_CTransport_Sort_Remote
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Sort_Remote = value
        End Set
    End Property

    Private _MainForm_CTransport_Sort_Priority As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Sort Priority
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Sort_Priority() As String
        Get
            Return _MainForm_CTransport_Sort_Priority
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Sort_Priority = value
        End Set
    End Property

    Private _MainForm_CTransport_Sort_FileName As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Sort FileName
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Sort_FileName() As String
        Get
            Return _MainForm_CTransport_Sort_FileName
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Sort_FileName = value
        End Set
    End Property

    Private _MainForm_CTransport_Sort_FullPath As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Sort FullPath
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Sort_FullPath() As String
        Get
            Return _MainForm_CTransport_Sort_FullPath
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Sort_FullPath = value
        End Set
    End Property

    Private _MainForm_CTransport_Sort_Size As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Sort Size
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Sort_Size() As String
        Get
            Return _MainForm_CTransport_Sort_Size
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Sort_Size = value
        End Set
    End Property

    Private _MainForm_CTransport_Sort_Status As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu Sort Status
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_Sort_Status() As String
        Get
            Return _MainForm_CTransport_Sort_Status
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_Sort_Status = value
        End Set
    End Property

    Private _MainForm_CTransport_ListMode As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu ListMode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_ListMode() As String
        Get
            Return _MainForm_CTransport_ListMode
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_ListMode = value
        End Set
    End Property

    Private _MainForm_CTransport_SearchBox As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu SearchBox
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_SearchBox() As String
        Get
            Return _MainForm_CTransport_SearchBox
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_SearchBox = value
        End Set
    End Property

    Private _MainForm_CTransport_AllSelect As String
    ''' <summary>
    ''' MainWindow Transport ContextMenu AllSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CTransport_AllSelect() As String
        Get
            Return _MainForm_CTransport_AllSelect
        End Get
        Set(ByVal value As String)
            _MainForm_CTransport_AllSelect = value
        End Set
    End Property
#End Region

#Region "MainWindow Transport"
    Private _MainForm_Transport_DownloadMenu As String
    ''' <summary>
    ''' MainWindow Transport DownloadMenu
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_DownloadMenu() As String
        Get
            Return _MainForm_Transport_DownloadMenu
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_DownloadMenu = value
        End Set
    End Property

    Private _MainForm_Transport_Download_Approval As String
    ''' <summary>
    ''' MainWindow Transport DownloadMenu Approvalo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Download_Approval() As String
        Get
            Return _MainForm_Transport_Download_Approval
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Download_Approval = value
        End Set
    End Property

    Private _MainForm_Transport_Download_AllApproval As String
    ''' <summary>
    ''' MainWindow Transport Download AllApproval
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Download_AllApproval() As String
        Get
            Return _MainForm_Transport_Download_AllApproval
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Download_AllApproval = value
        End Set
    End Property

    Private _MainForm_Transport_Download_Resume As String
    ''' <summary>
    ''' MainWindow Transport Download_Resume
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Download_Resume() As String
        Get
            Return _MainForm_Transport_Download_Resume
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Download_Resume = value
        End Set
    End Property

    Private _MainForm_Transport_UploadMenu As String
    ''' <summary>
    ''' MainWindow Transport UploadMenu
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_UploadMenu() As String
        Get
            Return _MainForm_Transport_UploadMenu
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_UploadMenu = value
        End Set
    End Property

    Private _MainForm_Transport_Upload_File As String
    ''' <summary>
    ''' MainWindow Transport Upload File
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Upload_File() As String
        Get
            Return _MainForm_Transport_Upload_File
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Upload_File = value
        End Set
    End Property

    Private _MainForm_Transport_Upload_Folder As String
    ''' <summary>
    ''' MainWindow Transport Upload Folder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Upload_Folder() As String
        Get
            Return _MainForm_Transport_Upload_Folder
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Upload_Folder = value
        End Set
    End Property

    Private _MainForm_Transport_Pause As String
    ''' <summary>
    ''' MainWindow Transport Pause
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Pause() As String
        Get
            Return _MainForm_Transport_Pause
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Pause = value
        End Set
    End Property

    Private _MainForm_Transport_Restart As String
    ''' <summary>
    ''' MainWindow Transport Restart
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Restart() As String
        Get
            Return _MainForm_Transport_Restart
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Restart = value
        End Set
    End Property

    Private _MainForm_Transport_Stop As String
    ''' <summary>
    ''' MainWindow Transport Stop
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Stop() As String
        Get
            Return _MainForm_Transport_Stop
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Stop = value
        End Set
    End Property

    Private _MainForm_Transport_Delete As String
    ''' <summary>
    ''' MainWindow Transport Delete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Delete() As String
        Get
            Return _MainForm_Transport_Delete
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Delete = value
        End Set
    End Property

    Private _MainForm_Transport_DoneDelete As String
    ''' <summary>
    ''' MainWindow Transport DoneDelete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_DoneDelete() As String
        Get
            Return _MainForm_Transport_DoneDelete
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_DoneDelete = value
        End Set
    End Property

    Private _MainForm_Transport_GoShareList As String
    ''' <summary>
    ''' MainWindow Transport GoShareList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_GoShareList() As String
        Get
            Return _MainForm_Transport_GoShareList
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_GoShareList = value
        End Set
    End Property

    Private _MainForm_Transport_Column_Remote As String
    ''' <summary>
    ''' MainWindow Transport Column Remote
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Column_Remote() As String
        Get
            Return _MainForm_Transport_Column_Remote
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Column_Remote = value
        End Set
    End Property

    Private _MainForm_Transport_Column_Priority As String
    ''' <summary>
    ''' MainWindow Transport Column Priority
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Column_Priority() As String
        Get
            Return _MainForm_Transport_Column_Priority
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Column_Priority = value
        End Set
    End Property

    Private _MainForm_Transport_Column_FileName As String
    ''' <summary>
    ''' MainWindow Transport Column FileName
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Column_FileName() As String
        Get
            Return _MainForm_Transport_Column_FileName
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Column_FileName = value
        End Set
    End Property

    Private _MainForm_Transport_Column_FullPath As String
    ''' <summary>
    ''' MainWindow Transport Column FullPath
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Column_FullPath() As String
        Get
            Return _MainForm_Transport_Column_FullPath
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Column_FullPath = value
        End Set
    End Property

    Private _MainForm_Transport_Column_Size As String
    ''' <summary>
    ''' MainWindow Transport Column Size
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Column_Size() As String
        Get
            Return _MainForm_Transport_Column_Size
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Column_Size = value
        End Set
    End Property

    Private _MainForm_Transport_Column_Status As String
    ''' <summary>
    ''' MainWindow Transport Column Status
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Transport_Column_Status() As String
        Get
            Return _MainForm_Transport_Column_status
        End Get
        Set(ByVal value As String)
            _MainForm_Transport_Column_status = value
        End Set
    End Property
#End Region

#Region "MainWindow CShare"
    Private _MainForm_CShare_Download As String
    ''' <summary>
    ''' MainWindow Share ContextMenu Download
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_Download() As String
        Get
            Return _MainForm_CShare_Download
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_Download = value
        End Set
    End Property

    Private _MainForm_CShare_Up As String
    ''' <summary>
    ''' MainWindow Share ContextMenu Up
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_Up() As String
        Get
            Return _MainForm_CShare_Up
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_Up = value
        End Set
    End Property

    Private _MainForm_CShare_Refresh As String
    ''' <summary>
    ''' MainWindow Share ContextMenu Refresh
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_Refresh() As String
        Get
            Return _MainForm_CShare_Refresh
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_Refresh = value
        End Set
    End Property

    Private _MainForm_CShare_Root As String
    ''' <summary>
    ''' MainWindow Share ContextMenu Root
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_Root() As String
        Get
            Return _MainForm_CShare_Root
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_Root = value
        End Set
    End Property

    Private _MainForm_CShare_Sort As String
    ''' <summary>
    ''' MainWindow Share ContextMenu Sort
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_Sort() As String
        Get
            Return _MainForm_CShare_Sort
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_Sort = value
        End Set
    End Property

    Private _MainForm_CShare_Sort_FileName As String
    ''' <summary>
    ''' MainWindow Share ContextMenu Sort FileName
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_Sort_FileName() As String
        Get
            Return _MainForm_CShare_Sort_FileName
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_Sort_FileName = value
        End Set
    End Property

    Private _MainForm_CShare_Sort_Extension As String
    ''' <summary>
    ''' MainWindow Share ContextMenu Sort Extesnion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_Sort_Extension() As String
        Get
            Return _MainForm_CShare_Sort_Extension
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_Sort_Extension = value
        End Set
    End Property

    Private _MainForm_CShare_Sort_Size As String
    ''' <summary>
    ''' MainWindow Share ContextMenu Sort Size
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_Sort_Size() As String
        Get
            Return _MainForm_CShare_Sort_Size
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_Sort_Size = value
        End Set
    End Property

    Private _MainForm_CShare_Sort_DateModified As String
    ''' <summary>
    ''' MainWindow Share ContextMenu Sort DateModified
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_Sort_DateModified() As String
        Get
            Return _MainForm_CShare_Sort_DateModified
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_Sort_DateModified = value
        End Set
    End Property

    Private _MainForm_CShare_SearchBox As String
    ''' <summary>
    ''' MainWindow Share ContextMenu SearchBox
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_SearchBox() As String
        Get
            Return _MainForm_CShare_SearchBox
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_SearchBox = value
        End Set
    End Property

    Private _MainForm_CShare_AllSelect As String
    ''' <summary>
    ''' MainWindow Share ContextMenu AllSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_CShare_AllSelect() As String
        Get
            Return _MainForm_CShare_AllSelect
        End Get
        Set(ByVal value As String)
            _MainForm_CShare_AllSelect = value
        End Set
    End Property
#End Region

#Region "MainWindow Share"
    Private _MainForm_Share_Download As String
    ''' <summary>
    ''' MainWindow Share Download
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Share_Download() As String
        Get
            Return _MainForm_Share_Download
        End Get
        Set(ByVal value As String)
            _MainForm_Share_Download = value
        End Set
    End Property

    Private _MainForm_Share_Up As String
    ''' <summary>
    ''' MainWindow Share Up
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Share_Up() As String
        Get
            Return _MainForm_Share_Up
        End Get
        Set(ByVal value As String)
            _MainForm_Share_Up = value
        End Set
    End Property

    Private _MainForm_Share_Refresh As String
    ''' <summary>
    ''' MainWindow Share Refresh
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Share_Refresh() As String
        Get
            Return _MainForm_Share_Refresh
        End Get
        Set(ByVal value As String)
            _MainForm_Share_Refresh = value
        End Set
    End Property

    Private _MainForm_Share_Root As String
    ''' <summary>
    ''' MainWindow Share Root
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Share_Root() As String
        Get
            Return _MainForm_Share_Root
        End Get
        Set(ByVal value As String)
            _MainForm_Share_Root = value
        End Set
    End Property

    Private _MainForm_Share_GoTransferList As String
    ''' <summary>
    ''' MainWindow Share GoTransportList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Share_GoTransportList() As String
        Get
            Return _MainForm_Share_GoTransferList
        End Get
        Set(ByVal value As String)
            _MainForm_Share_GoTransferList = value
        End Set
    End Property

    Private _MainForm_Share_Column_FileName As String
    ''' <summary>
    ''' MainWindow Share Column FileName
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Share_Column_FileName() As String
        Get
            Return _MainForm_Share_Column_FileName
        End Get
        Set(ByVal value As String)
            _MainForm_Share_Column_FileName = value
        End Set
    End Property

    Private _MainForm_Share_Column_Extension As String
    ''' <summary>
    ''' MainWindow Share Column Extension
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Share_Column_Extension() As String
        Get
            Return _MainForm_Share_Column_Extension
        End Get
        Set(ByVal value As String)
            _MainForm_Share_Column_Extension = value
        End Set
    End Property

    Private _MainForm_Share_Column_Size As String
    ''' <summary>
    ''' MainWindow Share Column Size
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Share_Column_Size() As String
        Get
            Return _MainForm_Share_Column_Size
        End Get
        Set(ByVal value As String)
            _MainForm_Share_Column_Size = value
        End Set
    End Property

    Private _MainForm_Share_Column_DateModified As String
    ''' <summary>
    ''' MainWindow Share Column DateModified
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Share_Column_DateModified() As String
        Get
            Return _MainForm_Share_Column_DateModified
        End Get
        Set(ByVal value As String)
            _MainForm_Share_Column_DateModified = value
        End Set
    End Property
#End Region

#Region "MainWindow"
    Private _MainForm_SearchBox_SearchButton As String
    ''' <summary>
    ''' MainWindow SearchBox SearchButton
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_SearchBox_SearchButton() As String
        Get
            Return _MainForm_SearchBox_SearchButton
        End Get
        Set(ByVal value As String)
            _MainForm_SearchBox_SearchButton = value
        End Set
    End Property

    Private _MainForm_Infomation_ConnectStartTime As String
    ''' <summary>
    ''' MainWindow Infomation ConnectStartTime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_ConnectStartTime() As String
        Get
            Return _MainForm_Infomation_ConnectStartTime
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_ConnectStartTime = value
        End Set
    End Property

    Private _MainForm_Infomation_ConnectTime As String
    ''' <summary>
    ''' MainWindow Infomation ConnectTime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_ConnectTime() As String
        Get
            Return _MainForm_Infomation_ConnectTime
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_ConnectTime = value
        End Set
    End Property

    Private _MainForm_Infomation_TotalDownloaded As String
    ''' <summary>
    ''' MainWindow Infomation SumReceiveSize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_SumReceiveSize() As String
        Get
            Return _MainForm_Infomation_TotalDownloaded
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_TotalDownloaded = value
        End Set
    End Property

    Private _MainForm_Infomation_TotalUploaded As String
    ''' <summary>
    ''' MainWindow Infomation SumSendSize
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_SumSendSize() As String
        Get
            Return _MainForm_Infomation_TotalUploaded
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_TotalUploaded = value
        End Set
    End Property

    Private _MainForm_Infomation_UploadedSpeed As String
    ''' <summary>
    ''' MainWindow Infomation SendSpeed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_SendSpeed() As String
        Get
            Return _MainForm_Infomation_UploadedSpeed
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_UploadedSpeed = value
        End Set
    End Property

    Private _MainForm_Infomation_DownloadedSpeed As String
    ''' <summary>
    ''' MainWindow Infomation ReceiveSpeed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_ReceiveSpeed() As String
        Get
            Return _MainForm_Infomation_DownloadedSpeed
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_DownloadedSpeed = value
        End Set
    End Property

    Private _MainForm_Infomation_YourVersion As String
    ''' <summary>
    ''' MainWindow Infomation YourVersion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_YourVersion() As String
        Get
            Return _MainForm_Infomation_YourVersion
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_YourVersion = value
        End Set
    End Property

    Private _MainForm_Infomation_YourProtocolVersion As String
    ''' <summary>
    ''' MainWindow Infomation YourProtocolVersion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_YourProtocolVersion() As String
        Get
            Return _MainForm_Infomation_YourProtocolVersion
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_YourProtocolVersion = value
        End Set
    End Property

    Private _MainForm_Infomation_MyVersion As String
    ''' <summary>
    ''' MainWindow Infomation MyVersion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_MyVersion() As String
        Get
            Return _MainForm_Infomation_MyVersion
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_MyVersion = value
        End Set
    End Property

    Private _MainForm_Infomation_MyProtocolVersion As String
    ''' <summary>
    ''' MainWindow Infomation MyProtocolVersion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_MyProtocolVersion() As String
        Get
            Return _MainForm_Infomation_MyProtocolVersion
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_MyProtocolVersion = value
        End Set
    End Property

    Private _MainForm_Infomation_YourIPAddress As String
    ''' <summary>
    ''' MainWindow Infomation YourIPAddress
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_YourIPAddress() As String
        Get
            Return _MainForm_Infomation_YourIPAddress
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_YourIPAddress = value
        End Set
    End Property

    Private _MainForm_Infomation_YourPort As String
    ''' <summary>
    ''' MainWindow Infomation YourPort
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_YourPort() As String
        Get
            Return _MainForm_Infomation_YourPort
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_YourPort = value
        End Set
    End Property

    Private _MainForm_Infomation_RecEncryptDataNum As String
    ''' <summary>
    ''' MainWindow Infomation RecEncryptDataNum
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_RecEncryptDataNum() As String
        Get
            Return _MainForm_Infomation_RecEncryptDataNum
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_RecEncryptDataNum = value
        End Set
    End Property

    Private _MainForm_Infomation_SendEncryptDataNum As String
    ''' <summary>
    ''' MainWindow Infomation SendEncryptDataNum
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Infomation_SendEncryptDataNum() As String
        Get
            Return _MainForm_Infomation_SendEncryptDataNum
        End Get
        Set(ByVal value As String)
            _MainForm_Infomation_SendEncryptDataNum = value
        End Set
    End Property

    Private _MainForm_PortListenErrorMSG As String
    ''' <summary>
    ''' MainWindow PortListenErrorMSG
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_PortListenErrorMSG() As String
        Get
            Return _MainForm_PortListenErrorMSG
        End Get
        Set(ByVal value As String)
            _MainForm_PortListenErrorMSG = value
        End Set
    End Property

    Private _MainForm_Drawing_Transport_NotApproval As String
    ''' <summary>
    ''' MainWindow Drawing Transport NotApproval
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Drawing_Transport_NotApproval() As String
        Get
            Return _MainForm_Drawing_Transport_NotApproval
        End Get
        Set(ByVal value As String)
            _MainForm_Drawing_Transport_NotApproval = value
        End Set
    End Property

    Private _MainForm_Drawing_Transport_ErrorDone As String
    ''' <summary>
    ''' MainWindow Drawing Transport ErrorDone
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Drawing_Transport_ErrorDone() As String
        Get
            Return _MainForm_Drawing_Transport_ErrorDone
        End Get
        Set(ByVal value As String)
            _MainForm_Drawing_Transport_ErrorDone = value
        End Set
    End Property

    Private _MainForm_Drawing_Transport_Done As String
    ''' <summary>
    ''' MainWindow Drawing Transport Done
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Drawing_Transport_Done() As String
        Get
            Return _MainForm_Drawing_Transport_Done
        End Get
        Set(ByVal value As String)
            _MainForm_Drawing_Transport_Done = value
        End Set
    End Property

    Private _MainForm_Drawing_Transport_DownloadStop As String
    ''' <summary>
    ''' MainWindow Drawing Transport DownloadStop
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Drawing_Transport_DownloadStop() As String
        Get
            Return _MainForm_Drawing_Transport_DownloadStop
        End Get
        Set(ByVal value As String)
            _MainForm_Drawing_Transport_DownloadStop = value
        End Set
    End Property

    Private _MainForm_Drawing_Transport_UploadStop As String
    ''' <summary>
    ''' MainWindow Drawing Transport UploadStop
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Drawing_Transport_UploadStop() As String
        Get
            Return _MainForm_Drawing_Transport_UploadStop
        End Get
        Set(ByVal value As String)
            _MainForm_Drawing_Transport_UploadStop = value
        End Set
    End Property

    Private _MainForm_Drawing_Transport_QueueStop As String
    ''' <summary>
    ''' MainWindow Drawing Transport QueueStop
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Drawing_Transport_QueueStop() As String
        Get
            Return _MainForm_Drawing_Transport_QueueStop
        End Get
        Set(ByVal value As String)
            _MainForm_Drawing_Transport_QueueStop = value
        End Set
    End Property

    Private _MainForm_Drawing_Transport_Folder As String
    ''' <summary>
    ''' MainWindow Draing Transport Folder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Drawing_Transport_Folder() As String
        Get
            Return _MainForm_Drawing_Transport_Folder
        End Get
        Set(ByVal value As String)
            _MainForm_Drawing_Transport_Folder = value
        End Set
    End Property
#End Region

#Region "MainWindow Dialog"
    Private _MainForm_DialogTitle_AllApproval As String
    ''' <summary>
    ''' MainWindow DialogTitle AllApproval
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_DialogTitle_AllApproval() As String
        Get
            Return _MainForm_DialogTitle_AllApproval
        End Get
        Set(ByVal value As String)
            _MainForm_DialogTitle_AllApproval = value
        End Set
    End Property

    Private _MainForm_DialogTitle_UploadFile As String
    ''' <summary>
    ''' MainWindow DialogTitle UploadFile
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_DialogTitle_UploadFile() As String
        Get
            Return _MainForm_DialogTitle_UploadFile
        End Get
        Set(ByVal value As String)
            _MainForm_DialogTitle_UploadFile = value
        End Set
    End Property

    Private _MainForm_DialogTitle_UploadFolder As String
    ''' <summary>
    ''' MainWindow DialogTitle UploadFolder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_DialogTitle_UploadFolder() As String
        Get
            Return _MainForm_DialogTitle_UploadFolder
        End Get
        Set(ByVal value As String)
            _MainForm_DialogTitle_UploadFolder = value
        End Set
    End Property

    Private _MainForm_Dialog_UploadFileToAllUsers As String
    ''' <summary>
    ''' MainWindow DialogMSG UploadFileToAllUsers
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Dialog_UploadFileToAllUsers() As String
        Get
            Return _MainForm_Dialog_UploadFileToAllUsers
        End Get
        Set(ByVal value As String)
            _MainForm_Dialog_UploadFileToAllUsers = value
        End Set
    End Property

    Private _MainForm_Dialog_Stop As String
    ''' <summary>
    ''' MainWindow DialogMSG Stop
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Dialog_Stop() As String
        Get
            Return _MainForm_Dialog_Stop
        End Get
        Set(ByVal value As String)
            _MainForm_Dialog_Stop = value
        End Set
    End Property

    Private _MainForm_Dialog_Delete As String
    ''' <summary>
    ''' MainWindow DialogMSG Delete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Dialog_Delete() As String
        Get
            Return _MainForm_Dialog_Delete
        End Get
        Set(ByVal value As String)
            _MainForm_Dialog_Delete = value
        End Set
    End Property

    Private _MainForm_Dialog_Quit As String
    ''' <summary>
    ''' MainWindow Dialog Quit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainForm_Dialog_Quit() As String
        Get
            Return _MainForm_Dialog_Quit
        End Get
        Set(ByVal value As String)
            _MainForm_Dialog_Quit = value
        End Set
    End Property
#End Region

#Region "SettingWindow"
    Private _SettingForm_Title As String
    ''' <summary>
    ''' SettingWindow Title
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Title() As String
        Get
            Return _SettingForm_Title
        End Get
        Set(ByVal value As String)
            _SettingForm_Title = value
        End Set
    End Property

    Private _SettingForm_Version As String
    ''' <summary>
    ''' SettingWindow Version
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Version() As String
        Get
            Return _SettingForm_Version
        End Get
        Set(ByVal value As String)
            _SettingForm_Version = value
        End Set
    End Property

    Private _SettingForm_Version_SoftwareName As String
    ''' <summary>
    ''' SettingWindow Version SoftwareName
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Version_SoftwareName() As String
        Get
            Return _SettingForm_Version_SoftwareName
        End Get
        Set(ByVal value As String)
            _SettingForm_Version_SoftwareName = value
        End Set
    End Property

    Private _SettingForm_Version_Version As String
    ''' <summary>
    ''' SettingWindow Version Version
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Version_Version() As String
        Get
            Return _SettingForm_Version_Version
        End Get
        Set(ByVal value As String)
            _SettingForm_Version_Version = value
        End Set
    End Property

    Private _SettingForm_Version_ProtocolVersion As String
    ''' <summary>
    ''' SettingWindow Version ProtocolVersion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Version_ProtocolVersion() As String
        Get
            Return _SettingForm_Version_ProtocolVersion
        End Get
        Set(ByVal value As String)
            _SettingForm_Version_ProtocolVersion = value
        End Set
    End Property

    Private _SettingForm_Version_Developer As String
    ''' <summary>
    ''' SettingWindow Version Developer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Version_Developer() As String
        Get
            Return _SettingForm_Version_Developer
        End Get
        Set(ByVal value As String)
            _SettingForm_Version_Developer = value
        End Set
    End Property

    Private _SettingForm_Version_ToolbarIcon As String
    ''' <summary>
    ''' SettingWindow Version ToolbarIcon
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Version_ToolbarIcon() As String
        Get
            Return _SettingForm_Version_ToolbarIcon
        End Get
        Set(ByVal value As String)
            _SettingForm_Version_ToolbarIcon = value
        End Set
    End Property

    Private _SettingForm_Version_ExeIcon As String
    ''' <summary>
    ''' SettingWindow Version ExeIcon
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Version_ExeIcon() As String
        Get
            Return _SettingForm_Version_ExeIcon
        End Get
        Set(ByVal value As String)
            _SettingForm_Version_ExeIcon = value
        End Set
    End Property

    Private _SettingForm_General As String
    ''' <summary>
    ''' SettingWindow General
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_General() As String
        Get
            Return _SettingForm_General
        End Get
        Set(ByVal value As String)
            _SettingForm_General = value
        End Set
    End Property

    Private _SettingForm_General_AlwaysOnTop As String
    ''' <summary>
    ''' SettingWinwow General AlwaysOnTop
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_General_AlwaysOnTop() As String
        Get
            Return _SettingForm_General_AlwaysOnTop
        End Get
        Set(ByVal value As String)
            _SettingForm_General_AlwaysOnTop = value
        End Set
    End Property

    Private _SettingForm_General_HideWhenMinimization As String
    ''' <summary>
    ''' SettingWindow General HideWhenMinimization
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_General_HideWhenMinimization() As String
        Get
            Return _SettingForm_General_HideWhenMinimization
        End Get
        Set(ByVal value As String)
            _SettingForm_General_HideWhenMinimization = value
        End Set
    End Property

    Private _SettingForm_General_Port As String
    ''' <summary>
    ''' SettingWindow General Port
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_General_Port() As String
        Get
            Return _SettingForm_General_Port
        End Get
        Set(ByVal value As String)
            _SettingForm_General_Port = value
        End Set
    End Property

    Private _SettingForm_General_AllowMultipleStarts As String
    ''' <summary>
    ''' SettingWindow General AllowMultipleStarts
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_General_AllowMultipleStarts() As String
        Get
            Return _SettingForm_General_AllowMultipleStarts
        End Get
        Set(ByVal value As String)
            _SettingForm_General_AllowMultipleStarts = value
        End Set
    End Property

    Private _SettingForm_MyAccount As String
    ''' <summary>
    ''' SettingWindow MyAccount
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_MyAccount() As String
        Get
            Return _SettingForm_MyAccount
        End Get
        Set(ByVal value As String)
            _SettingForm_MyAccount = value
        End Set
    End Property

    Private _SettingForm_MyAccount_AccountName As String
    ''' <summary>
    ''' SettingWindow MyAccount_AccountName
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_MyAccount_AccountName() As String
        Get
            Return _SettingForm_MyAccount_AccountName
        End Get
        Set(ByVal value As String)
            _SettingForm_MyAccount_AccountName = value
        End Set
    End Property

    Private _SettingForm_MyAccount_Comment As String
    ''' <summary>
    ''' SettingWindow MyAccount Comment
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_MyAccount_Comment() As String
        Get
            Return _SettingForm_MyAccount_Comment
        End Get
        Set(ByVal value As String)
            _SettingForm_MyAccount_Comment = value
        End Set
    End Property

    Private _SettingForm_Notify As String
    ''' <summary>
    ''' SettingWindow Notify
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Notify() As String
        Get
            Return _SettingForm_Notify
        End Get
        Set(ByVal value As String)
            _SettingForm_Notify = value
        End Set
    End Property

    Private _SettingForm_Notify_WhenConnect As String
    ''' <summary>
    ''' SettingWindow Notify WhenConnect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Notify_WhenConnect() As String
        Get
            Return _SettingForm_Notify_WhenConnect
        End Get
        Set(ByVal value As String)
            _SettingForm_Notify_WhenConnect = value
        End Set
    End Property

    Private _SettingForm_Notify_WhenReceiveMessage As String
    ''' <summary>
    ''' SettingWindow Notify WhenReceiveMessage
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Notify_WhenReceiveMessage() As String
        Get
            Return _SettingForm_Notify_WhenReceiveMessage
        End Get
        Set(ByVal value As String)
            _SettingForm_Notify_WhenReceiveMessage = value
        End Set
    End Property

    Private _SettingForm_Notify_WhenDoneUpload As String
    ''' <summary>
    ''' SettingWindow Notify WhenDoneUpload
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Notify_WhenDoneUpload() As String
        Get
            Return _SettingForm_Notify_WhenDoneUpload
        End Get
        Set(ByVal value As String)
            _SettingForm_Notify_WhenDoneUpload = value
        End Set
    End Property

    Private _SettingForm_Notify_WhenDoneDownload As String
    ''' <summary>
    ''' SettingWindow Notify WhenDoneDownload
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Notify_WhenDoneDownload() As String
        Get
            Return _SettingForm_Notify_WhenDoneDownload
        End Get
        Set(ByVal value As String)
            _SettingForm_Notify_WhenDoneDownload = value
        End Set
    End Property

    Private _SettingForm_Notify_WhenUpload As String
    ''' <summary>
    ''' SettingWindow Notify WhenUpload
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Notify_WhenUpload() As String
        Get
            Return _SettingForm_Notify_WhenUpload
        End Get
        Set(ByVal value As String)
            _SettingForm_Notify_WhenUpload = value
        End Set
    End Property

    Private _SettingForm_Cancel As String
    ''' <summary>
    ''' SettingWindow Cancel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Cancel() As String
        Get
            Return _SettingForm_Cancel
        End Get
        Set(ByVal value As String)
            _SettingForm_Cancel = value
        End Set
    End Property

    Private _SettingForm_Ok As String
    ''' <summary>
    ''' SettingWindow Ok
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingForm_Ok() As String
        Get
            Return _SettingForm_Ok
        End Get
        Set(ByVal value As String)
            _SettingForm_Ok = value
        End Set
    End Property
#End Region

#Region "ChatWindow"
    Private _ChatForm_CView_Copy As String
    ''' <summary>
    ''' ChatWindow ChatView ContextMenu Copy
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChatForm_CView_Copy() As String
        Get
            Return _ChatForm_CView_Copy
        End Get
        Set(ByVal value As String)
            _ChatForm_CView_Copy = value
        End Set
    End Property

    Private _ChatForm_CView_AllSelect As String
    ''' <summary>
    ''' ChatWindow ChatView ContextMenu AllSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChatForm_CView_AllSelect() As String
        Get
            Return _ChatForm_CView_AllSelect
        End Get
        Set(ByVal value As String)
            _ChatForm_CView_AllSelect = value
        End Set
    End Property

    Private _ChatForm_CView_AllDelete As String
    ''' <summary>
    ''' ChatWindow ChatView ContextMenu AllDelete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChatForm_CView_AllDelete() As String
        Get
            Return _ChatForm_CView_AllDelete
        End Get
        Set(ByVal value As String)
            _ChatForm_CView_AllDelete = value
        End Set
    End Property

    Private _ChatForm_CView_AlwaysOnTop As String
    ''' <summary>
    ''' ChatWindow ChatView ContextMenu Always On Top
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChatForm_CView_AlwaysOnTop() As String
        Get
            Return _ChatForm_CView_AlwaysOnTop
        End Get
        Set(ByVal value As String)
            _ChatForm_CView_AlwaysOnTop = value
        End Set
    End Property

    Private _ChatForm_CView_ShowInfomation As String
    ''' <summary>
    ''' ChatWindow ChatView ContextMenu ShowInfomation
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChatForm_CView_ShowInfomation() As String
        Get
            Return _ChatForm_CView_ShowInfomation
        End Get
        Set(ByVal value As String)
            _ChatForm_CView_ShowInfomation = value
        End Set
    End Property

    Private _ChatForm_CView_Search As String
    ''' <summary>
    ''' ChatWindow ChatView ContextMenu Search
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChatForm_CView_Search() As String
        Get
            Return _ChatForm_CView_Search
        End Get
        Set(ByVal value As String)
            _ChatForm_CView_Search = value
        End Set
    End Property

    Private _ChatForm_SearchButton As String
    ''' <summary>
    ''' ChatWindow ChatView SearchButton
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChatForm_SearchButton() As String
        Get
            Return _ChatForm_SearchButton
        End Get
        Set(ByVal value As String)
            _ChatForm_SearchButton = value
        End Set
    End Property
#End Region

#Region "AccountWindow"
    Private _AccountForm_DestAccount As String
    ''' <summary>
    ''' AccountWindow DestAccount
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_DestAccount() As String
        Get
            Return _AccountForm_DestAccount
        End Get
        Set(ByVal value As String)
            _AccountForm_DestAccount = value
        End Set
    End Property

    Private _AccountForm_DestAccount_AccountName As String
    ''' <summary>
    ''' AccountWindow DestAccount AccountName
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_DestAccount_AccountName() As String
        Get
            Return _AccountForm_DestAccount_AccountName
        End Get
        Set(ByVal value As String)
            _AccountForm_DestAccount_AccountName = value
        End Set
    End Property

    Private _AccountForm_DestAccount_IPAddress As String
    ''' <summary>
    ''' AccountWindow DestAccount IPAddress
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_DestAccount_IPAddress() As String
        Get
            Return _AccountForm_DestAccount_IPAddress
        End Get
        Set(ByVal value As String)
            _AccountForm_DestAccount_IPAddress = value
        End Set
    End Property

    Private _AccountForm_DestAccount_Port As String
    ''' <summary>
    ''' AccountWindow DestAccount_Port
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_DestAccount_Port() As String
        Get
            Return _AccountForm_DestAccount_Port
        End Get
        Set(ByVal value As String)
            _AccountForm_DestAccount_Port = value
        End Set
    End Property

    Private _AccountForm_DestAccount_LoginKey As String
    ''' <summary>
    ''' AccountWindow DestAccount LoginKey
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_DestAccount_LoginKey() As String
        Get
            Return _AccountForm_DestAccount_LoginKey
        End Get
        Set(ByVal value As String)
            _AccountForm_DestAccount_LoginKey = value
        End Set
    End Property

    Private _AccountForm_DestAccount_Encrypt As String
    ''' <summary>
    ''' AccountWindow DestAccount Encrypt
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_DestAccount_Encrypt() As String
        Get
            Return _AccountForm_DestAccount_Encrypt
        End Get
        Set(ByVal value As String)
            _AccountForm_DestAccount_Encrypt = value
        End Set
    End Property

    Private _AccountForm_ApprovalDownload As String
    ''' <summary>
    ''' AccountWindow ApprovalDownload
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_ApprovalDownload() As String
        Get
            Return _AccountForm_ApprovalDownload
        End Get
        Set(ByVal value As String)
            _AccountForm_ApprovalDownload = value
        End Set
    End Property

    Private _AccountForm_ApprovalDownload_SavePath As String
    ''' <summary>
    ''' AccountWindow ApprovalDownload SavePath 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_ApprovalDownload_SavePath() As String
        Get
            Return _AccountForm_ApprovalDownload_SavePath
        End Get
        Set(ByVal value As String)
            _AccountForm_ApprovalDownload_SavePath = value
        End Set
    End Property

    Private _AccountForm_ApprovalDownload_SelectPath As String
    ''' <summary>
    ''' AccountWindow ApprovalDownload SelectPath
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_ApprovalDownload_SelectPath() As String
        Get
            Return _AccountForm_APprovalDownload_SelectPath
        End Get
        Set(ByVal value As String)
            _AccountForm_ApprovalDownload_SelectPath = value
        End Set
    End Property

    Private _AccountForm_EnableShare As String
    ''' <summary>
    ''' AccountWindow EnableShare
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_EnableShare() As String
        Get
            Return _AccountForm_EnableShare
        End Get
        Set(ByVal value As String)
            _AccountForm_EnableShare = value
        End Set
    End Property

    Private _AccountForm_CShare_AddFolder As String
    ''' <summary>
    ''' AccountWindow Share ContextMenu AddFolder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_CShare_AddFolder() As String
        Get
            Return _AccountForm_CShare_AddFolder
        End Get
        Set(ByVal value As String)
            _AccountForm_CShare_AddFolder = value
        End Set
    End Property

    Private _AccountForm_CShare_DeleteFromList As String
    ''' <summary>
    ''' AccountWindow Share ContextMenu DeleteFromList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_CShare_DeleteFromList() As String
        Get
            Return _AccountForm_CShare_DeleteFromList
        End Get
        Set(ByVal value As String)
            _AccountForm_CShare_DeleteFromList = value
        End Set
    End Property

    Private _AccountForm_CShare_DeleteUnExistFolder As String
    ''' <summary>
    ''' AccountWindow Share ContextMenu DeleteUnExistFolder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_CShare_DeleteUnExistFolder() As String
        Get
            Return _AccountForm_CShare_DeleteUnExistFolder
        End Get
        Set(ByVal value As String)
            _AccountForm_CShare_DeleteUnExistFolder = value
        End Set
    End Property

    Private _AccountForm_DialogTitle_SelectFolder As String
    ''' <summary>
    ''' AccountWindow DialogTitle SelectFolder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_DialogTitle_SelectFolder() As String
        Get
            Return _AccountForm_DialogTitle_SelectFolder
        End Get
        Set(ByVal value As String)
            _AccountForm_DialogTitle_SelectFolder = value
        End Set
    End Property

    Private _AccountForm_DialogMSG_AlreadyExist As String
    ''' <summary>
    ''' AccountWindow DialogMSG AlreadyExist
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_DialogMSG_AlreadyExist() As String
        Get
            Return _AccountForm_DialogMSG_AlreadyExist
        End Get
        Set(ByVal value As String)
            _AccountForm_DialogMSG_AlreadyExist = value
        End Set
    End Property

    Private _AccountForm_DialogMSG_AccountNameTooShort As String
    ''' <summary>
    ''' AccountWindow DialogMSG AccountNameTooShort
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_DialogMSG_AccountNameTooShort() As String
        Get
            Return _AccountForm_DialogMSG_AccountNameTooShort
        End Get
        Set(ByVal value As String)
            _AccountForm_DialogMSG_AccountNameTooShort = value
        End Set
    End Property

    Private _AccountForm_FormTitle As String
    ''' <summary>
    ''' AccountWindow FormTitle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_FormTitle() As String
        Get
            Return _AccountForm_FormTitle
        End Get
        Set(ByVal value As String)
            _AccountForm_FormTitle = value
        End Set
    End Property

    Private _AccountForm_OKButton As String
    ''' <summary>
    ''' AccountWindow OKButton
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_OKButton() As String
        Get
            Return _AccountForm_OKButton
        End Get
        Set(ByVal value As String)
            _AccountForm_OKButton = value
        End Set
    End Property

    Private _AccountForm_CancelButton As String
    ''' <summary>
    ''' AccountWindow CancelButton
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountForm_CancelButton() As String
        Get
            Return _AccountForm_CancelButton
        End Get
        Set(ByVal value As String)
            _AccountForm_CancelButton = value
        End Set
    End Property
#End Region

#Region "AccountClass"
    Private _AccountClass_NotifyMSG_Chat As String
    ''' <summary>
    ''' AccountClass NotifyMSG Chat
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_NotifyMSG_Chat(ByVal s As String) As String
        Get
            Return _AccountClass_NotifyMSG_Chat.Replace("%s%", s)
        End Get
        Set(ByVal value As String)
            _AccountClass_NotifyMSG_Chat = value
        End Set
    End Property

    Private _AccountClass_NotifyMSG_Upload As String
    ''' <summary>
    ''' AccountClass NotifyMSG Upload
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_NotifyMSG_Upload(ByVal s As String) As String
        Get
            Return _AccountClass_NotifyMSG_Upload.Replace("%s%", s)
        End Get
        Set(ByVal value As String)
            _AccountClass_NotifyMSG_Upload = value
        End Set
    End Property

    Private _AccountClass_NotifyMSG_Connect As String
    ''' <summary>
    ''' AccountClass NotifyMSG Connect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_NotifyMSG_Connect(ByVal s As String) As String
        Get
            Return _AccountClass_NotifyMSG_Connect.Replace("%s%", s)
        End Get
        Set(ByVal value As String)
            _AccountClass_NotifyMSG_Connect = value
        End Set
    End Property

    Private _AccountClass_NotifyTitle_Connect As String
    ''' <summary>
    ''' AccountClass NotifyTitle Connect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_NotifyTitle_Connect() As String
        Get
            Return _AccountClass_NotifyTitle_Connect
        End Get
        Set(ByVal value As String)
            _AccountClass_NotifyTitle_Connect = value
        End Set
    End Property

    Private _AccountClass_DialogMSG_MaxNumUpload As String
    ''' <summary>
    ''' AccountClass DialogMSG MaxNumUpload
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_DialogMSG_MaxNumUpload() As String
        Get
            Return _AccountClass_DialogMSG_MaxNumUpload
        End Get
        Set(ByVal value As String)
            _AccountClass_DialogMSG_MaxNumUpload = value
        End Set
    End Property

    Private _AccountClass_DialogTitle_UploadApprovalSavePath As String
    ''' <summary>
    ''' AccountClass DialogTitle UploadApprovalSavePath
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_DialogTitle_UploadApprovalSavePath(ByVal s As String) As String
        Get
            Return _AccountClass_DialogTitle_UploadApprovalSavePath.Replace("%s%", s)
        End Get
        Set(ByVal value As String)
            _AccountClass_DialogTitle_UploadApprovalSavePath = value
        End Set
    End Property

    Private _AccountClass_DialogTitle_ResumeFile As String
    ''' <summary>
    ''' AccountClass DialogTitle ResumeFile
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_DialogTitle_ResumeFile(ByVal s As String) As String
        Get
            Return _AccountClass_DialogTitle_ResumeFile.Replace("%s%", s)
        End Get
        Set(ByVal value As String)
            _AccountClass_DialogTitle_ResumeFile = value
        End Set
    End Property

    Private _AccountClass_DialogTitle_ResumeFolder As String
    ''' <summary>
    ''' AccountClass DialogTitle ResumeFolder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_DialogTitle_ResumeFolder(ByVal s As String) As String
        Get
            Return _AccountClass_DialogTitle_ResumeFolder.Replace("%s%", s)
        End Get
        Set(ByVal value As String)
            _AccountClass_DialogTitle_ResumeFolder = value
        End Set
    End Property

    Private _AccountClass_DialogMSG_MustNotResume As String
    ''' <summary>
    ''' AccountClass DialogMSG MustNotResume
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_DialogMSG_MustNotResume() As String
        Get
            Return _AccountClass_DialogMSG_MustNotResume
        End Get
        Set(ByVal value As String)
            _AccountClass_DialogMSG_MustNotResume = value
        End Set
    End Property

    Private _AccountClass_DialogTitle_SelectResumeFolder As String
    ''' <summary>
    ''' AccountClass_DialogTitle_SelectResumeFolder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_DialogTitle_SelectResumeFolder(ByVal s As String) As String
        Get
            Return _AccountClass_DialogTitle_SelectResumeFolder.Replace("%s%", s)
        End Get
        Set(ByVal value As String)
            _AccountClass_DialogTitle_SelectResumeFolder = value
        End Set
    End Property

    Private _AccountClass_Chat_AppendMSG As String
    ''' <summary>
    ''' AccountClass Chat AppendMSG
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountClass_Chat_AppendMSG() As String
        Get
            Return _AccountClass_Chat_AppendMSG
        End Get
        Set(ByVal value As String)
            _AccountClass_Chat_AppendMSG = value
        End Set
    End Property
#End Region

    Public Sub New(ByVal mode As LanguageType)
        Select Case mode
            Case LanguageType.Japanese
                _DateType = "HH:mm:ss"

                _MainForm_CAccount_Connect = "�ڑ�(&C)"
                _MainForm_CAccount_Reconnect = "�Đڑ�(&R)"
                _MainForm_CAccount_Chat = "�`���b�g(&H)"
                _MainForm_CAccount_AddAccount = "�ڑ���A�J�E���g�̒ǉ�(&A)"
                _MainForm_CAccount_EditAccount = "�ڑ���A�J�E���g�̕ҏW(&E)"
                _MainForm_CAccount_DeleteAccount = "�ڑ���A�J�E���g�̍폜(&D)"
                _MainForm_CAccount_Sort = "���ёւ�(&S)"
                _MainForm_CAccount_Sort_AccountName = "�A�J�E���g��(&N)"
                _MainForm_CAccount_Sort_LoginStatus = "���O�C�����(&L)"
                _MainForm_CAccount_Setting = "�ݒ�(&T)"

                _MainForm_CTransport_Open = "�J��(&O)"
                _MainForm_CTransport_PriorityUp = "�D�揇�ʂ��グ��(&U)"
                _MainForm_CTransport_PriorityDown = "�D�揇�ʂ�������(&N)"
                _MainForm_CTransport_Pause = "�ꎞ��~(&E)"
                _MainForm_CTransport_Restart = "�ĊJ(&R)"
                _MainForm_CTransport_Stop = "���f(&I)"
                _MainForm_CTransport_Delete = "�f�B�X�N����폜(&D)"
                _MainForm_CTransport_Sort = "���ёւ�(&S)"
                _MainForm_CTransport_Sort_Remote = "����(&T)"
                _MainForm_CTransport_Sort_Priority = "�L���[(&Q)"
                _MainForm_CTransport_Sort_FileName = "�t�@�C����(&F)"
                _MainForm_CTransport_Sort_FullPath = "�t���p�X(&L)"
                _MainForm_CTransport_Sort_Size = "�T�C�Y(&S)"
                _MainForm_CTransport_Sort_Status = "�X�e�[�^�X(&U)"
                _MainForm_CTransport_ListMode = "���X�g���[�h(&L)"
                _MainForm_CTransport_SearchBox = "�����{�b�N�X(&B)"
                _MainForm_CTransport_AllSelect = "�S�đI��(&A)"

                _MainForm_Transport_DownloadMenu = "�_�E�����[�h"
                _MainForm_Transport_Download_Approval = "���F"
                _MainForm_Transport_Download_AllApproval = "�ꊇ���F"
                _MainForm_Transport_Download_Resume = "���W���[�����F"
                _MainForm_Transport_UploadMenu = "�A�b�v���[�h"
                _MainForm_Transport_Upload_File = "�t�@�C��"
                _MainForm_Transport_Upload_Folder = "�t�H���_"
                _MainForm_Transport_Pause = "�ꎞ��~"
                _MainForm_Transport_Restart = "�ĊJ"
                _MainForm_Transport_Stop = "���f"
                _MainForm_Transport_Delete = "�폜"
                _MainForm_Transport_DoneDelete = "�����폜"
                _MainForm_Transport_GoShareList = "���L���X�g"
                _MainForm_Transport_Column_Remote = "����"
                _MainForm_Transport_Column_Priority = "�D�揇��"
                _MainForm_Transport_Column_FileName = "�t�@�C����"
                _MainForm_Transport_Column_FullPath = "�t���p�X"
                _MainForm_Transport_Column_Size = "�T�C�Y"
                _MainForm_Transport_Column_Status = "�X�e�[�^�X"

                _MainForm_CShare_Download = "�_�E�����[�h(&D)"
                _MainForm_CShare_Up = "���(&T)"
                _MainForm_CShare_Refresh = "�X�V(&R)"
                _MainForm_CShare_Root = "���[�g(&O)"
                _MainForm_CShare_Sort = "���ёւ�(&S)"
                _MainForm_CShare_Sort_FileName = "�t�@�C����(&F)"
                _MainForm_CShare_Sort_Extension = "�g���q(&E)"
                _MainForm_CShare_Sort_Size = "�T�C�Y(&S)"
                _MainForm_CShare_Sort_DateModified = "�X�V����(&T)"
                _MainForm_CShare_SearchBox = "�����{�b�N�X(&B)"
                _MainForm_CShare_AllSelect = "�S�đI��(&A)"

                _MainForm_Share_Download = "�_�E�����[�h"
                _MainForm_Share_Up = "���"
                _MainForm_Share_Refresh = "�X�V"
                _MainForm_Share_Root = "���[�g"
                _MainForm_Share_GoTransferList = "�]�����X�g"
                _MainForm_Share_Column_FileName = "�t�@�C����"
                _MainForm_Share_Column_Extension = "�g���q"
                _MainForm_Share_Column_Size = "�T�C�Y"
                _MainForm_Share_Column_DateModified = "�X�V����"

                _MainForm_SearchBox_SearchButton = "����(&B)"
                _MainForm_Infomation_ConnectStartTime = "�ڑ����ԁF"
                _MainForm_Infomation_ConnectTime = "�ʐM���ԁF"
                _MainForm_Infomation_TotalDownloaded = "����M�ʁF"
                _MainForm_Infomation_TotalUploaded = "�����M�ʁF"
                _MainForm_Infomation_UploadedSpeed = "���M���x�F"
                _MainForm_Infomation_DownloadedSpeed = "��M���x�F"
                _MainForm_Infomation_YourVersion = "����̃o�[�W�����F"
                _MainForm_Infomation_YourProtocolVersion = "����̃v���g�R���o�[�W�����F"
                _MainForm_Infomation_MyVersion = "���g�̃o�[�W�����F"
                _MainForm_Infomation_MyProtocolVersion = "���g�̃v���g�R���o�[�W�����F"
                _MainForm_Infomation_YourIPAddress = "�����IP�F"
                _MainForm_Infomation_YourPort = "����̃|�[�g�F"
                _MainForm_Infomation_RecEncryptDataNum = "�Í����f�[�^�̎�M�F"
                _MainForm_Infomation_SendEncryptDataNum = "�Í����f�[�^�̑��M�F"
                _MainForm_PortListenErrorMSG = "�|�[�g��Listen�o���܂���ł����B���̃|�[�g�͊���Listen����Ă���\��������܂��B"
                _MainForm_Drawing_Transport_NotApproval = "���F�҂�"
                _MainForm_Drawing_Transport_ErrorDone = "�G���[�I��"
                _MainForm_Drawing_Transport_Done = "����"
                _MainForm_Drawing_Transport_DownloadStop = "�_�E�����[�h��~��"
                _MainForm_Drawing_Transport_UploadStop = "�A�b�v���[�h��~��"
                _MainForm_Drawing_Transport_QueueStop = "���ԑ҂�"
                _MainForm_Drawing_Transport_Folder = "�t�H���_"

                _MainForm_DialogTitle_AllApproval = "�ꊇ���F��t�H���_���w�肵�Ă�������"
                _MainForm_DialogTitle_UploadFile = "�A�b�v���[�h����t�@�C����I�����Ă�������"
                _MainForm_DialogTitle_UploadFolder = "�A�b�v���[�h����t�H���_��I�����Ă�������"
                _MainForm_Dialog_UploadFileToAllUsers = "�I�����C���̑S�Ẵ��[�U�[�ɃA�b�v���[�h���܂�"
                _MainForm_Dialog_Stop = "�{���ɒ��f���܂����H"
                _MainForm_Dialog_Delete = "�f�B�X�N����_�E�����[�h�f�[�^�͍폜����܂��B�{���ɍ폜���܂����H"
                _MainForm_Dialog_Quit = "�{���ɏI�����܂����H"

                _SettingForm_Title = "�ݒ�"
                _SettingForm_Version = "�o�[�W����"
                _SettingForm_Version_SoftwareName = "�\�t�g���F"
                _SettingForm_Version_Version = "�o�[�W�����F"
                _SettingForm_Version_ProtocolVersion = "�v���g�R���o�[�W�����F"
                _SettingForm_Version_Developer = "�J���ҁF"
                _SettingForm_Version_ToolbarIcon = "�c�[���o�[�A�C�R���F"
                _SettingForm_Version_ExeIcon = "EXE�A�C�R���F"
                _SettingForm_General = "���"
                _SettingForm_General_AlwaysOnTop = "��Ɏ�O�ɕ\������"
                _SettingForm_General_HideWhenMinimization = "�ŏ������ɉB��"
                _SettingForm_General_Port = "�|�[�g"
                _SettingForm_General_AllowMultipleStarts = "���d�N����������"
                _SettingForm_MyAccount = "�����̃A�J�E���g"
                _SettingForm_MyAccount_AccountName = "�A�J�E���g��"
                _SettingForm_MyAccount_Comment = "�R�����g"
                _SettingForm_Notify = "�ʒm"
                _SettingForm_Notify_WhenConnect = "�ڑ����ꂽ��"
                _SettingForm_Notify_WhenReceiveMessage = "���b�Z�[�W���󂯎������"
                _SettingForm_Notify_WhenDoneUpload = "�A�b�v���[�h������������"
                _SettingForm_Notify_WhenDoneDownload = "�_�E�����[�h������������"
                _SettingForm_Notify_WhenUpload = "�A�b�v���[�h���ꂽ��"
                _SettingForm_Cancel = "�L�����Z��"
                _SettingForm_Ok = "����"

                _ChatForm_CView_Copy = "�R�s�[(&C)"
                _ChatForm_CView_AllSelect = "�S�đI��(&A)"
                _ChatForm_CView_AllDelete = "�S�č폜(&D)"
                _ChatForm_CView_AlwaysOnTop = "��Ɏ�O�ɕ\��(&M)"
                _ChatForm_CView_ShowInfomation = "�ʐM���̕\��(&T)"
                _ChatForm_CView_Search = "����(&F)"
                _ChatForm_SearchButton = "����"

                _AccountForm_DestAccount = "�ڑ���A�J�E���g"
                _AccountForm_DestAccount_AccountName = "�A�J�E���g��"
                _AccountForm_DestAccount_IPAddress = "IP�A�h���X"
                _AccountForm_DestAccount_Port = "�|�[�g"
                _AccountForm_DestAccount_LoginKey = "�F�؃L�["
                _AccountForm_DestAccount_Encrypt = "�ʐM���Í�������"
                _AccountForm_ApprovalDownload = "�_�E�����[�h�̎������F������"
                _AccountForm_ApprovalDownload_SavePath = "�ۑ��ꏊ"
                _AccountForm_ApprovalDownload_SelectPath = "�I��"
                _AccountForm_EnableShare = "�t�@�C���̋��L�@�\��L���ɂ���"
                _AccountForm_CShare_AddFolder = "���L�t�H���_��ǉ�(&A)"
                _AccountForm_CShare_DeleteFromList = "�I�������A�C�e�������X�g����폜(&D)"
                _AccountForm_CShare_DeleteUnExistFolder = "�p�X�����݂��Ȃ��A�C�e�����폜����(&R)"
                _AccountForm_DialogTitle_SelectFolder = "�t�H���_���w�肵�Ă�������"
                _AccountForm_DialogMSG_AlreadyExist = "���ɑ��݂��Ă���A�J�E���g���ł��B�ύX���Ă�������"
                _AccountForm_DialogMSG_AccountNameTooShort = "�A�J�E���g���̒������Z�����܂��B�ύX���Ă�������"
                _AccountForm_FormTitle = "�A�J�E���g�̕ҏW"
                _AccountForm_OKButton = "����"
                _AccountForm_CancelButton = "�L�����Z��"

                _AccountClass_NotifyMSG_Chat = "%s%����̃��b�Z�[�W"
                _AccountClass_NotifyMSG_Upload = "%s%����A�b�v���[�h"
                _AccountClass_NotifyMSG_Connect = "%s%�Ɛڑ�����܂���"
                _AccountClass_NotifyTitle_Connect = "�ڑ�����܂���"
                _AccountClass_DialogMSG_MaxNumUpload = "�A�b�v���[�h����ɒB���܂���"
                _AccountClass_DialogTitle_UploadApprovalSavePath = "%s%�̕ۑ����I�����Ă�������"
                _AccountClass_DialogTitle_ResumeFile = "%s%�̓r���̃t�@�C����I�����Ă�������"
                _AccountClass_DialogTitle_ResumeFolder = "%s%�̓r���̃t�H���_��I�����Ă�������"
                _AccountClass_DialogMSG_MustNotResume = "���W���[������K�v�͂���܂���"
                _AccountClass_DialogTitle_SelectResumeFolder = "%s%�̓r���̃t�H���_��I�����Ă�������"
                _AccountClass_Chat_AppendMSG = "������"
            Case LanguageType.English
                _DateType = "HH:mm:ss"

                _MainForm_CAccount_Connect = "Connect(&C)"
                _MainForm_CAccount_Reconnect = "Reconnect(&R)"
                _MainForm_CAccount_Chat = "Chat(&H)"
                _MainForm_CAccount_AddAccount = "Add account(&A)"
                _MainForm_CAccount_EditAccount = "Edit account(&E)"
                _MainForm_CAccount_DeleteAccount = "Delete account(&D)"
                _MainForm_CAccount_Sort = "Sort(&S)"
                _MainForm_CAccount_Sort_AccountName = "Account name(&N)"
                _MainForm_CAccount_Sort_LoginStatus = "Login status(&L)"
                _MainForm_CAccount_Setting = "Setting(&T)"

                _MainForm_CTransport_Open = "Open(&O)"
                _MainForm_CTransport_PriorityUp = "Increase priority(&U)"
                _MainForm_CTransport_PriorityDown = "Decrease priority(&N)"
                _MainForm_CTransport_Pause = "Pause(&E)"
                _MainForm_CTransport_Restart = "Restart(&R)"
                _MainForm_CTransport_Stop = "Stop(&I)"
                _MainForm_CTransport_Delete = "Delete from disk(&D)"
                _MainForm_CTransport_Sort = "Sort(&S)"
                _MainForm_CTransport_Sort_Remote = "Remote(&T)"
                _MainForm_CTransport_Sort_Priority = "Priority(&Q)"
                _MainForm_CTransport_Sort_FileName = "File name(&F)"
                _MainForm_CTransport_Sort_FullPath = "Full path(&L)"
                _MainForm_CTransport_Sort_Size = "Size(&S)"
                _MainForm_CTransport_Sort_Status = "Status(&U)"
                _MainForm_CTransport_ListMode = "List mode(&L)"
                _MainForm_CTransport_SearchBox = "Search box(&B)"
                _MainForm_CTransport_AllSelect = "All select(&A)"

                _MainForm_Transport_DownloadMenu = "Download"
                _MainForm_Transport_Download_Approval = "Approval"
                _MainForm_Transport_Download_AllApproval = "Approval together"
                _MainForm_Transport_Download_Resume = "Resume"
                _MainForm_Transport_UploadMenu = "Upload"
                _MainForm_Transport_Upload_File = "File"
                _MainForm_Transport_Upload_Folder = "Folder"
                _MainForm_Transport_Pause = "Pause"
                _MainForm_Transport_Restart = "Restart"
                _MainForm_Transport_Stop = "Stop"
                _MainForm_Transport_Delete = "Delete"
                _MainForm_Transport_DoneDelete = "Delete done"
                _MainForm_Transport_GoShareList = "Share list"
                _MainForm_Transport_Column_Remote = "Remote"
                _MainForm_Transport_Column_Priority = "Priority"
                _MainForm_Transport_Column_FileName = "FileName"
                _MainForm_Transport_Column_FullPath = "FullPath"
                _MainForm_Transport_Column_Size = "Size"
                _MainForm_Transport_Column_Status = "Status"

                _MainForm_CShare_Download = "Download(&D)"
                _MainForm_CShare_Up = "Up(&T)"
                _MainForm_CShare_Refresh = "Refresh(&R)"
                _MainForm_CShare_Root = "Root(&O)"
                _MainForm_CShare_Sort = "Sort(&S)"
                _MainForm_CShare_Sort_FileName = "File name(&F)"
                _MainForm_CShare_Sort_Extension = "Extension(&E)"
                _MainForm_CShare_Sort_Size = "Size(&S)"
                _MainForm_CShare_Sort_DateModified = "Date modified(&T)"
                _MainForm_CShare_SearchBox = "Search box(&B)"
                _MainForm_CShare_AllSelect = "All select(&A)"

                _MainForm_Share_Download = "Download"
                _MainForm_Share_Up = "Up"
                _MainForm_Share_Refresh = "Refresh"
                _MainForm_Share_Root = "Root"
                _MainForm_Share_GoTransferList = "Transfer list"
                _MainForm_Share_Column_FileName = "File name"
                _MainForm_Share_Column_Extension = "Extension"
                _MainForm_Share_Column_Size = "Size"
                _MainForm_Share_Column_DateModified = "Date modified"

                _MainForm_SearchBox_SearchButton = "Search(&B)"
                _MainForm_Infomation_ConnectStartTime = "Connect start:"
                _MainForm_Infomation_ConnectTime = "Connect time:"
                _MainForm_Infomation_TotalDownloaded = "Total down:"
                _MainForm_Infomation_TotalUploaded = "Total up:"
                _MainForm_Infomation_UploadedSpeed = "Up speed:"
                _MainForm_Infomation_DownloadedSpeed = "Down speed:"
                _MainForm_Infomation_YourVersion = "Your version:"
                _MainForm_Infomation_YourProtocolVersion = "Your prtc version:"
                _MainForm_Infomation_MyVersion = "My version:"
                _MainForm_Infomation_MyProtocolVersion = "My prtc version:"
                _MainForm_Infomation_YourIPAddress = "Your ip:"
                _MainForm_Infomation_YourPort = "your port:"
                _MainForm_Infomation_RecEncryptDataNum = "Down encrypt:"
                _MainForm_Infomation_SendEncryptDataNum = "Up encrypt:"
                _MainForm_PortListenErrorMSG = "Listen to the port could not. This port may already have been Listen."
                _MainForm_Drawing_Transport_NotApproval = "Waiting"
                _MainForm_Drawing_Transport_ErrorDone = "Error"
                _MainForm_Drawing_Transport_Done = "Done"
                _MainForm_Drawing_Transport_DownloadStop = "Stopping down"
                _MainForm_Drawing_Transport_UploadStop = "Stopping up"
                _MainForm_Drawing_Transport_QueueStop = "Queueing"
                _MainForm_Drawing_Transport_Folder = "Folder"

                _MainForm_DialogTitle_AllApproval = "Please select a folder."
                _MainForm_DialogTitle_UploadFile = "Please select a file to upload."
                _MainForm_DialogTitle_UploadFolder = "Please select a folder to upload."
                _MainForm_Dialog_UploadFileToAllUsers = "Upload all users online."
                _MainForm_Dialog_Stop = "Do you really want to stop?"
                _MainForm_Dialog_Delete = "Remove download data. Do you really want to remove?"
                _MainForm_Dialog_Quit = "Do you really want to exit?"

                _SettingForm_Title = "Setting"
                _SettingForm_Version = "Version"
                _SettingForm_Version_SoftwareName = "Software name:"
                _SettingForm_Version_Version = "Version:"
                _SettingForm_Version_ProtocolVersion = "Protocol version:"
                _SettingForm_Version_Developer = "Developer:"
                _SettingForm_Version_ToolbarIcon = "Toolbar icon:"
                _SettingForm_Version_ExeIcon = "Exe icon:"
                _SettingForm_General = "General"
                _SettingForm_General_AlwaysOnTop = "Always on top"
                _SettingForm_General_HideWhenMinimization = "Minimize to tray"
                _SettingForm_General_Port = "Port"
                _SettingForm_General_AllowMultipleStarts = "Allow multiple starts"
                _SettingForm_MyAccount = "My account"
                _SettingForm_MyAccount_AccountName = "Account name"
                _SettingForm_MyAccount_Comment = "Comment"
                _SettingForm_Notify = "Notify"
                _SettingForm_Notify_WhenConnect = "When connect"
                _SettingForm_Notify_WhenReceiveMessage = "When receive msg"
                _SettingForm_Notify_WhenDoneUpload = "When upload done"
                _SettingForm_Notify_WhenDoneDownload = "When download done"
                _SettingForm_Notify_WhenUpload = "When uploaded"
                _SettingForm_Cancel = "Cancel"
                _SettingForm_Ok = "OK"

                _ChatForm_CView_Copy = "Copy(&C)"
                _ChatForm_CView_AllSelect = "All select(&A)"
                _ChatForm_CView_AllDelete = "All delete(&D)"
                _ChatForm_CView_AlwaysOnTop = "Always on top(&M)"
                _ChatForm_CView_ShowInfomation = "Infomation(&T)"
                _ChatForm_CView_Search = "Search(&F)"
                _ChatForm_SearchButton = "Search"

                _AccountForm_DestAccount = "Dest account"
                _AccountForm_DestAccount_AccountName = "Account name"
                _AccountForm_DestAccount_IPAddress = "IP address"
                _AccountForm_DestAccount_Port = "Port"
                _AccountForm_DestAccount_LoginKey = "Login key"
                _AccountForm_DestAccount_Encrypt = "Send encrypted data"
                _AccountForm_ApprovalDownload = "Enable automatic approval of downloads"
                _AccountForm_ApprovalDownload_SavePath = "Location"
                _AccountForm_ApprovalDownload_SelectPath = "Select"
                _AccountForm_EnableShare = "Enable share folder"
                _AccountForm_CShare_AddFolder = "Add share folder(&A)"
                _AccountForm_CShare_DeleteFromList = "Remove selected item(&D)"
                _AccountForm_CShare_DeleteUnExistFolder = "Delete a folder that does not exist(&R)"
                _AccountForm_DialogTitle_SelectFolder = "Please select a folder"
                _AccountForm_DialogMSG_AlreadyExist = "The account name already exists. Please change."
                _AccountForm_DialogMSG_AccountNameTooShort = "Account name is too short. Please change."
                _AccountForm_FormTitle = "Edit account"
                _AccountForm_OKButton = "OK"
                _AccountForm_CancelButton = "Cancel"

                _AccountClass_NotifyMSG_Chat = "%s% messsage"
                _AccountClass_NotifyMSG_Upload = "%s% upload"
                _AccountClass_NotifyMSG_Connect = "%s% connect"
                _AccountClass_NotifyTitle_Connect = "Connect"
                _AccountClass_DialogMSG_MaxNumUpload = "Reached the maximum of uploads."
                _AccountClass_DialogTitle_UploadApprovalSavePath = "Please select a file of the %s%"
                _AccountClass_DialogTitle_ResumeFile = "Please select a file in the middle of the %s%"
                _AccountClass_DialogTitle_ResumeFolder = "Please select a folder in the middle of the %s%"
                _AccountClass_DialogMSG_MustNotResume = "There is no need to resume."
                _AccountClass_DialogTitle_SelectResumeFolder = "Please select a file in the middle of the %s%"
                _AccountClass_Chat_AppendMSG = "Anonymous"
        End Select
    End Sub
End Class