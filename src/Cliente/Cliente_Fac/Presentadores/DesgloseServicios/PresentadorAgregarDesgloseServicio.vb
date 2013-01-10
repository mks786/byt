Imports System
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DesgloseServicios
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.DesgloseServicios
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales


Namespace Presentadores.DesgloseServicios
    Class PresentadorAgregarDesgloseServicio
        Inherits PresentadorBase
        Private _ventana As IAgregarDesgloseServicio
        Private _DesgloseServicioServicios As IFacDesgloseServicioServicios
        Private _servicioServicios As IFacServicioServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IAgregarDesgloseServicio)
            Try
                Me._ventana = ventana
                'Me._ventana.DesgloseServicio = New DesgloseServicio()
                Me._DesgloseServicioServicios = DirectCast(Activator.GetObject(GetType(IFacDesgloseServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DesgloseServicioServicios")), IFacDesgloseServicioServicios)
                Me._servicioServicios = DirectCast(Activator.GetObject(GetType(IFacServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacServicioServicios")), IFacServicioServicios)

                Dim DesgloseServicio As New FacDesgloseServicio()
                Me._ventana.DesgloseServicio = DesgloseServicio

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        ''' <summary>
        ''' Método que carga los datos iniciales a mostrar en la página
        ''' </summary>
        Public Sub CargarPagina()
            Mouse.OverrideCursor = Cursors.Wait

            Try
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarDesgloseServicio, Recursos.Ids.AgregarUsuario)

                Me._ventana.Servicios = Me._servicioServicios.ConsultarTodos()

                Me._ventana.FocoPredeterminado()
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
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub

        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New AgregarDesgloseServicio())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        ''' <summary>
        ''' Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        ''' </summary>
        Public Sub Aceptar()
            Try
                Dim DesgloseServicio As FacDesgloseServicio = DirectCast(_ventana.DesgloseServicio, FacDesgloseServicio)
                DesgloseServicio.Servicio = DirectCast(Me._ventana.Servicio, FacServicio)
                DesgloseServicio.Id = Me._ventana.TipoDesgSer
                'If Not _DesgloseServicioServicios.VerificarExistencia(DesgloseServicio) Then
                Dim exitoso As Boolean = _DesgloseServicioServicios.InsertarOModificar(DesgloseServicio, UsuarioLogeado.Hash)

                If exitoso Then
                    Me.Navegar(Recursos.MensajesConElUsuario.fac_DesgloseServicioInsertado, False)
                End If
                'Else
                'Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorDesgloseServicioRepetido)
                'End If
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
    End Class
End Namespace



