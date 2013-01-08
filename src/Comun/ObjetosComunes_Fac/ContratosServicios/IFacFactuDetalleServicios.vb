Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFactuDetalleServicios
        Inherits IServicioBase(Of FacFactuDetalle)
        Function ObtenerFacFactuDetallesFiltro(ByVal FacFactuDetalle As FacFactuDetalle) As IList(Of FacFactuDetalle)
    End Interface
End Namespace