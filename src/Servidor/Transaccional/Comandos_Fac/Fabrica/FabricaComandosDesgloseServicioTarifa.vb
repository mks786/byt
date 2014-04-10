Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosDesgloseServicioTarifa
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos

Namespace Fabrica
    Public NotInheritable Class FabricaComandosDesgloseServicioTarifa
        Private Sub New()
        End Sub


        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los DesgloseServicioTarifas
        ''' </summary>
        ''' <returns>El Comando para consultar todos los DesgloseServicioes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacDesgloseServicioTarifa))
            Return New ComandoConsultarTodosDesgloseServiciosPorTarifas()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los DesgloseServicioTarifas por filtro
        ''' </summary>
        ''' <param name="FacDesgloseServicioTarifa">Desglose de Servicio por Tarifa usado como filtro</param>
        ''' <returns>Comando para consultar todos los DesgloseServiciosTarifa por filtro</returns>
        ''' <remarks></remarks>
        Shared Function ObtenerComandoConsultarFacDesgloseServicioTarifaFiltro(FacDesgloseServicioTarifa As FacDesgloseServicioTarifa) As ComandoBase(Of IList(Of FacDesgloseServicioTarifa))
            Return New ComandoConsultarFacDesgloseServicioTarifaFiltro(FacDesgloseServicioTarifa)
        End Function





    End Class
End Namespace