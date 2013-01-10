Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Contrato
'Imports Diginsoft.Bolet.AccesoDatos.Contrato
Namespace Contrato
    Public Interface IDaoChequeRecido
        Inherits IDaoBase(Of ChequeRecido, Integer)
        Function ObtenerChequeRecidosFiltro(ByVal chequerecido As ChequeRecido) As IList(Of ChequeRecido)
    End Interface
End Namespace