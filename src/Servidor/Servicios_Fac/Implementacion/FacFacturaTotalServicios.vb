Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacFacturaTotalServicios
        Inherits MarshalByRefObject
        Implements IFacFacturaTotalServicios



        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Método que obtiene todos los FacFacturaTotales
        ''' </summary>
        ''' <returns>Todos los FacFacturaTotales</returns>
        Public Function ConsultarTodos() As IList(Of FacFacturaTotal) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacFacturaTotal).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacFacturaTotals As IList(Of FacFacturaTotal) = ControladorFacFacturaTotal.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacFacturaTotals
        End Function

        Public Function ObteneFacFacturaTotalsFiltro(ByVal FacFacturaTotal As FacFacturaTotal) As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotal) Implements ObjetosComunes.ContratosServicios.IFacFacturaTotalServicios.ObtenerFacFacturaTotalsFiltro
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacFacturaTotals As IList(Of FacFacturaTotal)

            FacFacturaTotals = ControladorFacFacturaTotal.ConsultarFacFacturaTotalsFiltro(FacFacturaTotal)

            Return FacFacturaTotals

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Function


        Public Function ConsultarPorId(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotal) As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotal Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotal).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        Public Function Eliminar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotal, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotal).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function InsertarOModificar(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotal, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotal).InsertarOModificar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(ByVal entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotal) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacFacturaTotal).VerificarExistencia
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace