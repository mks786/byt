Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DepartamentoServicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.DepartamentoServicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.DepartamentoServicios
    Partial Public Class AgregarDepartamentoServicio
        Inherits Page
        Implements IAgregarDepartamentoServicio


        Private _presentador As PresentadorAgregarDepartamentoServicio
        Private _cargada As Boolean

#Region "IAgregarDepartamentoServicio"

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

        Public Property DepartamentoServicio() As Object Implements Contratos.DepartamentoServicios.IAgregarDepartamentoServicio.DepartamentoServicio
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Property Id() As Object Implements Contratos.DepartamentoServicios.IAgregarDepartamentoServicio.Id
            Get
                Return Me._cbxId.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxId.SelectedItem = value
            End Set
        End Property

        Public Property Ids() As Object Implements Contratos.DepartamentoServicios.IAgregarDepartamentoServicio.Ids
            Get
                Return Me._cbxId.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxId.DataContext = value
            End Set
        End Property

        Public Property Servicio() As Object Implements Contratos.DepartamentoServicios.IAgregarDepartamentoServicio.Servicio
            Get
                Return Me._cbxDoc_Servicio.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxDoc_Servicio.SelectedItem = value
            End Set
        End Property

        Public Property Servicios() As Object Implements Contratos.DepartamentoServicios.IAgregarDepartamentoServicio.Servicios
            Get
                Return Me._cbxDoc_Servicio.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxDoc_Servicio.DataContext = value
            End Set
        End Property


        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.DepartamentoServicios.IAgregarDepartamentoServicio.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub


#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorAgregarDepartamentoServicio(Me)
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
