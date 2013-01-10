Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Data

Namespace Transformadores
    Class DosValresAUno
        Implements IMultiValueConverter
        Public Function Convert(ByVal values() As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
            Dim numero As Integer = Integer.Parse(values(0).ToString())
            If Not numero.Equals(Integer.MinValue) Then
                Return values(0).ToString() & " - " & CType(values(1), DateTime).ToString("dd/MM/yyyy")
            End If

            Return ""
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace