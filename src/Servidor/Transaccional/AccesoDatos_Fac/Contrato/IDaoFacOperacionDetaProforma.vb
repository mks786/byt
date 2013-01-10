Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacOperacionDetaProforma
        Inherits IDaoBase(Of FacOperacionDetaProforma, Integer)
        Function ObtenerFacOperacionDetaProformasFiltro(ByVal FacOperacionDetaProforma As FacOperacionDetaProforma) As IList(Of FacOperacionDetaProforma)
    End Interface
End Namespace