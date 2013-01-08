Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacGestionServicios
        Inherits IServicioBase(Of FacGestion)
        Function ObtenerFacGestionesFiltro(ByVal FacGestion As FacGestion) As IList(Of FacGestion)
    End Interface
End Namespace