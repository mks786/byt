Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.NumeroControl
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.NumeroControl
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.NumeroControl
    Partial Public Class NumeroControl
        Inherits Page
        Implements INumeroControl

        Private _presentador As PresentadorNumeroControl
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
            ' Me._dpkId.Focus()
        End Sub


        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.NumeroControl.INumeroControl.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub


#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorNumeroControl(Me)
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

        'Public Property Moneda As String Implements Contratos.Tasas.IAgregarTasa.Moneda
        '    Get
        '        Return _cbxMoneda.Text
        '    End Get
        '    Set(ByVal value As String)
        '        _cbxMoneda.Text = value
        '    End Set
        'End Property

        Public Property NumeroControl As String Implements Contratos.NumeroControl.INumeroControl.NumeroControl
            Get
                Return _txtNumeroControl.Text
            End Get
            Set(ByVal value As String)
                _txtNumeroControl.Text = value
            End Set
        End Property


    End Class
End Namespace