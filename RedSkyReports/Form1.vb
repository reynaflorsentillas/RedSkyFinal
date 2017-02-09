
Imports System.Runtime.InteropServices

Public Class Form1

    Private Enum WTS_CONNECTSTATE_CLASS
        WTSActive
        WTSConnected
        WTSConnectQuery
        WTSShadow
        WTSDisconnected
        WTSIdle
        WTSListen
        WTSReset
        WTSDown
        WTSInit
    End Enum

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Private Structure WTS_SESSION_INFO
        Dim SessionID As Int32 'DWORD integer
        Dim pWinStationName As String ' integer LPTSTR - Pointer to a null-terminated string containing the name of the WinStation for this session
        Dim State As WTS_CONNECTSTATE_CLASS
    End Structure

    Friend Structure strSessionsInfo
        Dim SessionID As Integer
        Dim StationName As String
        Dim ConnectionState As String
    End Structure

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
    'Structure for TS Client IP Address
    <StructLayout(LayoutKind.Sequential)>
    Private Structure _WTS_CLIENT_ADDRESS
        Public AddressFamily As Integer
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=20)>
        Public Address As Byte()
    End Structure
    'Structure for TS Client Information
    Friend Structure WTS_CLIENT_INFO
        Public WTSStatus As Boolean
        Public WTSUserName As String
        Public WTSStationName As String
        Public WTSDomainName As String
        Public WTSClientName As String
        Public AddressFamily As Integer
        Public Address As Byte()
    End Structure

    'Function for TS Session Information excluding Client IP address
    Private Declare Function WTSQuerySessionInformation Lib "WtsApi32.dll" Alias "WTSQuerySessionInformationW" (ByVal hServer As Int32,
    ByVal SessionId As Int32, ByVal WTSInfoClass As Int32, <MarshalAs(UnmanagedType.LPWStr)> ByRef ppBuffer As String, ByRef pCount As Int32) As Boolean

    'Function for TS Client IP Address
    Private Declare Function WTSQuerySessionInformation2 Lib "WtsApi32.dll" Alias "WTSQuerySessionInformationW" (ByVal hServer As Int32,
      ByVal SessionId As Int32, ByVal WTSInfoClass As Int32, ByRef ppBuffer As IntPtr, ByRef pCount As Int32) As Boolean

    Private Declare Function GetCurrentProcessId Lib "Kernel32.dll" Alias "GetCurrentProcessId" () As Int32
    Private Declare Function ProcessIdToSessionId Lib "Kernel32.dll" Alias "ProcessIdToSessionId" (ByVal processID As Int32, ByRef sessionID As Int32) As Boolean
    Private Declare Function WTSGetActiveConsoleSessionId Lib "Kernel32.dll" Alias "WTSGetActiveConsoleSessionId" () As Int32


    <DllImport("wtsapi32.dll",
    BestFitMapping:=True,
    CallingConvention:=CallingConvention.StdCall,
    CharSet:=CharSet.Auto,
    EntryPoint:="WTSEnumerateSessions",
    SetLastError:=True,
    ThrowOnUnmappableChar:=True)>
    Private Shared Function WTSEnumerateSessions(
    ByVal hServer As IntPtr,
    <MarshalAs(UnmanagedType.U4)>
    ByVal Reserved As Int32,
    <MarshalAs(UnmanagedType.U4)>
    ByVal Vesrion As Int32,
    ByRef ppSessionInfo As IntPtr,
    <MarshalAs(UnmanagedType.U4)>
    ByRef pCount As Int32) As Int32
    End Function

    <DllImport("wtsapi32.dll")>
    Private Shared Sub WTSFreeMemory(ByVal pMemory As IntPtr)
    End Sub

    <DllImport("wtsapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function WTSOpenServer(ByVal pServerName As String) As IntPtr
    End Function

    <DllImport("wtsapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Sub WTSCloseServer(ByVal hServer As IntPtr)
    End Sub

    Friend Function GetSessions(ByVal ServerName As String, ByRef ClientInfo As WTS_CLIENT_INFO) As Boolean
        Dim ptrOpenedServer As IntPtr
        Try
            ptrOpenedServer = WTSOpenServer(ServerName)
            If ptrOpenedServer = vbNull Then
                MessageBox.Show("Terminal Services not running on : " & ServerName)
                GetSessions = False
                Exit Function
            End If
            Dim FRetVal As Int32
            Dim ppSessionInfo As IntPtr = IntPtr.Zero
            Dim Count As Int32 = 0
            Try
                FRetVal = WTSEnumerateSessions(ptrOpenedServer, 0, 1, ppSessionInfo, Count)
                If FRetVal <> 0 Then
                    Dim sessionInfo() As WTS_SESSION_INFO = New WTS_SESSION_INFO(Count) {}
                    Dim i As Integer
                    Dim session_ptr As System.IntPtr
                    For i = 0 To Count - 1
                        session_ptr = ppSessionInfo.ToInt32() + (i * Marshal.SizeOf(sessionInfo(i)))
                        sessionInfo(i) = CType(Marshal.PtrToStructure(session_ptr, GetType(WTS_SESSION_INFO)), WTS_SESSION_INFO)
                    Next
                    WTSFreeMemory(ppSessionInfo)
                    Dim tmpArr(sessionInfo.GetUpperBound(0)) As strSessionsInfo
                    For i = 0 To tmpArr.GetUpperBound(0)
                        tmpArr(i).SessionID = sessionInfo(i).SessionID
                        tmpArr(i).StationName = sessionInfo(i).pWinStationName
                        tmpArr(i).ConnectionState = GetConnectionState(sessionInfo(i).State)
                        'MessageBox.Show(tmpArr(i).StationName & "  " & tmpArr(i).SessionID & "  " & tmpArr(i).ConnectionState)
                    Next
                    ReDim sessionInfo(-1)
                Else
                    Throw New ApplicationException("No data retruned")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message & vbCrLf & System.Runtime.InteropServices.Marshal.GetLastWin32Error)
            End Try
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Exit Function
        Finally
        End Try
        'Get ProcessID of TS Session that executed this TS Session
        Dim active_process As Int32 = GetCurrentProcessId()
        Dim active_session As Int32 = 0
        Dim success1 As Boolean = ProcessIdToSessionId(active_process, active_session)
        If success1 = False Then
            MessageBox.Show("Error: ProcessIdToSessionId")
        End If
        Dim returned As Integer
        Dim str As String = ""
        Dim success As Boolean = False
        ClientInfo.WTSStationName = ""
        ClientInfo.WTSClientName = ""
        ClientInfo.Address(2) = 0
        ClientInfo.Address(3) = 0
        ClientInfo.Address(4) = 0
        ClientInfo.Address(5) = 0

        'Get User Name of this TS session
        If WTSQuerySessionInformation(ptrOpenedServer, active_session, WTS_INFO_CLASS.WTSUserName, str, returned) = True Then
            ClientInfo.WTSUserName = str
        End If

        'Get StationName of this TS session
        If WTSQuerySessionInformation(ptrOpenedServer, active_session, WTS_INFO_CLASS.WTSWinStationName, str, returned) = True Then
            ClientInfo.WTSStationName = str
        End If

        'Get Domain Name of this TS session
        If WTSQuerySessionInformation(ptrOpenedServer, active_session, WTS_INFO_CLASS.WTSDomainName, str, returned) = True Then
            ClientInfo.WTSDomainName = str
        End If

        'Skip client name and client address if this is a console session
        If ClientInfo.WTSStationName <> "Console" Then
            If WTSQuerySessionInformation(ptrOpenedServer, active_session, WTS_INFO_CLASS.WTSClientName, str, returned) = True Then
                ClientInfo.WTSClientName = str
            End If

            'Get client IP address
            Dim addr As IntPtr
            If WTSQuerySessionInformation2(ptrOpenedServer, active_session, WTS_INFO_CLASS.WTSClientAddress, addr, returned) = True Then
                Dim obj As New _WTS_CLIENT_ADDRESS()
                obj = CType(Marshal.PtrToStructure(addr, obj.GetType()), _WTS_CLIENT_ADDRESS)
                ClientInfo.Address(2) = obj.Address(2)
                ClientInfo.Address(3) = obj.Address(3)
                ClientInfo.Address(4) = obj.Address(4)
                ClientInfo.Address(5) = obj.Address(5)
            End If
        End If
        WTSCloseServer(ptrOpenedServer)
        Return True
    End Function

    Private Function GetConnectionState(ByVal State As WTS_CONNECTSTATE_CLASS) As String
        Dim RetVal As String
        Select Case State
            Case WTS_CONNECTSTATE_CLASS.WTSActive
                RetVal = "Active"
            Case WTS_CONNECTSTATE_CLASS.WTSConnected
                RetVal = "Connected"
            Case WTS_CONNECTSTATE_CLASS.WTSConnectQuery
                RetVal = "Query"
            Case WTS_CONNECTSTATE_CLASS.WTSDisconnected
                RetVal = "Disconnected"
            Case WTS_CONNECTSTATE_CLASS.WTSDown
                RetVal = "Down"
            Case WTS_CONNECTSTATE_CLASS.WTSIdle
                RetVal = "Idle"
            Case WTS_CONNECTSTATE_CLASS.WTSInit
                RetVal = "Initializing."
            Case WTS_CONNECTSTATE_CLASS.WTSListen
                RetVal = "Listen"
            Case WTS_CONNECTSTATE_CLASS.WTSReset
                RetVal = "reset"
            Case WTS_CONNECTSTATE_CLASS.WTSShadow
                RetVal = "Shadowing"
            Case Else
                RetVal = "Unknown connect state"
        End Select
        Return RetVal
    End Function

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim serverName As String
        Dim clientInfo As New WTS_CLIENT_INFO
        ReDim clientInfo.Address(20)
        serverName = ""
        'Server Name can be name of choice or name of server on which this application is running
        If GetSessions(serverName, clientInfo) = True Then
            Dim str As String
            str = "User Name: " & clientInfo.WTSUserName
            str &= vbNewLine & "Station Name: " & clientInfo.WTSStationName
            str &= vbNewLine & "Domain Name: " & clientInfo.WTSDomainName
            If clientInfo.WTSStationName <> "Console" Then
                str &= vbNewLine & "Client Name: " & clientInfo.WTSClientName
                str &= vbNewLine & "Client IP: " & clientInfo.Address(2) & "." & clientInfo.Address(3) & "." & clientInfo.Address(4) & "." & clientInfo.Address(5)
            End If
            MessageBox.Show(str)
        End If
    End Sub

End Class