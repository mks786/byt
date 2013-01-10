Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFactuDetaAnuladas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacFactuDetaAnuladas
Imports Trascend.Bolet.Cliente.Contratos
Namespace Ventanas.FacFactuDetaAnuladas
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarFacFactuDetaAnulada
        Inherits Page
        Implements IConsultarFacFactuDetaAnulada


        Private _presentador As PresentadorConsultarFacFactuDetaAnulada
        Private _cargada As Boolean

#Region "IConsultarFacFactuDetaAnulada"

        Public Property EstaCargada() As Boolean Implements Contratos.IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements Contratos.IPaginaBaseFac.FocoPredeterminado
            Me._txtId.Focus()
        End Sub


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.FacFactuDetaAnuladas.IConsultarFacFactuDetaAnulada.HabilitarCampos
            Set(ByVal value As Boolean)
                Me._txtDetalle.IsEnabled = value
                Me._txtId.IsEnabled = value
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.FacFactuDetaAnuladas.IConsultarFacFactuDetaAnulada.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal FacFactuDetaAnulada As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarFacFactuDetaAnulada(Me, FacFactuDetaAnulada)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarFacFactuDetaAnulada, "Eliminar FacFactuDetaAnulada", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                Me._presentador.Eliminar()
            End If
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            End If
        End Sub

        Public Property FacFactuDetaAnulada As Object Implements Contratos.FacFactuDetaAnuladas.IConsultarFacFactuDetaAnulada.FacFactuDetaAnulada
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

    End Class
End Namespace