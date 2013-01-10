Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacInternacionalAnulada
        Inherits IDaoBase(Of FacInternacionalAnulada, Integer)
        Function ObtenerFacInternacionalAnuladasFiltro(ByVal FacInternacionalAnulada As FacInternacionalAnulada) As IList(Of FacInternacionalAnulada)
    End Interface
End Namespace