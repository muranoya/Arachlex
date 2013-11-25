''' <summary>
''' ダブルバッファされるリストビュー
''' </summary>
''' <remarks></remarks>
Public Class DoubleBufferedListView
    Inherits System.Windows.Forms.ListView

    Public Sub New()
        MyBase.New()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.DoubleBuffered = True
        Me.TileSize = New Size(200, 50)
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ResumeLayout(False)

    End Sub
End Class