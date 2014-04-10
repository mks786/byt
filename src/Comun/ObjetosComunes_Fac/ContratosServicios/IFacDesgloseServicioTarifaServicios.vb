Imports System.Collections.Generic
Imports Trascend.Bolet.ObjetosComunes.Entidades

Namespace ContratosServicios
    Public Interface IFacDesgloseServicioTarifaServicios
        Inherits IServicioBase(Of FacDesgloseServicioTarifa)

        Function ObtenerFacDesgloseServicioTarifaFiltro(ByVal FacDesgloseServicioTarifa As FacDesgloseServicioTarifa) As IList(Of FacDesgloseServicioTarifa)

    End Interface
End Namespace
