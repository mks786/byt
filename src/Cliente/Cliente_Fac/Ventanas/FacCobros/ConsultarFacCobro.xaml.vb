Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacCobros
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacCobros
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacCobros
    Partial Public Class ConsultarFacCobro
        Inherits Page
        Implements IConsultarFacCobro

        Private _presentador As PresentadorConsultarFacCobro
        Private _cargada As Boolean

#Region "IConsultarFacCobro"

        Public Property EstaCargada As Boolean Implements IPaginaBaseFac.EstaCargada
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


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.FacCobros.IConsultarFacCobro.HabilitarCampos
            Set(ByVal value As Boolean)
                Dim valor As Integer = _txtpermisos.Text
                If valor = 1 Then
                    Me._txtDetalle.IsEnabled = value
                    ' Me._txtId.IsReadOnly = value
                    _dpkFechaCobro.IsEnabled = value
                    _dpkFechaB.IsEnabled = value
                    _cbxIdioma.IsEnabled = value
                    _cbxMoneda.IsEnabled = value
                    If value = True Then
                        _txtDetalle.IsReadOnly = False
                        _txtEstadoCuenta.IsReadOnly = False
                    Else
                        _txtDetalle.IsReadOnly = True
                        _txtEstadoCuenta.IsReadOnly = True
                    End If
                    _cbxBanco.IsEnabled = value

                    _btnConsultarfactura2.IsEnabled = value
                    _btnConsultarfactura.IsEnabled = value
                    _btnagregarfactura2.IsEnabled = value
                    _btnmodificarforma.IsEnabled = value
                Else
                    _cbxBanco.IsEnabled = value
                End If
                '_btnModificar.IsEnabled = value
                '_btnEliminar.IsEnabled = value
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.FacCobros.IConsultarFacCobro.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal FacCobro As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarFacCobro(Me, FacCobro)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.pararegresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarFacCobro, "Eliminar FacCobro", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                Me._presentador.Eliminar()
            End If
        End Sub

        Private Sub _btnLimpiar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Limpiar()
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            End If
            PintarAsociado()
        End Sub

        Public Property FacCobro As Object Implements Contratos.FacCobros.IConsultarFacCobro.FacCobro
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Property Idioma As Object Implements Contratos.FacCobros.IConsultarFacCobro.Idioma
            Get
                Return Me._cbxIdioma.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxIdioma.SelectedItem = value
            End Set
        End Property

        Public Property Idiomas As Object Implements Contratos.FacCobros.IConsultarFacCobro.Idiomas
            Get
                Return Me._cbxIdioma.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxIdioma.DataContext = value
            End Set
        End Property

        Public Property Moneda As Object Implements Contratos.FacCobros.IConsultarFacCobro.Moneda
            Get
                Return Me._cbxMoneda.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxMoneda.SelectedItem = value
            End Set
        End Property

        Public Property Monedas As Object Implements Contratos.FacCobros.IConsultarFacCobro.Monedas
            Get
                Return Me._cbxMoneda.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxMoneda.DataContext = value
            End Set
        End Property

        Private Sub _btnVerFacturas_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura_2.Visibility = Windows.Visibility.Visible
            Me._btnagregarfactura2.Visibility = Windows.Visibility.Visible
            _btnsaliragregarfactura2.Visibility = Windows.Visibility.Visible
            Me._lstFacturaCobro.Visibility = Windows.Visibility.Collapsed
            Me._btnConsultarfactura2.Visibility = Windows.Visibility.Collapsed
            Me._txtSumaBono.Visibility = Windows.Visibility.Collapsed
            Me._txtSumaBonoBf.Visibility = Windows.Visibility.Collapsed
            Me._presentador.VerFacturas()
        End Sub

        Private Sub _btnAgregarFacturas2_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura_2.Visibility = Windows.Visibility.Collapsed
            Me._btnagregarfactura2.Visibility = Windows.Visibility.Collapsed
            Me._lstFacturaCobro.Visibility = Windows.Visibility.Visible
            Me._btnConsultarfactura2.Visibility = Windows.Visibility.Visible
            _btnsaliragregarfactura2.Visibility = Windows.Visibility.Collapsed
            Me._txtSumaBono.Visibility = Windows.Visibility.Visible
            Me._txtSumaBonoBf.Visibility = Windows.Visibility.Visible
            Me._presentador.AgregarFacturas2()
        End Sub

        Private Sub _btnsalirAgregarFacturas2_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura_2.Visibility = Windows.Visibility.Collapsed
            Me._btnagregarfactura2.Visibility = Windows.Visibility.Collapsed
            _btnsaliragregarfactura2.Visibility = Windows.Visibility.Collapsed
            Me._lstFacturaCobro.Visibility = Windows.Visibility.Visible
            Me._btnConsultarfactura2.Visibility = Windows.Visibility.Visible
            Me._txtSumaBono.Visibility = Windows.Visibility.Visible
            Me._txtSumaBonoBf.Visibility = Windows.Visibility.Visible
            'Me._presentador.AgregarFacturas2()
        End Sub

        Private Sub _btnAgregarForma_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura.Visibility = Windows.Visibility.Collapsed
            Me._btnagregarforma.Visibility = Windows.Visibility.Collapsed
            Me._btnsaliragregarforma.Visibility = Windows.Visibility.Collapsed
            _btnFormaManual.Visibility = Windows.Visibility.Collapsed
            Me._btnConsultarfactura.Visibility = Windows.Visibility.Visible
            Me._lstForma.Visibility = Windows.Visibility.Visible
            Me._txtSumaBForma.Visibility = Windows.Visibility.Visible
            Me._txtSumaBFormaBf.Visibility = Windows.Visibility.Visible
            Me._btnFormaManual.Visibility = Windows.Visibility.Visible
            Me._presentador.AgregarForma()
        End Sub

        Private Sub _btnSalirAgregarForma_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura.Visibility = Windows.Visibility.Collapsed
            Me._btnagregarforma.Visibility = Windows.Visibility.Collapsed
            Me._btnsaliragregarforma.Visibility = Windows.Visibility.Collapsed
            _btnFormaManual.Visibility = Windows.Visibility.Collapsed
            Me._btnConsultarfactura.Visibility = Windows.Visibility.Visible
            Me._lstForma.Visibility = Windows.Visibility.Visible
            Me._txtSumaBForma.Visibility = Windows.Visibility.Visible
            Me._txtSumaBFormaBf.Visibility = Windows.Visibility.Visible
            Me._btnFormaManual.Visibility = Windows.Visibility.Visible
            Me.WPFormaManual.Visibility = Windows.Visibility.Collapsed
            WPFormaManuallabel.Visibility = Windows.Visibility.Collapsed
            Me._txtBForma.Visibility = Windows.Visibility.Collapsed
            Me._txtBFormaBf.Visibility = Windows.Visibility.Collapsed
            Me._txtXForma.Visibility = Windows.Visibility.Collapsed
            Me._btnmodificarforma.Visibility = Windows.Visibility.Collapsed
            Me._btnsalirmodificarforma.Visibility = Windows.Visibility.Collapsed
            'Me._presentador.AgregarForma()
        End Sub

        Private Sub _btnAgregarFormaManual_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura.Visibility = Windows.Visibility.Collapsed
            Me._btnagregarforma.Visibility = Windows.Visibility.Collapsed
            _btnFormaManual.Visibility = Windows.Visibility.Collapsed
            Me.WPFormaManual.Visibility = Windows.Visibility.Collapsed
            WPFormaManuallabel.Visibility = Windows.Visibility.Collapsed
            Me._btnConsultarfactura.Visibility = Windows.Visibility.Visible
            'Me._lstForma.Visibility = Windows.Visibility.Visible
            ' Me._txtSumaBForma.Visibility = Windows.Visibility.Visible
            'Me._txtSumaBFormaBf.Visibility = Windows.Visibility.Visible
            Me._btnFormaManual.Visibility = Windows.Visibility.Visible
            Me._presentador.AgregarFormaManual()
        End Sub

        Private Sub _lstForma_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Me._txtBForma.Visibility = Windows.Visibility.Visible
            Me._txtBFormaBf.Visibility = Windows.Visibility.Visible
            Me._txtXForma.Visibility = Windows.Visibility.Visible
            Me._btnmodificarforma.Visibility = Windows.Visibility.Visible
            Me._btnsalirmodificarforma.Visibility = Windows.Visibility.Visible
            Me._presentador.MostrarForma()
        End Sub

        Private Sub _btnModificarForma_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._txtBForma.Visibility = Windows.Visibility.Collapsed
            Me._txtBFormaBf.Visibility = Windows.Visibility.Collapsed
            Me._txtXForma.Visibility = Windows.Visibility.Collapsed
            Me._btnmodificarforma.Visibility = Windows.Visibility.Collapsed
            Me._btnsalirmodificarforma.Visibility = Windows.Visibility.Collapsed
            Me._presentador.ModificarForma()
        End Sub

        Public ReadOnly Property FacFormaSeleccionada As Object Implements Contratos.FacCobros.IConsultarFacCobro.FacFormaSeleccionada
            Get
                Return Me._lstForma.SelectedItem
            End Get
        End Property

        Private Sub _btnVerCreditos_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura.Visibility = Windows.Visibility.Visible
            Me._btnagregarforma.Visibility = Windows.Visibility.Visible
            Me._btnsaliragregarforma.Visibility = Windows.Visibility.Visible
            Me._btnConsultarfactura.Visibility = Windows.Visibility.Collapsed
            Me._lstForma.Visibility = Windows.Visibility.Collapsed
            Me._txtSumaBForma.Visibility = Windows.Visibility.Collapsed
            Me._txtSumaBFormaBf.Visibility = Windows.Visibility.Collapsed
            Me.WPFormaManual.Visibility = Windows.Visibility.Collapsed
            Me.WPFormaManuallabel.Visibility = Windows.Visibility.Collapsed
            Me._btnFormaManual.Visibility = Windows.Visibility.Collapsed
            Me._presentador.VerCreditos()
        End Sub

        Private Sub _btnFormaManual_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._lstfactura.Visibility = Windows.Visibility.Collapsed
            Me._btnagregarforma.Visibility = Windows.Visibility.Collapsed
            Me._btnConsultarfactura.Visibility = Windows.Visibility.Collapsed
            ' Me._lstForma.Visibility = Windows.Visibility.Collapsed
            ' Me._txtSumaBForma.Visibility = Windows.Visibility.Collapsed
            ' Me._txtSumaBFormaBf.Visibility = Windows.Visibility.Collapsed
            _btnFormaManual.Visibility = Windows.Visibility.Collapsed
            Me.WPFormaManual.Visibility = Windows.Visibility.Visible
            Me.WPFormaManuallabel.Visibility = Windows.Visibility.Visible
            'Me._presentador.VerCreditos()
        End Sub

        Public Property ResultadosFactura2() As Object Implements Contratos.FacCobros.IConsultarFacCobro.ResultadosFactura2
            Get
                Return Me._lstfactura_2.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstfactura_2.DataContext = value
            End Set
        End Property

        Public Property ResultadosFactura() As Object Implements Contratos.FacCobros.IConsultarFacCobro.ResultadosFactura
            Get
                Return Me._lstfactura.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstfactura.DataContext = value
            End Set
        End Property

        Public Property ResultadosFacturaCobro() As Object Implements Contratos.FacCobros.IConsultarFacCobro.ResultadosFacturaCobro
            Get
                Return Me._lstFacturaCobro.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstFacturaCobro.DataContext = value
            End Set
        End Property

        Public Property ResultadosForma() As Object Implements Contratos.FacCobros.IConsultarFacCobro.ResultadosForma
            Get
                Return Me._lstForma.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstForma.DataContext = value
            End Set
        End Property

        'Public Property MensajeErrorCobro As String Implements Contratos.FacCobros.IConsultarFacCobro.MensajeErrorCobro
        '    Get
        '        Return Me._txtMensajeError.Text
        '    End Get
        '    Set(ByVal value As String)
        '        Me._txtMensajeError.Text = value
        '    End Set

        'End Property

        Public Property SumaBforma As Double Implements Contratos.FacCobros.IConsultarFacCobro.SumaBforma
            Get
                Return GetFormatoDouble(Me._txtSumaBForma.Text)
            End Get
            Set(ByVal value As Double)
                Me._txtSumaBForma.Text = SetFormatoDouble(value)
            End Set
        End Property

        Public Property SumaBformaBf As Double Implements Contratos.FacCobros.IConsultarFacCobro.SumaBformaBf
            Get
                Return GetFormatoDouble(Me._txtSumaBFormaBf.Text)
            End Get
            Set(ByVal value As Double)
                Me._txtSumaBFormaBf.Text = SetFormatoDouble(value)
            End Set
        End Property

        Public Property SumaBono As Double Implements Contratos.FacCobros.IConsultarFacCobro.SumaBono
            Get
                Return GetFormatoDouble(Me._txtSumaBono.Text)
            End Get
            Set(ByVal value As Double)
                Me._txtSumaBono.Text = SetFormatoDouble(value)
            End Set
        End Property

        Public Property SumaBonoBf As Double Implements Contratos.FacCobros.IConsultarFacCobro.SumaBonoBf
            Get
                Return GetFormatoDouble(Me._txtSumaBonoBf.Text)
            End Get
            Set(ByVal value As Double)
                Me._txtSumaBonoBf.Text = SetFormatoDouble(value)
            End Set
        End Property

        Public Property Bforma As Double Implements Contratos.FacCobros.IConsultarFacCobro.Bforma
            Get
                Return GetFormatoDouble(Me._txtBForma.Text)
            End Get
            Set(ByVal value As Double)
                Me._txtBForma.Text = SetFormatoDouble(value)
            End Set
        End Property

        Public Property BformaBf As Double Implements Contratos.FacCobros.IConsultarFacCobro.BformaBf
            Get
                Return GetFormatoDouble(Me._txtBFormaBf.Text)
            End Get
            Set(ByVal value As Double)
                Me._txtBFormaBf.Text = SetFormatoDouble(value)
            End Set
        End Property

        Public Property Credito As Integer Implements Contratos.FacCobros.IConsultarFacCobro.Credito
            Get
                Return Me._txtCredito.Text
            End Get
            Set(ByVal value As Integer)
                Me._txtCredito.Text = value
            End Set
        End Property

        Public Property Xforma As String Implements Contratos.FacCobros.IConsultarFacCobro.Xforma
            Get
                Return Me._txtXForma.Text
            End Get
            Set(ByVal value As String)
                Me._txtXForma.Text = value
            End Set
        End Property

        Public Property Banco As Object Implements Contratos.FacCobros.IConsultarFacCobro.Banco
            Get
                Return Me._cbxBanco.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxBanco.SelectedItem = value
            End Set
        End Property

        Public Property Bancos As Object Implements Contratos.FacCobros.IConsultarFacCobro.Bancos
            Get
                Return Me._cbxBanco.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxBanco.DataContext = value
            End Set
        End Property


        Public Property Asociado As Object Implements Contratos.FacCobros.IConsultarFacCobro.Asociado

            Get
                Return Me._lstAsociados.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.SelectedItem = value
                Me._lstAsociados.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.FacCobros.IConsultarFacCobro.Asociados
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

            End If
        End Sub

        Private Sub _btnConsultarAsociado_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.BuscarAsociado2()
        End Sub

        Private Sub _Consultar_Enter(ByVal sender As Object, ByVal e As KeyEventArgs)
            If (e.Key = Key.Enter) Then
                Dim nom As String = DirectCast(sender, ByTTextBox).Name.ToString
                If nom = "_txtIdAsociado" Or nom = "_txtNombreAsociado" Then
                    Me._presentador.BuscarAsociado2()
                ElseIf nom = "_txtCbanco" Then
                    Me._presentador.ConsultarBanco()
                End If
            Else
                If e.Key = Key.Tab Then
                    Dim nom As String = DirectCast(sender, ByTTextBox).Name.ToString
                    If nom = "_txtCbanco" Then
                        Me._presentador.ConsultarBanco()
                    End If
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

        Public Property NombreAsociado() As String Implements Contratos.FacCobros.IConsultarFacCobro.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.FacCobros.IConsultarFacCobro.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.FacCobros.IConsultarFacCobro.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property


        'Public Property MensajeErrorCobro As String Implements Contratos.FacCobros.IConsultarFacCobro.MensajeErrorCobro
        '    Get

        '    End Get
        '    Set(ByVal value As String)

        '    End Set
        'End Property

        Public Property Permisos As Integer Implements Contratos.FacCobros.IConsultarFacCobro.Permisos
            Get
                Dim valor As Integer = _txtpermisos.Text
                Return (valor)
            End Get
            Set(ByVal value As Integer)
                If value = 1 Then
                    _btnModificar.IsEnabled = True
                    _btnEliminar.IsEnabled = False
                Else
                    _btnModificar.IsEnabled = True
                    _btnEliminar.IsEnabled = False
                End If
                _txtpermisos.Text = value
            End Set
        End Property

        Public Property Valor As Object Implements Contratos.FacCobros.IConsultarFacCobro.Valor
            Get
                Return Me._cbxValoresPago.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxValoresPago.SelectedItem = value
            End Set
        End Property

        Public Property Valores As Object Implements Contratos.FacCobros.IConsultarFacCobro.Valores
            Get
                Return Me._cbxValoresPago.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxValoresPago.DataContext = value
            End Set
        End Property

        Public Property BformaMan As Double? Implements Contratos.FacCobros.IConsultarFacCobro.BformaMan
            Get
                If IsNumeric(_txtBFormaMan.Text) Then
                    Return GetFormatoDouble(_txtBFormaMan.Text)
                Else
                    Return 0
                End If
            End Get
            Set(ByVal value As Double?)
                If value IsNot Nothing Then
                    _txtBFormaMan.Text = SetFormatoDouble(value)
                Else
                    _txtBFormaMan.Text = ""
                End If
            End Set
        End Property

        Public Property BformaBfMan As Double Implements Contratos.FacCobros.IConsultarFacCobro.BformaBfMan
            Get
                Return GetFormatoDouble(_txtBFormaBfMan.Text)
            End Get
            Set(ByVal value As Double)
                _txtBFormaBfMan.Text = SetFormatoDouble(value)
            End Set
        End Property

        Public Property XformaMan As String Implements Contratos.FacCobros.IConsultarFacCobro.XformaMan
            Get
                Return _txtXFormaMan.Text
            End Get
            Set(ByVal value As String)
                _txtXFormaMan.Text = value
            End Set
        End Property

        Public Function GetFormatoDouble(ByVal texto As String) As String
            'Dim valor As String = Replace(texto, ",", "")
            'valor = Replace(valor, ".", ",")

            'Return valor
            Return _presentador.GetFormatoDouble2(texto)
        End Function

        Public Function SetFormatoDouble(ByVal value As Double) As String

            'Console.WriteLine("{0,-6} {1}", Name + ":", value.ToString("N3", System.Globalization.CultureInfo.CreateSpecificCulture("en-US").NumberFormat))

            'Return value.ToString("N2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US").NumberFormat)
            Return _presentador.SetFormatoDouble2(value)
            'Return Replace(value.ToString("C2", New System.Globalization.CultureInfo("en-us")).Remove(0, 1), ",", "")
        End Function

        Public Sub Al_Eliminar_Pago(ByVal sender As Object, ByVal args As ExecutedRoutedEventArgs)
            Dim idforma As Integer = args.Parameter
            Me._presentador.eliminar_pago(idforma)
        End Sub
        Public Shared Eliminar_Pago As New RoutedCommand("Eliminar_Pago", GetType(ConsultarFacCobro))

        Public ReadOnly Property Cbanco As String Implements Contratos.FacCobros.IConsultarFacCobro.Cbanco
            Get
                Return _txtCbanco.Text
            End Get
        End Property

        Private Sub _dpkFecha_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs)

        End Sub

    End Class
End Namespace