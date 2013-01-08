Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFacturaAnuladaServicios
        Inherits IServicioBase(Of FacFacturaAnulada)
        Function ObtenerFacFacturaAnuladasFiltro(ByVal FacFacturaAnulada As FacFacturaAnulada) As IList(Of FacFacturaAnulada)
    End Interface
End Namespace