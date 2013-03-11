Imports System.Collections.Generic
'Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IBancoGServicios
        Inherits IServicioBase(Of BancoG)
        Function ObtenerBancoGsFiltro(ByVal BancoG As BancoG) As IList(Of BancoG)
    End Interface
End Namespace