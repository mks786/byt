Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacOperacionDetaTmProforma
        Inherits IDaoBase(Of FacOperacionDetaTmProforma, Integer)
        Function ObtenerFacOperacionDetaTmProformasFiltro(ByVal FacOperacionDetaTmProforma As FacOperacionDetaTmProforma) As IList(Of FacOperacionDetaTmProforma)
    End Interface
End Namespace