Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel

Public Class RedSkyConfiguration
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
    Dim conn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim reader As SqlDataReader
    Dim da As SqlDataAdapter
    Dim ds As New DataSet

    Dim initialConfig As Boolean
    Dim mode As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetReportConfiguration()
        GetMailingConfiguration()
        GetMailingList()
        lblMailingListId.Text = gvMailingList.Item(0, 0).Value
        txtMailingListEmailAddress.Text = gvMailingList.Item(1, 0).Value
        cboMailingListStatus.SelectedItem = gvMailingList.Item(2, 0).Value
        CheckReports(gvMailingList.Item(3, 0).Value)
        'GetSelectedRowGridView()
    End Sub

    Private Sub GetReportConfiguration()
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM ReportConfiguration"
            'cmd.ExecuteNonQuery()
            reader = cmd.ExecuteReader
            Dim reportType As String
            If reader.HasRows Then
                initialConfig = False
                Do While reader.Read
                    reportType = reader.GetValue(1).ToString

                    If reportType = "Daily" Then
                        dtDaily.Value = reader.GetDateTime(2)
                        lblDailyLastReport.Text = reader.GetValue(3).ToString
                        lblDailyNextReport.Text = reader.GetValue(4).ToString
                        cboDailyStatus.SelectedItem = reader.GetValue(5).ToString
                        dtDailyFrom.Value = reader.GetDateTime(6)
                        dtDailyTo.Value = reader.GetDateTime(7)
                    End If

                    If reportType = "Weekly" Then
                        dtWeekly.Value = reader.GetDateTime(2)
                        lblWeeklyLastReport.Text = reader.GetValue(3).ToString
                        lblWeeklyNextReport.Text = reader.GetValue(4).ToString
                        cboWeeklyStatus.SelectedItem = reader.GetValue(5).ToString
                        cboWeeklyDay.SelectedItem = reader.GetValue(8).ToString
                    End If

                    If reportType = "Monthly" Then
                        dtMonthly.Value = reader.GetDateTime(2)
                        lblMonthlyLastReport.Text = reader.GetValue(3).ToString
                        lblMonthyNextReport.Text = reader.GetValue(4).ToString
                        cboMonthlyStatus.SelectedItem = reader.GetValue(5).ToString
                    End If
                Loop
            Else
                initialConfig = True
                cboDailyStatus.SelectedItem = "ENABLED"
                cboWeeklyStatus.SelectedItem = "ENABLED"
                cboMonthlyStatus.SelectedItem = "ENABLED"
            End If
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            MsgBox("Error fetching configuration. " & ex.Message, MsgBoxStyle.Exclamation, "Report Configuration")
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub btnSaveReportConfiguration_Click(sender As Object, e As EventArgs) Handles btnSaveReportConfiguration.Click
        Dim reportType As String = ""
        Dim generationDateTime As DateTime
        Dim status As String = ""
        Dim dailyFrom As DateTime
        Dim dailyTo As DateTime
        Dim weeklyDay As String = ""

        Try
            For i = 0 To 2 Step 1
                'MsgBox(i.ToString & initialConfig.ToString)
                If i = 0 Then
                    reportType = "Daily"
                    generationDateTime = dtDaily.Value
                    status = cboDailyStatus.SelectedItem.ToString
                    dailyFrom = dtDailyFrom.Value
                    dailyTo = dtDailyTo.Value
                    'weeklyDay = Nothing
                ElseIf i = 1 Then
                    reportType = "Weekly"
                    generationDateTime = dtWeekly.Value
                    status = cboWeeklyStatus.SelectedItem.ToString
                    'dailyFrom = Nothing
                    'dailyTo = Nothing
                    weeklyDay = cboWeeklyDay.SelectedItem.ToString
                ElseIf i = 2 Then
                    reportType = "Monthly"
                    generationDateTime = dtMonthly.Value
                    status = cboMonthlyStatus.SelectedItem.ToString
                    'dailyFrom = Nothing
                    'dailyTo = Nothing
                    'weeklyDay = Nothing
                End If

                If Not reportType = "" Then
                    If initialConfig = True Then
                        NewReportConfiguration(reportType, generationDateTime, status, dailyFrom, dailyTo, weeklyDay)
                    Else
                        UpdateReportConfiguration(reportType, generationDateTime, status, dailyFrom, dailyTo, weeklyDay)
                    End If
                End If
            Next
            MsgBox("Successfully updated configuration. Please reload configuration. ", MsgBoxStyle.Information, "Update Report Configuration")
        Catch ex As Exception
            MsgBox("Error updating configuration. " & ex.Message, MsgBoxStyle.Exclamation, "Update Report Configuration")
        End Try


    End Sub

    Private Sub NewReportConfiguration(reportType As String, generationDateTime As DateTime, status As String, dailyFrom As DateTime, dailyTo As DateTime, weeklyDay As String)
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO ReportConfiguration(ReportType, GenerationDateTime, Status, DailyFrom, DailyTo, WeeklyDay) VALUES (@ReportType, @GenerationDateTime, @Status)"
            cmd.Parameters.AddWithValue("@ReportType", reportType)
            cmd.Parameters.AddWithValue("@GenerationDateTime", generationDateTime)
            cmd.Parameters.AddWithValue("@Status", status)
            cmd.Parameters.AddWithValue("@DailyFrom", dailyFrom)
            cmd.Parameters.AddWithValue("@DailyTo", dailyTo)
            cmd.Parameters.AddWithValue("@WeeklyDay", weeklyDay)
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            MsgBox("Error inserting record to table. " & ex.Message, MsgBoxStyle.Exclamation, "Database Error")
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub UpdateReportConfiguration(reportType As String, generationDateTime As DateTime, status As String, dailyFrom As DateTime, dailyTo As DateTime, weeklyDay As String)
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "UPDATE ReportConfiguration SET GenerationDateTime = @GenerationDateTime, Status = @Status, DailyFrom = @DailyFrom, DailyTo = @DailyTo, WeeklyDay = @WeeklyDay WHERE ReportType = @ReportType"
            cmd.Parameters.AddWithValue("@GenerationDateTime", generationDateTime)
            cmd.Parameters.AddWithValue("@Status", status)
            cmd.Parameters.AddWithValue("@ReportType", reportType)
            cmd.Parameters.AddWithValue("@DailyFrom", dailyFrom)
            cmd.Parameters.AddWithValue("@DailyTo", dailyTo)
            cmd.Parameters.AddWithValue("@WeeklyDay", weeklyDay)
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            MsgBox("Error updating record to table. " & ex.Message, MsgBoxStyle.Exclamation, "Database Error")
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub GetMailingConfiguration()
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM MailingConfiguration"
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                initialConfig = False
                Do While reader.Read
                    lblSMTPId.Text = reader.GetValue(0).ToString
                    txtSMTPServer.Text = reader.GetValue(1).ToString
                    'txtSMTPPortTLS.Text = reader.GetValue(2).ToString
                    txtSMTPPortSSL.Text = reader.GetValue(2).ToString
                    cboSMTPUseTLSSSL.SelectedItem = reader.GetValue(3).ToString
                    txtSMTPUsername.Text = reader.GetValue(4).ToString
                    txtSMTPPassword.Text = reader.GetValue(5).ToString
                Loop
            Else
                initialConfig = True
                lblSMTPId.Text = ""
                txtSMTPServer.Text = ""
                'txtSMTPPortTLS.Text = ""
                txtSMTPPortSSL.Text = ""
                cboSMTPUseTLSSSL.SelectedItem = "YES"
                txtSMTPUsername.Text = ""
                txtSMTPPassword.Text = ""
            End If
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            MsgBox("Error fetching configuration. " & ex.Message, MsgBoxStyle.Exclamation, "Report Configuration")
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub btnSaveMailingConfiguration_Click(sender As Object, e As EventArgs) Handles btnSaveMailingConfiguration.Click
        If txtSMTPServer.Text = "" Or txtSMTPPortSSL.Text = "" Or cboSMTPUseTLSSSL.SelectedItem = "" Or txtSMTPUsername.Text = "" Or txtSMTPPassword.Text = "" Then
            MsgBox("One or more field(s) is empty. All fields are required.", MsgBoxStyle.Exclamation, "Required Fields")
        Else
            Try
                If initialConfig = True Then
                    NewMailingConfiguration()
                Else
                    UpdateMailingConfiguration()
                End If
                MsgBox("Successfully updated configuration.", MsgBoxStyle.Information, "Update Report Configuration")
            Catch ex As Exception
                MsgBox("Error updating configuration. " & ex.Message, MsgBoxStyle.Exclamation, "Update Report Configuration")
            End Try
        End If
    End Sub

    Private Sub NewMailingConfiguration()
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO MailingConfiguration(SMTPServer, SMTPPortSSL, SMTPUseTLSSSL, SMTPUsername, SMTPPassword) VALUES (@SMTPServer, @SMTPPortSSL, @SMTPUseTLSSSL, @SMTPUsername, @SMTPPassword)"
            cmd.Parameters.AddWithValue("@SMTPServer", txtSMTPServer.Text)
            cmd.Parameters.AddWithValue("@SMTPPortSSL", txtSMTPPortSSL.Text)
            cmd.Parameters.AddWithValue("@SMTPUseTLSSSL", cboSMTPUseTLSSSL.SelectedItem.ToString)
            cmd.Parameters.AddWithValue("@SMTPUsername", txtSMTPUsername.Text)
            cmd.Parameters.AddWithValue("@SMTPPassword", txtSMTPPassword.Text)
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            MsgBox("Error inserting new record to table. " & ex.Message, MsgBoxStyle.Exclamation, "Database Error")
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub UpdateMailingConfiguration()
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "UPDATE MailingConfiguration SET SMTPServer = @SMTPServer, SMTPPortSSL = @SMTPPortSSL, SMTPUseTLSSSL = @SMTPUseTLSSSL, SMTPUsername = @SMTPUsername, SMTPPassword = @SMTPPassword WHERE Id = @Id"
            cmd.Parameters.AddWithValue("@SMTPServer", txtSMTPServer.Text)
            cmd.Parameters.AddWithValue("@SMTPPortSSL", txtSMTPPortSSL.Text)
            cmd.Parameters.AddWithValue("@SMTPUseTLSSSL", cboSMTPUseTLSSSL.SelectedItem.ToString)
            cmd.Parameters.AddWithValue("@SMTPUsername", txtSMTPUsername.Text)
            cmd.Parameters.AddWithValue("@SMTPPassword", txtSMTPPassword.Text)
            cmd.Parameters.AddWithValue("@Id", lblSMTPId.Text)
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            MsgBox("Error updating record to table. " & ex.Message, MsgBoxStyle.Exclamation, "Database Error")
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub GetMailingList()
        Try
            ds.Reset()
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM MailingList"
            da = New SqlDataAdapter("SELECT * FROM MailingList", conn)
            da.Fill(ds, "MailingList")
            conn.Close()
            conn.Dispose()
            gvMailingList.DataSource = ds.Tables("MailingList")
        Catch ex As Exception
            MsgBox("Error fetching record from table. " & ex.Message, MsgBoxStyle.Exclamation, "Database Error")
        End Try
    End Sub

    Private Sub btnMailingListAddNew_Click(sender As Object, e As EventArgs) Handles btnMailingListAddNew.Click
        mode = "New"
        lblMailingListId.Text = "ID"
        txtMailingListEmailAddress.Text = ""
        cboMailingListStatus.SelectedItem = "ACTIVE"
        chkDaily.Checked = False
        chkWeekly.Checked = False
        chkMonthly.Checked = False
        txtMailingListEmailAddress.Enabled = True
        cboMailingListStatus.Enabled = True
        chkDaily.Enabled = True
        chkWeekly.Enabled = True
        chkMonthly.Enabled = True
        btnMailingListSave.Enabled = True
        btnMailingListCancel.Enabled = True
        btnMailingListAddNew.Enabled = False
        btnMailingListEdit.Enabled = False
    End Sub

    Private Sub btnMailingListEdit_Click(sender As Object, e As EventArgs) Handles btnMailingListEdit.Click
        mode = "Edit"
        txtMailingListEmailAddress.Enabled = True
        cboMailingListStatus.Enabled = True
        chkDaily.Enabled = True
        chkWeekly.Enabled = True
        chkMonthly.Enabled = True
        btnMailingListSave.Enabled = True
        btnMailingListCancel.Enabled = True
        btnMailingListAddNew.Enabled = False
        btnMailingListEdit.Enabled = False
    End Sub

    Private Sub btnMailingListSave_Click(sender As Object, e As EventArgs) Handles btnMailingListSave.Click
        txtMailingListEmailAddress.Enabled = False
        cboMailingListStatus.Enabled = False
        chkDaily.Enabled = False
        chkWeekly.Enabled = False
        chkMonthly.Enabled = False
        btnMailingListSave.Enabled = False
        btnMailingListCancel.Enabled = False
        btnMailingListAddNew.Enabled = True
        btnMailingListEdit.Enabled = True

        Dim reports As String = ""

        If chkDaily.Checked = True Then
            reports = reports & chkDaily.Text & ", "
        End If

        If chkWeekly.Checked = True Then
            reports = reports & chkWeekly.Text & ", "
        End If

        If chkMonthly.Checked = True Then
            reports = reports & chkMonthly.Text & ", "
        End If


        If mode = "New" Then
            NewMailingList(reports)
        ElseIf mode = "Edit" Then
            UpdateMailingList(reports)
        End If

        GetMailingList()
        GetSelectedRowGridView()
    End Sub

    Private Sub btnMailingListCancel_Click(sender As Object, e As EventArgs) Handles btnMailingListCancel.Click
        If mode = "New" Then
            lblMailingListId.Text = "ID"
            txtMailingListEmailAddress.Text = ""
            cboMailingListStatus.SelectedItem = "ACTIVE"
            chkDaily.Checked = False
            chkWeekly.Checked = False
            chkMonthly.Checked = False
            txtMailingListEmailAddress.Enabled = False
            cboMailingListStatus.Enabled = False
            chkDaily.Enabled = False
            chkWeekly.Enabled = False
            chkMonthly.Enabled = False
            btnMailingListSave.Enabled = False
            btnMailingListCancel.Enabled = False
            btnMailingListAddNew.Enabled = True
            btnMailingListEdit.Enabled = True
        ElseIf mode = "Edit" Then
            txtMailingListEmailAddress.Enabled = False
            cboMailingListStatus.Enabled = False
            chkDaily.Enabled = False
            chkWeekly.Enabled = False
            chkMonthly.Enabled = False
            btnMailingListSave.Enabled = False
            btnMailingListCancel.Enabled = False
            btnMailingListAddNew.Enabled = True
            btnMailingListEdit.Enabled = True
        End If
        GetSelectedRowGridView()
    End Sub

    Private Sub NewMailingList(reports As String)
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO MailingList(EmailAddress, Status, Reports) VALUES (@EmailAddress, @Status, @Reports)"
            cmd.Parameters.AddWithValue("@EmailAddress", txtMailingListEmailAddress.Text)
            cmd.Parameters.AddWithValue("@Status", cboMailingListStatus.SelectedItem.ToString)
            cmd.Parameters.AddWithValue("@Reports", reports)
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            MsgBox("Error inserting new record to table. " & ex.Message, MsgBoxStyle.Exclamation, "Database Error")
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub UpdateMailingList(reports As String)
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "UPDATE MailingList SET EmailAddress = @EmailAddress, Status = @Status, Reports = @Reports WHERE Id = @Id"
            cmd.Parameters.AddWithValue("@EmailAddress", txtMailingListEmailAddress.Text)
            cmd.Parameters.AddWithValue("@Status", cboMailingListStatus.SelectedItem.ToString)
            cmd.Parameters.AddWithValue("@Reports", reports)
            cmd.Parameters.AddWithValue("@Id", lblMailingListId.Text)
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            MsgBox("Error updating record to table. " & ex.Message, MsgBoxStyle.Exclamation, "Database Error")
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub gvMailingList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles gvMailingList.CellClick
        Dim i As Integer
        i = gvMailingList.CurrentRow.Index
        lblMailingListId.Text = gvMailingList.Item(0, i).Value
        txtMailingListEmailAddress.Text = gvMailingList.Item(1, i).Value
        cboMailingListStatus.SelectedItem = gvMailingList.Item(2, i).Value
        CheckReports(gvMailingList.Item(3, i).Value)
    End Sub

    Private Sub GetSelectedRowGridView()
        lblMailingListId.Text = gvMailingList.Rows(gvMailingList.CurrentRow.Index).Cells(0).Value
        txtMailingListEmailAddress.Text = gvMailingList.Rows(gvMailingList.CurrentRow.Index).Cells(1).Value
        cboMailingListStatus.SelectedItem = gvMailingList.Rows(gvMailingList.CurrentRow.Index).Cells(2).Value
        CheckReports(gvMailingList.Rows(gvMailingList.CurrentRow.Index).Cells(3).Value)
    End Sub

    Private Sub CheckReports(reports As String)
        If reports.Contains("Daily") Then
            chkDaily.Checked = True
        Else
            chkDaily.Checked = False
        End If
        If reports.Contains("Weekly") Then
            chkWeekly.Checked = True
        Else
            chkWeekly.Checked = False
        End If
        If reports.Contains("Monthly") Then
            chkMonthly.Checked = True
        Else
            chkMonthly.Checked = False
        End If
    End Sub

End Class