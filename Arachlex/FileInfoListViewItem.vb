Imports Arachlex.DefinitionClass
''' <summary>
''' ファイルの情報を保持するリストビューアイテムです。
''' </summary>
''' <remarks></remarks>
Public Class FileInfoListViewItem
    Inherits ListViewItem

    Private _FileSize As Long
    ''' <summary>
    ''' ファイルサイズ
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
    ''' ファイル名
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
    ''' フルパス
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
    ''' アイテムの種類
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
    ''' タイムスタンプ
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
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="fileKind">ファイルの種類</param>
    ''' <param name="fSize">ファイルサイズ</param>
    ''' <param name="fName">ファイル名</param>
    ''' <param name="fTimeStamp">タイムスタンプ</param>
    ''' <param name="ItemID">ファイルのID</param>
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
    ''' コンストラクタ
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