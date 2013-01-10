Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacPagoBolivia
        Inherits IDaoBase(Of FacPagoBolivia, Integer)
        Function ObtenerFacPagoBoliviasFiltro(ByVal FacPagoBolivia As FacPagoBolivia) As IList(Of FacPagoBolivia)
    End Interface
End Namespace