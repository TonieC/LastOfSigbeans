Imports System.Data.OleDb
Imports System.Data

Public Class Form4

    Dim connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")
    Dim dt As New DataTable
    Dim currentTable As String = "login" ' Default table

    ' --- FORM LOAD ---
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTable("login")
    End Sub

    ' --- LOAD ANY TABLE ---
    Private Sub LoadTable(tableName As String)
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            dt = New DataTable()
            Dim sql As String = ""

            Select Case tableName
                Case "login"
                    sql = "SELECT EID, username, password, FullName, MobileN, Email, Address FROM [login] ORDER BY EID"
                    Label2.Text = "Viewing: Employees Data"
                Case "request"
                    sql = "SELECT EID, FullName, [Request], Status, RequestDate FROM [request] ORDER BY RequestDate"
                    Label2.Text = "Viewing: Leave Requests"
                Case "attendance"
                    sql = "SELECT Username, FullName, LoginDate, LoginTime, Status FROM [attendance] ORDER BY LoginDate"
                    Label2.Text = "Viewing: Attendance"
            End Select

            Dim da As New OleDbDataAdapter(sql, connect)
            da.Fill(dt)
            DataGridView1.DataSource = dt

            DataGridView1.ReadOnly = False
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            ' LOCK KEY FIELDS
            Select Case tableName
                Case "login", "request"
                    DataGridView1.Columns(0).ReadOnly = True ' EID
            End Select

            currentTable = tableName

        Catch ex As Exception
            MsgBox("Load error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' --- SWITCH TABLE BUTTONS ---
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadTable("login")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        LoadTable("request")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        LoadTable("attendance")
    End Sub

    ' --- UPDATE ROW ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select a row to update")
            Exit Sub
        End If

        Dim selectedRow = DataGridView1.SelectedRows(0)

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Dim cmd As New OleDbCommand("", connect)

            Select Case currentTable
                Case "login"
                    cmd.CommandText = "UPDATE [login] SET username=?, password=?, FullName=?, MobileN=?, Email=?, Address=? WHERE EID=?"
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("username").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("password").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("FullName").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("MobileN").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("Email").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("Address").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("EID").Value

                Case "request"
                    cmd.CommandText = "UPDATE [request] SET FullName=?, [Request]=?, Status=?, RequestDate=? WHERE EID=?"
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("FullName").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("Request").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("Status").Value
                    cmd.Parameters.Add("?", OleDbType.Date).Value = If(selectedRow.Cells("RequestDate").Value IsNot Nothing, selectedRow.Cells("RequestDate").Value, DBNull.Value)
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("EID").Value

                Case "attendance"
                    cmd.CommandText = "UPDATE [attendance] SET FullName=?, LoginDate=?, LoginTime=?, Status=? WHERE Username=?"
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("FullName").Value
                    cmd.Parameters.Add("?", OleDbType.Date).Value = If(selectedRow.Cells("LoginDate").Value IsNot Nothing, selectedRow.Cells("LoginDate").Value, DBNull.Value)
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("LoginTime").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("Status").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = selectedRow.Cells("Username").Value
            End Select

            cmd.ExecuteNonQuery()
            MsgBox("Record updated successfully")
            LoadTable(currentTable)

        Catch ex As Exception
            MsgBox("Update error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' --- DELETE ROW ---
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select a row to delete")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Dim cmd As New OleDbCommand("", connect)

            Select Case currentTable
                Case "login"
                    cmd.CommandText = "DELETE FROM [login] WHERE EID=?"
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.SelectedRows(0).Cells("EID").Value
                Case "request"
                    cmd.CommandText = "DELETE FROM [request] WHERE EID=?"
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.SelectedRows(0).Cells("EID").Value
                Case "attendance"
                    cmd.CommandText = "DELETE FROM [attendance] WHERE Username=?"
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.SelectedRows(0).Cells("Username").Value
            End Select

            cmd.ExecuteNonQuery()
            MsgBox("Record deleted")
            LoadTable(currentTable)

        Catch ex As Exception
            MsgBox("Delete error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' --- ADD NEW ROW ---
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Dim cmd As New OleDbCommand("", connect)

            Select Case currentTable
                Case "login"
                    cmd.CommandText = "INSERT INTO [login] (username, password, FullName, MobileN, Email, Address) VALUES (?, ?, ?, ?, ?, ?)"
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("username").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("password").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("FullName").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("MobileN").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("Email").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("Address").Value

                Case "request"
                    cmd.CommandText = "INSERT INTO [request] (FullName, [Request], Status, RequestDate) VALUES (?, ?, ?, ?)"
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("FullName").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("Request").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("Status").Value
                    cmd.Parameters.Add("?", OleDbType.Date).Value = If(DataGridView1.CurrentRow.Cells("RequestDate").Value IsNot Nothing, DataGridView1.CurrentRow.Cells("RequestDate").Value, DBNull.Value)

                Case "attendance"
                    cmd.CommandText = "INSERT INTO [attendance] (Username, FullName, LoginDate, LoginTime, Status) VALUES (?, ?, ?, ?, ?)"
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("Username").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("FullName").Value
                    cmd.Parameters.Add("?", OleDbType.Date).Value = If(DataGridView1.CurrentRow.Cells("LoginDate").Value IsNot Nothing, DataGridView1.CurrentRow.Cells("LoginDate").Value, DBNull.Value)
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("LoginTime").Value
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.CurrentRow.Cells("Status").Value
            End Select

            cmd.ExecuteNonQuery()
            MsgBox("New record added successfully")
            LoadTable(currentTable)

        Catch ex As Exception
            MsgBox("Add error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' --- REFRESH ---
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadTable(currentTable)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout") = MsgBoxResult.Yes Then
            Dim loginForm As New Form3
            loginForm.Show()
            Close()
        End If
    End Sub
End Class
