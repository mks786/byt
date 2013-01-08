Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacOperacionPaisServicios
        Inherits IServicioBase(Of FacOperacionPais)
        Function ObtenerFacOperacionPaisesFiltro(ByVal FacOperacionPais As FacOperacionPais) As IList(Of FacOperacionPais)
    End Interface
End Namespace