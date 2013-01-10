Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacFacturaTotalZServicios
        Inherits MarshalByRefObject
        Implements IFacFacturaTotalZServicios



        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los FacFacturaTotalZes
        ''' </summary>
        ''' <returns>Todos los FacFacturaTotalZes</returns>
        Public Function ConsultarTodos() As IList(Of FacFacturaTotalZ) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacFacturaTotalZ).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacFacturaTotalZs As IList(Of FacFacturaTotalZ) = ControladorFacFacturaTotalZ.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacFacturaTotalZs
        End Function

        Public Function ObteneFacFacturaTotalZsFiltro(ByVal FacFacturaTotalZ As FacFacturaTotalZ) As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotalZ) Implements ObjetosComunes.ContratosServicios.IFacFacturaTotalZServicios.ObtenerFacFacturaTotalZsFiltro
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacFacturaTotalZs As IList(Of FacFacturaTotalZ)

            FacFacturaTotalZs = ControladorFacFacturaTotalZ.ConsultarFacFacturaTotalZsFiltro(FacFacturaTotalZ)

            Return FacFacturaTotalZs

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Function

        'Public Function ConsultarPorId(ByVal entidad As ObjetosComunes.Entidades.FacFacturaTotalZ) As ObjetosComunes.Entidades.FacFacturaTotalZ Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFacturaTotalZ).ConsultarPorId
        '    Throw New NotImplementedException()
        'End Function

        'Public Function Eliminar(ByVal entidad As ObjetosComunes.Entidades.FacFacturaTotalZ, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFacturaTotalZ).Eliminar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function InsertarOModificar(ByVal entidad As ObjetosComunes.Entidades.FacFacturaTotalZ, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFacturaTotalZ).InsertarOModificar
        '    Throw New NotImplementedException()
        'End Function

        'Public Function VerificarExistencia(ByVal entidad As ObjetosComunes.Entidades.FacFacturaTotalZ) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of ObjetosComunes.Entidades.FacFacturaTotalZ).VerificarExistencia
        '    Throw New NotImplementedException()
        'End Function

        Public Function ConsultarPorId(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotalZ) As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotalZ Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotalZ).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        Public Function Eliminar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotalZ, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotalZ).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function InsertarOModificar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotalZ, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotalZ).InsertarOModificar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotalZ) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotalZ).VerificarExistencia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace