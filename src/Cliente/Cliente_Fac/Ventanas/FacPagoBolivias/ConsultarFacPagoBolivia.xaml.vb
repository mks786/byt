Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacPagoBolivias
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacPagoBolivias
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacPagoBolivias
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarFacPagoBolivia
        Inherits Page
        Implements IConsultarFacPagoBolivia

        Private _presentador As PresentadorConsultarFacPagoBolivia
        Private _cargada As Boolean

#Region "IConsultarFacPagoBolivia"

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

        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.HabilitarCampos
            Set(ByVal value As Boolean)
                _dpkFechaBanco.IsEnabled = value
                _cbxBancoRec.IsEnabled = value
                _cbxPagoRec.IsEnabled = value
                _cbxBancoPag.IsEnabled = value
                _cbxPagoPag.IsEnabled = value
                Dim valor As Boolean
                If value = True Then
                    valor = False
                Else
                    valor = True
                End If
                _txtMontoRec.IsReadOnly = valor
                _txtMontoBol.IsReadOnly = valor
                _txtDescripcionRec.IsReadOnly = valor
                'Me._txtDetalle.IsEnabled = value
                'Me._txtId.IsEnabled = value
            End Set
        End Property

        Public Property TextoBotonModificar() As String Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal FacPagoBolivia As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarFacPagoBolivia(Me, FacPagoBolivia)
        End Sub

        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarFacPagoBolivia, "Eliminar FacPagoBolivia", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                Me._presentador.Eliminar()
            End If
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            End If
            PintarAsociado()
            PintarCarta()
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

        Public Property FacPagoBolivia As Object Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.FacPagoBolivia
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Property BancoRec As Object Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.BancoRec
            Get
                Return Me._cbxBancoRec.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxBancoRec.SelectedItem = value
            End Set
        End Property

        Public Property BancosRec As Object Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.BancosRec
            Get
                Return Me._cbxBancoRec.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxBancoRec.DataContext = value
            End Set
        End Property

        Public ReadOnly Property TipoPago As Char Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.TipoPago
            Get
                If Not String.IsNullOrEmpty(Me._cbxPagoRec.Text) Then
                    Return (Me._cbxPagoRec.Text)(0)
                Else
                    Return " "c
                End If
            End Get
        End Property

        Private Sub _txtMontoRec_LostFocus(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles _txtMontoRec.LostFocus
            _txtMontoBol.Text = _txtMontoRec.Text
        End Sub
        'Contratos.FacPagoBolivias.IConsultarFacPagoBolivia

        Public Property Asociado As Object Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.Asociado

            Get
                Return Me._lstAsociados.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.SelectedItem = value
                Me._lstAsociados.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.Asociados
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
                ElseIf nom = "_txtIdCarta" Or nom = "_txtNombreCarta" Or nom = "_txtFechaCarta" Then
                    Me._presentador.BuscarCarta()
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

        Public Property NombreAsociado() As String Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property


        Public Property Carta As Object Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.Carta

            Get
                Return Me._lstCartas.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstCartas.SelectedItem = value
                Me._lstCartas.ScrollIntoView(value)
            End Set
        End Property

        Public Property Cartas As Object Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.Cartas
            Get
                Return Me._lstCartas.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstCartas.DataContext = value
            End Set
        End Property

        'Private Sub _btnConsultar(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '    Dim nom As String = DirectCast(sender, Button).Name.ToString
        '    If nom = "_btnConsultarCarta" Then
        '        Me._presentador.BuscarCarta2()
        '    ElseIf nom = "_btnCancelar" Then
        '        Me._presentador.Cancelar()
        '    ElseIf nom = "_btnConsulta" Then
        '        Me._presentador.Consultar()
        '    End If
        'End Sub

        Private Sub _btnConsultarCarta_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.BuscarCarta()
        End Sub

        Private Sub _btnVerCarta_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Ver_Carta()
        End Sub

        Private Sub _txtCarta_MouseDoubleClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ControlesMostrarCarta()
        End Sub

        Private Sub ControlesMostrarCarta()
            Me._txtCarta.Visibility = System.Windows.Visibility.Collapsed
            Me._btnVerGestion.Visibility = System.Windows.Visibility.Collapsed
            Me._lstCartas.Visibility = System.Windows.Visibility.Visible
            Me._lstCartas.IsEnabled = True
            Me._btnConsultarCarta.Visibility = System.Windows.Visibility.Visible
            Me._txtIdCarta.Visibility = System.Windows.Visibility.Visible
            Me._txtNombreCarta.Visibility = System.Windows.Visibility.Visible
            Me._lblIdCarta.Visibility = System.Windows.Visibility.Visible
            Me._dpkFechaCarta.Visibility = System.Windows.Visibility.Visible
            Me._lblCarta2.Visibility = System.Windows.Visibility.Collapsed
            Me._lblCarta.Visibility = System.Windows.Visibility.Visible
            Me._lblFechaCarta.Visibility = System.Windows.Visibility.Visible
            Me._lblNombreCarta.Visibility = System.Windows.Visibility.Visible
            ControlesOcultarAsociado()
        End Sub

        Private Sub _lstCartas_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Me._presentador.CambiarCarta()
            ControlesOcultarCarta()
        End Sub

        Private Sub ControlesOcultarCarta()
            Me._lstCartas.Visibility = System.Windows.Visibility.Collapsed
            Me._btnConsultarCarta.Visibility = System.Windows.Visibility.Collapsed
            Me._txtIdCarta.Visibility = System.Windows.Visibility.Collapsed
            Me._txtNombreCarta.Visibility = System.Windows.Visibility.Collapsed
            Me._txtCarta.Visibility = System.Windows.Visibility.Visible
            Me._btnVerGestion.Visibility = System.Windows.Visibility.Visible
            Me._lblIdCarta.Visibility = System.Windows.Visibility.Collapsed
            Me._dpkFechaCarta.Visibility = System.Windows.Visibility.Collapsed
            Me._lblCarta2.Visibility = System.Windows.Visibility.Visible
            Me._lblCarta.Visibility = System.Windows.Visibility.Collapsed
            Me._lblFechaCarta.Visibility = System.Windows.Visibility.Collapsed
            Me._lblNombreCarta.Visibility = System.Windows.Visibility.Collapsed
        End Sub

        Public Sub PintarCarta()
            Me._txtCarta.BorderBrush = New SolidColorBrush(Colors.LightGreen)
        End Sub

        Public Property NombreCarta() As String Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.NombreCarta
            Get
                Return Me._txtCarta.Text
            End Get
            Set(ByVal value As String)
                Me._txtCarta.Text = value
            End Set
        End Property

        Public Property idCartaFiltrar() As String Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.idCartaFiltrar
            Get
                Return Me._txtIdCarta.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdCarta.Text = value
            End Set
        End Property

        Public Property NombreCartaFiltrar() As String Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.NombreCartaFiltrar
            Get
                Return Me._txtNombreCarta.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreCarta.Text = value
            End Set
        End Property

        Public Property FechaCartaFiltrar() As String Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.FechaCartaFiltrar
            Get
                Return Me._dpkFechaCarta.Text
            End Get
            Set(ByVal value As String)
                Me._dpkFechaCarta.Text = value
            End Set
        End Property

        Public Property Banco As Object Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.Banco
            Get
                Return Me._cbxBancoPag.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxBancoPag.SelectedItem = value
            End Set
        End Property

        Public Property Bancos As Object Implements Contratos.FacPagoBolivias.IConsultarFacPagoBolivia.Bancos
            Get
                Return Me._cbxBancoPag.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxBancoPag.DataContext = value
            End Set
        End Property


        Public ReadOnly Property GetFormaPago() As Char Implements IConsultarFacPagoBolivia.GetFormaPago
            Get
                If Not String.IsNullOrEmpty(Me._cbxPagoPag.Text) Then
                    Return (Me._cbxPagoPag.Text)(0)
                Else
                    Return " "c
                End If
            End Get
        End Property

        Public WriteOnly Property SetFormaPago() As String Implements IConsultarFacPagoBolivia.SetFormaPago
            Set(ByVal value As String)
                Me._cbxPagoPag.Text = value
            End Set
        End Property
    End Class
End Namespace