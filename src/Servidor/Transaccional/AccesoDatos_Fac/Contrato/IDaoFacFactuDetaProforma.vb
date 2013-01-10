Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacFactuDetaProforma
        Inherits IDaoBase(Of FacFactuDetaProforma, Integer)
        Function ObtenerFacFactuDetaProformasFiltro(ByVal FacFactuDetaProforma As FacFactuDetaProforma) As IList(Of FacFactuDetaProforma)
    End Interface
End Namespace