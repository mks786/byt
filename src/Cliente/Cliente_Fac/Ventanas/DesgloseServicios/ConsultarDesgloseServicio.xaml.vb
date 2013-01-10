Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DesgloseServicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.DesgloseServicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.DesgloseServicios
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarDesgloseServicio
        Inherits Page
        Implements IConsultarDesgloseServicio



        Private _presentador As PresentadorConsultarDesgloseServicio
        Private _cargada As Boolean

#Region "IConsultarDesgloseServicio"

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


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.DesgloseServicios.IConsultarDesgloseServicio.HabilitarCampos
            Set(ByVal value As Boolean)
                Me._cbxServicio.IsEnabled = value
                Me._txtPporc.IsEnabled = value
                Me._cbxId.IsEnabled = value
            End Set
        End Property


        Public ReadOnly Property GetTipoDesgSer() As Char Implements IConsultarDesgloseServicio.GetTipoDesgSer
            Get
                If Not String.IsNullOrEmpty(Me._cbxId.Text) Then
                    Return (Me._cbxId.Text)(0)
                Else
                    Return " "c
                End If
            End Get
        End Property

        Public WriteOnly Property SetTipoDesgSer() As String Implements IConsultarDesgloseServicio.SetTipoDesgSer
            Set(ByVal value As String)
                Me._cbxId.Text = value
            End Set
        End Property

        Public Property Servicio() As Object Implements Contratos.DesgloseServicios.IConsultarDesgloseServicio.Servicio
            Get
                Return Me._cbxServicio.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxServicio.SelectedItem = value
            End Set
        End Property

        Public Property Servicios() As Object Implements Contratos.DesgloseServicios.IConsultarDesgloseServicio.Servicios
            Get
                Return Me._cbxServicio.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxServicio.DataContext = value
            End Set
        End Property

        Public Property TextoBotonModificar() As String Implements Contratos.DesgloseServicios.IConsultarDesgloseServicio.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal DesgloseServicio As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarDesgloseServicio(Me, DesgloseServicio)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarDesgloseServicio, "Eliminar DesgloseServicio", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
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

        Public Property DesgloseServicio As Object Implements Contratos.DesgloseServicios.IConsultarDesgloseServicio.DesgloseServicio
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property
    End Class
End Namespace