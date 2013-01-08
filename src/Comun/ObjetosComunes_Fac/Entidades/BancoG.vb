Namespace Entidades
    <Serializable()> _
    Public Class BancoG
#Region "Atributos"

        Private _id As Integer
        Private _xbanco As String
        Private _contacto As String
#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la BancoG
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As Integer)
            Me._id = id
        End Sub
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la BancoG
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
        ' ''' Propiedad que asigna u obtiene xbanco
        ' ''' </summary>
        Public Overridable Property XBanco() As String
            Get
                Return Me._xbanco
            End Get
            Set(ByVal value As String)
                Me._xbanco = value
            End Set
        End Property

        Public Overridable Property Contacto() As String
            Get
                Return Me._contacto
            End Get
            Set(ByVal value As String)
                Me._contacto = value
            End Set
        End Property

#End Region

    End Class
End Namespace