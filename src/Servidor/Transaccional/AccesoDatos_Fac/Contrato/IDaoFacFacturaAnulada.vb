Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacFacturaAnulada
        Inherits IDaoBase(Of FacFacturaAnulada, Integer)
        Function ObtenerFacFacturaAnuladasFiltro(ByVal FacFacturaAnulada As FacFacturaAnulada) As IList(Of FacFacturaAnulada)
    End Interface
End Namespace