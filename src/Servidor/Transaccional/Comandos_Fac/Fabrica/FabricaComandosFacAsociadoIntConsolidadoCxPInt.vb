Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacAsociadoIntConsolidadoCxPInt
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos

Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacAsociadoIntConsolidadoCxPInt
        Private Sub New()

        End Sub

        ''' <summary>
        ''' Metodo que obtiene el comando para consultar todos los registros de la tabla FAC_CXP_INT_CONSOLIDA
        ''' </summary>
        ''' <returns>ComandoConsultarTodosFacAsociadoIntConsolidadoCxPInt</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacAsociadoIntConsolidadoCxPInt))
            Return New ComandoConsultarTodosFacAsociadoIntConsolidadoCxPInt()
        End Function

        ''' <summary>
        ''' Metodo que obtiene el comando para insertar o modificar los datos de facturacion de un asociado internacional consolidado
        ''' </summary>
        ''' <param name="facAsociadoConsolidado">Asociado Internacional consolidado a actualizar</param>
        ''' <returns>ComandoInsertarOModificarFacAsociadoConsolidadoCxPInt</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal facAsociadoConsolidado As FacAsociadoIntConsolidadoCxPInt) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacAsociadoConsolidadoCxPInt(facAsociadoConsolidado)
        End Function

        ''' <summary>
        ''' Metodo que obtiene el comando para eliminar los datos de facturacion de un asociado internacional consolidado
        ''' </summary>
        ''' <param name="facAsociadoConsolidado">Asociado Internacional consolidado a eliminar</param>
        ''' <returns>ComandoEliminarFacAsociadoIntConsolidadoCxPInt</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerComandoEliminarFacAsociadoIntConsolidadoCxPInt(facAsociadoConsolidado As FacAsociadoIntConsolidadoCxPInt) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacAsociadoIntConsolidadoCxPInt(facAsociadoConsolidado)
        End Function




    End Class


End Namespace
