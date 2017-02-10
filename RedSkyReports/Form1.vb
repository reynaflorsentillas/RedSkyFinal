Imports System.DirectoryServices

Public Class Form1
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'For Each group As String In GetGroups()
        '    MsgBox(group)
        'Next

        Dim userName As String = "reynaflor.sentillas"

        Dim dirEntry As New DirectoryEntry("LDAP://CAPSTONE")
        Dim dirSearcher As New DirectorySearcher(dirEntry)
        dirSearcher.SearchScope = SearchScope.Subtree
        dirSearcher.Filter = String.Format("(&(objectClass=user)(|(cn={0})(sn={0}*)(givenName={0})(sAMAccountName={0}*)))", userName)
        'dirSearcher.Filter = "(&(ObjectClass=group))"
        'dirSearcher.Filter = "(&(objectCategory=person)(objectClass=user))"
        'dirSearcher.Filter = "(objectCategory=organizationalUnit)"
        'dirSearcher.Filter = "(groupType:1.2.840.113556.1.4.803:=4)"
        Dim searchResults = dirSearcher.FindAll()

        For Each sr As SearchResult In searchResults
            Dim de = sr.GetDirectoryEntry()
            'Dim user As String = de.Properties("distinguishedName")(0).ToString()
            Dim user As String = de.Properties("distinguishedName")(0).ToString()
            'Dim domain As String = de.Path.ToString().Split(New String() {",DC="}, StringSplitOptions.None)(1)
            'Dim group As String = de.Path.ToString().Split(New String() {",OU="}, StringSplitOptions.None)(1)
            MsgBox(de.Path.ToString)
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
                    MsgBox(group)
                    count += 1
                End If
            Next
            MsgBox(groups)

        Next
    End Sub



End Class