Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacFacturaProforma
        Inherits IDaoBase(Of FacFacturaProforma, Integer)
        Function ObtenerFacFacturaProformasFiltro(ByVal FacFacturaProforma As FacFacturaProforma) As IList(Of FacFacturaProforma)
    End Interface
End Namespace