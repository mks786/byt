Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DepartamentoServicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.DepartamentoServicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.DepartamentoServicios
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarDepartamentoServicio
        Inherits Page
        Implements IConsultarDepartamentoServicio



        Private _presentador As PresentadorConsultarDepartamentoServicio
        Private _cargada As Boolean

#Region "IConsultarDepartamentoServicio"

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


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicio.HabilitarCampos
            Set(ByVal value As Boolean)
                Me._cbxDoc_Servicio.IsEnabled = value
                Me._cbxId.IsEnabled = value
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicio.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

        Public Property Id() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicio.Id
            Get
                Return Me._cbxId.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxId.SelectedItem = value
            End Set
        End Property

        Public Property Ids() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicio.Ids
            Get
                Return Me._cbxId.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxId.DataContext = value
            End Set
        End Property

        Public Property Servicio() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicio.Servicio
            Get
                Return Me._cbxDoc_Servicio.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxDoc_Servicio.SelectedItem = value
            End Set
        End Property

        Public Property Servicios() As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicio.Servicios
            Get
                Return Me._cbxDoc_Servicio.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxDoc_Servicio.DataContext = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal DepartamentoServicio As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarDepartamentoServicio(Me, DepartamentoServicio)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarDepartamentoServicio, "Eliminar DepartamentoServicio", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                Me._presentador.Eliminar()
            End If
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

        Public Property DepartamentoServicio As Object Implements Contratos.DepartamentoServicios.IConsultarDepartamentoServicio.DepartamentoServicio
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property
    End Class
End Namespace