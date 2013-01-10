Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacOperacionDetalleTm
        Inherits IDaoBase(Of FacOperacionDetalleTm, Integer)
        Function ObtenerFacOperacionDetalleTmsFiltro(ByVal FacOperacionDetalleTm As FacOperacionDetalleTm) As IList(Of FacOperacionDetalleTm)
    End Interface
End Namespace