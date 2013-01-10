Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacAnuladaFisica
        Inherits IDaoBase(Of FacAnuladaFisica, Integer)
        Function ObtenerFacAnuladaFisicasFiltro(ByVal FacAnuladaFisica As FacAnuladaFisica) As IList(Of FacAnuladaFisica)
    End Interface
End Namespace