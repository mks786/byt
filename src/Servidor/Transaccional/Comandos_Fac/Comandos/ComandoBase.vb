Namespace Comandos
    ''' <summary>
    ''' Comando que devuelve un objeto genérico
    ''' </summary>
    ''' <typeparam name="T">Objeto genérico</typeparam>
    Public MustInherit Class ComandoBase(Of T)
        Private _receptor As Receptor(Of T) = Nothing

        ''' <summary>
        ''' Propiedad que asigna u obtiene el objeto genérico
        ''' </summary>
        Public Property Receptor() As Receptor(Of T)
            Get
                Return _receptor
            End Get
            Protected Set(ByVal value As Receptor(Of T))
                _receptor = value
            End Set
        End Property

        ''' <summary>
        ''' Método que Ejecuta la acción del comando
        ''' </summary>
        Public MustOverride Sub Ejecutar()
    End Class
End Namespace