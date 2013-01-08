Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacOperacionProformaServicios
        Inherits IServicioBase(Of FacOperacionProforma)
        Function ObtenerFacOperacionProformasFiltro(ByVal FacOperacionProforma As FacOperacionProforma) As IList(Of FacOperacionProforma)
    End Interface
End Namespace