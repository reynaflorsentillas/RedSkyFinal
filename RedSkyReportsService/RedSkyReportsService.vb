Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.Mail
'Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Drawing
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices

Public Class RedSkyReportsService

    Dim connectionString As String = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
    Dim conn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim reader As SqlDataReader

    Dim eventId As Integer = 0

    Dim SMTPServer As String
    Dim SMTPPort As String
    Dim SMTPUsername As String
    Dim SMTPPassword As String

    Dim dailyStatus As String
    Dim dailyGeneration As DateTime
    Dim dailyFrom As DateTime
    Dim dailyTo As DateTime

    Dim weeklyStatus As String
    Dim weeklyGeneration As DateTime

    Dim monthlyStatus As String
    Dim monthlyGeneration As DateTime

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        GetReportConfiguration()


        Dim timer As System.Timers.Timer = New System.Timers.Timer()
        timer.Interval = 60000 ' 5 minutes
        AddHandler timer.Elapsed, AddressOf Me.OnTimer
        timer.Start()

        'GetMailingConfiguration()
        'AutomaticEmail("Sample", SMTPUsername, "reynaflorsentillas@ymail.com", "Sample email.")
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
    End Sub

    Private Sub OnTimer(sender As Object, e As Timers.ElapsedEventArgs)
        GenerateMonthly()
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
                        dailyGeneration = reader.GetDateTime(2)
                        dailyStatus = reader.GetValue(5).ToString
                        dailyFrom = reader.GetValue(6)
                        dailyTo = reader.GetValue(7)
                        EventLog1.WriteEntry("Daily: " & dailyGeneration.ToString & " " & dailyStatus & " " & dailyFrom.ToString & " " & dailyTo.ToString, EventLogEntryType.Information, eventId)
                    End If

                    If reportType = "Weekly" Then
                        weeklyGeneration = reader.GetDateTime(2)
                        weeklyStatus = reader.GetValue(5).ToString
                        EventLog1.WriteEntry("Weekly:  " & weeklyGeneration.ToString & " " & weeklyStatus, EventLogEntryType.Information, eventId)
                    End If

                    If reportType = "Monthly" Then
                        monthlyGeneration = reader.GetDateTime(2)
                        monthlyStatus = reader.GetValue(5).ToString
                        EventLog1.WriteEntry("Monthly: " & monthlyGeneration.ToString & " " & monthlyStatus, EventLogEntryType.Information, eventId)
                    End If
                Loop
            End If
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            EventLog1.WriteEntry("Error while fetching record from table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub GenerateDaily()
        If dailyStatus = "ENABLED" Then
            Try
                conn.ConnectionString = connectionString
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "SELECT * FROM AgentLogin WHERE LoginTime BETWEEN @dateFrom AND @dateTo"
                cmd.Parameters.AddWithValue("@dateFrom", dailyFrom)
                cmd.Parameters.AddWithValue("@dateTo", dailyTo)
                reader = cmd.ExecuteReader
                If reader.HasRows Then
                    EventLog1.WriteEntry("Has Data.", EventLogEntryType.Information, eventId)
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
                        Loop

                        Dim fileName As String

                        fileName = "Agent Login Report " & dailyFrom.ToString("ddMMyyyy") & "-" & dailyTo.ToString("ddMMyyyy") & ".xlsx"

                        xlWorksheet.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & fileName)

                        xlWorkbook.Close()
                        xlApp.Quit()

                        xlApp = Nothing
                        xlWorkbook = Nothing
                        xlWorksheet = Nothing

                        EventLog1.WriteEntry("Successfully generated report.", EventLogEntryType.Information, eventId)
                        eventId += 1
                    Catch ex As Exception
                        EventLog1.WriteEntry("Error generating report. " & ex.Message, EventLogEntryType.Error, eventId)
                        eventId += 1
                    End Try
                Else
                    EventLog1.WriteEntry("No Data." & dailyFrom & " " & dailyTo, EventLogEntryType.Error, eventId)
                End If
                cmd.Parameters.Clear()
                conn.Close()
                conn.Dispose()
            Catch ex As Exception
                EventLog1.WriteEntry("Error generating report. " & ex.Message, EventLogEntryType.Error, eventId)
                eventId += 1
                conn.Close()
                conn.Dispose()
            End Try
        Else
            EventLog1.WriteEntry("Daily generation is DISABLED. Please change configuration.", EventLogEntryType.Error, eventId)
            eventId += 1
        End If
    End Sub

    Private Sub GenerateMonthly()
        Try
            Dim xlApp As New Application
            Dim xlWorkbook As Workbook = xlApp.Workbooks.Add
            Dim xlWorksheet As Worksheet = xlWorkbook.Sheets.Add
            xlWorksheet.Name = "AgentLogins"


            'xlWorksheet.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & fileName)
            'xlWorksheet.SaveAs("C:\RedSky" & "\" & fileName)

            xlWorkbook.Close()
            xlApp.Quit()
            Marshal.ReleaseComObject(xlWorksheet)
            Marshal.ReleaseComObject(xlWorkbook)
            Marshal.ReleaseComObject(xlApp)
            EventLog1.WriteEntry("Successfully generated report.", EventLogEntryType.Information, eventId)

        Catch ex As Exception
            EventLog1.WriteEntry("Error generating report. " & ex.Message, EventLogEntryType.Error, eventId)
        End Try

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
            EventLog1.WriteEntry("Error while fetching record from table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub AutomaticEmail(mailSubject As String, mailFrom As String, mailTo As String, mailBody As String)
        EventLog1.WriteEntry(SMTPServer & " " & SMTPPort & " " & SMTPUsername & " " & SMTPPassword, EventLogEntryType.Information, eventId)
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
            EventLog1.WriteEntry("Email Sent. ", EventLogEntryType.Information, eventId)
            eventId += 1
        Catch ex As Exception
            EventLog1.WriteEntry("Error while sending mail. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
        End Try

    End Sub
End Class
