Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Net.Mail
Imports System.IO

Public Class RedSkyMain
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
    Dim conn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim reader As SqlDataReader

    Dim timer As System.Timers.Timer = New System.Timers.Timer()

    Dim SMTPServer As String
    Dim SMTPPort As String
    Dim SMTPUseSSL As Boolean
    Dim SMTPUsername As String
    Dim SMTPPassword As String

    Dim mailingList As New Dictionary(Of String, String)

    'Dim reportsLocation As String

    'Dim dailyStatus As String
    Dim dailyGeneration As DateTime
    Dim dailyFrom As DateTime
    Dim dailyTo As DateTime
    Dim dailyLastGeneration As DateTime

    'Dim weeklyStatus As String
    Dim weeklyGeneration As DateTime
    Dim weeklyGenerationDay As String
    Dim weeklyLastGeneration As DateTime

    'Dim monthlyStatus As String
    Dim monthlyGeneration As DateTime
    Dim monthlyLastGeneration As DateTime

    Dim today As DateTime

    Dim filterDomain As String = ""
    Dim filterGroup As String = ""

    Dim currentMachine As String = System.Net.Dns.GetHostName.ToString

    Private Sub RedSkyMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetReportConfiguration()
        GetMailingConfiguration()
        GetOtherConfiguration()
        GetMailingList()
        SetRunStatus("STOP")
        btnStopReports.Enabled = False
        btnConfiguration.Enabled = True
        btnReloadConfiguration.Enabled = True
        btnRunReports.Enabled = True
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
                        dailyLastGeneration = If(reader.IsDBNull(4), Nothing, reader.GetDateTime(4))
                    End If

                    If reportType = "Weekly" Then
                        weeklyGeneration = reader.GetDateTime(2)
                        'weeklyStatus = reader.GetValue(5).ToString
                        weeklyGenerationDay = reader.GetValue(8).ToString
                        lblWeeklyStatus.Text = reader.GetValue(5).ToString
                        weeklyLastGeneration = If(reader.IsDBNull(4), Nothing, reader.GetDateTime(4))
                    End If

                    If reportType = "Monthly" Then
                        monthlyGeneration = reader.GetDateTime(2)
                        ' monthlyStatus = reader.GetValue(5).ToString
                        lblMonthlyStatus.Text = reader.GetValue(5).ToString
                        monthlyLastGeneration = If(reader.IsDBNull(4), Nothing, reader.GetDateTime(4))
                    End If
                Loop
            End If
        Catch ex As Exception
            WriteLog("Error fetching configuration. " & ex.Message)
        Finally
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
        timer.Interval = 15000 ' 1 minute
        AddHandler timer.Elapsed, AddressOf Me.OnTimer
        timer.Start()
        SetRunStatus("RUN")
        btnRunReports.Enabled = False
        btnConfiguration.Enabled = False
        btnReloadConfiguration.Enabled = False
        btnStopReports.Enabled = True
    End Sub

    Private Sub OnTimer(sender As Object, e As Timers.ElapsedEventArgs)

        today = DateTime.UtcNow.AddHours(8)

        'DAILY REPORT
        If lblDailyStatus.Text = "ENABLED" Then
            If today.ToShortTimeString = dailyGeneration.ToShortTimeString Then
                GenerateDaily()
            End If
        End If

        'WEEKLY REPORT
        If lblWeeklyStatus.Text = "ENABLED" Then
            If today.DayOfWeek.ToString.ToUpper = weeklyGenerationDay Then
                If today.ToShortTimeString = weeklyGeneration.ToShortTimeString Then
                    GenerateWeekly()
                End If
            End If
        End If

        'MONTHLY REPORT
        Dim daysInMonth As Integer
        daysInMonth = Date.DaysInMonth(today.Year, today.Month)
        Dim lastDayInMonth As Date = New Date(today.Year, today.Month, daysInMonth)
        If lblMonthlyStatus.Text = "ENABLED" Then
            If today.ToShortDateString = lastDayInMonth.ToShortDateString Then
                If today.ToShortTimeString = monthlyGeneration.ToShortTimeString Then
                    GenerateMonthly(lastDayInMonth)
                End If
            End If
        End If
    End Sub

    Private Sub GenerateDaily()
        Dim dateFrom As DateTime
        dateFrom = Convert.ToDateTime(today.ToShortDateString & " " & dailyFrom.ToLongTimeString)

        Dim dateTo As DateTime
        dateTo = Convert.ToDateTime(today.ToShortDateString & " " & dailyTo.ToLongTimeString)

        If dailyFrom.ToString("tt") = "PM" And dailyTo.ToString("tt") = "AM" Then
            dateFrom = dateFrom.AddDays(-1)
        End If

        Dim generated As Boolean = False
        Dim filePath As String = Application.StartupPath & "\Reports\Daily\"
        Dim fileName As String = ""
        Dim seats As Integer = 0

        If Not Directory.Exists(filePath) Then
            Directory.CreateDirectory(filePath)
        End If

        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT LoginDate, LoginTime, COUNT(Username), Username, Workstation FROM AgentLogin WHERE Domain = @Domain AND Group = @Group AND LoginTime BETWEEN @dateFrom AND @dateTo GROUP	BY LoginDate, LoginTime, Username, Workstation"
            cmd.Parameters.AddWithValue("@dateFrom", dateFrom)
            cmd.Parameters.AddWithValue("@dateTo", dateTo)
            cmd.Parameters.AddWithValue("@Domain", filterDomain)
            cmd.Parameters.AddWithValue("@Group", filterGroup)
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                'MsgBox("Has Data!")
                Try
                    Dim xlApp As New Excel.Application
                    Dim xlWorkbook As Excel.Workbook = xlApp.Workbooks.Add
                    Dim xlWorksheet As Excel.Worksheet = CType(xlWorkbook.Sheets("sheet1"), Excel.Worksheet)
                    xlWorksheet.Name = "Agent Logins"
                    Dim xlRange As Excel.Range
                    xlRange = xlWorksheet.Range("A5", "E5")
                    xlRange.ColumnWidth = 30
                    xlRange.Interior.Color = Color.LightBlue
                    xlRange.Font.Bold = True

                    xlWorksheet.Range("D2").Value = "PERIOD: " & dateFrom.ToString & " - " & dateTo.ToString

                    xlWorksheet.Range("A5").Value = "LOGIN DATE"
                    xlWorksheet.Range("B5").Value = "LOGIN TIME"
                    xlWorksheet.Range("C5").Value = "SEATS USED"
                    xlWorksheet.Range("D5").Value = "USERNAME"
                    xlWorksheet.Range("E5").Value = "WORDKSTATION"
                    'xlWorksheet.Range("E1").Value = "LOGIN TIME"
                    'xlWorksheet.Range("F1").Value = "LOGOUT DATE"
                    'xlWorksheet.Range("G1").Value = "LOGOUT TIME"
                    'xlWorksheet.Range("H1").Value = "LOGIN DURATION"

                    Dim rowCount As Integer = 6

                    Do While reader.Read
                        xlWorksheet.Cells(rowCount, 1) = If(reader.IsDBNull(0), Nothing, reader.GetValue(0))
                        xlWorksheet.Cells(rowCount, 2) = If(reader.IsDBNull(1), Nothing, reader.GetValue(1))
                        xlWorksheet.Cells(rowCount, 3) = If(reader.IsDBNull(2), Nothing, reader.GetValue(2))
                        xlWorksheet.Cells(rowCount, 4) = If(reader.IsDBNull(3), Nothing, reader.GetValue(3))
                        xlWorksheet.Cells(rowCount, 5) = If(reader.IsDBNull(4), Nothing, reader.GetValue(4))
                        rowCount += 1
                        seats += 1
                    Loop

                    xlWorksheet.Range("D3").Value = "TOTAL SEATS USED: " & seats.ToString

                    fileName = "CSI RedSky Daily Summary " & today.ToString("ddMMyyyy") & ".xlsx"

                    If File.Exists(filePath & fileName) Then
                        File.Delete(filePath & fileName)
                    End If

                    xlWorksheet.SaveAs(filePath & fileName)

                    xlWorkbook.Close()
                    xlApp.Quit()

                    xlApp = Nothing
                    xlWorkbook = Nothing
                    xlWorksheet = Nothing

                    generated = True

                    WriteLog("DAILY REPORT: Successfully generated report. " & fileName)
                Catch ex As Exception
                    WriteLog("DAILY REPORT: Error generating report. " & ex.Message)
                End Try
                'Else
                'MsgBox("No Data! " & dateFrom & " " & dateTo)
            End If
        Catch ex As Exception
            WriteLog("DAILY REPORT: Error generating report. " & ex.Message)
        Finally
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try

        If generated = True Then
            Dim subject As String
            subject = "CSI - RedSky: Daily Summary " & today.ToShortDateString
            Dim body As String
            body = "Hi, <br><br> Today, your number of seats utilized are: " & seats.ToString & " <br>Please see attched file for more details. <br><br>Best Regards, <br>Capstone Solutions Inc."
            Dim attachment As String
            attachment = filePath & fileName
            AutomaticEmail(subject, SMTPUsername, body, attachment, "Daily")

            Dim nextGeneration As DateTime
            nextGeneration = Convert.ToDateTime(today.ToShortDateString & " " & dailyGeneration.ToLongTimeString)
            If dailyLastGeneration = Nothing Then
                dailyLastGeneration = nextGeneration
            End If
            nextGeneration = nextGeneration.AddDays(1)
            If dailyLastGeneration.ToShortDateString = nextGeneration.ToShortDateString Then
                dailyLastGeneration = dailyLastGeneration.AddDays(-1)
            End If
            UpdateReportConiguration("Daily", dailyLastGeneration, nextGeneration)
        End If
    End Sub

    Private Sub GenerateWeekly()
        Dim dateFrom As DateTime
        dateFrom = Convert.ToDateTime(today.ToShortDateString)
        dateFrom = dateFrom.AddDays(-7)

        Dim dateTo As DateTime
        dateTo = Convert.ToDateTime(today.ToShortDateString)

        Dim generated As Boolean = False
        Dim filePath As String = Application.StartupPath & "\Reports\Weekly\"
        Dim fileName As String = ""
        Dim seats As Integer = 0

        If Not Directory.Exists(filePath) Then
            Directory.CreateDirectory(filePath)
        End If

        Dim nightShift As Boolean = False
        If dailyFrom.ToString("tt") = "PM" And dailyTo.ToString("tt") = "AM" Then
            nightShift = True
        End If

        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn

            If nightShift = True Then
                'cmd.CommandText = "SELECT * FROM AgentLogin WHERE LoginDate BETWEEN @dateFrom AND @dateTo AND CAST(LoginTime as time) >= @timeFrom OR CAST(LoginTime as time) <= @timeTo"
                cmd.CommandText = "SELECT LoginDate, COUNT(Username) AS Seats FROM AgentLogin WHERE Domain = @Domain AND Group = @Group AND LoginDate BETWEEN @dateFrom AND @dateTo AND CAST(LoginTime as time) >= @timeFrom OR CAST(LoginTime as time) <= @timeTo GROUP BY LoginDate"
            Else
                'cmd.CommandText = "SELECT * FROM AgentLogin WHERE LoginDate BETWEEN @dateFrom AND @dateTo AND CAST(LoginTime as time) >= @timeFrom AND CAST(LoginTime as time) <= @timeTo"
                cmd.CommandText = "SELECT LoginDate, COUNT(Username) AS Seats FROM AgentLogin WHERE Domain = @Domain AND Group = @Group AND LoginDate BETWEEN @dateFrom AND @dateTo AND CAST(LoginTime as time) >= @timeFrom AND CAST(LoginTime as time) <= @timeTo GROUP BY LoginDate"
            End If
            'cmd.CommandText = "SELECT * FROM AgentLogin WHERE LoginTime BETWEEN @dateFrom AND @dateTo"
            cmd.Parameters.AddWithValue("@dateFrom", dateFrom)
            cmd.Parameters.AddWithValue("@dateTo", dateTo)
            cmd.Parameters.AddWithValue("@timeFrom", dailyFrom.ToLongTimeString)
            cmd.Parameters.AddWithValue("@timeTo", dailyTo.ToLongTimeString)
            cmd.Parameters.AddWithValue("@Domain", filterDomain)
            cmd.Parameters.AddWithValue("@Group", filterGroup)
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                'MsgBox("Has Data!" & dateFrom.ToString & " " & dateTo.ToString)
                Try
                    Dim xlApp As New Excel.Application
                    Dim xlWorkbook As Excel.Workbook = xlApp.Workbooks.Add
                    Dim xlWorksheet As Excel.Worksheet = CType(xlWorkbook.Sheets("sheet1"), Excel.Worksheet)
                    xlWorksheet.Name = "Weekly Report"
                    Dim xlRange As Excel.Range
                    xlRange = xlWorksheet.Range("A2", "B2")
                    xlRange.ColumnWidth = 30
                    xlRange.Interior.Color = Color.LightBlue
                    xlRange.Font.Bold = True

                    xlWorksheet.Range("B1").Value = "PERIOD: " & dateFrom.ToShortDateString & " - " & dateTo.ToShortDateString

                    xlWorksheet.Range("A2").Value = "DATE"
                    xlWorksheet.Range("B2").Value = "NUMBER OF SEATS"
                    'xlWorksheet.Range("C1").Value = "WORKSTATION"
                    'xlWorksheet.Range("D1").Value = "LOGIN DATE"
                    'xlWorksheet.Range("E1").Value = "LOGIN TIME"
                    'xlWorksheet.Range("F1").Value = "LOGOUT DATE"
                    'xlWorksheet.Range("G1").Value = "LOGOUT TIME"
                    'xlWorksheet.Range("H1").Value = "LOGIN DURATION"

                    Dim rowCount As Integer = 3

                    Do While reader.Read
                        xlWorksheet.Cells(rowCount, 1) = If(reader.IsDBNull(0), Nothing, reader.GetValue(0).ToString)
                        xlWorksheet.Cells(rowCount, 2) = If(reader.IsDBNull(1), Nothing, reader.GetValue(1).ToString)
                        'xlWorksheet.Cells(rowCount, 3) = reader.GetValue(2).ToString
                        'xlWorksheet.Cells(rowCount, 4) = reader.GetDateTime(3).ToShortDateString
                        'xlWorksheet.Cells(rowCount, 5) = reader.GetDateTime(4).ToString
                        'xlWorksheet.Cells(rowCount, 6) = If(reader.IsDBNull(5), Nothing, reader.GetDateTime(5).ToShortDateString)
                        'xlWorksheet.Cells(rowCount, 7) = reader.GetValue(6).ToString
                        'xlWorksheet.Cells(rowCount, 8) = reader.GetValue(7).ToString
                        rowCount += 1
                        seats += reader.GetValue(1)
                    Loop

                    fileName = "CSI RedSky Weekly Summary " & dateFrom.ToString("ddMMyyyy") & " - " & dateTo.ToString("ddMMyyyy") & ".xlsx"

                    If File.Exists(filePath & fileName) Then
                        File.Delete(filePath & fileName)
                    End If

                    xlWorksheet.SaveAs(filePath & fileName)

                    xlWorkbook.Close()
                    xlApp.Quit()

                    xlApp = Nothing
                    xlWorkbook = Nothing
                    xlWorksheet = Nothing

                    generated = True

                    WriteLog("WEEKLY REPORT: Successfully generated report. " & fileName)

                Catch ex As Exception
                    MsgBox(ex.Message)
                    WriteLog("WEEKLY REPORT: Error generating report. " & ex.Message)
                End Try
                'Else
                'MsgBox("No Data! " & dateFrom.ToString & " " & dateTo.ToString)
            End If
        Catch ex As Exception
            WriteLog("WEEKLY REPORT: Error generating report. " & ex.Message)
        Finally
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try


        If generated = True Then
            Dim subject As String
            subject = "CSI - RedSky: Weekly Summary " & dateFrom.ToShortDateString & " - " & dateTo.ToShortDateString
            Dim body As String
            body = "Hi, <br><br> You utilized " & seats.ToString & " this week from " & dateFrom.ToShortDateString & " to " & dateTo.ToShortDateString & ". <br>Please see attched file for more details. <br><br>Best Regards, <br>Capstone Solutions Inc."
            Dim attachment As String
            attachment = filePath & fileName
            AutomaticEmail(subject, SMTPUsername, body, attachment, "Weekly")

            Dim nextGeneration As DateTime
            nextGeneration = Convert.ToDateTime(today.ToShortDateString & " " & weeklyGeneration.ToLongTimeString)
            If weeklyLastGeneration = Nothing Then
                weeklyLastGeneration = nextGeneration
            End If
            nextGeneration = nextGeneration.AddDays(7)
            If weeklyLastGeneration.ToShortDateString = nextGeneration.ToShortDateString Then
                weeklyLastGeneration = weeklyLastGeneration.AddDays(-7)
            End If
            UpdateReportConiguration("Weekly", weeklyLastGeneration, nextGeneration)
        End If
    End Sub

    Private Sub GenerateMonthly(lastDayInMonth As Date)
        Dim firstDayInMonth As Date
        firstDayInMonth = New Date(today.Year, today.Month, 1)

        Dim thisMonth As String
        thisMonth = MonthName(firstDayInMonth.Month)
        thisMonth = thisMonth & " " & firstDayInMonth.Year

        Dim generated As Boolean = False
        Dim filePath As String = Application.StartupPath & "\Reports\Monthly\"
        Dim fileName As String = ""
        Dim seats As Integer = 0

        If Not Directory.Exists(filePath) Then
            Directory.CreateDirectory(filePath)
        End If

        Dim nightShift As Boolean = False
        If dailyFrom.ToString("tt") = "PM" And dailyTo.ToString("tt") = "AM" Then
            nightShift = True
        End If

        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            If nightShift = True Then
                'cmd.CommandText = "SELECT * FROM AgentLogin WHERE LoginDate BETWEEN @dateFrom AND @dateTo AND CAST(LoginTime as time) >= @timeFrom OR CAST(LoginTime as time) <= @timeTo"
                cmd.CommandText = "SELECT LoginDate, COUNT(Username) AS Seats FROM AgentLogin WHERE Domain = @Domain AND Group = @Group AND LoginDate BETWEEN @dateFrom AND @dateTo AND CAST(LoginTime as time) >= @timeFrom OR CAST(LoginTime as time) <= @timeTo GROUP BY LoginDate"
            Else
                'cmd.CommandText = "SELECT * FROM AgentLogin WHERE LoginDate BETWEEN @dateFrom AND @dateTo AND CAST(LoginTime as time) >= @timeFrom AND CAST(LoginTime as time) <= @timeTo"
                cmd.CommandText = "SELECT LoginDate, COUNT(Username) AS Seats FROM AgentLogin WHERE Domain = @Domain AND Group = @Group AND LoginDate BETWEEN @dateFrom AND @dateTo AND CAST(LoginTime as time) >= @timeFrom AND CAST(LoginTime as time) <= @timeTo GROUP BY LoginDate"
            End If
            'cmd.CommandText = "SELECT * FROM AgentLogin WHERE LoginDate BETWEEN @dateFrom AND @dateTo"
            cmd.Parameters.AddWithValue("@dateFrom", firstDayInMonth)
            cmd.Parameters.AddWithValue("@dateTo", lastDayInMonth)
            cmd.Parameters.AddWithValue("@timeFrom", dailyFrom.ToLongTimeString)
            cmd.Parameters.AddWithValue("@timeTo", dailyTo.ToLongTimeString)
            cmd.Parameters.AddWithValue("@Domain", filterDomain)
            cmd.Parameters.AddWithValue("@Group", filterGroup)
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                'MsgBox("Has Data!" & firstDayInMonth.ToString & " " & lastDayInMonth.ToString)
                Try
                    Dim xlApp As New Excel.Application
                    Dim xlWorkbook As Excel.Workbook = xlApp.Workbooks.Add
                    Dim xlWorksheet As Excel.Worksheet = CType(xlWorkbook.Sheets("sheet1"), Excel.Worksheet)
                    xlWorksheet.Name = "Monthly Report"
                    Dim xlRange As Excel.Range
                    xlRange = xlWorksheet.Range("A5", "B5")
                    xlRange.ColumnWidth = 30
                    xlRange.Interior.Color = Color.LightBlue
                    xlRange.Font.Bold = True

                    xlWorksheet.Range("B2").Value = "PERIOD: " & thisMonth

                    xlWorksheet.Range("A5").Value = "LOGIN DATE"
                    xlWorksheet.Range("B5").Value = "NUMBER OF SEATS"
                    'xlWorksheet.Range("C1").Value = "WORKSTATION"
                    'xlWorksheet.Range("D1").Value = "LOGIN DATE"
                    'xlWorksheet.Range("E1").Value = "LOGIN TIME"
                    'xlWorksheet.Range("F1").Value = "LOGOUT DATE"
                    'xlWorksheet.Range("G1").Value = "LOGOUT TIME"
                    'xlWorksheet.Range("H1").Value = "LOGIN DURATION"

                    Dim rowCount As Integer = 6

                    Do While reader.Read
                        xlWorksheet.Cells(rowCount, 1) = reader.GetValue(0)
                        xlWorksheet.Cells(rowCount, 2) = reader.GetValue(1)
                        'xlWorksheet.Cells(rowCount, 3) = reader.GetValue(2).ToString
                        'xlWorksheet.Cells(rowCount, 4) = reader.GetDateTime(3).ToShortDateString
                        'xlWorksheet.Cells(rowCount, 5) = reader.GetDateTime(4).ToString
                        'xlWorksheet.Cells(rowCount, 6) = If(reader.IsDBNull(5), Nothing, reader.GetDateTime(5).ToShortDateString)
                        'xlWorksheet.Cells(rowCount, 7) = reader.GetValue(6).ToString
                        'xlWorksheet.Cells(rowCount, 8) = reader.GetValue(7).ToString
                        rowCount += 1
                        seats += reader.GetValue(1)
                    Loop

                    xlWorksheet.Range("B3").Value = "TOTAL SEATS USED: " & seats.ToString

                    fileName = "CSI RedSky Monthly Summary " & thisMonth & ".xlsx"

                    If File.Exists(filePath & fileName) Then
                        File.Delete(filePath & fileName)
                    End If

                    xlWorksheet.SaveAs(filePath & fileName)

                    xlWorkbook.Close()
                    xlApp.Quit()

                    xlApp = Nothing
                    xlWorkbook = Nothing
                    xlWorksheet = Nothing

                    generated = True

                    WriteLog("MONTHLY REPORT: Successfully generated report. " & fileName)
                Catch ex As Exception
                    WriteLog("MONTHLY REPORT: Error generating report. " & ex.Message)
                End Try
                'Else
                'MsgBox("No Data! " & firstDayInMonth.ToString & " " & lastDayInMonth.ToString)
            End If
        Catch ex As Exception
            WriteLog("MONTHLY REPORT: Error generating report. " & ex.Message)
        Finally
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try

        If generated = True Then
            Dim subject As String
            subject = "CSI - RedSky: Monthy Summary " & thisMonth
            Dim body As String
            body = "Hi, <br><br> You utilized " & seats.ToString & " for the month of " & thisMonth & ". <br>Please see attched file for more details. <br><br>Best Regards, <br>Capstone Solutions Inc."
            Dim attachment As String
            attachment = filePath & fileName
            AutomaticEmail(subject, SMTPUsername, body, attachment, "Monthly")

            Dim nextGeneration As DateTime
            nextGeneration = Convert.ToDateTime(today.ToShortDateString & " " & monthlyGeneration.ToLongTimeString)
            If monthlyLastGeneration = Nothing Then
                monthlyLastGeneration = nextGeneration
            End If
            nextGeneration = nextGeneration.AddMonths(1)
            If monthlyLastGeneration.ToShortDateString = nextGeneration.ToShortDateString Then
                monthlyLastGeneration = monthlyLastGeneration.AddMonths(-1)
            End If
            UpdateReportConiguration("Monthly", monthlyLastGeneration, nextGeneration)
        End If
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
                    SMTPUseSSL = Convert.ToBoolean(reader.GetValue(3)).ToString
                    SMTPUsername = reader.GetValue(4).ToString
                    SMTPPassword = reader.GetValue(5).ToString
                Loop
            End If
        Catch ex As Exception
            WriteLog("MAILING CONFIGURATION: Error fetching mailing configuration from table. " & ex.Message)
        Finally
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
        Catch ex As Exception
            WriteLog("MAILING LIST: Error fetching mailing list from table. " & ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub AutomaticEmail(mailSubject As String, mailFrom As String, mailBody As String, attachment As String, reportType As String)
        Dim mail As New MailMessage
        Dim smtpMail As New SmtpClient(SMTPServer)
        Dim mailAttach As New Attachment(attachment)

        Try

            mail.Subject = mailSubject
            mail.From = New MailAddress(mailFrom)

            For Each email In mailingList
                If email.Key.Contains(reportType) Then
                    mail.To.Add(email.Value)
                End If
            Next

            mail.Body = mailBody
            mail.Priority = MailPriority.High
            mail.IsBodyHtml = True

            If File.Exists(attachment) Then

                mail.Attachments.Add(mailAttach)
            End If

            smtpMail.Port = SMTPPort
            smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
            smtpMail.UseDefaultCredentials = False
            smtpMail.Credentials = New System.Net.NetworkCredential(SMTPUsername, SMTPPassword)
            smtpMail.EnableSsl = SMTPUseSSL
            smtpMail.Timeout = 60000
            smtpMail.Send(mail)
            WriteLog("AUTOMATIC EMAIL: Email Sent: " & mailSubject)
            mailAttach.Dispose()
            smtpMail.Dispose()
        Catch ex As Exception
            WriteLog("AUTOMATIC EMAIL: Error while sending mail. " & ex.Message)
            mailAttach.Dispose()
            smtpMail.Dispose()
        End Try
    End Sub

    Private Sub GetOtherConfiguration()
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM OtherConfiguration"
            'cmd.Parameters.AddWithValue("@ConfigName", "DOMAIN")
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                Do While reader.Read
                    If reader.GetValue(1).ToString = "DOMAIN" Then
                        filterDomain = reader.GetValue(2).ToString
                    ElseIf reader.GetValue(1).ToString = "GROUP" Then
                        filterGroup = reader(2).ToString
                    End If
                Loop
            End If
        Catch ex As Exception
            WriteLog("OTHER CONFIGURATION: Error fetching configuration. " & ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub btnConfiguration_Click(sender As Object, e As EventArgs) Handles btnConfiguration.Click
        Dim frm As New RedSkyConfiguration
        frm.Show()
    End Sub

    Private Sub btnReloadConfiguration_Click(sender As Object, e As EventArgs) Handles btnReloadConfiguration.Click
        GetReportConfiguration()
        GetMailingConfiguration()
        GetOtherConfiguration()
        SetRunStatus("STOP")
    End Sub

    Private Sub btnStopReports_Click(sender As Object, e As EventArgs) Handles btnStopReports.Click
        timer.Stop()
        SetRunStatus("STOP")
        btnStopReports.Enabled = False
        btnConfiguration.Enabled = True
        btnReloadConfiguration.Enabled = True
        btnRunReports.Enabled = True
    End Sub

    Private Sub WriteLog(message As String)
        Dim path As String = Application.StartupPath & "\Logs\"
        Dim filename As String = "Logs.txt"
        Dim writeTime As DateTime = DateTime.UtcNow.AddHours(8)

        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If

        If Not File.Exists(path & filename) Then
            File.Create(path & filename)
        Else
            Using writer As StreamWriter = File.AppendText(path & filename)
                writer.WriteLine("LOG TIMESTAMP: " & writeTime.ToString & vbTab & "LOG MESSAGE: " & message & vbNewLine & vbNewLine)
            End Using
        End If
    End Sub

    Private Sub UpdateReportConiguration(reportType As String, lastGeneration As DateTime, nextGeneration As DateTime)
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "UPDATE ReportConfiguration SET LastReportGeneration = @LastReportGeneration, NextReportGeneration = @NextReportGeneration WHERE ReportType = @ReportType"
            cmd.Parameters.AddWithValue("@LastReportGeneration", lastGeneration)
            cmd.Parameters.AddWithValue("@NextReportGeneration", nextGeneration)
            cmd.Parameters.AddWithValue("@ReportType", reportType)
            cmd.ExecuteNonQuery()
            WriteLog(reportType.ToString.ToUpper & " REPORT: Successfully updated report configuration. ")
        Catch ex As Exception
            WriteLog(reportType.ToString.ToUpper & " REPORT: Error updating Report Configuration. " & ex.Message)
        Finally
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

End Class