Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades


Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports NLog
Imports Trascend.Bolet.Cliente.Contratos.Cartas
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacGestiones


Namespace Ventanas.FacGestiones
    Partial Public Class Mostrar_Detalle_Carta
        Inherits Window
        Private _presentador As PresentadorAgregarFacGestion

        Public Sub New(ByVal carta As Object)
            InitializeComponent()
            carta = DirectCast(carta, Carta)
            Me._gridDatos.DataContext = DirectCast(carta, Carta)
        End Sub


        Public Sub AbrirCorrespondencia()
            Dim carta As Carta = Me._gridDatos.DataContext

            Dim rutaEntrada As String = ConfigurationManager.AppSettings("RutaVerEntrada")
            Dim comando As String = "START " & rutaEntrada
            Dim anoCorrespondencia As Integer = Carta.Fecha.Year
            Dim mesCorrespondencia As String = If(Carta.Fecha.Month.ToString().Length = 2, Carta.Fecha.Month.ToString(), "0" & Carta.Fecha.Month.ToString)



            Dim diaCorrespondencia As String = If(Carta.Fecha.Day.ToString().Length = 2, Carta.Fecha.Day.ToString(), "0" & Carta.Fecha.Day.ToString())
            Dim codigoEntrada As Integer = Carta.Id
            Dim comandoRutaCorrespondencia As String = rutaEntrada & anoCorrespondencia & "\" & mesCorrespondencia & "\" & anoCorrespondencia & mesCorrespondencia & diaCorrespondencia & "\" & codigoEntrada

            Dim ruta1 = ConfigurationManager.AppSettings("RutaBatCorrespondencia").ToString()
            Dim ruta2 = ConfigurationManager.AppSettings("ArchivoBatCorrespondencia").ToString()
            Dim ruta3 = ConfigurationManager.AppSettings("RutaBatCorrespondencia").ToString()
            Dim ruta4 = comandoRutaCorrespondencia & ".TIF" & " " & ConfigurationManager.AppSettings("RutaBatCorrespondencia").ToString() & comandoRutaCorrespondencia & ".MSG"
            Dim campo1 = ruta1 & ruta2
            Dim campo2 = ruta3 & ruta4
            EjecutarArchivoBAT2(campo1, campo2)
            '_presentador.EjecutarArchivoBAT(ConfigurationManager.AppSettings("RutaBatCorrespondencia").ToString() + ConfigurationManager.AppSettings("ArchivoBatCorrespondencia").ToString(), ConfigurationManager.AppSettings("RutaBatCorrespondencia").ToString() & comandoRutaCorrespondencia & ".TIF" & " " & ConfigurationManager.AppSettings("RutaBatCorrespondencia").ToString() & comandoRutaCorrespondencia & ".MSG")
        End Sub

        Private Sub _btnVerCorrespondencia_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
            AbrirCorrespondencia()
        End Sub

        Public Sub EjecutarArchivoBAT2(ByVal nombreArchivoBat As String, ByVal parametroA As String)
            Try
                Dim proc As New System.Diagnostics.Process()
                ' Declare New Process
                proc.StartInfo.FileName = nombreArchivoBat
                proc.StartInfo.RedirectStandardError = True
                proc.StartInfo.CreateNoWindow = True
                proc.StartInfo.RedirectStandardOutput = True
                proc.StartInfo.UseShellExecute = False
                proc.StartInfo.Arguments = parametroA

                proc.Start()
                proc.WaitForExit(2000)

                Dim errorMessage As String = proc.StandardError.ReadToEnd()
                proc.WaitForExit()

                Dim outputMessage As String = proc.StandardOutput.ReadToEnd()
                proc.WaitForExit()
            Catch ex As Exception
                Throw New ApplicationException()
            End Try
        End Sub


    End Class
End Namespace