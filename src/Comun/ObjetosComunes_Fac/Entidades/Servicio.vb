Namespace Entidades
    <Serializable()> _
    Public Class Servicio
#Region "Atributos"

        Private _id As String
        Private _cod_cont As String
        Private _detalle_esp As String
        Private _detalle_ing As String
        Private _itipo As Char
        Private _local As Char
        Private _itidoc As Char
        Private _itraduc As Char
        Private _anual As Char
        Private _detalles_esp As String
        Private _detalles_ing As String
        Private _codmult As String
        Private _xreferencia As String
        Private _imult As Char
        Private _imodpr As Char
        Private _recursos As Char
        Private _material As Char
        Private _aimpuesto As Char
        Private _desg As Char
#End Region

#Region "Constructores"
        Public Sub New()
            Me._itidoc = "F"c
            Me._itraduc = "F"c
            Me._anual = "F"c
            Me._imult = "F"c
            Me._imodpr = "F"c
            Me._recursos = "F"c
            Me.Material = "F"c
            Me._aimpuesto = "F"c
            Me._desg = "F"c
        End Sub
        ''' <summary>
        ''' Constructor que inicializa el Id de la Servicio
        ''' </summary>
        ''' <param name="id">Id de la servicioa</param>
        Public Sub New(ByVal id As String)
            Me._id = id
        End Sub
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad que asigna u obtiene el id de la documentacion Marca
        ''' </summary>
        Public Overridable Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal value As String)
                Me._id = value
            End Set
        End Property


        Public Overridable Property Cod_Cont() As String
            Get
                Return Me._cod_cont
            End Get
            Set(ByVal value As String)
                Me._cod_cont = value
            End Set
        End Property


        Public Overridable Property Detalle_Esp() As String
            Get
                Return Me._detalle_esp
            End Get
            Set(ByVal value As String)
                Me._detalle_esp = value
            End Set
        End Property


        Public Overridable Property Detalle_Ing() As String
            Get
                Return Me._detalle_ing
            End Get
            Set(ByVal value As String)
                Me._detalle_ing = value
            End Set
        End Property

        Public Overridable Property Itipo() As Char
            Get
                Return Me._itipo
            End Get
            Set(ByVal value As Char)
                Me._itipo = value
            End Set
        End Property

        Public Overridable Property Local() As Char
            Get
                Return Me._local
            End Get
            Set(ByVal value As Char)
                Me._local = value
            End Set
        End Property

        Public Overridable Property Itidoc() As Char
            Get
                Return Me._itidoc
            End Get
            Set(ByVal value As Char)
                Me._itidoc = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de Itidoc
        ''' </summary>
        Public Overridable Property BItidoc() As Boolean
            Get
                If Me.Itidoc.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Itidoc = "T"c
                Else
                    Me.Itidoc = "F"c
                End If
            End Set
        End Property


        Public Overridable Property Itraduc() As Char
            Get
                Return Me._itraduc
            End Get
            Set(ByVal value As Char)
                Me._itraduc = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de Itraduc
        ''' </summary>
        Public Overridable Property BItraduc() As Boolean
            Get
                If Me.Itraduc.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Itraduc = "T"c
                Else
                    Me.Itraduc = "F"c
                End If
            End Set
        End Property


        Public Overridable Property Anual() As Char
            Get
                Return Me._anual
            End Get
            Set(ByVal value As Char)
                Me._anual = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de Anual
        ''' </summary>
        Public Overridable Property BAnual() As Boolean
            Get
                If Me.Anual.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Anual = "T"c
                Else
                    Me.Anual = "F"c
                End If
            End Set
        End Property


        Public Overridable Property Detalles_Esp() As String
            Get
                Return Me._detalles_esp
            End Get
            Set(ByVal value As String)
                Me._detalles_esp = value
            End Set
        End Property


        Public Overridable Property Detalles_Ing() As String
            Get
                Return Me._detalles_ing
            End Get
            Set(ByVal value As String)
                Me._detalles_ing = value
            End Set
        End Property


        Public Overridable Property Codmult() As String
            Get
                Return Me._codmult
            End Get
            Set(ByVal value As String)
                Me._codmult = value
            End Set
        End Property


        Public Overridable Property Xreferencia() As String
            Get
                Return Me._xreferencia
            End Get
            Set(ByVal value As String)
                Me._xreferencia = value
            End Set
        End Property


        Public Overridable Property Imult() As Char
            Get
                Return Me._imult
            End Get
            Set(ByVal value As Char)
                Me._imult = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de Imult
        ''' </summary>
        Public Overridable Property BImult() As Boolean
            Get
                If Me.Imult.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Imult = "T"c
                Else
                    Me.Imult = "F"c
                End If
            End Set
        End Property

        Public Overridable Property Imodpr() As Char
            Get
                Return Me._imodpr
            End Get
            Set(ByVal value As Char)
                Me._imodpr = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de Imodpr
        ''' </summary>
        Public Overridable Property BImodpr() As Boolean
            Get
                If Me.Imodpr.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Imodpr = "T"c
                Else
                    Me.Imodpr = "F"c
                End If
            End Set
        End Property


        Public Overridable Property Recursos() As Char
            Get
                Return Me._recursos
            End Get
            Set(ByVal value As Char)
                Me._recursos = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de Recursos
        ''' </summary>
        Public Overridable Property BRecursos() As Boolean
            Get
                If Me.Recursos.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Recursos = "T"c
                Else
                    Me.Recursos = "F"c
                End If
            End Set
        End Property


        Public Overridable Property Material() As Char
            Get
                Return Me._material
            End Get
            Set(ByVal value As Char)
                Me._material = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de Material
        ''' </summary>
        Public Overridable Property BMaterial() As Boolean
            Get
                If Me.Material.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Material = "T"c
                Else
                    Me.Material = "F"c
                End If
            End Set
        End Property

        Public Overridable Property Aimpuesto() As Char
            Get
                Return Me._aimpuesto
            End Get
            Set(ByVal value As Char)
                Me._aimpuesto = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de Aimpuesto
        ''' </summary>
        Public Overridable Property BAimpuesto() As Boolean
            Get
                If Me.Aimpuesto.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Aimpuesto = "T"c
                Else
                    Me.Aimpuesto = "F"c
                End If
            End Set
        End Property


        Public Overridable Property Desg() As Char
            Get
                Return Me._desg
            End Get
            Set(ByVal value As Char)
                Me._desg = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de Desg
        ''' </summary>
        Public Overridable Property BDesg() As Boolean
            Get
                If Me.Desg.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Desg = "T"c
                Else
                    Me.Desg = "F"c
                End If
            End Set
        End Property

#End Region

    End Class
End Namespace