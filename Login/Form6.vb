Imports System.Data
Imports System.Data.OleDb
Imports System.Reflection.Emit
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class form6

    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    Private dt As DataTable
    Private currentTable As String = "request"

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub StaffDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTable("request") ' Default to requests
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
                    sql = "SELECT EID, username, FullName, MobileN, Email, Address 
                           FROM [login] ORDER BY EID"
                    Label2.Text = "Viewing: Employees"

                Case "request"
                    sql = "SELECT EID, FullName, Category, [Request], Status, RequestDate 
                           FROM [request] ORDER BY RequestDate DESC"
                    Label2.Text = "Viewing: Leave Requests"

                Case "attendance"
                    sql = "SELECT Username, FullName, LoginDate, LoginTime, Status 
                           FROM [attendance] ORDER BY LoginDate DESC"
                    Label2.Text = "Viewing: Attendance"
            End Select

            Dim da As New OleDbDataAdapter(sql, connect)
            da.Fill(dt)

            DataGridView1.DataSource = dt
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.ReadOnly = True  ' View-only by default

            ' Only requests are editable via approve/reject buttons
            If tableName = "request" Then
                DataGridView1.ReadOnly = False
                DataGridView1.Columns("EID").ReadOnly = True ' Lock primary key
            End If

            currentTable = tableName

        Catch ex As Exception
            MsgBox("Load error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' TABLE SWITCHING
    ' =========================
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadTable("login") ' View-only
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        LoadTable("request") ' Can approve/reject
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        LoadTable("attendance") ' View-only
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

    Private Sub UpdateRequestStatus(newStatus As String)
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select a request to update")
            Exit Sub
        End If

        Dim row = DataGridView1.SelectedRows(0)

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim cmd As New OleDbCommand(
            "UPDATE [request] SET Status=? WHERE EID=?", connect)

            cmd.Parameters.AddWithValue("?", newStatus)
            cmd.Parameters.AddWithValue("?", row.Cells("EID").Value)
            cmd.ExecuteNonQuery()

            MsgBox("Request " & newStatus.ToLower() & " successfully")
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
    End Sub

    ' =========================
    ' LOGOUT
    ' =========================
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            Dim f As New Form3
            f.Show()
            Close()
        End If
    End Sub

    ' =========================
    ' EXPORT CURRENT TABLE TO PDF
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("No data to export")
            Exit Sub
        End If

        Try
            ' Choose file location
            Dim saveDlg As New SaveFileDialog
            saveDlg.Filter = "PDF files (*.pdf)|*.pdf"
            saveDlg.FileName = currentTable & "_Export.pdf"
            If saveDlg.ShowDialog() <> DialogResult.OK Then Exit Sub

            ' Create PDF document
            Dim doc As New Document(PageSize.A4, 20, 20, 20, 20)
            PdfWriter.GetInstance(doc, New FileStream(saveDlg.FileName, FileMode.Create))
            doc.Open()

            ' Add title
            doc.Add(New Paragraph("Export of " & currentTable & " table"))
            doc.Add(New Paragraph("Generated on: " & DateTime.Now.ToString()))
            doc.Add(New Paragraph(" "))

            ' Create PDF table
            Dim pdfTable As New PdfPTable(DataGridView1.Columns.Count)
            pdfTable.WidthPercentage = 100

            ' Add headers
            For Each col As DataGridViewColumn In DataGridView1.Columns
                pdfTable.AddCell(New Phrase(col.HeaderText))
            Next

            ' Add rows
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    For Each cell As DataGridViewCell In row.Cells
                        pdfTable.AddCell(If(cell.Value IsNot Nothing, cell.Value.ToString(), ""))
                    Next
                End If
            Next

            doc.Add(pdfTable)
            doc.Close()

            MsgBox("PDF exported successfully to " & saveDlg.FileName)

        Catch ex As Exception
            MsgBox("Error exporting PDF: " & ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If dt Is Nothing Then Exit Sub  ' No table loaded

        Dim filter As String = TextBox1.Text.Trim().Replace("'", "''") ' sanitize quotes

        If filter = "" Then
            DataGridView1.DataSource = dt
            Exit Sub
        End If

        Dim dv As New DataView(dt)

        Select Case currentTable
            Case "login"
                dv.RowFilter = String.Format("Convert(EID, 'System.String') LIKE '%{0}%' OR " &
                                             "username LIKE '%{0}%' OR " &
                                             "FullName LIKE '%{0}%' OR " &
                                             "MobileN LIKE '%{0}%' OR " &
                                             "Email LIKE '%{0}%' OR " &
                                             "Address LIKE '%{0}%'", filter)
            Case "request"
                dv.RowFilter = String.Format("Convert(EID, 'System.String') LIKE '%{0}%' OR " &
                                             "FullName LIKE '%{0}%' OR " &
                                             "Category LIKE '%{0}%' OR " &
                                             "[Request] LIKE '%{0}%' OR " &
                                             "Status LIKE '%{0}%' OR " &
                                             "Convert(RequestDate, 'System.String') LIKE '%{0}%'", filter)
            Case "attendance"
                dv.RowFilter = String.Format("Username LIKE '%{0}%' OR " &
                                             "FullName LIKE '%{0}%' OR " &
                                             "Convert(LoginDate, 'System.String') LIKE '%{0}%' OR " &
                                             "Convert(LoginTime, 'System.String') LIKE '%{0}%' OR " &
                                             "Status LIKE '%{0}%'", filter)
        End Select

        DataGridView1.DataSource = dv
    End Sub

End Class
