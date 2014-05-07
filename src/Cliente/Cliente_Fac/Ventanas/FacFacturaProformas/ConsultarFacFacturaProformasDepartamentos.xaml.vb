Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturaProformas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacFacturaProformas
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacFacturaProformas
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ConsultarFacFacturaProformasDepartamentos
        Inherits Page
        Implements IConsultarFacFacturaProformasDepartamentos

        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorConsultarFacFacturaProformasDepartamentos
        Private _cargada As Boolean

#Region "IConsultarFacFacturaProformasDepartamentos"

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

        Public Property FacFacturaProformaFiltrar() As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.FacFacturaProformaFiltrar
            Get
                Return Me._splFiltro.DataContext
            End Get
            Set(ByVal value As Object)
                Me._splFiltro.DataContext = value
            End Set
        End Property

        Public ReadOnly Property FacFacturaProformaSeleccionado() As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.FacFacturaProformaSeleccionado
            Get
                Return Me._lstResultados.SelectedItem
            End Get
        End Property


        Public Property Resultados() As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Resultados
            Get
                Return Me._lstResultados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultados.DataContext = value
            End Set
        End Property

        'Public Property Id() As Integer Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Id
        '    Get
        '        Return Me._lstId.Text
        '    End Get
        '    Set(ByVal value As Integer)
        '        Me._lstId.Text = value
        '    End Set
        'End Property

        Public Property CurSortCol() As System.Windows.Controls.GridViewColumnHeader Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.CurSortCol
            Get
                Return _CurSortCol
            End Get
            Set(ByVal value As GridViewColumnHeader)
                _CurSortCol = value
            End Set
        End Property

        Public Property CurAdorner() As SortAdorner Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.CurAdorner
            Get
                Return _CurAdorner
            End Get
            Set(ByVal value As SortAdorner)
                _CurAdorner = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.ListaResultados
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

            Me._presentador = New PresentadorConsultarFacFacturaProformasDepartamentos(Me)
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
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

        Public Property Asociado As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Asociado

            Get
                Return Me._lstAsociados.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.SelectedItem = value
                Me._lstAsociados.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Asociados
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
                validarCamposVacios()
                Me._btnConsulta.Focus()
            End If
        End Sub
        'Public Property Banco As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Banco
        '    Get
        '        Return Me._cbxBanco.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxBanco.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Bancos As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Bancos
        '    Get
        '        Return Me._cbxBanco.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxBanco.DataContext = value
        '    End Set
        'End Property

        'Public Property Idioma As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Idioma
        '    Get
        '        Return Me._cbxIdioma.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxIdioma.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Idiomas As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Idiomas
        '    Get
        '        Return Me._cbxIdioma.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxIdioma.DataContext = value
        '    End Set
        'End Property

        'Public Property Moneda As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Moneda
        '    Get
        '        Return Me._cbxMoneda.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxMoneda.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Monedas As Object Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Monedas
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
                ElseIf nom = "_txtId" Then
                    Me._presentador.Consultar()
                    validarCamposVacios()
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
                Me._btnConsulta.IsDefault = True
                Me._btnConsulta.Focus()
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

        Public Property NombreAsociado() As String Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property

        Public ReadOnly Property FechaFactura() As String Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.FechaFactura
            Get
                Return Me._dpkFechaFactura.SelectedDate.ToString()
            End Get
        End Property

        Public ReadOnly Property Id() As String Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Id
            Get
                Return Me._txtId.Text
            End Get
        End Property

        'Public ReadOnly Property CreditoSent As String Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.CreditoSent
        '    Get
        '        Return Me._txtCreditoSent.Text
        '    End Get
        'End Property


        Public WriteOnly Property Cba As Double Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Cba
            Set(ByVal value As Double)
                Me._txtCba.Text = value
            End Set
        End Property

        'Public WriteOnly Property Cbf As Double Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Cbf
        '    Set(ByVal value As Double)
        '        Me._txtCbf.Text = value
        '    End Set
        'End Property

        Public WriteOnly Property Cbp As Double Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Cbp
            Set(ByVal value As Double)
                Me._txtCbp.Text = value
            End Set
        End Property

        Public WriteOnly Property Cbr As Double Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Cbr
            Set(ByVal value As Double)
                Me._txtCbr.Text = value
            End Set
        End Property

        Public WriteOnly Property Cda As Double Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Cda
            Set(ByVal value As Double)
                Me._txtCda.Text = value
            End Set
        End Property

        'Public WriteOnly Property Cdf As Double Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Cdf
        '    Set(ByVal value As Double)
        '        Me._txtCdf.Text = value
        '    End Set
        'End Property

        Public WriteOnly Property Cdp As Double Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Cdp
            Set(ByVal value As Double)
                Me._txtCdp.Text = value
            End Set
        End Property

        Public WriteOnly Property Cdr As Double Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Cdr
            Set(ByVal value As Double)
                Me._txtCdr.Text = value
            End Set
        End Property

        Public WriteOnly Property Count As Integer Implements Contratos.FacFacturaProformas.IConsultarFacFacturaProformasDepartamentos.Count
            Set(value As Integer)
                _lblHits.Text = value
            End Set
        End Property

        Private Sub validarCamposVacios()

            Dim todosCamposVacios As Boolean
            todosCamposVacios = True

            If Me._txtId.Text <> "" Then
                todosCamposVacios = False
                Me._txtId.Focus()
            End If

            If Me._dpkFechaFactura.Text <> "" Then
                todosCamposVacios = False
                Me._dpkFechaFactura.Focus()
            End If

            If todosCamposVacios Then
                Me._txtId.Focus()
            End If

        End Sub

        Private Sub _dpkFechaFactura_SelectedDateChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs)

            If (Me._dpkFechaFactura.SelectedDate IsNot Nothing) Then
                Me._btnConsulta.IsDefault = True
                Me._btnConsulta.Focus()

            End If

        End Sub
    End Class
End Namespace
