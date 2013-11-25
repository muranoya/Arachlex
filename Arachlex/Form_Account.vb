Imports Arachlex.DefinitionClass
Public Class Form_Account

    Private ControledForm As Form1
    Private NormalDone As Boolean = False
    Private _EditMode As Boolean = False
    Private ef_accountdata As AccountData

    Private Sub AccountForm_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountForm_OK.Click
        If _EditMode = False AndAlso ControledForm.ExistAccountName(AccountForm_AccountName.Text) Then
            MessageBox.Show(ControledForm.lang.AccountForm_DialogMSG_AlreadyExist, SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If AccountForm_AccountName.TextLength < Account_NameLength_Min Then
            MessageBox.Show(ControledForm.lang.AccountForm_DialogMSG_AccountNameTooShort, SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            NormalDone = True
            With ef_accountdata
                .AccountName = AccountForm_AccountName.Text
                .ConnectIP = AccountForm_Ip.Text
                .ConnectPort = CInt(AccountForm_Port.Value)
                .LoginPassword = AccountForm_AttKey.Text
                .UseEncryptNetwork = AccountForm_Encrypt.Checked
                .DownloadAutoSave = AccountForm_AutoDownload.Checked
                .DownloadAutoSavePath = AccountForm_SavePath.Text
                .Share_Use = AccountForm_Share_Use.Checked

                '共有リストの構築
                If AccountForm_Share_List.Items.Count > 0 Then
                    Dim sb As New Generic.List(Of String)
                    For i As Integer = 0 To AccountForm_Share_List.Items.Count - 1
                        sb.Add(AccountForm_Share_List.Items(i).ToString)
                    Next
                    .Share_OpenFolder() = sb
                Else
                    .Share_OpenFolder() = New Generic.List(Of String)
                End If
            End With

            Me.Close()
        End If
    End Sub
    Private Sub AccountForm_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountForm_Close.Click
        NormalDone = False
        Me.Close()
    End Sub

    Private Sub AccountForm_AutoDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AccountForm_AutoDownload.Click
        AccountForm_AutoDownload.Checked = Not AccountForm_AutoDownload.Checked

        AccountForm_SelectSavePath.Enabled = AccountForm_AutoDownload.Checked
        AccountForm_SavePath.Enabled = AccountForm_AutoDownload.Checked
    End Sub
    Private Sub AccountForm_SelectSavePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountForm_SelectSavePath.Click
        'FolderBrowserDialogクラスのインスタンスを作成
        Dim fbd As New FolderBrowserDialog

        '上部に表示する説明テキストを指定する
        fbd.Description = ControledForm.lang.AccountForm_DialogTitle_SelectFolder
        'ルートフォルダを指定する
        fbd.RootFolder = Environment.SpecialFolder.Desktop
        '最初に選択するフォルダを指定する
        If IO.Directory.Exists(AccountForm_SavePath.Text) Then
            fbd.SelectedPath = AccountForm_SavePath.Text
        Else
            fbd.SelectedPath = DefaultFolder
        End If
        'ユーザーが新しいフォルダを作成できるようにする
        fbd.ShowNewFolderButton = True

        'ダイアログを表示する
        If fbd.ShowDialog(Me) = DialogResult.OK Then
            AccountForm_SavePath.Text = fbd.SelectedPath
        End If
        fbd.Dispose()
    End Sub

    Private Sub AccountForm_Share_Use_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AccountForm_Share_Use.Click
        AccountForm_Share_Use.Checked = Not AccountForm_Share_Use.Checked

        AccountForm_Share_List.Enabled = AccountForm_Share_Use.Checked
    End Sub
    Private Sub AccountForm_Share_List_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles AccountForm_Share_List.DragDrop
        Dim fileName As String() = CType(e.Data.GetData(DataFormats.FileDrop, False), String())
        For i As Integer = 0 To fileName.Length - 1
            If IO.Directory.Exists(fileName(i)) AndAlso Not ExistsFolder(fileName(i)) Then
                AccountForm_Share_List.Items.Add(fileName(i))
            End If
        Next
    End Sub
    Private Sub AccountForm_Share_List_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles AccountForm_Share_List.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub SContext_AddFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SContext_AddFolder.Click
        Dim fbd As New FolderBrowserDialog
        fbd.Description = ControledForm.lang.AccountForm_DialogTitle_SelectFolder
        fbd.RootFolder = Environment.SpecialFolder.Desktop
        fbd.SelectedPath = My.Computer.FileSystem.SpecialDirectories.Desktop
        fbd.ShowNewFolderButton = True
        If fbd.ShowDialog(Me) = DialogResult.OK Then
            If Not ExistsFolder(fbd.SelectedPath) Then
                AccountForm_Share_List.Items.Add(fbd.SelectedPath)
            End If
        End If
        fbd.Dispose()
    End Sub
    Private Sub SContext_DeleteFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SContext_DeleteFolder.Click
        If AccountForm_Share_List.SelectedItems.Count > 0 Then
            Do
                AccountForm_Share_List.Items.RemoveAt(AccountForm_Share_List.SelectedIndices(0))
            Loop Until 0 = AccountForm_Share_List.SelectedItems.Count
        End If
    End Sub
    Private Sub SContext_CheckExist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SContext_CheckExist.Click
        If AccountForm_Share_List.Items.Count > 0 Then
            Dim i As Integer = 0
            Do
                If Not IO.Directory.Exists(AccountForm_Share_List.Items(i).ToString) Then
                    AccountForm_Share_List.Items.RemoveAt(i)
                Else
                    i += 1
                End If
            Loop Until i = AccountForm_Share_List.Items.Count
        End If
    End Sub

    ''' <summary>
    ''' 編集データを取得します
    ''' </summary>
    ''' <param name="OwnerForm">オーナーフォーム</param>
    ''' <param name="EditMode">編集モード</param>
    ''' <param name="EditAccountData">編集するアカウントデータ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AccountContactInfo(ByVal OwnerForm As Form1, ByVal EditMode As Boolean, ByVal EditAccountData As AccountData) As AccountData
        _EditMode = EditMode
        AccountForm_Port.Maximum = Port_Max
        AccountForm_Port.Minimum = Port_Min
        AccountForm_AccountName.MaxLength = Account_NameLength_Max
        AccountForm_AttKey.MaxLength = AttKey_Length_Max

        ControledForm = OwnerForm
        With EditAccountData
            AccountForm_AccountName.Text = .AccountName
            AccountForm_Ip.Text = .ConnectIP
            AccountForm_Port.Value = .ConnectPort
            AccountForm_AttKey.Text = .LoginPassword
            AccountForm_Encrypt.Checked = .UseEncryptNetwork
            AccountForm_AutoDownload.Checked = .DownloadAutoSave
            AccountForm_SavePath.Text = .DownloadAutoSavePath
            AccountForm_SavePath.Enabled = .DownloadAutoSave
            AccountForm_SelectSavePath.Enabled = .DownloadAutoSave
            AccountForm_Share_Use.Checked = .Share_Use
            '共有パスのロード
            If .Share_OpenFolder() IsNot Nothing AndAlso .Share_OpenFolder.Count > 0 Then
                AccountForm_Share_List.Items.AddRange(.Share_OpenFolder.ToArray)
            End If
            AccountForm_Share_List.Enabled = .Share_Use
        End With

        ef_accountdata = EditAccountData
        Me.Text = ControledForm.lang.AccountForm_FormTitle
        Me.TopMost = OwnerForm.TopMost
        Me.ShowDialog()

        If NormalDone Then
            Return ef_accountdata
        Else
            Return Nothing
        End If
    End Function

    Private Function ExistsFolder(ByVal fpath As String) As Boolean
        If AccountForm_Share_List.Items.Count > 0 Then
            For i As Integer = 0 To AccountForm_Share_List.Items.Count - 1
                If AccountForm_Share_List.Items(i).ToString.Equals(fpath, StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Private Sub Form_Account_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        With ControledForm.lang
            GroupBox1.Text = .AccountForm_DestAccount
            Label2.Text = .AccountForm_DestAccount_AccountName
            Label1.Text = .AccountForm_DestAccount_IPAddress
            Label4.Text = .AccountForm_DestAccount_Port
            Label3.Text = .AccountForm_DestAccount_LoginKey
            AccountForm_Encrypt.Text = .AccountForm_DestAccount_Encrypt
            AccountForm_AutoDownload.Text = .AccountForm_ApprovalDownload
            Label5.Text = .AccountForm_ApprovalDownload_SavePath
            AccountForm_SelectSavePath.Text = .AccountForm_ApprovalDownload_SelectPath
            AccountForm_Share_Use.Text = .AccountForm_EnableShare
            SContext_AddFolder.Text = .AccountForm_CShare_AddFolder
            SContext_DeleteFolder.Text = .AccountForm_CShare_DeleteFromList
            SContext_CheckExist.Text = .AccountForm_CShare_DeleteUnExistFolder
            AccountForm_OK.Text = .AccountForm_OKButton
            AccountForm_Close.Text = .AccountForm_CancelButton
        End With
    End Sub
End Class