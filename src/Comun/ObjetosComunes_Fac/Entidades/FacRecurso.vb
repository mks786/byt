Namespace Entidades
    <Serializable()> _
    Public Class FacRecurso
#Region "Atributos"

        Private _id As String
        Private _doc_esp As String
        Private _doc_ingl As String
#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la FacRecurso
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As String)
            Me._id = id
        End Sub
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la FacRecurso
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
        ''' Propiedad que asigna u obtiene docmuento español
        ''' </summary>
        Public Overridable Property Doc_Esp() As String
            Get
                Return Me._doc_esp
            End Get
            Set(ByVal value As String)
                Me._doc_esp = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene docmuento ingles
        ''' </summary>
        Public Overridable Property Doc_Ingl() As String
            Get
                Return Me._doc_ingl
            End Get
            Set(ByVal value As String)
                Me._doc_ingl = value
            End Set
        End Property

#End Region

    End Class
End Namespace