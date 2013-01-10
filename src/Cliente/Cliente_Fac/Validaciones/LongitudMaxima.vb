Imports System.Windows.Controls

Namespace Validaciones
    Public Class LongitudMaxima
        Inherits ValidationRule
        Private _longitud As Integer = 0

        Public Property Longitud() As Integer
            Get
                Return _longitud
            End Get
            Set(ByVal value As Integer)
                Me._longitud = value
            End Set
        End Property

        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As ValidationResult
            Dim valor As String = DirectCast(value, String)

            If Not String.IsNullOrEmpty(valor) AndAlso valor.Length > Me.Longitud Then
                Return New ValidationResult(False, String.Format(Recursos.MensajesValidaciones.LongitudMaxima, Me.Longitud))
            Else
                Return ValidationResult.ValidResult
            End If
        End Function
    End Class
End Namespace