Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace Entidades
    <Serializable()> _
    Public Class ViGestionAsociado
#Region "Atributos"
        Private _id As Asociado
        Private _cantidad As Double
        Private _fechaultima As DateTime
        Private _moneda As String
        Private _saldo As Double
#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Constructor que inicializa el Id de la ViGestionAsociado
        ' ''' </summary>
        ' ''' <param name="id">Id de la tasa</param>
        'Public Sub New(ByVal id As Integer)
        '    Me._id = id
        'End Sub


#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la ViGestionAsociado
        ''' </summary>
        Public Overridable Property Id() As Asociado
            Get
                Return Me._id
            End Get
            Set(ByVal value As Asociado)
                Me._id = value
            End Set
        End Property

        ' ''' <summary>
        ' ''' Propiedad que asigna u obtiene 
        ' ''' </summary>
        Public Overridable Property Cantidad() As Double
            Get
                Return Me._cantidad
            End Get
            Set(ByVal value As Double)
                Me._cantidad = value
            End Set
        End Property

        Public Overridable Property FechaUltima() As DateTime
            Get
                Return Me._fechaultima
            End Get
            Set(ByVal value As DateTime)
                Me._fechaultima = value
            End Set
        End Property

        ' ''' <summary>
        ' ''' Propiedad que asigna u obtiene 
        ' ''' </summary>
        Public Overridable Property Moneda() As String
            Get
                Return Me._moneda
            End Get
            Set(ByVal value As String)
                Me._moneda = value
            End Set
        End Property

        Public Overridable Property Saldo() As Double
            Get
                Return Me._saldo
            End Get
            Set(ByVal value As Double)
                Me._saldo = value
            End Set
        End Property
#End Region

    End Class
End Namespace