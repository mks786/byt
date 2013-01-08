Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacImpuestoServicios
        Inherits IServicioBase(Of FacImpuesto)
        Function ObtenerFacImpuestosFiltro(ByVal FacImpuesto As FacImpuesto) As IList(Of FacImpuesto)
    End Interface
End Namespace