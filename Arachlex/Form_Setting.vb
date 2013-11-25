Imports Arachlex.DefinitionClass
Imports Arachlex.TCPNetworkClass
Public Class Form_Setting

    Private ControledForm As Form1

    Private Sub Form_Setting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.TopMost = ControledForm.TopMost
        Me.Text = SoftwareName & " - " & ControledForm.lang.SettingForm_Title
        Setting_Port.Maximum = Port_Max
        Setting_Port.Minimum = Port_Min
        Setting_Language.DataSource = [Enum].GetValues(GetType(LanguageClass.LanguageType))

        Setting_Comment.MaxLength = Comment_Length_Max
        Setting_AccountName.MaxLength = Account_NameLength_Max

        With ControledForm.appSettings
            Setting_Port.Value = .Connect_ListenPort
            Setting_AllowMultipleStarts.Checked = .Main_AllowMultipleStarts
            Setting_TopMost.Checked = .Main_TopMost
            Setting_MiniHide.Checked = .Main_MiniHide

            Setting_Language.SelectedItem = .Language

            Setting_Comment.Text = .Account_Comment
            Setting_AccountName.Text = .Account_AccountName

            Setting_NotifyConnect.Checked = .Notify_Connect
            Setting_NotifyRecMSG.Checked = .Notify_ReceiveMSG
            Setting_NotifyDoneUpload.Checked = .Notify_DoneUpload
            Setting_NotifyDoneDownload.Checked = .Notify_DoneDownload
            Setting_NotifyUpload.Checked = .Notify_Upload
        End With

        '言語を適用
        With ControledForm.lang
            GroupBox1.Text = .SettingForm_General
            Setting_TopMost.Text = .SettingForm_General_AlwaysOnTop
            Setting_MiniHide.Text = .SettingForm_General_HideWhenMinimization
            Label2.Text = .SettingForm_General_Port
            Setting_AllowMultipleStarts.Text = .SettingForm_General_AllowMultipleStarts

            GroupBox2.Text = .SettingForm_MyAccount
            Label1.Text = .SettingForm_MyAccount_AccountName
            Label4.Text = .SettingForm_MyAccount_Comment

            GroupBox3.Text = .SettingForm_Notify
            Setting_NotifyConnect.Text = .SettingForm_Notify_WhenConnect
            Setting_NotifyRecMSG.Text = .SettingForm_Notify_WhenReceiveMessage
            Setting_NotifyDoneUpload.Text = .SettingForm_Notify_WhenDoneUpload
            Setting_NotifyDoneDownload.Text = .SettingForm_Notify_WhenDoneDownload
            Setting_NotifyUpload.Text = .SettingForm_Notify_WhenUpload

            Form_Version.Text = .SettingForm_Version
            Form_Close.Text = .SettingForm_Cancel
            Form_OK.Text = .SettingForm_Ok
        End With
    End Sub
    Private Sub Form_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Form_OK.Click
        With ControledForm.appSettings
            '-------------------設定変更前の値-------------------
            Dim BeforePort As Integer = .Connect_ListenPort
            Dim BeforeAccountName As String = .Account_AccountName
            Dim BeforeComment As String = .Account_Comment
            Dim BeforeLanguage As LanguageClass.LanguageType = .Language
            '-------------------設定をクラスへ格納-------------------
            .Language = CType(Setting_Language.SelectedItem, LanguageClass.LanguageType)
            .Main_TopMost = Setting_TopMost.Checked
            ControledForm.TopMost = .Main_TopMost
            .Main_MiniHide = Setting_MiniHide.Checked
            .Connect_ListenPort = CInt(Setting_Port.Value)
            .Main_AllowMultipleStarts = Setting_AllowMultipleStarts.Checked

            .Account_Comment = Setting_Comment.Text
            .Account_AccountName = Setting_AccountName.Text

            .Notify_Connect = Setting_NotifyConnect.Checked
            .Notify_ReceiveMSG = Setting_NotifyRecMSG.Checked
            .Notify_DoneDownload = Setting_NotifyDoneDownload.Checked
            .Notify_DoneUpload = Setting_NotifyDoneUpload.Checked
            .Notify_Upload = Setting_NotifyUpload.Checked

            '-------------------変更を通知-------------------
            'Language設定が変わっていれば
            If .Language = LanguageClass.LanguageType.LanguageFile Then
                ControledForm.lang = ControledForm.LoadLanguageFile()
                If ControledForm.lang Is Nothing Then
                    ControledForm.lang = New LanguageClass(LanguageClass.LanguageType.English)
                End If
            Else
                ControledForm.lang = New LanguageClass(.Language)
            End If
            ControledForm.ResetUserInterfaceLanguage()

            'ポート番号が変わっていればリッスンをしなおす
            If BeforePort <> .Connect_ListenPort Then
                If ControledForm.bgServer.IsBusy Then
                    ControledForm.tcpconnect.StopListen()
                Else
                    ControledForm.bgServer.RunWorkerAsync()
                End If
            End If

            'アカウント名とポートとコメントの更新を相手に通知する
            If Not BeforeAccountName.Equals(.Account_AccountName, StringComparison.OrdinalIgnoreCase) OrElse BeforePort <> .Connect_ListenPort OrElse Not BeforeComment.Equals(Setting_Comment.Text, StringComparison.OrdinalIgnoreCase) Then
                If ControledForm.UserAccount IsNot Nothing AndAlso ControledForm.UserAccount.Count > 0 Then
                    For i As Integer = 0 To ControledForm.UserAccount.Count - 1
                        If Not BeforeAccountName.Equals(.Account_AccountName, StringComparison.OrdinalIgnoreCase) Then
                            ControledForm.UserAccount(i).TCPConnect.SendPacket(.Account_AccountName, TCP_Header.ChangeAccountName, UShort.MinValue)
                        End If
                        If BeforePort <> .Connect_ListenPort Then
                            ControledForm.UserAccount(i).TCPConnect.SendPacket(CStr(.Connect_ListenPort), TCP_Header.ChangePort, UShort.MinValue)
                        End If
                        If Not BeforeComment.Equals(.Account_AccountName, StringComparison.OrdinalIgnoreCase) Then
                            ControledForm.UserAccount(i).TCPConnect.SendPacket(.Account_Comment, TCP_Header.ChangeComment, UShort.MinValue)
                        End If
                    Next
                End If
            End If
        End With

        Me.Close()
    End Sub
    Private Sub Form_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Form_Close.Click
        Me.Close()
    End Sub
    Private Sub Form_Version_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Form_Version.Click
        With ControledForm.lang
            MessageBox.Show(.SettingForm_Version_SoftwareName & SoftwareName & vbCrLf & _
                        .SettingForm_Version_Version & SoftwareVersion & vbCrLf & _
                        .SettingForm_Version_ProtocolVersion & ProtocolVersion & vbCrLf & _
                        .SettingForm_Version_Developer & Developer & vbCrLf & _
                        .SettingForm_Version_ToolbarIcon & IconProduct & vbCrLf & _
                        .SettingForm_Version_ExeIcon & EXEIconProduct, SoftwareName & .SettingForm_Version, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End With
    End Sub

    Public Sub New(ByVal _controledForm As Form1)

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        ControledForm = _controledForm
    End Sub

    Private Sub Setting_EditLanguage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Setting_EditLanguage.Click
        Dim n As New Form_EditLanguage
        Dim lgn As LanguageClass
        If CType(Setting_Language.SelectedItem, LanguageClass.LanguageType) = LanguageClass.LanguageType.LanguageFile Then
            lgn = ControledForm.LoadLanguageFile()
        Else
            lgn = New LanguageClass(CType(Setting_Language.SelectedItem, LanguageClass.LanguageType))
        End If
        n.PropertyGrid1.SelectedObject = lgn
        n.ShowDialog()
    End Sub
End Class