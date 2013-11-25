<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Download", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Upload", System.Windows.Forms.HorizontalAlignment.Left)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Context_Account = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Context_Account_Connect = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Account_Chat = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Account_Add = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Account_Edit = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Account_Delete = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Account_Sort = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Account_Sort_AccountName = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Account_Sort_LoginState = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Account_Setting = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Context_Transfer_Open = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Transfer_QueueUp = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_QueueDown = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Transfer_Stop = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_ReStart = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Transfer_Interruption = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_Delete = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Transfer_Sort = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_Sort_OtherPoint = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_Sort_Queue = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_Sort_FileName = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_Sort_FullPath = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_Sort_Size = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_Sort_Status = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Transfer_ListMode = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_FindBox = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Transfer_AllSelect = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Share = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Context_Share_Download = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Share_Up = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Share_Refresh = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Share_Root = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Share_Sort = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Share_Sort_FileName = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Share_Sort_Extension = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Share_Sort_Size = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Share_Sort_TimeStamp = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.Context_Share_FindBox = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Share_AllSelect = New System.Windows.Forms.ToolStripMenuItem
        Me.Share_SmallImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ConnectCheck = New System.Windows.Forms.Timer(Me.components)
        Me.bgServer = New System.ComponentModel.BackgroundWorker
        Me.RefreshStatus = New System.Windows.Forms.Timer(Me.components)
        Me.AccountImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.Share_LargeImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.MainSplitContainer = New System.Windows.Forms.SplitContainer
        Me.Account_ListView = New Arachlex.DoubleBufferedListView
        Me.AColumn_Name = New System.Windows.Forms.ColumnHeader
        Me.AColumn_Status = New System.Windows.Forms.ColumnHeader
        Me.Info_Speedometer = New System.Windows.Forms.Label
        Me.Info_Port = New System.Windows.Forms.Label
        Me.Panel_Share = New System.Windows.Forms.Panel
        Me.Share_ListView = New Arachlex.DoubleBufferedListView
        Me.SColumn_Filename = New System.Windows.Forms.ColumnHeader
        Me.SColumn_Extension = New System.Windows.Forms.ColumnHeader
        Me.SColumn_Size = New System.Windows.Forms.ColumnHeader
        Me.SColumn_TimeStamp = New System.Windows.Forms.ColumnHeader
        Me.ShareToolStrip = New System.Windows.Forms.ToolStrip
        Me.Share_Download = New System.Windows.Forms.ToolStripButton
        Me.Share_Up = New System.Windows.Forms.ToolStripButton
        Me.Share_Refresh = New System.Windows.Forms.ToolStripButton
        Me.Share_Root = New System.Windows.Forms.ToolStripButton
        Me.Share_PathText = New System.Windows.Forms.ToolStripTextBox
        Me.Share_GoTransferList = New System.Windows.Forms.ToolStripButton
        Me.Panel_Transfer = New System.Windows.Forms.Panel
        Me.Transfer_ListView = New Arachlex.DoubleBufferedListView
        Me.TColumn_Remote = New System.Windows.Forms.ColumnHeader
        Me.TColumn_Priority = New System.Windows.Forms.ColumnHeader
        Me.TColumn_FileName = New System.Windows.Forms.ColumnHeader
        Me.TColumn_FullPath = New System.Windows.Forms.ColumnHeader
        Me.TColumn_Size = New System.Windows.Forms.ColumnHeader
        Me.TColumn_Status = New System.Windows.Forms.ColumnHeader
        Me.TransferToolStrip = New System.Windows.Forms.ToolStrip
        Me.Transfer_Approval = New System.Windows.Forms.ToolStripDropDownButton
        Me.Transfer_Approval_Approval = New System.Windows.Forms.ToolStripMenuItem
        Me.Transfer_Approval_AllApproval = New System.Windows.Forms.ToolStripMenuItem
        Me.Transfer_Approval_Resume = New System.Windows.Forms.ToolStripMenuItem
        Me.Transfer_Upload = New System.Windows.Forms.ToolStripDropDownButton
        Me.Transfer_Upload_File = New System.Windows.Forms.ToolStripMenuItem
        Me.Transfer_Upload_Folder = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
        Me.Transfer_Stop = New System.Windows.Forms.ToolStripButton
        Me.Transfer_Restart = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.Transfer_Interruption = New System.Windows.Forms.ToolStripButton
        Me.Transfer_Delete = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator
        Me.Transfer_DoneDelete = New System.Windows.Forms.ToolStripButton
        Me.Transfer_GoShareList = New System.Windows.Forms.ToolStripButton
        Me.Panel_Find = New System.Windows.Forms.Panel
        Me.Find_Button = New System.Windows.Forms.Button
        Me.Find_TextBox = New System.Windows.Forms.TextBox
        Me.Context_Account_Reconnect = New System.Windows.Forms.ToolStripMenuItem
        Me.Context_Account.SuspendLayout()
        Me.Context_Transfer.SuspendLayout()
        Me.Context_Share.SuspendLayout()
        Me.MainSplitContainer.Panel1.SuspendLayout()
        Me.MainSplitContainer.Panel2.SuspendLayout()
        Me.MainSplitContainer.SuspendLayout()
        Me.Panel_Share.SuspendLayout()
        Me.ShareToolStrip.SuspendLayout()
        Me.Panel_Transfer.SuspendLayout()
        Me.TransferToolStrip.SuspendLayout()
        Me.Panel_Find.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'Context_Account
        '
        Me.Context_Account.DropShadowEnabled = False
        Me.Context_Account.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Context_Account_Connect, Me.Context_Account_Reconnect, Me.Context_Account_Chat, Me.ToolStripSeparator1, Me.Context_Account_Add, Me.Context_Account_Edit, Me.Context_Account_Delete, Me.ToolStripSeparator2, Me.Context_Account_Sort, Me.ToolStripSeparator13, Me.Context_Account_Setting})
        Me.Context_Account.Name = "ContextMenuStrip1"
        Me.Context_Account.ShowImageMargin = False
        Me.Context_Account.Size = New System.Drawing.Size(203, 220)
        '
        'Context_Account_Connect
        '
        Me.Context_Account_Connect.Enabled = False
        Me.Context_Account_Connect.Name = "Context_Account_Connect"
        Me.Context_Account_Connect.Size = New System.Drawing.Size(202, 22)
        Me.Context_Account_Connect.Text = "接続(&C)"
        '
        'Context_Account_Chat
        '
        Me.Context_Account_Chat.Enabled = False
        Me.Context_Account_Chat.Name = "Context_Account_Chat"
        Me.Context_Account_Chat.Size = New System.Drawing.Size(202, 22)
        Me.Context_Account_Chat.Text = "チャット(&H)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(199, 6)
        '
        'Context_Account_Add
        '
        Me.Context_Account_Add.Name = "Context_Account_Add"
        Me.Context_Account_Add.Size = New System.Drawing.Size(202, 22)
        Me.Context_Account_Add.Text = "接続先アカウントの追加(&A)"
        '
        'Context_Account_Edit
        '
        Me.Context_Account_Edit.Enabled = False
        Me.Context_Account_Edit.Name = "Context_Account_Edit"
        Me.Context_Account_Edit.Size = New System.Drawing.Size(202, 22)
        Me.Context_Account_Edit.Text = "接続先アカウントの編集(&E)"
        '
        'Context_Account_Delete
        '
        Me.Context_Account_Delete.Enabled = False
        Me.Context_Account_Delete.Name = "Context_Account_Delete"
        Me.Context_Account_Delete.Size = New System.Drawing.Size(202, 22)
        Me.Context_Account_Delete.Text = "接続先アカウントの削除(&D)"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(199, 6)
        '
        'Context_Account_Sort
        '
        Me.Context_Account_Sort.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Context_Account_Sort_AccountName, Me.Context_Account_Sort_LoginState})
        Me.Context_Account_Sort.Name = "Context_Account_Sort"
        Me.Context_Account_Sort.Size = New System.Drawing.Size(202, 22)
        Me.Context_Account_Sort.Text = "並び替え(&S)"
        '
        'Context_Account_Sort_AccountName
        '
        Me.Context_Account_Sort_AccountName.Name = "Context_Account_Sort_AccountName"
        Me.Context_Account_Sort_AccountName.Size = New System.Drawing.Size(167, 22)
        Me.Context_Account_Sort_AccountName.Text = "アカウント名(&N)"
        '
        'Context_Account_Sort_LoginState
        '
        Me.Context_Account_Sort_LoginState.Name = "Context_Account_Sort_LoginState"
        Me.Context_Account_Sort_LoginState.Size = New System.Drawing.Size(167, 22)
        Me.Context_Account_Sort_LoginState.Text = "ログイン状態(&L)"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(199, 6)
        '
        'Context_Account_Setting
        '
        Me.Context_Account_Setting.Name = "Context_Account_Setting"
        Me.Context_Account_Setting.Size = New System.Drawing.Size(202, 22)
        Me.Context_Account_Setting.Text = "設定(&T)"
        '
        'Context_Transfer
        '
        Me.Context_Transfer.DropShadowEnabled = False
        Me.Context_Transfer.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Context_Transfer_Open, Me.ToolStripSeparator4, Me.Context_Transfer_QueueUp, Me.Context_Transfer_QueueDown, Me.ToolStripSeparator3, Me.Context_Transfer_Stop, Me.Context_Transfer_ReStart, Me.ToolStripSeparator5, Me.Context_Transfer_Interruption, Me.Context_Transfer_Delete, Me.ToolStripSeparator6, Me.Context_Transfer_Sort, Me.ToolStripSeparator7, Me.Context_Transfer_ListMode, Me.Context_Transfer_FindBox, Me.Context_Transfer_AllSelect})
        Me.Context_Transfer.Name = "TransferListMenu"
        Me.Context_Transfer.Size = New System.Drawing.Size(243, 276)
        '
        'Context_Transfer_Open
        '
        Me.Context_Transfer_Open.Enabled = False
        Me.Context_Transfer_Open.Name = "Context_Transfer_Open"
        Me.Context_Transfer_Open.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_Open.Text = "開く(&O)"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(239, 6)
        '
        'Context_Transfer_QueueUp
        '
        Me.Context_Transfer_QueueUp.Enabled = False
        Me.Context_Transfer_QueueUp.Image = CType(resources.GetObject("Context_Transfer_QueueUp.Image"), System.Drawing.Image)
        Me.Context_Transfer_QueueUp.Name = "Context_Transfer_QueueUp"
        Me.Context_Transfer_QueueUp.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.Context_Transfer_QueueUp.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_QueueUp.Text = "優先順位を上げる(&U)"
        '
        'Context_Transfer_QueueDown
        '
        Me.Context_Transfer_QueueDown.Enabled = False
        Me.Context_Transfer_QueueDown.Image = CType(resources.GetObject("Context_Transfer_QueueDown.Image"), System.Drawing.Image)
        Me.Context_Transfer_QueueDown.Name = "Context_Transfer_QueueDown"
        Me.Context_Transfer_QueueDown.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.Context_Transfer_QueueDown.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_QueueDown.Text = "優先順位を下げる(&N)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(239, 6)
        '
        'Context_Transfer_Stop
        '
        Me.Context_Transfer_Stop.Enabled = False
        Me.Context_Transfer_Stop.Image = CType(resources.GetObject("Context_Transfer_Stop.Image"), System.Drawing.Image)
        Me.Context_Transfer_Stop.Name = "Context_Transfer_Stop"
        Me.Context_Transfer_Stop.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_Stop.Text = "一時停止(&F)"
        '
        'Context_Transfer_ReStart
        '
        Me.Context_Transfer_ReStart.Enabled = False
        Me.Context_Transfer_ReStart.Image = CType(resources.GetObject("Context_Transfer_ReStart.Image"), System.Drawing.Image)
        Me.Context_Transfer_ReStart.Name = "Context_Transfer_ReStart"
        Me.Context_Transfer_ReStart.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_ReStart.Text = "再開(&R)"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(239, 6)
        '
        'Context_Transfer_Interruption
        '
        Me.Context_Transfer_Interruption.Enabled = False
        Me.Context_Transfer_Interruption.Image = CType(resources.GetObject("Context_Transfer_Interruption.Image"), System.Drawing.Image)
        Me.Context_Transfer_Interruption.Name = "Context_Transfer_Interruption"
        Me.Context_Transfer_Interruption.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.Context_Transfer_Interruption.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_Interruption.Text = "中断(&I)"
        '
        'Context_Transfer_Delete
        '
        Me.Context_Transfer_Delete.Enabled = False
        Me.Context_Transfer_Delete.Image = CType(resources.GetObject("Context_Transfer_Delete.Image"), System.Drawing.Image)
        Me.Context_Transfer_Delete.Name = "Context_Transfer_Delete"
        Me.Context_Transfer_Delete.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_Delete.Text = "ディスクから削除(&D)"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(239, 6)
        '
        'Context_Transfer_Sort
        '
        Me.Context_Transfer_Sort.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Context_Transfer_Sort_OtherPoint, Me.Context_Transfer_Sort_Queue, Me.Context_Transfer_Sort_FileName, Me.Context_Transfer_Sort_FullPath, Me.Context_Transfer_Sort_Size, Me.Context_Transfer_Sort_Status})
        Me.Context_Transfer_Sort.Name = "Context_Transfer_Sort"
        Me.Context_Transfer_Sort.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_Sort.Text = "並び替え(&S)"
        '
        'Context_Transfer_Sort_OtherPoint
        '
        Me.Context_Transfer_Sort_OtherPoint.Name = "Context_Transfer_Sort_OtherPoint"
        Me.Context_Transfer_Sort_OtherPoint.Size = New System.Drawing.Size(155, 22)
        Me.Context_Transfer_Sort_OtherPoint.Text = "相手(&T)"
        '
        'Context_Transfer_Sort_Queue
        '
        Me.Context_Transfer_Sort_Queue.Name = "Context_Transfer_Sort_Queue"
        Me.Context_Transfer_Sort_Queue.Size = New System.Drawing.Size(155, 22)
        Me.Context_Transfer_Sort_Queue.Text = "キュー(&Q)"
        '
        'Context_Transfer_Sort_FileName
        '
        Me.Context_Transfer_Sort_FileName.Name = "Context_Transfer_Sort_FileName"
        Me.Context_Transfer_Sort_FileName.Size = New System.Drawing.Size(155, 22)
        Me.Context_Transfer_Sort_FileName.Text = "ファイル名(&F)"
        '
        'Context_Transfer_Sort_FullPath
        '
        Me.Context_Transfer_Sort_FullPath.Name = "Context_Transfer_Sort_FullPath"
        Me.Context_Transfer_Sort_FullPath.Size = New System.Drawing.Size(155, 22)
        Me.Context_Transfer_Sort_FullPath.Text = "フルパス(&L)"
        '
        'Context_Transfer_Sort_Size
        '
        Me.Context_Transfer_Sort_Size.Name = "Context_Transfer_Sort_Size"
        Me.Context_Transfer_Sort_Size.Size = New System.Drawing.Size(155, 22)
        Me.Context_Transfer_Sort_Size.Text = "サイズ(&S)"
        '
        'Context_Transfer_Sort_Status
        '
        Me.Context_Transfer_Sort_Status.Name = "Context_Transfer_Sort_Status"
        Me.Context_Transfer_Sort_Status.Size = New System.Drawing.Size(155, 22)
        Me.Context_Transfer_Sort_Status.Text = "ステータス(&U)"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(239, 6)
        '
        'Context_Transfer_ListMode
        '
        Me.Context_Transfer_ListMode.Name = "Context_Transfer_ListMode"
        Me.Context_Transfer_ListMode.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_ListMode.Text = "リストモード(&L)"
        '
        'Context_Transfer_FindBox
        '
        Me.Context_Transfer_FindBox.Name = "Context_Transfer_FindBox"
        Me.Context_Transfer_FindBox.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.Context_Transfer_FindBox.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_FindBox.Text = "検索ボックス(&B)"
        '
        'Context_Transfer_AllSelect
        '
        Me.Context_Transfer_AllSelect.Name = "Context_Transfer_AllSelect"
        Me.Context_Transfer_AllSelect.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.Context_Transfer_AllSelect.Size = New System.Drawing.Size(242, 22)
        Me.Context_Transfer_AllSelect.Text = "全て選択(&A)"
        '
        'Context_Share
        '
        Me.Context_Share.DropShadowEnabled = False
        Me.Context_Share.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Context_Share_Download, Me.Context_Share_Up, Me.Context_Share_Refresh, Me.Context_Share_Root, Me.ToolStripSeparator8, Me.Context_Share_Sort, Me.ToolStripSeparator9, Me.Context_Share_FindBox, Me.Context_Share_AllSelect})
        Me.Context_Share.Name = "ShareListMenu"
        Me.Context_Share.Size = New System.Drawing.Size(213, 170)
        '
        'Context_Share_Download
        '
        Me.Context_Share_Download.Enabled = False
        Me.Context_Share_Download.Image = CType(resources.GetObject("Context_Share_Download.Image"), System.Drawing.Image)
        Me.Context_Share_Download.Name = "Context_Share_Download"
        Me.Context_Share_Download.Size = New System.Drawing.Size(212, 22)
        Me.Context_Share_Download.Text = "ダウンロード(&D)"
        '
        'Context_Share_Up
        '
        Me.Context_Share_Up.Enabled = False
        Me.Context_Share_Up.Image = CType(resources.GetObject("Context_Share_Up.Image"), System.Drawing.Image)
        Me.Context_Share_Up.Name = "Context_Share_Up"
        Me.Context_Share_Up.Size = New System.Drawing.Size(212, 22)
        Me.Context_Share_Up.Text = "上へ(&T)"
        '
        'Context_Share_Refresh
        '
        Me.Context_Share_Refresh.Enabled = False
        Me.Context_Share_Refresh.Image = CType(resources.GetObject("Context_Share_Refresh.Image"), System.Drawing.Image)
        Me.Context_Share_Refresh.Name = "Context_Share_Refresh"
        Me.Context_Share_Refresh.Size = New System.Drawing.Size(212, 22)
        Me.Context_Share_Refresh.Text = "更新(&R)"
        '
        'Context_Share_Root
        '
        Me.Context_Share_Root.Enabled = False
        Me.Context_Share_Root.Image = CType(resources.GetObject("Context_Share_Root.Image"), System.Drawing.Image)
        Me.Context_Share_Root.Name = "Context_Share_Root"
        Me.Context_Share_Root.Size = New System.Drawing.Size(212, 22)
        Me.Context_Share_Root.Text = "ルート(&O)"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(209, 6)
        '
        'Context_Share_Sort
        '
        Me.Context_Share_Sort.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Context_Share_Sort_FileName, Me.Context_Share_Sort_Extension, Me.Context_Share_Sort_Size, Me.Context_Share_Sort_TimeStamp})
        Me.Context_Share_Sort.Name = "Context_Share_Sort"
        Me.Context_Share_Sort.Size = New System.Drawing.Size(212, 22)
        Me.Context_Share_Sort.Text = "並び替え(&S)"
        '
        'Context_Share_Sort_FileName
        '
        Me.Context_Share_Sort_FileName.Name = "Context_Share_Sort_FileName"
        Me.Context_Share_Sort_FileName.Size = New System.Drawing.Size(153, 22)
        Me.Context_Share_Sort_FileName.Text = "ファイル名(&F)"
        '
        'Context_Share_Sort_Extension
        '
        Me.Context_Share_Sort_Extension.Name = "Context_Share_Sort_Extension"
        Me.Context_Share_Sort_Extension.Size = New System.Drawing.Size(153, 22)
        Me.Context_Share_Sort_Extension.Text = "拡張子(&E)"
        '
        'Context_Share_Sort_Size
        '
        Me.Context_Share_Sort_Size.Name = "Context_Share_Sort_Size"
        Me.Context_Share_Sort_Size.Size = New System.Drawing.Size(153, 22)
        Me.Context_Share_Sort_Size.Text = "サイズ(&S)"
        '
        'Context_Share_Sort_TimeStamp
        '
        Me.Context_Share_Sort_TimeStamp.Name = "Context_Share_Sort_TimeStamp"
        Me.Context_Share_Sort_TimeStamp.Size = New System.Drawing.Size(153, 22)
        Me.Context_Share_Sort_TimeStamp.Text = "更新日時(&T)"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(209, 6)
        '
        'Context_Share_FindBox
        '
        Me.Context_Share_FindBox.Name = "Context_Share_FindBox"
        Me.Context_Share_FindBox.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.Context_Share_FindBox.Size = New System.Drawing.Size(212, 22)
        Me.Context_Share_FindBox.Text = "検索ボックス(&B)"
        '
        'Context_Share_AllSelect
        '
        Me.Context_Share_AllSelect.Name = "Context_Share_AllSelect"
        Me.Context_Share_AllSelect.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.Context_Share_AllSelect.Size = New System.Drawing.Size(212, 22)
        Me.Context_Share_AllSelect.Text = "全て選択(&A)"
        '
        'Share_SmallImageList
        '
        Me.Share_SmallImageList.ImageStream = CType(resources.GetObject("Share_SmallImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Share_SmallImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.Share_SmallImageList.Images.SetKeyName(0, "Folder")
        Me.Share_SmallImageList.Images.SetKeyName(1, "Nothing")
        '
        'ConnectCheck
        '
        Me.ConnectCheck.Enabled = True
        Me.ConnectCheck.Interval = 60000
        '
        'bgServer
        '
        Me.bgServer.WorkerReportsProgress = True
        '
        'RefreshStatus
        '
        Me.RefreshStatus.Enabled = True
        Me.RefreshStatus.Interval = 1000
        '
        'AccountImageList
        '
        Me.AccountImageList.ImageStream = CType(resources.GetObject("AccountImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.AccountImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.AccountImageList.Images.SetKeyName(0, "offline")
        Me.AccountImageList.Images.SetKeyName(1, "online")
        Me.AccountImageList.Images.SetKeyName(2, "encrypt")
        '
        'Share_LargeImageList
        '
        Me.Share_LargeImageList.ImageStream = CType(resources.GetObject("Share_LargeImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Share_LargeImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.Share_LargeImageList.Images.SetKeyName(0, "Folder")
        Me.Share_LargeImageList.Images.SetKeyName(1, "Nothing")
        '
        'MainSplitContainer
        '
        Me.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.MainSplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.MainSplitContainer.Name = "MainSplitContainer"
        '
        'MainSplitContainer.Panel1
        '
        Me.MainSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.MainSplitContainer.Panel1.Controls.Add(Me.Account_ListView)
        Me.MainSplitContainer.Panel1.Controls.Add(Me.Info_Speedometer)
        Me.MainSplitContainer.Panel1.Controls.Add(Me.Info_Port)
        '
        'MainSplitContainer.Panel2
        '
        Me.MainSplitContainer.Panel2.Controls.Add(Me.Panel_Share)
        Me.MainSplitContainer.Panel2.Controls.Add(Me.Panel_Transfer)
        Me.MainSplitContainer.Panel2.Controls.Add(Me.Panel_Find)
        Me.MainSplitContainer.Size = New System.Drawing.Size(522, 286)
        Me.MainSplitContainer.SplitterDistance = 124
        Me.MainSplitContainer.TabIndex = 2
        '
        'Account_ListView
        '
        Me.Account_ListView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Account_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.AColumn_Name, Me.AColumn_Status})
        Me.Account_ListView.ContextMenuStrip = Me.Context_Account
        Me.Account_ListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Account_ListView.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Account_ListView.FullRowSelect = True
        Me.Account_ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.Account_ListView.HideSelection = False
        Me.Account_ListView.Location = New System.Drawing.Point(0, 0)
        Me.Account_ListView.MultiSelect = False
        Me.Account_ListView.Name = "Account_ListView"
        Me.Account_ListView.OwnerDraw = True
        Me.Account_ListView.Size = New System.Drawing.Size(124, 244)
        Me.Account_ListView.TabIndex = 3
        Me.Account_ListView.TileSize = New System.Drawing.Size(200, 50)
        Me.Account_ListView.UseCompatibleStateImageBehavior = False
        Me.Account_ListView.View = System.Windows.Forms.View.Details
        '
        'Info_Speedometer
        '
        Me.Info_Speedometer.BackColor = System.Drawing.Color.White
        Me.Info_Speedometer.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Info_Speedometer.Location = New System.Drawing.Point(0, 244)
        Me.Info_Speedometer.Name = "Info_Speedometer"
        Me.Info_Speedometer.Size = New System.Drawing.Size(124, 26)
        Me.Info_Speedometer.TabIndex = 1
        Me.Info_Speedometer.Text = "Down：0KB" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Up：0KB"
        Me.Info_Speedometer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Info_Port
        '
        Me.Info_Port.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Info_Port.Location = New System.Drawing.Point(0, 270)
        Me.Info_Port.Name = "Info_Port"
        Me.Info_Port.Size = New System.Drawing.Size(124, 16)
        Me.Info_Port.TabIndex = 2
        Me.Info_Port.Text = "Port："
        Me.Info_Port.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel_Share
        '
        Me.Panel_Share.Controls.Add(Me.Share_ListView)
        Me.Panel_Share.Controls.Add(Me.ShareToolStrip)
        Me.Panel_Share.Location = New System.Drawing.Point(3, 129)
        Me.Panel_Share.Name = "Panel_Share"
        Me.Panel_Share.Size = New System.Drawing.Size(383, 120)
        Me.Panel_Share.TabIndex = 3
        '
        'Share_ListView
        '
        Me.Share_ListView.AllowColumnReorder = True
        Me.Share_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.SColumn_Filename, Me.SColumn_Extension, Me.SColumn_Size, Me.SColumn_TimeStamp})
        Me.Share_ListView.ContextMenuStrip = Me.Context_Share
        Me.Share_ListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Share_ListView.FullRowSelect = True
        Me.Share_ListView.GridLines = True
        Me.Share_ListView.HideSelection = False
        Me.Share_ListView.LargeImageList = Me.Share_LargeImageList
        Me.Share_ListView.Location = New System.Drawing.Point(0, 25)
        Me.Share_ListView.Name = "Share_ListView"
        Me.Share_ListView.ShowItemToolTips = True
        Me.Share_ListView.Size = New System.Drawing.Size(383, 95)
        Me.Share_ListView.SmallImageList = Me.Share_SmallImageList
        Me.Share_ListView.TabIndex = 1
        Me.Share_ListView.TileSize = New System.Drawing.Size(200, 50)
        Me.Share_ListView.UseCompatibleStateImageBehavior = False
        Me.Share_ListView.View = System.Windows.Forms.View.Details
        '
        'SColumn_Filename
        '
        Me.SColumn_Filename.Text = "ファイル名"
        '
        'SColumn_Extension
        '
        Me.SColumn_Extension.Text = "拡張子"
        '
        'SColumn_Size
        '
        Me.SColumn_Size.Text = "サイズ"
        '
        'SColumn_TimeStamp
        '
        Me.SColumn_TimeStamp.Text = "更新日時"
        '
        'ShareToolStrip
        '
        Me.ShareToolStrip.BackColor = System.Drawing.Color.Transparent
        Me.ShareToolStrip.CanOverflow = False
        Me.ShareToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ShareToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Share_Download, Me.Share_Up, Me.Share_Refresh, Me.Share_Root, Me.Share_PathText, Me.Share_GoTransferList})
        Me.ShareToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ShareToolStrip.Name = "ShareToolStrip"
        Me.ShareToolStrip.Size = New System.Drawing.Size(383, 25)
        Me.ShareToolStrip.TabIndex = 0
        Me.ShareToolStrip.Text = "ToolStrip2"
        '
        'Share_Download
        '
        Me.Share_Download.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Share_Download.Enabled = False
        Me.Share_Download.Image = CType(resources.GetObject("Share_Download.Image"), System.Drawing.Image)
        Me.Share_Download.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Share_Download.Name = "Share_Download"
        Me.Share_Download.Size = New System.Drawing.Size(23, 22)
        Me.Share_Download.Text = "ダウンロード"
        '
        'Share_Up
        '
        Me.Share_Up.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Share_Up.Enabled = False
        Me.Share_Up.Image = CType(resources.GetObject("Share_Up.Image"), System.Drawing.Image)
        Me.Share_Up.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Share_Up.Name = "Share_Up"
        Me.Share_Up.Size = New System.Drawing.Size(23, 22)
        Me.Share_Up.Text = "上へ"
        '
        'Share_Refresh
        '
        Me.Share_Refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Share_Refresh.Enabled = False
        Me.Share_Refresh.Image = CType(resources.GetObject("Share_Refresh.Image"), System.Drawing.Image)
        Me.Share_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Share_Refresh.Name = "Share_Refresh"
        Me.Share_Refresh.Size = New System.Drawing.Size(23, 22)
        Me.Share_Refresh.Text = "更新"
        '
        'Share_Root
        '
        Me.Share_Root.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Share_Root.Enabled = False
        Me.Share_Root.Image = CType(resources.GetObject("Share_Root.Image"), System.Drawing.Image)
        Me.Share_Root.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Share_Root.Name = "Share_Root"
        Me.Share_Root.Size = New System.Drawing.Size(23, 22)
        Me.Share_Root.Text = "ルート"
        '
        'Share_PathText
        '
        Me.Share_PathText.AutoSize = False
        Me.Share_PathText.Name = "Share_PathText"
        Me.Share_PathText.Size = New System.Drawing.Size(100, 25)
        '
        'Share_GoTransferList
        '
        Me.Share_GoTransferList.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Share_GoTransferList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Share_GoTransferList.Image = CType(resources.GetObject("Share_GoTransferList.Image"), System.Drawing.Image)
        Me.Share_GoTransferList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Share_GoTransferList.Name = "Share_GoTransferList"
        Me.Share_GoTransferList.Size = New System.Drawing.Size(72, 22)
        Me.Share_GoTransferList.Text = "転送リスト"
        '
        'Panel_Transfer
        '
        Me.Panel_Transfer.Controls.Add(Me.Transfer_ListView)
        Me.Panel_Transfer.Controls.Add(Me.TransferToolStrip)
        Me.Panel_Transfer.Location = New System.Drawing.Point(3, 3)
        Me.Panel_Transfer.Name = "Panel_Transfer"
        Me.Panel_Transfer.Size = New System.Drawing.Size(383, 120)
        Me.Panel_Transfer.TabIndex = 2
        '
        'Transfer_ListView
        '
        Me.Transfer_ListView.AllowColumnReorder = True
        Me.Transfer_ListView.AllowDrop = True
        Me.Transfer_ListView.AutoArrange = False
        Me.Transfer_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.TColumn_Remote, Me.TColumn_Priority, Me.TColumn_FileName, Me.TColumn_FullPath, Me.TColumn_Size, Me.TColumn_Status})
        Me.Transfer_ListView.ContextMenuStrip = Me.Context_Transfer
        Me.Transfer_ListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Transfer_ListView.FullRowSelect = True
        Me.Transfer_ListView.GridLines = True
        ListViewGroup1.Header = "Download"
        ListViewGroup1.Name = "GTransfer_Download"
        ListViewGroup2.Header = "Upload"
        ListViewGroup2.Name = "GTransfer_Upload"
        Me.Transfer_ListView.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.Transfer_ListView.HideSelection = False
        Me.Transfer_ListView.Location = New System.Drawing.Point(0, 25)
        Me.Transfer_ListView.Name = "Transfer_ListView"
        Me.Transfer_ListView.OwnerDraw = True
        Me.Transfer_ListView.ShowItemToolTips = True
        Me.Transfer_ListView.Size = New System.Drawing.Size(383, 95)
        Me.Transfer_ListView.TabIndex = 1
        Me.Transfer_ListView.TileSize = New System.Drawing.Size(200, 50)
        Me.Transfer_ListView.UseCompatibleStateImageBehavior = False
        Me.Transfer_ListView.View = System.Windows.Forms.View.Tile
        '
        'TColumn_Remote
        '
        Me.TColumn_Remote.Text = "相手"
        '
        'TColumn_Priority
        '
        Me.TColumn_Priority.Text = "優先順位"
        '
        'TColumn_FileName
        '
        Me.TColumn_FileName.Text = "ファイル名"
        '
        'TColumn_FullPath
        '
        Me.TColumn_FullPath.Text = "フルパス"
        '
        'TColumn_Size
        '
        Me.TColumn_Size.Text = "サイズ"
        '
        'TColumn_Status
        '
        Me.TColumn_Status.Text = "ステータス"
        '
        'TransferToolStrip
        '
        Me.TransferToolStrip.BackColor = System.Drawing.Color.Transparent
        Me.TransferToolStrip.CanOverflow = False
        Me.TransferToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TransferToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Transfer_Approval, Me.Transfer_Upload, Me.ToolStripSeparator10, Me.Transfer_Stop, Me.Transfer_Restart, Me.ToolStripSeparator11, Me.Transfer_Interruption, Me.Transfer_Delete, Me.ToolStripSeparator12, Me.Transfer_DoneDelete, Me.Transfer_GoShareList})
        Me.TransferToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.TransferToolStrip.Name = "TransferToolStrip"
        Me.TransferToolStrip.Size = New System.Drawing.Size(383, 25)
        Me.TransferToolStrip.TabIndex = 0
        Me.TransferToolStrip.Text = "ToolStrip1"
        '
        'Transfer_Approval
        '
        Me.Transfer_Approval.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Transfer_Approval.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Transfer_Approval_Approval, Me.Transfer_Approval_AllApproval, Me.Transfer_Approval_Resume})
        Me.Transfer_Approval.Image = CType(resources.GetObject("Transfer_Approval.Image"), System.Drawing.Image)
        Me.Transfer_Approval.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Transfer_Approval.Name = "Transfer_Approval"
        Me.Transfer_Approval.Size = New System.Drawing.Size(29, 22)
        Me.Transfer_Approval.Text = "ダウンロード"
        '
        'Transfer_Approval_Approval
        '
        Me.Transfer_Approval_Approval.Enabled = False
        Me.Transfer_Approval_Approval.Name = "Transfer_Approval_Approval"
        Me.Transfer_Approval_Approval.Size = New System.Drawing.Size(160, 22)
        Me.Transfer_Approval_Approval.Text = "承認"
        '
        'Transfer_Approval_AllApproval
        '
        Me.Transfer_Approval_AllApproval.Enabled = False
        Me.Transfer_Approval_AllApproval.Name = "Transfer_Approval_AllApproval"
        Me.Transfer_Approval_AllApproval.Size = New System.Drawing.Size(160, 22)
        Me.Transfer_Approval_AllApproval.Text = "一括承認"
        '
        'Transfer_Approval_Resume
        '
        Me.Transfer_Approval_Resume.Enabled = False
        Me.Transfer_Approval_Resume.Name = "Transfer_Approval_Resume"
        Me.Transfer_Approval_Resume.Size = New System.Drawing.Size(160, 22)
        Me.Transfer_Approval_Resume.Text = "レジューム承認"
        '
        'Transfer_Upload
        '
        Me.Transfer_Upload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Transfer_Upload.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Transfer_Upload_File, Me.Transfer_Upload_Folder})
        Me.Transfer_Upload.Image = CType(resources.GetObject("Transfer_Upload.Image"), System.Drawing.Image)
        Me.Transfer_Upload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Transfer_Upload.Name = "Transfer_Upload"
        Me.Transfer_Upload.Size = New System.Drawing.Size(29, 22)
        Me.Transfer_Upload.Text = "アップロード"
        '
        'Transfer_Upload_File
        '
        Me.Transfer_Upload_File.Enabled = False
        Me.Transfer_Upload_File.Name = "Transfer_Upload_File"
        Me.Transfer_Upload_File.Size = New System.Drawing.Size(124, 22)
        Me.Transfer_Upload_File.Text = "ファイル"
        '
        'Transfer_Upload_Folder
        '
        Me.Transfer_Upload_Folder.Enabled = False
        Me.Transfer_Upload_Folder.Name = "Transfer_Upload_Folder"
        Me.Transfer_Upload_Folder.Size = New System.Drawing.Size(124, 22)
        Me.Transfer_Upload_Folder.Text = "フォルダ"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 25)
        '
        'Transfer_Stop
        '
        Me.Transfer_Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Transfer_Stop.Enabled = False
        Me.Transfer_Stop.Image = CType(resources.GetObject("Transfer_Stop.Image"), System.Drawing.Image)
        Me.Transfer_Stop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Transfer_Stop.Name = "Transfer_Stop"
        Me.Transfer_Stop.Size = New System.Drawing.Size(23, 22)
        Me.Transfer_Stop.Text = "一時停止"
        '
        'Transfer_Restart
        '
        Me.Transfer_Restart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Transfer_Restart.Enabled = False
        Me.Transfer_Restart.Image = CType(resources.GetObject("Transfer_Restart.Image"), System.Drawing.Image)
        Me.Transfer_Restart.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Transfer_Restart.Name = "Transfer_Restart"
        Me.Transfer_Restart.Size = New System.Drawing.Size(23, 22)
        Me.Transfer_Restart.Text = "再開"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 25)
        '
        'Transfer_Interruption
        '
        Me.Transfer_Interruption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Transfer_Interruption.Enabled = False
        Me.Transfer_Interruption.Image = CType(resources.GetObject("Transfer_Interruption.Image"), System.Drawing.Image)
        Me.Transfer_Interruption.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Transfer_Interruption.Name = "Transfer_Interruption"
        Me.Transfer_Interruption.Size = New System.Drawing.Size(23, 22)
        Me.Transfer_Interruption.Text = "中断"
        '
        'Transfer_Delete
        '
        Me.Transfer_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Transfer_Delete.Enabled = False
        Me.Transfer_Delete.Image = CType(resources.GetObject("Transfer_Delete.Image"), System.Drawing.Image)
        Me.Transfer_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Transfer_Delete.Name = "Transfer_Delete"
        Me.Transfer_Delete.Size = New System.Drawing.Size(23, 22)
        Me.Transfer_Delete.Text = "データを削除"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(6, 25)
        '
        'Transfer_DoneDelete
        '
        Me.Transfer_DoneDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Transfer_DoneDelete.Image = CType(resources.GetObject("Transfer_DoneDelete.Image"), System.Drawing.Image)
        Me.Transfer_DoneDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Transfer_DoneDelete.Name = "Transfer_DoneDelete"
        Me.Transfer_DoneDelete.Size = New System.Drawing.Size(23, 22)
        Me.Transfer_DoneDelete.Text = "完了削除"
        '
        'Transfer_GoShareList
        '
        Me.Transfer_GoShareList.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Transfer_GoShareList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Transfer_GoShareList.Image = CType(resources.GetObject("Transfer_GoShareList.Image"), System.Drawing.Image)
        Me.Transfer_GoShareList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Transfer_GoShareList.Name = "Transfer_GoShareList"
        Me.Transfer_GoShareList.Size = New System.Drawing.Size(72, 22)
        Me.Transfer_GoShareList.Text = "共有リスト"
        '
        'Panel_Find
        '
        Me.Panel_Find.Controls.Add(Me.Find_Button)
        Me.Panel_Find.Controls.Add(Me.Find_TextBox)
        Me.Panel_Find.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel_Find.Location = New System.Drawing.Point(0, 260)
        Me.Panel_Find.Name = "Panel_Find"
        Me.Panel_Find.Size = New System.Drawing.Size(394, 26)
        Me.Panel_Find.TabIndex = 0
        '
        'Find_Button
        '
        Me.Find_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Find_Button.Location = New System.Drawing.Point(313, 3)
        Me.Find_Button.Name = "Find_Button"
        Me.Find_Button.Size = New System.Drawing.Size(78, 20)
        Me.Find_Button.TabIndex = 1
        Me.Find_Button.Text = "検索(&B)"
        Me.Find_Button.UseVisualStyleBackColor = True
        '
        'Find_TextBox
        '
        Me.Find_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Find_TextBox.Location = New System.Drawing.Point(3, 4)
        Me.Find_TextBox.Name = "Find_TextBox"
        Me.Find_TextBox.Size = New System.Drawing.Size(304, 19)
        Me.Find_TextBox.TabIndex = 0
        '
        'Context_Account_Reconnect
        '
        Me.Context_Account_Reconnect.Enabled = False
        Me.Context_Account_Reconnect.Name = "Context_Account_Reconnect"
        Me.Context_Account_Reconnect.Size = New System.Drawing.Size(202, 22)
        Me.Context_Account_Reconnect.Text = "再接続(&R)"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 286)
        Me.Controls.Add(Me.MainSplitContainer)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "Arachlex"
        Me.Context_Account.ResumeLayout(False)
        Me.Context_Transfer.ResumeLayout(False)
        Me.Context_Share.ResumeLayout(False)
        Me.MainSplitContainer.Panel1.ResumeLayout(False)
        Me.MainSplitContainer.Panel2.ResumeLayout(False)
        Me.MainSplitContainer.ResumeLayout(False)
        Me.Panel_Share.ResumeLayout(False)
        Me.Panel_Share.PerformLayout()
        Me.ShareToolStrip.ResumeLayout(False)
        Me.ShareToolStrip.PerformLayout()
        Me.Panel_Transfer.ResumeLayout(False)
        Me.Panel_Transfer.PerformLayout()
        Me.TransferToolStrip.ResumeLayout(False)
        Me.TransferToolStrip.PerformLayout()
        Me.Panel_Find.ResumeLayout(False)
        Me.Panel_Find.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents Transfer_ListView As Arachlex.DoubleBufferedListView
    Friend WithEvents Share_ListView As Arachlex.DoubleBufferedListView
    Friend WithEvents SColumn_Filename As System.Windows.Forms.ColumnHeader
    Friend WithEvents SColumn_Extension As System.Windows.Forms.ColumnHeader
    Friend WithEvents SColumn_Size As System.Windows.Forms.ColumnHeader
    Friend WithEvents SColumn_TimeStamp As System.Windows.Forms.ColumnHeader
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents Context_Account As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Context_Account_Edit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Account_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Transfer As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Context_Share As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Share_SmallImageList As System.Windows.Forms.ImageList
    Friend WithEvents Context_Transfer_Open As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Context_Transfer_Stop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Transfer_ReStart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Context_Transfer_Interruption As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Context_Transfer_AllSelect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Share_Download As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Share_Refresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Share_Up As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Share_Root As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Context_Share_AllSelect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConnectCheck As System.Windows.Forms.Timer
    Friend WithEvents bgServer As System.ComponentModel.BackgroundWorker
    Friend WithEvents RefreshStatus As System.Windows.Forms.Timer
    Friend WithEvents Context_Account_Connect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AccountImageList As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Context_Transfer_Sort As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Transfer_Sort_OtherPoint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Transfer_Sort_FileName As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Transfer_Sort_FullPath As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Transfer_Sort_Size As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Transfer_Sort_Status As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Context_Share_Sort As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Share_Sort_FileName As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Share_Sort_Extension As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Share_Sort_Size As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Share_Sort_TimeStamp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Panel_Share As System.Windows.Forms.Panel
    Friend WithEvents Panel_Transfer As System.Windows.Forms.Panel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Context_Transfer_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Info_Speedometer As System.Windows.Forms.Label
    Friend WithEvents Info_Port As System.Windows.Forms.Label
    Friend WithEvents Share_LargeImageList As System.Windows.Forms.ImageList
    Friend WithEvents ShareToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents TransferToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents Transfer_Approval As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents Transfer_Approval_Approval As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Transfer_Approval_AllApproval As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Transfer_Approval_Resume As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Transfer_Upload As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents Transfer_Upload_File As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Transfer_Upload_Folder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Transfer_Stop As System.Windows.Forms.ToolStripButton
    Friend WithEvents Transfer_Restart As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Transfer_Interruption As System.Windows.Forms.ToolStripButton
    Friend WithEvents Transfer_Delete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Transfer_DoneDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents Share_Download As System.Windows.Forms.ToolStripButton
    Friend WithEvents Share_Up As System.Windows.Forms.ToolStripButton
    Friend WithEvents Share_Refresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents Share_Root As System.Windows.Forms.ToolStripButton
    Friend WithEvents Share_PathText As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents Context_Account_Setting As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Share_GoTransferList As System.Windows.Forms.ToolStripButton
    Friend WithEvents Transfer_GoShareList As System.Windows.Forms.ToolStripButton
    Friend WithEvents Context_Account_Add As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Transfer_QueueUp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Transfer_QueueDown As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Context_Transfer_Sort_Queue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel_Find As System.Windows.Forms.Panel
    Friend WithEvents Find_Button As System.Windows.Forms.Button
    Friend WithEvents Find_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Context_Transfer_FindBox As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Share_FindBox As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Account_ListView As Arachlex.DoubleBufferedListView
    Friend WithEvents AColumn_Name As System.Windows.Forms.ColumnHeader
    Friend WithEvents Context_Account_Sort As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Account_Sort_AccountName As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Account_Sort_LoginState As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AColumn_Status As System.Windows.Forms.ColumnHeader
    Friend WithEvents Context_Account_Chat As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Context_Transfer_ListMode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TColumn_Remote As System.Windows.Forms.ColumnHeader
    Friend WithEvents TColumn_Priority As System.Windows.Forms.ColumnHeader
    Friend WithEvents TColumn_FileName As System.Windows.Forms.ColumnHeader
    Friend WithEvents TColumn_FullPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents TColumn_Size As System.Windows.Forms.ColumnHeader
    Friend WithEvents TColumn_Status As System.Windows.Forms.ColumnHeader
    Friend WithEvents Context_Account_Reconnect As System.Windows.Forms.ToolStripMenuItem

End Class
