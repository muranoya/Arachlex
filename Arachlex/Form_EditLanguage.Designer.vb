<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_EditLanguage
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.Menu_File = New System.Windows.Forms.ToolStripMenuItem
        Me.Menu_File_Export = New System.Windows.Forms.ToolStripMenuItem
        Me.Menu_File_Exit = New System.Windows.Forms.ToolStripMenuItem
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_File})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(341, 26)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'Menu_File
        '
        Me.Menu_File.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_File_Export, Me.Menu_File_Exit})
        Me.Menu_File.Name = "Menu_File"
        Me.Menu_File.Size = New System.Drawing.Size(40, 22)
        Me.Menu_File.Text = "File"
        '
        'Menu_File_Export
        '
        Me.Menu_File_Export.Name = "Menu_File_Export"
        Me.Menu_File_Export.Size = New System.Drawing.Size(114, 22)
        Me.Menu_File_Export.Text = "Export"
        '
        'Menu_File_Exit
        '
        Me.Menu_File_Exit.Name = "Menu_File_Exit"
        Me.Menu_File_Exit.Size = New System.Drawing.Size(114, 22)
        Me.Menu_File_Exit.Text = "Exit"
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 26)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(341, 369)
        Me.PropertyGrid1.TabIndex = 1
        '
        'Form_EditLanguage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(341, 395)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form_EditLanguage"
        Me.Text = "EditLanguage"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents Menu_File As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_File_Export As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents Menu_File_Exit As System.Windows.Forms.ToolStripMenuItem
End Class
