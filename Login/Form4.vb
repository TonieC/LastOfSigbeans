Imports System.Data
Imports System.Data.OleDb

Public Class Form4

    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    Private dt As DataTable
    Private currentTable As String = "login"

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTable("login")
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
                    sql = "SELECT EID, username, password, FullName, MobileN, Email, Address 
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
            DataGridView1.ReadOnly = False

            ' Lock primary keys
            Select Case tableName
                Case "login", "request"
                    DataGridView1.Columns(0).ReadOnly = True
            End Select

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
        LoadTable("login")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        LoadTable("request")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        LoadTable("attendance")
    End Sub

    ' =========================
    ' UPDATE RECORD
    ' =========================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select a row to update")
            Exit Sub
        End If

        Dim row = DataGridView1.SelectedRows(0)

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Dim cmd As New OleDbCommand("", connect)

            Select Case currentTable

                Case "login"
                    cmd.CommandText =
                        "UPDATE [login] 
                         SET username=?, password=?, FullName=?, MobileN=?, Email=?, Address=?
                         WHERE EID=?"

                    cmd.Parameters.AddWithValue("?", row.Cells("username").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("password").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("FullName").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("MobileN").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("Email").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("Address").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("EID").Value)

                Case "request"
                    cmd.CommandText =
                        "UPDATE [request] 
                         SET FullName=?, Category=?, [Request]=?, Status=?, RequestDate=?
                         WHERE EID=?"

                    cmd.Parameters.AddWithValue("?", row.Cells("FullName").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("Category").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("Request").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("Status").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("RequestDate").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("EID").Value)

                Case "attendance"
                    cmd.CommandText =
                        "UPDATE [attendance] 
                         SET FullName=?, LoginDate=?, LoginTime=?, Status=?
                         WHERE Username=?"

                    cmd.Parameters.AddWithValue("?", row.Cells("FullName").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("LoginDate").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("LoginTime").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("Status").Value)
                    cmd.Parameters.AddWithValue("?", row.Cells("Username").Value)
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
            Dim cmd As New OleDbCommand("", connect)

            Select Case currentTable
                Case "login"
                    cmd.CommandText = "DELETE FROM [login] WHERE EID=?"
                    cmd.Parameters.AddWithValue("?", DataGridView1.SelectedRows(0).Cells("EID").Value)

                Case "request"
                    cmd.CommandText = "DELETE FROM [request] WHERE EID=?"
                    cmd.Parameters.AddWithValue("?", DataGridView1.SelectedRows(0).Cells("EID").Value)

                Case "attendance"
                    cmd.CommandText = "DELETE FROM [attendance] WHERE Username=?"
                    cmd.Parameters.AddWithValue("?", DataGridView1.SelectedRows(0).Cells("Username").Value)
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

End Class
