Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacInternacionalConsolidadaServicios
        Inherits MarshalByRefObject
        Implements IFacInternacionalConsolidadaServicios

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Servicio que obtiene todos los registros de la tabla FAC_CXP_INT_ISEL
        ''' </summary>
        ''' <returns>Todos los registros de la tabla FAC_CXP_INT_ISEL</returns>
        ''' <remarks></remarks>
        Public Function ConsultarTodos() As IList(Of FacInternacionalConsolidada) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacInternacionalConsolidada).ConsultarTodos

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Dim FacInternacionales As IList(Of FacInternacional) = ControladorFacInternacional.ConsultarTodos()
            Dim FacInternaionalesConsolidada As IList(Of FacInternacionalConsolidada) = ControladorFacInternacionalConsolidada.ConsultarTodos()


            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacInternaionalesConsolidada


        End Function


        Public Function ConsultarPorId(ByVal entidad As FacInternacionalConsolidada) As FacInternacionalConsolidada Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacInternacionalConsolidada).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Servicio que elimina un registro de la tabla FAC_CXP_INT_ISEL
        ''' </summary>
        ''' <param name="entidad">Registro a eliminar de la tabla </param>
        ''' <param name="hash">Hash del usuario logueado</param>
        ''' <returns>True si es exitosa la operacion; False, en caso contrario</returns>
        ''' <remarks></remarks>
        Public Function Eliminar(ByVal entidad As FacInternacionalConsolidada, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacInternacionalConsolidada).Eliminar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Dim exitoso As Boolean = ControladorFacInternacional.Eliminar(entidad,hash)
            Dim exitoso As Boolean = ControladorFacInternacionalConsolidada.Eliminar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        ''' <summary>
        ''' Servicio que inserta o actualiza una Factura Internacional Consolidada en la tabla tempora FAC_CXP_INT_ISEL
        ''' </summary>
        ''' <param name="entidad">Factura a insertar o actualizar</param>
        ''' <param name="hash">Hash del usuario logueado</param>
        ''' <returns>True si es exitosa la actualizacion; False, en caso contrario</returns>
        ''' <remarks></remarks>
        Public Function InsertarOModificar(ByVal entidad As FacInternacionalConsolidada, ByVal hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacInternacionalConsolidada).InsertarOModificar

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                Logger.Debug("Entrando al Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacInternacionalConsolidada.InsertarOModificar(entidad, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                Logger.Debug("Saliendo del Método {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso

        End Function

        Public Function VerificarExistencia(ByVal entidad As FacInternacionalConsolidada) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of FacInternacionalConsolidada).VerificarExistencia
            Throw New NotImplementedException()
        End Function

    End Class
End Namespace

