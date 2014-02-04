Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Imports Trascend.Bolet.LogicaNegocio.Controladores

Namespace Controladores
    Public Class ControladorFacAsociadoIntConsolidadoCxPInt
        Inherits ControladorBase
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Metodo que obtiene todos los registros de la tabla FAC_CXP_INT_CONSOLIDA
        ''' </summary>
        ''' <returns>Lista de todos los registros de la tabla mencionada</returns>
        ''' <remarks></remarks>
        Public Shared Function ConsultarTodos() As IList(Of FacAsociadoIntConsolidadoCxPInt)
            Dim retorno As IList(Of FacAsociadoIntConsolidadoCxPInt)
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region      

                'Dim comando As ComandoBase(Of IList(Of FacInternacional)) = FabricaComandosFacInternacional.ObtenerComandoConsultarTodos()
                Dim comando As ComandoBase(Of IList(Of FacAsociadoIntConsolidadoCxPInt)) = FabricaComandosFacAsociadoIntConsolidadoCxPInt.ObtenerComandoConsultarTodos()
                comando.Ejecutar()
                retorno = comando.Receptor.ObjetoAlmacenado

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try
            Return retorno
        End Function

        ''' <summary>
        ''' Metodo que que inserta o actualiza un Asociado Internacional Consolidado
        ''' </summary>
        ''' <param name="facAsociadoConsolidado">Datos Facturacion Asociado Internacional Consolidado CxP Internacional</param>
        ''' <param name="hash">Hash del usuario logueado</param>
        ''' <returns>True si la operacion se realizo con exito; False, en caso contrario</returns>
        ''' <remarks></remarks>
        Public Shared Function InsertarOModificar(facAsociadoConsolidado As FacAsociadoIntConsolidadoCxPInt, hash As Integer) As Boolean
            Dim exitoso As Boolean = False

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                'Dim comando As ComandoBase(Of Boolean) = FabricaComandosFacInternacional.ObtenerComandoInsertarOModificar(FacInternacional)
                Dim comando As ComandoBase(Of Boolean) = FabricaComandosFacAsociadoIntConsolidadoCxPInt.ObtenerComandoInsertarOModificar(facAsociadoConsolidado)
                comando.Ejecutar()
                exitoso = comando.Receptor.ObjetoAlmacenado

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try
            Return exitoso
        End Function

        ''' <summary>
        ''' Metodo que elimina un Asociado Internacional Consolidado con sus datos de facturacion
        ''' </summary>
        ''' <param name="facAsociadoConsolidado">Asociado Internacional Consolidado a eliminar</param>
        ''' <param name="hash">Hash del usuario logueado</param>
        ''' <returns>True si la operacion es exitosa; False, en caso contrario</returns>
        ''' <remarks></remarks>
        Public Shared Function Eliminar(facAsociadoConsolidado As FacAsociadoIntConsolidadoCxPInt, hash As Integer) As Boolean
            Dim exitoso As Boolean = False
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) =
                    FabricaComandosFacAsociadoIntConsolidadoCxPInt.ObtenerComandoEliminarFacAsociadoIntConsolidadoCxPInt(facAsociadoConsolidado)
                comando.Ejecutar()
                exitoso = True

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try

            Return exitoso
        End Function

    End Class


End Namespace
