Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DocumentosMarcas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.DocumentosMarcas
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.DocumentosMarcas
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarDocumentosMarca
        Inherits Page
        Implements IConsultarDocumentosMarca



        Private _presentador As PresentadorConsultarDocumentosMarca
        Private _cargada As Boolean

#Region "IConsultarDocumentosMarca"

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


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.DocumentosMarcas.IConsultarDocumentosMarca.HabilitarCampos
            Set(ByVal value As Boolean)
                Me._txtDoc_Esp.IsEnabled = value
                Me._txtDoc_Ingl.IsEnabled = value
                Me._txtId.IsEnabled = value
                Me._txtMont_Bs.IsEnabled = value
                Me._txtMont_Us.IsEnabled = value
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.DocumentosMarcas.IConsultarDocumentosMarca.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal DocumentosMarca As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarDocumentosMarca(Me, DocumentosMarca)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarDocumentosMarca, "Eliminar DocumentosMarca", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
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

        Public Property DocumentosMarca As Object Implements Contratos.DocumentosMarcas.IConsultarDocumentosMarca.DocumentosMarca
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property
    End Class
End Namespace