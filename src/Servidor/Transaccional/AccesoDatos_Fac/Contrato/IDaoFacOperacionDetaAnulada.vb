Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacOperacionDetaAnulada
        Inherits IDaoBase(Of FacOperacionDetaAnulada, Integer)
        Function ObtenerFacOperacionDetaAnuladasFiltro(ByVal FacOperacionDetaAnulada As FacOperacionDetaAnulada) As IList(Of FacOperacionDetaAnulada)
    End Interface
End Namespace