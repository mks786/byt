Namespace Entidades
    <Serializable()> _
    Public Class Impuesto
#Region "Atributos"

        Private _id As DateTime
        Private _valor As Double
#End Region

#Region "Constructores"

        ''' <summary>
        ''' Constructor que inicializa el Id de la Impuesto
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As DateTime)
            Me._id = id
        End Sub
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la Impuesto
        ''' </summary>
        Public Overridable Property Id() As DateTime
            Get
                Return Me._id
            End Get
            Set(ByVal value As DateTime)
                Me._id = value
            End Set
        End Property

        ' ''' <summary>
        ' ''' Propiedad que asigna u obtiene docmuento español
        ' ''' </summary>
        Public Overridable Property Valor() As Double
            Get
                Return Me._valor
            End Get
            Set(ByVal value As Double)
                Me._valor = value
            End Set
        End Property

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        Public Sub New()
        End Sub
#End Region

    End Class
End Namespace