Imports System.Collections.Generic
Imports Trascend.Bolet.ObjetosComunes.Entidades

Namespace ContratosServicios
    Public Interface IFacDepartamentoServicioServicios
        Inherits IServicioBase(Of FacDepartamentoServicio)
        Function ObtenerFacDepartamentoServiciosFiltro(ByVal FacDepartamentoServicio As FacDepartamentoServicio) As IList(Of FacDepartamentoServicio)
    End Interface
End Namespace