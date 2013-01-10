Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacFacturas
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Namespace Ventanas.FacFacturas
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ConsultarDepositoFacFacturas
        Inherits Page
        Implements IConsultarDepositoFacFacturas

        Dim value As Boolean = False
        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorConsultarDepositoFacFacturas
        Private _cargada As Boolean

#Region "IConsultarFacFacturaes"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._txtNDeposito.Focus()
        End Sub

        Public Property FacFacturaFiltrar() As Object Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.FacFacturaFiltrar
            Get
                Return Me._splFiltro.DataContext
            End Get
            Set(ByVal value As Object)
                Me._splFiltro.DataContext = value
            End Set
        End Property

        Public ReadOnly Property FacFacturaSeleccionado() As Object Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.FacFacturaSeleccionado
            Get
                Return Me._lstResultados.SelectedItem
            End Get
        End Property


        Public Property Resultados() As Object Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.Resultados
            Get
                Return Me._lstResultados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultados.DataContext = value
            End Set
        End Property

        'Public Property Id() As Integer Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.Id
        '    Get
        '        Return Me._lstId.Text
        '    End Get
        '    Set(ByVal value As Integer)
        '        Me._lstId.Text = value
        '    End Set
        'End Property

        Public Property CurSortCol() As System.Windows.Controls.GridViewColumnHeader Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.CurSortCol
            Get
                Return _CurSortCol
            End Get
            Set(ByVal value As GridViewColumnHeader)
                _CurSortCol = value
            End Set
        End Property

        Public Property CurAdorner() As SortAdorner Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.CurAdorner
            Get
                Return _CurAdorner
            End Get
            Set(ByVal value As SortAdorner)
                _CurAdorner = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.ListaResultados
            Get
                Return Me._lstResultados
            End Get
            Set(ByVal value As ListView)
                Me._lstResultados = value
            End Set
        End Property

#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False

            Me._presentador = New PresentadorConsultarDepositoFacFacturas(Me)
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            Else
                Me._presentador.ActualizarTitulo()
            End If
        End Sub

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
        End Sub

        Private Sub _btnAplicarDeposito_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._btnAplicarDeposito.Focus()
            Me._presentador.AplicarDeposito()
        End Sub


        Private Sub _Ordenar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.OrdenarColumna(TryCast(sender, GridViewColumnHeader))
        End Sub


        Public Property Banco As Object Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.Banco
            Get
                Return Me._cbxBanco.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxBanco.SelectedItem = value
            End Set
        End Property

        Public Property Bancos As Object Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.Bancos
            Get
                Return Me._cbxBanco.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxBanco.DataContext = value
            End Set
        End Property


        Public ReadOnly Property FechaDeposito As String Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.FechaDeposito
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Private Sub _btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles _btnSeleccionar.Click
            If (value = False) Then
                value = True
            Else
                value = False
            End If
            Me._presentador.seleccionar(value)
        End Sub


        Public Property NReg As Integer Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.NReg
            Get
                Return Me._txtNReg.Text
            End Get
            Set(ByVal value As Integer)
                Me._txtNReg.Text = value
            End Set
        End Property

        Public Property Tmonto As Double Implements Contratos.FacFacturas.IConsultarDepositoFacFacturas.Tmonto
            Get
                Return Me._txtTMonto.Text
            End Get
            Set(ByVal value As Double)
                Me._txtTMonto.Text = value
            End Set
        End Property
    End Class
End Namespace
