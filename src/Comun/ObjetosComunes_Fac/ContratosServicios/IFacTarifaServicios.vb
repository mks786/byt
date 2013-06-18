Imports System.Collections.Generic
Imports Trascend.Bolet.ObjetosComunes.Entidades

Namespace ContratosServicios
    Public Interface IFacTarifaServicios
        Inherits IServicioBase(Of FacTarifa)
        Function ObtenerFacTarifasFiltro(ByVal FacTarifa As FacTarifa) As IList(Of FacTarifa)
    End Interface
End Namespace