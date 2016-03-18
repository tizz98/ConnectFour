﻿Public Class frmConnectFour
    ' Defaults
    Private BTN_FONT As New Font("Courier New", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Private BTN_SIZE As New Size(70, 70)
    Private BTN_DROPZONE_STARTING_PT As New Point(160, 35)
    Private BTN_TOP_ROW_STARTING_PT As New Point(160, 142)
    Private Const ROW_SPACE As Integer = 76
    Private Const COL_SPACE As Integer = 103
    Private Const DROP_ZONE_FMT_STR As String = "btnColumn{0}DropZone"
    Private Const COL_ROW_FMT_STR As String = "btnCol{0}Row{1}"
    Private Shared PLAYER_1_COLOR As Color = Color.Aqua
    Private Shared PLAYER_2_COLOR As Color = Color.Yellow
    Private OK_DROP_COLOR As Color = Color.Green
    Private BAD_DROP_COLOR As Color = Color.Red
    Private Const GO_TEXT As String = ": Go!"
    Private Const PLAYER_1_TAG As String = "PLAYER_1"
    Private Const PLAYER_2_TAG As String = "PLAYER_2"
    Private Const PLAYER_REPR As String = "O"

    Private WithEvents colDropZoneButtons As List(Of Button)
    Private WithEvents colsRowsButtons As List(Of Button)

    Private Class Winner
        Public Column As Integer
        Public Row As Integer
        Public BackColor As Color
        Public IsWinner As Boolean = False
        Public IsColWin As Boolean = False
        Public IsRowWin As Boolean = False

        Public Function getPlayerWinnerTag() As String
            If BackColor = PLAYER_1_COLOR Then
                Return PLAYER_1_TAG
            ElseIf BackColor = PLAYER_2_COLOR Then
                Return PLAYER_2_TAG
            Else
                Return Nothing
            End If
        End Function
    End Class

    Private Sub frmConnectFour_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        createGameBoard()
        startNewGame()

        btnPlayer1.Tag = PLAYER_1_TAG
        btnPlayer2.Tag = PLAYER_2_TAG
    End Sub

    Private Function getCurrentPlayerTag() As Object
        If btnPlayer1.Enabled Then
            Return PLAYER_1_TAG
        ElseIf btnPlayer2.Enabled Then
            Return PLAYER_2_TAG
        Else
            Return Nothing
        End If
    End Function

    Private Sub playerButton_MouseMove(sender As Object, e As MouseEventArgs) Handles btnPlayer1.MouseMove, btnPlayer2.MouseMove
        If e.Button = MouseButtons.Left Then
            Dim srcBtn As Button = DirectCast(sender, Button)
            srcBtn.DoDragDrop(srcBtn.Tag.ToString(), DragDropEffects.All Or DragDropEffects.Link)
        End If
    End Sub

    Private Sub dropZoneButton_DragEnter(sender As Object, e As DragEventArgs)
        ' todo: make sure the column can be dropped on
        Dim tempBtn As Button = DirectCast(sender, Button)

        If canDropInColumn(CInt(tempBtn.Text)) Then
            tempBtn.BackColor = OK_DROP_COLOR
            e.Effect = DragDropEffects.Copy
        Else
            tempBtn.BackColor = BAD_DROP_COLOR
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub dropZoneButton_DragLeave(sender As Object, e As EventArgs)
        Dim tempBtn As Button = DirectCast(sender, Button)
        tempBtn.BackColor = DefaultBackColor()
    End Sub

    Private Sub dropZoneButton_DragDrop(sender As Object, e As DragEventArgs)
        Dim srcButton As Button = DirectCast(sender, Button)
        Dim w As Winner

        srcButton.BackColor = SystemColors.Control

        Console.Write(e.Data.GetData("Text", True))
        Console.WriteLine(" in col: " + srcButton.Text)

        dropPlayer(CInt(srcButton.Text), CStr(getCurrentPlayerTag()))
        w = getWinner()

        If Not w.IsWinner Then
            switchTurns()
        Else
            lblWinnerText.Text = "Game goes to " & IIf(w.getPlayerWinnerTag() = PLAYER_1_TAG, "1", "2") & " - " & IIf(w.IsColWin, "Column", "Row") & " " & IIf(w.IsColWin, w.Column, w.Row)
            lblWinnerText.Visible = True

            finishGame()
        End If

        If Not hasMovesAvailable() Then
            lblWinnerText.Text = "No spaces left to move"
            lblWinnerText.Visible = True

            finishGame()
        End If
    End Sub

    Private Sub finishGame()
        btnPlayer1.Enabled = False
        btnPlayer2.Enabled = False

        lblPlayer1.Text = lblPlayer1.Text.Replace(GO_TEXT, "")
        lblPlayer2.Text = lblPlayer2.Text.Replace(GO_TEXT, "")
    End Sub

    Private Function canDropInColumn(column As Integer) As Boolean
        ' basically if the column isn't full; free spaces will have their Text = ""
        Dim buttonsCol = From btn In colsRowsButtons
                         Where (DirectCast(btn.Tag, Point)).X = column And btn.Text = ""
                         Select btn
        Return buttonsCol.ToArray.Length > 0
    End Function

    Private Sub dropPlayer(column As Integer, player As String)
        Dim playerColor As Color = IIf(player = PLAYER_1_TAG, PLAYER_1_COLOR, PLAYER_2_COLOR)
        Dim buttonsCol = From btn In colsRowsButtons
                         Where (DirectCast(btn.Tag, Point)).X = column And btn.Text = ""
                         Order By (DirectCast(btn.Tag, Point)).Y Ascending
                         Select btn

        Dim buttonsList As List(Of Button) = buttonsCol.ToList()

        For Each btn As Button In buttonsList
            btn.BackColor = playerColor
            btn.Text = PLAYER_REPR

            System.Threading.Thread.Sleep(20)
            Application.DoEvents()

            If Not btn Is buttonsList.Last Then
                btn.BackColor = DefaultBackColor()
                btn.Text = ""
            Else
                Beep()
            End If
        Next
    End Sub

    Private Function getWinner() As Winner
        Dim w As New Winner()

        Dim cols = From btn In colsRowsButtons
                   Group btn By (DirectCast(btn.Tag, Point)).X Into Group
                   Select Group

        For Each col In cols.ToList()
            Dim tmpStr As String = ""
            Dim row As Button = Nothing

            For Each row In col
                If row.BackColor = PLAYER_1_COLOR Then
                    tmpStr &= "1"
                ElseIf row.BackColor = PLAYER_2_COLOR Then
                    tmpStr &= "2"
                Else
                    tmpStr &= " "
                End If
            Next

            If (Not IsNothing(row)) And (tmpStr.Contains("1111") Or tmpStr.Contains("2222")) Then
                w.IsWinner = True
                w.IsColWin = True
                w.Column = (DirectCast(row.Tag, Point)).X
                w.BackColor = IIf(tmpStr.Contains("1111"), PLAYER_1_COLOR, PLAYER_2_COLOR)  ' there was a winner so it is one or the other

                Return w
            End If
        Next

        Dim rows = From btn In colsRowsButtons
                   Group btn By (DirectCast(btn.Tag, Point)).Y Into Group
                   Select Group

        For Each row In rows.ToList()
            Dim tmpStr As String = ""
            Dim col As Button = Nothing

            For Each col In row
                If col.BackColor = PLAYER_1_COLOR Then
                    tmpStr &= "1"
                ElseIf col.BackColor = PLAYER_2_COLOR Then
                    tmpStr &= "2"
                Else
                    tmpStr &= " "
                End If
            Next

            If (Not IsNothing(col)) And (tmpStr.Contains("1111") Or tmpStr.Contains("2222")) Then
                w.IsWinner = True
                w.IsRowWin = True
                w.Row = (DirectCast(col.Tag, Point)).Y
                w.BackColor = IIf(tmpStr.Contains("1111"), PLAYER_1_COLOR, PLAYER_2_COLOR)  ' there was a winner so it is one or the other
            End If
        Next

        Return w  ' no winner, no dinner
    End Function

    Private Function hasMovesAvailable() As Boolean
        Dim spaces = (From btn In colsRowsButtons
                      Where btn.Text = ""
                      Select btn).ToArray()
        Return spaces.Length > 0
    End Function

    Private Sub startNewGame()
        Dim player1First As Boolean = MessageBox.Show("Should player one go first?", "Start New Game",
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes
        ' reversed so that switchTurns will unreverse it
        btnPlayer1.Enabled = Not player1First
        btnPlayer2.Enabled = player1First
        switchTurns()
    End Sub

    Private Sub switchTurns()
        btnPlayer1.Enabled = Not btnPlayer1.Enabled
        btnPlayer2.Enabled = Not btnPlayer2.Enabled

        If btnPlayer1.Enabled Then
            lblPlayer1.Text = lblPlayer1.Text & GO_TEXT
        Else
            lblPlayer1.Text = lblPlayer1.Text.Replace(GO_TEXT, "")
        End If

        If btnPlayer2.Enabled Then
            lblPlayer2.Text = lblPlayer2.Text & GO_TEXT
        Else
            lblPlayer2.Text = lblPlayer2.Text.Replace(GO_TEXT, "")
        End If
    End Sub

    Private Sub createGameBoard()
        Dim tempBtn As Button
        Dim tempPoint As Point
        Dim colXCoord As Integer
        Dim colInt As Integer  ' used for naming
        Dim rowInt As Integer  ' used for naming

        Dim previousTopRowStartingPoint As Point = BTN_TOP_ROW_STARTING_PT

        colDropZoneButtons = New List(Of Button)
        colsRowsButtons = New List(Of Button)

        ' Create columns
        For col As Integer = 0 To 6
            ' Dropzone button first
            colInt = col + 1

            tempBtn = New Button()
            tempBtn.Font = BTN_FONT

            ' Y never changes, just like people
            colXCoord = BTN_DROPZONE_STARTING_PT.X + (col * COL_SPACE)
            tempPoint = New Point(colXCoord, BTN_DROPZONE_STARTING_PT.Y)

            tempBtn.Location = tempPoint
            tempBtn.Name = String.Format(DROP_ZONE_FMT_STR, colInt)
            tempBtn.Size = BTN_SIZE
            tempBtn.Text = colInt
            tempBtn.BackColor = DefaultBackColor()  ' for consistency
            tempBtn.AllowDrop = True

            ' add handlers
            AddHandler tempBtn.DragEnter, AddressOf dropZoneButton_DragEnter
            AddHandler tempBtn.DragLeave, AddressOf dropZoneButton_DragLeave
            AddHandler tempBtn.DragDrop, AddressOf dropZoneButton_DragDrop

            Me.Controls.Add(tempBtn)
            colDropZoneButtons.Add(tempBtn)

            ' column buttons - start at top and work down
            For row As Integer = 5 To 0 Step -1
                rowInt = row + 1

                tempBtn = New Button()
                tempBtn.Font = BTN_FONT

                ' X never changes (for a column)
                tempPoint = New Point(colXCoord, BTN_TOP_ROW_STARTING_PT.Y + (row * ROW_SPACE))

                tempBtn.Location = tempPoint
                tempBtn.Name = String.Format(COL_ROW_FMT_STR, colInt, rowInt)
                tempBtn.Size = BTN_SIZE
                tempBtn.Enabled = False
                tempBtn.Tag = New Point(colInt, rowInt)  ' instead of Text

                Me.Controls.Add(tempBtn)
                colsRowsButtons.Add(tempBtn)
            Next
        Next
    End Sub

    Private Sub btnResetBoard_Click(sender As Object, e As EventArgs) Handles btnResetBoard.Click
        For Each dropZoneBtn As Button In colDropZoneButtons
            Me.Controls.Remove(dropZoneBtn)
        Next

        For Each colRowBtn As Button In colsRowsButtons
            Me.Controls.Remove(colRowBtn)
        Next

        lblPlayer1.Text = lblPlayer1.Text.Replace(GO_TEXT, "")
        lblPlayer2.Text = lblPlayer2.Text.Replace(GO_TEXT, "")

        lblWinnerText.Text = ""
        lblWinnerText.Visible = False

        createGameBoard()
        startNewGame()
    End Sub
End Class
