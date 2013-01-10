Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DepartamentoServicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.DepartamentoServicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda

Namespace Ventanas.DepartamentoServicios
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ConsultarDepartamentoServicios
        Inherits Page
        Implements IConsultarDepartamentoServicios

        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorConsultarDepartamentoServicios
        Private _cargada As Boolean

#Region "IConsultarDepartamentoServicioes"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._cbxId.Focus()
        End Sub

        Public Property DepartamentoServicioFiltrar() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.DepartamentoServicioFiltrar
            Get
                Return Me._splFiltro.DataContext
            End Get
            Set(ByVal value As Object)
                Me._splFiltro.DataContext = value
            End Set
        End Property

        Public ReadOnly Property DepartamentoServicioSeleccionado() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.DepartamentoServicioSeleccionado
            Get
                Return Me._lstResultados.SelectedItem
            End Get
        End Property


        Public Property Resultados() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.Resultados
            Get
                Return Me._lstResultados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultados.DataContext = value
            End Set
        End Property

        Public Property GetSetid() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.GetSetId
            Get
                Return Me._cbxId.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxId.SelectedItem = value
            End Set
        End Property

        Public Property GetSetIds() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.GetSetIds
            Get
                Return Me._cbxId.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxId.DataContext = value
            End Set
        End Property

        Public Property Servicio() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.Servicio
            Get
                Return Me._cbxDoc_Servicio.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxDoc_Servicio.SelectedItem = value
            End Set
        End Property

        Public Property Servicios() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.Servicios
            Get
                Return Me._cbxDoc_Servicio.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxDoc_Servicio.DataContext = value
            End Set
        End Property

        Public Property Id() As String Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.Id
            Get
                Return Me._cbxId.Text
            End Get
            Set(ByVal value As String)
                Me._cbxId.Text = value
            End Set
        End Property

        Public Property CurSortCol() As System.Windows.Controls.GridViewColumnHeader Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.CurSortCol
            Get
                Return _CurSortCol
            End Get
            Set(ByVal value As GridViewColumnHeader)
                _CurSortCol = value
            End Set
        End Property

        Public Property CurAdorner() As SortAdorner Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.CurAdorner
            Get
                Return _CurAdorner
            End Get
            Set(ByVal value As SortAdorner)
                _CurAdorner = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.ListaResultados
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

            Me._presentador = New PresentadorConsultarDepartamentoServicios(Me)
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

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
        End Sub

        Private Sub _btnConsultar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._btnConsultar.Focus()
            Me._presentador.Consultar()
        End Sub

        Private Sub _lstResultados_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If Me._lstResultados.SelectedItem IsNot Nothing Then
                Me._presentador.IrConsultarDepartamentoServicio()
            End If
        End Sub

        Private Sub _Ordenar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.OrdenarColumna(TryCast(sender, GridViewColumnHeader))
        End Sub

        Public WriteOnly Property Count As Integer Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicios.Count
            Set(ByVal value As Integer)
                _lblHits.Text = value
            End Set
        End Property
    End Class
End Namespace
