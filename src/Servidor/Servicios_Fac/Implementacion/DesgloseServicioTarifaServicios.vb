Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class DesgloseServicioTarifaServicios
        Inherits MarshalByRefObject
        Implements IFacDesgloseServicioTarifaServicios

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()


        Public Function ConsultarPorId(entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa) As Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Servicio que consulta todos los Desgloses de todos los Servicios por Tarifa 
        ''' </summary>
        ''' <returns>Todos los Desgloses de todos los Servicios</returns>
        ''' <remarks></remarks>
        Public Function ConsultarTodos() As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa).ConsultarTodos

            Dim DesgloseServiciosTarifas As IList(Of FacDesgloseServicioTarifa)

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                DesgloseServiciosTarifas = ControladorDesgloseServicioTarifa.ConsultarTodos()

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try

            Return DesgloseServiciosTarifas

        End Function

        ''' <summary>
        ''' Servicio que consulta Desgloses de Servicios de Tarifas por filtro
        ''' </summary>
        ''' <param name="FacDesgloseServicioTarifa">Desglose de Servicio por Tarifa que sirve de filtro</param>
        ''' <returns>Lista de Desgloses de Servicios por Tarifa</returns>
        ''' <remarks></remarks>
        Public Function ObtenerFacDesgloseServicioTarifaFiltro(ByVal FacDesgloseServicioTarifa As FacDesgloseServicioTarifa) As IList(Of FacDesgloseServicioTarifa) Implements ObjetosComunes.ContratosServicios.IFacDesgloseServicioTarifaServicios.ObtenerFacDesgloseServicioTarifaFiltro

            Dim DesgloseServiciosTarifas As IList(Of FacDesgloseServicioTarifa)

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                DesgloseServiciosTarifas = ControladorDesgloseServicioTarifa.ConsultarFacDesgloseServicioTarifaFiltro(FacDesgloseServicioTarifa)

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try

            Return DesgloseServiciosTarifas

        End Function

        Public Function Eliminar(entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa, hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa).Eliminar
            Throw New NotImplementedException()
        End Function

        Public Function InsertarOModificar(entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa, hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa).InsertarOModificar
            Throw New NotImplementedException()
        End Function

        Public Function VerificarExistencia(entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacDesgloseServicioTarifa).VerificarExistencia
            Throw New NotImplementedException()
        End Function

    End Class

End Namespace
