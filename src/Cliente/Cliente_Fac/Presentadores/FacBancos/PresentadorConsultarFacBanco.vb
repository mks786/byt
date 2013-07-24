Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacBancos
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacBancos
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios

Namespace Presentadores.FacBancos
    Class PresentadorConsultarFacBanco
        Inherits PresentadorBase

        Private _ventana As IConsultarFacBanco
        Private _FacBancoServicios As IFacBancoServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacBanco As FacBanco
        Private _monedasServicios As IMonedaServicios
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        ''' <param name="FacBanco">FacBanco a mostrar</param>
        Public Sub New(ByVal ventana As IConsultarFacBanco, ByVal FacBanco As Object)
            Try
                Me._ventana = ventana
                Me._ventana.FacBanco = FacBanco
                _FacBanco = DirectCast(FacBanco, FacBanco)
                'Me._ventana.Region = DirectCast(Me._ventana.FacBanco, FacBanco).Region
                Me._ventana.HabilitarCampos = False
                Me._FacBancoServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
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

            Me.Navegar(New ConsultarFacBanco(_FacBanco))
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

                Dim monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                Me._ventana.Monedas = monedas
                Dim monedanuevo As New Moneda
                If _FacBanco.Moneda <> "" And _FacBanco.Moneda IsNot Nothing Then
                    monedanuevo.Id = _FacBanco.Moneda
                    Me._ventana.Moneda = Me.BuscarMoneda(monedas, monedanuevo)
                End If

                If _FacBanco.Iw = "S" Then
                    _ventana.Publica = "SI"
                Else
                    _ventana.Publica = "NO"
                End If

                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacBanco, Recursos.Ids.fac_ConsultarFacBanco)
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

                    'Modifica los datos del FacBanco
                    Dim FacBanco As FacBanco = DirectCast(Me._ventana.FacBanco, FacBanco)
                    'FacBanco.Region = If(Not Me._ventana.Region.Equals(""), Me._ventana.Region, Nothing)

                    Dim moneda As Moneda = DirectCast(_ventana.Moneda, Moneda)
                    If moneda IsNot Nothing Then
                        If moneda.Id <> "" Then
                            FacBanco.Moneda = moneda.Id
                        End If
                    End If

                    Dim publica As String = _ventana.Publica
                    If publica = "SI" Then
                        FacBanco.Iw = "S"
                    Else
                        FacBanco.Iw = "N"
                    End If

                    If Me._FacBancoServicios.InsertarOModificar(FacBanco, UsuarioLogeado.Hash) Then
                        '_paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacBancoModificado
                        'Me.Navegar(_paginaPrincipal)
                        MessageBox.Show(Recursos.MensajesConElUsuario.fac_FacBancoModificado, "Modificado")
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

                If Me._FacBancoServicios.Eliminar(DirectCast(Me._ventana.FacBanco, FacBanco), UsuarioLogeado.Hash) Then
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacBancoEliminado
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

