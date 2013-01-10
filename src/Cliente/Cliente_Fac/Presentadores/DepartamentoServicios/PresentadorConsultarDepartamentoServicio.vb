Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DepartamentoServicios
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.DepartamentoServicios
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.DepartamentoServicios
    Class PresentadorConsultarDepartamentoServicio
        Inherits PresentadorBase

        Private _ventana As IConsultarDepartamentoServicio
        Private _DepartamentoServicioServicios As IFacDepartamentoServicioServicios
        Private _departamentoServicios As IDepartamentoServicios
        Private _servicioServicios As IFacServicioServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _DepartamentoServicio As FacDepartamentoServicio

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        ''' <param name="DepartamentoServicio">DepartamentoServicio a mostrar</param>
        Public Sub New(ByVal ventana As IConsultarDepartamentoServicio, ByVal DepartamentoServicio As Object)
            Try
                Me._ventana = ventana
                Me._ventana.DepartamentoServicio = DepartamentoServicio
                _DepartamentoServicio = DirectCast(DepartamentoServicio, FacDepartamentoServicio)
                Me._DepartamentoServicioServicios = DirectCast(Activator.GetObject(GetType(IFacDepartamentoServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DepartamentoServicioServicios")), IFacDepartamentoServicioServicios)
                Me._departamentoServicios = DirectCast(Activator.GetObject(GetType(IDepartamentoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DepartamentoServicios")), IDepartamentoServicios)
                Me._servicioServicios = DirectCast(Activator.GetObject(GetType(IFacServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacServicioServicios")), IFacServicioServicios)

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

            Me.Navegar(New ConsultarDepartamentoServicio(_DepartamentoServicio))
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
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarDepartamentoServicio, Recursos.Ids.fac_ConsultarDepartamentoServicio)

                'Me._ventana.DepartamentoServicio = Me._DepartamentoServicioServicios.ConsultarDepartamentoServicioConTodo(DirectCast(Me._ventana.DepartamentoServicio, DepartamentoServicio))

                Dim departamentoServicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio, FacDepartamentoServicio)

                Dim ids As IList(Of Departamento) = Me._DepartamentoServicios.ConsultarTodos()
                Me._ventana.Ids = ids
                Me._ventana.Id = Me.BuscarDepartamento(ids, departamentoServicio.Id)

                Dim servicios As IList(Of FacServicio) = Me._servicioServicios.ConsultarTodos()
                Me._ventana.Servicios = servicios
                Me._ventana.Servicio = Me.BuscarServicio(servicios, departamentoServicio.Servicio)
                'Me._ventana.Concepto = Me.BuscarConcepto(conceptos, DirectCast(Me._ventana.Justificacion, Justificacion).Concepto)
                Me._ventana.FocoPredeterminado()

                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub

        ''' <summary>
        ''' Método que dependiendo del estado de la página, habilita los campos o 
        ''' modifica los datos del usuario
        ''' </summary>
        Public Sub Modificar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                'Habilitar campos
                If Me._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar Then
                    Me._ventana.HabilitarCampos = True
                    Me._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar
                Else

                    'Modifica los datos del DepartamentoServicio
                    Dim DepartamentoServicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio, FacDepartamentoServicio)
                    'DepartamentoServicio.Region = If(Not Me._ventana.Region.Equals(""), Me._ventana.Region, Nothing)

                    DepartamentoServicio.Id = DirectCast(Me._ventana.Id, Departamento)
                    DepartamentoServicio.Servicio = DirectCast(Me._ventana.Servicio, FacServicio)

                    If Me._DepartamentoServicioServicios.InsertarOModificar(DepartamentoServicio, UsuarioLogeado.Hash) Then
                        '_paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_DepartamentoServicioModificado
                        'Me.Navegar(_paginaPrincipal)
                        MessageBox.Show(Recursos.MensajesConElUsuario.fac_DepartamentoServicioModificado, "Modificado")
                    End If
                End If

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

        Public Sub Eliminar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                If Me._DepartamentoServicioServicios.Eliminar(DirectCast(Me._ventana.DepartamentoServicio, FacDepartamentoServicio), UsuarioLogeado.Hash) Then
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_DepartamentoServicioEliminado
                    Me.Navegar(_paginaPrincipal)
                End If

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
    End Class
End Namespace
