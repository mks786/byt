Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFactuDetaProformaServicios
        Inherits IServicioBase(Of FacFactuDetaProforma)
        Function ObtenerFacFactuDetaProformasFiltro(ByVal FacFactuDetaProforma As FacFactuDetaProforma) As IList(Of FacFactuDetaProforma)
    End Interface
End Namespace