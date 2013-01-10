Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoTipoPatente
        Inherits IDaoBase(Of TipoPatente, String)
        Function ObtenerTipoPatentesFiltro(ByVal TipoPatente As TipoPatente) As IList(Of TipoPatente)
    End Interface
End Namespace