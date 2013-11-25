Imports Arachlex.DefinitionClass
''' <summary>
''' �t�@�C���̏���ێ����郊�X�g�r���[�A�C�e���ł��B
''' </summary>
''' <remarks></remarks>
Public Class FileInfoListViewItem
    Inherits ListViewItem

    Private _FileSize As Long
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

    Private _FileName As String
    ''' <summary>
    ''' �t�@�C����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FileName() As String
        Get
            Return _FileName
        End Get
        Set(ByVal value As String)
            _FileName = value
        End Set
    End Property

    Private _FullPath As String
    ''' <summary>
    ''' �t���p�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FullPath() As String
        Get
            Return _FullPath
        End Get
        Set(ByVal value As String)
            _FullPath = value
        End Set
    End Property

    Private _Attribute As Arachlex_Item
    ''' <summary>
    ''' �A�C�e���̎��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Attribute() As Arachlex_Item
        Get
            Return _Attribute
        End Get
        Set(ByVal value As Arachlex_Item)
            _Attribute = value
        End Set
    End Property

    Private _TimeStamp As Date
    ''' <summary>
    ''' �^�C���X�^���v
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TimeStamp() As Date
        Get
            Return _TimeStamp
        End Get
        Set(ByVal value As Date)
            _TimeStamp = value
        End Set
    End Property

    Private _ID As Integer
    ''' <summary>
    ''' ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <param name="fileKind">�t�@�C���̎��</param>
    ''' <param name="fSize">�t�@�C���T�C�Y</param>
    ''' <param name="fName">�t�@�C����</param>
    ''' <param name="fTimeStamp">�^�C���X�^���v</param>
    ''' <param name="ItemID">�t�@�C����ID</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal fSize As Long, ByVal fName As String, ByVal fileKind As Arachlex_Item, ByVal fTimeStamp As Date, ByVal ItemID As Integer)
        FileSize = fSize
        FullPath = ""
        FileName = fName
        Attribute = fileKind
        TimeStamp = fTimeStamp
        ID = ItemID
    End Sub

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        FileSize = 0
        FullPath = ""
        FileName = ""
        Attribute = Arachlex_Item.File
        TimeStamp = Now
        ID = 0
    End Sub
End Class