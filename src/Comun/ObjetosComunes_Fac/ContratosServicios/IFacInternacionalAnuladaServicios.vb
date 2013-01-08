Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacInternacionalAnuladaServicios
        Inherits IServicioBase(Of FacInternacionalAnulada)
        Function ObtenerFacInternacionalAnuladasFiltro(ByVal FacInternacionalAnulada As FacInternacionalAnulada) As IList(Of FacInternacionalAnulada)
    End Interface
End Namespace