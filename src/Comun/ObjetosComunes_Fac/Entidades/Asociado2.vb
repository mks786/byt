Imports System.Collections.Generic
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace Entidades
    <Serializable()> _
    Public Class Asociado2
#Region "Atributos"

        Private _id As Integer
        Private _nombre As String
        Private _tipoPersona As Char
        Private _domicilio As String
        Private _pais As Pais
        Private _contribuyente As String
        Private _rif As String
        Private _nit As String
        Private _idioma As Idioma
        Private _moneda As Moneda
        Private _descuento As Single
        Private _telefono1 As String
        Private _telefono2 As String
        Private _telefono3 As String
        Private _fax1 As String
        Private _fax2 As String
        Private _fax3 As String
        Private _email As String
        Private _web As String
        Private _diaCredito As Integer
        Private _tipoCliente As TipoCliente
        Private _activo As String
        Private _edoCuenta As Char
        Private _edoCuentaDigital As String
        Private _pendienteStatement As Char
        Private _isf As Char
        Private _alerta As Char
        Private _alarmaDescripcion As String
        Private _tarifa As Tarifa
        Private _etiqueta As Etiqueta
        Private _detallePago As DetallePago
        Private _operacion As String
        Private _justificaciones As IList(Of Justificacion)
        Private _contactos As IList(Of Contacto)
        Private _cartas As IList(Of Carta)
        Private _datosTransferencias As IList(Of DatosTransferencia)

#End Region

#Region "Constructores"

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        Public Sub New()
            Me._activo = "NO"
            Me._contribuyente = "NO"
            Me._edoCuentaDigital = "NO"
            Me._edoCuenta = "F"c
            Me._isf = "F"c
            Me._alerta = "F"c
        End Sub

        ''' <summary>
        ''' Constructor que inicializa el condigo del asociado
        ''' </summary>
        ''' <param name="codigo">Codigo del asociado</param>
        Public Sub New(ByVal id As Integer)
            Me._id = id
        End Sub

#End Region

#Region "Propiedades"

        ''' <summary>
        ''' Propiedad que asigna u obtiene el código del asociado
        ''' </summary>
        Public Overridable Property Id() As Integer
            Get
                Return Me._id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el nombre del asociado
        ''' </summary>
        Public Overridable Property Nombre() As String
            Get
                Return Me._nombre
            End Get
            Set(ByVal value As String)
                Me._nombre = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el tipo de perosna que es el asociado
        ''' </summary>
        Public Overridable Property TipoPersona() As Char
            Get
                Return _tipoPersona
            End Get
            Set(ByVal value As Char)
                _tipoPersona = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el domicilio del asociado
        ''' </summary>
        Public Overridable Property Domicilio() As String
            Get
                Return _domicilio
            End Get
            Set(ByVal value As String)
                _domicilio = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el pais del asociado
        ''' </summary>
        Public Overridable Property Pais() As Pais
            Get
                Return _pais
            End Get
            Set(ByVal value As Pais)
                _pais = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene si un asociado es contribuyente o no
        ''' </summary>
        Public Overridable Property Contribuyente() As String
            Get
                Return _contribuyente
            End Get
            Set(ByVal value As String)
                _contribuyente = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de contribuyente
        ''' </summary>
        Public Overridable Property BContribuyente() As Boolean
            Get
                If Me.Contribuyente IsNot Nothing AndAlso Me.Contribuyente.Equals("SI") Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Contribuyente = "SI"
                Else
                    Me.Contribuyente = "NO"
                End If
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el rif del asociado
        ''' </summary>
        Public Overridable Property Rif() As String
            Get
                Return _rif
            End Get
            Set(ByVal value As String)
                _rif = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el nit del asociado
        ''' </summary>
        Public Overridable Property Nit() As String
            Get
                Return _nit
            End Get
            Set(ByVal value As String)
                _nit = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el idioma del asociado
        ''' </summary>
        Public Overridable Property Idioma() As Idioma
            Get
                Return _idioma
            End Get
            Set(ByVal value As Idioma)
                _idioma = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el moneda del asociado
        ''' </summary>
        Public Overridable Property Moneda() As Moneda
            Get
                Return _moneda
            End Get
            Set(ByVal value As Moneda)
                _moneda = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el descuento del asociado
        ''' </summary>
        Public Overridable Property Descuento() As Single
            Get
                Return _descuento
            End Get
            Set(ByVal value As Single)
                _descuento = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el telefono1 del asociado
        ''' </summary>
        Public Overridable Property Telefono1() As String
            Get
                Return _telefono1
            End Get
            Set(ByVal value As String)
                _telefono1 = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el telefono2 del asociado
        ''' </summary>
        Public Overridable Property Telefono2() As String
            Get
                Return _telefono2
            End Get
            Set(ByVal value As String)
                _telefono2 = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el telefono3 del asociado
        ''' </summary>
        Public Overridable Property Telefono3() As String
            Get
                Return _telefono3
            End Get
            Set(ByVal value As String)
                _telefono3 = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el fax1 del asociado
        ''' </summary>
        Public Overridable Property Fax1() As String
            Get
                Return _fax1
            End Get
            Set(ByVal value As String)
                _fax1 = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el fax2 del asociado
        ''' </summary>
        Public Overridable Property Fax2() As String
            Get
                Return _fax2
            End Get
            Set(ByVal value As String)
                _fax2 = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el fax3 del asociado
        ''' </summary>
        Public Overridable Property Fax3() As String
            Get
                Return _fax3
            End Get
            Set(ByVal value As String)
                _fax3 = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el email del asociado
        ''' </summary>
        Public Overridable Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el web del asociado
        ''' </summary>
        Public Overridable Property Web() As String
            Get
                Return _web
            End Get
            Set(ByVal value As String)
                _web = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene los dias de creditos del asociado
        ''' </summary>
        Public Overridable Property DiaCredito() As Integer
            Get
                Return _diaCredito
            End Get
            Set(ByVal value As Integer)
                _diaCredito = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el tipo del asociado
        ''' </summary>
        Public Overridable Property TipoCliente() As TipoCliente
            Get
                Return _tipoCliente
            End Get
            Set(ByVal value As TipoCliente)
                _tipoCliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene si el asociado esta activo
        ''' </summary>
        Public Overridable Property Activo() As String
            Get
                Return _activo
            End Get
            Set(ByVal value As String)
                _activo = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano de activo
        ''' </summary>
        Public Overridable Property BActivo() As Boolean
            Get
                If Me.Activo IsNot Nothing AndAlso Me.Activo.Equals("SI") Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Activo = "SI"
                Else
                    Me.Activo = "NO"
                End If
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene si se le manda estados de cuenta
        ''' </summary>
        Public Overridable Property EdoCuenta() As Char
            Get
                Return _edoCuenta
            End Get
            Set(ByVal value As Char)
                _edoCuenta = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano del estado de cuenta
        ''' </summary>
        Public Overridable Property BEdoCuenta() As Boolean
            Get
                If Me.EdoCuenta.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.EdoCuenta = "T"c
                Else
                    Me.EdoCuenta = "F"c
                End If
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene si se le manda estados de cuentas digitales
        ''' </summary>
        Public Overridable Property EdoCuentaDigital() As String
            Get
                Return _edoCuentaDigital
            End Get
            Set(ByVal value As String)
                _edoCuentaDigital = value
            End Set
        End Property


        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano del estado de cuenta digital
        ''' </summary>
        Public Overridable Property BEdoCuentaDigital() As Boolean
            Get
                If Me.EdoCuentaDigital IsNot Nothing AndAlso Me.EdoCuentaDigital.Equals("SI") Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.EdoCuentaDigital = "SI"
                Else
                    Me.EdoCuentaDigital = "NO"
                End If
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene si esta pendiente con statement
        ''' </summary>
        Public Overridable Property PendienteStatement() As Char
            Get
                Return _pendienteStatement
            End Get
            Set(ByVal value As Char)
                _pendienteStatement = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano del estado de pendiente con statement
        ''' </summary>
        Public Overridable Property BPendienteStatement() As Boolean
            Get
                If Me.PendienteStatement.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.PendienteStatement = "T"c
                Else
                    Me.PendienteStatement = "F"c
                End If
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene si tiene isf
        ''' </summary>
        Public Overridable Property Isf() As Char
            Get
                Return _isf
            End Get
            Set(ByVal value As Char)
                _isf = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano del estado de Isf
        ''' </summary>
        Public Overridable Property BIsf() As Boolean
            Get
                If Me.Isf.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Isf = "T"c
                Else
                    Me.Isf = "F"c
                End If
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene si tiene alerta
        ''' </summary>
        Public Overridable Property Alerta() As Char
            Get
                Return _alerta
            End Get
            Set(ByVal value As Char)
                _alerta = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el booleano del estado de Alerta
        ''' </summary>
        Public Overridable Property BAlerta() As Boolean
            Get
                If Me.Alerta.Equals("T"c) Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    Me.Alerta = "T"c
                Else
                    Me.Alerta = "F"c
                End If
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene descripcion de la alerta
        ''' </summary>
        Public Overridable Property AlarmaDescripcion() As String
            Get
                Return _alarmaDescripcion
            End Get
            Set(ByVal value As String)
                _alarmaDescripcion = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene la tarifa del asociado
        ''' </summary>
        Public Overridable Property Tarifa() As Tarifa
            Get
                Return _tarifa
            End Get
            Set(ByVal value As Tarifa)
                _tarifa = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene la etiqueta del asociado
        ''' </summary>
        Public Overridable Property Etiqueta() As Etiqueta
            Get
                Return _etiqueta
            End Get
            Set(ByVal value As Etiqueta)
                _etiqueta = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene el detalle de pago asociado
        ''' </summary>
        Public Overridable Property DetallePago() As DetallePago
            Get
                Return _detallePago
            End Get
            Set(ByVal value As DetallePago)
                _detallePago = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene la operacion
        ''' </summary>
        Public Overridable Property Operacion() As String
            Get
                Return _operacion
            End Get
            Set(ByVal value As String)
                _operacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propiedad que asigna u obtiene la lista de justificaciones
        ''' de un asociado
        ''' </summary>
        Public Overridable Property Justificaciones() As IList(Of Justificacion)
            Get
                Return _justificaciones
            End Get
            Set(ByVal value As IList(Of Justificacion))
                _justificaciones = value
            End Set
        End Property

        Public Overridable Property Contactos() As IList(Of Contacto)
            Get
                Return _contactos
            End Get
            Set(ByVal value As IList(Of Contacto))
                _contactos = value
            End Set
        End Property

        Public Overridable Property Cartas() As IList(Of Carta)
            Get
                Return _cartas
            End Get
            Set(ByVal value As IList(Of Carta))
                _cartas = value
            End Set
        End Property

        Public Overridable Property DatosTransferencias() As IList(Of DatosTransferencia)
            Get
                Return _datosTransferencias
            End Get
            Set(ByVal value As IList(Of DatosTransferencia))
                _datosTransferencias = value
            End Set
        End Property

#End Region
    End Class
End Namespace
