Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacInternacionales
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacInternacionales
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Imports Trascend.Bolet.ControlesByT

Namespace Ventanas.FacInternacionales
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ConsultarFacInternacionalesProforma
        Inherits Page
        Implements IConsultarFacInternacionalesProforma

        'Public Shared desactivar_item As New RoutedCommand("desactivar_item", GetType(ConsultarFacInternacionalesProforma))

        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorConsultarFacInternacionalesProforma
        Private _cargada As Boolean

#Region "IConsultarFacInternacionalesProforma"

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

        Public Property FacFacturaProformaFiltrar() As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.FacFacturaProformaFiltrar
            Get
                Return Me._splFiltro.DataContext
            End Get
            Set(ByVal value As Object)
                Me._splFiltro.DataContext = value
            End Set
        End Property

        Public ReadOnly Property FacFacturaProformaSeleccionado() As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.FacFacturaProformaSeleccionado
            Get
                Return Me._lstResultados.SelectedItem
            End Get
        End Property


        Public Property Resultados() As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Resultados
            Get
                Return Me._lstResultados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultados.DataContext = value
            End Set
        End Property

        'Public Property Id() As Integer Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Id
        '    Get
        '        Return Me._lstId.Text
        '    End Get
        '    Set(ByVal value As Integer)
        '        Me._lstId.Text = value
        '    End Set
        'End Property

        Public Property CurSortCol() As System.Windows.Controls.GridViewColumnHeader Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.CurSortCol
            Get
                Return _CurSortCol
            End Get
            Set(ByVal value As GridViewColumnHeader)
                _CurSortCol = value
            End Set
        End Property

        Public Property CurAdorner() As SortAdorner Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.CurAdorner
            Get
                Return _CurAdorner
            End Get
            Set(ByVal value As SortAdorner)
                _CurAdorner = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.ListaResultados
            Get
                Return Me._lstResultados
            End Get
            Set(ByVal value As ListView)
                Me._lstResultados = value
            End Set
        End Property

#Region "AsociadosInternacionales"

        Public Property NombreAsociadoInt() As String Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.NombreAsociadoInt
            Get
                Return Me._txtAsociadoInt.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociadoInt.Text = value
            End Set
        End Property

        Public Property idAsociadoIntFiltrar() As String Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.idAsociadoIntFiltrar
            Get
                Return Me._txtIdAsociadoInt.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociadoInt.Text = value
            End Set
        End Property

        Public Property NombreAsociadoIntFiltrar() As String Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.NombreAsociadoIntFiltrar
            Get
                Return Me._txtNombreAsociadoInt.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociadoInt.Text = value
            End Set
        End Property

        Public Property AsociadosInternacionales As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.AsociadosInternacionales
            Get
                Return Me._lstAsociadosInt.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstAsociadosInt.DataContext = value
            End Set
        End Property

        Public Property AsociadoInternacional As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.AsociadoInternacional

            Get
                Return Me._lstAsociadosInt.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociadosInt.SelectedItem = value
                Me._lstAsociadosInt.ScrollIntoView(value)
            End Set
        End Property

#End Region

        Public Property NumeroFactInternacional() As String Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.NumeroFactInternacional
            Get
                Return Me._txtNumeroFacInt.Text
            End Get
            Set(ByVal value As String)
                Me._txtNumeroFacInt.Text = value
            End Set
        End Property

        Public Property PaisesAsocInt() As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.PaisesAsocInt
            Get
                Return Me._CbxPaisAsocInt.DataContext
            End Get
            Set(ByVal value As Object)
                Me._CbxPaisAsocInt.DataContext = value
            End Set
        End Property

        Public Property PaisAsocInt() As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.PaisAsocInt
            Get
                Return Me._CbxPaisAsocInt.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._CbxPaisAsocInt.SelectedItem = value
            End Set
        End Property

        Public Property DetalleFacAsocInt() As String Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.DetalleFacAsocInt
            Get
                Return Me._txtDetalleFacAsocInt.Text
            End Get
            Set(ByVal value As String)
                Me._txtDetalleFacAsocInt.Text = value
            End Set
        End Property


#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarFacInternacionalesProforma(Me)
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            Else
                Me._presentador.ActualizarTitulo()
            End If
            PintarAsociado()
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
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
                Me._presentador.IrConsultarFacFacturaProforma()
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

        Public Property Asociado As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Asociado

            Get
                Return Me._lstAsociados.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.SelectedItem = value
                Me._lstAsociados.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Asociados
            Get
                Return Me._lstAsociados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.DataContext = value
            End Set
        End Property

        Private Sub _btnConsultar(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim nom As String = DirectCast(sender, Button).Name.ToString
            If nom = "_btnConsultarAsociado" Then
                Me._presentador.BuscarAsociado2()
            ElseIf nom = "_btnCancelar" Then
                Me._presentador.Cancelar()
            ElseIf nom = "_btnConsulta" Then
                Me._presentador.Consultar()
            End If
        End Sub
        'Public Property Banco As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Banco
        '    Get
        '        Return Me._cbxBanco.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxBanco.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Bancos As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Bancos
        '    Get
        '        Return Me._cbxBanco.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxBanco.DataContext = value
        '    End Set
        'End Property

        'Public Property Idioma As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Idioma
        '    Get
        '        Return Me._cbxIdioma.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxIdioma.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Idiomas As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Idiomas
        '    Get
        '        Return Me._cbxIdioma.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxIdioma.DataContext = value
        '    End Set
        'End Property

        'Public Property Moneda As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Moneda
        '    Get
        '        Return Me._cbxMoneda.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxMoneda.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Monedas As Object Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Monedas
        '    Get
        '        Return Me._cbxMoneda.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxMoneda.DataContext = value
        '    End Set
        'End Property

        Private Sub _btnConsultarAsociado_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.BuscarAsociado2()
        End Sub
        Private Sub _Consultar_Enter(ByVal sender As Object, ByVal e As KeyEventArgs)
            If (e.Key = Key.Enter) Then
                Dim nom As String = DirectCast(sender, ByTTextBox).Name.ToString
                If nom = "_txtIdAsociado" Or nom = "_txtNombreAsociado" Then
                    Me._presentador.BuscarAsociado2()
                ElseIf nom = "_txtIdAsociadoInt" Or nom = "_txtNombreAsociadoInt" Then
                    Me._presentador.BuscarAsociadoInternacional()
                Else
                    Me._presentador.Consultar()
                End If
            End If
        End Sub

        Private Sub _txtAsociado_MouseDoubleClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ControlesMostrarAsociado()
        End Sub

        Private Sub ControlesMostrarAsociado()
            Me._txtAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._lblasociado2.Visibility = System.Windows.Visibility.Collapsed
            'Me._txtAsociadoId.Visibility = System.Windows.Visibility.Collapsed
            Me._lstAsociados.Visibility = System.Windows.Visibility.Visible
            Me._lstAsociados.IsEnabled = True
            Me._btnConsultarAsociado.Visibility = System.Windows.Visibility.Visible
            Me._txtIdAsociado.Visibility = System.Windows.Visibility.Visible
            Me._txtNombreAsociado.Visibility = System.Windows.Visibility.Visible
            Me._lblIdAsociado.Visibility = System.Windows.Visibility.Visible
            Me._lblNombreAsociado.Visibility = System.Windows.Visibility.Visible
            Me._lblasociado.Visibility = System.Windows.Visibility.Visible
        End Sub



        Private Sub _lstAsociados_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If Me._lstAsociados.SelectedItem IsNot Nothing Then
                Me._presentador.CambiarAsociado()
                ControlesOcultarAsociado()
            End If
        End Sub

        Private Sub ControlesOcultarAsociado()
            Me._lstAsociados.Visibility = System.Windows.Visibility.Collapsed
            Me._btnConsultarAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._txtIdAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._txtNombreAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._lblasociado.Visibility = System.Windows.Visibility.Collapsed
            Me._txtAsociado.Visibility = System.Windows.Visibility.Visible
            Me._lblasociado2.Visibility = System.Windows.Visibility.Visible
            'Me._txtAsociadoId.Visibility = System.Windows.Visibility.Visible
            Me._lblIdAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._lblNombreAsociado.Visibility = System.Windows.Visibility.Collapsed
        End Sub

        Public Sub PintarAsociado()
            Me._txtAsociado.BorderBrush = New SolidColorBrush(Colors.LightGreen)
        End Sub

        Public Property NombreAsociado() As String Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property

        Public ReadOnly Property FechaFactura() As String Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.FechaFactura
            Get
                Return Me._dpkFechaFactura.SelectedDate.ToString()
            End Get
        End Property

        Public ReadOnly Property Id() As String Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Id
            Get
                Return Me._txtId.Text
            End Get
        End Property


        Public Sub al_Registrar_Item(ByVal sender As Object, ByVal args As ExecutedRoutedEventArgs)
            Dim cproforma As Integer = args.Parameter
            Me._presentador.IrConsultarRegistrointernacional(cproforma)
        End Sub

        Public Sub al_Pagar_Item(ByVal sender As Object, ByVal args As ExecutedRoutedEventArgs)
            Dim cproforma As Integer = args.Parameter
            Me._presentador.IrConsultarPagointernacional(cproforma)
        End Sub

        Public Sub al_VerProforma_Item(ByVal sender As Object, ByVal args As ExecutedRoutedEventArgs)
            Dim cproforma As Integer = args.Parameter
        End Sub

        Public Shared Registrar_Item As New RoutedCommand("Registrar_Item", GetType(ConsultarFacInternacionalesProforma))
        Public Shared Pagar_Item As New RoutedCommand("Pagar_Item", GetType(ConsultarFacInternacionalesProforma))
        Public Shared VerProforma_Item As New RoutedCommand("VerProforma_Item", GetType(ConsultarFacInternacionalesProforma))

        Public WriteOnly Property Count As Integer Implements Contratos.FacInternacionales.IConsultarFacInternacionalesProforma.Count
            Set(ByVal value As Integer)
                _lblHits.Text = value
            End Set
        End Property

        Private Sub _txtAsociadoInt_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs)
            ControlesMostrarAsociadoInternacional()
        End Sub

        Private Sub ControlesMostrarAsociadoInternacional()
            Me._txtAsociadoInt.Visibility = System.Windows.Visibility.Collapsed
            Me._lblasociado2Int.Visibility = System.Windows.Visibility.Collapsed
            Me._lstAsociadosInt.Visibility = System.Windows.Visibility.Visible
            Me._lstAsociadosInt.IsEnabled = True
            Me._btnConsultarAsociadoInt.Visibility = System.Windows.Visibility.Visible
            Me._txtIdAsociadoInt.Visibility = System.Windows.Visibility.Visible
            Me._txtNombreAsociadoInt.Visibility = System.Windows.Visibility.Visible
            Me._lblIdAsociadoIntFiltrar.Visibility = System.Windows.Visibility.Visible
            Me._lblNombreAsociadoIntFiltrar.Visibility = System.Windows.Visibility.Visible
            Me._lblasociadoInt.Visibility = System.Windows.Visibility.Visible
        End Sub

        Private Sub ControlesOcultarAsociadoInternacional()
            Me._lstAsociadosInt.Visibility = System.Windows.Visibility.Collapsed
            Me._btnConsultarAsociadoInt.Visibility = System.Windows.Visibility.Collapsed
            Me._txtIdAsociadoInt.Visibility = System.Windows.Visibility.Collapsed
            Me._txtNombreAsociadoInt.Visibility = System.Windows.Visibility.Collapsed
            Me._lblasociadoInt.Visibility = System.Windows.Visibility.Collapsed
            Me._txtAsociadoInt.Visibility = System.Windows.Visibility.Visible
            Me._lblasociado2Int.Visibility = System.Windows.Visibility.Visible
            Me._lblIdAsociadoIntFiltrar.Visibility = System.Windows.Visibility.Collapsed
            Me._lblNombreAsociadoIntFiltrar.Visibility = System.Windows.Visibility.Collapsed
        End Sub

        Private Sub _btnConsultarAsociadoInt_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
            Me._presentador.BuscarAsociadoInternacional()
        End Sub

        Private Sub _lstAsociadosInt_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs)
            If Me._lstAsociadosInt.SelectedItem IsNot Nothing Then
                Me._presentador.CambiarAsociadoInternacional()
                ControlesOcultarAsociadoInternacional()
            End If
        End Sub




    End Class

End Namespace
