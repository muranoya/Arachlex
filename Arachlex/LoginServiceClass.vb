Public Class LoginServiceClass
    Private AttRj As System.Security.Cryptography.RijndaelManaged
    Private enc As System.Text.Encoding = System.Text.Encoding.UTF8
    Private att_ns As Net.Sockets.NetworkStream = Nothing

    Public Enum ServiceMode As Byte
        Server
        Client
    End Enum

    ''' <summary>
    ''' 接続認証
    ''' </summary>
    ''' <param name="service">認証モード</param> 
    ''' <param name="AKey">認証キー</param>
    '''<param name="ns">認証に使用するネットワークストリーム</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoginAccount(ByVal service As ServiceMode, ByVal AKey As String, ByVal ns As System.Net.Sockets.NetworkStream) As Byte()
        '1.認証キーからSHA256ハッシュを求める
        '2.ラインダールのIVとKeyを認証キーから求めたSHA256ハッシュを使ってセットする。
        '3.クライアントがサーバに付加データを送信する(64Byte)。
        '4.サーバがクライアントに付加データを送信する(64Byte)。
        '5.クライアントがサーバへ、二つの付加データと認証キーを結合したSHA256ハッシュを計算しサーバへ送る。
        'なお、結合は、認証キー + クライアントデータ + サーバデータ順に結合する。
        '6.サーバは自身で計算したものと一致するか調べる。
        '7.サーバは一致が確認されればクライアントへ送る。

        Const ClientHashSize As Integer = 64 'クライアントのハッシュ長、単位はByte
        Const ServerHashSize As Integer = 64 'サーバのハッシュ長、単位はByte
        Const AttComplete As String = "AttOk" '認証完了

        Dim RecBytes(1023) As Byte '受信用のバッファ
        Dim SendBytes() As Byte = New Byte() {} '送信用バッファ
        Dim ClientPartHash As Byte() = New Byte() {} 'クライアントの生成した付加データ
        Dim ServerPartHash As Byte() = New Byte() {} 'サーバの生成した付加データ
        Dim ClientHash As String = "" 'クライアントのハッシュデータ
        Dim ServerHash As String = "" 'サーバのハッシュデータ
        Dim sha512 As New System.Security.Cryptography.SHA512Managed

        att_ns = ns

        Try
            'ブロックサイズとキーサイズ
            Const KeySizeDef As Integer = 256
            Const BlockSizeDef As Integer = 128
            '暗号化に使用する認証キーを設定する
            AttRj = New System.Security.Cryptography.RijndaelManaged
            AttRj.KeySize = KeySizeDef
            AttRj.BlockSize = BlockSizeDef
            AttRj.FeedbackSize = 128
            AttRj.Mode = System.Security.Cryptography.CipherMode.CBC
            AttRj.Padding = System.Security.Cryptography.PaddingMode.PKCS7
            Dim rij_Key As Byte() = New Byte() {0} '警告を消すため、長さ1の配列で初期化しています
            Dim rij_IV As Byte() = New Byte() {0} '警告を消すため、長さ1の配列で初期化しています
            Dim gPW As Byte() = sha512.ComputeHash(enc.GetBytes(AKey))
            GenerateKeyFromPassword(gPW, KeySizeDef, rij_Key, BlockSizeDef, rij_IV)
            AttRj.Key = rij_Key
            AttRj.IV = rij_IV

            '認証の結果得たデータを格納するByte配列。クライアントハッシュが先に入る。その後サーバハッシュを入れる。
            Dim retBytes(ClientHashSize + ServerHashSize - 1) As Byte

            If service = ServiceMode.Server Then 'サーバー時
                'クライアント側付加データの受信
                RecBytes = RecData()
                ClientPartHash = DecryptBytes(RecBytes)

                '戻り値の生成、クライアントハッシュの格納
                Array.Copy(ClientPartHash, 0, retBytes, 0, ClientPartHash.Length)

                '付加データのチェック
                If ClientPartHash Is Nothing OrElse ClientPartHash.Length < ClientHashSize Then
                    Throw New Exception("不正なクライアントからのハッシュキー入力です")
                End If

                '自身の付加データを生成
                ServerPartHash = GetRandomBytes(ServerHashSize)

                '戻り値の生成、サーバハッシュの格納
                Array.Copy(ServerPartHash, 0, retBytes, ClientHashSize, ServerPartHash.Length)

                '自身の付加データを送信
                SendBytes = EncryptBytes(ServerPartHash)
                SendBytes = GetSendData(SendBytes)
                att_ns.Write(SendBytes, 0, SendBytes.Length)

                'クライアントの作ったSHA256ハッシュを待つ
                RecBytes = RecData()
                ClientHash = enc.GetString(DecryptBytes(RecBytes))

                'SHA256ハッシュを生成する
                ServerHash = GetHash(ClientPartHash, ServerPartHash, enc.GetBytes(AKey), sha512)

                'ハッシュの一致を確認
                If ServerHash Is Nothing OrElse ServerHash.Length = 0 OrElse _
                ClientHash Is Nothing OrElse ClientHash.Length = 0 OrElse _
                Not ClientHash.Equals(ServerHash, StringComparison.OrdinalIgnoreCase) Then
                    Throw New Exception("ハッシュが一致しません。")
                End If

                '完了の通知
                SendBytes = EncryptBytes(enc.GetBytes(AttComplete))
                SendBytes = GetSendData(SendBytes)
                att_ns.Write(SendBytes, 0, SendBytes.Length)

                '無事認証完了、生成鍵を返す
                Return retBytes
            Else
                'クライアント側の付加データを送信
                ClientPartHash = GetRandomBytes(ClientHashSize)

                '戻り値の生成、クライアントハッシュの格納
                Array.Copy(ClientPartHash, 0, retBytes, 0, ClientPartHash.Length)

                '暗号化してサーバに送信する
                SendBytes = EncryptBytes(ClientPartHash)
                SendBytes = GetSendData(SendBytes)
                att_ns.Write(SendBytes, 0, SendBytes.Length)

                'サーバからの付加データ待ち
                RecBytes = RecData()
                ServerPartHash = DecryptBytes(RecBytes)

                '付加データチェック
                If ServerPartHash Is Nothing OrElse ServerPartHash.Length < ServerHashSize Then
                    Throw New Exception("不正なサーバからのハッシュキー入力です")
                End If

                '戻り値の生成、サーバハッシュの格納
                Array.Copy(ServerPartHash, 0, retBytes, ClientHashSize, ServerPartHash.Length)

                'SHA256ハッシュを生成する
                ClientHash = GetHash(ClientPartHash, ServerPartHash, enc.GetBytes(AKey), sha512)

                'サーバへハッシュを送信する
                SendBytes = EncryptBytes(enc.GetBytes(ClientHash))
                SendBytes = GetSendData(SendBytes)
                att_ns.Write(SendBytes, 0, SendBytes.Length)

                '問題なかったか、サーバからの返答を待つ
                RecBytes = RecData()
                Dim recstr As String = enc.GetString(DecryptBytes(RecBytes))
                If Not AttComplete.Equals(recstr, StringComparison.OrdinalIgnoreCase) Then
                    Throw New Exception("認証に失敗しました")
                End If

                '認証完了、生成鍵を返す
                Return retBytes
            End If
        Catch
            Return Nothing
        Finally
            sha512.Clear()
            AttRj.Clear()
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' パスワードから共有キーと初期化ベクタを生成する
    ''' </summary>
    ''' <param name="password">基になるパスワード</param>
    ''' <param name="keySize">共有キーのサイズ（ビット）</param>
    ''' <param name="key">作成された共有キー</param>
    ''' <param name="blockSize">初期化ベクタのサイズ（ビット）</param>
    ''' <param name="iv">作成された初期化ベクタ</param>
    Private Sub GenerateKeyFromPassword(ByVal password As Byte(), ByVal keySize As Integer, ByRef key As Byte(), ByVal blockSize As Integer, ByRef iv As Byte())
        'パスワードから共有キーと初期化ベクタを作る
        Dim salt() As Byte = enc.GetBytes("arachlex.exe login")
        'Rfc2898DeriveBytesオブジェクトを作成する
        Dim deriveBytes As New System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, 2000)

        '共有キーと初期化ベクタを生成する
        key = deriveBytes.GetBytes(keySize \ 8)
        iv = deriveBytes.GetBytes(blockSize \ 8)
    End Sub
    ''' <summary>
    ''' 認証用ハッシュを調べます
    ''' </summary>
    ''' <param name="clientBytes">クライアントのキー</param>
    ''' <param name="serverBytes">サーバのキー</param>
    ''' <param name="keyBytes">認証キー</param>
    ''' <param name="hash">計算にしようするSHA-512インスタント</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetHash(ByVal clientBytes() As Byte, ByVal serverBytes() As Byte, ByVal keyBytes() As Byte, ByVal hash As System.Security.Cryptography.SHA512Managed) As String
        Try
            Dim hashBytes As New List(Of Byte)
            hashBytes.AddRange(keyBytes)
            hashBytes.AddRange(clientBytes)
            hashBytes.AddRange(serverBytes)
            Return BitConverter.ToString(hash.ComputeHash(hashBytes.ToArray)).ToLower().Replace("-", "")
        Catch
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' ランダムなバイト配列を取得します
    ''' </summary>
    ''' <param name="len">取得するバイト配列の長さ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRandomBytes(ByVal len As Integer) As Byte()
        Dim bytes(len - 1) As Byte
        Dim newRandom As New System.Security.Cryptography.RNGCryptoServiceProvider
        newRandom.GetBytes(bytes)
        Return bytes
    End Function
    ''' <summary>
    ''' 文字列の暗号化関数
    ''' </summary>
    ''' <param name="Bytes">暗号化するバイト配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EncryptBytes(ByVal Bytes() As Byte) As Byte()
        Dim msOut As New System.IO.MemoryStream
        Dim cryptStreem As System.Security.Cryptography.CryptoStream = Nothing
        Try
            cryptStreem = New System.Security.Cryptography.CryptoStream(msOut, AttRj.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
            '書き込む
            cryptStreem.Write(Bytes, 0, Bytes.Length)
            cryptStreem.FlushFinalBlock()
            Return msOut.ToArray
        Catch
            Return Nothing
        Finally
            msOut.Close()
            msOut.Dispose()
            If cryptStreem IsNot Nothing Then
                cryptStreem.Close()
                cryptStreem.Dispose()
            End If
        End Try
    End Function
    ''' <summary>
    ''' 文字列の復号化関数
    ''' </summary>
    ''' <param name="bytes">復号化対象の暗号化されたバイト配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DecryptBytes(ByVal bytes() As Byte) As Byte()
        Dim msIn As System.IO.MemoryStream = Nothing
        Dim cryptStreem As System.Security.Cryptography.CryptoStream = Nothing
        Dim retMS As IO.MemoryStream = Nothing
        Try
            '読み込みオブジェクトの作成
            msIn = New System.IO.MemoryStream(bytes, 0, bytes.Length)
            '復号化オブジェクトの作成
            cryptStreem = New System.Security.Cryptography.CryptoStream(msIn, AttRj.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Read)
            'データを読み取る
            Dim bs(123) As Byte
            Dim readLen As Integer
            retMS = New IO.MemoryStream
            Do
                readLen = cryptStreem.Read(bs, 0, bs.Length)
                If readLen > 0 Then
                    retMS.Write(bs, 0, readLen)
                End If
            Loop While (readLen > 0)
            Return retMS.ToArray
        Catch
            Return Nothing
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
    End Function
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
            att_ns.Read(pSizeBuffer, 0, pSizeBuffer.Length)

            Dim psize As UShort = System.BitConverter.ToUInt16(pSizeBuffer, 0)
            Dim readlen As Integer = 0
            Dim msData As New List(Of Byte)
            Dim buf(255) As Byte
            Do
                Dim reminSize As Integer = psize - readlen
                If reminSize > buf.Length Then
                    reminSize = buf.Length
                End If
                Dim rSize As Integer = att_ns.Read(buf, 0, reminSize)
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
End Class