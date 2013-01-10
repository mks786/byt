Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacCobro
        Inherits IDaoBase(Of FacCobro, Integer)
        Function ObtenerFacCobrosFiltro(ByVal FacCobro As FacCobro) As IList(Of FacCobro)
    End Interface
End Namespace