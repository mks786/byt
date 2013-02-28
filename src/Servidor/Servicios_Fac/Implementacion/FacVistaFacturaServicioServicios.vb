Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacVistaFacturaServicioServicios
        Inherits MarshalByRefObject
        Implements IFacVistaFacturaServicioServicios



        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los FacVistaFacturaServicioes
        ''' </summary>
        ''' <returns>Todos los FacVistaFacturaServicioes</returns>
        Public Function ConsultarTodos() As IList(Of FacVistaFacturaServicio) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacVistaFacturaServicio).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacVistaFacturaServicios As IList(Of FacVistaFacturaServicio) = ControladorFacVistaFacturaServicio.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacVistaFacturaServicios
        End Function

        Public Function ObteneFacVistaFacturaServiciosFiltro(ByVal FacVistaFacturaServicio As FacVistaFacturaServicio) As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturaServicio) Implements ObjetosComunes.ContratosServicios.IFacVistaFacturaServicioServicios.ObtenerFacVistaFacturaServiciosFiltro
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacVistaFacturaServicios As IList(Of FacVistaFacturaServicio)

            FacVistaFacturaServicios = ControladorFacVistaFacturaServicio.ConsultarFacVistaFacturaServiciosFiltro(FacVistaFacturaServicio)

            Return FacVistaFacturaServicios

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Function

        'Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.FacVistaFacturaServicio) As ObjetosComunes.Entidades.FacVistaFacturaServicio Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacVistaFacturaServicio).ConsultarPorId
        '    Throw New NotImplementedException()
        'End Function

        'Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.FacVistaFacturaServicio, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacVistaFacturaServicio).Eliminar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.FacVistaFacturaServicio, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacVistaFacturaServicio).InsertarOModificar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.FacVistaFacturaServicio) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacVistaFacturaServicio).VerificarExistencia
        '    Throw New NotImplementedException()
        'End Function

        Public Function ConsultarPorId(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturaServicio) As Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturaServicio Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturaServicio).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        Public Function Eliminar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturaServicio, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturaServicio).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function InsertarOModificar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturaServicio, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturaServicio).InsertarOModificar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturaServicio) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacVistaFacturaServicio).VerificarExistencia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace