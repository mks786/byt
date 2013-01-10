Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoTipoClase
        Inherits IDaoBase(Of TipoClase, String)
        Function ObtenerTipoClasesFiltro(ByVal TipoClase As TipoClase) As IList(Of TipoClase)
    End Interface
End Namespace