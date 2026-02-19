Imports System.Data
Imports System.Data.OleDb
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Form6

    ' =========================
    ' CONNECTION STRING
    ' =========================
    Private ReadOnly connString As String =
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb"

    Private dt As DataTable
    Private currentTable As String = "request"
    Private loggedInStaffUsername As String

    ' =========================
    ' CONSTRUCTOR (USERNAME REQUIRED)
    ' =========================
    Public Sub New(username As String)
        InitializeComponent()

        If String.IsNullOrWhiteSpace(username) Then
            Throw New Exception("Staff username is REQUIRED.")
        End If

        loggedInStaffUsername = username
    End Sub

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTable("request")
        AddLog("FORM_LOAD", "Staff dashboard loaded")
    End Sub

    ' =========================
    ' LOGGING (SEPARATE CONNECTION)
    ' =========================
    Private Sub AddLog(action As String, details As String)

        Using logConn As New OleDbConnection(connString)
            Try
                logConn.Open()

                Using cmd As New OleDbCommand(
                    "INSERT INTO logs ([Username], ActionDate, [Action], Details) VALUES (?,?,?,?)", logConn)

                    cmd.Parameters.Add("?", OleDbType.VarWChar).Value = loggedInStaffUsername
                    cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now
                    cmd.Parameters.Add("?", OleDbType.VarWChar).Value = action
                    cmd.Parameters.Add("?", OleDbType.VarWChar).Value = details

                    cmd.ExecuteNonQuery()
                End Using

            Catch ex As Exception
                MsgBox("LOGGING FAILED: " & ex.Message)
            End Try
        End Using

    End Sub

    ' =========================
    ' LOAD TABLES
    ' =========================
    Private Sub LoadTable(tableName As String)

        Using conn As New OleDbConnection(connString)
            Try
                conn.Open()

                dt = New DataTable()
                Dim sql As String = ""

                Select Case tableName
                    Case "login"
                        sql = "SELECT EID, Username, FullName, MobileN, Email, Address, Gender, Age, Department, Status FROM login"
                        Label2.Text = "Viewing: Employees"

                    Case "request"
                        sql = "SELECT UID, EID, FullName, Category, [Request], Status, RequestDate FROM request"
                        Label2.Text = "Viewing: Leave Requests"

                    Case "attendance"
                        sql = "SELECT Username, FullName, LoginDate, LoginTime, Status FROM attendance"
                        Label2.Text = "Viewing: Attendance"
                End Select

                Using da As New OleDbDataAdapter(sql, conn)
                    da.Fill(dt)
                End Using

                DataGridView1.DataSource = dt
                DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                DataGridView1.MultiSelect = False
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                DataGridView1.ReadOnly = True

                If tableName = "request" Then
                    DataGridView1.ReadOnly = False
                    For Each c In {"UID", "EID", "FullName", "Category", "Request", "RequestDate"}
                        DataGridView1.Columns(c).ReadOnly = True
                    Next
                End If

                currentTable = tableName

            Catch ex As Exception
                MsgBox("Load error: " & ex.Message)
            End Try
        End Using

    End Sub

    ' =========================
    ' TABLE SWITCH
    ' =========================
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadTable("login")
        AddLog("VIEW", "Viewed employees")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        LoadTable("request")
        AddLog("VIEW", "Viewed requests")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        LoadTable("attendance")
        AddLog("VIEW", "Viewed attendance")
    End Sub

    ' =========================
    ' APPROVE / REJECT REQUEST
    ' =========================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        UpdateRequest("Approved")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UpdateRequest("Rejected")
    End Sub

    Private Sub UpdateRequest(newStatus As String)

        If currentTable <> "request" OrElse DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select a request first.")
            Exit Sub
        End If

        Dim row = DataGridView1.SelectedRows(0)
        Dim uid As Integer = CInt(row.Cells("UID").Value)
        Dim eid As Integer = CInt(row.Cells("EID").Value)

        Using conn As New OleDbConnection(connString)
            Try
                conn.Open()

                Using cmd As New OleDbCommand(
                    "UPDATE request SET Status=? WHERE UID=?", conn)
                    cmd.Parameters.Add("?", OleDbType.VarWChar).Value = newStatus
                    cmd.Parameters.Add("?", OleDbType.Integer).Value = uid
                    cmd.ExecuteNonQuery()
                End Using

                If newStatus = "Approved" Then
                    Using cmd As New OleDbCommand(
                        "UPDATE login SET Status='On Leave' WHERE EID=?", conn)
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = eid
                        cmd.ExecuteNonQuery()
                    End Using
                Else
                    Using cmd As New OleDbCommand(
                        "UPDATE login SET Status='Active' WHERE EID=?", conn)
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = eid
                        cmd.ExecuteNonQuery()
                    End Using
                End If

                AddLog("REQUEST_UPDATE", $"UID={uid}, Status={newStatus}")
                LoadTable("request")
                MsgBox("Request updated.")

            Catch ex As Exception
                MsgBox("Update error: " & ex.Message)
            End Try
        End Using

    End Sub

    ' =========================
    ' REFRESH
    ' =========================
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadTable(currentTable)
        AddLog("REFRESH", currentTable)
    End Sub

    ' =========================
    ' EXPORT PDF
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If DataGridView1.Rows.Count = 0 Then Exit Sub

        Using dlg As New SaveFileDialog With {
            .Filter = "PDF (*.pdf)|*.pdf",
            .FileName = currentTable & "_Export.pdf"
        }

            If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim doc As New Document(PageSize.A4.Rotate())
            PdfWriter.GetInstance(doc, New FileStream(dlg.FileName, FileMode.Create))
            doc.Open()

            Dim table As New PdfPTable(DataGridView1.Columns.Count)
            table.WidthPercentage = 100

            For Each col As DataGridViewColumn In DataGridView1.Columns
                table.AddCell(col.HeaderText)
            Next

            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.IsNewRow Then Continue For
                For Each cell As DataGridViewCell In row.Cells
                    table.AddCell(If(cell.Value, "").ToString())
                Next
            Next

            doc.Add(table)
            doc.Close()

            AddLog("EXPORT_PDF", currentTable)
            MsgBox("PDF exported.")

        End Using
    End Sub

    ' =========================
    ' SEARCH
    ' =========================
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If dt Is Nothing Then Exit Sub
        Dim dv As New DataView(dt)
        dv.RowFilter = $"FullName LIKE '%{TextBox1.Text.Replace("'", "''")}%'"
        DataGridView1.DataSource = dv
    End Sub

    ' =========================
    ' VIEW LOGS
    ' =========================
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        AddLog("VIEW_LOGS", "Opened logs")
        Dim f As New Form8(loggedInStaffUsername)
        f.ShowDialog()
    End Sub

    ' =========================
    ' LOGOUT
    ' =========================
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MsgBox("Logout?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            AddLog("LOGOUT", "Staff logged out")
            Dim f As New Form3
            f.Show()
            Close()
        End If
    End Sub

End Class
