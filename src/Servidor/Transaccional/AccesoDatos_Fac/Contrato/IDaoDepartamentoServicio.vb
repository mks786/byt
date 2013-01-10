Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoDepartamentoServicio
        Inherits IDaoBase(Of FacDepartamentoServicio, String)
        Function ObtenerFacDepartamentoServiciosFiltro(ByVal FacDepartamentoServicio As FacDepartamentoServicio) As IList(Of FacDepartamentoServicio)
    End Interface
End Namespace