Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacTipoServicioServicios
        Inherits IServicioBase(Of FacTipoServicio)
        Function ObtenerFacTipoServiciosFiltro(ByVal FacTipoServicio As FacTipoServicio) As IList(Of FacTipoServicio)
    End Interface
End Namespace