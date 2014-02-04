Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacInternacionales
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacInternacionales
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacInternacionales
    Partial Public Class FacInternacionalPago
        Inherits Page
        Implements IFacInternacionalPago

        Private _presentador As PresentadorFacInternacionalPago
        Private _cargada As Boolean

#Region "IFacInternacionalPago"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado

        End Sub

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.FacInternacionales.IFacInternacionalPago.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub

        Public Property BISel() As Boolean Implements Contratos.FacInternacionales.IFacInternacionalPago.BISel
            Get
                Return Me._chkBISel.IsChecked
            End Get
            Set(ByVal value As Boolean)
                Me._chkBISel.IsChecked = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal proforma As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorFacInternacionalPago(Me, proforma)
        End Sub

        Private Sub _btnImprimir_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ' Me._presentador.Reporte()
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            End If
            PintarAsociado()
            PintarAsociado2()
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
        End Sub

        Private Sub _btnAceptar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Aceptar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Public Property Fecha As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.Fecha
            Get
                Return _dpkFecha.Text
            End Get
            Set(ByVal value As Object)
                _dpkFecha.Text = value
            End Set
        End Property

        Public Property FechaPago As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.FechaPago
            Get
                Return _dpkFechaPago.Text
            End Get
            Set(ByVal value As Object)
                _dpkFechaPago.Text = value
            End Set
        End Property

        Private Sub _btnConsultar(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim nom As String = DirectCast(sender, Button).Name.ToString
            If nom = "_btnConsultarAsociado" Then
                Me._presentador.BuscarAsociado2()
            ElseIf nom = "_btnConsultarAsociado2" Then
                Me._presentador.BuscarAsociadofin()
            End If
        End Sub

        Private Sub _Consultar_Enter(ByVal sender As Object, ByVal e As KeyEventArgs)
            If (e.Key = Key.Enter) Then
                Dim nom As String = DirectCast(sender, ByTTextBox).Name.ToString
                If nom = "_txtIdAsociado" Or nom = "_txtNombreAsociado" Then
                    Me._presentador.BuscarAsociado2()
                End If
                If nom = "_txtIdAsociado2" Or nom = "_txtNombreAsociado2" Then
                    Me._presentador.BuscarAsociadofin()
                End If
            End If
        End Sub

        Public Property Asociado As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.Asociado

            Get
                Return Me._lstAsociados.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.SelectedItem = value
                Me._lstAsociados.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.Asociados
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

        Public Property NombreAsociado() As String Implements Contratos.FacInternacionales.IFacInternacionalPago.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.FacInternacionales.IFacInternacionalPago.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.FacInternacionales.IFacInternacionalPago.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property

        'Public ReadOnly Property TipoSel As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.TipoSel
        '    Get
        '        Return _cbxTipoSel.Text
        '    End Get
        'End Property


        Public Property Pais As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.Pais
            Get
                Return Me._CbxPais.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._CbxPais.SelectedItem = value
            End Set
        End Property

        Public Property Paises As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.Paises
            Get
                Return Me._CbxPais.DataContext
            End Get
            Set(ByVal value As Object)
                Me._CbxPais.DataContext = value
            End Set
        End Property

        Public Property Asociado2 As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.Asociado2

            Get
                Return Me._lstAsociados2.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados2.SelectedItem = value
                Me._lstAsociados2.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados2 As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.Asociados2
            Get
                Return Me._lstAsociados2.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados2.DataContext = value
            End Set
        End Property

        Private Sub _txtAsociado2_MouseDoubleClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ControlesMostrarAsociado2()
        End Sub

        Private Sub ControlesMostrarAsociado2()
            Me._txtAsociado2.Visibility = System.Windows.Visibility.Collapsed
            Me._lblasociado2_2.Visibility = System.Windows.Visibility.Collapsed
            'Me._txtAsociadoId.Visibility = System.Windows.Visibility.Collapsed
            Me._lstAsociados2.Visibility = System.Windows.Visibility.Visible
            Me._lstAsociados2.IsEnabled = True
            Me._btnConsultarAsociado2.Visibility = System.Windows.Visibility.Visible
            Me._txtIdAsociado2.Visibility = System.Windows.Visibility.Visible
            Me._txtNombreAsociado2.Visibility = System.Windows.Visibility.Visible
            Me._lblIdAsociado2.Visibility = System.Windows.Visibility.Visible
            Me._lblNombreAsociado2.Visibility = System.Windows.Visibility.Visible
            Me._lblasociado2.Visibility = System.Windows.Visibility.Visible
        End Sub

        Private Sub _lstAsociados2_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Me._presentador.CambiarAsociadofin()
            ControlesOcultarAsociado2()
        End Sub

        Private Sub ControlesOcultarAsociado2()
            Me._lstAsociados2.Visibility = System.Windows.Visibility.Collapsed
            Me._btnConsultarAsociado2.Visibility = System.Windows.Visibility.Collapsed
            Me._txtIdAsociado2.Visibility = System.Windows.Visibility.Collapsed
            Me._txtNombreAsociado2.Visibility = System.Windows.Visibility.Collapsed
            Me._lblasociado2_1.Visibility = System.Windows.Visibility.Collapsed
            Me._txtAsociado2.Visibility = System.Windows.Visibility.Visible
            Me._lblasociado2_2.Visibility = System.Windows.Visibility.Visible
            'Me._txtAsociadoId.Visibility = System.Windows.Visibility.Visible
            Me._lblIdAsociado2.Visibility = System.Windows.Visibility.Collapsed
            Me._lblNombreAsociado2.Visibility = System.Windows.Visibility.Collapsed
        End Sub

        Public Sub PintarAsociado2()
            Me._txtAsociado2.BorderBrush = New SolidColorBrush(Colors.LightGreen)
        End Sub

        Public Property NombreAsociado2() As String Implements Contratos.FacInternacionales.IFacInternacionalPago.NombreAsociado2
            Get
                Return Me._txtAsociado2.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado2.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar2() As String Implements Contratos.FacInternacionales.IFacInternacionalPago.idAsociadoFiltrar2
            Get
                Return Me._txtIdAsociado2.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado2.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar2() As String Implements Contratos.FacInternacionales.IFacInternacionalPago.NombreAsociadoFiltrar2
            Get
                Return Me._txtNombreAsociado2.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado2.Text = value
            End Set
        End Property

        Public Property FacInternacional As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.FacInternacional
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public ReadOnly Property GetTipoPago As Char Implements Contratos.FacInternacionales.IFacInternacionalPago.GetTipoPago
            Get
                If Not String.IsNullOrEmpty(Me._cbxTipoPago.Text) Then
                    Return (Me._cbxTipoPago.Text)(0)
                Else
                    Return " "c
                End If
            End Get
        End Property

        Public WriteOnly Property SetTipoPago() As String Implements Contratos.FacInternacionales.IFacInternacionalPago.SetTipoPago
            Set(ByVal value As String)
                Me._cbxTipoPago.Text = value
            End Set
        End Property

        Public Property Banco As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.Banco
            Get
                Return Me._cbxBanco.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxBanco.SelectedItem = value
            End Set
        End Property

        Public Property Bancos As Object Implements Contratos.FacInternacionales.IFacInternacionalPago.Bancos
            Get
                Return Me._cbxBanco.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxBanco.DataContext = value
            End Set
        End Property
    End Class
End Namespace
