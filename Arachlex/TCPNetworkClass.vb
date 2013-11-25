Public Class TCPNetworkClass

    Private ns As System.Net.Sockets.NetworkStream
    Private rijndael As System.Security.Cryptography.RijndaelManaged

    'TCP�o�b�t�@
    Public Const Transport_ConnectBufferSize As Integer = 524288 '512KB

    '��M�o�b�t�@
    Public Const Transport_RecBufferSize_Max As Integer = 4194304 '4MB
    Public Const Transport_RecBufferSize_Min As Integer = 1024
    Public Const Transport_RecBufferSize_Default As Integer = 65536 '64KB

    Private enc As System.Text.Encoding

    Public Enum TCP_Header As Byte
        '�v���g�R���̓��e
        '{}����̋�؂�B
        '(File)�̓t�@�C���̏ꍇ�A(Folder)�̓t�H���_�̏ꍇ�B�����ꍇ��{}���̈�Ԑ擪�ɏ����B
        '[OR]�͂ǂ��炩�B[Nothing]�͒��g�͑��݂��Ȃ����Ƃ������B
        'Upload�̓A�b�v���[�h�ADownload�̓_�E�����[�h�����瑗�M����閽�߂ł��邱�Ƃ��Ӗ�����B����͕K�v�ȏꍇ��Ԑ擪�ɏ����B
        '�t�H���_�ƃt�@�C���ɂ���āA���g�̈Ӗ����Ⴄ�ꍇ�A[]�ň͂ށB

        ''' <summary>
        ''' �`���b�g���b�Z�[�W
        ''' {�n���h����} + vbcrlf + {���g}
        ''' </summary>
        ''' <remarks></remarks>
        ChatMSG

        ''' <summary>
        ''' �A�b�v���[�h���J�n����
        ''' [Upload{�A�C�e���̎��} +vbcrlf + {Queue} + vbcrlf + {�t�@�C����} + vbcrlf + {[(File)�t�@�C���T�C�Y] , [(Folder)�S�t�@�C����]}]
        ''' </summary>
        ''' <remarks></remarks>
        ItemUpload
        ''' <summary>
        ''' �_�E�����[�h�����F����
        ''' [Download(File){�t�@�C���V�[�N�ʒu}]
        ''' </summary>
        ''' <remarks></remarks>
        ItemApp
        ''' <summary>
        ''' �A�C�e���̓]�����ꎞ��~���܂�
        ''' {[Nothing]}
        ''' </summary>
        ''' <remarks></remarks>
        ItemStop
        ''' <summary>
        ''' �A�C�e���̓]�����ĊJ���܂�
        ''' {[Nothing]}
        ''' </summary>
        ''' <remarks></remarks>
        ItemReStart
        ''' <summary>
        ''' �A�C�e�����폜���܂�
        ''' {[Nothing]}
        ''' </summary>
        ''' <remarks></remarks>
        ItemDelete
        ''' <summary>
        ''' �A�C�e���̗D�揇�ʂ�ύX���܂�
        ''' {�ύX��̗D�揇��}
        ''' </summary>
        ''' <remarks></remarks>
        ItemChangeQueue

        ''' <summary>
        ''' �t�H���_�̍쐬
        ''' [Upload(Folder){�t�@�C�����X�g + vbcrlf}]
        ''' </summary>
        ''' <remarks></remarks>
        MakeFolder
        ''' <summary>
        ''' ���̑��M�t�@�C�����Z�b�g���܂�
        ''' [Upload(Folder){�t�@�C���̑��΃p�X} + vbcrlf + {�t�@�C���T�C�Y} + vbcrlf + {�I���t�@�C����}]
        ''' </summary>
        ''' <remarks></remarks>
        NextFolderFile
        ''' <summary>
        ''' �_�E�����[�h���̃Z�b�g����������
        ''' [Download(Folder){[Nothing]}]
        ''' </summary>
        ''' <remarks></remarks>
        NextFolderFileOk
        ''' <summary>
        ''' �t�H���_�̓]������
        ''' [Upload(Folder){[Nothing]}]
        ''' </summary>
        ''' <remarks></remarks>
        FolderDone

        ''' <summary>
        ''' �A�b�v���[�h���̃V�[�N�ʒu�𒲐�����
        ''' [Download{�t�@�C���̃V�[�N�ʒu}]
        ''' </summary>
        ''' <remarks></remarks>
        SetSeek

        ''' <summary>
        ''' �t�@�C�����M
        ''' [Upload{�f�[�^}]
        ''' </summary>
        ''' <remarks></remarks>
        SendFile
        ''' <summary>
        ''' �t�@�C���̑��M���m�F
        ''' [Download{[Nothing]}]
        ''' </summary>
        ''' <remarks></remarks>
        SendFileOk
        ''' <summary>
        ''' �t�@�C�����M�������������Ƃ�m�点��
        ''' [Download{[Nothing]}]
        ''' </summary>
        ''' <remarks></remarks>
        SendFileDone

        ''' <summary>
        ''' ���X�g�̓�������
        ''' {[Nothing] [OR] [�t�@�C�����X�g(ID + vbcr + �t�@�C���T�C�Y) + vbcrlf]}
        ''' </summary>
        ''' <remarks></remarks>
        SyncItem
        ''' <summary>
        ''' ���g�̃o�[�W����
        ''' {�\�t�g�E�F�A�o�[�W����} + vbcrlf + {�v���g�R���o�[�W����}
        ''' </summary>
        ''' <remarks></remarks>
        MyVersion
        ''' <summary>
        ''' ���g�̃R�����g���ύX���ꂽ����ʒm����
        ''' {�ύX��̃R�����g}
        ''' </summary>
        ''' <remarks></remarks>
        ChangeComment
        ''' <summary>
        ''' ���g�̃A�J�E���g�����ύX���ꂽ����ʒm����
        ''' {�ύX��̃A�J�E���g��}
        ''' </summary>
        ''' <remarks></remarks>
        ChangeAccountName
        ''' <summary>
        ''' ���g�̃|�[�g�ԍ����ύX���ꂽ����ʒm����
        ''' {�ύX��̃|�[�g�ԍ�}
        ''' </summary>
        ''' <remarks></remarks>
        ChangePort

        ''' <summary>
        ''' �w��p�X�̓��e���擾���܂��B
        ''' {�擾�̎��} [OR] {�擾�̎��} + vbcrlf + {ID}
        ''' </summary>
        ''' <remarks></remarks>
        Share_GetList
        ''' <summary>
        ''' �w��p�X���_�E�����[�h���܂��B
        ''' {ID}
        ''' </summary>
        ''' <remarks></remarks>
        Share_Download
        ''' <summary>
        ''' ���X�g�̓��e�ł��B
        ''' {�t�@�C���̃��X�g}
        ''' </summary>
        ''' <remarks></remarks>
        Share_List

        ''' <summary>
        ''' �ʐM�ێ��p�W�����N�f�[�^
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

#Region "�v���p�e�B"
    ''' <summary>
    ''' �ڑ���ԁBTrue�Ȃ�ڑ����Ă���
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
    ''' �ʐM����̃\�t�g�o�[�W����
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
    ''' �ʐM����̃v���g�R���o�[�W����
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
    ''' �ڑ���IP
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
    ''' �ڑ���|�[�g
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
    ''' �ڑ��J�n�̓���
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
    ''' �ʐM�p������
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
    ''' ����M��
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
    ''' �����M��
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
    ''' ���Ԋu�Ŏ�M������
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
    ''' ���Ԋu�ő��M������
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
    ''' �Í��������f�[�^�̑��M��
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
    ''' �Í��������f�[�^�̎�M��
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
    ''' ���M�f�[�^���Í������邩
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
    ''' �|�[�g�̃��b�X���𒆎~���܂�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopListen()
        _StopListen = True
    End Sub

    ''' <summary>
    ''' ��M�ʂƑ��M�ʂ����Z�b�g���܂�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ResetOneSize()
        _OneReceiveSize = 0
        _OneSendSize = 0
    End Sub

    ''' <summary>
    ''' �l�b�g���[�N�̒ʐM���I�������܂�
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
    ''' ���O�C�����܂�
    ''' </summary>
    ''' <param name="passwd">�p�X���[�h</param>
    ''' <param name="mode">���[�h</param>
    ''' <param name="UseEncrypt">���̌�̒ʐM���Í������邩</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Login(ByVal passwd As String, ByVal mode As LoginServiceClass.ServiceMode, ByVal UseEncrypt As Boolean) As Boolean
        '�Í�����Ԃ��Z�b�g����
        _UseEncrypt = UseEncrypt

        If Not AceptInfo Then
            Return False
        End If

        Dim lgs As New LoginServiceClass
        Dim retbytes() As Byte = lgs.LoginAccount(mode, passwd, ns)

        '�F�؂Ɏ��s������
        If retbytes Is Nothing OrElse retbytes.Length = 0 Then
            NetworkClose()
            Return False
        End If

        '�F�؂ɐ���������
        SetEncrypt(retbytes)
        Return True
    End Function

    ''' <summary>
    ''' �T�[�o�ɐڑ����܂�
    ''' </summary>
    ''' <param name="HostName">�T�[�oIP</param>
    ''' <param name="HostPort">�|�[�g�ԍ�</param>
    ''' <param name="accountname">���M����A�J�E���g��</param>
    ''' <param name="ShowError">�G���[��\�����邩</param>
    ''' <remarks></remarks>
    Public Sub ConnectionNetwork(ByVal HostName As String, ByVal HostPort As Integer, ByVal accountname As String, ByVal ShowError As Boolean)
        Try
            If accountname Is Nothing OrElse accountname.Length = 0 Then
                Throw New Exception("Invalid AccountName")
            End If

            Dim tcp_ns As New System.Net.Sockets.TcpClient(HostName, HostPort)
            '�o�b�t�@�̒���
            tcp_ns.ReceiveBufferSize = Transport_ConnectBufferSize
            tcp_ns.SendBufferSize = Transport_ConnectBufferSize
            tcp_ns.Client.ReceiveBufferSize = Transport_ConnectBufferSize
            tcp_ns.Client.SendBufferSize = Transport_ConnectBufferSize
            '�o�b�t�@�̒�������
            _RemoteIp = CType(tcp_ns.Client.RemoteEndPoint, Net.IPEndPoint).Address.ToString
            _RemotePort = CType(tcp_ns.Client.RemoteEndPoint, Net.IPEndPoint).Port
            _StartAceptTime = DateTime.Now

            ns = tcp_ns.GetStream

            '�A�J�E���g���̑��M
            Dim sendBytes() As Byte = GetSendData(enc.GetBytes(accountname))
            ns.Write(sendBytes, 0, sendBytes.Length)
        Catch ex As Exception
            NetworkClose()
        End Try
    End Sub

    ''' <summary>
    ''' �|�[�g�����b�X�����A�ڑ��v��������̂�҂��܂�
    ''' </summary>
    ''' <param name="port">Listen�Ώۂ̃|�[�g</param>
    ''' <param name="accountname">����̃A�J�E���g��</param>
    ''' <remarks></remarks>
    Public Function ListenPort(ByVal port As Integer, ByRef accountname As String) As ListenStatus
        Dim listener As New System.Net.Sockets.TcpListener(System.Net.IPAddress.Any, port)
        Try
            '���b�X�����J�n
            Try
                listener.Start()
            Catch ex As Exception
                Return ListenStatus.ListenError
            End Try

            '�ڑ����J�n
            Do
                If _StopListen Then
                    Throw New Exception("Connect was cancelled")
                End If
                System.Threading.Thread.Sleep(100)
            Loop Until listener.Pending

            Dim tcp_ns As System.Net.Sockets.TcpClient = listener.AcceptTcpClient
            '�o�b�t�@�̒���
            tcp_ns.ReceiveBufferSize = Transport_ConnectBufferSize
            tcp_ns.SendBufferSize = Transport_ConnectBufferSize
            tcp_ns.Client.SendBufferSize = Transport_ConnectBufferSize
            tcp_ns.Client.ReceiveBufferSize = Transport_ConnectBufferSize
            '�o�b�t�@�̒�������
            _RemoteIp = CType(tcp_ns.Client.RemoteEndPoint, System.Net.IPEndPoint).Address.ToString
            _RemotePort = CType(tcp_ns.Client.RemoteEndPoint, System.Net.IPEndPoint).Port
            _StartAceptTime = DateTime.Now

            ns = tcp_ns.GetStream

            '�A�J�E���g�̎�M
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
    ''' �p�P�b�g�𑗐M���܂�
    ''' </summary>
    ''' <param name="buf">�o�b�t�@</param>
    ''' <param name="header">�w�b�_</param>
    ''' <param name="File_ID">�t�@�C��ID</param>
    ''' <remarks></remarks>
    Public Sub SendPacket(ByVal buf() As Byte, ByVal header As TCP_Header, ByVal File_ID As UShort)
        Dim sendBytes As New List(Of Byte) 'Protcol{1Byte} + UseEncrypt(1Byte) + ID{2Byte} + PacketSize{4Byte} + Cache.Length{unknown}
        Try
            sendBytes.Add(header) '�w�b�_���i�[
            sendBytes.Add(CByte(_UseEncrypt)) '�Í������A0�Ȃ�False�A����ȊO��True
            sendBytes.AddRange(System.BitConverter.GetBytes(File_ID)) '�t�@�C��ID�i�[
            If buf IsNot Nothing AndAlso buf.Length > 0 Then
                If UseEncrypt Then
                    Dim encBytes() As Byte = EncryptData(buf)
                    sendBytes.AddRange(System.BitConverter.GetBytes(encBytes.Length)) '�Í�����̃t�@�C���T�C�Y�̊i�[
                    sendBytes.AddRange(encBytes) '�Í�����̖{�̊i�[

                    '�Í������ꂽ�f�[�^�̑��M��
                    _SendEncryptDataNum = CUInt(_SendEncryptDataNum + 1)
                Else
                    sendBytes.AddRange(System.BitConverter.GetBytes(buf.Length)) '�t�@�C���T�C�Y�i�[
                    sendBytes.AddRange(buf) '�{�̊i�[
                End If
            Else
                sendBytes.AddRange(New Byte() {0, 0, 0, 0}) '�t�@�C���T�C�Y0���i�[����
            End If

            '���M����
            ns.Write(sendBytes.ToArray, 0, sendBytes.Count)

            '���M�T�C�Y�ɉ��Z
            _AllSendSize += CLng(sendBytes.Count)
            _OneSendSize += sendBytes.Count
        Catch ex As Exception
            NetworkClose()
        End Try
    End Sub

    ''' <summary>
    ''' �p�P�b�g�𑗐M���܂�
    ''' </summary>
    ''' <param name="msg">���M���镶����</param>
    ''' <param name="header">�w�b�_</param>
    ''' <param name="File_ID">�t�@�C��ID</param>
    ''' <remarks></remarks>
    Public Sub SendPacket(ByVal msg As String, ByVal header As TCP_Header, ByVal File_ID As UShort)
        SendPacket(enc.GetBytes(msg), header, File_ID)
    End Sub

    ''' <summary>
    ''' �f�[�^����M���܂�
    ''' </summary>
    ''' <param name="buf">�ǂݎ��o�b�t�@</param>
    ''' <param name="File_ID">�t�@�C��ID</param>
    ''' <param name="header">�w�b�_</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReceiveData(ByRef buf() As Byte, ByRef File_ID As UShort, ByRef header As TCP_Header) As Boolean
        Dim firstRec(7) As Byte 'Protcol{1Byte} + UseEncrypt(1Byte) + ID{2Byte} + PacketSize{4Byte} + Cache.Length{unknown}
        Dim recNS As New IO.MemoryStream

        Try
            '�w�b�_��M
            Dim RecSize As Integer = ns.Read(firstRec, 0, firstRec.Length)
            '�ؒf����ĂȂ����`�F�b�N
            If RecSize <= 0 Then
                Throw New Exception("Connect close")
            End If
            '�v���g�R���w�b�_
            header = CType(firstRec(0), TCP_Header)
            '�Í�����
            Dim encrypt As Boolean = CBool(firstRec(1))
            '�t�@�C��ID��M
            File_ID = System.BitConverter.ToUInt16(firstRec, 2)
            '�{�f�[�^�T�C�Y
            Dim header_Size As Integer = System.BitConverter.ToInt32(firstRec, 4)

            '�{�f�[�^��M�̂��߂̃o�b�t�@�T�C�Y����
            Dim bufSize As Integer = header_Size \ 2
            If bufSize > Transport_RecBufferSize_Max Then
                bufSize = Transport_RecBufferSize_Max
            ElseIf bufSize < Transport_RecBufferSize_Min Then
                bufSize = Transport_RecBufferSize_Min
            End If

            '�{�f�[�^��M�̃o�b�t�@�[
            Dim resBytes(bufSize) As Byte

            '�y�C���[�h��M�J�n
            If header_Size < 0 Then
                Throw New Exception("Invalid header")
            ElseIf header_Size > 0 Then
                '�{�f�[�^�����̎�M
                Dim AllRecSize As Integer = 0
                Dim reminSize As Integer = 0
                Do
                    reminSize = header_Size - AllRecSize  '��M����c��̃T�C�Y
                    If reminSize > resBytes.Length Then
                        reminSize = resBytes.Length
                    End If
                    RecSize = ns.Read(resBytes, 0, reminSize)
                    If RecSize <= 0 Then
                        Throw New Exception("Connect close")
                    End If
                    '�f�[�^�𒙂߂�
                    recNS.Write(resBytes, 0, RecSize)
                    AllRecSize += RecSize
                Loop While (AllRecSize < header_Size)

                '��M�T�C�Y�X�V
                _AllReceiveSize += CLng(AllRecSize + firstRec.Length)
                _OneReceiveSize += (AllRecSize + firstRec.Length)

                If encrypt Then
                    '����������
                    buf = DecryptData(recNS.ToArray)

                    '�Í����f�[�^��M��
                    _ReceiveEncryptDataNum = CUInt(_ReceiveEncryptDataNum + 1)
                Else
                    buf = recNS.ToArray
                End If
            Else
                '�y�C���[�h�Ȃ��Ɣ���
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
    ''' �f�[�^���Í������܂�
    ''' </summary>
    ''' <param name="encBytes">�Í����Ώۂ̃f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EncryptData(ByVal encBytes() As Byte) As Byte()
        If encBytes IsNot Nothing AndAlso encBytes.Length > 0 Then
            Dim msOut As New System.IO.MemoryStream
            Dim cryptStreem As System.Security.Cryptography.CryptoStream = Nothing
            Try
                cryptStreem = New System.Security.Cryptography.CryptoStream(msOut, rijndael.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
                '��������
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
    ''' �f�[�^�𕜍������܂�
    ''' </summary>
    ''' <param name="decBytes">�������Ώۂ̃f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DecryptData(ByVal decBytes() As Byte) As Byte()
        If decBytes IsNot Nothing AndAlso decBytes.Length > 0 Then
            Dim msIn As System.IO.MemoryStream = Nothing
            Dim cryptStreem As System.Security.Cryptography.CryptoStream = Nothing
            Dim retMS As IO.MemoryStream = Nothing
            Try
                '�ǂݍ��݃I�u�W�F�N�g�̍쐬
                msIn = New System.IO.MemoryStream(decBytes, 0, decBytes.Length)
                '�������I�u�W�F�N�g�̍쐬
                cryptStreem = New System.Security.Cryptography.CryptoStream(msIn, rijndael.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Read)
                '�f�[�^��ǂݎ��
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
        End If
        Return New Byte() {}
    End Function
    ''' <summary>
    ''' �Í����C���X�^���X���Z�b�g����
    ''' </summary>
    ''' <param name="key">�Í����L�[</param>
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

        '�p�X���[�h���狤�L�L�[�Ə������x�N�^�����
        Dim salt() As Byte = enc.GetBytes("arachlex.exe encryptdata")
        'Rfc2898DeriveBytes�I�u�W�F�N�g���쐬����
        Dim deriveBytes As New System.Security.Cryptography.Rfc2898DeriveBytes(key, salt, 1000)
        '���L�L�[�Ə������x�N�^�𐶐�����
        rijndael.Key = deriveBytes.GetBytes(256 \ 8)
        rijndael.IV = deriveBytes.GetBytes(128 \ 8)
    End Sub

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
    ''' �R���X�g���N�^
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