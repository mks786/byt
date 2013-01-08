Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacCobroFacturaServicios
        Inherits IServicioBase(Of FacCobroFactura)
        Function ObtenerFacCobroFacturasFiltro(ByVal FacCobroFactura As FacCobroFactura) As IList(Of FacCobroFactura)
    End Interface
End Namespace