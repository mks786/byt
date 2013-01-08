Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFacturaProformaServicios
        Inherits IServicioBase(Of FacFacturaProforma)
        Function ObtenerFacFacturaProformasFiltro(ByVal FacFacturaProforma As FacFacturaProforma) As IList(Of FacFacturaProforma)
    End Interface
End Namespace