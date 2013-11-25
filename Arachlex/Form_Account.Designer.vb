<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Account
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Account))
        Me.AccountForm_AccountName = New System.Windows.Forms.TextBox
        Me.AccountForm_AttKey = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.AccountForm_Port = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.AccountForm_Ip = New System.Windows.Forms.TextBox
        Me.AccountForm_Close = New System.Windows.Forms.Button
        Me.AccountForm_OK = New System.Windows.Forms.Button
        Me.AccountForm_Encrypt = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.AccountForm_SelectSavePath = New System.Windows.Forms.Button
        Me.AccountForm_SavePath = New System.Windows.Forms.TextBox
        Me.AccountForm_AutoDownload = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.AccountForm_Share_List = New System.Windows.Forms.ListBox
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SContext_AddFolder = New System.Windows.Forms.ToolStripMenuItem
        Me.SContext_DeleteFolder = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.SContext_CheckExist = New System.Windows.Forms.ToolStripMenuItem
        Me.AccountForm_Share_Use = New System.Windows.Forms.CheckBox
        CType(Me.AccountForm_Port, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'AccountForm_AccountName
        '
        Me.AccountForm_AccountName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountForm_AccountName.Location = New System.Drawing.Point(93, 18)
        Me.AccountForm_AccountName.Name = "AccountForm_AccountName"
        Me.AccountForm_AccountName.Size = New System.Drawing.Size(269, 19)
        Me.AccountForm_AccountName.TabIndex = 1
        '
        'AccountForm_AttKey
        '
        Me.AccountForm_AttKey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountForm_AttKey.Location = New System.Drawing.Point(93, 68)
        Me.AccountForm_AttKey.Name = "AccountForm_AttKey"
        Me.AccountForm_AttKey.Size = New System.Drawing.Size(121, 19)
        Me.AccountForm_AttKey.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "認証キー"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "アカウント名"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AccountForm_Port
        '
        Me.AccountForm_Port.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountForm_Port.Location = New System.Drawing.Point(259, 43)
        Me.AccountForm_Port.Name = "AccountForm_Port"
        Me.AccountForm_Port.Size = New System.Drawing.Size(103, 19)
        Me.AccountForm_Port.TabIndex = 5
        Me.AccountForm_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.AccountForm_Port.ThousandsSeparator = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(220, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "ポート"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(6, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "IPアドレス"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AccountForm_Ip
        '
        Me.AccountForm_Ip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountForm_Ip.Location = New System.Drawing.Point(93, 43)
        Me.AccountForm_Ip.Name = "AccountForm_Ip"
        Me.AccountForm_Ip.Size = New System.Drawing.Size(121, 19)
        Me.AccountForm_Ip.TabIndex = 3
        '
        'AccountForm_Close
        '
        Me.AccountForm_Close.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.AccountForm_Close.Location = New System.Drawing.Point(213, 307)
        Me.AccountForm_Close.Name = "AccountForm_Close"
        Me.AccountForm_Close.Size = New System.Drawing.Size(86, 23)
        Me.AccountForm_Close.TabIndex = 3
        Me.AccountForm_Close.Text = "キャンセル"
        Me.AccountForm_Close.UseVisualStyleBackColor = True
        '
        'AccountForm_OK
        '
        Me.AccountForm_OK.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.AccountForm_OK.Location = New System.Drawing.Point(305, 307)
        Me.AccountForm_OK.Name = "AccountForm_OK"
        Me.AccountForm_OK.Size = New System.Drawing.Size(75, 23)
        Me.AccountForm_OK.TabIndex = 4
        Me.AccountForm_OK.Text = "完了"
        Me.AccountForm_OK.UseVisualStyleBackColor = True
        '
        'AccountForm_Encrypt
        '
        Me.AccountForm_Encrypt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountForm_Encrypt.Location = New System.Drawing.Point(220, 71)
        Me.AccountForm_Encrypt.Name = "AccountForm_Encrypt"
        Me.AccountForm_Encrypt.Size = New System.Drawing.Size(142, 16)
        Me.AccountForm_Encrypt.TabIndex = 8
        Me.AccountForm_Encrypt.Text = "通信を暗号化する"
        Me.AccountForm_Encrypt.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.AccountForm_AccountName)
        Me.GroupBox1.Controls.Add(Me.AccountForm_Encrypt)
        Me.GroupBox1.Controls.Add(Me.AccountForm_Ip)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.AccountForm_AttKey)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.AccountForm_Port)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(368, 96)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "接続先アカウント"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.AccountForm_SelectSavePath)
        Me.GroupBox2.Controls.Add(Me.AccountForm_SavePath)
        Me.GroupBox2.Controls.Add(Me.AccountForm_AutoDownload)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 114)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(368, 52)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(6, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 12)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "保存場所"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AccountForm_SelectSavePath
        '
        Me.AccountForm_SelectSavePath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountForm_SelectSavePath.Location = New System.Drawing.Point(293, 20)
        Me.AccountForm_SelectSavePath.Name = "AccountForm_SelectSavePath"
        Me.AccountForm_SelectSavePath.Size = New System.Drawing.Size(69, 23)
        Me.AccountForm_SelectSavePath.TabIndex = 3
        Me.AccountForm_SelectSavePath.Text = "選択"
        Me.AccountForm_SelectSavePath.UseVisualStyleBackColor = True
        '
        'AccountForm_SavePath
        '
        Me.AccountForm_SavePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountForm_SavePath.Location = New System.Drawing.Point(81, 22)
        Me.AccountForm_SavePath.Name = "AccountForm_SavePath"
        Me.AccountForm_SavePath.Size = New System.Drawing.Size(206, 19)
        Me.AccountForm_SavePath.TabIndex = 2
        '
        'AccountForm_AutoDownload
        '
        Me.AccountForm_AutoDownload.AutoCheck = False
        Me.AccountForm_AutoDownload.AutoSize = True
        Me.AccountForm_AutoDownload.Location = New System.Drawing.Point(6, 0)
        Me.AccountForm_AutoDownload.Name = "AccountForm_AutoDownload"
        Me.AccountForm_AutoDownload.Size = New System.Drawing.Size(165, 16)
        Me.AccountForm_AutoDownload.TabIndex = 0
        Me.AccountForm_AutoDownload.Text = "ダウンロードの自動承認をする"
        Me.AccountForm_AutoDownload.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.AccountForm_Share_List)
        Me.GroupBox3.Controls.Add(Me.AccountForm_Share_Use)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 172)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(368, 129)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'AccountForm_Share_List
        '
        Me.AccountForm_Share_List.AllowDrop = True
        Me.AccountForm_Share_List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountForm_Share_List.ContextMenuStrip = Me.ContextMenuStrip1
        Me.AccountForm_Share_List.FormattingEnabled = True
        Me.AccountForm_Share_List.HorizontalScrollbar = True
        Me.AccountForm_Share_List.ItemHeight = 12
        Me.AccountForm_Share_List.Location = New System.Drawing.Point(6, 22)
        Me.AccountForm_Share_List.Name = "AccountForm_Share_List"
        Me.AccountForm_Share_List.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.AccountForm_Share_List.Size = New System.Drawing.Size(356, 100)
        Me.AccountForm_Share_List.TabIndex = 1
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SContext_AddFolder, Me.SContext_DeleteFolder, Me.ToolStripSeparator1, Me.SContext_CheckExist})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(299, 76)
        '
        'SContext_AddFolder
        '
        Me.SContext_AddFolder.Name = "SContext_AddFolder"
        Me.SContext_AddFolder.Size = New System.Drawing.Size(298, 22)
        Me.SContext_AddFolder.Text = "共有するフォルダを追加(&A)"
        '
        'SContext_DeleteFolder
        '
        Me.SContext_DeleteFolder.Name = "SContext_DeleteFolder"
        Me.SContext_DeleteFolder.Size = New System.Drawing.Size(298, 22)
        Me.SContext_DeleteFolder.Text = "選択したアイテムをリストから削除(&D)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(295, 6)
        '
        'SContext_CheckExist
        '
        Me.SContext_CheckExist.Name = "SContext_CheckExist"
        Me.SContext_CheckExist.Size = New System.Drawing.Size(298, 22)
        Me.SContext_CheckExist.Text = "パスが存在しないアイテムを削除する(&R)"
        '
        'AccountForm_Share_Use
        '
        Me.AccountForm_Share_Use.AutoCheck = False
        Me.AccountForm_Share_Use.AutoSize = True
        Me.AccountForm_Share_Use.Location = New System.Drawing.Point(6, 0)
        Me.AccountForm_Share_Use.Name = "AccountForm_Share_Use"
        Me.AccountForm_Share_Use.Size = New System.Drawing.Size(177, 16)
        Me.AccountForm_Share_Use.TabIndex = 0
        Me.AccountForm_Share_Use.Text = "ファイルの共有機能を有効にする"
        Me.AccountForm_Share_Use.UseVisualStyleBackColor = True
        '
        'Form_Account
        '
        Me.AcceptButton = Me.AccountForm_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 340)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.AccountForm_OK)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.AccountForm_Close)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_Account"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form_Account"
        CType(Me.AccountForm_Port, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AccountForm_AccountName As System.Windows.Forms.TextBox
    Friend WithEvents AccountForm_AttKey As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents AccountForm_Port As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AccountForm_Ip As System.Windows.Forms.TextBox
    Friend WithEvents AccountForm_Close As System.Windows.Forms.Button
    Friend WithEvents AccountForm_OK As System.Windows.Forms.Button
    Friend WithEvents AccountForm_Encrypt As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents AccountForm_SelectSavePath As System.Windows.Forms.Button
    Friend WithEvents AccountForm_SavePath As System.Windows.Forms.TextBox
    Friend WithEvents AccountForm_AutoDownload As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents AccountForm_Share_List As System.Windows.Forms.ListBox
    Friend WithEvents AccountForm_Share_Use As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SContext_AddFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SContext_DeleteFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SContext_CheckExist As System.Windows.Forms.ToolStripMenuItem
End Class
