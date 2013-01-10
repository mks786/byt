Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.TipoPatentes
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.TipoPatentes
Imports Diginsoft.Bolet.Cliente.Fac.Contratos

Namespace Ventanas.TipoPatentes
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarTipoPatente
        Inherits Page
        Implements IConsultarTipoPatente



        Private _presentador As PresentadorConsultarTipoPatente
        Private _cargada As Boolean

#Region "IConsultarTipoPatente"

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


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.TipoPatentes.IConsultarTipoPatente.HabilitarCampos
            Set(ByVal value As Boolean)
                Me._txtDoc_Esp.IsEnabled = value
                Me._txtDoc_Ingl.IsEnabled = value
                Me._txtDocs_Esp.IsEnabled = value
                Me._txtDocs_Ingl.IsEnabled = value
                Me._txtId.IsEnabled = value
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.TipoPatentes.IConsultarTipoPatente.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal TipoPatente As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarTipoPatente(Me, tipoPatente)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarTipoPatente, "Eliminar TipoPatente", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
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

        Public Property TipoPatente As Object Implements Contratos.TipoPatentes.IConsultarTipoPatente.TipoPatente
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property
    End Class
End Namespace