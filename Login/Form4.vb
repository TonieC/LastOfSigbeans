Imports System.Data.OleDb
Imports System.Data

Public Class Form4

    Dim connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    Dim dt As New DataTable

    ' LOAD DATA ON FORM LOAD
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployees()
    End Sub

    ' LOAD EMPLOYEES INTO GRID
    Private Sub LoadEmployees()
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql As String =
                "SELECT EID, username, FullName, MobileN, Email, Address FROM [login] ORDER BY EID"

            Dim da As New OleDbDataAdapter(sql, connect)
            dt.Clear()
            da.Fill(dt)
            DataGridView1.DataSource = dt

            DataGridView1.ReadOnly = False
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            ' LOCK EID (DO NOT EDIT)
            DataGridView1.Columns("EID").ReadOnly = True

        Catch ex As Exception
            MsgBox("Load error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' EDIT / UPDATE EMPLOYEE
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select an employee to update")
            Exit Sub
        End If

        Dim eid = DataGridView1.SelectedRows(0).Cells("EID").Value.ToString

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql =
                "UPDATE [login] SET username=?, FullName=?, MobileN=?, Email=?, Address=? WHERE EID=?"

            Using cmd As New OleDbCommand(sql, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.SelectedRows(0).Cells("username").Value
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.SelectedRows(0).Cells("FullName").Value
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.SelectedRows(0).Cells("MobileN").Value
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.SelectedRows(0).Cells("Email").Value
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = DataGridView1.SelectedRows(0).Cells("Address").Value
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = eid

                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Employee updated successfully")
            LoadEmployees()

        Catch ex As Exception
            MsgBox("Update error: " & ex.Message)
        Finally
            connect.Close()
        End Try

    End Sub

    ' DELETE EMPLOYEE
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Select an employee to delete")
            Exit Sub
        End If

        Dim eid = DataGridView1.SelectedRows(0).Cells("EID").Value.ToString

        If MsgBox("Delete employee EID " & eid & "?", MsgBoxStyle.YesNo Or MsgBoxStyle.Critical) = MsgBoxResult.No Then
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql = "DELETE FROM [login] WHERE EID=?"

            Using cmd As New OleDbCommand(sql, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = eid
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Employee deleted")
            LoadEmployees()

        Catch ex As Exception
            MsgBox("Delete error: " & ex.Message)
        Finally
            connect.Close()
        End Try

    End Sub

    ' ADD NEW EMPLOYEE (ADMIN CONTROLLED)
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            ' Prompt admin for employee info
            Dim username As String = InputBox("Enter username:")
            If username = "" Then Exit Sub

            ' Check duplicate username
            Dim cmdCheck As New OleDbCommand("SELECT COUNT(*) FROM [login] WHERE username=?", connect)
            cmdCheck.Parameters.Add("?", OleDbType.VarChar).Value = username
            If CInt(cmdCheck.ExecuteScalar()) > 0 Then
                MsgBox("Username already exists.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            Dim password As String = InputBox("Enter password:")
            If password = "" Then Exit Sub

            Dim fullName As String = InputBox("Enter full name:")
            If fullName = "" Then Exit Sub

            Dim mobile As String = InputBox("Enter mobile number:")
            If mobile = "" Then Exit Sub

            Dim email As String = InputBox("Enter email:")
            If email = "" Then Exit Sub

            Dim address As String = InputBox("Enter address:")
            If address = "" Then Exit Sub

            ' Generate new EID
            Dim cmdMax As New OleDbCommand("SELECT MAX(EID) FROM [login]", connect)
            Dim maxEID As Object = cmdMax.ExecuteScalar()
            Dim newEID As String = "001"
            If maxEID IsNot DBNull.Value Then
                newEID = (CInt(maxEID) + 1).ToString("D3")
            End If

            ' Insert employee into DB
            Dim cmdInsert As New OleDbCommand(
                "INSERT INTO [login] (EID, username, [password], FullName, MobileN, Email, Address) VALUES (?,?,?,?,?,?,?)",
                connect)

            cmdInsert.Parameters.Add("?", OleDbType.VarChar).Value = newEID
            cmdInsert.Parameters.Add("?", OleDbType.VarChar).Value = username
            cmdInsert.Parameters.Add("?", OleDbType.VarChar).Value = password
            cmdInsert.Parameters.Add("?", OleDbType.VarChar).Value = fullName
            cmdInsert.Parameters.Add("?", OleDbType.VarChar).Value = mobile
            cmdInsert.Parameters.Add("?", OleDbType.VarChar).Value = email
            cmdInsert.Parameters.Add("?", OleDbType.VarChar).Value = address

            cmdInsert.ExecuteNonQuery()
            MsgBox("Employee added successfully.", MsgBoxStyle.Information)

            ' Refresh grid
            LoadEmployees()

        Catch ex As Exception
            MsgBox("Error adding employee: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' REFRESH EMPLOYEE GRID
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadEmployees()
    End Sub

End Class
