Imports Arachlex.DefinitionClass
<Serializable()> Public Class AccountData

    Private _AccountName As String
    ''' <summary>
    ''' ダブらないアカウント名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountName() As String
        Get
            If _AccountName Is Nothing Then
                _AccountName = Account_Default
            ElseIf _AccountName.Length > Account_NameLength_Max Then
                _AccountName = _AccountName.Substring(Account_NameLength_Max)
            ElseIf _AccountName.Length < Account_NameLength_Min Then
                _AccountName = Account_Default
            End If
            Return _AccountName
        End Get
        Set(ByVal value As String)
            _AccountName = value
        End Set
    End Property

    Private _UserComment As String
    ''' <summary>
    ''' ユーザーのコメント
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserComment() As String
        Get
            If _UserComment Is Nothing Then
                _UserComment = ""
            ElseIf _UserComment.Length > Comment_Length_Max Then
                _UserComment = _UserComment.Substring(Comment_Length_Max)
            ElseIf _UserComment.Length < Comment_Length_Min Then
                _UserComment = ""
            End If
            Return _UserComment
        End Get
        Set(ByVal value As String)
            _UserComment = value
        End Set
    End Property

    Private _ConnectPort As Integer
    ''' <summary>
    ''' 接続するためのポート
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConnectPort() As Integer
        Get
            If _ConnectPort > Port_Max Then
                _ConnectPort = Port_Max
            ElseIf _ConnectPort < Port_Min Then
                _ConnectPort = Port_Min
            End If
            Return _ConnectPort
        End Get
        Set(ByVal value As Integer)
            If value > Port_Max Or value < Port_Min Then
                _ConnectPort = Port_Default
            Else
                _ConnectPort = value
            End If
        End Set
    End Property

    Private _ConnectIP As String
    ''' <summary>
    ''' 接続するIPアドレス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConnectIP() As String
        Get
            Return _ConnectIP
        End Get
        Set(ByVal value As String)
            _ConnectIP = value
        End Set
    End Property

    Private _LoginPassword As String
    ''' <summary>
    ''' 認証キー
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LoginPassword() As String
        Get
            If _LoginPassword Is Nothing Then
                _LoginPassword = ""
            ElseIf _LoginPassword.Length > AttKey_Length_Max Then
                _LoginPassword = _LoginPassword.Substring(AttKey_Length_Max)
            ElseIf _LoginPassword.Length < AttKey_Length_Min Then
                _LoginPassword = ""
            End If
            Return _LoginPassword
        End Get
        Set(ByVal value As String)
            _LoginPassword = value
        End Set
    End Property

    Private _UseEncryptNetwork As Boolean
    ''' <summary>
    ''' 通信経路を暗号化するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseEncryptNetwork() As Boolean
        Get
            Return _UseEncryptNetwork
        End Get
        Set(ByVal value As Boolean)
            _UseEncryptNetwork = value
        End Set
    End Property

    Private _DownloadAutoSave As Boolean
    ''' <summary>
    ''' ダウンロードの自動承認を有効にするか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DownloadAutoSave() As Boolean
        Get
            Return _DownloadAutoSave
        End Get
        Set(ByVal value As Boolean)
            _DownloadAutoSave = value
        End Set
    End Property

    Private _DownloadAutoSavePath As String
    ''' <summary>
    ''' ダウンロード自動承認時の保存場所
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DownloadAutoSavePath() As String
        Get
            If _DownloadAutoSavePath Is Nothing Then
                _DownloadAutoSavePath = DefaultFolder
            ElseIf Not IO.Directory.Exists(_DownloadAutoSavePath) Then
                _DownloadAutoSavePath = DefaultFolder
            End If
            Return _DownloadAutoSavePath
        End Get
        Set(ByVal value As String)
            _DownloadAutoSavePath = value
        End Set
    End Property

    Private _Share_Use As Boolean
    ''' <summary>
    ''' ファイル共有機能を利用するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Share_Use() As Boolean
        Get
            Return _Share_Use
        End Get
        Set(ByVal value As Boolean)
            _Share_Use = value
        End Set
    End Property

    Private _Share_OpenFolder As Generic.List(Of String)
    ''' <summary>
    ''' ファイル共有機能で公開するフォルダ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Share_OpenFolder() As Generic.List(Of String)
        Get
            Return _Share_OpenFolder
        End Get
        Set(ByVal value As Generic.List(Of String))
            _Share_OpenFolder = value
            SetSuccessionArray(_Share_OpenFolder)
        End Set
    End Property

    Private _Chat_Location As Point
    ''' <summary>
    ''' チャットウィンドウの位置
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Chat_Location() As Point
        Get
            Return _Chat_Location
        End Get
        Set(ByVal value As Point)
            _Chat_Location = value
        End Set
    End Property

    Private _Chat_Size As Size
    ''' <summary>
    ''' チャットウィンドウのサイズ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Chat_Size() As Size
        Get
            Return _Chat_Size
        End Get
        Set(ByVal value As Size)
            _Chat_Size = value
        End Set
    End Property

    Private _Chat_SplitDistanceInputBoxWithView As Integer
    ''' <summary>
    ''' チャットウィンドウの入力ボックスとビューワの距離
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Chat_SplitDistanceInputBoxWithView() As Integer
        Get
            Return _Chat_SplitDistanceInputBoxWithView
        End Get
        Set(ByVal value As Integer)
            _Chat_SplitDistanceInputBoxWithView = value
        End Set
    End Property

    Private _Chat_SplitDistanceViewWithInfo As Integer
    ''' <summary>
    ''' チャットウィンドウの入力ボックスとビューワの距離
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Chat_SplitDistanceViewWithInfo() As Integer
        Get
            Return _Chat_SplitDistanceViewWithInfo
        End Get
        Set(ByVal value As Integer)
            _Chat_SplitDistanceViewWithInfo = value
        End Set
    End Property

    Private _Chat_ShowInfo As Boolean
    ''' <summary>
    ''' チャットの情報を表示するかどうか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Chat_ShowInfo() As Boolean
        Get
            Return _Chat_ShowInfo
        End Get
        Set(ByVal value As Boolean)
            _Chat_ShowInfo = value
        End Set
    End Property

    Private _Chat_ShowFindBox As Boolean
    ''' <summary>
    ''' チャットの検索ボックスを表示するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Chat_ShowFindBox() As Boolean
        Get
            Return _Chat_ShowFindBox
        End Get
        Set(ByVal value As Boolean)
            _Chat_ShowFindBox = value
        End Set
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        AccountName = Account_Default
        UserComment = ""
        ConnectPort = Port_Default
        ConnectIP = ""
        LoginPassword = ""
        _DownloadAutoSave = False
        _DownloadAutoSavePath = DefaultFolder
        _Share_Use = False
        _Share_OpenFolder = New Generic.List(Of String)
        _UseEncryptNetwork = False

        _Chat_Location = New Point(100, 100)
        _Chat_Size = New Size(370, 440)
        _Chat_SplitDistanceInputBoxWithView = 350
        _Chat_SplitDistanceViewWithInfo = 135
        _Chat_ShowInfo = False
        _Chat_ShowFindBox = False
    End Sub

    ''' <summary>
    ''' 存在することが保障された公開フォルダリストを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetExitsSharedList() As Generic.List(Of String)
        Dim sb As New Generic.List(Of String)
        If _Share_OpenFolder IsNot Nothing AndAlso _Share_OpenFolder.Count > 0 Then
            For i As Integer = 0 To _Share_OpenFolder.Count - 1
                If IO.Directory.Exists(_Share_OpenFolder(i)) Then
                    sb.Add(_Share_OpenFolder(i))
                End If
            Next
        End If
        Return sb
    End Function
End Class