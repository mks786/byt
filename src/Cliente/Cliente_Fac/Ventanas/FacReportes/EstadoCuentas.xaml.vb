Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacReportes
    Partial Public Class EstadoCuentas
        Inherits Page
        Implements IEstadoCuentas

        Private _presentador As PresentadorEstadoCuentas
        Private _cargada As Boolean

#Region "IEstadoCuentas"

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

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.FacReportes.IEstadoCuentas.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub
#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorEstadoCuentas(Me)
        End Sub

        Private Sub _btnImprimir_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Reporte()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
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

        Public Property Fecha1 As String Implements Contratos.FacReportes.IEstadoCuentas.Fecha1
            Get
                Return _dpkFecha1.Text
            End Get
            Set(ByVal value As String)
                _dpkFecha1.Text = value
            End Set
        End Property

        Public Property Fecha2 As String Implements Contratos.FacReportes.IEstadoCuentas.Fecha2
            Get
                Return _dpkFecha2.Text
            End Get
            Set(ByVal value As String)
                _dpkFecha2.Text = value
            End Set
        End Property

        Public ReadOnly Property TipoFactura As Object Implements Contratos.FacReportes.IEstadoCuentas.TipoFactura
            Get
                Return _cbxTipoMoneda.Text
            End Get
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

        Public Property Asociado As Object Implements Contratos.FacReportes.IEstadoCuentas.Asociado

            Get
                Return Me._lstAsociados.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados.SelectedItem = value
                Me._lstAsociados.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados As Object Implements Contratos.FacReportes.IEstadoCuentas.Asociados
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

        Public Property NombreAsociado() As String Implements Contratos.FacReportes.IEstadoCuentas.NombreAsociado
            Get
                Return Me._txtAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar() As String Implements Contratos.FacReportes.IEstadoCuentas.idAsociadoFiltrar
            Get
                Return Me._txtIdAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar() As String Implements Contratos.FacReportes.IEstadoCuentas.NombreAsociadoFiltrar
            Get
                Return Me._txtNombreAsociado.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado.Text = value
            End Set
        End Property

        Public ReadOnly Property TipoMoneda As Object Implements Contratos.FacReportes.IEstadoCuentas.TipoMoneda
            Get
                Return _cbxTipoMoneda.Text
            End Get
        End Property

        Public Property Asociado2 As Object Implements Contratos.FacReportes.IEstadoCuentas.Asociado2

            Get
                Return Me._lstAsociados2.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._lstAsociados2.SelectedItem = value
                Me._lstAsociados2.ScrollIntoView(value)
            End Set
        End Property

        Public Property Asociados2 As Object Implements Contratos.FacReportes.IEstadoCuentas.Asociados2
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

        Public Property NombreAsociado2() As String Implements Contratos.FacReportes.IEstadoCuentas.NombreAsociado2
            Get
                Return Me._txtAsociado2.Text
            End Get
            Set(ByVal value As String)
                Me._txtAsociado2.Text = value
            End Set
        End Property

        Public Property idAsociadoFiltrar2() As String Implements Contratos.FacReportes.IEstadoCuentas.idAsociadoFiltrar2
            Get
                Return Me._txtIdAsociado2.Text
            End Get
            Set(ByVal value As String)
                Me._txtIdAsociado2.Text = value
            End Set
        End Property

        Public Property NombreAsociadoFiltrar2() As String Implements Contratos.FacReportes.IEstadoCuentas.NombreAsociadoFiltrar2
            Get
                Return Me._txtNombreAsociado2.Text
            End Get
            Set(ByVal value As String)
                Me._txtNombreAsociado2.Text = value
            End Set
        End Property

        Public Property Pais As Object Implements Contratos.FacReportes.IEstadoCuentas.Pais
            Get
                Return Me._cbxPais.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxPais.SelectedItem = value
            End Set
        End Property

        Public Property Paises As Object Implements Contratos.FacReportes.IEstadoCuentas.Paises
            Get
                Return Me._cbxPais.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxPais.DataContext = value
            End Set
        End Property

    End Class

End Namespace
