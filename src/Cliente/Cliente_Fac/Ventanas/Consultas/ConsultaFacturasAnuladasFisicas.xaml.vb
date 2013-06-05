Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.Consultas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.Consultas
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.Consultas
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ConsultaFacturasAnuladasFisicas
        Inherits Page
        Implements IConsultaFacturasAnuladasFisicas

        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorConsultaFacturasAnuladasFisicas
        Private _cargada As Boolean

#Region "IConsultarFacAnuladaFisicasAnuladas"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            'Me._txtId.Focus()
        End Sub

        'Public Property FacAnuladaFisicaFiltrar() As Object Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.FacAnuladaFisicaFiltrar
        '    Get
        '        Return Me._splFiltro.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._splFiltro.DataContext = value
        '    End Set
        'End Property

        Public ReadOnly Property FacAnuladaFisicaSeleccionado() As Object Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.FacAnuladaFisicaSeleccionado
            Get
                Return Me._lstResultados.SelectedItem
            End Get
        End Property


        Public Property Resultados() As Object Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.Resultados
            Get
                Return Me._lstResultados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultados.DataContext = value
            End Set
        End Property

        'Public Property Id() As Integer Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.Id
        '    Get
        '        Return Me._lstId.Text
        '    End Get
        '    Set(ByVal value As Integer)
        '        Me._lstId.Text = value
        '    End Set
        'End Property

        Public Property CurSortCol() As System.Windows.Controls.GridViewColumnHeader Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.CurSortCol
            Get
                Return _CurSortCol
            End Get
            Set(ByVal value As GridViewColumnHeader)
                _CurSortCol = value
            End Set
        End Property

        Public Property CurAdorner() As SortAdorner Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.CurAdorner
            Get
                Return _CurAdorner
            End Get
            Set(ByVal value As SortAdorner)
                _CurAdorner = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.ListaResultados
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

            Me._presentador = New PresentadorConsultaFacturasAnuladasFisicas(Me)
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            Else
                Me._presentador.ActualizarTitulo()
            End If
        End Sub

        Public Sub _btnLimpiar_Click()
            'Me._presentador.Limpiar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        'Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '    Me._presentador.Cancelar()
        'End Sub

        'Private Sub _btnConsultar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '    'Me._btnConsultar.Focus()
        '    Me._presentador.Consultar()
        'End Sub

        Private Sub _lstResultados_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If Me._lstResultados.SelectedItem IsNot Nothing Then
                Me._presentador.IrConsultarFacAnuladaFisica()
            End If
        End Sub

        Private Sub _Ordenar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.OrdenarColumna(TryCast(sender, GridViewColumnHeader))
        End Sub

        Private Sub _dpkFechaUltima_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            'If Not String.IsNullOrEmpty(Me._dpkFechaUltima.Text) Then
            '    Me._presentador.DeshabilitarDias(Me._dpkFechaBoletinVence, Me._dpkFechaBoletin.SelectedDate.Value.AddDays(-1))
            'End If
        End Sub



        Private Sub _btnConsultar(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim nom As String = DirectCast(sender, Button).Name.ToString

            If nom = "_btnCancelar" Then
                Me._presentador.Cancelar()
                'Me._presentador.reporte()
            ElseIf nom = "_btnConsulta" Then
                Me._presentador.Consultar()
            End If
        End Sub
        'Public Property Banco As Object Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.Banco
        '    Get
        '        Return Me._cbxBanco.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxBanco.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Bancos As Object Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.Bancos
        '    Get
        '        Return Me._cbxBanco.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxBanco.DataContext = value
        '    End Set
        'End Property

        'Public Property Idioma As Object Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.Idioma
        '    Get
        '        Return Me._cbxIdioma.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxIdioma.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Idiomas As Object Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.Idiomas
        '    Get
        '        Return Me._cbxIdioma.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxIdioma.DataContext = value
        '    End Set
        'End Property

        'Public Property Moneda As Object Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.Moneda
        '    Get
        '        Return Me._cbxMoneda.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxMoneda.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Monedas As Object Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.Monedas
        '    Get
        '        Return Me._cbxMoneda.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxMoneda.DataContext = value
        '    End Set
        'End Property

        Public WriteOnly Property Count As Integer Implements Contratos.Consultas.IConsultaFacturasAnuladasFisicas.Count
            Set(ByVal value As Integer)
                _lblHits.Text = value
            End Set
        End Property

    End Class
End Namespace
