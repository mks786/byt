Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacInternacional
        Inherits IDaoBase(Of FacInternacional, Integer)
        Function ObtenerFacInternacionalesFiltro(ByVal FacInternacional As FacInternacional) As IList(Of FacInternacional)
    End Interface
End Namespace