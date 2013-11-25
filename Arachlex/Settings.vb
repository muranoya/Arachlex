Imports Arachlex.DefinitionClass
<Serializable()> Public Class Settings

    Private _Main_Location As Point
    Private _Main_Size As Size
    Private _Main_MiniHide As Boolean
    Private _Main_TopMost As Boolean
    Private _Main_AllowMultipleStarts As Boolean

    Private _Split_Main As Integer
    Private _TransferList_ListMode As Boolean
    Private _ShowFindBox As Boolean

    Private _Language As LanguageClass.LanguageType

    Private _Sort_Account_Order As SortOrder
    Private _Sort_Account_Column As Integer
    Private _Sort_Transfer_Order As SortOrder
    Private _Sort_Transfer_Column As Integer
    Private _Sort_Share_Order As SortOrder
    Private _Sort_Share_Column As Integer

    Private _Column_Transfer_Width As List(Of Integer)
    Private _Column_Transfer_DisplayIndex As List(Of Integer)
    Private _Column_Share_Width As List(Of Integer)
    Private _Column_Share_DisplayIndex As List(Of Integer)

    Private _SaveDialogPath As String
    Private _OpenDialogPath As String
    Private _AllSaveDialogPath As String

    Private _Account_AccountName As String
    Private _Account_Comment As String
    Private _Account_Data As List(Of AccountData)

    Private _Connect_ListenPort As Integer

    Private _Notify_Connect As Boolean
    Private _Notify_ReceiveMSG As Boolean
    Private _Notify_DoneDownload As Boolean
    Private _Notify_DoneUpload As Boolean
    Private _Notify_Upload As Boolean

    ''' <summary>
    ''' 自身のウィンドウの位置
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Main_Location() As Point
        Get
            Return _Main_Location
        End Get
        Set(ByVal value As Point)
            _Main_Location = value
        End Set
    End Property
    ''' <summary>
    ''' 自身のウィンドウのサイズ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Main_Size() As Size
        Get
            Return _Main_Size
        End Get
        Set(ByVal value As Size)
            _Main_Size = value
        End Set
    End Property
    ''' <summary>
    ''' 最小化時に隠すかどうか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Main_MiniHide() As Boolean
        Get
            Return _Main_MiniHide
        End Get
        Set(ByVal value As Boolean)
            _Main_MiniHide = value
        End Set
    End Property
    ''' <summary>
    ''' 常に手前に表示しているか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Main_TopMost() As Boolean
        Get
            Return _Main_TopMost
        End Get
        Set(ByVal value As Boolean)
            _Main_TopMost = value
        End Set
    End Property
    ''' <summary>
    ''' 多重起動を許可するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Main_AllowMultipleStarts() As Boolean
        Get
            Return _Main_AllowMultipleStarts
        End Get
        Set(ByVal value As Boolean)
            _Main_AllowMultipleStarts = value
        End Set
    End Property

    ''' <summary>
    ''' スプリットコンテナのチャットとコンタクトリストとの分割サイズ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Split_Main() As Integer
        Get
            Return _Split_Main
        End Get
        Set(ByVal value As Integer)
            _Split_Main = value
        End Set
    End Property
    ''' <summary>
    ''' 転送リストの表示をリスト表示にするか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransferList_ListMode() As Boolean
        Get
            Return _TransferList_ListMode
        End Get
        Set(ByVal value As Boolean)
            _TransferList_ListMode = value
        End Set
    End Property
    ''' <summary>
    ''' 検索ボックスを表示するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowFindBox() As Boolean
        Get
            Return _ShowFindBox
        End Get
        Set(ByVal value As Boolean)
            _ShowFindBox = value
        End Set
    End Property

    ''' <summary>
    ''' 言語設定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Language() As LanguageClass.LanguageType
        Get
            Return _Language
        End Get
        Set(ByVal value As LanguageClass.LanguageType)
            _Language = value
        End Set
    End Property

    ''' <summary>
    ''' アカウントリストのソートオーダー
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sort_Account_Order() As SortOrder
        Get
            Return _Sort_Account_Order
        End Get
        Set(ByVal value As SortOrder)
            _Sort_Account_Order = value
        End Set
    End Property
    ''' <summary>
    ''' アカウントリストのソートするカラム。ただし、ログイン状態でソートする場合はColumnIndexに1を指定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sort_Account_Column() As Integer
        Get
            Return _Sort_Account_Column
        End Get
        Set(ByVal value As Integer)
            _Sort_Account_Column = value
        End Set
    End Property
    ''' <summary>
    ''' 転送リストのソートオーダー
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sort_Transfer_Order() As SortOrder
        Get
            Return _Sort_Transfer_Order
        End Get
        Set(ByVal value As SortOrder)
            _Sort_Transfer_Order = value
        End Set
    End Property
    ''' <summary>
    ''' 転送リストのソートするカラム
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sort_Transfer_Column() As Integer
        Get
            Return _Sort_Transfer_Column
        End Get
        Set(ByVal value As Integer)
            _Sort_Transfer_Column = value
        End Set
    End Property
    ''' <summary>
    ''' 共有リストのソートオーダー
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sort_Share_Order() As SortOrder
        Get
            Return _Sort_Share_Order
        End Get
        Set(ByVal value As SortOrder)
            _Sort_Share_Order = value
        End Set
    End Property
    ''' <summary>
    ''' 共有リストのソートするカラム
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sort_Share_Column() As Integer
        Get
            Return _Sort_Share_Column
        End Get
        Set(ByVal value As Integer)
            _Sort_Share_Column = value
        End Set
    End Property

    ''' <summary>
    ''' 転送リストの幅カラム
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Column_Transfer_Width() As List(Of Integer)
        Get
            Return _Column_Transfer_Width
        End Get
        Set(ByVal value As List(Of Integer))
            _Column_Transfer_Width = value
        End Set
    End Property
    ''' <summary>
    ''' 転送リストのディスプレイインデックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Column_Transfer_DisplayIndex() As List(Of Integer)
        Get
            Return _Column_Transfer_DisplayIndex
        End Get
        Set(ByVal value As List(Of Integer))
            _Column_Transfer_DisplayIndex = value
        End Set
    End Property
    ''' <summary>
    ''' 共有リストの幅カラム
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Column_Share_Width() As List(Of Integer)
        Get
            If _Column_Share_Width Is Nothing Then
                _Column_Share_Width = New List(Of Integer)
            End If
            Return _Column_Share_Width
        End Get
        Set(ByVal value As List(Of Integer))
            _Column_Share_Width = value
        End Set
    End Property
    ''' <summary>
    ''' 共有リストのディスプレイインデックス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Column_Share_DisplayIndex() As List(Of Integer)
        Get
            If _Column_Share_DisplayIndex Is Nothing Then
                _Column_Share_DisplayIndex = New List(Of Integer)
            End If
            Return _Column_Share_DisplayIndex
        End Get
        Set(ByVal value As List(Of Integer))
            _Column_Share_DisplayIndex = value
        End Set
    End Property

    ''' <summary>
    ''' 保存ダイアログの最初に表示するパス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SaveDialogPath() As String
        Get
            If _SaveDialogPath Is Nothing Then
                _SaveDialogPath = DefaultFolder
            ElseIf IO.Directory.Exists(_SaveDialogPath) = False Then
                _SaveDialogPath = DefaultFolder
            End If
            Return _SaveDialogPath
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                _SaveDialogPath = DefaultFolder
            ElseIf IO.Directory.Exists(value) Then
                _SaveDialogPath = value
            ElseIf IO.Directory.Exists(IO.Path.GetDirectoryName(value)) Then
                _SaveDialogPath = IO.Path.GetDirectoryName(value)
            End If
        End Set
    End Property
    ''' <summary>
    ''' 開くダイアログの最初に表示するパス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OpenDialogPath() As String
        Get
            If _OpenDialogPath Is Nothing Then
                _OpenDialogPath = DefaultFolder
            ElseIf IO.Directory.Exists(_OpenDialogPath) = False Then
                _OpenDialogPath = DefaultFolder
            End If
            Return _OpenDialogPath
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                _OpenDialogPath = DefaultFolder
            ElseIf IO.Directory.Exists(value) Then
                _OpenDialogPath = value
            ElseIf IO.Directory.Exists(IO.Path.GetDirectoryName(value)) Then
                _OpenDialogPath = IO.Path.GetDirectoryName(value)
            End If
        End Set
    End Property
    ''' <summary>
    ''' 一括承認ダイアログの表示パス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllSaveDialogPath() As String
        Get
            If _AllSaveDialogPath Is Nothing Then
                _AllSaveDialogPath = DefaultFolder
            ElseIf Not IO.Directory.Exists(_AllSaveDialogPath) Then
                _AllSaveDialogPath = DefaultFolder
            End If
            Return _AllSaveDialogPath
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                _AllSaveDialogPath = DefaultFolder
            ElseIf IO.Directory.Exists(value) Then
                _AllSaveDialogPath = value
            ElseIf IO.Directory.Exists(IO.Path.GetDirectoryName(value)) Then
                _AllSaveDialogPath = IO.Path.GetDirectoryName(value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' アカウント名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Account_AccountName() As String
        Get
            If _Account_AccountName Is Nothing Then
                _Account_AccountName = Account_MyName_Default
            ElseIf _Account_AccountName.Length > Account_NameLength_Max OrElse _Account_AccountName.Length < Account_NameLength_Min Then
                _Account_AccountName = Account_MyName_Default
            End If
            Return _Account_AccountName
        End Get
        Set(ByVal value As String)
            _Account_AccountName = value
        End Set
    End Property
    ''' <summary>
    ''' アカウントのコメントです
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Account_Comment() As String
        Get
            If _Account_Comment Is Nothing Then
                _Account_Comment = ""
            ElseIf _Account_Comment.Length > Comment_Length_Max Then
                _Account_Comment = _Account_Comment.Substring(Comment_Length_Max)
            ElseIf _Account_Comment.Length < Comment_Length_Min Then
                _Account_Comment = ""
            End If
            Return _Account_Comment
        End Get
        Set(ByVal value As String)
            _Account_Comment = value
        End Set
    End Property
    ''' <summary>
    ''' アカウントのデータです
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Account_Data() As List(Of AccountData)
        Get
            Return _Account_Data
        End Get
        Set(ByVal value As List(Of AccountData))
            _Account_Data = value
            '同じアカウント名のアイテムがないか調べ、同じアイテムが存在する場合は削除する
            If _Account_Data IsNot Nothing AndAlso _Account_Data.Count > 0 Then
                'アカウント名だけ抽出する
                Dim newAccountName As New List(Of String)
                For i As Integer = 0 To Account_Data.Count - 1
                    newAccountName.Add(_Account_Data(i).AccountName.ToLower)
                Next

                'アカウント名だけのリストから調べる
                Dim k As Integer = 0
                Do
                    Dim ind As Integer = k + 1
                    If ind > newAccountName.Count Then ind = newAccountName.Count - 1
                    If newAccountName.IndexOf(newAccountName(k).ToLower, ind) >= 0 OrElse _Account_AccountName.ToLower = newAccountName(k).ToLower Then
                        newAccountName.RemoveAt(k)
                        _Account_Data.RemoveAt(k)
                    Else
                        k += 1
                    End If
                Loop Until k = newAccountName.Count
            End If
        End Set
    End Property

    ''' <summary>
    ''' 接続待ちするポート番号
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Connect_ListenPort() As Integer
        Get
            If _Connect_ListenPort < Port_Min OrElse _Connect_ListenPort > Port_Max Then
                _Connect_ListenPort = Port_Default
            End If
            Return _Connect_ListenPort
        End Get
        Set(ByVal value As Integer)
            _Connect_ListenPort = value
        End Set
    End Property

    ''' <summary>
    ''' 接続されたら通知するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Notify_Connect() As Boolean
        Get
            Return _Notify_Connect
        End Get
        Set(ByVal value As Boolean)
            _Notify_Connect = value
        End Set
    End Property
    ''' <summary>
    ''' メッセージを受け取ったら通知するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Notify_ReceiveMSG() As Boolean
        Get
            Return _Notify_ReceiveMSG
        End Get
        Set(ByVal value As Boolean)
            _Notify_ReceiveMSG = value
        End Set
    End Property
    ''' <summary>
    ''' ダウンロードが完了したら通知する
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Notify_DoneDownload() As Boolean
        Get
            Return _Notify_DoneDownload
        End Get
        Set(ByVal value As Boolean)
            _Notify_DoneDownload = value
        End Set
    End Property
    ''' <summary>
    ''' アップロードが完了したら通知するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Notify_DoneUpload() As Boolean
        Get
            Return _Notify_DoneUpload
        End Get
        Set(ByVal value As Boolean)
            _Notify_DoneUpload = value
        End Set
    End Property
    ''' <summary>
    ''' アップロードされたら通知するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Notify_Upload() As Boolean
        Get
            Return _Notify_Upload
        End Get
        Set(ByVal value As Boolean)
            _Notify_Upload = value
        End Set
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        _Main_Location = New Point(100, 100)
        _Main_Size = New Size(540, 350)
        _Main_MiniHide = False
        _Main_TopMost = False
        _Main_AllowMultipleStarts = True

        _Split_Main = 140
        _TransferList_ListMode = False
        _ShowFindBox = False

        _Language = LanguageClass.LanguageType.Japanese

        _Sort_Account_Order = SortOrder.Ascending
        _Sort_Account_Column = 0
        _Sort_Transfer_Order = SortOrder.Ascending
        _Sort_Transfer_Column = 0
        _Sort_Share_Order = SortOrder.Ascending
        _Sort_Share_Column = 0

        _Column_Transfer_Width = New List(Of Integer)
        With _Column_Transfer_Width
            .Add(40) '相手カラム
            .Add(30) '優先順位カラム
            .Add(100) 'ファイル名カラム
            .Add(70) 'フルパスカラム
            .Add(50) 'サイズカラム
            .Add(70) 'ステータスカラム
        End With
        _Column_Transfer_DisplayIndex = New List(Of Integer)
        With _Column_Transfer_DisplayIndex
            .Add(0)
            .Add(1)
            .Add(2)
            .Add(3)
            .Add(4)
            .Add(5)
        End With
        _Column_Share_Width = New List(Of Integer)
        With _Column_Share_Width
            .Add(230) 'ファイル名カラム
            .Add(80) '拡張子カラム
            .Add(70) 'サイズカラム
            .Add(150) 'タイムスタンプカラム
        End With
        _Column_Share_DisplayIndex = New List(Of Integer)
        With _Column_Share_DisplayIndex
            .Add(0)
            .Add(1)
            .Add(2)
            .Add(3)
        End With

        _SaveDialogPath = DefaultFolder
        _OpenDialogPath = DefaultFolder
        _AllSaveDialogPath = DefaultFolder

        _Account_AccountName = Account_MyName_Default
        _Account_Comment = ""
        _Account_Data = New Generic.List(Of AccountData)

        _Connect_ListenPort = Port_Default

        _Notify_Connect = True
        _Notify_ReceiveMSG = True
        _Notify_DoneDownload = True
        _Notify_DoneUpload = True
        _Notify_Upload = True
    End Sub
End Class