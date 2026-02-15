Imports System.Data.OleDb

Public Class Form7

    Private loggedInUsername As String
    Private employeeID As String
    Private fullName As String
    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    ' Constructor to receive logged-in username
    Public Sub New(username As String)
        InitializeComponent()
        loggedInUsername = username
    End Sub

    ' Form Load
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Fill ComboBox with leave categories
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Sick Leave")
        ComboBox1.Items.Add("Emergency Leave")
        ComboBox1.Items.Add("Vacation Leave")
        ComboBox1.SelectedIndex = -1 ' Nothing selected by default
        TextBox1.Clear()

        ' Load employee ID and FullName from login table
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql As String = "SELECT EID, FullName FROM [login] WHERE username=?"
            Using cmd As New OleDbCommand(sql, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername

                Using reader As OleDbDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        employeeID = reader("EID").ToString()        ' <-- Set employeeID
                        fullName = reader("FullName").ToString()    ' <-- Set fullName
                        Label14.Text = employeeID                   ' Display EID
                        ' Optional: display fullName in another label if needed
                    Else
                        MsgBox("Employee record not found")
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error loading employee data: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub


    ' Leave request message textbox
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Optional validation while typing
    End Sub

    ' Leave category combobox
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Optional: handle category selection change
    End Sub

    ' Submit leave request
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Validate category
        If ComboBox1.SelectedIndex = -1 Then
            MsgBox("Please select a leave category.")
            Exit Sub
        End If

        ' Validate request message
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MsgBox("Please enter your leave request.")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open

            Using cmd As New OleDbCommand(
                "INSERT INTO [request] (EID, FullName, Category, [Request], Status, RequestDate) " &
                "VALUES (?, ?, ?, ?, ?, ?)", connect)

                ' Use loaded employee info
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = employeeID
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = fullName
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = ComboBox1.SelectedItem.ToString
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = TextBox1.Text.Trim
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = "Pending"
                cmd.Parameters.Add("?", OleDbType.Date).Value = Date.Now

                cmd.ExecuteNonQuery
            End Using

            MsgBox("Leave request submitted successfully.")

            ' Clear input
            ComboBox1.SelectedIndex = -1
            TextBox1.Clear

        Catch ex As Exception
            MsgBox("Error submitting leave request: " & ex.Message)
        Finally
            connect.Close
        End Try
    End Sub

    ' Back to profile
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            ' 1. Check the most recent request for this user
            Dim checkSql As String = "SELECT TOP 1 [Request], Status FROM [request] WHERE EID=? ORDER BY RequestDate DESC"
            Using checkCmd As New OleDbCommand(checkSql, connect)
                checkCmd.Parameters.Add("?", OleDbType.VarChar).Value = employeeID

                Using reader As OleDbDataReader = checkCmd.ExecuteReader()
                    If reader.Read() Then
                        Dim lastRequest As String = reader("Request").ToString()
                        Dim status As String = reader("Status").ToString()

                        If status = "Pending" Then
                            ' 2. Delete the pending request
                            Dim deleteSql As String = "DELETE FROM [request] WHERE EID=? AND [Request]=? AND Status='Pending'"
                            Using deleteCmd As New OleDbCommand(deleteSql, connect)
                                deleteCmd.Parameters.Add("?", OleDbType.VarChar).Value = employeeID
                                deleteCmd.Parameters.Add("?", OleDbType.VarChar).Value = lastRequest
                                deleteCmd.ExecuteNonQuery()
                            End Using

                            MsgBox("Pending leave request cancelled successfully.")
                        Else
                            ' 3. If approved/rejected
                            MsgBox($"Last request was already {status}. Cannot cancel.")
                        End If
                    Else
                        ' No requests found
                        MsgBox("No leave requests found to cancel.")
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error cancelling leave request: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

End Class
