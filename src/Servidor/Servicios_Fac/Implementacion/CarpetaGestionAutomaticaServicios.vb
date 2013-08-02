Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class CarpetaGestionAutomaticaServicios
        Inherits MarshalByRefObject
        Implements ICarpetaGestionAutomaticaServicios


        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerCarpetasPorIniciales(ByVal Usuario As Usuario) As System.Collections.Generic.IList(Of CarpetaGestionAutomatica) Implements ObjetosComunes.ContratosServicios.ICarpetaGestionAutomaticaServicios.ObtenerCarpetasPorIniciales
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim carpetas As IList(Of CarpetaGestionAutomatica)

            carpetas = ControladorCarpetaGestionAutomatica.ObtenerCarpetasPorIniciales(Usuario)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return carpetas

        End Function

        Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.CarpetaGestionAutomatica) As ObjetosComunes.Entidades.CarpetaGestionAutomatica Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.CarpetaGestionAutomatica).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        Public Function ConsultarTodos() As System.Collections.Generic.IList(Of ObjetosComunes.Entidades.CarpetaGestionAutomatica) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.CarpetaGestionAutomatica).ConsultarTodos
            Throw New NotImplementedException()
        End Function

        Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.CarpetaGestionAutomatica, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.CarpetaGestionAutomatica).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.CarpetaGestionAutomatica, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.CarpetaGestionAutomatica).InsertarOModificar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.CarpetaGestionAutomatica) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.CarpetaGestionAutomatica).VerificarExistencia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace

