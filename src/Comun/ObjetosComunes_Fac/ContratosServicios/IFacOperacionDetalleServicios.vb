Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacOperacionDetalleServicios
        Inherits IServicioBase(Of FacOperacionDetalle)
        Function ObtenerFacOperacionDetallesFiltro(ByVal FacOperacionDetalle As FacOperacionDetalle) As IList(Of FacOperacionDetalle)
    End Interface
End Namespace