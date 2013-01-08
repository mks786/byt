Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace Entidades
    <Serializable()> _
    Public Class DesgloseServicio
#Region "Atributos"

        Private _id As Char
        Private _servicio As FacServicio
        Private _pporc As Double

#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la DesgloseServicio
        ''' </summary>
        ''' <param name="id">Id de la DesgloseServicio</param>
        Public Sub New(ByVal id As Char, ByVal servicio As FacServicio)
            Me._id = id
            Me._servicio = servicio
        End Sub
#End Region

#Region "Propiedades"

        'para llave compuesta
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then
                Return False
            End If
            Dim t = TryCast(obj, DesgloseServicio)
            If t Is Nothing Then
                Return False
            End If
            If (Id = (t.Id)) AndAlso (Servicio.Id = t.Servicio.Id) Then
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
        ''' Propiedad que asigna u obtiene el id de la DesgloseServicio
        ''' </summary>
        Public Overridable Property Id() As Char
            Get
                Return Me._id
            End Get
            Set(ByVal value As Char)
                Me._id = value
            End Set
        End Property

        Public Overridable Property Servicio() As FacServicio
            Get
                Return Me._servicio
            End Get
            Set(ByVal value As FacServicio)
                Me._servicio = value
            End Set
        End Property

        Public Overridable Property Pporc() As Double
            Get
                Return Me._pporc
            End Get
            Set(ByVal value As Double)
                Me._pporc = value
            End Set
        End Property

#End Region

    End Class
End Namespace