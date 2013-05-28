Imports System
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.NumeroControl
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.NumeroControl
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.NumeroControl
    Class PresentadorNumeroControl
        Inherits PresentadorBase
        Private _ventana As INumeroControl
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _contadorfacServicios As IContadorFacServicios
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As INumeroControl)
            Try
                Me._ventana = ventana
                'Me._ventana.Tasa = New Tasa()
                Me._contadorfacServicios = DirectCast(Activator.GetObject(GetType(IContadorFacServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ContadorFacServicios")), IContadorFacServicios)
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

            ' Me.Navegar(New AgregarTasa())
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
            buscar_contador()
            Try
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleNumeroControl, Recursos.Ids.AgregarUsuario)
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

                If IsNumeric(Me._ventana.NumeroControl) Then
                    Dim exitoso As Boolean = actualizar_contador()
                    If exitoso Then


                        Me.Navegar(Recursos.MensajesConElUsuario.fac_NumeroControl, False)
                    End If
                Else
                    MessageBox.Show("Formato del Numero de Control Incorrecto", "Error")
                End If
                'Else
                'Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorTasaRepetida)
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

        Public Sub buscar_contador()
            'para el contador
            Dim contador As New ContadorFac
            contador.Id = "FAC_CONTROL"
            contador = _contadorfacServicios.ConsultarPorId(contador)
            Me._ventana.NumeroControl = contador.ProximoValor
            'contador.ProximoValor = System.Math.Max(System.Threading.Interlocked.Increment(contador.ProximoValor), contador.ProximoValor - 1)
            'Dim exitocontador As Boolean = _contadorfacServicios.InsertarOModificar(contador, UsuarioLogeado.Hash)
        End Sub

        Public Function actualizar_contador() As Boolean
            'para el contador
            Dim contador As New ContadorFac
            contador.Id = "FAC_CONTROL"
            contador = _contadorfacServicios.ConsultarPorId(contador)
            contador.ProximoValor = Me._ventana.NumeroControl
            'contador.ProximoValor = System.Math.Max(System.Threading.Interlocked.Increment(contador.ProximoValor), contador.ProximoValor - 1)
            Dim exitocontador As Boolean = _contadorfacServicios.InsertarOModificar(contador, UsuarioLogeado.Hash)
            Return exitocontador
        End Function
    End Class
End Namespace