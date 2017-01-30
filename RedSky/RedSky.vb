Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.ServiceProcess
Imports System.Management

Public Class RedSky
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
    Dim conn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim reader As SqlDataReader

    Dim currentAgentLogin As DateTime
    Dim currentLoginDuration As Double = 0
    Dim eventId As Integer = 1

    Dim timer As System.Timers.Timer = New System.Timers.Timer()
    'Dim forceLogOffTimer As System.Timers.Timer = New System.Timers.Timer()

    Dim currentDomain As String = Environment.UserDomainName
    Dim currentUser As String = ""
    'Dim localWorkstation As String = Environment.MachineName
    Dim currentMachine As String = System.Net.Dns.GetHostName.ToString

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        'Me.AgentLogin()
        'Dim timer As System.Timers.Timer = New System.Timers.Timer()
        'Timer.Interval = 300000 ' 5 minutes
        'AddHandler timer.Elapsed, AddressOf Me.OnTimer
        'Timer.Start()

        Try
            Dim searcher As New ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem")
            Dim collection As ManagementObjectCollection = searcher.[Get]()
            Dim users As String = ""
            For Each oReturn As ManagementObject In collection
                users = oReturn("UserName")
                Dim user As String()
                user = users.Split("\")
                For i = 0 To user.Length Step 1
                    If i = 1 Then
                        currentUser = user(i)
                    End If
                Next
            Next
        Catch ex As Exception
            EventLog1.WriteEntry("Error fetching and parsing username of current user. " & ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
        End Try

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        Me.AgentLogout()
    End Sub

    'Protected Overrides Sub OnShutdown()
    'Me.AgentLogout()
    'End Sub
    Protected Overrides Sub OnSessionChange(changeDescription As SessionChangeDescription)
        timer.Interval = 60000 ' 5 minutes
        AddHandler timer.Elapsed, AddressOf Me.OnTimer

        If changeDescription.Reason = SessionChangeReason.SessionLogon Then
            Me.AgentLogin()
            timer.Start()

        ElseIf changeDescription.Reason = SessionChangeReason.SessionLogoff Then
            Me.AgentLogout()
            timer.Stop()
        End If
        MyBase.OnSessionChange(changeDescription)
    End Sub

    Private Sub AgentLogin()
        'Dim machinename As String = Environment.MachineName
        'Dim username As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name
        Dim user As String = currentDomain & "\" & currentUser
        Dim agentlogin As DateTime = DateTime.UtcNow.AddHours(8)
        currentAgentLogin = agentlogin
        Me.CheckAgentLogin(user, currentMachine)
        Me.AgentLoginDBInsert(user, currentMachine, agentlogin)
    End Sub

    Private Sub CheckAgentLogin(username As String, machinename As String)
        conn.ConnectionString = connectionString
        Dim setForceLogOff As Integer = 0
        Try
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM AgentLogin WHERE Username = @Username AND isLogin = 1"
            cmd.Parameters.AddWithValue("@Username", username)
            'cmd.ExecuteNonQuery()
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                setForceLogOff = 1
            End If
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            EventLog1.WriteEntry("Error while fetching record on table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
            conn.Close()
            conn.Dispose()
        End Try

        If setForceLogOff = 1 Then
            Dim remarks As String
            remarks = "No multiple login allowed for user " & username & ". User is currently loggedin on " & machinename & ". Force log off implemented."
            Try
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "UPDATE AgentLogin SET ForceLogOff = 1, Remarks = @Remarks, ModifiedDate = @ModifiedDate WHERE Username = @Username AND isLogin = 1"
                cmd.Parameters.AddWithValue("@Remarks", remarks)
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.UtcNow.AddHours(8))
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.ExecuteNonQuery()
                EventLog1.WriteEntry(remarks, EventLogEntryType.Information, eventId)
                eventId += 1
                cmd.Parameters.Clear()
                conn.Close()
                conn.Dispose()
            Catch ex As Exception
                EventLog1.WriteEntry("Error while updating record on table. " + ex.Message, EventLogEntryType.Error, eventId)
                eventId += 1
                conn.Close()
                conn.Dispose()
            End Try
        End If
    End Sub

    Private Sub AgentLoginDBInsert(username As String, machinename As String, agentlogin As DateTime)
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO AgentLogin(Username, Workstation, LoginDate, LoginTime, isLogin, DateAdded) VALUES (@Username, @Workstation, @LoginDate, @LoginTime, 1, @DateAdded)"
            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.AddWithValue("@Workstation", machinename)
            cmd.Parameters.AddWithValue("@LoginDate", agentlogin.Date)
            cmd.Parameters.AddWithValue("@LoginTime", agentlogin)
            cmd.Parameters.AddWithValue("@DateAdded", agentlogin)
            cmd.ExecuteNonQuery()
            EventLog1.WriteEntry("New login from " + username + " in " + machinename + ". " + agentlogin, EventLogEntryType.Information, eventId)
            eventId += 1
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            EventLog1.WriteEntry("Error while inserting record on table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub AgentLogout()
        'Dim machinename As String = Environment.MachineName
        'Dim username As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name
        Dim user As String = currentDomain & "\" & currentUser
        Dim agentlogout As DateTime = DateTime.UtcNow.AddHours(8)
        Me.AgentLogoutDBUpdate(user, currentMachine, agentlogout)
    End Sub

    Private Sub AgentLogoutDBUpdate(username As String, machinename As String, agentlogout As DateTime)
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "UPDATE AgentLogin SET LogoutDate = @LogoutDate, LogoutTime = @LogoutTime, isLogin = 0,  DateModified = @DateModified WHERE Username = @LogoutUsername AND Workstation = @LogoutWorkstation AND isLogin = 1"
            cmd.Parameters.AddWithValue("@LogoutDate", agentlogout.Date)
            cmd.Parameters.AddWithValue("@LogoutTime", agentlogout)
            cmd.Parameters.AddWithValue("@DateModified", agentlogout)
            cmd.Parameters.AddWithValue("@LogoutUsername", username)
            cmd.Parameters.AddWithValue("@LogoutWorkstation", machinename)
            cmd.ExecuteNonQuery()
            EventLog1.WriteEntry(username + " logged out in " + machinename + ". " + agentlogout, EventLogEntryType.Information, eventId)
            eventId += 1
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            EventLog1.WriteEntry("Error while updating record on table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub OnTimer(sender As Object, e As Timers.ElapsedEventArgs)
        ' TODO: Insert monitoring activities here.
        'Dim machinename As String = Environment.MachineName
        'Dim username As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name
        Dim user As String = currentDomain & "\" & currentUser
        Dim currentDateTime As DateTime = DateTime.UtcNow.AddHours(8)

        Dim span As New TimeSpan
        span = currentDateTime.Subtract(currentAgentLogin)
        currentLoginDuration = span.TotalSeconds

        Dim loginDurationSpan As TimeSpan = TimeSpan.FromSeconds(currentLoginDuration)
        Dim loginDuration As String = loginDurationSpan.ToString("hh\:mm\:ss")

        'EventLog1.WriteEntry("Monitoring the System. Current Agent Login: " + currentAgentLogin + " Current Date Time: " + currentDateTime + " Login Duration: " + loginDuration, EventLogEntryType.Information)
        'Me.AgentLoginDBUpdate(user, currentDomain, loginDuration, currentDateTime)
        Me.CheckForceLogOff(user, currentDomain)
    End Sub

    Private Sub AgentLoginDBUpdate(username As String, machinename As String, loginDuration As String, currentDateTime As DateTime)
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "UPDATE AgentLogin SET LoginDuration = @LoginDuration, DateModified = @LoginDurationDateModified WHERE Username = @LoginDurationUsername AND Workstation = @LoginDurationWorkstation AND isLogin = 1"
            cmd.Parameters.AddWithValue("@LoginDuration", loginDuration)
            cmd.Parameters.AddWithValue("@LoginDurationDateModified", currentDateTime)
            cmd.Parameters.AddWithValue("@LoginDurationUsername", username)
            cmd.Parameters.AddWithValue("@LoginDurationWorkstation", machinename)
            cmd.ExecuteNonQuery()
            EventLog1.WriteEntry("Monitoring the System. Current Agent Login: " + currentAgentLogin + " Current Date Time: " + currentDateTime + " Login Duration: " + loginDuration, EventLogEntryType.Information, eventId)
            eventId += 1
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            EventLog1.WriteEntry("Error while updating record on table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub CheckForceLogOff(username As String, machinename As String)
        Dim forceLogOff As Integer = 0
        Try
            conn.ConnectionString = connectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM AgentLogin WHERE Username = @Username AND Workstation = @Workstation AND isLogin = 1 AND ForceLogOff = 1"
            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.AddWithValue("@Workstation", machinename)
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                forceLogOff = 1
            End If
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            EventLog1.WriteEntry("Error while fetching record on table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
            conn.Close()
            conn.Dispose()
        End Try

        If forceLogOff = 1 Then
            EventLog1.WriteEntry("Force logoff implemented for user " & username & " in comouter " & machinename & ". Goodbye.", EventLogEntryType.Information, eventId)
            eventId += 1
            Shell("shutdown -l")
        End If
    End Sub

End Class
