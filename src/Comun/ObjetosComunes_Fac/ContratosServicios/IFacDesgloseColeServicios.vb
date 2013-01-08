Imports System.Collections.Generic
Imports Trascend.Bolet.ObjetosComunes.Entidades

Namespace ContratosServicios
    Public Interface IFacDesgloseColeServicios
        Inherits IServicioBase(Of FacDesgloseCole)
        Function ObtenerFacDesgloseColesFiltro(ByVal FacDesgloseCole As FacDesgloseCole) As IList(Of FacDesgloseCole)
    End Interface
End Namespace