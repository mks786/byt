Imports System.Collections.Generic
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace ContratosServicios
    Public Interface IAsociado2Servicios
        Inherits IServicioBase(Of Asociado2)
        Function AuditoriaPorFkyTabla(ByVal auditoria As Auditoria) As IList(Of Auditoria)
        Function ConsultarAsociadoConTodo(ByVal asociado As Asociado) As Asociado
    End Interface
End Namespace
