Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFacturaTotalServicios
        Inherits IServicioBase(Of FacFacturaTotal)
        Function ObtenerFacFacturaTotalsFiltro(ByVal FacFacturaTotal As FacFacturaTotal) As IList(Of FacFacturaTotal)
    End Interface
End Namespace