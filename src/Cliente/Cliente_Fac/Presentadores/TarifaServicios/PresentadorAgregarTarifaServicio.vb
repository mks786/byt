Imports System
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
    Class PresentadorAgregarTarifaServicio
        Inherits PresentadorBase
        Private _ventana As IAgregarTarifaServicio
        Private _TarifaServicioServicios As ITarifaServicioServicios
        Private _tarifaServicios As ITarifaServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IAgregarTarifaServicio)
            Try
                Me._ventana = ventana
                'Me._ventana.TarifaServicio = New TarifaServicio()
                Me._TarifaServicioServicios = DirectCast(Activator.GetObject(GetType(ITarifaServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TarifaServicioServicios")), ITarifaServicioServicios)
                Me._tarifaServicios = DirectCast(Activator.GetObject(GetType(ITarifaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TarifaServicios")), ITarifaServicios)

                Dim TarifaServicio As New TarifaServicio()
                Me._ventana.TarifaServicio = TarifaServicio

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New AgregarTarifaServicio())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        ''' <summary>
        ''' Método que carga los datos iniciales a mostrar en la página
        ''' </summary>
        Public Sub CargarPagina()
            Mouse.OverrideCursor = Cursors.Wait

            Try
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarTarifaServicio, Recursos.Ids.AgregarUsuario)

                Me._ventana.Tarifas = Me._tarifaServicios.ConsultarTodos()

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

        ''' <summary>
        ''' Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        ''' </summary>
        Public Sub Aceptar()
            Try
                Dim TarifaServicio As Diginsoft.Bolet.ObjetosComunes.Entidades.TarifaServicio = DirectCast(_ventana.TarifaServicio, Diginsoft.Bolet.ObjetosComunes.Entidades.TarifaServicio)

                'esto es devido a que tarifas esta en el proyecto de Trascend  y en el mapeo no puedo
                Dim tarifa As Tarifa = DirectCast(Me._ventana.Tarifa, Tarifa)
                Dim tarifa2 As New Tarifa2
                tarifa2.Descripcion = tarifa.Descripcion
                tarifa2.Id = tarifa.Id
                TarifaServicio.Tarifa = DirectCast(tarifa2, Tarifa2)

                'If Not _TarifaServicioServicios.VerificarExistencia(TarifaServicio) Then
                Dim exitoso As Boolean = _TarifaServicioServicios.InsertarOModificar(TarifaServicio, UsuarioLogeado.Hash)

                If exitoso Then
                    Me.Navegar(Recursos.MensajesConElUsuario.fac_TarifaServicioInsertado, False)
                End If
                'Else
                'Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorTarifaServicioRepetido)
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



