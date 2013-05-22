Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.ChequeRecidos
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.ChequeRecidos
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.ChequeRecidos
    Partial Public Class ConsultarChequeRecido
        Inherits Page
        Implements IConsultarChequeRecido

        Private _presentador As PresentadorConsultarChequeRecido
        Private _cargada As Boolean

#Region "IConsultarChequeRecido"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            'Me._lstId.Focus()
        End Sub

        Public Property ChequeRecido() As Object Implements Contratos.ChequeRecidos.IConsultarChequeRecido.ChequeRecido
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        'Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.ChequeRecidos.IConsultarChequeRecido.Mensaje
        '    MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        'End Sub


#End Region

        Public Sub New(ByVal cheque As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarChequeRecido(Me, cheque)
        End Sub

        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarConceptoGestion, "Eliminar ConceptoGestion", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
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

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

        Public Property BancoG As Object Implements Contratos.ChequeRecidos.IConsultarChequeRecido.BancoG
            Get
                Return Me._cbxBancoG.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxBancoG.SelectedItem = value
            End Set
        End Property

        Public Property BancoGs As Object Implements Contratos.ChequeRecidos.IConsultarChequeRecido.BancoGs
            Get
                Return Me._cbxBancoG.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxBancoG.DataContext = value
            End Set
        End Property

        'Contratos.ChequeRecidos.IConsultarChequeRecido

        Public Property Asociado As Object Implements Contratos.ChequeRecidos.IConsultarChequeRecido.Asociado

            Get
                Return Me._lstAsociados.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.SelectedItem = value
                Me._lstAsociados.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.ChequeRecidos.IConsultarChequeRecido.Asociados
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

        Public Property NombreAsociado() As String Implements Contratos.ChequeRecidos.IConsultarChequeRecido.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.ChequeRecidos.IConsultarChequeRecido.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.ChequeRecidos.IConsultarChequeRecido.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property

        Public WriteOnly Property HabilitarCampos As Boolean Implements Contratos.ChequeRecidos.IConsultarChequeRecido.HabilitarCampos
            Set(ByVal value As Boolean)
                Me._cbxBancoG.IsEnabled = value
                Me._dpkFecha.IsEnabled = value
                Dim valor As Boolean
                If value = True Then
                    valor = False
                Else
                    valor = True
                End If
                _txtValor.IsReadOnly = valor
                _txtNCheque.IsReadOnly = valor
            End Set
        End Property

        Public Property TextoBotonModificar As String Implements Contratos.ChequeRecidos.IConsultarChequeRecido.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

        Public Property Monto As Double Implements Contratos.ChequeRecidos.IConsultarChequeRecido.Monto
            Get
                Return _presentador.GetFormatoDouble2(Me._txtValor.Text)
            End Get
            Set(ByVal value As Double)
                Dim a As String = _presentador.SetFormatoDouble2(value)
                Me._txtValor.Text = _presentador.SetFormatoDouble2(value)
            End Set
        End Property

        Public Sub _btnNuevoCheque_Click()
            Me._presentador.NuevaProforma()
        End Sub

    End Class

End Namespace
