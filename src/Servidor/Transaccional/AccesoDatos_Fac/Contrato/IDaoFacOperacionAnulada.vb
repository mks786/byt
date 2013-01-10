Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacOperacionAnulada
        Inherits IDaoBase(Of FacOperacionAnulada, Integer)
        Function ObtenerFacOperacionAnuladasFiltro(ByVal FacOperacionAnulada As FacOperacionAnulada) As IList(Of FacOperacionAnulada)
    End Interface
End Namespace