Namespace Comandos
    Public Class Receptor(Of T)
        ''' <summary>
        ''' Objeto generico a ser almacenado
        ''' </summary>
        Private _objetoAlmacenado As T

        ''' <summary>
        ''' Propiedad que retorna el objeto genérico almacenado
        ''' </summary>
        Public ReadOnly Property ObjetoAlmacenado() As T
            Get
                Return _objetoAlmacenado
            End Get
        End Property

        ' ''' <summary>
        ' ''' Constructo predeterminado
        ' ''' </summary>
        ' ''' <param name="objetoRecibido">Objeto a almacenar</param>
        Public Sub New(ByVal objetoAlmacenado As T)
            Me._objetoAlmacenado = objetoAlmacenado
        End Sub
    End Class
End Namespace