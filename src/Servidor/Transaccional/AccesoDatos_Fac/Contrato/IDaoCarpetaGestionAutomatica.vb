Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato

Namespace Contrato
    Public Interface IDaoCarpetaGestionAutomatica
        Inherits IDaoBase(Of CarpetaGestionAutomatica, String)
        Function ObtenerCarpetasPorIniciales(ByVal Usuario As Usuario) As IList(Of CarpetaGestionAutomatica)
    End Interface
End Namespace

