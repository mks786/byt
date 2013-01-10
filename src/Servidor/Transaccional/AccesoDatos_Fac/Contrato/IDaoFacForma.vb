Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacForma
        Inherits IDaoBase(Of FacForma, Integer)
        Function ObtenerFacFormasFiltro(ByVal FacForma As FacForma) As IList(Of FacForma)
    End Interface
End Namespace