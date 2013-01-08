Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacInternacionalServicios
        Inherits IServicioBase(Of FacInternacional)
        Function ObtenerFacInternacionalesFiltro(ByVal FacInternacional As FacInternacional) As IList(Of FacInternacional)
    End Interface
End Namespace