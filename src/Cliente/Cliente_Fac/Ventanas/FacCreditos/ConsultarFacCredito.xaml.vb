Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacCreditos
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacCreditos
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacCreditos
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarFacCredito
        Inherits Page
        Implements IConsultarFacCredito



        Private _presentador As PresentadorConsultarFacCredito
        Private _cargada As Boolean

#Region "IConsultarFacCredito"

        Public Property EstaCargada() As Boolean Implements IPaginaBasefac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBasefac.FocoPredeterminado
            Me._txtId.Focus()
        End Sub


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.FacCreditos.IConsultarFacCredito.HabilitarCampos
            Set(ByVal value As Boolean)                
                'Me._txtId.IsEnabled = value
                Me._txtCreditoSent.IsEnabled = value
                Me._txtBCreditoBf.IsEnabled = value
                Me._txtBCredito.IsEnabled = value
                Me._txtConcepto.IsEnabled = value
                Me._txtBSaldo.IsEnabled = value
                Me._dpkFechaCredito.IsEnabled = value
                Me._cbxBanco.IsEnabled = value
                Me._txtAsociado.IsEnabled = value
                Me._cbxIdioma.IsEnabled = value
                Me._cbxMoneda.IsEnabled = value                
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.FacCreditos.IConsultarFacCredito.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal FacCredito As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarFacCredito(Me, FacCredito)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.pararegresar()
        End Sub

        Private Sub _btnLimpiar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Limpiar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarFacCredito, "Eliminar FacCredito", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                Me._presentador.Eliminar()
            End If
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            End If
            PintarAsociado()
        End Sub

        Public Property FacCredito As Object Implements Contratos.FacCreditos.IConsultarFacCredito.FacCredito
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Property Banco As Object Implements Contratos.FacCreditos.IConsultarFacCredito.Banco
            Get
                Return Me._cbxBanco.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxBanco.SelectedItem = value
            End Set
        End Property

        Public Property Bancos As Object Implements Contratos.FacCreditos.IConsultarFacCredito.Bancos
            Get
                Return Me._cbxBanco.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxBanco.DataContext = value
            End Set
        End Property

        Public Property Idioma As Object Implements Contratos.FacCreditos.IConsultarFacCredito.Idioma
            Get
                Return Me._cbxIdioma.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxIdioma.SelectedItem = value
            End Set
        End Property

        Public Property Idiomas As Object Implements Contratos.FacCreditos.IConsultarFacCredito.Idiomas
            Get
                Return Me._cbxIdioma.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxIdioma.DataContext = value
            End Set
        End Property

        Public Property Moneda As Object Implements Contratos.FacCreditos.IConsultarFacCredito.Moneda
            Get
                Return Me._cbxMoneda.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxMoneda.SelectedItem = value
            End Set
        End Property

        Public Property Monedas As Object Implements Contratos.FacCreditos.IConsultarFacCredito.Monedas
            Get
                Return Me._cbxMoneda.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxMoneda.DataContext = value
            End Set
        End Property

        Public Property BCreditoBf() As Double Implements Contratos.FacCreditos.IConsultarFacCredito.BCreditoBf
            Get
                Return GetFormatoDouble(Me._txtBCreditoBf.Text)
            End Get
            Set(ByVal value As Double)
                Me._txtBCreditoBf.Text = SetFormatoDouble(value)
            End Set
        End Property

        Public Property BSaldo() As Double Implements Contratos.FacCreditos.IConsultarFacCredito.BSaldo
            Get
                Return GetFormatoDouble(Me._txtBSaldo.Text)
            End Get
            Set(ByVal value As Double)
                Me._txtBSaldo.Text = SetFormatoDouble(value)
            End Set
        End Property

        Private Sub _txtBCredito_LostFocus(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles _txtBCredito.LostFocus
            Dim a As Object = Me._cbxIdioma.SelectionBoxItem
            Me._presentador.BuscarTasa()
        End Sub

        Public Property Resultados() As Object Implements Contratos.FacCreditos.IConsultarFacCredito.Resultados
            Get
                Return Me._lstResultadosFormas.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultadosFormas.DataContext = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.FacCreditos.IConsultarFacCredito.ListaResultados
            Get
                Return Me._lstResultadosFormas
            End Get
            Set(ByVal value As ListView)
                Me._lstResultadosFormas = value
            End Set
        End Property

        Public Property Asociado As Object Implements Contratos.FacCreditos.IConsultarFacCredito.Asociado

            Get
                Return Me._lstAsociados.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.SelectedItem = value
                Me._lstAsociados.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.FacCreditos.IConsultarFacCredito.Asociados
            Get
                Return Me._lstAsociados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.DataContext = value
            End Set
        End Property

        'Private Sub _btnConsultar(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '    Dim nom As String = DirectCast(sender, Button).Name.ToString
        '    If nom = "_btnConsultarAsociado" Then
        '        Me._presentador.BuscarAsociado2()
        '    ElseIf nom = "_btnCancelar" Then
        '        Me._presentador.Cancelar()
        '    ElseIf nom = "_btnConsulta" Then
        '        Me._presentador.Consultar()
        '    End If
        'End Sub

        Private Sub _btnConsultarAsociado_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.BuscarAsociado2()
        End Sub
        Private Sub _Consultar_Enter(ByVal sender As Object, ByVal e As KeyEventArgs)
            If (e.Key = Key.Enter) Then
                Dim nom As String = DirectCast(sender, ByTTextBox).Name.ToString
                If nom = "_txtIdAsociado" Or nom = "_txtNombreAsociado" Then
                    Me._presentador.BuscarAsociado2()
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

        Public Property NombreAsociado() As String Implements Contratos.FacCreditos.IConsultarFacCredito.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.FacCreditos.IConsultarFacCredito.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.FacCreditos.IConsultarFacCredito.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property

        Public Function GetFormatoDouble(texto As String) As String
            Dim valor As String = Replace(texto, ",", "")
            valor = Replace(texto, ".", ",")
            'Convert.ToDecimal(_txtBFormaMan.Text).ToString("N2")
            Return valor
        End Function

        Public Function SetFormatoDouble(ByVal value As Double) As String

            'Console.WriteLine("{0,-6} {1}", Name + ":", value.ToString("N3", System.Globalization.CultureInfo.CreateSpecificCulture("en-US").NumberFormat))

            Return Replace(value.ToString("N2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US").NumberFormat), ",", "")
            'Return Replace(value.ToString("C2", New System.Globalization.CultureInfo("en-us")).Remove(0, 1), ",", "")
        End Function

    End Class
End Namespace