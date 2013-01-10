Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFactuDetaProformas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Namespace Presentadores.FacFactuDetaProformas
    Class PresentadorConsultarFacFactuDetaProforma
        Inherits PresentadorBase

        Private _ventana As IConsultarFacFactuDetaProforma
        Private _FacFactuDetaProformaServicios As IFacFactuDetaProformaServicios
        Private _AsociadoServicios As IAsociadoServicios
        Private _BancoGServicios As IBancoGServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        ''' <param name="FacFactuDetaProforma">FacFactuDetaProforma a mostrar</param>
        Public Sub New(ByVal ventana As IConsultarFacFactuDetaProforma, ByVal FacFactuDetaProforma As Object)
            Try
                Me._ventana = ventana
                Me._ventana.FacFactuDetaProforma = FacFactuDetaProforma
                'Me._ventana.Region = DirectCast(Me._ventana.FacFactuDetaProforma, FacFactuDetaProforma).Region
                Me._FacFactuDetaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetaProformaServicios")), IFacFactuDetaProformaServicios)
                Me._AsociadoServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._BancoGServicios = DirectCast(Activator.GetObject(GetType(IBancoGServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("BancoGServicios")), IBancoGServicios)
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
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacFactuDetaProforma, Recursos.Ids.fac_ConsultarFacFactuDetaProforma)

                Dim FacFactuDetaProforma As FacFactuDetaProforma = DirectCast(Me._ventana.FacFactuDetaProforma, FacFactuDetaProforma)

                Dim asociados As IList(Of Servicio) = Me._AsociadoServicios.ConsultarTodos()
                Me._ventana.Asociados = asociados
                Me._ventana.Asociado = Me.BuscarAsociado(asociados, FacFactuDetaProforma.Id)

                Dim bancos As IList(Of BancoG) = Me._BancoGServicios.ConsultarTodos()
                Me._ventana.Bancos = Bancos
                'Me._ventana.Banco = Me.BuscarBancoG(bancos, FacFactuDetaProforma.Banco)

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

                    'Modifica los datos del FacFactuDetaProforma
                    Dim FacFactuDetaProforma As FacFactuDetaProforma = DirectCast(Me._ventana.FacFactuDetaProforma, FacFactuDetaProforma)
                    'FacFactuDetaProforma.Region = If(Not Me._ventana.Region.Equals(""), Me._ventana.Region, Nothing)

                    If Me._FacFactuDetaProformaServicios.InsertarOModificar(FacFactuDetaProforma, UsuarioLogeado.Hash) Then
                        _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacFactuDetaProformaModificado
                        Me.Navegar(_paginaPrincipal)
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

                If Me._FacFactuDetaProformaServicios.Eliminar(DirectCast(Me._ventana.FacFactuDetaProforma, FacFactuDetaProforma), UsuarioLogeado.Hash) Then
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacFactuDetaProformaEliminado
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
