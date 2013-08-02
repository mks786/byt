Namespace Entidades
    <Serializable()> _
    Public Class CarpetaGestionAutomatica
#Region "Atributos"

        Private _id As String
        Private _iniciales As String
        Private _nombreUsuario As String
        Private _carpeta As String
        Private _prioridad As Integer

#End Region

#Region "Constructores"
        ''' <summary>
        ''' Constructor por defecto de la entidad
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor por defecto de la entidad donde se inicializa por las iniciales de la carpeta de Gestion Automatica
        ''' </summary>
        Public Sub New(ByVal iniciales As String)
            Me._iniciales = iniciales
        End Sub
#End Region

#Region "Propiedades"

        ''' <summary>
        ''' Propiedad que asigna u obtiene el Id en la CarpetaGestionAutomatica
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
        ''' Propiedad que asigna u obtiene las Iniciales de la CarpetaGestionAutomatica
        ''' </summary>
        Public Overridable Property Iniciales() As String
            Get
                Return Me._iniciales
            End Get
            Set(ByVal value As String)
                Me._iniciales = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el Nombre del usuario propietario de la CarpetaGestionAutomatica
        ''' </summary>
        Public Overridable Property NombreUsuario() As String
            Get
                Return Me._nombreUsuario
            End Get
            Set(ByVal value As String)
                Me._nombreUsuario = value
            End Set
        End Property
        ''' <summary>
        ''' Propiedad que asigna u obtiene el nombre del carpeta en la entidad CarpetaGestionAutomatica
        ''' </summary>
        Public Overridable Property Carpeta() As String
            Get
                Return Me._carpeta
            End Get
            Set(ByVal value As String)
                Me._carpeta = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene la prioridad de la carpeta en la entidad CarpetaGestionAutomatica
        ''' </summary>
        Public Overridable Property Prioridad() As Integer
            Get
                Return Me._prioridad
            End Get
            Set(ByVal value As Integer)
                Me._prioridad = value
            End Set
        End Property
#End Region

    End Class

End Namespace