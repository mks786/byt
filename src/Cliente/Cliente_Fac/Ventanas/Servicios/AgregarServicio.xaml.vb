Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.Servicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.Servicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.Servicios
    Partial Public Class AgregarServicio
        Inherits Page
        Implements IAgregarServicio

        Private _presentador As PresentadorAgregarServicio
        Private _cargada As Boolean

#Region "IAgregarServicio"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._txtId.Focus()
        End Sub

        'Combox
        Public ReadOnly Property Tipo() As Char Implements IAgregarServicio.Tipo
            Get
                If Not String.IsNullOrEmpty(Me._cbxItipo.Text) Then
                    Return (Me._cbxItipo.Text)(0)
                Else
                    Return " "c
                End If
            End Get
        End Property

        Public ReadOnly Property Localidad() As Char Implements IAgregarServicio.Localidad
            Get
                If Not String.IsNullOrEmpty(Me._cbxLocal.Text) Then
                    Return (Me._cbxLocal.Text)(0)
                Else
                    Return " "c
                End If
            End Get
        End Property

        Public ReadOnly Property EstructurasMultiples() As String Implements IAgregarServicio.EstructurasMultiples
            Get
                If Not String.Equals("", Me._cbxCodmult.Text) Then
                    Return DirectCast(Me._cbxCodmult.Text, String)
                End If
                Return ""
            End Get
        End Property

        'Public ReadOnly Property EstructurasMultiples() As String
        '    Get
        '        If Not String.IsNullOrEmpty(Me._cbxCodmult.Text) Then
        '            Return (Me._cbxCodmult.Text)(0)
        '        Else
        '            Return " "c
        '        End If
        '    End Get
        'End Property
        'fin Combox

        Public Property Servicio() As Object Implements Contratos.Servicios.IAgregarServicio.Servicio
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.Servicios.IAgregarServicio.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub


#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorAgregarServicio(Me)
        End Sub

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
        End Sub

        Private Sub _btnAceptar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Aceptar()
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            End If
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

    End Class
End Namespace
