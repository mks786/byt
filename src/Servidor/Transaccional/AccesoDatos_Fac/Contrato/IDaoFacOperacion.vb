Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacOperacion
        Inherits IDaoBase(Of FacOperacion, Integer)
        Function ObtenerFacOperacionesFiltro(ByVal FacOperacion As FacOperacion) As IList(Of FacOperacion)
    End Interface
End Namespace