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

    Dim array4(255, 2) As String

    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim file As String = My.Resources.StansGrocery

        Timer1.Start()
        SplashScreenForm.BackgroundImageLayout = ImageLayout.Stretch
        SplashScreenForm.BackgroundImage = My.Resources.stans
        SplashScreenForm.Size = Me.Size
        SplashScreenForm.Show()
        Me.Show()

        Dim string1, string2, string3 As String
        Dim array1(), array2() As String
        Dim match1 As Match
        Dim integer1, integer2, integer3, integer4 As Integer

        string1 = Regex.Replace(My.Resources.StansGrocery, "/", "✷")
        string2 = Regex.Replace(string1, "\p{P}|\p{Sc}", "")
        array1 = Regex.Split(string2, vbLf)
        Array.Sort(array1)
        string3 = String.Join("", array1)
        array2 = Regex.Split(string3, "(?=ITM)|(?=CAT)|(?=LOC)")

        For i = 0 To UBound(array2)
            match1 = Regex.Match(array2(i), "ITM")
            If match1.Success = True Then
                integer1 += 1
            End If
        Next

        Dim array3(integer1 - 1, 2) As String
        integer2 = 0

        For i = 0 To UBound(array2)
            match1 = Regex.Match(array2(i), "ITM")
            If match1.Success = True Then
                array3(integer2, 0) = array2(i)
                integer2 += 1
            End If
        Next

        For i = 0 To UBound(array2)
            match1 = Regex.Match(array2(i), "LOC")
            If match1.Success = True Then
                array3(integer3, 1) = array2(i)
                integer3 += 1
            End If
        Next

        For i = 0 To UBound(array2)
            match1 = Regex.Match(array2(i), "CAT")
            If match1.Success = True Then
                array3(integer4, 2) = array2(i)
                integer4 += 1
            End If
        Next

        For i = 0 To UBound(array3) - 1
            For j = 0 To 2
                array3(i, j) = Regex.Replace(array3(i, j), "✷", "/")
                array3(i, j) = Regex.Replace(array3(i, j), "ITM", "")
                array3(i, j) = Regex.Replace(array3(i, j), "CAT", "")
                array3(i, j) = Regex.Replace(array3(i, j), "LOC", "")
            Next
        Next

        array4 = array3

        For i = 0 To UBound(array3) - 1
            For j = 0 To 2
                DisplayListBox.Items.Add(array3(i, j))
            Next
        Next

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        SplashScreenForm.Hide()
    End Sub


End Class
