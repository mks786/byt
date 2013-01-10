Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacOperacionProforma
        Inherits IDaoBase(Of FacOperacionProforma, Integer)
        Function ObtenerFacOperacionProformasFiltro(ByVal FacOperacionProforma As FacOperacionProforma) As IList(Of FacOperacionProforma)
    End Interface
End Namespace