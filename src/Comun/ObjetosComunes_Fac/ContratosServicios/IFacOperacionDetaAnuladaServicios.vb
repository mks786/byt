Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacOperacionDetaAnuladaServicios
        Inherits IServicioBase(Of FacOperacionDetaAnulada)
        Function ObtenerFacOperacionDetaAnuladasFiltro(ByVal FacOperacionDetaAnulada As FacOperacionDetaAnulada) As IList(Of FacOperacionDetaAnulada)
    End Interface
End Namespace