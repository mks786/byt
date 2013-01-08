Namespace Entidades
    <Serializable()> _
    Public Class FacJustificacion
#Region "Atributos"

        Private _id As String
        Private _concepto As String
#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la FacJustificacion
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As String)
            Me._id = id
        End Sub
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la FacJustificacion
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
        ''' Propiedad que asigna u obtiene Concepto
        ''' </summary>
        Public Overridable Property Concepto() As String
            Get
                Return Me._concepto
            End Get
            Set(ByVal value As String)
                Me._concepto = value
            End Set
        End Property
#End Region
    End Class
End Namespace