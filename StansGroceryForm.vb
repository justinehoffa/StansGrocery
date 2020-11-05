'Justine Hoffa 
'RCET0265
'Fall2020
'StansGrocery
'https://github.com/justinehoffa/StansGrocery

Option Strict On
Option Explicit On
Option Compare Binary

Public Class StansGroceryForm

    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim file As String = My.Resources.StansGrocery

        Timer1.Start()
        SplashScreenForm.BackgroundImageLayout = ImageLayout.Stretch
        SplashScreenForm.BackgroundImage = My.Resources.stans
        SplashScreenForm.Size = Me.Size
        SplashScreenForm.Show()
        Me.Show()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        SplashScreenForm.Hide()
    End Sub
End Class
