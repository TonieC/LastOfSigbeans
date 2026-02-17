Imports System.Data
Imports System.Data.OleDb

Public Class Form7

    Private loggedInUsername As String
    Private employeeID As String
    Private fullName As String

    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    ' =========================
    ' CONSTRUCTOR
    ' =========================
    Public Sub New(username As String)
        InitializeComponent()
        loggedInUsername = username
    End Sub

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Sick Leave")
        ComboBox1.Items.Add("Emergency Leave")
        ComboBox1.Items.Add("Vacation Leave")
        ComboBox1.SelectedIndex = -1

        TextBox1.Clear()

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Using cmd As New OleDbCommand(
                "SELECT EID, FullName FROM [login] WHERE username=?", connect)

                cmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername

                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        employeeID = reader("EID").ToString()
                        fullName = reader("FullName").ToString()
                        Label14.Text = employeeID
                    Else
                        MsgBox("Employee record not found.")
                        Close()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error loading employee data: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' GET NEXT UID (001, 002…)
    ' =========================
    Private Function GetNextUID() As Integer
        Dim nextUID As Integer = 1

        Using cmd As New OleDbCommand(
            "SELECT MAX(UID) FROM [request]", connect)

            Dim result = cmd.ExecuteScalar()

            If Not IsDBNull(result) Then
                nextUID = CInt(result) + 1
            End If
        End Using

        Return nextUID
    End Function

    ' =========================
    ' SUBMIT REQUEST
    ' =========================
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        If ComboBox1.SelectedIndex = -1 Then
            MsgBox("Please select a leave category.")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MsgBox("Please enter your leave request.")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim uid As Integer = GetNextUID()

            Using cmd As New OleDbCommand(
                "INSERT INTO [request] " &
                "(UID, EID, FullName, Category, [Request], Status, RequestDate) " &
                "VALUES (?, ?, ?, ?, ?, ?, ?)", connect)

                cmd.Parameters.Add("?", OleDbType.Integer).Value = uid
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = employeeID
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = fullName
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = ComboBox1.SelectedItem.ToString()
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = TextBox1.Text.Trim()
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = "Pending"
                cmd.Parameters.Add("?", OleDbType.Date).Value = Date.Now

                cmd.ExecuteNonQuery()
            End Using

            MsgBox($"Leave request submitted successfully. UID: {uid:000}")

            ComboBox1.SelectedIndex = -1
            TextBox1.Clear()

        Catch ex As Exception
            MsgBox("Error submitting leave request: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' CANCEL LAST PENDING REQUEST
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Using cmd As New OleDbCommand(
                "SELECT TOP 1 UID, Status FROM [request] " &
                "WHERE EID=? ORDER BY RequestDate DESC", connect)

                cmd.Parameters.Add("?", OleDbType.VarChar).Value = employeeID

                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then

                        Dim uid As Integer = CInt(reader("UID"))
                        Dim status As String = reader("Status").ToString()

                        If status <> "Pending" Then
                            MsgBox($"Last request already {status}. Cannot cancel.")
                            Exit Sub
                        End If

                        Using delCmd As New OleDbCommand(
                            "DELETE FROM [request] WHERE UID=?", connect)

                            delCmd.Parameters.Add("?", OleDbType.Integer).Value = uid
                            delCmd.ExecuteNonQuery()
                        End Using

                        MsgBox($"Pending request UID {uid:000} cancelled successfully.")
                    Else
                        MsgBox("No leave requests found.")
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error cancelling request: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' CLOSE FORM
    ' =========================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

End Class
