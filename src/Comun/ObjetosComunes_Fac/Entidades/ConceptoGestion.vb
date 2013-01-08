Namespace Entidades
    <Serializable()> _
    Public Class ConceptoGestion
#Region "Atributos"

        Private _id As String
        Private _descripcion As String
#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la ConceptoGestion
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As String)
            Me._id = id
        End Sub
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la ConceptoGestion
        ''' </summary>
        Public Overridable Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal value As String)
                Me._id = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene descripcion
        ''' </summary>
        Public Overridable Property Descripcion() As String
            Get
                Return Me._descripcion
            End Get
            Set(ByVal value As String)
                Me._descripcion = value
            End Set
        End Property
#End Region
    End Class
End Namespace