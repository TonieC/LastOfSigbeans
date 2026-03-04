Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing.Printing
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf


Public Class Form8

    Private staffUsername As String
    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")
    Private dt As DataTable

    ' SINGLE CONSTRUCTOR WITH OPTIONAL USERNAME
    Public Sub New(Optional username As String = "")
        InitializeComponent()
        staffUsername = username
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If String.IsNullOrEmpty(staffUsername) Then
            MsgBox("Staff identity not loaded.")
            Exit Sub
        End If

        Label1.Text = "Logs for: " & staffUsername
        LoadLogs()
    End Sub

    ' =========================
    ' LOAD LOGS
    ' =========================
    Private Sub LoadLogs()
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            ' Show all logs for this staff including username, action, details, and date
            Dim sql As String = "SELECT Username, ActionDate, Action, Details FROM [logs] WHERE Username=? ORDER BY ActionDate DESC"
            Dim da As New OleDbDataAdapter(sql, connect)
            da.SelectCommand.Parameters.AddWithValue("?", staffUsername)

            dt = New DataTable()
            da.Fill(dt)

            DataGridView1.DataSource = dt
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.ReadOnly = True

        Catch ex As Exception
            MsgBox("Error loading logs: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' ADD LOG ENTRY
    ' =========================
    Public Sub AddLog(action As String, details As String)
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim cmd As New OleDbCommand("INSERT INTO [logs] (Username, ActionDate, Action, Details) VALUES (?, ?, ?, ?)", connect)
            cmd.Parameters.AddWithValue("?", staffUsername)
            cmd.Parameters.AddWithValue("?", DateTime.Now)
            cmd.Parameters.AddWithValue("?", action)
            cmd.Parameters.AddWithValue("?", details)
            cmd.ExecuteNonQuery()

            ' Refresh grid after adding
            LoadLogs()

        Catch ex As Exception
            MsgBox("Error adding log: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' SEARCH LOGS
    ' =========================
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If dt Is Nothing Then Exit Sub

        Dim filter As String = TextBox1.Text.Trim().Replace("'", "''")
        If filter = "" Then
            DataGridView1.DataSource = dt
            Exit Sub
        End If

        Dim dv As New DataView(dt)
        dv.RowFilter = String.Format("Convert(ActionDate,'System.String') LIKE '%{0}%' OR " &
                                     "Username LIKE '%{0}%' OR " &
                                     "Action LIKE '%{0}%' OR " &
                                     "Details LIKE '%{0}%'", filter)
        DataGridView1.DataSource = dv
    End Sub

    ' =========================
    ' BACK BUTTON TO STAFF DASHBOARD
    ' =========================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Open staff dashboard and pass the username
        Dim f As New Form6(staffUsername)
        f.Show()
        Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("No logs to export")
            Exit Sub
        End If

        Try
            ' Ask user where to save
            Dim saveDlg As New SaveFileDialog With {
            .Filter = "PDF files (*.pdf)|*.pdf",
            .FileName = "Logs_" & staffUsername & ".pdf"
        }
            If saveDlg.ShowDialog() <> DialogResult.OK Then Exit Sub

            ' Create PDF document
            Dim doc As New Document(PageSize.A4, 20, 20, 20, 20)
            PdfWriter.GetInstance(doc, New FileStream(saveDlg.FileName, FileMode.Create))
            doc.Open()

            ' Add title and metadata
            doc.Add(New Paragraph("Logs for: " & staffUsername))
            doc.Add(New Paragraph("Generated on: " & DateTime.Now))
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
                        pdfTable.AddCell(If(cell.Value, "").ToString())
                    Next
                End If
            Next

            doc.Add(pdfTable)
            doc.Close()

            ' Log PDF export
            AddLog("Export PDF", "Exported logs to PDF")

            MsgBox("Logs exported successfully to PDF!")

        Catch ex As Exception
            MsgBox("Error exporting logs: " & ex.Message)
        End Try
    End Sub
End Class
