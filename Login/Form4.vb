Imports System.Data
Imports System.Data.OleDb
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Form4


    Private ReadOnly connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    Private dt As DataTable
    Private currentTable As String = "user"
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
        LoadTable("user")
        DateTimePicker1.Checked = False
        DateTimePicker1.ShowCheckBox = True
    End Sub

    ' =========================
    ' LOGGING
    ' =========================
    Private Sub AddLog(action As String, details As String)
        Try
            connect.Open()

            Using cmd As New OleDbCommand(
                "INSERT INTO [logs] ([Username], [ActionDate], [Action], [Details]) VALUES (?,?,?,?)", connect)

                cmd.Parameters.AddWithValue("?", loggedInAdmin)
                cmd.Parameters.AddWithValue("?", DateTime.Now)
                cmd.Parameters.AddWithValue("?", action)
                cmd.Parameters.AddWithValue("?", details)

                cmd.ExecuteNonQuery()
            End Using

        Catch
        Finally
            If connect.State = ConnectionState.Open Then connect.Close()
        End Try
    End Sub

    ' =========================
    ' LOAD TABLE
    ' =========================
    Private Sub LoadTable(tableName As String)
        Try
            connect.Open()
            dt = New DataTable()

            Dim sql As String = ""

            Select Case tableName
                Case "user"
                    ' Fixed: FROM clause error solved, keep Status
                    sql = "SELECT [EID], [username], [password], [Role], [FullName], [MobileN], [Email], [Address], [Status] FROM [user]"

                Case "request"
                    sql = "SELECT [UID], [EID], [FullName], [Category], [Request], [Status], [RequestDate] FROM [request]"

                Case "attendance"
                    sql = "SELECT [Username], [FullName], [LoginDate], [LoginTime], [Status] FROM [attendance]"

                Case "logs"
                    sql = "SELECT [Username], [Action], [ActionDate], [Details] FROM [logs] ORDER BY [ActionDate] DESC"
            End Select

            Using da As New OleDbDataAdapter(sql, connect)
                da.Fill(dt)
            End Using

            DataGridView1.DataSource = dt
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            If dt.Columns.Count > 0 Then
                DataGridView1.Columns(0).ReadOnly = True
            End If

            currentTable = tableName

            ' =========================
            ' UPDATE LABEL4
            ' =========================
            Select Case tableName
                Case "user"
                    Label4.Text = "Viewing Employees"
                Case "request"
                    Label4.Text = "Viewing Requests"
                Case "attendance"
                    Label4.Text = "Viewing Attendance"
                Case "logs"
                    Label4.Text = "Viewing Logs"
                Case Else
                    Label4.Text = "Viewing Table"
            End Select

        Catch ex As Exception
            MsgBox("Load error: " & ex.Message)
        Finally
            If connect.State = ConnectionState.Open Then connect.Close()
        End Try
    End Sub

    ' =========================
    ' SWITCH TABLES
    ' =========================
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadTable("user")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        LoadTable("request")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        LoadTable("attendance")
    End Sub

    ' =========================
    ' ADD USER
    ' =========================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If currentTable <> "user" Then Exit Sub

        Dim username As String = InputBox("Enter username:")
        If String.IsNullOrWhiteSpace(username) Then Exit Sub

        Try
            connect.Open()
            Using cmd As New OleDbCommand("INSERT INTO [user] ([username], [Status]) VALUES (?,?)", connect)
                cmd.Parameters.AddWithValue("?", username)
                cmd.Parameters.AddWithValue("?", "Active")
                cmd.ExecuteNonQuery()
            End Using

            AddLog("ADD_USER", username)
            LoadTable("user")

        Catch ex As Exception
            MsgBox("Add error: " & ex.Message)
        Finally
            If connect.State = ConnectionState.Open Then connect.Close()
        End Try
    End Sub

    ' =========================
    ' UPDATE
    ' =========================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If DataGridView1.SelectedRows.Count = 0 Then Exit Sub
        Dim r = DataGridView1.SelectedRows(0)

        Try
            connect.Open()
            Using cmd As New OleDbCommand("", connect)

                Select Case currentTable
                    Case "user"
                        cmd.CommandText =
                            "UPDATE [user] SET [username]=?, [password]=?, [Role]=?, [FullName]=?, [MobileN]=?, [Email]=?, [Address]=?, [Status]=? WHERE [EID]=?"
                        cmd.Parameters.AddWithValue("?", r.Cells("username").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("password").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("Role").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("FullName").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("MobileN").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("Email").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("Address").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("Status").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("EID").Value)

                    Case "request"
                        cmd.CommandText = "UPDATE [request] SET [Status]=? WHERE [UID]=?"
                        cmd.Parameters.AddWithValue("?", r.Cells("Status").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("UID").Value)

                    Case "attendance"
                        cmd.CommandText = "UPDATE [attendance] SET [Status]=? WHERE [Username]=? AND [LoginDate]=?"
                        cmd.Parameters.AddWithValue("?", r.Cells("Status").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("Username").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("LoginDate").Value)
                End Select

                cmd.ExecuteNonQuery()
            End Using

            AddLog("UPDATE", currentTable)
            LoadTable(currentTable)

        Catch ex As Exception
            MsgBox("Update error: " & ex.Message)
        Finally
            If connect.State = ConnectionState.Open Then connect.Close()
        End Try
    End Sub

    ' =========================
    ' DELETE
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If DataGridView1.SelectedRows.Count = 0 Then Exit Sub
        If MsgBox("Delete selected record?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub

        Dim r = DataGridView1.SelectedRows(0)

        Try
            connect.Open()
            Using cmd As New OleDbCommand("", connect)

                Select Case currentTable
                    Case "user"
                        cmd.CommandText = "DELETE FROM [user] WHERE [EID]=?"
                        cmd.Parameters.AddWithValue("?", r.Cells("EID").Value)

                    Case "request"
                        cmd.CommandText = "DELETE FROM [request] WHERE [UID]=?"
                        cmd.Parameters.AddWithValue("?", r.Cells("UID").Value)

                    Case "attendance"
                        cmd.CommandText = "DELETE FROM [attendance] WHERE [Username]=? AND [LoginDate]=?"
                        cmd.Parameters.AddWithValue("?", r.Cells("Username").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("LoginDate").Value)
                End Select

                cmd.ExecuteNonQuery()
            End Using

            AddLog("DELETE", currentTable)
            LoadTable(currentTable)

        Catch ex As Exception
            MsgBox("Delete error: " & ex.Message)
        Finally
            If connect.State = ConnectionState.Open Then connect.Close()
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ApplyFilters()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        ApplyFilters()
    End Sub

    Private Sub ApplyFilters()

        If dt Is Nothing Then Exit Sub

        Dim dv As New DataView(dt)

        Dim searchText As String = TextBox1.Text.Replace("'", "''")
        Dim filter As String = ""

        ' =========================
        ' TEXT SEARCH FILTER
        ' =========================
        If Not String.IsNullOrWhiteSpace(searchText) Then

            Select Case currentTable

                Case "user"
                    filter = $"username LIKE '%{searchText}%' OR FullName LIKE '%{searchText}%' OR Role LIKE '%{searchText}%'"

                Case "request"
                    filter = $"FullName LIKE '%{searchText}%' OR Category LIKE '%{searchText}%'"

                Case "attendance"
                    filter = $"Username LIKE '%{searchText}%' OR FullName LIKE '%{searchText}%'"

                Case "logs"
                    filter = $"Username LIKE '%{searchText}%' OR Action LIKE '%{searchText}%'"

            End Select

        End If

        ' =========================
        ' DATE FILTER (ONLY IF TOGGLED)
        ' =========================
        If DateTimePicker1.Checked Then

            Dim selectedDate As String = DateTimePicker1.Value.ToString("MM/dd/yyyy")

            Select Case currentTable

                Case "request"
                    If filter <> "" Then filter &= " AND "
                    filter &= $"CONVERT(RequestDate, 'System.String') LIKE '%{selectedDate}%'"

                Case "attendance"
                    If filter <> "" Then filter &= " AND "
                    filter &= $"CONVERT(LoginDate, 'System.String') LIKE '%{selectedDate}%'"

                Case "logs"
                    If filter <> "" Then filter &= " AND "
                    filter &= $"CONVERT(ActionDate, 'System.String') LIKE '%{selectedDate}%'"

            End Select
        End If

        dv.RowFilter = filter
        DataGridView1.DataSource = dv

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Me.Hide()
    End Sub
End Class