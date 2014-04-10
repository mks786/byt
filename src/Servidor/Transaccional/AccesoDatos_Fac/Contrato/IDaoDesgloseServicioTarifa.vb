Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato

Namespace Contrato
    Public Interface IDaoDesgloseServicioTarifa
        Inherits IDaoBase(Of FacDesgloseServicioTarifa, Char)

        Function ObtenerFacDesgloseServiciosTarifaFiltro(FacDesgloseServicioTarifa As FacDesgloseServicioTarifa) As IList(Of FacDesgloseServicioTarifa)

    End Interface

End Namespace
