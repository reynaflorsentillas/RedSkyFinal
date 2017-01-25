Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Net.Mail

Public Class RedSkyMain
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
    Dim conn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim reader As SqlDataReader

    Dim timer As System.Timers.Timer = New System.Timers.Timer()

    Dim SMTPServer As String
    Dim SMTPPort As String
    Dim SMTPUsername As String
    Dim SMTPPassword As String

    Dim mailingList As New Dictionary(Of String, String)

    'Dim dailyStatus As String
    Dim dailyGeneration As DateTime
    Dim dailyFrom As DateTime
    Dim dailyTo As DateTime

    'Dim weeklyStatus As String
    Dim weeklyGeneration As DateTime

    'Dim monthlyStatus As String
    Dim monthlyGeneration As DateTime

    Dim today As DateTime

    Private Sub RedSkyMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetReportConfiguration()
        GetMailingConfiguration()
        SetRunStatus("STOP")
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
                Do While reader.Read
                    reportType = reader.GetValue(1).ToString

                    If reportType = "Daily" Then
                        lblDailyStatus.Text = reader.GetValue(5).ToString
                        dailyGeneration = reader.GetDateTime(2)
                        'dailyStatus = reader.GetValue(5).ToString
                        dailyFrom = reader.GetValue(6)
                        dailyTo = reader.GetValue(7)
                    End If

                    If reportType = "Weekly" Then
                        weeklyGeneration = reader.GetDateTime(2)
                        'weeklyStatus = reader.GetValue(5).ToString
                        lblWeeklyStatus.Text = reader.GetValue(5).ToString
                    End If

                    If reportType = "Monthly" Then
                        monthlyGeneration = reader.GetDateTime(2)
                        ' monthlyStatus = reader.GetValue(5).ToString
                        lblMonthlyStatus.Text = reader.GetValue(5).ToString
                    End If
                Loop
            End If
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            MsgBox("Error fetching configuration. " & ex.Message, MsgBoxStyle.Exclamation, "Report Configuration")
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub SetRunStatus(runStatus As String)
        If runStatus = "RUN" Then
            If lblDailyStatus.Text = "ENABLED" Then
                lblDailyRunStatus.Text = "RUNNING"
                lblDailyRunStatus.ForeColor = Color.DarkGreen
            Else
                lblDailyRunStatus.Text = lblDailyStatus.Text
                lblDailyRunStatus.ForeColor = Color.DarkOrange
            End If

            If lblDailyStatus.Text = "ENABLED" Then
                lblWeeklyRunStatus.Text = "RUNNING"
                lblWeeklyRunStatus.ForeColor = Color.DarkGreen
            Else
                lblWeeklyRunStatus.Text = lblDailyStatus.Text
                lblWeeklyRunStatus.ForeColor = Color.DarkOrange
            End If

            If lblMonthlyStatus.Text = "ENABLED" Then
                lblMonthlyRunStatus.Text = "RUNNING"
                lblMonthlyRunStatus.ForeColor = Color.DarkGreen
            Else
                lblMonthlyRunStatus.Text = lblMonthlyStatus.Text
                lblMonthlyRunStatus.ForeColor = Color.DarkOrange
            End If
        Else
            lblDailyRunStatus.Text = "STOPPED"
            lblDailyRunStatus.ForeColor = Color.DarkRed
            lblWeeklyRunStatus.Text = "STOPPED"
            lblWeeklyRunStatus.ForeColor = Color.DarkRed
            lblMonthlyRunStatus.Text = "STOPPED"
            lblMonthlyRunStatus.ForeColor = Color.DarkRed
        End If
    End Sub

    Private Sub btnRunReports_Click(sender As Object, e As EventArgs) Handles btnRunReports.Click
        timer.Interval = 30000 ' 1 minute
        AddHandler timer.Elapsed, AddressOf Me.OnTimer
        timer.Start()
        SetRunStatus("RUN")
        btnRunReports.Enabled = False
        btnStopReports.Enabled = True
    End Sub

    Private Sub OnTimer(sender As Object, e As Timers.ElapsedEventArgs)
        'GenerateDaily()
        today = DateTime.UtcNow.AddHours(8)
        'MsgBox(today.ToShortTimeString)
        'MsgBox(dailyGeneration.ToShortTimeString)
        Dim newDateFrom As DateTime
        newDateFrom = Convert.ToDateTime(today.ToShortDateString & " " & dailyFrom.ToLongTimeString)

        Dim newDateTo As DateTime
        newDateTo = Convert.ToDateTime(today.ToShortDateString & " " & dailyTo.ToLongTimeString)

        If dailyFrom.ToString("tt") = "PM" And dailyTo.ToString("tt") = "AM" Then
            newDateFrom = newDateFrom.AddDays(-1)
        End If

        If today.ToShortTimeString = dailyGeneration.ToShortTimeString Then
            GenerateDaily(newDateFrom, newDateTo, today)
            'MsgBox("Generated!")
        Else
            MsgBox("Do not generate!")
        End If
    End Sub

    Private Sub GenerateDaily(dateFrom As DateTime, dateTo As DateTime, dateGenerated As DateTime)
        Dim generated As Boolean = False
        Dim filePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Dim fileName As String = ""
        Dim seats As Integer = 0

        If lblDailyStatus.Text = "ENABLED" Then
            Try
                conn.ConnectionString = connectionString
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "SELECT * FROM AgentLogin WHERE LoginTime BETWEEN @dateFrom AND @dateTo"
                cmd.Parameters.AddWithValue("@dateFrom", dateFrom)
                cmd.Parameters.AddWithValue("@dateTo", dateTo)
                reader = cmd.ExecuteReader
                If reader.HasRows Then
                    'MsgBox("Has Data!")
                    Try
                        Dim xlApp As New Excel.Application
                        Dim xlWorkbook As Excel.Workbook = xlApp.Workbooks.Add
                        Dim xlWorksheet As Excel.Worksheet = CType(xlWorkbook.Sheets("sheet1"), Excel.Worksheet)
                        xlWorksheet.Name = "Agent Logins"
                        Dim xlRange As Excel.Range
                        xlRange = xlWorksheet.Range("A1", "H1")
                        xlRange.ColumnWidth = 30
                        xlRange.Interior.Color = Color.LightBlue
                        xlRange.Font.Bold = True

                        xlWorksheet.Range("A1").Value = "ID"
                        xlWorksheet.Range("B1").Value = "USERNAME"
                        xlWorksheet.Range("C1").Value = "WORKSTATION"
                        xlWorksheet.Range("D1").Value = "LOGIN DATE"
                        xlWorksheet.Range("E1").Value = "LOGIN TIME"
                        xlWorksheet.Range("F1").Value = "LOGOUT DATE"
                        xlWorksheet.Range("G1").Value = "LOGOUT TIME"
                        xlWorksheet.Range("H1").Value = "LOGIN DURATION"

                        Dim rowCount As Integer = 2

                        Do While reader.Read
                            xlWorksheet.Cells(rowCount, 1) = reader.GetValue(0).ToString
                            xlWorksheet.Cells(rowCount, 2) = reader.GetValue(1).ToString
                            xlWorksheet.Cells(rowCount, 3) = reader.GetValue(2).ToString
                            xlWorksheet.Cells(rowCount, 4) = reader.GetDateTime(3).ToShortDateString
                            xlWorksheet.Cells(rowCount, 5) = reader.GetDateTime(4).ToString
                            xlWorksheet.Cells(rowCount, 6) = If(reader.IsDBNull(5), Nothing, reader.GetDateTime(5).ToShortDateString)
                            xlWorksheet.Cells(rowCount, 7) = reader.GetValue(6).ToString
                            xlWorksheet.Cells(rowCount, 8) = reader.GetValue(7).ToString
                            rowCount += 1
                            seats += 1
                        Loop

                        fileName = "DailyReport" & dateGenerated.ToString("ddmmyyyy") & ".xlsx"

                        xlWorksheet.SaveAs(filePath & "\" & fileName)

                        xlWorkbook.Close()
                        xlApp.Quit()

                        xlApp = Nothing
                        xlWorkbook = Nothing
                        xlWorksheet = Nothing

                        generated = True

                        MsgBox("Successfully generated report.", MsgBoxStyle.Information, "Daily Report")
                    Catch ex As Exception
                        MsgBox("Error generating report. " & ex.Message, MsgBoxStyle.Exclamation, "Daily Report")
                    End Try
                    'Else
                    'MsgBox("No Data! " & dateFrom & " " & dateTo)
                End If
                cmd.Parameters.Clear()
                conn.Close()
                conn.Dispose()
            Catch ex As Exception
                MsgBox("Error generating report. " & ex.Message, MsgBoxStyle.Exclamation, "Daily Report")
                conn.Close()
                conn.Dispose()
            End Try
        Else
            MsgBox("Daily generation is DISABLED. Please change configuration.", MsgBoxStyle.Exclamation, "Daily Report")
        End If

        If generated = True Then
            Dim subject As String
            subject = "RedSky Daily Report " & today.ToShortDateString
            Dim body As String
            body = "Hi, " + vbNewLine + vbNewLine +
                "Today, your number of seats utilized are: " + seats + vbNewLine +
                "Please see attched file for more details." + vbNewLine + vbNewLine +
                "Best Regards," + vbNewLine +
                "Capstone Solutions Inc."
            Dim attachment As String
            attachment = filePath & "\" & fileName
            AutomaticEmail(subject, SMTPUsername, "reynaflorsentillas.rs@gmail.com", body, attachment)
        End If
    End Sub

    Private Sub btnConfiguration_Click(sender As Object, e As EventArgs) Handles btnConfiguration.Click
        Dim frm As New RedSkyConfiguration
        frm.Show()
    End Sub

    Private Sub GetMailingConfiguration()
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM MailingConfiguration"
            'cmd.ExecuteNonQuery()
            reader = cmd.ExecuteReader
            Dim reportType As String
            If reader.HasRows Then
                Do While reader.Read
                    reportType = reader.GetValue(1).ToString
                    SMTPServer = reader.GetValue(1).ToString
                    SMTPPort = reader.GetValue(2).ToString
                    SMTPUsername = reader.GetValue(4).ToString
                    SMTPPassword = reader.GetValue(5).ToString
                Loop
            End If
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub GetMailingList()
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM MailingList"
            'cmd.ExecuteNonQuery()
            reader = cmd.ExecuteReader
            Dim status As String
            If reader.HasRows Then
                Do While reader.Read
                    status = reader.GetValue(2).ToString
                    If status = "ACTIVE" Then
                        mailingList.Add(reader.GetValue(3).ToString, reader.GetValue(1).ToString)
                    End If
                Loop
            End If
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub AutomaticEmail(mailSubject As String, mailFrom As String, mailTo As String, mailBody As String, attachment As String)
        Try
            Dim mail As New MailMessage
            mail.Subject = mailSubject
            mail.From = New MailAddress(mailFrom)
            mail.To.Add(mailTo)
            mail.Body = mailBody
            mail.Priority = MailPriority.High
            mail.IsBodyHtml = True

            'Dim MsgAttach As New Attachment(Application.StartupPath() + "\Upload\Sample.xml")
            'MailMsg.Attachments.Add(MsgAttach)

            Dim smtpMail As New SmtpClient(SMTPServer)
            smtpMail.Port = SMTPPort
            smtpMail.UseDefaultCredentials = False
            smtpMail.Credentials = New System.Net.NetworkCredential(SMTPUsername, SMTPPassword)
            smtpMail.EnableSsl = True
            smtpMail.Timeout = 20000
            smtpMail.Send(mail)
            MsgBox("Email Sent.", MsgBoxStyle.Information, "Automatic Email")
        Catch ex As Exception
            MsgBox("Error while sending mail.", MsgBoxStyle.Exclamation, "Automatic Email")
        End Try
    End Sub

    Private Sub btnReloadConfiguration_Click(sender As Object, e As EventArgs) Handles btnReloadConfiguration.Click
        GetReportConfiguration()
    End Sub

    Private Sub btnStopReports_Click(sender As Object, e As EventArgs) Handles btnStopReports.Click
        timer.Stop()
        SetRunStatus("STOP")
        btnStopReports.Enabled = False
        btnRunReports.Enabled = True
    End Sub
End Class