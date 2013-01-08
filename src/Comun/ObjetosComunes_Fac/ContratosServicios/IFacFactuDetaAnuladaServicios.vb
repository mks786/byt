Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFactuDetaAnuladaServicios
        Inherits IServicioBase(Of FacFactuDetaAnulada)
        Function ObtenerFacFactuDetaAnuladasFiltro(ByVal FacFactuDetaAnulada As FacFactuDetaAnulada) As IList(Of FacFactuDetaAnulada)
    End Interface
End Namespace