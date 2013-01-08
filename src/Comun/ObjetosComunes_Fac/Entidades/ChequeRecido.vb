Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace Entidades
    <Serializable()> _
    Public Class ChequeRecido
#Region "Atributos"
        Private _id As Asociado
        Private _ncheque As String
        Private _fecha As System.Nullable(Of DateTime)
        Private _bancog As BancoG
        Private _monto As Double
        Private _deposito As String
        Private _fechadeposito As DateTime
        Private _ndeposito As String
        Private _banco As BancoG
        Private _fechareg As System.Nullable(Of DateTime)

#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Constructor que inicializa el Id de la ChequeRecido
        ' ''' </summary>
        ' ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As Asociado, ByVal ncheque As String)
            Me._id = id
            Me._ncheque = ncheque
        End Sub


#End Region

#Region "Propiedades"
        ' para llave compuesta
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then
                Return False
            End If
            Dim t = TryCast(obj, ChequeRecido)
            If t Is Nothing Then
                Return False
            End If
            If (Id.Id = (t.Id.Id)) AndAlso (NCheque = t.NCheque) Then
                'If (Doc_servicio = t.Doc_servicio) Then
                Return True
            End If
            Return False
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return MyBase.GetHashCode()
        End Function

        Public Overrides Function ToString() As String
            Return MyBase.ToString()
        End Function

        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la ChequeRecido
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
        Public Overridable Property NCheque() As String
            Get
                Return Me._ncheque
            End Get
            Set(ByVal value As String)
                Me._ncheque = value
            End Set
        End Property

        Public Overridable Property Fecha() As System.Nullable(Of DateTime)
            Get
                Return Me._fecha
            End Get
            Set(ByVal value As System.Nullable(Of DateTime))
                Me._fecha = value
            End Set
        End Property

        ' ''' <summary>
        ' ''' Propiedad que asigna u obtiene 
        ' ''' </summary>
        Public Overridable Property BancoG() As BancoG
            Get
                Return Me._bancog
            End Get
            Set(ByVal value As BancoG)
                Me._bancog = value
            End Set
        End Property

        Public Overridable Property Monto() As Double
            Get
                Return Me._monto
            End Get
            Set(ByVal value As Double)
                Me._monto = value
            End Set
        End Property

        Public Overridable Property Deposito() As String
            Get
                Return Me._deposito
            End Get
            Set(ByVal value As String)
                Me._deposito = value
            End Set
        End Property

        Public Overridable Property FechaDeposito() As DateTime
            Get
                Return Me._fechadeposito
            End Get
            Set(ByVal value As DateTime)
                Me._fechadeposito = value
            End Set
        End Property

        Public Overridable Property NDeposito() As Integer
            Get
                Return Me._ndeposito
            End Get
            Set(ByVal value As Integer)
                Me._ndeposito = value
            End Set
        End Property

        Public Overridable Property Banco() As BancoG
            Get
                Return Me._banco
            End Get
            Set(ByVal value As BancoG)
                Me._banco = value
            End Set
        End Property

        Public Overridable Property FechaReg() As System.Nullable(Of DateTime)
            Get
                Return _fechareg
            End Get
            Set(ByVal value As System.Nullable(Of DateTime))
                _fechareg = value
            End Set
        End Property
#End Region

    End Class
End Namespace