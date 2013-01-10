Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.TarifaServicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.TarifaServicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda

Namespace Ventanas.TarifaServicios
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ConsultarTarifaServicios
        Inherits Page
        Implements IConsultarTarifaServicios

        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorConsultarTarifaServicios
        Private _cargada As Boolean

#Region "IConsultarTarifaServicioes"

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
        Public Property Tarifa() As Object Implements Contratos.TarifaServicios.IConsultarTarifaServicios.Tarifa
            Get
                Return Me._cbxTarifa.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxTarifa.SelectedItem = value
            End Set
        End Property

        Public Property Tarifas() As Object Implements Contratos.TarifaServicios.IConsultarTarifaServicios.Tarifas
            Get
                Return Me._cbxTarifa.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxTarifa.DataContext = value
            End Set
        End Property
        Public Property TarifaServicioFiltrar() As Object Implements Contratos.TarifaServicios.IConsultarTarifaServicios.TarifaServicioFiltrar
            Get
                Return Me._splFiltro.DataContext
            End Get
            Set(ByVal value As Object)
                Me._splFiltro.DataContext = value
            End Set
        End Property

        Public ReadOnly Property TarifaServicioSeleccionado() As Object Implements Contratos.TarifaServicios.IConsultarTarifaServicios.TarifaServicioSeleccionado
            Get
                Return Me._lstResultados.SelectedItem
            End Get
        End Property


        Public Property Resultados() As Object Implements Contratos.TarifaServicios.IConsultarTarifaServicios.Resultados
            Get
                Return Me._lstResultados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultados.DataContext = value
            End Set
        End Property

        Public Property Id() As String Implements Contratos.TarifaServicios.IConsultarTarifaServicios.Id
            Get
                Return Me._txtId.Text
            End Get
            Set(ByVal value As String)
                Me._txtId.Text = value
            End Set
        End Property

        Public Property CurSortCol() As System.Windows.Controls.GridViewColumnHeader Implements Contratos.TarifaServicios.IConsultarTarifaServicios.CurSortCol
            Get
                Return _CurSortCol
            End Get
            Set(ByVal value As GridViewColumnHeader)
                _CurSortCol = value
            End Set
        End Property

        Public Property CurAdorner() As SortAdorner Implements Contratos.TarifaServicios.IConsultarTarifaServicios.CurAdorner
            Get
                Return _CurAdorner
            End Get
            Set(ByVal value As SortAdorner)
                _CurAdorner = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.TarifaServicios.IConsultarTarifaServicios.ListaResultados
            Get
                Return Me._lstResultados
            End Get
            Set(ByVal value As ListView)
                Me._lstResultados = value
            End Set
        End Property

#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False

            Me._presentador = New PresentadorConsultarTarifaServicios(Me)
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            Else
                Me._presentador.ActualizarTitulo()
            End If
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

        Public ReadOnly Property Tasa As String Implements Contratos.TarifaServicios.IConsultarTarifaServicios.Tasa
            Get
                Return Me._txtTasa.Text
            End Get
        End Property

        Public ReadOnly Property Mont_Us As String Implements Contratos.TarifaServicios.IConsultarTarifaServicios.Mont_Us
            Get
                Return Me._txtMont_Us.Text
            End Get
        End Property

        Public ReadOnly Property Mont_Bs As String Implements Contratos.TarifaServicios.IConsultarTarifaServicios.Mont_Bs
            Get
                Return Me._txtMont_Bs.Text
            End Get
        End Property

        Public ReadOnly Property Mont_Bf As String Implements Contratos.TarifaServicios.IConsultarTarifaServicios.Mont_Bf
            Get
                Return Me._txtMont_Bf.Text
            End Get
        End Property

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
        End Sub

        Private Sub _btnConsultar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._btnConsultar.Focus()
            Me._presentador.Consultar()
        End Sub

        Private Sub _lstResultados_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If Me._lstResultados.SelectedItem IsNot Nothing Then
                Me._presentador.IrConsultarTarifaServicio()
            End If
        End Sub

        Private Sub _Ordenar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.OrdenarColumna(TryCast(sender, GridViewColumnHeader))
        End Sub

        Public WriteOnly Property Count As Integer Implements Contratos.TarifaServicios.IConsultarTarifaServicios.Count
            Set(ByVal value As Integer)
                _lblHits.Text = value
            End Set
        End Property
    End Class
End Namespace
