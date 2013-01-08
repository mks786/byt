Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFacturaPendienteServicios
        Inherits IServicioBase(Of FacFacturaPendiente)
        Function ObtenerFacFacturaPendientesFiltro(ByVal FacFacturaPendiente As FacFacturaPendiente) As IList(Of FacFacturaPendiente)
    End Interface
End Namespace