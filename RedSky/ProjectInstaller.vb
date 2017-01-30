Imports System.ComponentModel
Imports System.Configuration.Install

Public Class ProjectInstaller

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

    End Sub

    'Automatically start windows service after install
    Protected Overrides Sub OnAfterInstall(savedState As IDictionary)
        MyBase.OnAfterInstall(savedState)

        'The following code starts the services after it is installed.
        Using serviceController As New System.ServiceProcess.ServiceController(ServiceInstaller1.ServiceName)
            serviceController.Start()
        End Using
    End Sub
End Class
