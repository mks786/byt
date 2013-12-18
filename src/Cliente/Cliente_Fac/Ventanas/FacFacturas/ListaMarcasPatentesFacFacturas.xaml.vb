Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacFacturas
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacFacturas
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ListaMarcasPatentesFacFacturas
        Inherits Page
        Implements IListaMarcasPatentesFacFactura

        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorListaMarcasPatentesFacFacturas
        Private _cargada As Boolean

#Region "ListaMarcasPatentesFacFacturas"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._btnCancelar.Focus()
        End Sub

        Public Property Elementos As Object Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.Elementos
            Get
                Return Me._cbxTiposABuscar.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxTiposABuscar.DataContext = value
            End Set
        End Property

        Public Property ElementoConsultado As Object Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.ElementoConsultado
            Get
                Return Me._cbxTiposABuscar.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxTiposABuscar.SelectedItem = value
            End Set
        End Property

        Public Property IdElemento As String Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.IdElemento
            Get
                Return Me._txtIdElemento.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdElemento.Text = value
            End Set
        End Property

        Public Property IdInternacionalElemento As String Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.IdInternacionalElemento
            Get
                Return Me._txtIdInternacionalElemento.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdInternacionalElemento.Text = value
            End Set
        End Property

        Public Property Solicitud As String Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.Solicitud
            Get
                Return Me._txtSolicitud.Text
            End Get
            Set(ByVal value As String)
                Me._txtSolicitud.Text = value
            End Set
        End Property

        Public Property Registro As String Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.Registro
            Get
                Return Me._txtRegistro.Text
            End Get
            Set(ByVal value As String)
                Me._txtRegistro.Text = value
            End Set
        End Property

        Public Property Marcas() As Object Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.Marcas
            Get
                Return Me._lstMarcas.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstMarcas.DataContext = value
            End Set
        End Property

        Public ReadOnly Property MarcaSeleccionada() As Object Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.MarcaSeleccionada
            Get
                Return Me._lstMarcas.SelectedItem
            End Get
        End Property

        Public Property Patentes() As Object Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.Patentes
            Get
                Return Me._lstPatentes.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstPatentes.DataContext = value
            End Set
        End Property

        Public ReadOnly Property PatenteSeleccionada() As Object Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.PatenteSeleccionada
            Get
                Return Me._lstPatentes.SelectedItem
            End Get
        End Property

#End Region

#Region "Constructores"
        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorListaMarcasPatentesFacFacturas(Me)
        End Sub

        Public Sub New(ByVal ventanaPadre As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorListaMarcasPatentesFacFacturas(Me, ventanaPadre)
        End Sub

#End Region

#Region "Eventos"

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            Else
                Me._presentador.ActualizarTitulo()
            End If
        End Sub

        Private Sub _cbxTiposABuscar_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs)
            Me._presentador.BusquedaMarcaOPatenteInternacional()
        End Sub

        Private Sub _btnCancelar_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
            Me._presentador.RegresarVentanaPadre()
        End Sub

        Private Sub _btnConsultar_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
            Me._presentador.Consultar()
        End Sub

        Private Sub _btnLimpiar_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
            Me._presentador.LimpiarTodo()
        End Sub

        Private Sub _lstMarcas_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs)
            Me._presentador.ConsultarMarcaSeleccionada()
        End Sub

        Private Sub _lstPatentes_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs)
            Me._presentador.ConsultarPatenteSeleccionada()
        End Sub

#End Region

#Region "Metodos"
        Public Sub HabilitarCampoInternacional(ByVal estado As Boolean) Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.HabilitarCampoInternacional
            Me._txtIdInternacionalElemento.IsEnabled = estado
        End Sub

        Public Sub Mensaje(ByVal mensaje As String, ByVal tipoMensaje As Integer) Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.Mensaje
            Select Case tipoMensaje
                Case 0
                    MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
                Case 1
                    MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning)
                Case 2
                    MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information)
            End Select

        End Sub

        Public Sub GestionarVisibilidadListas(ByVal estado As Boolean) Implements Contratos.FacFacturas.IListaMarcasPatentesFacFactura.GestionarVisibilidadListas
            If estado Then
                Me._lstMarcas.Visibility = Windows.Visibility.Visible
                Me._lstPatentes.Visibility = Windows.Visibility.Collapsed
            Else
                Me._lstMarcas.Visibility = Windows.Visibility.Collapsed
                Me._lstPatentes.Visibility = Windows.Visibility.Visible
            End If
        End Sub


#End Region





    End Class
End Namespace
