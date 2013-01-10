Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Contrato
    Public Interface IDaoBaseFac(Of T, Id)
        Function ObtenerTodos() As IList(Of T)

        Function ObtenerPorId(ByVal id As Id) As T

        Function ObtenerPorIdYBloquear(ByVal id As Id) As T

        Function InsertarOModificar(ByVal entidad As T) As Boolean

        Function Eliminar(ByVal entidad As T) As Boolean

        Function VerificarExistencia(ByVal id As Id) As Boolean

    End Interface
End Namespace