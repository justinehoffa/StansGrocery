'Justine Hoffa 
'RCET0265
'Fall2020
'StansGrocery
'https://github.com/justinehoffa/StansGrocery

Option Strict On
Option Explicit On
Option Compare Binary

Imports System.Text.RegularExpressions

Public Class StansGroceryForm

    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim file As String = My.Resources.StansGrocery

        Timer1.Start()
        SplashScreenForm.BackgroundImageLayout = ImageLayout.Stretch
        SplashScreenForm.BackgroundImage = My.Resources.stans
        SplashScreenForm.Size = Me.Size
        SplashScreenForm.Show()
        Me.Show()

        Dim string1, string2, string3 As String
        Dim array1(), array2(), array3() As String
        Dim match1 As Match
        Dim integer1, integer2, integer3, integer4 As Integer

        string1 = Regex.Replace(My.Resources.StansGrocery, "/", "✷")
        array1 = Regex.Split(string1, "\p{P}|\p{Sc}")
        string2 = String.Join("", array1)
        array2 = Regex.Split(string2, vbLf)
        Array.Sort(array2)
        string3 = String.Join("", array2)
        array3 = Regex.Split(string3, "(?=ITM)|(?=LOC)|(?=CAT)")

        For i = 0 To UBound(array3)
            match1 = Regex.Match(array3(i), "ITM")
            If match1.Success = True Then
                integer1 += 1
            End If
        Next

        Dim array4(integer1 - 1, 2) As String
        integer2 = 0

        For i = 0 To UBound(array3)
            match1 = Regex.Match(array3(i), "ITM")
            If match1.Success = True Then
                array4(integer2, 0) = array3(i)
                integer2 += 1
            End If
        Next

        For i = 0 To UBound(array3)
            match1 = Regex.Match(array3(i), "LOC")
            If match1.Success = True Then
                array4(integer3, 1) = array3(i)
                integer3 += 1
            End If
        Next

        For i = 0 To UBound(array3)
            match1 = Regex.Match(array3(i), "CAT")
            If match1.Success = True Then
                array4(integer4, 2) = array3(i)
                integer4 += 1
            End If
        Next

        For i = 0 To UBound(array4)
            For j = 0 To 2
                DisplayListBox.Items.Add(array4(i, j))
            Next
        Next


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        SplashScreenForm.Hide()
    End Sub
End Class
