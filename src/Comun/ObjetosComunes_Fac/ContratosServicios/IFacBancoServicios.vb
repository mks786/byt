Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacBancoServicios
        Inherits IServicioBase(Of FacBanco)
        Function ObtenerFacBancosFiltro(ByVal FacBanco As FacBanco) As IList(Of FacBanco)
    End Interface
End Namespace