Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IFacFacturaPendienteConGruServicios
        Inherits IServicioBase(Of FacFacturaPendienteConGru)
        Function ObtenerFacFacturaPendienteConGrusFiltro(ByVal FacFacturaPendienteConGru As FacFacturaPendienteConGru) As IList(Of FacFacturaPendienteConGru)
    End Interface
End Namespace