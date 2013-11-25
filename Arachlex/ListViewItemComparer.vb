Imports Arachlex.DefinitionClass
Public Class ListViewItemComparer
    Implements IComparer

    ''' <summary>
    ''' ��r������@
    ''' </summary>
    Public Enum ComparerMode
        Account_Name
        Account_Login

        FileInfo_String
        FileInfo_Size
        FileInfo_Time

        Transfer_OtherPoint
        Transfer_Queue
        Transfer_FileName
        Transfer_FullPath
        Transfer_Size
        Transfer_Status
    End Enum

    Private _column As Integer
    Private _order As SortOrder
    Private _mode As ComparerMode
    Private _columnModes() As ComparerMode

    ''' <summary>
    ''' ���ёւ���ListView��̔ԍ�
    ''' </summary>
    Public Property Column() As Integer
        Get
            Return _column
        End Get
        Set(ByVal Value As Integer)
            _column = Value
        End Set
    End Property

    ''' <summary>
    ''' �������~����
    ''' </summary>
    Public Property Order() As SortOrder
        Get
            Return _order
        End Get
        Set(ByVal Value As SortOrder)
            _order = Value
        End Set
    End Property

    ''' <summary>
    ''' ���ёւ��̕��@
    ''' </summary>
    Public Property Mode() As ComparerMode
        Get
            Return _mode
        End Get
        Set(ByVal Value As ComparerMode)
            _mode = Value
        End Set
    End Property

    ''' <summary>
    ''' �񂲂Ƃ̕��ёւ��̕��@
    ''' </summary>
    Public WriteOnly Property ColumnModes() As ComparerMode()
        Set(ByVal Value As ComparerMode())
            _columnModes = Value
        End Set
    End Property

    ''' <summary>
    ''' ListViewItemComparer�N���X�̃R���X�g���N�^
    ''' </summary>
    ''' <param name="col">���ёւ����ԍ�</param>
    ''' <param name="ord">�������~����</param>
    ''' <param name="cmod">���ёւ��̕��@</param>
    Public Sub New(ByVal col As Integer, ByVal ord As SortOrder, ByVal cmod As ComparerMode)
        _column = col
        _order = ord
        _mode = cmod
    End Sub
    Public Sub New()
        _column = 0
        _order = SortOrder.Ascending
        _mode = ComparerMode.Account_Login
    End Sub

    ''' <summary>
    ''' x��y��菬�����Ƃ��̓}�C�i�X�̐��A�傫���Ƃ��̓v���X�̐��A�����Ƃ���0��Ԃ�
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim result As Integer = 0

        '���בւ��̕��@������
        If _columnModes IsNot Nothing AndAlso _columnModes.Length > _column Then
            _mode = _columnModes(_column)
        End If

        Select Case _mode
            Case ComparerMode.Account_Name
                Dim xItemInfo As AccountListViewItem = DirectCast(x, AccountListViewItem)
                Dim yItemInfo As AccountListViewItem = DirectCast(y, AccountListViewItem)

                result = String.Compare(xItemInfo.IndividualData.AccountName, yItemInfo.IndividualData.AccountName)
            Case ComparerMode.Account_Login
                Dim xItemInfo As AccountListViewItem = DirectCast(x, AccountListViewItem)
                Dim yItemInfo As AccountListViewItem = DirectCast(y, AccountListViewItem)

                If xItemInfo.TCPConnect.AceptInfo AndAlso yItemInfo.TCPConnect.AceptInfo Then
                    result = String.Compare(xItemInfo.IndividualData.AccountName, yItemInfo.IndividualData.AccountName)
                ElseIf xItemInfo.TCPConnect.AceptInfo AndAlso Not yItemInfo.TCPConnect.AceptInfo Then
                    result = -1
                ElseIf Not xItemInfo.TCPConnect.AceptInfo AndAlso yItemInfo.TCPConnect.AceptInfo Then
                    result = 1
                ElseIf Not xItemInfo.TCPConnect.AceptInfo AndAlso Not yItemInfo.TCPConnect.AceptInfo Then
                    result = String.Compare(xItemInfo.IndividualData.AccountName, yItemInfo.IndividualData.AccountName)
                End If
            Case ComparerMode.FileInfo_Size
                Dim xItemInfo As FileInfoListViewItem = DirectCast(x, FileInfoListViewItem)
                Dim yItemInfo As FileInfoListViewItem = DirectCast(y, FileInfoListViewItem)

                If (xItemInfo.Attribute = Arachlex_Item.File AndAlso yItemInfo.Attribute = Arachlex_Item.File) Then
                    result = xItemInfo.FileSize.CompareTo(yItemInfo.FileSize)
                ElseIf (xItemInfo.Attribute = Arachlex_Item.Folder AndAlso yItemInfo.Attribute = Arachlex_Item.Folder) Then
                    result = 0
                ElseIf xItemInfo.Attribute = Arachlex_Item.File AndAlso yItemInfo.Attribute = Arachlex_Item.Folder Then
                    result = 1
                ElseIf xItemInfo.Attribute = Arachlex_Item.Folder AndAlso yItemInfo.Attribute = Arachlex_Item.File Then
                    result = -1
                End If
            Case ComparerMode.FileInfo_String
                Dim xItemInfo As FileInfoListViewItem = DirectCast(x, FileInfoListViewItem)
                Dim yItemInfo As FileInfoListViewItem = DirectCast(y, FileInfoListViewItem)

                If (xItemInfo.Attribute = Arachlex_Item.File AndAlso yItemInfo.Attribute = Arachlex_Item.File) OrElse _
                (xItemInfo.Attribute = Arachlex_Item.Folder AndAlso yItemInfo.Attribute = Arachlex_Item.Folder) Then
                    result = String.Compare(xItemInfo.SubItems(_column).Text, yItemInfo.SubItems(_column).Text)
                ElseIf xItemInfo.Attribute = Arachlex_Item.File AndAlso yItemInfo.Attribute = Arachlex_Item.Folder Then
                    result = 1
                ElseIf xItemInfo.Attribute = Arachlex_Item.Folder AndAlso yItemInfo.Attribute = Arachlex_Item.File Then
                    result = -1
                End If
            Case ComparerMode.FileInfo_Time
                Dim xItemInfo As FileInfoListViewItem = DirectCast(x, FileInfoListViewItem)
                Dim yItemInfo As FileInfoListViewItem = DirectCast(y, FileInfoListViewItem)

                If (xItemInfo.Attribute = Arachlex_Item.File AndAlso yItemInfo.Attribute = Arachlex_Item.File) OrElse _
                (xItemInfo.Attribute = Arachlex_Item.Folder AndAlso yItemInfo.Attribute = Arachlex_Item.Folder) Then
                    result = DateTime.Compare(xItemInfo.TimeStamp, yItemInfo.TimeStamp)
                ElseIf xItemInfo.Attribute = Arachlex_Item.File AndAlso yItemInfo.Attribute = Arachlex_Item.Folder Then
                    result = 1
                ElseIf xItemInfo.Attribute = Arachlex_Item.Folder AndAlso yItemInfo.Attribute = Arachlex_Item.File Then
                    result = -1
                End If
            Case ComparerMode.Transfer_OtherPoint
                Dim ItemXStr As String = DirectCast(x, TransferListViewItem).OwnerAccount.IndividualData.AccountName
                Dim ItemYStr As String = DirectCast(y, TransferListViewItem).OwnerAccount.IndividualData.AccountName

                result = String.Compare(ItemXStr, ItemYStr)
            Case ComparerMode.Transfer_Queue
                Dim xItemInfo As TransferListViewItem = DirectCast(x, TransferListViewItem)
                Dim yItemInfo As TransferListViewItem = DirectCast(y, TransferListViewItem)

                If xItemInfo.TransportingDone AndAlso yItemInfo.TransportingDone Then
                    result = String.Compare(xItemInfo.FileName, yItemInfo.FileName)
                ElseIf xItemInfo.TransportingDone AndAlso Not yItemInfo.TransportingDone Then
                    result = -1
                ElseIf Not xItemInfo.TransportingDone AndAlso yItemInfo.TransportingDone Then
                    result = 1
                ElseIf Not xItemInfo.TransportingDone AndAlso Not yItemInfo.TransportingDone Then
                    result = CInt(xItemInfo.TransportQueue) - CInt(yItemInfo.TransportQueue)
                End If
            Case ComparerMode.Transfer_FileName
                Dim ItemXStr As String = DirectCast(x, TransferListViewItem).FileName
                Dim ItemYStr As String = DirectCast(y, TransferListViewItem).FileName

                result = String.Compare(ItemXStr, ItemYStr)
            Case ComparerMode.Transfer_FullPath
                Dim ItemXStr As String = DirectCast(x, TransferListViewItem).FilePath
                Dim ItemYStr As String = DirectCast(y, TransferListViewItem).FilePath

                result = String.Compare(ItemXStr, ItemYStr)
            Case ComparerMode.Transfer_Size
                Dim xItemInfo As TransferListViewItem = DirectCast(x, TransferListViewItem)
                Dim yItemInfo As TransferListViewItem = DirectCast(y, TransferListViewItem)

                result = xItemInfo.FileSize.CompareTo(yItemInfo.FileSize)
            Case ComparerMode.Transfer_Status
                Dim xItemInfo As TransferListViewItem = DirectCast(x, TransferListViewItem)
                Dim yItemInfo As TransferListViewItem = DirectCast(y, TransferListViewItem)

                If xItemInfo.ItemAttribute = Arachlex_Item.File AndAlso yItemInfo.ItemAttribute = Arachlex_Item.File Then
                    result = CInt((xItemInfo.FileSeek / xItemInfo.FileSize - yItemInfo.FileSeek / yItemInfo.FileSize) * 100)
                ElseIf xItemInfo.ItemAttribute = Arachlex_Item.File AndAlso yItemInfo.ItemAttribute = Arachlex_Item.Folder Then
                    result = 1
                ElseIf xItemInfo.ItemAttribute = Arachlex_Item.Folder AndAlso yItemInfo.ItemAttribute = Arachlex_Item.File Then
                    result = -1
                Else '�Ⴄ�ꍇ
                    result = String.Compare(xItemInfo.SubItems(_column).Text, yItemInfo.SubItems(_column).Text)
                End If
        End Select

        '�~���̎��͌��ʂ�+-�t�ɂ���
        If _order = SortOrder.Descending Then
            result = -result
        ElseIf _order = SortOrder.None Then
            result = 0
        End If
        '���ʂ�Ԃ�
        Return result
    End Function
End Class