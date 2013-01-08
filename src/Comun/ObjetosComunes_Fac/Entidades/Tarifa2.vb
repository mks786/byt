Imports System.Collections.Generic

Namespace Entidades
    <Serializable()> _
    Public Class Tarifa2
#Region "Atributos"

        Private _id As String
        Private _descripcion As String

#End Region

#Region "Constructores"


        Public Sub New()
        End Sub


        Public Sub New(ByVal id As String)
            Me._id = id
        End Sub

#End Region

#Region "Propiedades"

        Public Overridable Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal value As String)
                Me._id = value
            End Set
        End Property

        Public Overridable Property Descripcion() As String
            Get
                Return Me._descripcion
            End Get
            Set(ByVal value As String)
                Me._descripcion = value
            End Set
        End Property

#End Region
    End Class
End Namespace