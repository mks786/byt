Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class UsuarioServicios
        Inherits MarshalByRefObject
        Implements IUsuarioServicios

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.Usuario) As ObjetosComunes.Entidades.Usuario Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.Usuario).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        Public Function ConsultarTodos() As System.Collections.Generic.IList(Of ObjetosComunes.Entidades.Usuario) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.Usuario).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim usuarios As IList(Of Usuario) = ControladorUsuario.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return usuarios
        End Function

        Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.Usuario, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.Usuario).Eliminar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorUsuario.Eliminar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.Usuario, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.Usuario).InsertarOModificar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorUsuario.InsertarOModificar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.Usuario) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.Usuario).VerificarExistencia
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorUsuario.VerificarExistencia(entidad)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        Public Function Autenticar(ByVal usuario As ObjetosComunes.Entidades.Usuario) As ObjetosComunes.Entidades.Usuario Implements ObjetosComunes.ContratosServicios.IUsuarioServicios.Autenticar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim usuarioAutenticado As Usuario = ControladorUsuario.Autenticar(usuario)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return usuarioAutenticado
        End Function

        Public Sub CerrarSession(ByVal hash As Integer) Implements ObjetosComunes.ContratosServicios.IUsuarioServicios.CerrarSession
            ControladorUsuario.CerrarSesion(hash)
        End Sub
    End Class
End Namespace