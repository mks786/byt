Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacGestiones
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacGestiones
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacGestiones
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarFacGestion
        Inherits Page
        Implements IConsultarFacGestion


        Private _presentador As PresentadorConsultarFacGestion
        Private _cargada As Boolean

#Region "IConsultarFacGestion"

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


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.FacGestiones.IConsultarFacGestion.HabilitarCampos
            Set(ByVal value As Boolean)
                Dim valor As Integer = _txtpermisos.Text
                If valor = 1 Then
                    Me._cbxConceptoGestion.IsEnabled = value
                    Me._cbxConceptoGestionRespuesta.IsEnabled = value
                    Me._cbxMedioGestion.IsEnabled = value
                    If value = False Then
                        Me._txtObservacion.IsReadOnly = True
                        Me._txtRuta.IsReadOnly = True
                    Else
                        Me._txtObservacion.IsReadOnly = False
                        Me._txtRuta.IsReadOnly = False
                    End If
                    ' Me._txtId.IsReadOnly = value
                    _dpkFechaGestion.IsEnabled = value

                Else                    
                End If
                '_btnModificar.IsEnabled = value
                '_btnEliminar.IsEnabled = value
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.FacGestiones.IConsultarFacGestion.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal Facgestion As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarFacGestion(Me, Facgestion)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarFacGestion, "Eliminar Gestion", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                Me._presentador.Eliminar()
            End If
        End Sub

        Private Sub _btnVerGestion_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Ver_Carta_Gestion()
        End Sub

        Private Sub _btnVerRespuesta_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Ver_Carta_Respuesta()
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            End If
            PintarAsociado()
            PintarCarta()
            PintarCarta_2()
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

        Public Property Medio As Object Implements Contratos.FacGestiones.IConsultarFacGestion.Medio
            Get
                Return Me._cbxMedioGestion.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxMedioGestion.SelectedItem = value
            End Set
        End Property

        Public Property Medios As Object Implements Contratos.FacGestiones.IConsultarFacGestion.Medios
            Get
                Return Me._cbxMedioGestion.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxMedioGestion.DataContext = value
            End Set
        End Property

        Public Property Concepto As Object Implements Contratos.FacGestiones.IConsultarFacGestion.Concepto
            Get
                Return Me._cbxConceptoGestion.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxConceptoGestion.SelectedItem = value
            End Set
        End Property

        Public Property Conceptos As Object Implements Contratos.FacGestiones.IConsultarFacGestion.Conceptos
            Get
                Return Me._cbxConceptoGestion.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxConceptoGestion.DataContext = value
            End Set
        End Property

        Public Property ConceptoRespuesta As Object Implements Contratos.FacGestiones.IConsultarFacGestion.ConceptoRespuesta
            Get
                Return Me._cbxConceptoGestionRespuesta.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxConceptoGestionRespuesta.SelectedItem = value
            End Set
        End Property

        Public Property ConceptoRespuestas As Object Implements Contratos.FacGestiones.IConsultarFacGestion.ConceptoRespuestas
            Get
                Return Me._cbxConceptoGestionRespuesta.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxConceptoGestionRespuesta.DataContext = value
            End Set
        End Property

        Public Property TipoCliente As Object Implements Contratos.FacGestiones.IConsultarFacGestion.TipoCliente
            Get
                Return Me._cbxTipoAsociado.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxTipoAsociado.SelectedItem = value
            End Set
        End Property

        Public Property TipoClientes As Object Implements Contratos.FacGestiones.IConsultarFacGestion.TipoClientes
            Get
                Return Me._cbxTipoAsociado.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxTipoAsociado.DataContext = value
            End Set
        End Property

        Public Property Asociado As Object Implements Contratos.FacGestiones.IConsultarFacGestion.Asociado

            Get
                Return Me._lstAsociados.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.SelectedItem = value
                Me._lstAsociados.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.FacGestiones.IConsultarFacGestion.Asociados
            Get
                Return Me._lstAsociados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.DataContext = value
            End Set
        End Property

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
                ElseIf nom = "_txtIdCarta_2" Or nom = "_txtNombreCarta_2" Or nom = "_txtFechaCarta_2" Then
                    Me._presentador.BuscarCarta_2()

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
            ControlesOcultarCarta()
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

        Public Property NombreAsociado() As String Implements Contratos.FacGestiones.IConsultarFacGestion.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.FacGestiones.IConsultarFacGestion.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.FacGestiones.IConsultarFacGestion.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property

        Public Property Carta As Object Implements Contratos.FacGestiones.IConsultarFacGestion.Carta

            Get
                Return Me._lstCartas.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstCartas.SelectedItem = value
                Me._lstCartas.ScrollIntoView(value)
            End Set
        End Property

        Public Property Cartas As Object Implements Contratos.FacGestiones.IConsultarFacGestion.Cartas
            Get
                Return Me._lstCartas.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstCartas.DataContext = value
            End Set
        End Property

        Private Sub _btnConsultarCarta_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.BuscarCarta()
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

        Public Property NombreCarta() As String Implements Contratos.FacGestiones.IConsultarFacGestion.NombreCarta
            Get
                Return Me._txtCarta.Text
            End Get
            Set(ByVal value As String)
                Me._txtCarta.Text = value
            End Set
        End Property

        Public Property idCartaFiltrar() As String Implements Contratos.FacGestiones.IConsultarFacGestion.idCartaFiltrar
            Get
                Return Me._txtIdCarta.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdCarta.Text = value
            End Set
        End Property

        Public Property NombreCartaFiltrar() As String Implements Contratos.FacGestiones.IConsultarFacGestion.NombreCartaFiltrar
            Get
                Return Me._txtNombreCarta.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreCarta.Text = value
            End Set
        End Property

        Public Property FechaCartaFiltrar() As String Implements Contratos.FacGestiones.IConsultarFacGestion.FechaCartaFiltrar
            Get
                Return Me._dpkFechaCarta.Text
            End Get
            Set(ByVal value As String)
                Me._dpkFechaCarta.Text = value
            End Set
        End Property

        Public Property MensajeErrorFacGestion As String Implements Contratos.FacGestiones.IConsultarFacGestion.MensajeErrorFacGestion
            Get
                Return ""
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Public Property Carta_2 As Object Implements Contratos.FacGestiones.IConsultarFacGestion.Carta_2

            Get
                Return Me._lstCartas_2.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstCartas_2.SelectedItem = value
                Me._lstCartas_2.ScrollIntoView(value)
            End Set
        End Property

        Public Property Cartas_2 As Object Implements Contratos.FacGestiones.IConsultarFacGestion.Cartas_2
            Get
                Return Me._lstCartas_2.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstCartas_2.DataContext = value
            End Set
        End Property

        'Private Sub _btnConsultar(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '    Dim nom As String = DirectCast(sender, Button).Name.ToString
        '    If nom = "_btnConsultarCarta_2" Then
        '        Me._presentador.BuscarCarta_22()
        '    ElseIf nom = "_btnCancelar" Then
        '        Me._presentador.Cancelar()
        '    ElseIf nom = "_btnConsulta" Then
        '        Me._presentador.Consultar()
        '    End If
        'End Sub

        Private Sub _btnConsultarCarta_2_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.BuscarCarta_2()
        End Sub

        Private Sub _txtCarta_2_MouseDoubleClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ControlesMostrarCarta_2()
        End Sub

        Private Sub ControlesMostrarCarta_2()
            Me._txtCarta_2.Visibility = System.Windows.Visibility.Collapsed
            Me._btnVerRespuesta.Visibility = System.Windows.Visibility.Collapsed
            Me._lstCartas_2.Visibility = System.Windows.Visibility.Visible
            Me._lstCartas_2.IsEnabled = True
            Me._btnConsultarCarta_2.Visibility = System.Windows.Visibility.Visible
            Me._txtIdCarta_2.Visibility = System.Windows.Visibility.Visible
            Me._txtNombreCarta_2.Visibility = System.Windows.Visibility.Visible
            Me._lblIdCarta_2.Visibility = System.Windows.Visibility.Visible
            Me._dpkFechaCarta_2.Visibility = System.Windows.Visibility.Visible
            Me._lblCarta_22.Visibility = System.Windows.Visibility.Collapsed
            Me._lblCarta_2.Visibility = System.Windows.Visibility.Visible
            Me._lblFechaCarta_2.Visibility = System.Windows.Visibility.Visible
            Me._lblNombreCarta_2.Visibility = System.Windows.Visibility.Visible
            ControlesOcultarAsociado()
        End Sub

        Private Sub _lstCartas_2_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Me._presentador.CambiarCarta_2()
            ControlesOcultarCarta_2()
        End Sub

        Private Sub ControlesOcultarCarta_2()
            Me._lstCartas_2.Visibility = System.Windows.Visibility.Collapsed
            Me._btnConsultarCarta_2.Visibility = System.Windows.Visibility.Collapsed
            Me._txtIdCarta_2.Visibility = System.Windows.Visibility.Collapsed
            Me._txtNombreCarta_2.Visibility = System.Windows.Visibility.Collapsed
            Me._txtCarta_2.Visibility = System.Windows.Visibility.Visible
            Me._btnVerRespuesta.Visibility = System.Windows.Visibility.Visible
            Me._lblIdCarta_2.Visibility = System.Windows.Visibility.Collapsed
            Me._dpkFechaCarta_2.Visibility = System.Windows.Visibility.Collapsed
            Me._lblCarta_22.Visibility = System.Windows.Visibility.Visible
            Me._lblCarta_2.Visibility = System.Windows.Visibility.Collapsed
            Me._lblFechaCarta_2.Visibility = System.Windows.Visibility.Collapsed
            Me._lblNombreCarta_2.Visibility = System.Windows.Visibility.Collapsed
        End Sub

        Public Sub PintarCarta_2()
            Me._txtCarta_2.BorderBrush = New SolidColorBrush(Colors.LightGreen)
        End Sub

        Public Property NombreCarta_2() As String Implements Contratos.FacGestiones.IConsultarFacGestion.NombreCarta_2
            Get
                Return Me._txtCarta_2.Text
            End Get
            Set(ByVal value As String)
                Me._txtCarta_2.Text = value
            End Set
        End Property

        Public Property idCarta_2Filtrar() As String Implements Contratos.FacGestiones.IConsultarFacGestion.idCarta_2Filtrar
            Get
                Return Me._txtIdCarta_2.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdCarta_2.Text = value
            End Set
        End Property

        Public Property NombreCarta_2Filtrar() As String Implements Contratos.FacGestiones.IConsultarFacGestion.NombreCarta_2Filtrar
            Get
                Return Me._txtNombreCarta_2.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreCarta_2.Text = value
            End Set
        End Property

        Public Property FechaCarta_2Filtrar() As String Implements Contratos.FacGestiones.IConsultarFacGestion.FechaCarta_2Filtrar
            Get
                Return Me._dpkFechaCarta_2.Text
            End Get
            Set(ByVal value As String)
                Me._dpkFechaCarta_2.Text = value
            End Set
        End Property

        Public Property Id As Integer? Implements Contratos.FacGestiones.IConsultarFacGestion.Id
            Get
                Return _txtId.Text
            End Get
            Set(ByVal value As Integer?)
                _txtId.Text = value
            End Set
        End Property

        Public Property FacGestion As Object Implements Contratos.FacGestiones.IConsultarFacGestion.FacGestion
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.FacGestiones.IConsultarFacGestion.Mensaje

        End Sub


        Public Property Permisos As Integer Implements Contratos.FacGestiones.IConsultarFacGestion.Permisos
            Get
                Dim valor As Integer = _txtpermisos.Text
                Return (valor)
            End Get
            Set(ByVal value As Integer)
                If value = 1 Then
                    _btnModificar.IsEnabled = True
                    _btnEliminar.IsEnabled = True
                Else
                    _btnModificar.IsEnabled = True
                    _btnEliminar.IsEnabled = False
                End If
                _txtpermisos.Text = value
            End Set
        End Property

        Private Sub _btnAuditoria_Click(sender As Object, e As RoutedEventArgs)
            Me._presentador.Auditoria()
        End Sub

        Private Sub _btnNuevaGestion_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
            Me._presentador.AgregarNuevaGestion()
        End Sub
    End Class
End Namespace