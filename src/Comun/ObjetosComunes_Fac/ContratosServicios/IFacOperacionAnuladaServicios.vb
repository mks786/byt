Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacOperacionAnuladaServicios
        Inherits IServicioBase(Of FacOperacionAnulada)
        Function ObtenerFacOperacionAnuladasFiltro(ByVal FacOperacionAnulada As FacOperacionAnulada) As IList(Of FacOperacionAnulada)
    End Interface
End Namespace