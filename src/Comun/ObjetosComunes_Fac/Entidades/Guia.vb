Namespace Entidades
    <Serializable()> _
    Public Class Guia
#Region "Atributos"

        Private _id As String
        Private _des_guia As String
        Private _fec_guia As DateTime
#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la Guias
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As String)
            Me._id = id
        End Sub
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la Guias
        ''' </summary>
        Public Overridable Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal value As String)
                Me._id = value
            End Set
        End Property

        ' ''' <summary>
        ' ''' Propiedad que asigna u obtiene docmuento español
        ' ''' </summary>
        Public Overridable Property Des_guia() As String
            Get
                Return Me._des_guia
            End Get
            Set(ByVal value As String)
                Me._des_guia = value
            End Set
        End Property

        ' ''' <summary>
        ' ''' Propiedad que asigna u obtiene docmuento ingles
        ' ''' </summary>
        Public Overridable Property Fec_guia() As DateTime
            Get
                Return Me._fec_guia
            End Get
            Set(ByVal value As DateTime)
                Me._fec_guia = value
            End Set
        End Property

#End Region

    End Class
End Namespace