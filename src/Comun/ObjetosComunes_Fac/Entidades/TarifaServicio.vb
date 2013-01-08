Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace Entidades
    <Serializable()> _
    Public Class TarifaServicio
#Region "Atributos"
        Private _id As String
        Private _tarifa As Tarifa2
        Private _mont_us As Double
        Private _mont_bs As Double
        Private _tasa As Double
        Private _mont_bf As Double
#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la TarifaServicio
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As String, ByVal tarifa As Tarifa2)
            Me._id = id
            Me._tarifa = tarifa
        End Sub
#End Region

#Region "Propiedades"

        ' para llave compuesta
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then
                Return False
            End If
            Dim t = TryCast(obj, TarifaServicio)
            If t Is Nothing Then
                Return False
            End If
            If (Id = (t.Id)) AndAlso (Tarifa.Id = t.Tarifa.Id) Then
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
        ''' Propiedad que asigna u obtiene el id de la Tarifa Servicio
        ''' </summary>
        Public Overridable Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal value As String)
                Me._id = value
            End Set
        End Property

        Public Overridable Property Tarifa() As Tarifa2
            Get
                Return Me._tarifa
            End Get
            Set(ByVal value As Tarifa2)
                Me._tarifa = value
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

        Public Overridable Property Tasa() As Double
            Get
                Return Me._tasa
            End Get
            Set(ByVal value As Double)
                Me._tasa = value
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

#End Region

    End Class
End Namespace