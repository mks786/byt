Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacOperacionDetaProformas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacOperacionDetaProformas
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.FacOperacionDetaProformas
    Partial Public Class AgregarFacOperacionDetaProforma
        Inherits Page
        Implements IAgregarFacOperacionDetaProforma

        Private _presentador As PresentadorAgregarFacOperacionDetaProforma
        Private _cargada As Boolean

#Region "IAgregarFacOperacionDetaProforma"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._lstId.Focus()
        End Sub

        Public Property FacOperacionDetaProforma() As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.FacOperacionDetaProforma
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub


#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorAgregarFacOperacionDetaProforma(Me)
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


        Public Property Asociado As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Asociado

            Get
                Return Me._lstId.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstId.SelectedItem = value
                Me._lstId.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Asociados
            Get
                Return Me._lstId.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstId.DataContext = value
            End Set
        End Property

        Public Property Idioma As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Idioma
            Get
                Return Me._cbxIdioma.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxIdioma.SelectedItem = value
            End Set
        End Property

        Public Property Idiomas As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Idiomas
            Get
                Return Me._cbxIdioma.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxIdioma.DataContext = value
            End Set
        End Property

        Public Property Moneda As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Moneda
            Get
                Return Me._cbxMoneda.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxMoneda.SelectedItem = value
            End Set
        End Property

        Public Property Monedas As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Monedas
            Get
                Return Me._cbxMoneda.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxMoneda.DataContext = value
            End Set
        End Property

        Private Sub _btnConsultarAsociado_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.BuscarAsociado2()
        End Sub

        Private Sub _btnVerFacturas_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura_2.Visibility = Windows.Visibility.Visible
            Me._btnagregarfactura2.Visibility = Windows.Visibility.Visible
            Me._lstFacturaCobro.Visibility = Windows.Visibility.Collapsed
            Me._btnConsultarfactura2.Visibility = Windows.Visibility.Collapsed
            Me._txtSumaBono.Visibility = Windows.Visibility.Collapsed
            Me._txtSumaBonoBf.Visibility = Windows.Visibility.Collapsed
            Me._presentador.VerFacturas()
        End Sub

        Private Sub _btnAgregarFacturas2_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura_2.Visibility = Windows.Visibility.Collapsed
            Me._btnagregarfactura2.Visibility = Windows.Visibility.Collapsed
            Me._lstFacturaCobro.Visibility = Windows.Visibility.Visible
            Me._btnConsultarfactura2.Visibility = Windows.Visibility.Visible
            Me._txtSumaBono.Visibility = Windows.Visibility.Visible
            Me._txtSumaBonoBf.Visibility = Windows.Visibility.Visible
            Me._presentador.AgregarFacturas2()
        End Sub

        Private Sub _btnAgregarForma_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura.Visibility = Windows.Visibility.Collapsed
            Me._btnagregarforma.Visibility = Windows.Visibility.Collapsed
            Me._btnConsultarfactura.Visibility = Windows.Visibility.Visible
            Me._lstForma.Visibility = Windows.Visibility.Visible
            Me._txtSumaBForma.Visibility = Windows.Visibility.Visible
            Me._txtSumaBFormaBf.Visibility = Windows.Visibility.Visible
            Me._presentador.AgregarForma()
        End Sub

        Private Sub _lstForma_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Me._txtBForma.Visibility = Windows.Visibility.Visible
            Me._txtBFormaBf.Visibility = Windows.Visibility.Visible
            Me._txtXForma.Visibility = Windows.Visibility.Visible
            Me._btnmodificarforma.Visibility = Windows.Visibility.Visible
            Me._presentador.MostrarForma()
        End Sub

        Private Sub _btnModificarForma_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._txtBForma.Visibility = Windows.Visibility.Collapsed
            Me._txtBFormaBf.Visibility = Windows.Visibility.Collapsed
            Me._txtXForma.Visibility = Windows.Visibility.Collapsed
            Me._btnmodificarforma.Visibility = Windows.Visibility.Collapsed
            Me._presentador.ModificarForma()
        End Sub

        Public ReadOnly Property FacFormaSeleccionada As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.FacFormaSeleccionada
            Get
                Return Me._lstForma.SelectedItem
            End Get
        End Property

        Private Sub _btnVerCreditos_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura.Visibility = Windows.Visibility.Visible
            Me._btnagregarforma.Visibility = Windows.Visibility.Visible
            Me._btnConsultarfactura.Visibility = Windows.Visibility.Collapsed
            Me._lstForma.Visibility = Windows.Visibility.Collapsed
            Me._txtSumaBForma.Visibility = Windows.Visibility.Collapsed
            Me._txtSumaBFormaBf.Visibility = Windows.Visibility.Collapsed

            Me._presentador.VerCreditos()
        End Sub

        Private Sub _txtAsociado_GotFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._txtAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._lstId.Visibility = System.Windows.Visibility.Visible
            Me._lstId.IsEnabled = True
            Me._btnConsultarAsociado.Visibility = System.Windows.Visibility.Visible
            Me._txtIdAsociado.Visibility = System.Windows.Visibility.Visible
            Me._txtNombreAsociado.Visibility = System.Windows.Visibility.Visible
            Me._lblIdAsociado.Visibility = System.Windows.Visibility.Visible
            Me._lblNombreAsociado.Visibility = System.Windows.Visibility.Visible
        End Sub

        Private Sub _lstAsociados_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Me._presentador.CambiarAsociado()
            Me._lstId.Visibility = System.Windows.Visibility.Collapsed
            Me._btnConsultarAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._txtIdAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._txtNombreAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._txtAsociado.Visibility = System.Windows.Visibility.Visible
            Me._lblIdAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._lblNombreAsociado.Visibility = System.Windows.Visibility.Collapsed
        End Sub
        Public Property NombreAsociado() As String Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property

        Public Property ResultadosFactura2() As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.ResultadosFactura2
            Get
                Return Me._lstfactura_2.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstfactura_2.DataContext = value
            End Set
        End Property

        Public Property ResultadosFactura() As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.ResultadosFactura
            Get
                Return Me._lstfactura.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstfactura.DataContext = value
            End Set
        End Property

        Public Property ResultadosFacturaCobro() As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.ResultadosFacturaCobro
            Get
                Return Me._lstFacturaCobro.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstFacturaCobro.DataContext = value
            End Set
        End Property

        Public Property ResultadosForma() As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.ResultadosForma
            Get
                Return Me._lstForma.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstForma.DataContext = value
            End Set
        End Property

        Public Property MensajeErrorCobro As String Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.MensajeErrorCobro
            Get
                Return Me._txtMensajeError.Text
            End Get
            Set(ByVal value As String)
                Me._txtMensajeError.Text = value
            End Set

        End Property

        Public Property SumaBforma As Double Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.SumaBforma
            Get
                Return Me._txtSumaBForma.Text
            End Get
            Set(ByVal value As Double)
                Me._txtSumaBForma.Text = value
            End Set
        End Property

        Public Property SumaBformaBf As Double Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.SumaBformaBf
            Get
                Return Me._txtSumaBFormaBf.Text
            End Get
            Set(ByVal value As Double)
                Me._txtSumaBFormaBf.Text = value
            End Set
        End Property

        Public Property SumaBono As Double Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.SumaBono
            Get
                Return Me._txtSumaBono.Text
            End Get
            Set(ByVal value As Double)
                Me._txtSumaBono.Text = value
            End Set
        End Property

        Public Property SumaBonoBf As Double Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.SumaBonoBf
            Get
                Return Me._txtSumaBonoBf.Text
            End Get
            Set(ByVal value As Double)
                Me._txtSumaBonoBf.Text = value
            End Set
        End Property

        Public Property Bforma As Double Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Bforma
            Get
                Return Me._txtBForma.Text
            End Get
            Set(ByVal value As Double)
                Me._txtBForma.Text = value
            End Set
        End Property

        Public Property BformaBf As Double Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.BformaBf
            Get
                Return Me._txtBFormaBf.Text
            End Get
            Set(ByVal value As Double)
                Me._txtBFormaBf.Text = value
            End Set
        End Property

        Public Property Credito As Integer Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Credito
            Get
                Return Me._txtCredito.Text
            End Get
            Set(ByVal value As Integer)
                Me._txtCredito.Text = value
            End Set
        End Property

        Public Property Xforma As String Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Xforma
            Get
                Return Me._txtXForma.Text
            End Get
            Set(ByVal value As String)
                Me._txtXForma.Text = value
            End Set
        End Property

        Public Property Banco As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Banco
            Get
                Return Me._cbxBanco.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxBanco.SelectedItem = value
            End Set
        End Property

        Public Property Bancos As Object Implements Contratos.FacOperacionDetaProformas.IAgregarFacOperacionDetaProforma.Bancos
            Get
                Return Me._cbxBanco.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxBanco.DataContext = value
            End Set
        End Property
    End Class
End Namespace
