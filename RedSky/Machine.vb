Imports System.Management

Public Class Machine
    Private Shared _classLocker As New [Object]()
    Private Shared _machine As Machine

    Private Sub New()
    End Sub
    ' end private Machine()
    Public Shared Function getInstance() As Machine
        If _machine Is Nothing Then
            SyncLock _classLocker
                If _machine Is Nothing Then
                    _machine = New Machine()
                End If
            End SyncLock
        End If
        Return _machine
    End Function
    ' end public static Machine getInstance()
    Public Function getUsername() As [String]
        Dim username As String = Nothing
        Try
            ' Define WMI scope to look for the Win32_ComputerSystem object
            Dim ms As New ManagementScope("\\.\root\cimv2")
            ms.Connect()

            Dim query As New ObjectQuery("SELECT * FROM Win32_ComputerSystem")
            Dim searcher As New ManagementObjectSearcher(ms, query)

            ' This loop will only run at most once.
            For Each mo As ManagementObject In searcher.[Get]()
                ' Extract the username
                username = mo("UserName").ToString()
            Next
            ' Remove the domain part from the username
            Dim usernameParts As String() = username.Split("\"c)
            ' The username is contained in the last string portion.
            username = usernameParts(usernameParts.Length - 1)
        Catch generatedExceptionName As Exception
            ' The system currently has no users who are logged on
            ' Set the username to "SYSTEM" to denote that
            username = "SYSTEM"
        End Try
        Return username
    End Function
    ' end String getUsername()
End Class
