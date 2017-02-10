Imports System.Data.SqlClient
Imports System.Configuration
Imports System.ServiceProcess
Imports System.Runtime.InteropServices
Imports System.DirectoryServices

Public Class RedSky
    Private Enum WTS_INFO_CLASS
        WTSInitialProgram
        WTSApplicationName
        WTSWorkingDirectory
        WTSOEMId
        WTSSessionId
        WTSUserName
        WTSWinStationName
        WTSDomainName
        WTSConnectState
        WTSClientBuildNumber
        WTSClientName
        WTSClientDirectory
        WTSClientProductId
        WTSClientHardwareId
        WTSClientAddress
        WTSClientDisplay
        WTSClientProtocolType
        WTSIdleTime
        WTSLogonTime
        WTSIncomingBytes
        WTSOutgoingBytes
        WTSIncomingFrames
        WTSOutgoingFrames
    End Enum

    Private Declare Auto Function WTSQuerySessionInformation Lib "wtsapi32.dll" (
     ByVal hServer As Int32,
     ByVal SessionId As Int32,
     ByVal InfoClass As WTS_INFO_CLASS,
     ByRef ppBuffer As IntPtr,
     ByRef pCount As Int32) As Int32

    'Dim connectionString As String = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
    Dim conn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim reader As SqlDataReader

    Dim currentAgentLogin As DateTime
    Dim currentLoginDuration As Double = 0
    Dim eventId As Integer = 1

    Dim timer As System.Timers.Timer = New System.Timers.Timer()
    'Dim forceLogOffTimer As System.Timers.Timer = New System.Timers.Timer()

    Dim currentDomain As String = ""
    Dim currentUser As String = ""
    'Dim localWorkstation As String = Environment.MachineName
    Dim currentMachine As String = ""
    Dim currentGroup As String = ""


    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        'Me.AgentLogin()
        'Dim timer As System.Timers.Timer = New System.Timers.Timer()
        'Timer.Interval = 300000 ' 5 minutes
        'AddHandler timer.Elapsed, AddressOf Me.OnTimer
        'Timer.Start()

        Init()



        'timer.Interval = 60000 ' 1 minute
        'AddHandler timer.Elapsed, AddressOf Me.OnTimer
        'timer.Start()
        'CreateBatFileForLogOff()
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        ' Me.AgentLogout()
    End Sub

    Protected Overrides Sub OnSessionChange(changeDescription As SessionChangeDescription)
        timer.Interval = 60000 ' 5 minutes
        AddHandler timer.Elapsed, AddressOf Me.OnTimer

        If changeDescription.Reason = SessionChangeReason.SessionLogon Then
            Init()
            Me.AgentLogin()
            timer.Start()
        ElseIf changeDescription.Reason = SessionChangeReason.SessionLogoff Then
            Me.AgentLogout()
            timer.Stop()
        End If
        MyBase.OnSessionChange(changeDescription)
    End Sub

    Private Sub AgentLogin()
        Dim agentlogin As DateTime = DateTime.UtcNow.AddHours(8)
        currentAgentLogin = agentlogin
        Me.CheckAgentLogin(currentDomain, currentGroup, currentUser, currentMachine)
        Me.AgentLoginDBInsert(currentDomain, currentGroup, currentUser, currentMachine, agentlogin)
    End Sub

    Private Sub CheckAgentLogin(domain As String, group As String, username As String, machinename As String)
        Dim setForceLogOff As Integer = 0
        Try
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM AgentLogin WHERE Domain = @Domain AND Group = @Group AND Username = @Username AND isLogin = 1"
            cmd.Parameters.AddWithValue("@Domain", domain)
            cmd.Parameters.AddWithValue("@Group", group)
            cmd.Parameters.AddWithValue("@Username", username)
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                setForceLogOff = 1
            End If
        Catch ex As Exception
            EventLog1.WriteEntry("Error while fetching record on table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
        Finally
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try

        If setForceLogOff = 1 Then
            Dim remarks As String
            remarks = "No multiple login allowed for " & domain & "\" & username & ". User is currently loggedin on " & machinename & ". Force log off implemented."
            Try
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "UPDATE AgentLogin SET ForceLogOff = 1, Remarks = @Remarks, DateModified = @DateModified WHERE Domain = @Domain AND Group = @Group AND Username = @Username AND isLogin = 1"
                cmd.Parameters.AddWithValue("@Remarks", remarks)
                cmd.Parameters.AddWithValue("@DateModified", DateTime.UtcNow.AddHours(8))
                cmd.Parameters.AddWithValue("@Domain", domain)
                cmd.Parameters.AddWithValue("@Group", group)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.ExecuteNonQuery()
                EventLog1.WriteEntry(remarks, EventLogEntryType.Information, eventId)
                eventId += 1
            Catch ex As Exception
                EventLog1.WriteEntry("Error while updating record on table. " + ex.Message, EventLogEntryType.Error, eventId)
                eventId += 1
            Finally
                cmd.Parameters.Clear()
                conn.Close()
                conn.Dispose()
            End Try
        End If
    End Sub

    Private Sub AgentLoginDBInsert(domain As String, group As String, username As String, machinename As String, agentlogin As DateTime)
        Try
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO AgentLogin(Domain, Group, Username, Workstation, LoginDate, LoginTime, isLogin, DateAdded) VALUES (@Domain, @Group, @Username, @Workstation, @LoginDate, @LoginTime, 1, @DateAdded)"
            cmd.Parameters.AddWithValue("@Domain", domain)
            cmd.Parameters.AddWithValue("#Group", group)
            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.AddWithValue("@Workstation", machinename)
            cmd.Parameters.AddWithValue("@LoginDate", agentlogin.Date)
            cmd.Parameters.AddWithValue("@LoginTime", agentlogin)
            cmd.Parameters.AddWithValue("@DateAdded", agentlogin)
            cmd.ExecuteNonQuery()
            EventLog1.WriteEntry("New login from " + domain + "\" + username + " in " + machinename + ". " + agentlogin, EventLogEntryType.Information, eventId)
            eventId += 1
        Catch ex As Exception
            EventLog1.WriteEntry("Error while inserting record on table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
        Finally
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub AgentLogout()
        Dim agentlogout As DateTime = DateTime.UtcNow.AddHours(8)
        Me.AgentLogoutDBUpdate(currentDomain, currentGroup, currentUser, currentMachine, agentlogout)
    End Sub

    Private Sub AgentLogoutDBUpdate(domain As String, group As String, username As String, machinename As String, agentlogout As DateTime)
        Try
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "UPDATE AgentLogin SET LogoutDate = @LogoutDate, LogoutTime = @LogoutTime, isLogin = 0,  DateModified = @DateModified WHERE Domain = @Domain AND Group = @Group AND Username = @LogoutUsername AND Workstation = @LogoutWorkstation AND isLogin = 1"
            cmd.Parameters.AddWithValue("@LogoutDate", agentlogout.Date)
            cmd.Parameters.AddWithValue("@LogoutTime", agentlogout)
            cmd.Parameters.AddWithValue("@DateModified", agentlogout)
            cmd.Parameters.AddWithValue("@Domain", domain)
            cmd.Parameters.AddWithValue("@Group", group)
            cmd.Parameters.AddWithValue("@LogoutUsername", username)
            cmd.Parameters.AddWithValue("@LogoutWorkstation", machinename)
            cmd.ExecuteNonQuery()
            EventLog1.WriteEntry(username + " logged out in " + machinename + ". " + agentlogout, EventLogEntryType.Information, eventId)
            eventId += 1
        Catch ex As Exception
            EventLog1.WriteEntry("Error while updating record on table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
        Finally
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub OnTimer(sender As Object, e As Timers.ElapsedEventArgs)
        ' TODO: Insert monitoring activities here.
        Dim currentDateTime As DateTime = DateTime.UtcNow.AddHours(8)

        Dim span As New TimeSpan
        span = currentDateTime.Subtract(currentAgentLogin)
        currentLoginDuration = span.TotalSeconds

        Dim loginDurationSpan As TimeSpan = TimeSpan.FromSeconds(currentLoginDuration)
        Dim loginDuration As String = loginDurationSpan.ToString("hh\:mm\:ss")
        'EventLog1.WriteEntry("Monitoring the System. Current Agent Login: " + currentAgentLogin + " Current Date Time: " + currentDateTime + " Login Duration: " + loginDuration, EventLogEntryType.Information)

        'EventLog1.WriteEntry("Monitoring the System. Current Agent Login: " + currentAgentLogin + " Current Date Time: " + currentDateTime + " Login Duration: " + loginDuration, EventLogEntryType.Information)
        'Me.AgentLoginDBUpdate(currentDomain, currentUser, currentDomain, loginDuration, currentDateTime)
        Me.CheckForceLogOff(currentDomain, currentGroup, currentUser, currentMachine)
    End Sub

    Private Sub AgentLoginDBUpdate(domain As String, group As String, username As String, machinename As String, loginDuration As String, currentDateTime As DateTime)
        Try
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "UPDATE AgentLogin SET LoginDuration = @LoginDuration, DateModified = @LoginDurationDateModified WHERE Domain = @Domain AND Group = @Group AND Username = @LoginDurationUsername AND Workstation = @LoginDurationWorkstation AND isLogin = 1"
            cmd.Parameters.AddWithValue("@LoginDuration", loginDuration)
            cmd.Parameters.AddWithValue("@LoginDurationDateModified", currentDateTime)
            cmd.Parameters.AddWithValue("@Domain", domain)
            cmd.Parameters.AddWithValue("@Group", group)
            cmd.Parameters.AddWithValue("@LoginDurationUsername", username)
            cmd.Parameters.AddWithValue("@LoginDurationWorkstation", machinename)
            cmd.ExecuteNonQuery()
            EventLog1.WriteEntry("Monitoring the System. Current Agent Login: " + currentAgentLogin + " Current Date Time: " + currentDateTime + " Login Duration: " + loginDuration, EventLogEntryType.Information, eventId)
            eventId += 1
        Catch ex As Exception
            EventLog1.WriteEntry("Error while updating record on table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
        Finally
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub CheckForceLogOff(domain As String, group As String, username As String, machinename As String)
        Dim forceLogOff As Integer = 0
        Try
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("RedSkyConnectionString").ConnectionString
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM AgentLogin WHERE Domain = @Domain AND Group = @Group AND Username = @Username AND Workstation = @Workstation AND isLogin = 1 AND ForceLogOff = 1"
            cmd.Parameters.AddWithValue("@Domain", domain)
            cmd.Parameters.AddWithValue("@Group", group)
            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.AddWithValue("@Workstation", machinename)
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                forceLogOff = 1
                '    EventLog1.WriteEntry("Has Rows", EventLogEntryType.Information, eventId)
                'Else
                '    EventLog1.WriteEntry("No Rows", EventLogEntryType.Information, eventId)
            End If
        Catch ex As Exception
            EventLog1.WriteEntry("Error while fetching record on table. " + ex.Message, EventLogEntryType.Error, eventId)
            eventId += 1
        Finally
            cmd.Parameters.Clear()
            conn.Close()
            conn.Dispose()
        End Try

        'EventLog1.WriteEntry(forceLogOff.ToString, EventLogEntryType.Information, eventId)

        If forceLogOff = 1 Then
            EventLog1.WriteEntry("Force logoff implemented for user " & username & " in computer " & machinename & ". Goodbye.", EventLogEntryType.Information, eventId)
            eventId += 1
            'Process.Start(batPath & batFilename)
            Dim currentSessionId As Integer = WTSGetActiveConsoleSessionId()
            LogOffUser(currentSessionId)
        End If
    End Sub

    Private Sub Init()
        currentDomain = Environment.UserDomainName
        currentUser = Machine.getInstance().getUsername()
        currentMachine = System.Net.Dns.GetHostName.ToString

        Dim dirEntry As New DirectoryEntry("LDAP://" & currentDomain)
        Dim dirSearcher As New DirectorySearcher(dirEntry)
        dirSearcher.SearchScope = SearchScope.Subtree
        dirSearcher.Filter = String.Format("(&(objectClass=user)(|(cn={0})(sn={0}*)(givenName={0})(sAMAccountName={0}*)))", currentUser)
        Dim searchResults = dirSearcher.FindAll()

        For Each sr As SearchResult In searchResults
            Dim de = sr.GetDirectoryEntry()
            Dim words As String() = de.Path.ToString().Split(",")
            Dim groups As String = ""
            Dim count As Integer = 0
            For Each item As String In words
                If item.Contains("OU=") Then
                    Dim group As String = item.ToString().Split(New String() {"OU="}, StringSplitOptions.None)(1)
                    If count = 0 Then
                        groups = group
                    Else
                        groups = group + "/" + groups
                    End If
                    count += 1
                End If
            Next
            currentGroup = groups
        Next

        EventLog1.WriteEntry(currentDomain & " " & currentGroup & " " & currentUser & " " & currentMachine, EventLogEntryType.Information, eventId)
        eventId += 1

        'Dim currentSessionId As Integer = WTSGetActiveConsoleSessionId()

        'Dim user As String = GetUsernameBySessionId(currentSessionId)
        'EventLog1.WriteEntry(user)

        'LogOffUser(currentSessionId)
    End Sub



    <DllImport("Wtsapi32.dll")>
    Private Shared Function WTSQuerySessionInformation(hServer As IntPtr, sessionId As Integer, wtsInfoClass As WTS_INFO_CLASS, ByRef ppBuffer As IntPtr, ByRef pBytesReturned As Integer) As Boolean
    End Function

    <DllImport("Kernel32.dll")>
    Public Shared Function WTSGetActiveConsoleSessionId() As Integer
    End Function

    <DllImport("Wtsapi32.dll")>
    Private Shared Sub WTSFreeMemory(pointer As IntPtr)
    End Sub

    <DllImport("WtsApi32.dll", SetLastError:=True)>
    Private Shared Function WTSDisconnectSession(hServer As IntPtr, sessionID As UInt32, bWait As Int32) As Boolean
    End Function

    <DllImport("wtsapi32.dll", SetLastError:=True)>
    Private Shared Function WTSLogoffSession(hServer As IntPtr, SessionId As Integer, bWait As Boolean) As Boolean
    End Function

    Public Shared Function GetUsernameBySessionId(sessionId As Integer) As [String]
        Dim buffer As IntPtr
        Dim strLen As Integer
        Dim username = "SYSTEM"
        ' System as this will return "\0" below
        If WTSQuerySessionInformation(IntPtr.Zero, sessionId, WTS_INFO_CLASS.WTSUserName, buffer, strLen) AndAlso strLen > 1 Then
            username = Marshal.PtrToStringAnsi(buffer)
            'don 't need length as these are null terminated strings
            WTSFreeMemory(buffer)
            If WTSQuerySessionInformation(IntPtr.Zero, sessionId, WTS_INFO_CLASS.WTSDomainName, buffer, strLen) AndAlso strLen > 1 Then
                username = Marshal.PtrToStringAnsi(buffer) + "\" + username
                'prepend domain name
                WTSFreeMemory(buffer)
            End If
        End If
        Return username
    End Function

    Public Sub LogOffUser(sessionId As Integer)
        'Dim WTS_CURRENT_SESSION As UInt32 = UInt32.MaxValue
        Dim WTS_CURRENT_SERVER_HANDLE As New IntPtr()
        'WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, sessionId, 0)
        WTSLogoffSession(WTS_CURRENT_SERVER_HANDLE, sessionId, 0)
    End Sub


End Class
