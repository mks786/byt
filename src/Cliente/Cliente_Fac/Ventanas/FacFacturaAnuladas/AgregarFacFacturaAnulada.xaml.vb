Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturaAnuladas
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacFacturaAnuladas
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacFacturaAnuladas
    Partial Public Class AgregarFacFacturaAnulada
        Inherits Page
        Implements IAgregarFacFacturaAnulada


        Private _presentador As PresentadorAgregarFacFacturaAnulada
        Private _cargada As Boolean

#Region "IAgregarFacFacturaAnulada"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._txtFactura.Focus()
        End Sub

        Public Property FacFacturaAnulada() As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.FacFacturaAnulada
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub


#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorAgregarFacFacturaAnulada(Me)
        End Sub

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
        End Sub

        'Private Sub _btnAceptar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '    Me._presentador.Aceptar()
        'End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            End If
            PintarAsociado()
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

        Public Property Asociado As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Asociado

            Get
                Return Me._lstAsociados.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.SelectedItem = value
                Me._lstAsociados.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Asociados
            Get
                Return Me._lstAsociados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.DataContext = value
            End Set
        End Property

        Public Property Motivos As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Motivos
            Get
                Return Me._cbxMotivo.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxMotivo.DataContext = value
            End Set
        End Property

        Public Property Motivo As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Motivo

            Get
                Return Me._cbxMotivo.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxMotivo.SelectedItem = value
                'Me._cbxMotivo.ScrollIntoView(value)
            End Set
        End Property

        Public Property Motivo2 As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Motivo2

            Get
                Return Me._cbxMotivo2.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxMotivo2.SelectedItem = value
                'Me._cbxMotivo.ScrollIntoView(value)
            End Set
        End Property

        Public Property Motivos2 As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Motivos2
            Get
                Return Me._cbxMotivo2.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxMotivo2.DataContext = value
            End Set
        End Property

        Public ReadOnly Property Localidad() As Char Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Localidad
            Get
                If Not String.IsNullOrEmpty(Me._cbxLocal.Text) Then
                    Return (Me._cbxLocal.Text)(0)
                Else
                    Return " "c
                End If
            End Get
        End Property

        Public ReadOnly Property cpro As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.cpro
            Get
                Return (_txtCpro.Text)
            End Get
        End Property

        Private Sub _btnConsultarAsociado_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.BuscarAsociado2()
        End Sub

        Private Sub _btnAnularFactura_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Aceptar()
        End Sub

        Private Sub _btnConsultar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim nom As String = DirectCast(sender, Button).Name.ToString
            If nom = "_btnConsultarAsociado" Then
                Me._presentador.BuscarAsociado2()
            ElseIf nom = "_btn_btnProforma" Then
                'Me._presentador.Aceptar() 'aqui va la pantalla de proforma
                'ElseIf nom = "_btnConsultarCarta" Then
                '    Me._presentador.BuscarCarta()
                'ElseIf nom = "_btnConsultarInteresado" Then
                '    Me._presentador.BuscarInteresado2()
                'ElseIf nom = "_btnBuscarDepartamentoServicio" Then
                '    Me._presentador.VerDepartamentoServicios()
                'ElseIf nom = "_btnConsultarMarca" Then
                '    Me._presentador.BuscarMarca()
                'ElseIf nom = "_btnConsultarPatente" Then
                '    Me._presentador.BuscarPatente()
            End If
        End Sub

        Private Sub Consultar_Focus(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If _txtFactura.Text <> "" And _txttabulador.Text <> "2" Then
                Me._presentador.BuscarFactura()
            End If
        End Sub

        Private Sub _Consultar_Enter(ByVal sender As Object, ByVal e As KeyEventArgs)
            _txttabulador.Text = "1"
            If (e.Key = Key.Enter) Then
                Dim nom As String = DirectCast(sender, ByTTextBox).Name.ToString
                If nom = "_txtIdAsociado" Or nom = "_txtNombreAsociado" Then
                    Me._presentador.BuscarAsociado2()
                ElseIf nom = "_txtFactura" Then
                    If _txtFactura.Text <> "" Then
                        Me._presentador.BuscarFactura()
                    End If
                    'ElseIf nom = "_txtIdInteresado" Or nom = "_txtNombreInteresado" Then
                    '    Me._presentador.BuscarInteresado2()
                    'ElseIf nom = "_txtIdCarta" Or nom = "_txtNombreCarta" Or nom = "_dpkFechaCarta" Then
                    '    Me._presentador.BuscarCarta()
                    'ElseIf nom = "_txtServicioId" Or nom = "_txtServicioCod_Cont" Or nom = "_txtServicioXreferencia" Then
                    '    Me._presentador.VerDepartamentoServicios()
                    'ElseIf nom = "_txtNombreMarca" Or nom = "_txtIdMarca" Then
                    '    Me._presentador.BuscarMarca()
                    'ElseIf nom = "_txtNombrePatente" Or nom = "_txtIdPatente" Then
                    '    Me._presentador.BuscarPatente()
                End If
            Else
                If e.Key = Key.Tab Then
                    Dim nom As String = DirectCast(sender, ByTTextBox).Name.ToString
                    If nom = "_txtFactura" Then
                        If _txtFactura.Text <> "" Then
                            Me._presentador.BuscarFactura()
                            _txttabulador.Text = "2"
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub _txtAsociado_MouseDoubleClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ControlesMostrarAsociado()
        End Sub

        Private Sub ControlesMostrarAsociado()
            Me._txtAsociado.Visibility = System.Windows.Visibility.Collapsed
            'Me._txtAsociadoId.Visibility = System.Windows.Visibility.Collapsed
            Me._lstAsociados.Visibility = System.Windows.Visibility.Visible
            Me._lstAsociados.IsEnabled = True
            Me._btnConsultarAsociado.Visibility = System.Windows.Visibility.Visible
            Me._txtIdAsociado.Visibility = System.Windows.Visibility.Visible
            Me._txtNombreAsociado.Visibility = System.Windows.Visibility.Visible
            Me._lblIdAsociado.Visibility = System.Windows.Visibility.Visible
            Me._lblNombreAsociado.Visibility = System.Windows.Visibility.Visible
        End Sub

        Public Sub PintarAsociado()
            Me._txtAsociado.BorderBrush = New SolidColorBrush(Colors.LightGreen)
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
            Me._txtAsociado.Visibility = System.Windows.Visibility.Visible
            'Me._txtAsociadoId.Visibility = System.Windows.Visibility.Visible
            Me._lblIdAsociado.Visibility = System.Windows.Visibility.Collapsed
            Me._lblNombreAsociado.Visibility = System.Windows.Visibility.Collapsed
        End Sub

        Public ReadOnly Property Factura() As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Factura
            Get
                Return Me._txtFactura.Text
            End Get
        End Property

        Public Property NombreAsociado() As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property

        Public Property Secuencia() As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Secuencia
            Get
                Return Me._txtSecuencia.Text
            End Get
            Set(ByVal value As String)
                Me._txtSecuencia.Text = value
            End Set
        End Property

        Public Property Secuencia2() As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Secuencia2
            Get
                Return Me._txtSecuencia2.Text
            End Get
            Set(ByVal value As String)
                Me._txtSecuencia2.Text = value
            End Set
        End Property

        Public Property Detalle() As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Detalle
            Get
                Return Me._txtDetalle.Text
            End Get
            Set(ByVal value As String)
                Me._txtDetalle.Text = value
            End Set
        End Property

        Public Property Detalle2() As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Detalle2
            Get
                Return Me._txtDetalle2.Text
            End Get
            Set(ByVal value As String)
                Me._txtDetalle2.Text = value
            End Set
        End Property

        Public Property Control() As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Control
            Get
                Return Me._txtControl.Text
            End Get
            Set(ByVal value As String)
                Me._txtControl.Text = value
            End Set
        End Property
        'Public Property ResultadosFactura2() As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.ResultadosFactura2
        '    Get
        '        Return Me._lstfactura_2.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._lstfactura_2.DataContext = value
        '    End Set
        'End Property

        'Public Property ResultadosFactura() As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.ResultadosFactura
        '    Get
        '        Return Me._lstfactura.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._lstfactura.DataContext = value
        '    End Set
        'End Property

        'Public Property ResultadosFacturaCobro() As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.ResultadosFacturaCobro
        '    Get
        '        Return Me._lstFacturaCobro.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._lstFacturaCobro.DataContext = value
        '    End Set
        'End Property

        'Public Property ResultadosForma() As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.ResultadosForma
        '    Get
        '        Return Me._lstForma.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._lstForma.DataContext = value
        '    End Set
        'End Property

        Public Property MensajeErrorCobro As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.MensajeErrorCobro
            Get
                Return Me._txtMensajeError.Text
            End Get
            Set(ByVal value As String)
                Me._txtMensajeError.Text = value
            End Set

        End Property

        'Public Property SumaBforma As Double Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.SumaBforma
        '    Get
        '        Return Me._txtSumaBForma.Text
        '    End Get
        '    Set(ByVal value As Double)
        '        Me._txtSumaBForma.Text = value
        '    End Set
        'End Property

        'Public Property SumaBformaBf As Double Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.SumaBformaBf
        '    Get
        '        Return Me._txtSumaBFormaBf.Text
        '    End Get
        '    Set(ByVal value As Double)
        '        Me._txtSumaBFormaBf.Text = value
        '    End Set
        'End Property

        'Public Property SumaBono As Double Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.SumaBono
        '    Get
        '        Return Me._txtSumaBono.Text
        '    End Get
        '    Set(ByVal value As Double)
        '        Me._txtSumaBono.Text = value
        '    End Set
        'End Property

        'Public Property SumaBonoBf As Double Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.SumaBonoBf
        '    Get
        '        Return Me._txtSumaBonoBf.Text
        '    End Get
        '    Set(ByVal value As Double)
        '        Me._txtSumaBonoBf.Text = value
        '    End Set
        'End Property

        'Public Property Bforma As Double Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Bforma
        '    Get
        '        Return Me._txtBForma.Text
        '    End Get
        '    Set(ByVal value As Double)
        '        Me._txtBForma.Text = value
        '    End Set
        'End Property

        'Public Property BformaBf As Double Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.BformaBf
        '    Get
        '        Return Me._txtBFormaBf.Text
        '    End Get
        '    Set(ByVal value As Double)
        '        Me._txtBFormaBf.Text = value
        '    End Set
        'End Property

        'Public Property Credito As Integer Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Credito
        '    Get
        '        Return Me._txtCredito.Text
        '    End Get
        '    Set(ByVal value As Integer)
        '        Me._txtCredito.Text = value
        '    End Set
        'End Property

        'Public Property Xforma As String Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Xforma
        '    Get
        '        Return Me._txtXForma.Text
        '    End Get
        '    Set(ByVal value As String)
        '        Me._txtXForma.Text = value
        '    End Set
        'End Property

        'Public Property Banco As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Banco
        '    Get
        '        Return Me._cbxBanco.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxBanco.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Bancos As Object Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.Bancos
        '    Get
        '        Return Me._cbxBanco.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxBanco.DataContext = value
        '    End Set
        'End Property

        Public WriteOnly Property ActivaDesactiva As Boolean Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.ActivaDesactiva
            Set(ByVal value As Boolean)                                
                _txtDetalle2.IsReadOnly = value
                _txtSecuencia2.IsReadOnly = value
                If value = True Then
                    _cbxMotivo2.IsEnabled = False
                Else
                    _cbxMotivo2.IsEnabled = True
                End If
            End Set
        End Property

        Public ReadOnly Property BDesg As Boolean Implements Contratos.FacFacturaAnuladas.IAgregarFacFacturaAnulada.BDesg
            Get
                Return _chkDesg.IsChecked
            End Get
        End Property
    End Class
End Namespace
