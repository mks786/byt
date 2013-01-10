Imports System
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.Correspondencias
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales


Namespace Presentadores.Correspondencias
    Class PresentadorAgregarCorrespondencia
        Inherits PresentadorBase
        Private _ventana As IAgregarCorrespondencia
        Private _CorrespondenciaServicios As ICorrespondenciaServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IAgregarCorrespondencia)
            Try
                Me._ventana = ventana
                'Me._ventana.Correspondencia = New Correspondencia()
                Me._CorrespondenciaServicios = DirectCast(Activator.GetObject(GetType(ICorrespondenciaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("CorrespondenciaServicios")), ICorrespondenciaServicios)

                Dim Correspondencia As New Correspondencia()

                Me._ventana.Correspondencia = Correspondencia

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
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarCorrespondencia, Recursos.Ids.AgregarUsuario)
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
                Dim Correspondencia As Diginsoft.Bolet.ObjetosComunes.Entidades.Correspondencia = DirectCast(_ventana.Correspondencia, Diginsoft.Bolet.ObjetosComunes.Entidades.Correspondencia)

                If Not _CorrespondenciaServicios.VerificarExistencia(Correspondencia) Then
                    Dim exitoso As Boolean = _CorrespondenciaServicios.InsertarOModificar(Correspondencia, UsuarioLogeado.Hash)

                    If exitoso Then
                        Me.Navegar(Recursos.MensajesConElUsuario.fac_CorrespondenciaInsertado, False)
                    End If
                Else
                    Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorCorrespondenciaRepetido)
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



