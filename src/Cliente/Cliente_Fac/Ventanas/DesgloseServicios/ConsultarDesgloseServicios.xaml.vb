Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DesgloseServicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.DesgloseServicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda

Namespace Ventanas.DesgloseServicios
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ConsultarDesgloseServicios
        Inherits Page
        Implements IConsultarDesgloseServicios

        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorConsultarDesgloseServicios
        Private _cargada As Boolean

#Region "IConsultarDesgloseServicioes"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Property Servicio() As Object Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.Servicio
            Get
                Return Me._cbxServicio.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxServicio.SelectedItem = value
            End Set
        End Property

        Public Property Servicios() As Object Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.Servicios
            Get
                Return Me._cbxServicio.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxServicio.DataContext = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._cbxServicio.Focus()
        End Sub

        Public Property DesgloseServicioFiltrar() As Object Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.DesgloseServicioFiltrar
            Get
                Return Me._splFiltro.DataContext
            End Get
            Set(ByVal value As Object)
                Me._splFiltro.DataContext = value
            End Set
        End Property

        Public ReadOnly Property DesgloseServicioSeleccionado() As Object Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.DesgloseServicioSeleccionado
            Get
                Return Me._lstResultados.SelectedItem
            End Get
        End Property


        Public Property Resultados() As Object Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.Resultados
            Get
                Return Me._lstResultados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultados.DataContext = value
            End Set
        End Property

        Public Property Id() As Char Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.Id

            Get
                If Not String.IsNullOrEmpty(Me._cbxId.Text) Then
                    Return DirectCast(Me._cbxId.Text, String)(0)
                Else
                    Return " "c
                End If
            End Get

            Set(ByVal value As Char)
                Me._cbxId.Text = value
            End Set
        End Property

        Public ReadOnly Property Pporc As String Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.Pporc
            Get
                Return Me._txtPporc.Text
            End Get
        End Property

        Public Property CurSortCol() As System.Windows.Controls.GridViewColumnHeader Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.CurSortCol
            Get
                Return _CurSortCol
            End Get
            Set(ByVal value As GridViewColumnHeader)
                _CurSortCol = value
            End Set
        End Property

        Public Property CurAdorner() As SortAdorner Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.CurAdorner
            Get
                Return _CurAdorner
            End Get
            Set(ByVal value As SortAdorner)
                _CurAdorner = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.ListaResultados
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

            Me._presentador = New PresentadorConsultarDesgloseServicios(Me)
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
                Me._presentador.IrConsultarDesgloseServicio()
            End If
        End Sub

        Private Sub _Ordenar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.OrdenarColumna(TryCast(sender, GridViewColumnHeader))
        End Sub

        Public WriteOnly Property Count As Integer Implements Contratos.DesgloseServicios.IConsultarDesgloseServicios.Count
            Set(ByVal value As Integer)
                _lblHits.Text = value
            End Set
        End Property
    End Class
End Namespace
