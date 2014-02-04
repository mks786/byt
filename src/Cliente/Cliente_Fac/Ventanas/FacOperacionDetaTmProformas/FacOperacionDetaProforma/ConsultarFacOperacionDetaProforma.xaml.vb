Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacOperacionDetaProformas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacOperacionDetaProformas
Imports Trascend.Bolet.Cliente.Contratos
Namespace Ventanas.FacOperacionDetaProformas
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarFacOperacionDetaProforma
        Inherits Page
        Implements IConsultarFacOperacionDetaProforma



        Private _presentador As PresentadorConsultarFacOperacionDetaProforma
        Private _cargada As Boolean

#Region "IConsultarFacOperacionDetaProforma"

        Public Property EstaCargada() As Boolean Implements IPaginaBase.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBase.FocoPredeterminado
            Me._txtId.Focus()
        End Sub


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.FacOperacionDetaProformas.IConsultarFacOperacionDetaProforma.HabilitarCampos
            Set(ByVal value As Boolean)
                Me._txtDetalle.IsEnabled = value
                Me._txtId.IsEnabled = value
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.FacOperacionDetaProformas.IConsultarFacOperacionDetaProforma.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal FacOperacionDetaProforma As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarFacOperacionDetaProforma(Me, FacOperacionDetaProforma)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarFacOperacionDetaProforma, "Eliminar FacOperacionDetaProforma", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                Me._presentador.Eliminar()
            End If
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            End If
        End Sub

        Public Property FacOperacionDetaProforma As Object Implements Contratos.FacOperacionDetaProformas.IConsultarFacOperacionDetaProforma.FacOperacionDetaProforma
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property
    End Class
End Namespace