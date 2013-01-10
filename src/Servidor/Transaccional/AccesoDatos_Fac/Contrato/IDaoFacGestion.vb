Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacGestion
        Inherits IDaoBase(Of FacGestion, Integer)
        Function ObtenerFacGestionesFiltro(ByVal FacGestion As FacGestion) As IList(Of FacGestion)
    End Interface
End Namespace