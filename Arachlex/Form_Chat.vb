Imports Arachlex.TCPNetworkClass
Imports Arachlex.DefinitionClass
Public Class Form_Chat

    Private ControledAccount As AccountListViewItem

    Private Sub Form_Chat_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Chat_InputBox.Focus()
    End Sub

    Private Sub Form_Chat_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        With ControledAccount.IndividualData
            .Chat_SplitDistanceInputBoxWithView = SplitContainer1.SplitterDistance
            .Chat_SplitDistanceViewWithInfo = SplitContainer2.SplitterDistance
            .Chat_ShowFindBox = Panel1.Visible
            .Chat_ShowInfo = Context_Chat_ShowInfo.Checked

            .Chat_Location = Me.Location
            .Chat_Size = Me.Size
        End With

        'チャットデータの保存
        ControledAccount.UserChat = Chat_View.Rtf

        'インスタンスの削除
        ControledAccount.ChatWindow = Nothing
    End Sub
    Private Sub Form_Chat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        With ControledAccount.IndividualData
            Me.Size = .Chat_Size
            Me.Location = .Chat_Location
        End With

        Me.Text = ControledAccount.IndividualData.AccountName

        With ControledAccount.IndividualData
            '設定の適用
            Context_Chat_ShowInfo.Checked = .Chat_ShowInfo
            SplitContainer2.Panel1Collapsed = Not .Chat_ShowInfo
            Context_Chat_Find.Checked = .Chat_ShowFindBox
            Panel1.Visible = .Chat_ShowFindBox
            SplitContainer1.SplitterDistance = .Chat_SplitDistanceInputBoxWithView
            SplitContainer2.SplitterDistance = .Chat_SplitDistanceViewWithInfo
        End With

        '言語を適用
        With ControledAccount.ControledForm.lang
            Chat_Find_Button.Text = .ChatForm_SearchButton
            Context_Chat_Copy.Text = .ChatForm_CView_Copy
            Context_Chat_AllSelect.Text = .ChatForm_CView_AllSelect
            Context_Chat_Delete.Text = .ChatForm_CView_AllDelete
            Context_Chat_TopMost.Text = .ChatForm_CView_AlwaysOnTop
            Context_Chat_ShowInfo.Text = .ChatForm_CView_ShowInfomation
            Context_Chat_Find.Text = .ChatForm_CView_Search
        End With

        'チャットの復元
        Chat_View.Rtf = ControledAccount.UserChat
    End Sub
    Private Sub Form_Chat_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Chat_InputBox.Focus()
    End Sub

    Private Sub Context_Chat_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Context_Chat.Opening
        Context_Chat_Copy.Enabled = Chat_View.SelectionLength > 0
    End Sub
    Private Sub Context_Chat_Copy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Chat_Copy.Click
        If Chat_View.SelectionLength > 0 Then
            Clipboard.SetText(Replace(Chat_View.SelectedText, vbLf, vbCrLf))
        End If
    End Sub
    Private Sub Context_Chat_AllSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Chat_AllSelect.Click
        Chat_View.SelectAll()
    End Sub
    Private Sub Context_Chat_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Chat_Delete.Click
        Chat_View.Clear()
    End Sub
    Private Sub Context_Chat_TopMost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Chat_TopMost.Click
        Me.TopMost = Not Me.TopMost
        Context_Chat_TopMost.Checked = Me.TopMost
    End Sub
    Private Sub Context_Chat_ShowInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Chat_ShowInfo.Click
        Context_Chat_ShowInfo.Checked = Not Context_Chat_ShowInfo.Checked
        SplitContainer2.Panel1Collapsed = Not Context_Chat_ShowInfo.Checked

        '設定を変える。Form1のタイマーで更新しているため、ここで情報を表示しているのか知らせないと負荷が高くなるおそれがある
        ControledAccount.IndividualData.Chat_ShowInfo = Context_Chat_ShowInfo.Checked
    End Sub
    Private Sub Context_Chat_Find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Context_Chat_Find.Click
        '設定はウィンドウを閉じたときに保存するため、ここでは外面の操作のみ
        Context_Chat_Find.Checked = Not Context_Chat_Find.Checked
        Panel1.Visible = Context_Chat_Find.Checked

        If Panel1.Visible Then
            Chat_Find_InputBox.Focus()
        End If
    End Sub

    Private Sub Chat_View_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles Chat_View.LinkClicked
        Try
            Process.Start(e.LinkText)
        Catch ex As Exception
            MessageBox.Show(ex.Message, SoftwareName, MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub Chat_View_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chat_View.Resize
        Chat_View.SelectionStart = Chat_View.TextLength
        Chat_View.ScrollToCaret()
    End Sub

    Private Sub Chat_InputBox_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Chat_InputBox.DragDrop
        If ControledAccount IsNot Nothing AndAlso ControledAccount.TCPConnect.AceptInfo Then
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                'ファイルの場合
                Dim fileName As String() = CType(e.Data.GetData(DataFormats.FileDrop, False), String())
                For i As Integer = 0 To fileName.Length - 1
                    If Not ControledAccount.UploadItem(fileName(i), True) Then
                        Exit For
                    End If
                Next
            ElseIf e.Data.GetDataPresent(DataFormats.Text) Then
                'テキストの場合
                Chat_InputBox.AppendText(CStr(e.Data.GetData(DataFormats.Text, False)))
            End If
        End If
    End Sub
    Private Sub Chat_InputBox_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Chat_InputBox.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) OrElse e.Data.GetDataPresent(DataFormats.Text) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub Chat_InputBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chat_InputBox.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Not e.Shift AndAlso ControledAccount IsNot Nothing AndAlso ControledAccount.TCPConnect.AceptInfo Then
            If ControledAccount.TCPConnect.AceptInfo AndAlso Chat_InputBox.TextLength > 0 Then
                ControledAccount.TCPConnect.SendPacket(ControledAccount.ControledForm.appSettings.Account_AccountName & vbCrLf & Chat_InputBox.Text, TCP_Header.ChatMSG, 0)
                ControledAccount.ApendChat(Chat_InputBox.Text, ControledAccount.ControledForm.appSettings.Account_AccountName, Now, True)
                Chat_InputBox.Clear()
            End If
            e.SuppressKeyPress = True
            e.Handled = True
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Chat_Find_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chat_Find_Button.Click
        Try
            Dim startInt As Integer = 0
            If Chat_View.TextLength <> Chat_View.SelectionStart Then
                startInt = Chat_View.SelectionStart + 1
            End If
            Chat_View.Select(Chat_View.Text.IndexOf(Chat_Find_InputBox.Text, startInt, StringComparison.InvariantCultureIgnoreCase), Chat_Find_InputBox.Text.Length)
        Catch
            Try
                Chat_View.Select(Chat_View.Text.IndexOf(Chat_Find_InputBox.Text, 0, StringComparison.InvariantCultureIgnoreCase), Chat_Find_InputBox.Text.Length)
            Catch
                Chat_View.SelectionStart = Chat_View.TextLength
            End Try
        End Try
    End Sub

    Public Sub New(ByVal cAccount As AccountListViewItem)
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        ControledAccount = cAccount
    End Sub
End Class