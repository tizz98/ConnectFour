<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmConnectFour
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnPlayer1 = New System.Windows.Forms.Button()
        Me.lblPlayer1 = New System.Windows.Forms.Label()
        Me.lblPlayer2 = New System.Windows.Forms.Label()
        Me.btnPlayer2 = New System.Windows.Forms.Button()
        Me.lblWinnerText = New System.Windows.Forms.Label()
        Me.btnResetBoard = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnPlayer1
        '
        Me.btnPlayer1.BackColor = System.Drawing.Color.Aqua
        Me.btnPlayer1.Font = New System.Drawing.Font("Courier New", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlayer1.Location = New System.Drawing.Point(30, 317)
        Me.btnPlayer1.Name = "btnPlayer1"
        Me.btnPlayer1.Size = New System.Drawing.Size(70, 70)
        Me.btnPlayer1.TabIndex = 1
        Me.btnPlayer1.Text = "O"
        Me.btnPlayer1.UseVisualStyleBackColor = False
        '
        'lblPlayer1
        '
        Me.lblPlayer1.AutoSize = True
        Me.lblPlayer1.Location = New System.Drawing.Point(12, 291)
        Me.lblPlayer1.Name = "lblPlayer1"
        Me.lblPlayer1.Size = New System.Drawing.Size(45, 13)
        Me.lblPlayer1.TabIndex = 2
        Me.lblPlayer1.Text = "Player 1"
        '
        'lblPlayer2
        '
        Me.lblPlayer2.AutoSize = True
        Me.lblPlayer2.Location = New System.Drawing.Point(898, 291)
        Me.lblPlayer2.Name = "lblPlayer2"
        Me.lblPlayer2.Size = New System.Drawing.Size(45, 13)
        Me.lblPlayer2.TabIndex = 4
        Me.lblPlayer2.Text = "Player 2"
        '
        'btnPlayer2
        '
        Me.btnPlayer2.BackColor = System.Drawing.Color.Yellow
        Me.btnPlayer2.Font = New System.Drawing.Font("Courier New", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlayer2.Location = New System.Drawing.Point(916, 317)
        Me.btnPlayer2.Name = "btnPlayer2"
        Me.btnPlayer2.Size = New System.Drawing.Size(70, 70)
        Me.btnPlayer2.TabIndex = 3
        Me.btnPlayer2.Text = "O"
        Me.btnPlayer2.UseVisualStyleBackColor = False
        '
        'lblWinnerText
        '
        Me.lblWinnerText.AutoSize = True
        Me.lblWinnerText.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWinnerText.Location = New System.Drawing.Point(136, 617)
        Me.lblWinnerText.Name = "lblWinnerText"
        Me.lblWinnerText.Size = New System.Drawing.Size(872, 73)
        Me.lblWinnerText.TabIndex = 5
        Me.lblWinnerText.Text = "Winner winner chicken dinner"
        Me.lblWinnerText.Visible = False
        '
        'btnResetBoard
        '
        Me.btnResetBoard.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnResetBoard.Location = New System.Drawing.Point(288, 704)
        Me.btnResetBoard.Name = "btnResetBoard"
        Me.btnResetBoard.Size = New System.Drawing.Size(442, 109)
        Me.btnResetBoard.TabIndex = 6
        Me.btnResetBoard.Text = "Reset Board"
        Me.btnResetBoard.UseVisualStyleBackColor = True
        '
        'frmConnectFour
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1033, 843)
        Me.Controls.Add(Me.btnResetBoard)
        Me.Controls.Add(Me.lblWinnerText)
        Me.Controls.Add(Me.lblPlayer2)
        Me.Controls.Add(Me.btnPlayer2)
        Me.Controls.Add(Me.lblPlayer1)
        Me.Controls.Add(Me.btnPlayer1)
        Me.Name = "frmConnectFour"
        Me.Text = "Drag 'n Drop Connect Four"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPlayer1 As Button
    Friend WithEvents lblPlayer1 As Label
    Friend WithEvents lblPlayer2 As Label
    Friend WithEvents btnPlayer2 As Button
    Friend WithEvents lblWinnerText As Label
    Friend WithEvents btnResetBoard As Button
End Class
