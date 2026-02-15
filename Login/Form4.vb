Imports System.Data
Imports System.Data.OleDb

Public Class Form4

    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    Private dt As DataTable
    Private currentTable As String = "login"
    Private loggedInAdmin As String

    ' =========================
    ' CONSTRUCTOR
    ' =========================
    Public Sub New(adminUsername As String)
        InitializeComponent()
        loggedInAdmin = adminUsername
    End Sub

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTable("login")
    End Sub

    ' =========================
    ' LOGGING FUNCTION
    ' =========================
    Private Sub AddLog(action As String, details As String)
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Using cmd As New OleDbCommand(
                "INSERT INTO [logs] (Username, ActionDate, [Action], Details) VALUES (?, ?, ?, ?)", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInAdmin
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = action
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = details
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MsgBox("Error logging action: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' LOAD TABLE
    ' =========================
    Private Sub LoadTable(tableName As String)
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            dt = New DataTable()
            Dim sql As String = ""

            Select Case tableName
                Case "login"
                    sql = "SELECT EID, username, password, Role, FullName, MobileN, Email, Address FROM [login] ORDER BY EID"
                    Label2.Text = "Viewing: Employees"

                Case "request"
                    sql = "SELECT EID, FullName, Category, [Request], Status, RequestDate FROM [request] ORDER BY RequestDate DESC"
                    Label2.Text = "Viewing: Leave Requests"

                Case "attendance"
                    sql = "SELECT Username, FullName, LoginDate, LoginTime, Status FROM [attendance] ORDER BY LoginDate DESC"
                    Label2.Text = "Viewing: Attendance"
            End Select

            Dim da As New OleDbDataAdapter(sql, connect)
            da.Fill(dt)

            DataGridView1.DataSource = dt
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.ReadOnly = False

            ' Always lock primary key
            DataGridView1.Columns(0).ReadOnly = True

            currentTable = tableName
            AddLog("Table Switch", $"Switched to table: {tableName}")
        Catch ex As Exception
            MsgBox("Load error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' TABLE SWITCHING BUTTONS
    ' =========================
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadTable("login")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        LoadTable("request")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        LoadTable("attendance")
    End Sub

    ' =========================
    ' UPDATE RECORD (UNRESTRICTED)
    ' =========================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select a row to update")
            Exit Sub
        End If

        Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Using cmd As New OleDbCommand("", connect)
                Select Case currentTable
                    Case "login"
                        cmd.CommandText = "UPDATE [login] " &
                      "SET [username]=?, [password]=?, [Role]=?, [FullName]=?, [MobileN]=?, [Email]=?, [Address]=? " &
                      "WHERE [EID]=?"
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("username").Value Is Nothing, "", row.Cells("username").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("password").Value Is Nothing, "", row.Cells("password").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("Role").Value Is Nothing, "", row.Cells("Role").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("FullName").Value Is Nothing, "", row.Cells("FullName").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("MobileN").Value Is Nothing, "", row.Cells("MobileN").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("Email").Value Is Nothing, "", row.Cells("Email").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("Address").Value Is Nothing, "", row.Cells("Address").Value)
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = CInt(row.Cells("EID").Value)

                    Case "request"
                        cmd.CommandText = "UPDATE [request] SET FullName=?, Category=?, [Request]=?, Status=?, RequestDate=? WHERE EID=?"
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("FullName").Value Is Nothing, "", row.Cells("FullName").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("Category").Value Is Nothing, "", row.Cells("Category").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("Request").Value Is Nothing, "", row.Cells("Request").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("Status").Value Is Nothing, "", row.Cells("Status").Value)
                        cmd.Parameters.Add("?", OleDbType.Date).Value = If(row.Cells("RequestDate").Value Is Nothing, DateTime.Now, row.Cells("RequestDate").Value)
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = CInt(row.Cells("EID").Value)

                    Case "attendance"
                        cmd.CommandText = "UPDATE [attendance] SET FullName=?, LoginDate=?, LoginTime=?, Status=? WHERE Username=?"
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("FullName").Value Is Nothing, "", row.Cells("FullName").Value)
                        cmd.Parameters.Add("?", OleDbType.Date).Value = If(row.Cells("LoginDate").Value Is Nothing, DateTime.Now, row.Cells("LoginDate").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("LoginTime").Value Is Nothing, "", row.Cells("LoginTime").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(row.Cells("Status").Value Is Nothing, "", row.Cells("Status").Value)
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = row.Cells("Username").Value
                End Select

                Dim affected As Integer = cmd.ExecuteNonQuery()
                MsgBox("Record updated successfully")
                AddLog("Update", $"Updated row in {currentTable} (ID: {row.Cells(0).Value})")
                LoadTable(currentTable)
            End Using

        Catch ex As Exception
            MsgBox("Update error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' DELETE RECORD
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select a row to delete")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Using cmd As New OleDbCommand("", connect)
                Select Case currentTable
                    Case "login"
                        cmd.CommandText = "DELETE FROM [login] WHERE EID=?"
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = DataGridView1.SelectedRows(0).Cells("EID").Value

                    Case "request"
                        cmd.CommandText = "DELETE FROM [request] WHERE EID=?"
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = DataGridView1.SelectedRows(0).Cells("EID").Value

                    Case "attendance"
                        cmd.CommandText = "DELETE FROM [attendance] WHERE Username=?"
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.SelectedRows(0).Cells("Username").Value
                End Select

                cmd.ExecuteNonQuery()
                MsgBox("Record deleted")
                AddLog("Delete", $"Deleted row in {currentTable} (ID: {DataGridView1.SelectedRows(0).Cells(0).Value})")
                LoadTable(currentTable)
            End Using

        Catch ex As Exception
            MsgBox("Delete error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' SEARCH
    ' =========================
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If dt Is Nothing Then Exit Sub
        Dim filter As String = TextBox1.Text.Trim().Replace("'", "''")
        If filter = "" Then
            DataGridView1.DataSource = dt
            Exit Sub
        End If

        Dim dv As New DataView(dt)
        Select Case currentTable
            Case "login"
                dv.RowFilter = String.Format(
                    "Convert(EID,'System.String') LIKE '%{0}%' OR username LIKE '%{0}%' OR Role LIKE '%{0}%' OR FullName LIKE '%{0}%' OR MobileN LIKE '%{0}%' OR Email LIKE '%{0}%' OR Address LIKE '%{0}%'", filter)
            Case "request"
                dv.RowFilter = String.Format(
                    "Convert(EID,'System.String') LIKE '%{0}%' OR FullName LIKE '%{0}%' OR Category LIKE '%{0}%' OR [Request] LIKE '%{0}%' OR Status LIKE '%{0}%'", filter)
            Case "attendance"
                dv.RowFilter = String.Format(
                    "Username LIKE '%{0}%' OR FullName LIKE '%{0}%' OR Status LIKE '%{0}%'", filter)
        End Select

        DataGridView1.DataSource = dv
        AddLog("Search", $"Searched '{filter}' in {currentTable}")
    End Sub

    ' =========================
    ' LOGOUT
    ' =========================
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            AddLog("Logout", "Admin logged out")
            Dim f As New Form3
            f.Show()
            Close()
        End If
    End Sub

End Class
