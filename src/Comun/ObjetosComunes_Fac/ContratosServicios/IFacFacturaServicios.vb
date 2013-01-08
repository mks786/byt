Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFacturaServicios
        Inherits IServicioBase(Of FacFactura)
        Function ObtenerFacFacturasFiltro(ByVal FacFactura As FacFactura) As IList(Of FacFactura)
    End Interface
End Namespace