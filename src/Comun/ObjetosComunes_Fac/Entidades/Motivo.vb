Namespace Entidades
    <Serializable()> _
    Public Class Motivo
#Region "Atributos"

        Private _id As Integer
        Private _detalle As String
#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la Motivo
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As Integer)
            Me._id = id
        End Sub
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la Motivo
        ''' </summary>
        Public Overridable Property Id() As Integer
            Get
                Return Me._id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        ' ''' <summary>
        ' ''' Propiedad que asigna u obtiene docmuento español
        ' ''' </summary>
        Public Overridable Property Detalle() As String
            Get
                Return Me._detalle
            End Get
            Set(ByVal value As String)
                Me._detalle = value
            End Set
        End Property


#End Region

    End Class
End Namespace