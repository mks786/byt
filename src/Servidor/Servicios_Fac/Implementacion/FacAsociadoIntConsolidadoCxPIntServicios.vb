Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.LogicaNegocio.Controladores
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports System.Runtime.Remoting

Namespace Implementacion
    Public Class FacAsociadoIntConsolidadoCxPIntServicios
        Inherits MarshalByRefObject
        Implements IFacAsociadoIntConsolidadoCxPIntServicios

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()


        
        ''' <summary>
        ''' Servicio que consulta todos los registros de la tabla FAC_CXP_INT_CONSOLIDA
        ''' </summary>
        ''' <returns>Lista de todos los registros de la tabla mencionada</returns>
        ''' <remarks></remarks>
        Public Function ConsultarTodos() As System.Collections.Generic.IList(Of Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt) Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt).ConsultarTodos
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                Logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim FacAsociadoConsolidado As IList(Of FacAsociadoIntConsolidadoCxPInt) = ControladorFacAsociadoIntConsolidadoCxPInt.ConsultarTodos()

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                Logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return FacAsociadoConsolidado
        End Function

        Public Function ConsultarPorId(entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt) As Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt).ConsultarPorId
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Servicio para eliminar un FacAsociadoIntCxpInt
        ''' </summary>
        ''' <param name="facAsociadoConsolidado">Registro a eliminar</param>
        ''' <param name="hash">Hash del usuario logueado</param>
        ''' <returns>True si la operacion se realizo con exito; False, en caso contrario</returns>
        ''' <remarks></remarks>
        Public Function Eliminar(facAsociadoConsolidado As Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt, hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt).Eliminar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacAsociadoIntConsolidadoCxPInt.Eliminar(facAsociadoConsolidado, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        ''' <summary>
        ''' Servicio que inserta o actualiza un Asociado Internacional Consolidado 
        ''' </summary>
        ''' <param name="facAsociadoConsolidado">Datos Facturacion Asociado Internacional Consolidado CxP Internacional</param>
        ''' <param name="hash">Hash del usuario logueado</param>
        ''' <returns>True si la operacion se realizo con exito; False, en caso contrario</returns>
        ''' <remarks></remarks>
        Public Function InsertarOModificar(facAsociadoConsolidado As Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt, hash As Integer) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt).InsertarOModificar
            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim exitoso As Boolean = ControladorFacAsociadoIntConsolidadoCxPInt.InsertarOModificar(facAsociadoConsolidado, hash)

            '#Region "trace"
            If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Return exitoso
        End Function

        Public Function VerificarExistencia(entidad As Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt) As Boolean Implements ObjetosComunes.ContratosServicios.IServicioBase(Of Trascend.Bolet.ObjetosComunes.Entidades.FacAsociadoIntConsolidadoCxPInt).VerificarExistencia
            Throw New NotImplementedException()
        End Function

    End Class

End Namespace
