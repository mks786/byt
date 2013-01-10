Imports System.Windows.Controls
Imports System.Text.RegularExpressions

Namespace Validaciones
    Public Class SoloNumeros
        Inherits ValidationRule
        Private Const _expresionRegular As String = "\d"

        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As ValidationResult
            Dim valor As String = DirectCast(value, String)

            Dim regular As New Regex(_expresionRegular)
            If (valor <> Nothing) Then

                If Not regular.IsMatch(valor) AndAlso Not String.IsNullOrEmpty(valor) Then
                    Return New ValidationResult(False, Recursos.MensajesValidaciones.SoloNumeros)
                Else
                    Return ValidationResult.ValidResult
                End If
            Else
                Return ValidationResult.ValidResult
            End If
        End Function
    End Class
End Namespace