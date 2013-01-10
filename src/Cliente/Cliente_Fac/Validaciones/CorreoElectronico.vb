Imports System.Windows.Controls
Imports System.Text.RegularExpressions

Namespace Validaciones
    Public Class CorreoElectronico
        Inherits ValidationRule
        Private Const _expresionRegular As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As ValidationResult
            Dim valor As String = DirectCast(value, String)

            Dim regular As New Regex(_expresionRegular)

            If Not regular.IsMatch(valor) AndAlso Not String.IsNullOrEmpty(valor) Then
                Return New ValidationResult(False, Recursos.MensajesValidaciones.CorreoElectronico)
            Else
                Return ValidationResult.ValidResult
            End If
        End Function
    End Class
End Namespace