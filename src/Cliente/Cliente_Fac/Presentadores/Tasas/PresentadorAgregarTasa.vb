Imports System
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.Tasas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Tasas
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.Tasas
    Class PresentadorAgregartasa
        Inherits PresentadorBase
        Private _ventana As IAgregarTasa
        Private _tasaServicios As ITasaServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IAgregarTasa)
            Try
                Me._ventana = ventana
                'Me._ventana.Tasa = New Tasa()
                Me._tasaServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)

                Dim tasa As New Tasa()

                tasa.Id = FormatDateTime(System.DateTime.Now, DateFormat.ShortDate)
                tasa.Moneda = ""
                tasa.Tasabf = 0
                tasa.Tasabs = 0
                Me._ventana.Tasa = tasa

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

            Me.Navegar(New AgregarTasa())
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
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarTasa, Recursos.Ids.AgregarUsuario)
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
                Dim tasa As Diginsoft.Bolet.ObjetosComunes.Entidades.Tasa = DirectCast(_ventana.Tasa, Diginsoft.Bolet.ObjetosComunes.Entidades.Tasa)
                If (tasa.Moneda = "Bolivares") Then
                    tasa.Moneda = "BS"
                End If
                If (tasa.Moneda = "Bolivares Fuertes") Then
                    tasa.Moneda = "BF"
                End If
                If (tasa.Moneda = "US Dolares") Then
                    tasa.Moneda = "US"
                End If

                'If Not _tasaServicios.VerificarExistencia(tasa) Then
                Dim exitoso As Boolean = _tasaServicios.InsertarOModificar(tasa, UsuarioLogeado.Hash)

                If exitoso Then
                    tasa.Id = tasa.Id.AddDays(1)

                    If (tasa.Moneda = "BS") Then
                        Me._ventana.Moneda = "Bolivares"
                    End If
                    If (tasa.Moneda = "BF") Then
                        Me._ventana.Moneda = "Bolivares Fuertes"
                    End If
                    If (tasa.Moneda = "US") Then
                        Me._ventana.Moneda = "US Dolares"
                    End If



                    Me._ventana.Tasa = Nothing
                    Me._ventana.Tasa = tasa

                    ' Me.Navegar(Recursos.MensajesConElUsuario.fac_TipoClaseInsertado, False)
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
    End Class
End Namespace