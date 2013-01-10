Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.Tasas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.Tasas
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.Tasas
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarTasa
        Inherits Page
        Implements IConsultarTasa



        Private _presentador As PresentadorConsultarTasa
        Private _cargada As Boolean

#Region "IConsultarTasa"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._dpkId.Focus()
        End Sub


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.Tasas.IConsultarTasa.HabilitarCampos
            Set(ByVal value As Boolean)
                Me._txtMoneda.IsEnabled = value
                Me._txtTasabf.IsEnabled = value
                Me._txtTasabs.IsEnabled = value
                'Me._dpkId = System.DateTime.Now
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.Tasas.IConsultarTasa.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal Tasa As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarTasa(Me, Tasa)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarTasa, "Eliminar Tasa", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
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

        Public Property Tasa As Object Implements Contratos.Tasas.IConsultarTasa.Tasa
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property
    End Class
End Namespace