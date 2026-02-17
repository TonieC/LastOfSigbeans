Imports System.Data
Imports System.Data.OleDb
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Form6

    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    Private dt As DataTable
    Private currentTable As String = "request"
    Private loggedInStaffUsername As String

    ' =========================
    ' CONSTRUCTOR
    ' =========================
    Public Sub New(Optional username As String = "")
        InitializeComponent()
        loggedInStaffUsername = username
    End Sub

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub StaffDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTable("request")
    End Sub

    ' =========================
    ' LOGGING
    ' =========================
    Private Sub AddLog(action As String, details As String)
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Using cmd As New OleDbCommand(
                "INSERT INTO [logs] (Username, ActionDate, [Action], Details) VALUES (?, ?, ?, ?)", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInStaffUsername
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = action
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = details
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MsgBox("Log error: " & ex.Message)
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
                    sql = "SELECT EID, username, FullName, MobileN, Email, Address, Gender, Age, Department FROM [login] ORDER BY EID"
                    Label2.Text = "Viewing: Employees"

                Case "request"
                    sql = "SELECT UID, EID, FullName, Category, [Request], Status, RequestDate FROM [request] ORDER BY RequestDate DESC"
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

            ' Configure read-only per table
            DataGridView1.ReadOnly = True
            If tableName = "request" Then
                DataGridView1.ReadOnly = False
                For Each colName In {"UID", "EID", "FullName", "Category", "Request", "RequestDate"}
                    DataGridView1.Columns(colName).ReadOnly = True
                Next
            ElseIf tableName = "login" Then
                ' Only allow editing of non-ID fields if needed
                For Each colName In {"EID", "username"}
                    DataGridView1.Columns(colName).ReadOnly = True
                Next
            End If

            currentTable = tableName
            AddLog("Table Switch", $"Switched to {tableName}")

        Catch ex As Exception
            MsgBox("Load error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' TABLE SWITCH BUTTONS
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
    ' APPROVE / REJECT REQUEST
    ' =========================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        UpdateRequestStatus("Approved")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UpdateRequestStatus("Rejected")
    End Sub

    Private Sub UpdateRequestStatus(newStatus As String)
        If currentTable <> "request" OrElse DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select a request first.")
            Exit Sub
        End If

        Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)
        Dim uid As Integer = CInt(row.Cells("UID").Value)

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Using cmd As New OleDbCommand("UPDATE [request] SET Status=? WHERE UID=?", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = newStatus
                cmd.Parameters.Add("?", OleDbType.Integer).Value = uid
                If cmd.ExecuteNonQuery() = 0 Then
                    MsgBox("Update failed.")
                    Exit Sub
                End If
            End Using

            MsgBox($"Request {newStatus.ToLower()} successfully.")
            AddLog("Request Update", $"UID {uid} set to {newStatus}")
            LoadTable("request")
        Catch ex As Exception
            MsgBox("Update error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' REFRESH TABLE
    ' =========================
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadTable(currentTable)
        AddLog("Refresh", currentTable)
    End Sub

    ' =========================
    ' LOGOUT
    ' =========================
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MsgBox("Logout?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            AddLog("Logout", "Staff logged out")
            Dim f As New Form3
            f.Show()
            Close()
        End If
    End Sub

    ' =========================
    ' EXPORT TO PDF
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.Rows.Count = 0 Then Exit Sub

        Using dlg As New SaveFileDialog With {
            .Filter = "PDF (*.pdf)|*.pdf",
            .FileName = currentTable & "_Export.pdf"
        }
            If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim doc As New Document(PageSize.A4)
            PdfWriter.GetInstance(doc, New FileStream(dlg.FileName, FileMode.Create))
            doc.Open()

            Dim table As New PdfPTable(DataGridView1.Columns.Count)
            table.WidthPercentage = 100

            ' Add headers
            For Each col As DataGridViewColumn In DataGridView1.Columns
                table.AddCell(col.HeaderText)
            Next

            ' Add rows
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    For Each cell As DataGridViewCell In row.Cells
                        table.AddCell(If(cell.Value, "").ToString())
                    Next
                End If
            Next

            doc.Add(table)
            doc.Close()
            MsgBox("PDF exported.")
            AddLog("Export PDF", currentTable)
        End Using
    End Sub

    ' =========================
    ' SEARCH
    ' =========================
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If dt Is Nothing Then Exit Sub

        Dim filter As String = TextBox1.Text.Replace("'", "''")
        Dim dv As New DataView(dt)

        If filter <> "" Then
            Select Case currentTable
                Case "request"
                    dv.RowFilter = $"Convert(UID,'System.String') LIKE '%{filter}%' OR " &
                                   $"EID LIKE '%{filter}%' OR FullName LIKE '%{filter}%' OR " &
                                   $"Category LIKE '%{filter}%' OR Status LIKE '%{filter}%'"
                Case "login"
                    dv.RowFilter = $"Convert(EID,'System.String') LIKE '%{filter}%' OR " &
                                   $"username LIKE '%{filter}%' OR FullName LIKE '%{filter}%' OR " &
                                   $"MobileN LIKE '%{filter}%' OR Email LIKE '%{filter}%' OR " &
                                   $"Address LIKE '%{filter}%' OR Gender LIKE '%{filter}%' OR " &
                                   $"Convert(Age,'System.String') LIKE '%{filter}%' OR Department LIKE '%{filter}%'"
                Case "attendance"
                    dv.RowFilter = $"Username LIKE '%{filter}%' OR FullName LIKE '%{filter}%' OR Status LIKE '%{filter}%'"
            End Select
        End If

        DataGridView1.DataSource = dv
    End Sub

    ' =========================
    ' VIEW LOGS
    ' =========================
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If String.IsNullOrEmpty(loggedInStaffUsername) Then
            MsgBox("Staff identity not loaded.")
            Exit Sub
        End If

        AddLog("Open Logs", "Opened logs viewer")
        Dim logsForm As New Form8(loggedInStaffUsername)
        logsForm.ShowDialog()
    End Sub

End Class
