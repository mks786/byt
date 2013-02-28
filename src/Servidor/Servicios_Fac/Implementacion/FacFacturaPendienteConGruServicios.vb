Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacFacturaPendienteConGruServicios
        Inherits MarshalByRefObject
        Implements IFacFacturaPendienteConGruServicios



        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los FacFacturaPendienteConGrues
        ''' </summary>
        ''' <returns>Todos los FacFacturaPendienteConGrues</returns>
        Public Function ConsultarTodos() As IList(Of FacFacturaPendienteConGru) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacFacturaPendienteConGru).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacFacturaPendienteConGrus As IList(Of FacFacturaPendienteConGru) = ControladorFacFacturaPendienteConGru.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacFacturaPendienteConGrus
        End Function

        Public Function ObteneFacFacturaPendienteConGrusFiltro(ByVal FacFacturaPendienteConGru As FacFacturaPendienteConGru) As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaPendienteConGru) Implements ObjetosComunes.ContratosServicios.IFacFacturaPendienteConGruServicios.ObtenerFacFacturaPendienteConGrusFiltro
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacFacturaPendienteConGrus As IList(Of FacFacturaPendienteConGru)

            FacFacturaPendienteConGrus = ControladorFacFacturaPendienteConGru.ConsultarFacFacturaPendienteConGrusFiltro(FacFacturaPendienteConGru)

            Return FacFacturaPendienteConGrus

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Function

        'Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.FacFacturaPendienteConGru) As ObjetosComunes.Entidades.FacFacturaPendienteConGru Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFacturaPendienteConGru).ConsultarPorId
        '    Throw New NotImplementedException()
        'End Function

        'Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.FacFacturaPendienteConGru, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFacturaPendienteConGru).Eliminar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.FacFacturaPendienteConGru, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFacturaPendienteConGru).InsertarOModificar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.FacFacturaPendienteConGru) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFacturaPendienteConGru).VerificarExistencia
        '    Throw New NotImplementedException()
        'End Function

        Public Function ConsultarPorId(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaPendienteConGru) As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaPendienteConGru Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaPendienteConGru).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        Public Function Eliminar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaPendienteConGru, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaPendienteConGru).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function InsertarOModificar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaPendienteConGru, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaPendienteConGru).InsertarOModificar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaPendienteConGru) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaPendienteConGru).VerificarExistencia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace