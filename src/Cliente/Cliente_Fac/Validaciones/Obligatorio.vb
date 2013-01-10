Imports System.Windows.Controls

Namespace Validaciones
    Public Class Obligatorio
        Inherits ValidationRule
        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As ValidationResult
            Dim valor As String = DirectCast(value, String)

            If String.IsNullOrEmpty(valor) Then
                Return New ValidationResult(False, Recursos.MensajesValidaciones.Obligarotio)
            Else
                Return ValidationResult.ValidResult
            End If
        End Function
    End Class
End Namespace