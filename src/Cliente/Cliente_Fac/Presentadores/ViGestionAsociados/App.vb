Imports System.Collections.Generic
Imports System.Runtime.Remoting
Imports System.Windows
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

'Namespa
Class App
    Inherits Application
    Private Const _ventanaInicial As String = "Ventanas/Logines/Login.xaml"
    Private Const _estiloPrincipal As String = "Estilos/EstiloPrincipal.xaml"
    Private Const _archivo As String = "Diginsoft.Bolet.Cliente.Fac.exe.config"

    <STAThread()> _
    Public Shared Sub Main()
        RemotingConfiguration.Configure(_archivo, False)
        'App.
        Dim app As New App()
        app.StartupUri = New Uri(_ventanaInicial, UriKind.Relative)
        Dim estilo As New ResourceDictionary()
        estilo.Source = New Uri(_estiloPrincipal, UriKind.Relative)
        app.Resources.MergedDictionaries.Add(estilo)
        app.Run()
    End Sub
End Class
'End Namespace