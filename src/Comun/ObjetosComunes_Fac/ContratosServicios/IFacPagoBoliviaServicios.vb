Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacPagoBoliviaServicios
        Inherits IServicioBase(Of FacPagoBolivia)
        Function ObtenerFacPagoBoliviasFiltro(ByVal FacPagoBolivia As FacPagoBolivia) As IList(Of FacPagoBolivia)
    End Interface
End Namespace