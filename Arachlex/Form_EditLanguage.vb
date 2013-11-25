Public Class Form_EditLanguage

    Private Sub Menu_File_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_File_Exit.Click
        Me.Close()
    End Sub

    Private Sub Menu_File_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_File_Export.Click
        Dim sfd As New SaveFileDialog()
        sfd.FileName = "lang.arachlex_language"
        sfd.InitialDirectory = Application.StartupPath
        sfd.Filter = "Arachlex_Language(*.arachlex_language)|*.arachlex_language"
        sfd.Title = "Please select a location"
        sfd.RestoreDirectory = True
        sfd.OverwritePrompt = True
        sfd.CheckPathExists = True

        'ダイアログを表示する
        If sfd.ShowDialog() = DialogResult.OK Then
            Try
                Dim bf1 As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
                Dim fs1 As New System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
                bf1.Serialize(fs1, CType(PropertyGrid1.SelectedObject, LanguageClass))
                fs1.Close()
            Catch ex As Exception
                MessageBox.Show("Failed to save file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class