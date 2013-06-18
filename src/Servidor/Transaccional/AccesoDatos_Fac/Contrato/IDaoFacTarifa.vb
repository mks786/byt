Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoFacTarifa
        Inherits IDaoBase(Of FacTarifa, Integer)
        Function ObtenerFacTarifasFiltro(ByVal FacTarifa As FacTarifa) As IList(Of FacTarifa)
    End Interface
End Namespace