Imports System.Runtime.InteropServices
Public Class DefinitionClass

#Region "�\�t�g�E�F�A"
    Public Const SoftwareName As String = "Arachlex"
    Public Const SoftwareVersion As String = "2.5 beta"
    Public Const Developer As String = "���̉�"
    Public Const ProtocolVersion As Integer = 3
    Public Const IconProduct As String = "Fugue Icon, Copyright (C) 2010 Yusuke Kamiyamane."
    Public Const EXEIconProduct As String = "DTI��"
#End Region

    '�|�[�g�ԍ�
    Public Const Port_Max As Integer = UShort.MaxValue  '�ȉ�
    Public Const Port_Min As Integer = 1  '�ȏ�
    Public Const Port_Default As Integer = 37842

    '���M�o�b�t�@
    Public Const Transport_SendBufferSize_Max As Integer = 4194304 '4MB
    Public Const Transport_SendBufferSize_Min As Integer = 1024
    Public Const Transport_SendBufferSize_Default As Integer = 65536 '64KB

    '�A�J�E���g��
    Public Const Account_NameLength_Max As Integer = 50
    Public Const Account_NameLength_Min As Integer = 1
    Public Const Account_Default As String = "friend"
    Public Shared ReadOnly Account_MyName_Default As String = Environment.MachineName

    '�R�����g
    Public Const Comment_Length_Max As Integer = 500
    Public Const Comment_Length_Min As Integer = 0

    '�p�X���[�h
    Public Const AttKey_Length_Max As Integer = 50
    Public Const AttKey_Length_Min As Integer = 0

    '�t�@�C���ƃt�H���_
    Public Const CacheExt As String = ".temp"
    Public Shared ReadOnly DefaultFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
    Public Shared ReadOnly SaveConfigPath As String = IO.Path.Combine(Application.StartupPath, "Setting.Arachlex")
    Public Shared ReadOnly enc As System.Text.Encoding = System.Text.Encoding.UTF8

    '����
    Public Shared ReadOnly LanguageLocation As String = IO.Path.Combine(Application.StartupPath, "lang.arachlex_language")

    Public Enum Arachlex_Item
        File
        Folder
    End Enum

    Public Enum Arachlex_Transport
        Upload
        Download
    End Enum

#Region "Win32API"
    <DllImport("user32.dll", CharSet:=CharSet.Auto)> Public Shared Function FlashWindow(ByVal hWnd As IntPtr, ByVal bInvert As Boolean) As Boolean
    End Function

    <DllImport("shell32.dll", CharSet:=CharSet.Auto)> Private Shared Function ExtractIcon(ByVal hInst As IntPtr, ByVal lpszExeFileName As String, ByVal nIconIndex As Integer) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)> Public Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As Boolean
    End Function
#End Region

    ''' <summary>
    ''' �P�ʂ�t���ăt�@�C���T�C�Y��Ԃ��܂�
    ''' </summary>
    ''' <param name="fSize">�ϊ��Ώۂ̃o�C�g�P�ʂ̒l</param>
    ''' <remarks></remarks>
    Public Shared Function RetFileSize(ByVal fSize As Long) As String
        If fSize > 1073741824 Then 'GB
            Return Math.Round(fSize / 1073741824, 2) & "GB"
        ElseIf fSize > 1048576 Then 'MB
            Return Math.Round(fSize / 1048576, 2) & "MB"
        ElseIf fSize > 1024 Then 'KB
            Return Math.Round(fSize / 1024, 2) & "KB"
        Else 'Byte
            Return fSize & "Byte"
        End If
    End Function

    ''' <summary>
    ''' �w�肵���p�X�̏d�����Ȃ��p�X��Ԃ��܂�
    ''' </summary>
    ''' <param name="fPath">�Ώۂ̃p�X</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRepetitionFileName(ByVal fPath As String) As String
        If IO.File.Exists(fPath) Then
            Dim i As Integer = 0
            Dim oldPath As String = IO.Path.Combine(IO.Path.GetDirectoryName(fPath), IO.Path.GetFileNameWithoutExtension(fPath))
            Dim oldEXT As String = IO.Path.GetExtension(fPath)
            Do
                If Not IO.File.Exists(oldPath & "(" & i & ")" & oldEXT) Then
                    Return oldPath & "(" & i & ")" & oldEXT
                End If
                i += 1
            Loop
        End If
        Return fPath
    End Function

    ''' <summary>
    ''' �w�肵���t�H���_�p�X�̏d�����Ȃ��p�X��Ԃ��܂�
    ''' </summary>
    ''' <param name="fpath">�Ώۂ̃t�H���_�p�X</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRepetitionFolderName(ByVal fpath As String) As String
        If IO.Directory.Exists(fpath) Then
            Dim i As Integer = 0
            Do
                If Not IO.Directory.Exists(fpath & "(" & i & ")") Then
                    Return fpath & "(" & i & ")"
                End If
                i += 1
            Loop
        End If
        Return fpath
    End Function

    ''' <summary>
    ''' �w��g���q�̃A�C�R�����擾���܂�
    ''' </summary>
    ''' <param name="exeStr">�g���q��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetExtensionIcon(ByVal callHandle As IntPtr, ByVal exeStr As String) As Icon
        Const DefaultIconPath As String = "%SystemRoot%\System32\shell32.dll"
        Try
            '�g���q�̃`�F�b�N
            If exeStr Is Nothing OrElse exeStr.Length = 0 OrElse ".ico".Equals(exeStr, StringComparison.OrdinalIgnoreCase) Then
                '�g���q��ico�̏ꍇ�A�A�C�R���͑��݂��Ȃ�
                Return Nothing
            ElseIf ".exe".Equals(exeStr, StringComparison.OrdinalIgnoreCase) Then
                'exe�A�C�R���͗�O�Ƃ���
                Return Icon.FromHandle(ExtractIcon(callHandle, DefaultIconPath, 2))
            End If
        Catch ex As Exception
            Return Nothing
        End Try

        Dim regkey As Microsoft.Win32.RegistryKey = Nothing
        Dim regDefaultIconKey As Microsoft.Win32.RegistryKey = Nothing
        Try
            '�w�肳�ꂽ�g���q�̃��W�X�g���L�[���J��
            regkey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(exeStr, False)
            If regkey Is Nothing Then
                Return Nothing
            End If

            Dim regStr As String = CStr(regkey.GetValue(""))
            regDefaultIconKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(regStr & "\DefaultIcon", False)
            If regDefaultIconKey Is Nothing Then
                regkey.Close()
                regkey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(regStr & "\CurVer", False)
                If regkey Is Nothing Then
                    Return Nothing
                End If

                regDefaultIconKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(CStr(regkey.GetValue("")) & "\DefaultIcon", False)
                If regDefaultIconKey Is Nothing Then
                    Return Nothing
                End If
            End If

            Dim exeIconPath As String() = CStr(regDefaultIconKey.GetValue("")).Split(New Char() {","c}, 2)
            If exeIconPath IsNot Nothing AndAlso exeIconPath.Length = 2 Then
                Return Icon.FromHandle(ExtractIcon(callHandle, exeIconPath(0).Replace("""", ""), CInt(exeIconPath(1))))
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        Finally
            '�J��
            If regkey IsNot Nothing Then
                regkey.Close()
            End If
            If regDefaultIconKey IsNot Nothing Then
                regDefaultIconKey.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Array1�̔z����𒲂ׁA�S�ăI���W�i���̒l�ɐ��񂳂��܂�
    ''' </summary>
    ''' <param name="Array1">�Ώۂ̔z��</param>
    ''' <remarks></remarks>
    Public Shared Sub SetSuccessionArray(ByRef Array1 As Generic.List(Of String))
        If Array1 IsNot Nothing AndAlso Array1.Count > 0 Then
            Dim i As Integer = 0 'sb�̃C���f�b�N�X
            Do
                Dim index As Integer = i + 1
                If index > Array1.Count Then index = Array1.Count - 1
                If Array1.IndexOf(Array1(i), index) >= 0 Then
                    Array1.RemoveAt(i)
                Else
                    i += 1
                End If
            Loop Until i = Array1.Count
        End If
    End Sub
End Class