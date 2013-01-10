Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacFactuDetaAnulada
        Inherits IDaoBase(Of FacFactuDetaAnulada, Integer)
        Function ObtenerFacFactuDetaAnuladasFiltro(ByVal FacFactuDetaAnulada As FacFactuDetaAnulada) As IList(Of FacFactuDetaAnulada)
    End Interface
End Namespace