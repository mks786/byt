Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.Tasas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.Tasas
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.Tasas
    Partial Public Class AgregarTasa
        Inherits Page
        Implements IAgregarTasa

        Private _presentador As PresentadorAgregartasa
        Private _cargada As Boolean

#Region "IAgregarTasa"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._dpkId.Focus()
        End Sub

        Public Property Tasa() As Object Implements Contratos.Tasas.IAgregarTasa.Tasa
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.Tasas.IAgregarTasa.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub


#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorAgregartasa(Me)
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

        Public Property Moneda As String Implements Contratos.Tasas.IAgregarTasa.Moneda
            Get
                Return _cbxMoneda.Text
            End Get
            Set(value As String)
                _cbxMoneda.Text = value
            End Set
        End Property
    End Class
End Namespace