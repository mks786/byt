Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacAsociadoMarcaPatentes
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacAsociadoMarcaPatentes
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacAsociadoMarcaPatentes
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ConsultarFacVistaFacturacionCxpInternas
        Inherits Page
        Implements IConsultarFacVistaFacturacionCxpInternas

        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorConsultarFacVistaFacturacionCxpInternas
        Private _cargada As Boolean

#Region "IConsultarFacVistaFacturacionCxpInternas"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property


        Public Property Resultados() As Object Implements Contratos.FacAsociadoMarcaPatentes.IConsultarFacVistaFacturacionCxpInternas.Resultados
            Get
                Return Me._lstResultados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultados.DataContext = value
            End Set
        End Property

        Public Property FacVistaFacCxpInternaSeleccionado() As Object Implements Contratos.FacAsociadoMarcaPatentes.IConsultarFacVistaFacturacionCxpInternas.FacVistaFacCxpInternaSeleccionado
            Get
                Return Me._lstResultados.SelectedItem
            End Get
            Set(value As Object)
                _lstResultados.SelectedItem = value
            End Set
        End Property

        Public Property CurSortCol() As System.Windows.Controls.GridViewColumnHeader Implements Contratos.FacAsociadoMarcaPatentes.IConsultarFacVistaFacturacionCxpInternas.CurSortCol
            Get
                Return _CurSortCol
            End Get
            Set(ByVal value As GridViewColumnHeader)
                _CurSortCol = value
            End Set
        End Property

        Public Property CurAdorner() As SortAdorner Implements Contratos.FacAsociadoMarcaPatentes.IConsultarFacVistaFacturacionCxpInternas.CurAdorner
            Get
                Return _CurAdorner
            End Get
            Set(ByVal value As SortAdorner)
                _CurAdorner = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.FacAsociadoMarcaPatentes.IConsultarFacVistaFacturacionCxpInternas.ListaResultados
            Get
                Return Me._lstResultados
            End Get
            Set(ByVal value As ListView)
                Me._lstResultados = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal Asociado As Object)
            InitializeComponent()
            Me._cargada = False

            Me._presentador = New PresentadorConsultarFacVistaFacturacionCxpInternas(Me, Asociado)
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            Else
                Me._presentador.ActualizarTitulo()
            End If
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Public Sub _btnLimpiar_Click()
            ' Me._presentador.Limpiar()
        End Sub

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
        End Sub

        Private Sub _Ordenar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.OrdenarColumna(TryCast(sender, GridViewColumnHeader))
        End Sub


        Public WriteOnly Property Count As Integer Implements Contratos.FacAsociadoMarcaPatentes.IConsultarFacVistaFacturacionCxpInternas.Count
            Set(ByVal value As Integer)
                _lblHits.Text = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements Contratos.IPaginaBaseFac.FocoPredeterminado

        End Sub

        Private Sub _lstResultados_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs)
            Me._presentador.IrConsultarPagoInternacional()
        End Sub
    End Class
End Namespace
