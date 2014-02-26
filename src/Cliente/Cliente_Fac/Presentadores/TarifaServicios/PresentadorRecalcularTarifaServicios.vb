Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.TarifaServicios
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.TarifaServicios
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.TarifaServicios
    Public Class PresentadorRecalcularTarifaServicios
        Inherits PresentadorBase

        Private _ventana As IRecalcularTarifaServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _TarifaServicioServicios As ITarifaServicioServicios
        Private _tarifasServicio As List(Of TarifaServicio)

        ''' Constructor por defecto que recibe una ventana actual y una lista de Tarifa Servicios a recalcular
        Public Sub New(ByVal ventana As IRecalcularTarifaServicios, ByVal listaTarifaServicios As Object)
            Try
                Me._ventana = ventana
                Me._tarifasServicio = DirectCast(listaTarifaServicios, List(Of TarifaServicio))

                Me._TarifaServicioServicios =
                    DirectCast(Activator.GetObject(GetType(ITarifaServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TarifaServicioServicios")), ITarifaServicioServicios)

                CargarPagina()

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        

        ''' Metodo que carga los valores iniciales de la ventana
        Private Sub CargarPagina()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                'Me._ventana.TasaCambio = "0,00"

                Me._ventana.FocoPredeterminado()

                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Me.Navegar(ex.Message, True)
            Catch ex As RemotingException
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, True)
            Catch ex As SocketException
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, True)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try

        End Sub

        ''' Metodo que recalcula los Precios de acuerdo a la tasa ingresada por el usuario a la ventana
        Public Sub RecalcularTarifas()

            Dim tasaCambio As Double = Double.Parse(Me._ventana.TasaCambio)
            Dim exitoso As Boolean
            exitoso = False

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                For Each tarifa As TarifaServicio In Me._tarifasServicio

                    tarifa.Tasa = tasaCambio
                    tarifa.Mont_Bf = tarifa.Mont_Us * (tasaCambio / 1000)
                    tarifa.Mont_Bs = tarifa.Mont_Us * tasaCambio
                    exitoso = Me._TarifaServicioServicios.InsertarOModificar(tarifa, UsuarioLogeado.Hash)
                Next

                If (exitoso) Then
                    Me._ventana.Mensaje("Cálculo de Tarifa(s) completado", 2)
                End If

                Me.Navegar(New ConsultarTarifaServicios())


                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, True)
            End Try
        End Sub



    End Class
End Namespace

