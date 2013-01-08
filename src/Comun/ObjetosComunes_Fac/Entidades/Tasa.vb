Imports System
Imports System.Collections.Generic
Namespace Entidades
    <Serializable()> _
    Public Class Tasa
#Region "Atributos"

        Private _id As DateTime
        Private _moneda As String
        Private _tasabf As Single
        Private _tasabs As Single

#End Region

#Region "Constructores"


        ''' <summary>
        ''' Constructor que inicializa el Id de la tasa
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As DateTime, ByVal cmoneda As String)
            Me._id = id
            Me._moneda = cmoneda
        End Sub

#End Region

#Region "Propiedades"

        'para llave compuesta
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then
                Return False
            End If
            Dim t = TryCast(obj, Tasa)
            If t Is Nothing Then
                Return False
            End If
            If (Id = (t.Id)) AndAlso (Moneda = t.Moneda) Then
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
        ''' Propiedad que asigna u obtiene el id de la tasa 
        ''' </summary>
        Public Overridable Property Id() As DateTime
            Get
                Return Me._id
            End Get
            Set(ByVal value As DateTime)
                Me._id = value
            End Set
        End Property
        ''' <summary>
        ''' Propiedad que asigna u obtiene el código de la moneda
        ''' </summary>
        Public Overridable Property Moneda() As String
            Get
                Return Me._moneda
            End Get
            Set(ByVal value As String)
                Me._moneda = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el valor en BF
        ''' </summary>
        Public Overridable Property Tasabf() As Single
            Get
                Return Me._tasabf
            End Get
            Set(ByVal value As Single)
                Me._tasabf = value
            End Set
        End Property

        Public Overridable Property Tasabs() As Single
            Get
                Return Me._tasabs
            End Get
            Set(ByVal value As Single)
                Me._tasabs = value
            End Set
        End Property

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        Public Sub New()
        End Sub

#End Region
    End Class
End Namespace