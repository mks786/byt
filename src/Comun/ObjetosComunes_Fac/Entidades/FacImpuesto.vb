Imports System
Imports System.Collections.Generic
Namespace Entidades
    <Serializable()> _
    Public Class FacImpuesto
#Region "Atributos"

        Private _id As DateTime        
        Private _impuesto As Double        

#End Region

#Region "Constructores"


        ''' <summary>
        ''' Constructor que inicializa el Id de la FacImpuesto
        ''' </summary>
        ''' <param name="id">Id de la FacImpuesto</param>
        Public Sub New(ByVal id As DateTime)
            Me._id = id
        End Sub

#End Region

#Region "Propiedades"

        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la FacImpuesto 
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
        ''' Propiedad que asigna u obtiene el valor en BF
        ''' </summary>
        Public Overridable Property Impuesto() As Double
            Get
                Return Me._impuesto
            End Get
            Set(ByVal value As Double)
                Me._impuesto = value
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