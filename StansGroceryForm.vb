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
    Dim array4(255, 2), sortedAisle(16), sortedCategory(23) As String
    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim file As String = My.Resources.StansGrocery
        Timer1.Start()
        StansGrocerySplashScreen.BackgroundImageLayout = ImageLayout.Stretch
        StansGrocerySplashScreen.BackgroundImage = My.Resources.stans
        StansGrocerySplashScreen.Size = Me.Size
        StansGrocerySplashScreen.Show()
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
        AisleSorter()
        CategorySorter()
        FilterComboBox.SelectedItem = "Show All"
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        StansGrocerySplashScreen.Hide()
    End Sub

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Dim goodData As Boolean
        Dim searchMatch As Match
        goodData = False
        DisplayListBox.Items.Clear()
        DisplayLabel.Text = String.Empty
        If SearchTextBox.TextLength = 1 Then
            DisplayLabel.Text = "Please be more specific."
            Exit Sub
        ElseIf SearchTextBox.Text = "zzz" Then
            Me.Close()
        End If
        For i = 0 To UBound(array4) - 1
            searchMatch = Regex.Match(array4(i, 0), "\b" & SearchTextBox.Text, RegexOptions.IgnoreCase)
            If searchMatch.Success = True Then
                DisplayListBox.Items.Add(array4(i, 0))
                goodData = True
            End If
        Next
        If goodData = False Then
            DisplayLabel.Text = $"Sorry, no matches for {SearchTextBox.Text}"
        End If
        DisplayListBox.Items.Remove("")
    End Sub

    Private Sub FilterComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FilterComboBox.SelectedIndexChanged
        FilterComboBox.SelectedItem.ToString()
        DisplayListBox.Items.Clear()
        For i = 0 To 255
            If FilterComboBox.SelectedItem.ToString() = "Show All" Then
                DisplayListBox.Items.Add(array4(i, 0))
            End If
            For j = 0 To 2
                If FilterComboBox.SelectedItem.ToString() = array4(i, j) Then
                    DisplayListBox.Items.Add(array4(i, 0))
                End If
            Next
        Next
        DisplayListBox.Items.Remove("")
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Me.Close()
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AisleRadioButton.CheckedChanged, CategoryRadioButton.CheckedChanged
        If AisleRadioButton.Checked = True Then
            FilterComboBox.Items.Clear()
            FilterComboBox.Items.Add("Show All")
            FilterComboBox.Items.Add("Choose Aisle...")
            FilterComboBox.SelectedItem = "Choose Aisle..."
            For i = 0 To UBound(sortedAisle)
                FilterComboBox.Items.Add(sortedAisle(i))
            Next
        Else
            FilterComboBox.Items.Clear()
            FilterComboBox.Items.Add("Show All")
            FilterComboBox.Items.Add("Choose Category...")
            FilterComboBox.SelectedItem = "Choose Category..."
            For j = 0 To UBound(sortedAisle)
                FilterComboBox.Items.Add(sortedCategory(j))
            Next
        End If
        FilterComboBox.Items.Remove("")
    End Sub

    Private Sub DisplayListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DisplayListBox.SelectedIndexChanged
        For i = 0 To 255
            For j = 0 To 2
                If DisplayListBox.SelectedItem.ToString = array4(i, j) Then
                    DisplayLabel.Text = "You will find " & array4(i, j) & " on aisle " &
                        array4(i, j + 1) & " with the " & array4(i, j + 2)
                End If
            Next
        Next
    End Sub

    Sub AisleSorter()
        Dim aisle(UBound(array4)) As String
        For i = 0 To UBound(array4)
            aisle(i) = array4(i, 1)
        Next
        Dim preDedupe As String = String.Join(",", aisle)
        Dim dedupe As String = DeDupeinator(preDedupe)
        sortedAisle = Regex.Split(dedupe, ",")
        Array.Sort(sortedAisle)
        Console.Read()
    End Sub

    Sub CategorySorter()
        Dim category(UBound(array4)) As String
        For i = 0 To UBound(array4)
            category(i) = array4(i, 2)
        Next
        Dim preDedupe As String = String.Join(",", category)
        Dim dedupe As String = DeDupeinator(preDedupe)
        sortedCategory = Regex.Split(dedupe, ",")
        Array.Sort(sortedCategory)
        Console.Read()
    End Sub

    Function DeDupeinator(ByVal sInput As String, Optional ByVal sDelimiter As String = ",") As String
        Dim varSection, sTemp As String
        For Each varSection In Split(sInput, sDelimiter)
            If InStr(1, sDelimiter & sTemp & sDelimiter, sDelimiter & varSection & sDelimiter, vbTextCompare) = 0 Then
                sTemp = sTemp & sDelimiter & varSection
            End If
        Next varSection
        DeDupeinator = Mid(sTemp, Len(sDelimiter) + 1)
    End Function

    Private Sub AboutTopMenuItem_Click(sender As Object, e As EventArgs) Handles AboutTopMenuItem.Click
        Form1.Size = Me.Size()
        Form1.Show()
        Me.Hide()
    End Sub
End Class

