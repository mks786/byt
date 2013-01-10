Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DesgloseServicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.DesgloseServicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.DesgloseServicios
    Partial Public Class AgregarDesgloseServicio
        Inherits Page
        Implements IAgregarDesgloseServicio

        Private _presentador As PresentadorAgregarDesgloseServicio
        Private _cargada As Boolean

#Region "IAgregarDesgloseServicio"

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

        Public ReadOnly Property TipoDesgSer() As Char Implements IAgregarDesgloseServicio.TipoDesgSer
            Get
                If Not String.IsNullOrEmpty(Me._cbxId.Text) Then
                    Return (Me._cbxId.Text)(0)
                Else
                    Return " "c
                End If
            End Get
        End Property

        Public Property Servicio() As Object Implements Contratos.DesgloseServicios.IAgregarDesgloseServicio.Servicio
            Get
                Return Me._cbxServicio.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxServicio.SelectedItem = value
            End Set
        End Property

        Public Property Servicios() As Object Implements Contratos.DesgloseServicios.IAgregarDesgloseServicio.Servicios
            Get
                Return Me._cbxServicio.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxServicio.DataContext = value
            End Set
        End Property

        Public Property DesgloseServicio() As Object Implements Contratos.DesgloseServicios.IAgregarDesgloseServicio.DesgloseServicio
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.DesgloseServicios.IAgregarDesgloseServicio.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub


#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorAgregarDesgloseServicio(Me)
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
