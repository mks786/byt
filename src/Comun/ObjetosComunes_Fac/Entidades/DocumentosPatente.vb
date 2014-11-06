Namespace Entidades
    <Serializable()> _
    Public Class DocumentosPatente
#Region "Atributos"

        Private _id As String
        Private _doc_esp As String
        Private _doc_ingl As String
        Private _mont_bs As Double
        Private _mont_us As Double
        Private _mont_bf As Double
        Private _montalt_us As Double
        Private _montalt_bf As Double
#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la DocumentosPatente
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As String)
            Me._id = id
        End Sub
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la documentacion Patente
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

        Public Overridable Property Mont_Bs() As Double
            Get
                Return Me._mont_bs
            End Get
            Set(ByVal value As Double)
                Me._mont_bs = value
            End Set
        End Property

        Public Overridable Property Mont_Us() As Double
            Get
                Return Me._mont_us
            End Get
            Set(ByVal value As Double)
                Me._mont_us = value
            End Set
        End Property

        Public Overridable Property Mont_Bf() As Double
            Get
                Return Me._mont_bf
            End Get
            Set(ByVal value As Double)
                Me._mont_bf = value
            End Set
        End Property

        Public Overridable Property MontAlt_Us() As Double
            Get
                Return Me._montalt_us
            End Get
            Set(ByVal value As Double)
                Me._montalt_us = value
            End Set
        End Property

        Public Overridable Property MontAlt_Bf() As Double
            Get
                Return Me._montalt_bf
            End Get
            Set(ByVal value As Double)
                Me._montalt_bf = value
            End Set
        End Property

#End Region

    End Class
End Namespace