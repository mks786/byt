Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoTipoMarca
        Inherits IDaoBase(Of TipoMarca, String)
        Function ObtenerTipoMarcasFiltro(ByVal TipoMarca As TipoMarca) As IList(Of TipoMarca)
    End Interface
End Namespace