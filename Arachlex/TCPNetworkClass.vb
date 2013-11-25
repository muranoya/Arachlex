Public Class TCPNetworkClass

    Private ns As System.Net.Sockets.NetworkStream
    Private rijndael As System.Security.Cryptography.RijndaelManaged

    'TCPバッファ
    Public Const Transport_ConnectBufferSize As Integer = 524288 '512KB

    '受信バッファ
    Public Const Transport_RecBufferSize_Max As Integer = 4194304 '4MB
    Public Const Transport_RecBufferSize_Min As Integer = 1024
    Public Const Transport_RecBufferSize_Default As Integer = 65536 '64KB

    Private enc As System.Text.Encoding

    Public Enum TCP_Header As Byte
        'プロトコルの内容
        '{}が一つの区切り。
        '(File)はファイルの場合、(Folder)はフォルダの場合。書く場合は{}内の一番先頭に書く。
        '[OR]はどちらか。[Nothing]は中身は存在しないことを示す。
        'Uploadはアップロード、Downloadはダウンロード側から送信される命令であることを意味する。これは必要な場合一番先頭に書く。
        'フォルダとファイルによって、中身の意味が違う場合、[]で囲む。

        ''' <summary>
        ''' チャットメッセージ
        ''' {ハンドル名} + vbcrlf + {中身}
        ''' </summary>
        ''' <remarks></remarks>
        ChatMSG

        ''' <summary>
        ''' アップロードを開始する
        ''' [Upload{アイテムの種類} +vbcrlf + {Queue} + vbcrlf + {ファイル名} + vbcrlf + {[(File)ファイルサイズ] , [(Folder)全ファイル数]}]
        ''' </summary>
        ''' <remarks></remarks>
        ItemUpload
        ''' <summary>
        ''' ダウンロードを承認する
        ''' [Download(File){ファイルシーク位置}]
        ''' </summary>
        ''' <remarks></remarks>
        ItemApp
        ''' <summary>
        ''' アイテムの転送を一時停止します
        ''' {[Nothing]}
        ''' </summary>
        ''' <remarks></remarks>
        ItemStop
        ''' <summary>
        ''' アイテムの転送を再開します
        ''' {[Nothing]}
        ''' </summary>
        ''' <remarks></remarks>
        ItemReStart
        ''' <summary>
        ''' アイテムを削除します
        ''' {[Nothing]}
        ''' </summary>
        ''' <remarks></remarks>
        ItemDelete
        ''' <summary>
        ''' アイテムの優先順位を変更します
        ''' {変更後の優先順位}
        ''' </summary>
        ''' <remarks></remarks>
        ItemChangeQueue

        ''' <summary>
        ''' フォルダの作成
        ''' [Upload(Folder){ファイルリスト + vbcrlf}]
        ''' </summary>
        ''' <remarks></remarks>
        MakeFolder
        ''' <summary>
        ''' 次の送信ファイルをセットします
        ''' [Upload(Folder){ファイルの相対パス} + vbcrlf + {ファイルサイズ} + vbcrlf + {終了ファイル数}]
        ''' </summary>
        ''' <remarks></remarks>
        NextFolderFile
        ''' <summary>
        ''' ダウンロード側のセットが完了した
        ''' [Download(Folder){[Nothing]}]
        ''' </summary>
        ''' <remarks></remarks>
        NextFolderFileOk
        ''' <summary>
        ''' フォルダの転送完了
        ''' [Upload(Folder){[Nothing]}]
        ''' </summary>
        ''' <remarks></remarks>
        FolderDone

        ''' <summary>
        ''' アップロード側のシーク位置を調整する
        ''' [Download{ファイルのシーク位置}]
        ''' </summary>
        ''' <remarks></remarks>
        SetSeek

        ''' <summary>
        ''' ファイル送信
        ''' [Upload{データ}]
        ''' </summary>
        ''' <remarks></remarks>
        SendFile
        ''' <summary>
        ''' ファイルの送信を確認
        ''' [Download{[Nothing]}]
        ''' </summary>
        ''' <remarks></remarks>
        SendFileOk
        ''' <summary>
        ''' ファイル送信が完了したことを知らせる
        ''' [Download{[Nothing]}]
        ''' </summary>
        ''' <remarks></remarks>
        SendFileDone

        ''' <summary>
        ''' リストの同期処理
        ''' {[Nothing] [OR] [ファイルリスト(ID + vbcr + ファイルサイズ) + vbcrlf]}
        ''' </summary>
        ''' <remarks></remarks>
        SyncItem
        ''' <summary>
        ''' 自身のバージョン
        ''' {ソフトウェアバージョン} + vbcrlf + {プロトコルバージョン}
        ''' </summary>
        ''' <remarks></remarks>
        MyVersion
        ''' <summary>
        ''' 自身のコメントが変更された事を通知する
        ''' {変更後のコメント}
        ''' </summary>
        ''' <remarks></remarks>
        ChangeComment
        ''' <summary>
        ''' 自身のアカウント名が変更された事を通知する
        ''' {変更後のアカウント名}
        ''' </summary>
        ''' <remarks></remarks>
        ChangeAccountName
        ''' <summary>
        ''' 自身のポート番号が変更された事を通知する
        ''' {変更後のポート番号}
        ''' </summary>
        ''' <remarks></remarks>
        ChangePort

        ''' <summary>
        ''' 指定パスの内容を取得します。
        ''' {取得の種類} [OR] {取得の種類} + vbcrlf + {ID}
        ''' </summary>
        ''' <remarks></remarks>
        Share_GetList
        ''' <summary>
        ''' 指定パスをダウンロードします。
        ''' {ID}
        ''' </summary>
        ''' <remarks></remarks>
        Share_Download
        ''' <summary>
        ''' リストの内容です。
        ''' {ファイルのリスト}
        ''' </summary>
        ''' <remarks></remarks>
        Share_List

        ''' <summary>
        ''' 通信維持用ジャンクデータ
        ''' {[Nothing]}
        ''' </summary>
        ''' <remarks></remarks>
        JUNK
    End Enum

    Public Enum ListenStatus
        ListenError
        [Error]
        Done
    End Enum

#Region "プロパティ"
    ''' <summary>
    ''' 接続状態。Trueなら接続している
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AceptInfo() As Boolean
        Get
            Return ns IsNot Nothing
        End Get
    End Property

    Private _SoftVersion As String
    ''' <summary>
    ''' 通信相手のソフトバージョン
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SoftVersion() As String
        Get
            Return _SoftVersion
        End Get
        Set(ByVal value As String)
            _SoftVersion = value
        End Set
    End Property

    Private _SoftProtocol As Integer
    ''' <summary>
    ''' 通信相手のプロトコルバージョン
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SoftProtocol() As Integer
        Get
            Return _SoftProtocol
        End Get
        Set(ByVal value As Integer)
            _SoftProtocol = value
        End Set
    End Property

    Private _RemoteIp As String
    ''' <summary>
    ''' 接続先IP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RemoteIp() As String
        Get
            Return _RemoteIp
        End Get
    End Property

    Private _RemotePort As Integer
    ''' <summary>
    ''' 接続先ポート
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RemotePort() As Integer
        Get
            Return _RemotePort
        End Get
    End Property

    Private _StartAceptTime As DateTime
    ''' <summary>
    ''' 接続開始の日時
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property StartAceptTime() As DateTime
        Get
            Return _StartAceptTime
        End Get
    End Property

    ''' <summary>
    ''' 通信継続時間
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AceptTime() As String
        Get
            Dim d As TimeSpan = DateTime.Now.Subtract(StartAceptTime)
            Return d.Hours & ":" & d.Minutes & ":" & d.Seconds
        End Get
    End Property

    Private _AllReceiveSize As Long
    ''' <summary>
    ''' 総受信量
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AllReceiveSize() As Long
        Get
            Return _AllReceiveSize
        End Get
    End Property

    Private _AllSendSize As Long
    ''' <summary>
    ''' 総送信量
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AllSendSize() As Long
        Get
            Return _AllSendSize
        End Get
    End Property

    Private _OneReceiveSize As Integer
    ''' <summary>
    ''' 一定間隔で受信した量
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property OneReceiveSize() As Integer
        Get
            Return _OneReceiveSize
        End Get
    End Property

    Private _OneSendSize As Integer
    ''' <summary>
    ''' 一定間隔で送信した量
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property OneSendSize() As Integer
        Get
            Return _OneSendSize
        End Get
    End Property

    Private _SendEncryptDataNum As UInteger
    ''' <summary>
    ''' 暗号化したデータの送信回数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SendEncryptDataNum() As UInteger
        Get
            Return _SendEncryptDataNum
        End Get
    End Property

    Private _ReceiveEncryptDataNum As UInteger
    ''' <summary>
    ''' 暗号化したデータの受信回数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ReceiveEncryptDataNum() As UInteger
        Get
            Return _ReceiveEncryptDataNum
        End Get
    End Property

    Private _UseEncrypt As Boolean
    ''' <summary>
    ''' 送信データを暗号化するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseEncrypt() As Boolean
        Get
            Return _UseEncrypt
        End Get
        Set(ByVal value As Boolean)
            _UseEncrypt = value
        End Set
    End Property
#End Region

    Private _StopListen As Boolean
    ''' <summary>
    ''' ポートのリッスンを中止します
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopListen()
        _StopListen = True
    End Sub

    ''' <summary>
    ''' 受信量と送信量をリセットします
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ResetOneSize()
        _OneReceiveSize = 0
        _OneSendSize = 0
    End Sub

    ''' <summary>
    ''' ネットワークの通信を終了させます
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub NetworkClose()
        Try
            If ns IsNot Nothing Then
                ns.Close()
                ns.Dispose()
            End If
        Catch
        Finally
            ns = Nothing
            _RemoteIp = Nothing
            _StartAceptTime = Nothing
            _AllReceiveSize = 0
            _AllSendSize = 0
            _OneReceiveSize = 0
            _OneSendSize = 0
            _StopListen = False
            If rijndael IsNot Nothing Then
                rijndael.Clear()
            End If
            rijndael = Nothing
            _SendEncryptDataNum = 0
            _ReceiveEncryptDataNum = 0
        End Try
    End Sub

    ''' <summary>
    ''' ログインします
    ''' </summary>
    ''' <param name="passwd">パスワード</param>
    ''' <param name="mode">モード</param>
    ''' <param name="UseEncrypt">その後の通信を暗号化するか</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Login(ByVal passwd As String, ByVal mode As LoginServiceClass.ServiceMode, ByVal UseEncrypt As Boolean) As Boolean
        '暗号化状態をセットする
        _UseEncrypt = UseEncrypt

        If Not AceptInfo Then
            Return False
        End If

        Dim lgs As New LoginServiceClass
        Dim retbytes() As Byte = lgs.LoginAccount(mode, passwd, ns)

        '認証に失敗したら
        If retbytes Is Nothing OrElse retbytes.Length = 0 Then
            NetworkClose()
            Return False
        End If

        '認証に成功したら
        SetEncrypt(retbytes)
        Return True
    End Function

    ''' <summary>
    ''' サーバに接続します
    ''' </summary>
    ''' <param name="HostName">サーバIP</param>
    ''' <param name="HostPort">ポート番号</param>
    ''' <param name="accountname">送信するアカウント名</param>
    ''' <param name="ShowError">エラーを表示するか</param>
    ''' <remarks></remarks>
    Public Sub ConnectionNetwork(ByVal HostName As String, ByVal HostPort As Integer, ByVal accountname As String, ByVal ShowError As Boolean)
        Try
            If accountname Is Nothing OrElse accountname.Length = 0 Then
                Throw New Exception("Invalid AccountName")
            End If

            Dim tcp_ns As New System.Net.Sockets.TcpClient(HostName, HostPort)
            'バッファの調整
            tcp_ns.ReceiveBufferSize = Transport_ConnectBufferSize
            tcp_ns.SendBufferSize = Transport_ConnectBufferSize
            tcp_ns.Client.ReceiveBufferSize = Transport_ConnectBufferSize
            tcp_ns.Client.SendBufferSize = Transport_ConnectBufferSize
            'バッファの調整完了
            _RemoteIp = CType(tcp_ns.Client.RemoteEndPoint, Net.IPEndPoint).Address.ToString
            _RemotePort = CType(tcp_ns.Client.RemoteEndPoint, Net.IPEndPoint).Port
            _StartAceptTime = DateTime.Now

            ns = tcp_ns.GetStream

            'アカウント名の送信
            Dim sendBytes() As Byte = GetSendData(enc.GetBytes(accountname))
            ns.Write(sendBytes, 0, sendBytes.Length)
        Catch ex As Exception
            NetworkClose()
        End Try
    End Sub

    ''' <summary>
    ''' ポートをリッスンし、接続要求が来るのを待ちます
    ''' </summary>
    ''' <param name="port">Listen対象のポート</param>
    ''' <param name="accountname">相手のアカウント名</param>
    ''' <remarks></remarks>
    Public Function ListenPort(ByVal port As Integer, ByRef accountname As String) As ListenStatus
        Dim listener As New System.Net.Sockets.TcpListener(System.Net.IPAddress.Any, port)
        Try
            'リッスンを開始
            Try
                listener.Start()
            Catch ex As Exception
                Return ListenStatus.ListenError
            End Try

            '接続を開始
            Do
                If _StopListen Then
                    Throw New Exception("Connect was cancelled")
                End If
                System.Threading.Thread.Sleep(100)
            Loop Until listener.Pending

            Dim tcp_ns As System.Net.Sockets.TcpClient = listener.AcceptTcpClient
            'バッファの調整
            tcp_ns.ReceiveBufferSize = Transport_ConnectBufferSize
            tcp_ns.SendBufferSize = Transport_ConnectBufferSize
            tcp_ns.Client.SendBufferSize = Transport_ConnectBufferSize
            tcp_ns.Client.ReceiveBufferSize = Transport_ConnectBufferSize
            'バッファの調整完了
            _RemoteIp = CType(tcp_ns.Client.RemoteEndPoint, System.Net.IPEndPoint).Address.ToString
            _RemotePort = CType(tcp_ns.Client.RemoteEndPoint, System.Net.IPEndPoint).Port
            _StartAceptTime = DateTime.Now

            ns = tcp_ns.GetStream

            'アカウントの受信
            Dim buf() As Byte = RecData()
            If buf.Length <= 0 Then
                Throw New Exception("Connect close")
            End If
            accountname = enc.GetString(buf, 0, buf.Length)
            Return ListenStatus.Done
        Catch ex As Exception
            NetworkClose()
            Return ListenStatus.Error
        Finally
            If listener IsNot Nothing Then
                listener.Stop()
            End If
            _StopListen = False
        End Try
    End Function

    ''' <summary>
    ''' パケットを送信します
    ''' </summary>
    ''' <param name="buf">バッファ</param>
    ''' <param name="header">ヘッダ</param>
    ''' <param name="File_ID">ファイルID</param>
    ''' <remarks></remarks>
    Public Sub SendPacket(ByVal buf() As Byte, ByVal header As TCP_Header, ByVal File_ID As UShort)
        Dim sendBytes As New List(Of Byte) 'Protcol{1Byte} + UseEncrypt(1Byte) + ID{2Byte} + PacketSize{4Byte} + Cache.Length{unknown}
        Try
            sendBytes.Add(header) 'ヘッダを格納
            sendBytes.Add(CByte(_UseEncrypt)) '暗号化情報、0ならFalse、それ以外はTrue
            sendBytes.AddRange(System.BitConverter.GetBytes(File_ID)) 'ファイルID格納
            If buf IsNot Nothing AndAlso buf.Length > 0 Then
                If UseEncrypt Then
                    Dim encBytes() As Byte = EncryptData(buf)
                    sendBytes.AddRange(System.BitConverter.GetBytes(encBytes.Length)) '暗号化後のファイルサイズの格納
                    sendBytes.AddRange(encBytes) '暗号化後の本体格納

                    '暗号化されたデータの送信回数
                    _SendEncryptDataNum = CUInt(_SendEncryptDataNum + 1)
                Else
                    sendBytes.AddRange(System.BitConverter.GetBytes(buf.Length)) 'ファイルサイズ格納
                    sendBytes.AddRange(buf) '本体格納
                End If
            Else
                sendBytes.AddRange(New Byte() {0, 0, 0, 0}) 'ファイルサイズ0を格納する
            End If

            '送信する
            ns.Write(sendBytes.ToArray, 0, sendBytes.Count)

            '送信サイズに加算
            _AllSendSize += CLng(sendBytes.Count)
            _OneSendSize += sendBytes.Count
        Catch ex As Exception
            NetworkClose()
        End Try
    End Sub

    ''' <summary>
    ''' パケットを送信します
    ''' </summary>
    ''' <param name="msg">送信する文字列</param>
    ''' <param name="header">ヘッダ</param>
    ''' <param name="File_ID">ファイルID</param>
    ''' <remarks></remarks>
    Public Sub SendPacket(ByVal msg As String, ByVal header As TCP_Header, ByVal File_ID As UShort)
        SendPacket(enc.GetBytes(msg), header, File_ID)
    End Sub

    ''' <summary>
    ''' データを受信します
    ''' </summary>
    ''' <param name="buf">読み取りバッファ</param>
    ''' <param name="File_ID">ファイルID</param>
    ''' <param name="header">ヘッダ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReceiveData(ByRef buf() As Byte, ByRef File_ID As UShort, ByRef header As TCP_Header) As Boolean
        Dim firstRec(7) As Byte 'Protcol{1Byte} + UseEncrypt(1Byte) + ID{2Byte} + PacketSize{4Byte} + Cache.Length{unknown}
        Dim recNS As New IO.MemoryStream

        Try
            'ヘッダ受信
            Dim RecSize As Integer = ns.Read(firstRec, 0, firstRec.Length)
            '切断されてないかチェック
            If RecSize <= 0 Then
                Throw New Exception("Connect close")
            End If
            'プロトコルヘッダ
            header = CType(firstRec(0), TCP_Header)
            '暗号化状況
            Dim encrypt As Boolean = CBool(firstRec(1))
            'ファイルID受信
            File_ID = System.BitConverter.ToUInt16(firstRec, 2)
            '本データサイズ
            Dim header_Size As Integer = System.BitConverter.ToInt32(firstRec, 4)

            '本データ受信のためのバッファサイズ決定
            Dim bufSize As Integer = header_Size \ 2
            If bufSize > Transport_RecBufferSize_Max Then
                bufSize = Transport_RecBufferSize_Max
            ElseIf bufSize < Transport_RecBufferSize_Min Then
                bufSize = Transport_RecBufferSize_Min
            End If

            '本データ受信のバッファー
            Dim resBytes(bufSize) As Byte

            'ペイロード受信開始
            If header_Size < 0 Then
                Throw New Exception("Invalid header")
            ElseIf header_Size > 0 Then
                '本データ部分の受信
                Dim AllRecSize As Integer = 0
                Dim reminSize As Integer = 0
                Do
                    reminSize = header_Size - AllRecSize  '受信する残りのサイズ
                    If reminSize > resBytes.Length Then
                        reminSize = resBytes.Length
                    End If
                    RecSize = ns.Read(resBytes, 0, reminSize)
                    If RecSize <= 0 Then
                        Throw New Exception("Connect close")
                    End If
                    'データを貯める
                    recNS.Write(resBytes, 0, RecSize)
                    AllRecSize += RecSize
                Loop While (AllRecSize < header_Size)

                '受信サイズ更新
                _AllReceiveSize += CLng(AllRecSize + firstRec.Length)
                _OneReceiveSize += (AllRecSize + firstRec.Length)

                If encrypt Then
                    '復号化する
                    buf = DecryptData(recNS.ToArray)

                    '暗号化データ受信回数
                    _ReceiveEncryptDataNum = CUInt(_ReceiveEncryptDataNum + 1)
                Else
                    buf = recNS.ToArray
                End If
            Else
                'ペイロードなしと判定
                buf = New Byte() {}
            End If
            Return True
        Catch ex As Exception
            NetworkClose()
            Return False
        Finally
            recNS.Close()
            recNS.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' データを暗号化します
    ''' </summary>
    ''' <param name="encBytes">暗号化対象のデータ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EncryptData(ByVal encBytes() As Byte) As Byte()
        If encBytes IsNot Nothing AndAlso encBytes.Length > 0 Then
            Dim msOut As New System.IO.MemoryStream
            Dim cryptStreem As System.Security.Cryptography.CryptoStream = Nothing
            Try
                cryptStreem = New System.Security.Cryptography.CryptoStream(msOut, rijndael.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
                '書き込む
                cryptStreem.Write(encBytes, 0, encBytes.Length)
                cryptStreem.FlushFinalBlock()
                Return msOut.ToArray
            Finally
                msOut.Close()
                msOut.Dispose()
                If cryptStreem IsNot Nothing Then
                    cryptStreem.Close()
                    cryptStreem.Dispose()
                End If
            End Try
        End If
        Return New Byte() {}
    End Function
    ''' <summary>
    ''' データを復号化します
    ''' </summary>
    ''' <param name="decBytes">復号化対象のデータ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DecryptData(ByVal decBytes() As Byte) As Byte()
        If decBytes IsNot Nothing AndAlso decBytes.Length > 0 Then
            Dim msIn As System.IO.MemoryStream = Nothing
            Dim cryptStreem As System.Security.Cryptography.CryptoStream = Nothing
            Dim retMS As IO.MemoryStream = Nothing
            Try
                '読み込みオブジェクトの作成
                msIn = New System.IO.MemoryStream(decBytes, 0, decBytes.Length)
                '復号化オブジェクトの作成
                cryptStreem = New System.Security.Cryptography.CryptoStream(msIn, rijndael.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Read)
                'データを読み取る
                Dim bs(262143) As Byte
                Dim readLen As Integer
                retMS = New IO.MemoryStream
                Do
                    readLen = cryptStreem.Read(bs, 0, bs.Length)
                    If readLen > 0 Then
                        retMS.Write(bs, 0, readLen)
                    End If
                Loop While (readLen > 0)
                Return retMS.ToArray
            Finally
                '開放
                If msIn IsNot Nothing Then
                    msIn.Close()
                    msIn.Dispose()
                End If
                If retMS IsNot Nothing Then
                    retMS.Close()
                    retMS.Dispose()
                End If
                If cryptStreem IsNot Nothing Then
                    cryptStreem.Close()
                    cryptStreem.Dispose()
                End If
            End Try
        End If
        Return New Byte() {}
    End Function
    ''' <summary>
    ''' 暗号化インスタンスをセットする
    ''' </summary>
    ''' <param name="key">暗号化キー</param>
    ''' <remarks></remarks>
    Public Sub SetEncrypt(ByVal key() As Byte)
        If rijndael IsNot Nothing Then
            rijndael.Clear()
            rijndael = Nothing
        End If

        rijndael = New System.Security.Cryptography.RijndaelManaged
        rijndael.KeySize = 256
        rijndael.BlockSize = 128
        rijndael.FeedbackSize = 128
        rijndael.Mode = Security.Cryptography.CipherMode.CBC
        rijndael.Padding = Security.Cryptography.PaddingMode.PKCS7

        'パスワードから共有キーと初期化ベクタを作る
        Dim salt() As Byte = enc.GetBytes("arachlex.exe encryptdata")
        'Rfc2898DeriveBytesオブジェクトを作成する
        Dim deriveBytes As New System.Security.Cryptography.Rfc2898DeriveBytes(key, salt, 1000)
        '共有キーと初期化ベクタを生成する
        rijndael.Key = deriveBytes.GetBytes(256 \ 8)
        rijndael.IV = deriveBytes.GetBytes(128 \ 8)
    End Sub

    ''' <summary>
    ''' 送信するデータを作成します
    ''' </summary>
    ''' <param name="senddata">送信するデータ配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSendData(ByVal senddata As Byte()) As Byte()
        If senddata IsNot Nothing Then
            'PayloadSize{UShort} + Data{payloadsize}
            Dim psize As UShort = CUShort(senddata.Length)
            Dim sData As New List(Of Byte)
            sData.AddRange(System.BitConverter.GetBytes(psize))
            sData.AddRange(senddata)
            Return sData.ToArray
        End If
        Return Nothing
    End Function
    ''' <summary>
    ''' データを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecData() As Byte()
        Try
            Dim pSizeBuffer(1) As Byte
            ns.Read(pSizeBuffer, 0, pSizeBuffer.Length)

            Dim psize As UShort = System.BitConverter.ToUInt16(pSizeBuffer, 0)
            Dim readlen As Integer = 0
            Dim msData As New List(Of Byte)
            Dim buf(255) As Byte
            Do
                Dim reminSize As Integer = psize - readlen
                If reminSize > buf.Length Then
                    reminSize = buf.Length
                End If
                Dim rSize As Integer = ns.Read(buf, 0, reminSize)
                If rSize = 0 Then
                    Return Nothing
                End If
                readlen += rSize
                If buf.Length <> rSize Then
                    Array.Resize(buf, rSize)
                End If
                msData.AddRange(buf)
            Loop Until readlen = psize
            Return msData.ToArray
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal nenc As System.Text.Encoding)
        ns = Nothing
        rijndael = Nothing
        _RemoteIp = Nothing
        _RemotePort = 0
        _SoftVersion = Nothing
        _SoftProtocol = 0
        _StartAceptTime = Nothing
        _AllReceiveSize = 0
        _AllSendSize = 0
        _OneReceiveSize = 0
        _OneSendSize = 0
        _UseEncrypt = False
        _StopListen = False
        _SendEncryptDataNum = 0
        _ReceiveEncryptDataNum = 0

        enc = nenc
    End Sub
End Class