Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacBancos
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacBancos
Imports Diginsoft.Bolet.Cliente.Fac.Contratos

Namespace Ventanas.FacBancos
    Partial Public Class AgregarFacBanco
        Inherits Page
        Implements IAgregarFacBanco

        Private _presentador As PresentadorAgregarFacBanco
        Private _cargada As Boolean

#Region "IAgregarFacBanco"

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

        Public Property FacBanco() As Object Implements Contratos.FacBancos.IAgregarFacBanco.FacBanco
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.FacBancos.IAgregarFacBanco.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub


#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorAgregarFacBanco(Me)
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

        Public Property Moneda As Object Implements Contratos.FacBancos.IAgregarFacBanco.Moneda
            Get
                Return Me._cbxMoneda.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxMoneda.SelectedItem = value
            End Set
        End Property

        Public Property Monedas As Object Implements Contratos.FacBancos.IAgregarFacBanco.Monedas
            Get
                Return Me._cbxMoneda.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxMoneda.DataContext = value
            End Set
        End Property

        Public Property Publica As String Implements Contratos.FacBancos.IAgregarFacBanco.Publica
            Get
                Return _cbxIIw.Text
            End Get
            Set(ByVal value As String)
                _cbxIIw.Text = value
            End Set
        End Property
    End Class
End Namespace
