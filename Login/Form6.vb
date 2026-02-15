Imports System.Data
Imports System.Data.OleDb
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class form6

    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    Private dt As DataTable
    Private currentTable As String = "request"
    Private loggedInStaffUsername As String

    ' =========================
    ' SINGLE CONSTRUCTOR (OPTIONAL USERNAME)
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
    ' LOGGING FUNCTION
    ' =========================
    Private Sub AddLog(action As String, details As String)
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Using cmd As New OleDbCommand("INSERT INTO [logs] (Username, ActionDate, [Action], Details) VALUES (?, ?, ?, ?)", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInStaffUsername
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
                    sql = "SELECT EID, username, FullName, MobileN, Email, Address FROM [login] ORDER BY EID"
                    Label2.Text = "Viewing: Employees"

                Case "request"
                    ' Select all actual columns
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
            DataGridView1.ReadOnly = True

            ' Only REQUESTS can be updated (status only)
            If tableName = "request" Then
                DataGridView1.ReadOnly = False
                DataGridView1.Columns("EID").ReadOnly = True
                DataGridView1.Columns("FullName").ReadOnly = True
                DataGridView1.Columns("Category").ReadOnly = True
                DataGridView1.Columns("Request").ReadOnly = True
                DataGridView1.Columns("RequestDate").ReadOnly = True
            End If

            currentTable = tableName
            AddLog("Table Switch", "Switched to table: " & tableName)

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
    ' APPROVE / REJECT
    ' =========================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        UpdateRequestStatus("Approved")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UpdateRequestStatus("Rejected")
    End Sub

    ' =========================
    ' UPDATE REQUEST STATUS
    ' =========================
    Private Sub UpdateRequestStatus(newStatus As String)
        If currentTable <> "request" OrElse DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select a request to update")
            Exit Sub
        End If

        Dim dgvRow As DataGridViewRow = DataGridView1.SelectedRows(0)
        Dim eid As String = dgvRow.Cells("EID").Value.ToString() ' Use EID as unique identifier

        Try
            ' Update DataGridView first
            dgvRow.Cells("Status").Value = newStatus

            ' Update database using EID
            If connect.State = ConnectionState.Closed Then connect.Open()
            Using cmd As New OleDbCommand("UPDATE [request] SET Status=? WHERE EID=?", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = newStatus
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = eid

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected = 0 Then
                    MsgBox("No rows were updated. Something went wrong.")
                    Exit Sub
                End If
            End Using

            MsgBox("Request " & newStatus.ToLower() & " successfully")
            AddLog("Request " & newStatus, $"Request (EID: {eid}) updated to {newStatus}")

            LoadTable(currentTable)
        Catch ex As Exception
            MsgBox("Update error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub


    ' =========================
    ' REFRESH
    ' =========================
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadTable(currentTable)
        AddLog("Refresh", "Refreshed table: " & currentTable)
    End Sub

    ' =========================
    ' LOGOUT
    ' =========================
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
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
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("No data to export")
            Exit Sub
        End If

        Try
            Dim saveDlg As New SaveFileDialog With {
                .Filter = "PDF files (*.pdf)|*.pdf",
                .FileName = currentTable & "_Export.pdf"
            }

            If saveDlg.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim doc As New Document(PageSize.A4, 20, 20, 20, 20)
            PdfWriter.GetInstance(doc, New FileStream(saveDlg.FileName, FileMode.Create))
            doc.Open()

            doc.Add(New Paragraph("Export of " & currentTable))
            doc.Add(New Paragraph("Generated on: " & DateTime.Now))
            doc.Add(New Paragraph(" "))

            Dim pdfTable As New PdfPTable(DataGridView1.Columns.Count)
            pdfTable.WidthPercentage = 100

            For Each col As DataGridViewColumn In DataGridView1.Columns
                pdfTable.AddCell(New Phrase(col.HeaderText))
            Next

            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    For Each cell As DataGridViewCell In row.Cells
                        pdfTable.AddCell(If(cell.Value, "").ToString())
                    Next
                End If
            Next

            doc.Add(pdfTable)
            doc.Close()

            MsgBox("PDF exported successfully")
            AddLog("Export PDF", "Exported " & currentTable & " to PDF")
        Catch ex As Exception
            MsgBox("Export error: " & ex.Message)
        End Try
    End Sub

    ' =========================
    ' SEARCH
    ' =========================
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If dt Is Nothing Then Exit Sub

        Dim filter As String = TextBox1.Text.Trim().Replace("'", "''")
        Dim dv As New DataView(dt)
        Dim rowFilter As String = ""

        ' Text filter including all fields
        If filter <> "" Then
            Dim textFilter As String = ""
            Select Case currentTable
                Case "login"
                    textFilter = $"(EID LIKE '%{filter}%' OR username LIKE '%{filter}%' OR FullName LIKE '%{filter}%' OR MobileN LIKE '%{filter}%' OR Email LIKE '%{filter}%' OR Address LIKE '%{filter}%')"
                Case "request"
                    textFilter = $"(EID LIKE '%{filter}%' OR FullName LIKE '%{filter}%' OR Category LIKE '%{filter}%' OR [Request] LIKE '%{filter}%' OR Status LIKE '%{filter}%')"
                Case "attendance"
                    textFilter = $"(Username LIKE '%{filter}%' OR FullName LIKE '%{filter}%' OR LoginTime LIKE '%{filter}%' OR Status LIKE '%{filter}%')"
            End Select

            rowFilter = textFilter
        End If

        ' Date filter using start/end of day
        If DateTimePicker1.Checked Then
            Dim startDate As Date = DateTimePicker1.Value.Date
            Dim nextDate As Date = startDate.AddDays(1)
            Dim dateFilter As String = ""

            Select Case currentTable
                Case "request"
                    dateFilter = $"RequestDate IS NOT NULL AND RequestDate >= #{startDate:MM/dd/yyyy}# AND RequestDate < #{nextDate:MM/dd/yyyy}#"
                Case "attendance"
                    dateFilter = $"LoginDate IS NOT NULL AND LoginDate >= #{startDate:MM/dd/yyyy}# AND LoginDate < #{nextDate:MM/dd/yyyy}#"
            End Select

            If dateFilter <> "" Then
                If rowFilter <> "" Then rowFilter &= " AND "
                rowFilter &= dateFilter
            End If
        End If

        dv.RowFilter = rowFilter
        DataGridView1.DataSource = dv

        AddLog("Search", $"Searched '{filter}' with date '{If(DateTimePicker1.Checked, DateTimePicker1.Value.ToShortDateString(), "N/A")}' in {currentTable}")
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        ' Trigger search when date changes
        TextBox1_TextChanged(Nothing, Nothing)
    End Sub



    ' =========================
    ' OPEN LOGS (FORM8)
    ' =========================
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.Hide()
        If String.IsNullOrEmpty(loggedInStaffUsername) Then
            MsgBox("Staff identity not loaded.")
            Exit Sub
        End If

        AddLog("Open Logs", "Opened logs viewer (Form8)")
        Dim logsForm As New Form8(loggedInStaffUsername)
        logsForm.ShowDialog()
    End Sub

End Class
