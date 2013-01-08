Imports System.Collections.Generic
Imports Trascend.Bolet.ObjetosComunes.Entidades

Namespace ContratosServicios
    Public Interface IFacDesgloseServicioServicios
        Inherits IServicioBase(Of FacDesgloseServicio)
        Function ObtenerFacDesgloseServiciosFiltro(ByVal FacDesgloseServicio As FacDesgloseServicio) As IList(Of FacDesgloseServicio)
    End Interface
End Namespace