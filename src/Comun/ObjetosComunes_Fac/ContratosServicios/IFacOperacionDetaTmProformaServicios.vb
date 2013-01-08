Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacOperacionDetaTmProformaServicios
        Inherits IServicioBase(Of FacOperacionDetaTmProforma)
        Function ObtenerFacOperacionDetaTmProformasFiltro(ByVal FacOperacionDetaTmProforma As FacOperacionDetaTmProforma) As IList(Of FacOperacionDetaTmProforma)
    End Interface
End Namespace