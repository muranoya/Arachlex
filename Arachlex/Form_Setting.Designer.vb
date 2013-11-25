<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Setting
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Setting))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Setting_EditLanguage = New System.Windows.Forms.Button
        Me.Setting_Language = New System.Windows.Forms.ComboBox
        Me.Setting_Port = New System.Windows.Forms.NumericUpDown
        Me.Label2 = New System.Windows.Forms.Label
        Me.Setting_MiniHide = New System.Windows.Forms.CheckBox
        Me.Setting_TopMost = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Setting_NotifyDoneDownload = New System.Windows.Forms.CheckBox
        Me.Setting_NotifyUpload = New System.Windows.Forms.CheckBox
        Me.Setting_NotifyRecMSG = New System.Windows.Forms.CheckBox
        Me.Setting_NotifyDoneUpload = New System.Windows.Forms.CheckBox
        Me.Setting_NotifyConnect = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Setting_Comment = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Setting_AccountName = New System.Windows.Forms.TextBox
        Me.Form_Close = New System.Windows.Forms.Button
        Me.Form_OK = New System.Windows.Forms.Button
        Me.Form_Version = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Setting_AllowMultipleStarts = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.Setting_Port, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Setting_AllowMultipleStarts)
        Me.GroupBox1.Controls.Add(Me.Setting_Port)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Setting_MiniHide)
        Me.GroupBox1.Controls.Add(Me.Setting_TopMost)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(343, 62)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "一般"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(6, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(325, 47)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "If you use ""Language File"" setting, you put a language file the location where th" & _
            "is software same directory and you need to be changed to ""lang.arachlex_language" & _
            """."
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Setting_EditLanguage
        '
        Me.Setting_EditLanguage.Location = New System.Drawing.Point(276, 18)
        Me.Setting_EditLanguage.Name = "Setting_EditLanguage"
        Me.Setting_EditLanguage.Size = New System.Drawing.Size(55, 20)
        Me.Setting_EditLanguage.TabIndex = 7
        Me.Setting_EditLanguage.Text = "Edit"
        Me.Setting_EditLanguage.UseVisualStyleBackColor = True
        '
        'Setting_Language
        '
        Me.Setting_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Setting_Language.FormattingEnabled = True
        Me.Setting_Language.Location = New System.Drawing.Point(6, 18)
        Me.Setting_Language.Name = "Setting_Language"
        Me.Setting_Language.Size = New System.Drawing.Size(264, 20)
        Me.Setting_Language.TabIndex = 5
        '
        'Setting_Port
        '
        Me.Setting_Port.Location = New System.Drawing.Point(191, 40)
        Me.Setting_Port.Name = "Setting_Port"
        Me.Setting_Port.Size = New System.Drawing.Size(93, 19)
        Me.Setting_Port.TabIndex = 3
        Me.Setting_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Setting_Port.ThousandsSeparator = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(152, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "ポート"
        '
        'Setting_MiniHide
        '
        Me.Setting_MiniHide.AutoSize = True
        Me.Setting_MiniHide.Location = New System.Drawing.Point(6, 40)
        Me.Setting_MiniHide.Name = "Setting_MiniHide"
        Me.Setting_MiniHide.Size = New System.Drawing.Size(103, 16)
        Me.Setting_MiniHide.TabIndex = 1
        Me.Setting_MiniHide.Text = "最小化時に隠す"
        Me.Setting_MiniHide.UseVisualStyleBackColor = True
        '
        'Setting_TopMost
        '
        Me.Setting_TopMost.AutoSize = True
        Me.Setting_TopMost.Location = New System.Drawing.Point(6, 18)
        Me.Setting_TopMost.Name = "Setting_TopMost"
        Me.Setting_TopMost.Size = New System.Drawing.Size(121, 16)
        Me.Setting_TopMost.TabIndex = 0
        Me.Setting_TopMost.Text = "常に手前に表示する"
        Me.Setting_TopMost.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Setting_NotifyDoneDownload)
        Me.GroupBox3.Controls.Add(Me.Setting_NotifyUpload)
        Me.GroupBox3.Controls.Add(Me.Setting_NotifyRecMSG)
        Me.GroupBox3.Controls.Add(Me.Setting_NotifyDoneUpload)
        Me.GroupBox3.Controls.Add(Me.Setting_NotifyConnect)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 257)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(343, 84)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "通知"
        '
        'Setting_NotifyDoneDownload
        '
        Me.Setting_NotifyDoneDownload.AutoSize = True
        Me.Setting_NotifyDoneDownload.Location = New System.Drawing.Point(190, 40)
        Me.Setting_NotifyDoneDownload.Name = "Setting_NotifyDoneDownload"
        Me.Setting_NotifyDoneDownload.Size = New System.Drawing.Size(143, 16)
        Me.Setting_NotifyDoneDownload.TabIndex = 4
        Me.Setting_NotifyDoneDownload.Text = "ダウンロードが完了した時"
        Me.Setting_NotifyDoneDownload.UseVisualStyleBackColor = True
        '
        'Setting_NotifyUpload
        '
        Me.Setting_NotifyUpload.AutoSize = True
        Me.Setting_NotifyUpload.Location = New System.Drawing.Point(6, 62)
        Me.Setting_NotifyUpload.Name = "Setting_NotifyUpload"
        Me.Setting_NotifyUpload.Size = New System.Drawing.Size(117, 16)
        Me.Setting_NotifyUpload.TabIndex = 2
        Me.Setting_NotifyUpload.Text = "アップロードされた時"
        Me.Setting_NotifyUpload.UseVisualStyleBackColor = True
        '
        'Setting_NotifyRecMSG
        '
        Me.Setting_NotifyRecMSG.AutoSize = True
        Me.Setting_NotifyRecMSG.Location = New System.Drawing.Point(190, 18)
        Me.Setting_NotifyRecMSG.Name = "Setting_NotifyRecMSG"
        Me.Setting_NotifyRecMSG.Size = New System.Drawing.Size(141, 16)
        Me.Setting_NotifyRecMSG.TabIndex = 3
        Me.Setting_NotifyRecMSG.Text = "メッセージを受け取った時"
        Me.Setting_NotifyRecMSG.UseVisualStyleBackColor = True
        '
        'Setting_NotifyDoneUpload
        '
        Me.Setting_NotifyDoneUpload.AutoSize = True
        Me.Setting_NotifyDoneUpload.Location = New System.Drawing.Point(6, 40)
        Me.Setting_NotifyDoneUpload.Name = "Setting_NotifyDoneUpload"
        Me.Setting_NotifyDoneUpload.Size = New System.Drawing.Size(141, 16)
        Me.Setting_NotifyDoneUpload.TabIndex = 1
        Me.Setting_NotifyDoneUpload.Text = "アップロードが完了した時"
        Me.Setting_NotifyDoneUpload.UseVisualStyleBackColor = True
        '
        'Setting_NotifyConnect
        '
        Me.Setting_NotifyConnect.AutoSize = True
        Me.Setting_NotifyConnect.Location = New System.Drawing.Point(6, 18)
        Me.Setting_NotifyConnect.Name = "Setting_NotifyConnect"
        Me.Setting_NotifyConnect.Size = New System.Drawing.Size(88, 16)
        Me.Setting_NotifyConnect.TabIndex = 0
        Me.Setting_NotifyConnect.Text = "接続された時"
        Me.Setting_NotifyConnect.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Setting_Comment)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Setting_AccountName)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 180)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(343, 71)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "自分のアカウント"
        '
        'Setting_Comment
        '
        Me.Setting_Comment.Location = New System.Drawing.Point(102, 43)
        Me.Setting_Comment.Name = "Setting_Comment"
        Me.Setting_Comment.Size = New System.Drawing.Size(229, 19)
        Me.Setting_Comment.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 12)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "コメント"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(6, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "アカウント名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Setting_AccountName
        '
        Me.Setting_AccountName.Location = New System.Drawing.Point(102, 18)
        Me.Setting_AccountName.Name = "Setting_AccountName"
        Me.Setting_AccountName.Size = New System.Drawing.Size(229, 19)
        Me.Setting_AccountName.TabIndex = 1
        '
        'Form_Close
        '
        Me.Form_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Form_Close.Location = New System.Drawing.Point(190, 347)
        Me.Form_Close.Name = "Form_Close"
        Me.Form_Close.Size = New System.Drawing.Size(84, 23)
        Me.Form_Close.TabIndex = 4
        Me.Form_Close.Text = "キャンセル"
        Me.Form_Close.UseVisualStyleBackColor = True
        '
        'Form_OK
        '
        Me.Form_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Form_OK.Location = New System.Drawing.Point(280, 347)
        Me.Form_OK.Name = "Form_OK"
        Me.Form_OK.Size = New System.Drawing.Size(75, 23)
        Me.Form_OK.TabIndex = 5
        Me.Form_OK.Text = "完了"
        Me.Form_OK.UseVisualStyleBackColor = True
        '
        'Form_Version
        '
        Me.Form_Version.Location = New System.Drawing.Point(12, 347)
        Me.Form_Version.Name = "Form_Version"
        Me.Form_Version.Size = New System.Drawing.Size(94, 23)
        Me.Form_Version.TabIndex = 3
        Me.Form_Version.Text = "バージョン"
        Me.Form_Version.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Setting_EditLanguage)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.Setting_Language)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 80)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(343, 94)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Language"
        '
        'Setting_AllowMultipleStarts
        '
        Me.Setting_AllowMultipleStarts.AutoSize = True
        Me.Setting_AllowMultipleStarts.Location = New System.Drawing.Point(154, 18)
        Me.Setting_AllowMultipleStarts.Name = "Setting_AllowMultipleStarts"
        Me.Setting_AllowMultipleStarts.Size = New System.Drawing.Size(124, 16)
        Me.Setting_AllowMultipleStarts.TabIndex = 4
        Me.Setting_AllowMultipleStarts.Text = "多重起動を許可する"
        Me.Setting_AllowMultipleStarts.UseVisualStyleBackColor = True
        '
        'Form_Setting
        '
        Me.AcceptButton = Me.Form_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(368, 380)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.Form_Version)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Form_OK)
        Me.Controls.Add(Me.Form_Close)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_Setting"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "設定"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Setting_Port, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Setting_NotifyDoneDownload As System.Windows.Forms.CheckBox
    Friend WithEvents Setting_NotifyUpload As System.Windows.Forms.CheckBox
    Friend WithEvents Setting_NotifyRecMSG As System.Windows.Forms.CheckBox
    Friend WithEvents Setting_NotifyDoneUpload As System.Windows.Forms.CheckBox
    Friend WithEvents Setting_NotifyConnect As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Setting_Port As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Setting_ShareOpenAllMemberAndOther As System.Windows.Forms.CheckBox
    Friend WithEvents Form_Close As System.Windows.Forms.Button
    Friend WithEvents Form_OK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Setting_AccountName As System.Windows.Forms.TextBox
    Friend WithEvents Setting_Comment As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Setting_MiniHide As System.Windows.Forms.CheckBox
    Friend WithEvents Setting_TopMost As System.Windows.Forms.CheckBox
    Friend WithEvents Form_Version As System.Windows.Forms.Button
    Friend WithEvents Setting_Language As System.Windows.Forms.ComboBox
    Friend WithEvents Setting_EditLanguage As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Setting_AllowMultipleStarts As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
End Class
