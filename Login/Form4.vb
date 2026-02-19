Imports System.Data
Imports System.Data.OleDb
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Form4

    Private ReadOnly connect As New OleDbConnection(
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
    ' LOGGING
    ' =========================
    Private Sub AddLog(action As String, details As String)
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Using cmd As New OleDbCommand(
                "INSERT INTO logs (Username, ActionDate, [Action], Details) VALUES (?,?,?,?)", connect)
                cmd.Parameters.AddWithValue("?", loggedInAdmin)
                cmd.Parameters.AddWithValue("?", DateTime.Now)
                cmd.Parameters.AddWithValue("?", action)
                cmd.Parameters.AddWithValue("?", details)
                cmd.ExecuteNonQuery()
            End Using
        Catch
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' LOAD TABLES
    ' =========================
    Private Sub LoadTable(tableName As String)
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            dt = New DataTable()

            Dim sql As String = ""

            Select Case tableName
                Case "login"
                    sql = "SELECT EID, Username, [Password], Role, FullName, MobileN, Email, Address, Status FROM login"

                Case "request"
                    sql = "SELECT UID, EID, FullName, Category, [Request], Status, RequestDate FROM request"

                Case "attendance"
                    sql = "SELECT Username, FullName, LoginDate, LoginTime, Status FROM attendance"

                Case "logs"
                    sql = "SELECT Username, [Action], ActionDate, Details FROM logs ORDER BY ActionDate DESC"
            End Select

            Using da As New OleDbDataAdapter(sql, connect)
                da.Fill(dt)
            End Using

            DataGridView1.DataSource = dt
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.ReadOnly = False

            If dt.Columns.Count > 0 Then
                DataGridView1.Columns(0).ReadOnly = True
            End If

            currentTable = tableName

        Catch ex As Exception
            MsgBox("Load error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' SWITCH TABLES
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
    ' ADD EMPLOYEE
    ' =========================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If currentTable <> "login" Then Exit Sub

        Dim username As String = InputBox("Enter username:")
        If String.IsNullOrWhiteSpace(username) Then Exit Sub

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Using cmd As New OleDbCommand(
                "INSERT INTO login (Username, Status) VALUES (?,?)", connect)
                cmd.Parameters.AddWithValue("?", username)
                cmd.Parameters.AddWithValue("?", "Active")
                cmd.ExecuteNonQuery()
            End Using

            AddLog("ADD_EMPLOYEE", username)
            LoadTable("login")

        Catch ex As Exception
            MsgBox("Add error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' UPDATE RECORD
    ' =========================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count = 0 Then Exit Sub
        Dim r = DataGridView1.SelectedRows(0)

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Using cmd As New OleDbCommand("", connect)

                Select Case currentTable

                    Case "login"
                        cmd.CommandText =
                            "UPDATE login SET Username=?, [Password]=?, Role=?, FullName=?, MobileN=?, Email=?, Address=?, Status=? WHERE EID=?"

                        cmd.Parameters.AddWithValue("?", r.Cells("Username").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("Password").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("Role").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("FullName").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("MobileN").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("Email").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("Address").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("Status").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("EID").Value)

                    Case "request"
                        cmd.CommandText = "UPDATE request SET Status=? WHERE UID=?"
                        cmd.Parameters.AddWithValue("?", r.Cells("Status").Value)
                        cmd.Parameters.AddWithValue("?", r.Cells("UID").Value)

                    Case "attendance"
                        cmd.CommandText = "UPDATE attendance SET Status=? WHERE Username=? AND LoginDate=?"
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
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' DELETE RECORD
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.SelectedRows.Count = 0 Then Exit Sub
        If MsgBox("Delete selected record?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub

        Dim r = DataGridView1.SelectedRows(0)

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Using cmd As New OleDbCommand("", connect)

                Select Case currentTable
                    Case "login"
                        cmd.CommandText = "DELETE FROM login WHERE EID=?"
                        cmd.Parameters.AddWithValue("?", r.Cells("EID").Value)

                    Case "request"
                        cmd.CommandText = "DELETE FROM request WHERE UID=?"
                        cmd.Parameters.AddWithValue("?", r.Cells("UID").Value)

                    Case "attendance"
                        cmd.CommandText = "DELETE FROM attendance WHERE Username=? AND LoginDate=?"
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
    ' EXPORT PDF
    ' =========================
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If DataGridView1.Rows.Count = 0 Then Exit Sub

        Using sfd As New SaveFileDialog With {
            .Filter = "PDF (*.pdf)|*.pdf",
            .FileName = currentTable & "_" & Now.ToString("yyyyMMdd_HHmmss") & ".pdf"
        }

            If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim doc As New Document(PageSize.A4.Rotate())
            PdfWriter.GetInstance(doc, New FileStream(sfd.FileName, FileMode.Create))
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
    ' LOGOUT
    ' =========================
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        AddLog("LOGOUT", loggedInAdmin)
        Dim f As New Form3
        f.Show()
        Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim f As New Form8(loggedInAdmin)
        f.Show()
        Me.Hide()
    End Sub
End Class
