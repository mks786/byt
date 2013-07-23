Imports System
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
    Class PresentadorAgregarFacBanco
        Inherits PresentadorBase
        Private _ventana As IAgregarFacBanco
        Private _FacBancoServicios As IFacBancoServicios
        Private _monedasServicios As IMonedaServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IAgregarFacBanco)
            Try
                Me._ventana = ventana
                'Me._ventana.FacBanco = New FacBanco()
                Me._FacBancoServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
                Dim FacBanco As New FacBanco()


                FacBanco.FechaSaldo = FormatDateTime(Date.Now, DateFormat.ShortDate)
                Me._ventana.FacBanco = FacBanco

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

            Me.Navegar(New AgregarFacBanco())
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


            Dim Monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
            Dim primeramoneda As New Moneda()
            primeramoneda.Id = ""
            Monedas.Insert(0, primeramoneda)
            Me._ventana.Monedas = Monedas


            Try
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarFacBanco, Recursos.Ids.AgregarUsuario)
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
                Dim FacBanco As FacBanco = DirectCast(_ventana.FacBanco, FacBanco)

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

                'If Not _FacBancoServicios.VerificarExistencia(FacBanco) Then
                Dim exitoso As Boolean = _FacBancoServicios.InsertarOModificar(FacBanco, UsuarioLogeado.Hash)

                If exitoso Then
                    Me.Navegar(Recursos.MensajesConElUsuario.fac_FacBancoInsertado, False)
                End If
                'Else
                '    Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorFacBancoRepetido)
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
