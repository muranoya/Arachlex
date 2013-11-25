Public Class LoginServiceClass
    Private AttRj As System.Security.Cryptography.RijndaelManaged
    Private enc As System.Text.Encoding = System.Text.Encoding.UTF8
    Private att_ns As Net.Sockets.NetworkStream = Nothing

    Public Enum ServiceMode As Byte
        Server
        Client
    End Enum

    ''' <summary>
    ''' �ڑ��F��
    ''' </summary>
    ''' <param name="service">�F�؃��[�h</param> 
    ''' <param name="AKey">�F�؃L�[</param>
    '''<param name="ns">�F�؂Ɏg�p����l�b�g���[�N�X�g���[��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoginAccount(ByVal service As ServiceMode, ByVal AKey As String, ByVal ns As System.Net.Sockets.NetworkStream) As Byte()
        '1.�F�؃L�[����SHA256�n�b�V�������߂�
        '2.���C���_�[����IV��Key��F�؃L�[���狁�߂�SHA256�n�b�V�����g���ăZ�b�g����B
        '3.�N���C�A���g���T�[�o�ɕt���f�[�^�𑗐M����(64Byte)�B
        '4.�T�[�o���N���C�A���g�ɕt���f�[�^�𑗐M����(64Byte)�B
        '5.�N���C�A���g���T�[�o�ցA��̕t���f�[�^�ƔF�؃L�[����������SHA256�n�b�V�����v�Z���T�[�o�֑���B
        '�Ȃ��A�����́A�F�؃L�[ + �N���C�A���g�f�[�^ + �T�[�o�f�[�^���Ɍ�������B
        '6.�T�[�o�͎��g�Ōv�Z�������̂ƈ�v���邩���ׂ�B
        '7.�T�[�o�͈�v���m�F�����΃N���C�A���g�֑���B

        Const ClientHashSize As Integer = 64 '�N���C�A���g�̃n�b�V�����A�P�ʂ�Byte
        Const ServerHashSize As Integer = 64 '�T�[�o�̃n�b�V�����A�P�ʂ�Byte
        Const AttComplete As String = "AttOk" '�F�؊���

        Dim RecBytes(1023) As Byte '��M�p�̃o�b�t�@
        Dim SendBytes() As Byte = New Byte() {} '���M�p�o�b�t�@
        Dim ClientPartHash As Byte() = New Byte() {} '�N���C�A���g�̐��������t���f�[�^
        Dim ServerPartHash As Byte() = New Byte() {} '�T�[�o�̐��������t���f�[�^
        Dim ClientHash As String = "" '�N���C�A���g�̃n�b�V���f�[�^
        Dim ServerHash As String = "" '�T�[�o�̃n�b�V���f�[�^
        Dim sha512 As New System.Security.Cryptography.SHA512Managed

        att_ns = ns

        Try
            '�u���b�N�T�C�Y�ƃL�[�T�C�Y
            Const KeySizeDef As Integer = 256
            Const BlockSizeDef As Integer = 128
            '�Í����Ɏg�p����F�؃L�[��ݒ肷��
            AttRj = New System.Security.Cryptography.RijndaelManaged
            AttRj.KeySize = KeySizeDef
            AttRj.BlockSize = BlockSizeDef
            AttRj.FeedbackSize = 128
            AttRj.Mode = System.Security.Cryptography.CipherMode.CBC
            AttRj.Padding = System.Security.Cryptography.PaddingMode.PKCS7
            Dim rij_Key As Byte() = New Byte() {0} '�x�����������߁A����1�̔z��ŏ��������Ă��܂�
            Dim rij_IV As Byte() = New Byte() {0} '�x�����������߁A����1�̔z��ŏ��������Ă��܂�
            Dim gPW As Byte() = sha512.ComputeHash(enc.GetBytes(AKey))
            GenerateKeyFromPassword(gPW, KeySizeDef, rij_Key, BlockSizeDef, rij_IV)
            AttRj.Key = rij_Key
            AttRj.IV = rij_IV

            '�F�؂̌��ʓ����f�[�^���i�[����Byte�z��B�N���C�A���g�n�b�V������ɓ���B���̌�T�[�o�n�b�V��������B
            Dim retBytes(ClientHashSize + ServerHashSize - 1) As Byte

            If service = ServiceMode.Server Then '�T�[�o�[��
                '�N���C�A���g���t���f�[�^�̎�M
                RecBytes = RecData()
                ClientPartHash = DecryptBytes(RecBytes)

                '�߂�l�̐����A�N���C�A���g�n�b�V���̊i�[
                Array.Copy(ClientPartHash, 0, retBytes, 0, ClientPartHash.Length)

                '�t���f�[�^�̃`�F�b�N
                If ClientPartHash Is Nothing OrElse ClientPartHash.Length < ClientHashSize Then
                    Throw New Exception("�s���ȃN���C�A���g����̃n�b�V���L�[���͂ł�")
                End If

                '���g�̕t���f�[�^�𐶐�
                ServerPartHash = GetRandomBytes(ServerHashSize)

                '�߂�l�̐����A�T�[�o�n�b�V���̊i�[
                Array.Copy(ServerPartHash, 0, retBytes, ClientHashSize, ServerPartHash.Length)

                '���g�̕t���f�[�^�𑗐M
                SendBytes = EncryptBytes(ServerPartHash)
                SendBytes = GetSendData(SendBytes)
                att_ns.Write(SendBytes, 0, SendBytes.Length)

                '�N���C�A���g�̍����SHA256�n�b�V����҂�
                RecBytes = RecData()
                ClientHash = enc.GetString(DecryptBytes(RecBytes))

                'SHA256�n�b�V���𐶐�����
                ServerHash = GetHash(ClientPartHash, ServerPartHash, enc.GetBytes(AKey), sha512)

                '�n�b�V���̈�v���m�F
                If ServerHash Is Nothing OrElse ServerHash.Length = 0 OrElse _
                ClientHash Is Nothing OrElse ClientHash.Length = 0 OrElse _
                Not ClientHash.Equals(ServerHash, StringComparison.OrdinalIgnoreCase) Then
                    Throw New Exception("�n�b�V������v���܂���B")
                End If

                '�����̒ʒm
                SendBytes = EncryptBytes(enc.GetBytes(AttComplete))
                SendBytes = GetSendData(SendBytes)
                att_ns.Write(SendBytes, 0, SendBytes.Length)

                '�����F�؊����A��������Ԃ�
                Return retBytes
            Else
                '�N���C�A���g���̕t���f�[�^�𑗐M
                ClientPartHash = GetRandomBytes(ClientHashSize)

                '�߂�l�̐����A�N���C�A���g�n�b�V���̊i�[
                Array.Copy(ClientPartHash, 0, retBytes, 0, ClientPartHash.Length)

                '�Í������ăT�[�o�ɑ��M����
                SendBytes = EncryptBytes(ClientPartHash)
                SendBytes = GetSendData(SendBytes)
                att_ns.Write(SendBytes, 0, SendBytes.Length)

                '�T�[�o����̕t���f�[�^�҂�
                RecBytes = RecData()
                ServerPartHash = DecryptBytes(RecBytes)

                '�t���f�[�^�`�F�b�N
                If ServerPartHash Is Nothing OrElse ServerPartHash.Length < ServerHashSize Then
                    Throw New Exception("�s���ȃT�[�o����̃n�b�V���L�[���͂ł�")
                End If

                '�߂�l�̐����A�T�[�o�n�b�V���̊i�[
                Array.Copy(ServerPartHash, 0, retBytes, ClientHashSize, ServerPartHash.Length)

                'SHA256�n�b�V���𐶐�����
                ClientHash = GetHash(ClientPartHash, ServerPartHash, enc.GetBytes(AKey), sha512)

                '�T�[�o�փn�b�V���𑗐M����
                SendBytes = EncryptBytes(enc.GetBytes(ClientHash))
                SendBytes = GetSendData(SendBytes)
                att_ns.Write(SendBytes, 0, SendBytes.Length)

                '���Ȃ��������A�T�[�o����̕ԓ���҂�
                RecBytes = RecData()
                Dim recstr As String = enc.GetString(DecryptBytes(RecBytes))
                If Not AttComplete.Equals(recstr, StringComparison.OrdinalIgnoreCase) Then
                    Throw New Exception("�F�؂Ɏ��s���܂���")
                End If

                '�F�؊����A��������Ԃ�
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
    ''' �p�X���[�h���狤�L�L�[�Ə������x�N�^�𐶐�����
    ''' </summary>
    ''' <param name="password">��ɂȂ�p�X���[�h</param>
    ''' <param name="keySize">���L�L�[�̃T�C�Y�i�r�b�g�j</param>
    ''' <param name="key">�쐬���ꂽ���L�L�[</param>
    ''' <param name="blockSize">�������x�N�^�̃T�C�Y�i�r�b�g�j</param>
    ''' <param name="iv">�쐬���ꂽ�������x�N�^</param>
    Private Sub GenerateKeyFromPassword(ByVal password As Byte(), ByVal keySize As Integer, ByRef key As Byte(), ByVal blockSize As Integer, ByRef iv As Byte())
        '�p�X���[�h���狤�L�L�[�Ə������x�N�^�����
        Dim salt() As Byte = enc.GetBytes("arachlex.exe login")
        'Rfc2898DeriveBytes�I�u�W�F�N�g���쐬����
        Dim deriveBytes As New System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, 2000)

        '���L�L�[�Ə������x�N�^�𐶐�����
        key = deriveBytes.GetBytes(keySize \ 8)
        iv = deriveBytes.GetBytes(blockSize \ 8)
    End Sub
    ''' <summary>
    ''' �F�ؗp�n�b�V���𒲂ׂ܂�
    ''' </summary>
    ''' <param name="clientBytes">�N���C�A���g�̃L�[</param>
    ''' <param name="serverBytes">�T�[�o�̃L�[</param>
    ''' <param name="keyBytes">�F�؃L�[</param>
    ''' <param name="hash">�v�Z�ɂ��悤����SHA-512�C���X�^���g</param>
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
    ''' �����_���ȃo�C�g�z����擾���܂�
    ''' </summary>
    ''' <param name="len">�擾����o�C�g�z��̒���</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRandomBytes(ByVal len As Integer) As Byte()
        Dim bytes(len - 1) As Byte
        Dim newRandom As New System.Security.Cryptography.RNGCryptoServiceProvider
        newRandom.GetBytes(bytes)
        Return bytes
    End Function
    ''' <summary>
    ''' ������̈Í����֐�
    ''' </summary>
    ''' <param name="Bytes">�Í�������o�C�g�z��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EncryptBytes(ByVal Bytes() As Byte) As Byte()
        Dim msOut As New System.IO.MemoryStream
        Dim cryptStreem As System.Security.Cryptography.CryptoStream = Nothing
        Try
            cryptStreem = New System.Security.Cryptography.CryptoStream(msOut, AttRj.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
            '��������
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
    ''' ������̕������֐�
    ''' </summary>
    ''' <param name="bytes">�������Ώۂ̈Í������ꂽ�o�C�g�z��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DecryptBytes(ByVal bytes() As Byte) As Byte()
        Dim msIn As System.IO.MemoryStream = Nothing
        Dim cryptStreem As System.Security.Cryptography.CryptoStream = Nothing
        Dim retMS As IO.MemoryStream = Nothing
        Try
            '�ǂݍ��݃I�u�W�F�N�g�̍쐬
            msIn = New System.IO.MemoryStream(bytes, 0, bytes.Length)
            '�������I�u�W�F�N�g�̍쐬
            cryptStreem = New System.Security.Cryptography.CryptoStream(msIn, AttRj.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Read)
            '�f�[�^��ǂݎ��
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
            '�J��
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
    ''' ���M����f�[�^���쐬���܂�
    ''' </summary>
    ''' <param name="senddata">���M����f�[�^�z��</param>
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
    ''' �f�[�^���擾���܂�
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