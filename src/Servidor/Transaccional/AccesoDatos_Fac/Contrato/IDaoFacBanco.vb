Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacBanco
        Inherits IDaoBase(Of FacBanco, Integer)
        Function ObtenerFacBancosFiltro(ByVal FacBanco As FacBanco) As IList(Of FacBanco)
    End Interface
End Namespace