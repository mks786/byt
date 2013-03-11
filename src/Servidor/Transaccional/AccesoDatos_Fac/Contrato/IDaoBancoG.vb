Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoBancoG
        Inherits IDaoBase(Of BancoG, Integer)
        Function ObtenerBancoGsFiltro(ByVal BancoG As BancoG) As IList(Of BancoG)
    End Interface
End Namespace