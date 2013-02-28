Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacFacturaPendienteConGru
        Inherits IDaoBase(Of FacFacturaPendienteConGru, Integer)
        Function ObtenerFacFacturaPendienteConGrusFiltro(ByVal FacFacturaPendienteConGru As FacFacturaPendienteConGru) As IList(Of FacFacturaPendienteConGru)
    End Interface
End Namespace