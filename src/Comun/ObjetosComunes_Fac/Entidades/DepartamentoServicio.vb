Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace Entidades
    <Serializable()> _
    Public Class DepartamentoServicio
#Region "Atributos"

        Private _id As Departamento
        Private _doc_servicio As FacServicio

#End Region

#Region "Constructores"
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la DepartamentoServicio
        ''' </summary>
        ''' <param name="id">Id de la tasa</param>
        Public Sub New(ByVal id As Departamento, ByVal doc_servicio As FacServicio)
            Me._id = id
            Me._doc_servicio = doc_servicio
        End Sub
#End Region

#Region "Propiedades"

        'para llave compuesta
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then
                Return False
            End If
            Dim t = TryCast(obj, DepartamentoServicio)
            If t Is Nothing Then
                Return False
            End If
            If (Id.Id = (t.Id.Id)) AndAlso (Doc_Servicio.Id = t.Doc_Servicio.Id) Then
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
        ''' Propiedad que asigna u obtiene el id del Departamento Servicio
        ''' </summary>
        Public Overridable Property Id() As Departamento
            Get
                Return Me._id
            End Get
            Set(ByVal value As Departamento)
                Me._id = value
            End Set
        End Property

        Public Overridable Property Doc_Servicio() As FacServicio
            Get
                Return Me._doc_servicio
            End Get
            Set(ByVal value As FacServicio)
                Me._doc_servicio = value
            End Set
        End Property

#End Region

    End Class
End Namespace