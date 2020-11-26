Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AboutLabel.Text = "Justine Hoffa" &
            vbNewLine & "Fall 2020" &
            vbNewLine & "RCET 0265" &
            vbNewLine & "Stans Grocery" &
            vbNewLine & "https://github.com/justinehoffa/StansGrocery"
    End Sub
    Private Sub AboutForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        StansGroceryForm.Show()
    End Sub
End Class