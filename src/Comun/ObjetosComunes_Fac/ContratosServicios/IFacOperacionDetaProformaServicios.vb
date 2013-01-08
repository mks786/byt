Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacOperacionDetaProformaServicios
        Inherits IServicioBase(Of FacOperacionDetaProforma)
        Function ObtenerFacOperacionDetaProformasFiltro(ByVal FacPagoFacOperacionDetaProformaBolivia As FacOperacionDetaProforma) As IList(Of FacOperacionDetaProforma)
    End Interface
End Namespace