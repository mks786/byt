Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacInternacionalConsolidada
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos

Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacInternacionalConsolidada
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Funcion que obtiene el objeto comando para insertar y/o actualizar una factura internacional consolidada
        ''' </summary>
        ''' <param name="facInternacionalConsolidada">Factura Internacional Consolidada a actualizar</param>
        ''' <returns>Comando para insertar y/o actualizar la factura internacional consolidada</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerComandoInsertarOModificar(facInternacionalConsolidada As FacInternacionalConsolidada) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacInternacionalConsolidada(facInternacionalConsolidada)
        End Function

        ''' <summary>
        ''' Funcion que obtiene el objeto comando para consultar todos los registros de la tabla FAC_CXP_INT_ISEL
        ''' </summary>
        ''' <returns>Comando para consultar todos los registros de la tabla FAC_CXP_INT_ISEL</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacInternacionalConsolidada))
            Return New ComandoConsultarTodosFacInternacionalConsolidada()
        End Function

        ''' <summary>
        ''' Funcion que obtiene el objeto comando para eliminar un registro de la tabla FAC_CXP_INT_ISEL
        ''' </summary>
        ''' <param name="FacInternacionalConsolidada">FacInternacionalConsolidada a eliminar</param>
        ''' <returns>Comando para eliminar un registro de la tabla FAC_CXP_INT_ISEL</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerComandoEliminarFacInternacionalConsolidada(FacInternacionalConsolidada As FacInternacionalConsolidada) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacInternacionalConsolidada(FacInternacionalConsolidada)
        End Function

        

    End Class
End Namespace

