Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace ContratosServicios
    Public Interface IServicioBase(Of Entidad)
        Function ConsultarTodos() As IList(Of Entidad)

        Function ConsultarPorId(ByVal entidad As Entidad) As Entidad

        Function InsertarOModificar(ByVal entidad As Entidad, ByVal hash As Integer) As Boolean

        Function Eliminar(ByVal entidad As Entidad, ByVal hash As Integer) As Boolean

        Function VerificarExistencia(ByVal entidad As Entidad) As Boolean
    End Interface
End Namespace