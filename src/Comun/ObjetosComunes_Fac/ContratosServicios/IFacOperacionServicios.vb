Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacOperacionServicios                   
        Inherits IServicioBase(Of FacOperacion)
        Function ObtenerFacOperacionesFiltro(ByVal FacOperacion As FacOperacion) As IList(Of FacOperacion)
    End Interface
End Namespace