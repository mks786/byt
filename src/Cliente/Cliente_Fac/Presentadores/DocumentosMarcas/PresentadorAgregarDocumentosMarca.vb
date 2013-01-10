Imports System
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DocumentosMarcas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.DocumentosMarcas
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales


Namespace Presentadores.DocumentosMarcas
    Class PresentadorAgregarDocumentosMarca
        Inherits PresentadorBase
        Private _ventana As IAgregarDocumentosMarca
        Private _DocumentosMarcaServicios As IDocumentosMarcaServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IAgregarDocumentosMarca)
            Try
                Me._ventana = ventana
                'Me._ventana.DocumentosMarca = New DocumentosMarca()
                Me._DocumentosMarcaServicios = DirectCast(Activator.GetObject(GetType(IDocumentosMarcaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DocumentosMarcaServicios")), IDocumentosMarcaServicios)

                Dim DocumentosMarca As New DocumentosMarca()

                DocumentosMarca.Id = ""
                DocumentosMarca.Doc_Esp = ""
                DocumentosMarca.Doc_Ingl = ""


                Me._ventana.DocumentosMarca = DocumentosMarca

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
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarDocumentosMarca, Recursos.Ids.AgregarUsuario)
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

            Me.Navegar(New AgregarDocumentosMarca())
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
                Dim DocumentosMarca As Diginsoft.Bolet.ObjetosComunes.Entidades.DocumentosMarca = DirectCast(_ventana.DocumentosMarca, Diginsoft.Bolet.ObjetosComunes.Entidades.DocumentosMarca)

                If Not _DocumentosMarcaServicios.VerificarExistencia(DocumentosMarca) Then
                    Dim exitoso As Boolean = _DocumentosMarcaServicios.InsertarOModificar(DocumentosMarca, UsuarioLogeado.Hash)

                    If exitoso Then
                        Me.Navegar(Recursos.MensajesConElUsuario.fac_DocumentosMarcaInsertado, False)
                    End If
                Else
                    Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorDocumentosMarcaRepetido)
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



