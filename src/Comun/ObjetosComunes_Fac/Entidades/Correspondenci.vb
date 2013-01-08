Namespace Entidades
    <Serializable()> _
    Public Class Correspondencia
#Region "Atributos"

        Private _id As Integer
        Private _xfrom As String
        Private _xto As String
        Private _xcc As String
        Private _xcco As String
        Private _subject As String
        Private _fecha_cor As datetime
        Private _fecha_ing As datetime
        Private _fecha As datetime
        Private _cuerpo As String
        Private _status As Char
        Private _xattlis As String
        Private _xdir As String
        Private _idx As String




#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la Correspondencia
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As Integer)
            Me._id = id
        End Sub
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la Correspondencia
        ''' </summary>
        Public Overridable Property Id() As Integer
            Get
                Return Me._id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property


        Public Overridable Property Xfrom() As String
            Get
                Return Me._xfrom
            End Get
            Set(ByVal value As String)
                Me._xfrom = value
            End Set
        End Property


        Public Overridable Property Xto() As String
            Get
                Return Me._xto
            End Get
            Set(ByVal value As String)
                Me._xto = value
            End Set
        End Property

        Public Overridable Property Xcc() As String
            Get
                Return Me._xcc
            End Get
            Set(ByVal value As String)
                Me._xcc = value
            End Set
        End Property

        Public Overridable Property Xcco() As String
            Get
                Return Me._xcco
            End Get
            Set(ByVal value As String)
                Me._xcco = value
            End Set
        End Property


        Public Overridable Property Subject() As String
            Get
                Return Me._subject
            End Get
            Set(ByVal value As String)
                Me._subject = value
            End Set
        End Property

        Public Overridable Property Fecha_cor() As DateTime
            Get
                Return Me._fecha_cor
            End Get
            Set(ByVal value As DateTime)
                Me._fecha_cor = value
            End Set
        End Property


        Public Overridable Property Fecha_Ing() As DateTime
            Get
                Return Me._fecha_ing
            End Get
            Set(ByVal value As DateTime)
                Me._fecha_ing = value
            End Set
        End Property


        Public Overridable Property fecha() As DateTime
            Get
                Return Me._fecha_cor
            End Get
            Set(ByVal value As DateTime)
                Me._fecha = value
            End Set
        End Property

        Public Overridable Property Cuerpo() As String
            Get
                Return Me._cuerpo
            End Get
            Set(ByVal value As String)
                Me._cuerpo = value
            End Set
        End Property


        Public Overridable Property Status() As Char
            Get
                Return Me._status
            End Get
            Set(ByVal value As Char)
                Me._status = value
            End Set
        End Property

        Public Overridable Property Xattlis() As String
            Get
                Return Me._xattlis
            End Get
            Set(ByVal value As String)
                Me._xattlis = value
            End Set
        End Property

        Public Overridable Property Xdir() As String
            Get
                Return Me._xdir
            End Get
            Set(ByVal value As String)
                Me._xdir = value
            End Set
        End Property

        Public Overridable Property Idx() As String
            Get
                Return Me._idx
            End Get
            Set(ByVal value As String)
                Me._idx = value
            End Set
        End Property

#End Region

    End Class
End Namespace