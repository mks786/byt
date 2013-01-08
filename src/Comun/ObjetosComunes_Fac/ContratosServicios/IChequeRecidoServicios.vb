Imports System.Collections.Generic
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace ContratosServicios
    Public Interface IChequeRecidoServicios
        Inherits IServicioBase(Of ChequeRecido)
        Function ObtenerChequeRecidosFiltro(ByVal ChequeRecido As ChequeRecido) As IList(Of ChequeRecido)        
    End Interface
End Namespace