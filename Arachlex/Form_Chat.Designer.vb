<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Chat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Chat))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.Info_YourVersion = New System.Windows.Forms.Label
        Me.Info_ReceiveEncryptData = New System.Windows.Forms.Label
        Me.Info_MyVersion = New System.Windows.Forms.Label
        Me.Info_SendEncryptData = New System.Windows.Forms.Label
        Me.Info_ConnectStartTime = New System.Windows.Forms.Label
        Me.Info_MyProtocol = New System.Windows.Forms.Label
        Me.Info_AllReceiveSize = New System.Windows.Forms.Label
        Me.Info_YourProtocol = New System.Windows.Forms.Label
        Me.Info_YourPort = New System.Windows.Forms.Label
        Me.Info_YourIP = New System.Windows.Forms.Label
        Me.Info_AllSendSize = New System.Windows.Forms.Label
        Me.Info_SendTransferSpeed = New System.Windows.Forms.Label
        Me.Info_ConnectTime = New System.Windows.Forms.Label
        Me.Info_RecTransferSpeed = New System.Windows.Forms.Label
        Me.Chat_View = New System.Windows.Forms.RichTextBox
        Me.Context_Chat = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Context_Chat_Copy = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Chat_AllSelect = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Chat_Delete = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Chat_TopMost = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Chat_ShowInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Chat_Find = New System.Windows.Forms.ToolStripMenuItem
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Chat_Find_Button = New System.Windows.Forms.Button
        Me.Chat_Find_InputBox = New System.Windows.Forms.TextBox
        Me.Chat_InputBox = New System.Windows.Forms.TextBox
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Context_Chat.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Chat_InputBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(354, 371)
        Me.SplitContainer1.SplitterDistance = 315
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.AutoScroll = True
        Me.SplitContainer2.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_YourVersion)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_ReceiveEncryptData)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_MyVersion)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_SendEncryptData)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_ConnectStartTime)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_MyProtocol)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_AllReceiveSize)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_YourProtocol)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_YourPort)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_YourIP)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_AllSendSize)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_SendTransferSpeed)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_ConnectTime)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Info_RecTransferSpeed)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Chat_View)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Size = New System.Drawing.Size(354, 315)
        Me.SplitContainer2.SplitterDistance = 153
        Me.SplitContainer2.TabIndex = 0
        '
        'Info_YourVersion
        '
        Me.Info_YourVersion.Location = New System.Drawing.Point(3, 89)
        Me.Info_YourVersion.Name = "Info_YourVersion"
        Me.Info_YourVersion.Size = New System.Drawing.Size(170, 20)
        Me.Info_YourVersion.TabIndex = 4
        Me.Info_YourVersion.Tag = ""
        Me.Info_YourVersion.Text = "相手のバージョン"
        Me.Info_YourVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_ReceiveEncryptData
        '
        Me.Info_ReceiveEncryptData.Location = New System.Drawing.Point(3, 129)
        Me.Info_ReceiveEncryptData.Name = "Info_ReceiveEncryptData"
        Me.Info_ReceiveEncryptData.Size = New System.Drawing.Size(170, 20)
        Me.Info_ReceiveEncryptData.TabIndex = 5
        Me.Info_ReceiveEncryptData.Tag = ""
        Me.Info_ReceiveEncryptData.Text = "暗号化データの受信回数"
        Me.Info_ReceiveEncryptData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_MyVersion
        '
        Me.Info_MyVersion.Location = New System.Drawing.Point(3, 109)
        Me.Info_MyVersion.Name = "Info_MyVersion"
        Me.Info_MyVersion.Size = New System.Drawing.Size(170, 20)
        Me.Info_MyVersion.TabIndex = 5
        Me.Info_MyVersion.Tag = ""
        Me.Info_MyVersion.Text = "自身のバージョン"
        Me.Info_MyVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_SendEncryptData
        '
        Me.Info_SendEncryptData.Location = New System.Drawing.Point(179, 129)
        Me.Info_SendEncryptData.Name = "Info_SendEncryptData"
        Me.Info_SendEncryptData.Size = New System.Drawing.Size(170, 20)
        Me.Info_SendEncryptData.TabIndex = 11
        Me.Info_SendEncryptData.Text = "暗号化データの送信回数"
        Me.Info_SendEncryptData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_ConnectStartTime
        '
        Me.Info_ConnectStartTime.Location = New System.Drawing.Point(3, 9)
        Me.Info_ConnectStartTime.Name = "Info_ConnectStartTime"
        Me.Info_ConnectStartTime.Size = New System.Drawing.Size(170, 20)
        Me.Info_ConnectStartTime.TabIndex = 0
        Me.Info_ConnectStartTime.Tag = ""
        Me.Info_ConnectStartTime.Text = "通信開始時間"
        Me.Info_ConnectStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_MyProtocol
        '
        Me.Info_MyProtocol.Location = New System.Drawing.Point(179, 109)
        Me.Info_MyProtocol.Name = "Info_MyProtocol"
        Me.Info_MyProtocol.Size = New System.Drawing.Size(170, 20)
        Me.Info_MyProtocol.TabIndex = 11
        Me.Info_MyProtocol.Text = "自身のプロトコル"
        Me.Info_MyProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_AllReceiveSize
        '
        Me.Info_AllReceiveSize.Location = New System.Drawing.Point(3, 29)
        Me.Info_AllReceiveSize.Name = "Info_AllReceiveSize"
        Me.Info_AllReceiveSize.Size = New System.Drawing.Size(170, 20)
        Me.Info_AllReceiveSize.TabIndex = 1
        Me.Info_AllReceiveSize.Tag = ""
        Me.Info_AllReceiveSize.Text = "総受信量"
        Me.Info_AllReceiveSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_YourProtocol
        '
        Me.Info_YourProtocol.Location = New System.Drawing.Point(179, 89)
        Me.Info_YourProtocol.Name = "Info_YourProtocol"
        Me.Info_YourProtocol.Size = New System.Drawing.Size(170, 20)
        Me.Info_YourProtocol.TabIndex = 10
        Me.Info_YourProtocol.Text = "相手のプロトコル"
        Me.Info_YourProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_YourPort
        '
        Me.Info_YourPort.Location = New System.Drawing.Point(179, 69)
        Me.Info_YourPort.Name = "Info_YourPort"
        Me.Info_YourPort.Size = New System.Drawing.Size(170, 20)
        Me.Info_YourPort.TabIndex = 9
        Me.Info_YourPort.Text = "相手のポート番号"
        Me.Info_YourPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_YourIP
        '
        Me.Info_YourIP.Location = New System.Drawing.Point(3, 69)
        Me.Info_YourIP.Name = "Info_YourIP"
        Me.Info_YourIP.Size = New System.Drawing.Size(170, 20)
        Me.Info_YourIP.TabIndex = 3
        Me.Info_YourIP.Tag = ""
        Me.Info_YourIP.Text = "相手のIP"
        Me.Info_YourIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_AllSendSize
        '
        Me.Info_AllSendSize.Location = New System.Drawing.Point(179, 29)
        Me.Info_AllSendSize.Name = "Info_AllSendSize"
        Me.Info_AllSendSize.Size = New System.Drawing.Size(170, 20)
        Me.Info_AllSendSize.TabIndex = 7
        Me.Info_AllSendSize.Text = "総送信量"
        Me.Info_AllSendSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_SendTransferSpeed
        '
        Me.Info_SendTransferSpeed.Location = New System.Drawing.Point(179, 49)
        Me.Info_SendTransferSpeed.Name = "Info_SendTransferSpeed"
        Me.Info_SendTransferSpeed.Size = New System.Drawing.Size(170, 20)
        Me.Info_SendTransferSpeed.TabIndex = 8
        Me.Info_SendTransferSpeed.Text = "送信速度"
        Me.Info_SendTransferSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_ConnectTime
        '
        Me.Info_ConnectTime.Location = New System.Drawing.Point(179, 9)
        Me.Info_ConnectTime.Name = "Info_ConnectTime"
        Me.Info_ConnectTime.Size = New System.Drawing.Size(170, 20)
        Me.Info_ConnectTime.TabIndex = 6
        Me.Info_ConnectTime.Text = "通信時間"
        Me.Info_ConnectTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_RecTransferSpeed
        '
        Me.Info_RecTransferSpeed.Location = New System.Drawing.Point(3, 49)
        Me.Info_RecTransferSpeed.Name = "Info_RecTransferSpeed"
        Me.Info_RecTransferSpeed.Size = New System.Drawing.Size(170, 20)
        Me.Info_RecTransferSpeed.TabIndex = 2
        Me.Info_RecTransferSpeed.Tag = ""
        Me.Info_RecTransferSpeed.Text = "受信速度"
        Me.Info_RecTransferSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Chat_View
        '
        Me.Chat_View.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Chat_View.ContextMenuStrip = Me.Context_Chat
        Me.Chat_View.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Chat_View.HideSelection = False
        Me.Chat_View.Location = New System.Drawing.Point(0, 0)
        Me.Chat_View.Name = "Chat_View"
        Me.Chat_View.Size = New System.Drawing.Size(354, 130)
        Me.Chat_View.TabIndex = 0
        Me.Chat_View.Text = ""
        '
        'Context_Chat
        '
        Me.Context_Chat.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Context_Chat_Copy, Me.Context_Chat_AllSelect, Me.Context_Chat_Delete, Me.ToolStripSeparator1, Me.Context_Chat_TopMost, Me.Context_Chat_ShowInfo, Me.Context_Chat_Find})
        Me.Context_Chat.Name = "Context_Chat"
        Me.Context_Chat.Size = New System.Drawing.Size(181, 142)
        '
        'Context_Chat_Copy
        '
        Me.Context_Chat_Copy.Name = "Context_Chat_Copy"
        Me.Context_Chat_Copy.Size = New System.Drawing.Size(180, 22)
        Me.Context_Chat_Copy.Text = "コピー(&C)"
        '
        'Context_Chat_AllSelect
        '
        Me.Context_Chat_AllSelect.Name = "Context_Chat_AllSelect"
        Me.Context_Chat_AllSelect.Size = New System.Drawing.Size(180, 22)
        Me.Context_Chat_AllSelect.Text = "全て選択(&A)"
        '
        'Context_Chat_Delete
        '
        Me.Context_Chat_Delete.Name = "Context_Chat_Delete"
        Me.Context_Chat_Delete.Size = New System.Drawing.Size(180, 22)
        Me.Context_Chat_Delete.Text = "全て削除(&D)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(177, 6)
        '
        'Context_Chat_TopMost
        '
        Me.Context_Chat_TopMost.Name = "Context_Chat_TopMost"
        Me.Context_Chat_TopMost.Size = New System.Drawing.Size(180, 22)
        Me.Context_Chat_TopMost.Text = "常に手前に表示(&M)"
        '
        'Context_Chat_ShowInfo
        '
        Me.Context_Chat_ShowInfo.Name = "Context_Chat_ShowInfo"
        Me.Context_Chat_ShowInfo.Size = New System.Drawing.Size(180, 22)
        Me.Context_Chat_ShowInfo.Text = "通信情報の表示(&T)"
        '
        'Context_Chat_Find
        '
        Me.Context_Chat_Find.Name = "Context_Chat_Find"
        Me.Context_Chat_Find.Size = New System.Drawing.Size(180, 22)
        Me.Context_Chat_Find.Text = "検索(&F)"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Chat_Find_Button)
        Me.Panel1.Controls.Add(Me.Chat_Find_InputBox)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 130)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(354, 28)
        Me.Panel1.TabIndex = 1
        '
        'Chat_Find_Button
        '
        Me.Chat_Find_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Chat_Find_Button.Location = New System.Drawing.Point(286, 3)
        Me.Chat_Find_Button.Name = "Chat_Find_Button"
        Me.Chat_Find_Button.Size = New System.Drawing.Size(65, 23)
        Me.Chat_Find_Button.TabIndex = 1
        Me.Chat_Find_Button.Text = "検索"
        Me.Chat_Find_Button.UseVisualStyleBackColor = True
        '
        'Chat_Find_InputBox
        '
        Me.Chat_Find_InputBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Chat_Find_InputBox.Location = New System.Drawing.Point(3, 5)
        Me.Chat_Find_InputBox.Name = "Chat_Find_InputBox"
        Me.Chat_Find_InputBox.Size = New System.Drawing.Size(277, 19)
        Me.Chat_Find_InputBox.TabIndex = 0
        '
        'Chat_InputBox
        '
        Me.Chat_InputBox.AllowDrop = True
        Me.Chat_InputBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Chat_InputBox.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.Chat_InputBox.Location = New System.Drawing.Point(0, 0)
        Me.Chat_InputBox.Multiline = True
        Me.Chat_InputBox.Name = "Chat_InputBox"
        Me.Chat_InputBox.Size = New System.Drawing.Size(354, 52)
        Me.Chat_InputBox.TabIndex = 0
        '
        'Form_Chat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 371)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form_Chat"
        Me.Text = "Form_Chat"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.Context_Chat.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Chat_View As System.Windows.Forms.RichTextBox
    Friend WithEvents Chat_InputBox As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Context_Chat As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Info_YourVersion As System.Windows.Forms.Label
    Friend WithEvents Info_MyVersion As System.Windows.Forms.Label
    Friend WithEvents Info_ConnectStartTime As System.Windows.Forms.Label
    Friend WithEvents Info_MyProtocol As System.Windows.Forms.Label
    Friend WithEvents Info_AllReceiveSize As System.Windows.Forms.Label
    Friend WithEvents Info_YourProtocol As System.Windows.Forms.Label
    Friend WithEvents Info_YourPort As System.Windows.Forms.Label
    Friend WithEvents Info_YourIP As System.Windows.Forms.Label
    Friend WithEvents Info_AllSendSize As System.Windows.Forms.Label
    Friend WithEvents Info_SendTransferSpeed As System.Windows.Forms.Label
    Friend WithEvents Info_ConnectTime As System.Windows.Forms.Label
    Friend WithEvents Info_RecTransferSpeed As System.Windows.Forms.Label
    Friend WithEvents Context_Chat_Copy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Chat_AllSelect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Chat_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Context_Chat_ShowInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Context_Chat_Find As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Chat_Find_Button As System.Windows.Forms.Button
    Friend WithEvents Chat_Find_InputBox As System.Windows.Forms.TextBox
    Friend WithEvents Context_Chat_TopMost As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Info_ReceiveEncryptData As System.Windows.Forms.Label
    Friend WithEvents Info_SendEncryptData As System.Windows.Forms.Label
End Class
