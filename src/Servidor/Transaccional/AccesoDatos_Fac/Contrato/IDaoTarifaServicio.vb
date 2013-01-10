Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoTarifaServicio
        Inherits IDaoBase(Of TarifaServicio, String)
        Function ObtenerTarifaServiciosFiltro(ByVal TarifaServicio As TarifaServicio) As IList(Of TarifaServicio)
    End Interface
End Namespace