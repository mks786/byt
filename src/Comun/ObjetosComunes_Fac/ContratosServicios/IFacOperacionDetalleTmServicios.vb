Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacOperacionDetalleTmServicios
        Inherits IServicioBase(Of FacOperacionDetalleTm)
        Function ObtenerFacOperacionDetalleTmsFiltro(ByVal FacOperacionDetalleTm As FacOperacionDetalleTm) As IList(Of FacOperacionDetalleTm)
    End Interface
End Namespace