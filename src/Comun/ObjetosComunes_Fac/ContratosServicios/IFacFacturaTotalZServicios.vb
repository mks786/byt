Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFacturaTotalZServicios
        Inherits IServicioBase(Of FacFacturaTotalZ)
        Function ObtenerFacFacturaTotalZsFiltro(ByVal FacFacturaTotalZ As FacFacturaTotalZ) As IList(Of FacFacturaTotalZ)
    End Interface
End Namespace