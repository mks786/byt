Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacOperacionPais
        Inherits IDaoBase(Of FacOperacionPais, Integer)
        Function ObtenerFacOperacionPaisesFiltro(ByVal FacOperacionPais As FacOperacionPais) As IList(Of FacOperacionPais)
    End Interface
End Namespace