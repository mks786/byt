Imports System.Collections.Generic
Imports System.Configuration
Imports NLog
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Fabrica
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Imports Trascend.Bolet.LogicaNegocio.Controladores

Namespace Controladores

    Public Class ControladorFacInternacionalConsolidada
        Inherits ControladorBase
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Metodo que inserta o actualiza una Factura Internacional Consolidada
        ''' </summary>
        ''' <param name="entidad">Factura a insertar y/o actualizar</param>
        ''' <param name="hash">Hash del usuario logueado</param>
        ''' <returns>True si la operacion se realiza con exito; False, en caso contrario</returns>
        ''' <remarks></remarks>
        Public Shared Function InsertarOModificar(facInternacionalConsolidada As FacInternacionalConsolidada, hash As Integer) As Boolean

            Dim exitoso As Boolean = False

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                'Dim comando As ComandoBase(Of Boolean) = FabricaComandosFacInternacional.ObtenerComandoInsertarOModificar(FacInternacional)
                Dim comando As ComandoBase(Of Boolean) = FabricaComandosFacInternacionalConsolidada.ObtenerComandoInsertarOModificar(facInternacionalConsolidada)
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
        ''' Metodo que obtiene todos los registros de la tabla FAC_CXP_INT_ISEL
        ''' </summary>
        ''' <returns>Todos los registros de la tabla FAC_CXP_INT_ISEL</returns>
        ''' <remarks></remarks>
        Public Shared Function ConsultarTodos() As IList(Of FacInternacionalConsolidada)

            Dim retorno As IList(Of FacInternacionalConsolidada)
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region                
                'Dim comando As ComandoBase(Of IList(Of FacInternacional)) = FabricaComandosFacInternacional.ObtenerComandoConsultarTodos()
                Dim comando As ComandoBase(Of IList(Of FacInternacionalConsolidada)) = FabricaComandosFacInternacionalConsolidada.ObtenerComandoConsultarTodos()
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
        ''' Metodo que elimina un registro de la tabla FAC_CXP_INT_ISEL
        ''' </summary>
        ''' <param name="entidad">Registro a eliminar de la tabla</param>
        ''' <param name="hash">Hash del usuario logueado</param>
        ''' <returns>True si es exitosa la operacion; False, en caso contrario</returns>
        ''' <remarks></remarks>
        Public Shared Function Eliminar(FacInternacionalConsolidada As FacInternacionalConsolidada, hash As Integer) As Boolean

            Dim exitoso As Boolean = False
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim comando As ComandoBase(Of Boolean) = FabricaComandosFacInternacionalConsolidada.ObtenerComandoEliminarFacInternacionalConsolidada(FacInternacionalConsolidada)
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
