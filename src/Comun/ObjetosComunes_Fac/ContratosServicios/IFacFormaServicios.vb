Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFormaServicios
        Inherits IServicioBase(Of FacForma)
        Function ObtenerFacFormasFiltro(ByVal FacForma As FacForma) As IList(Of FacForma)
    End Interface
End Namespace