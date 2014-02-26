Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.TarifaServicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.TarifaServicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda

Namespace Ventanas.TarifaServicios
    Public Class RecalcularTarifaServicios
        Inherits Window
        Implements IRecalcularTarifaServicios

        Private _presentador As PresentadorRecalcularTarifaServicios
        Private _cargada As Boolean

        ''' CONSTRUCTOR PREDETERMINADO
        Public Sub New(ByVal listaTarifaServicios As Object)

            ' Llamada necesaria para el diseñador.
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorRecalcularTarifaServicios(Me, listaTarifaServicios)
            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        End Sub



        Public Property EstaCargada As Boolean Implements Contratos.IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements Contratos.IPaginaBaseFac.FocoPredeterminado
            Me._txtTasaDolar.Focus()
        End Sub

        Public Property TasaCambio() As String Implements Contratos.TarifaServicios.IRecalcularTarifaServicios.TasaCambio

            Get
                Return Me._txtTasaDolar.Text
            End Get
            Set(value As String)
                Me._txtTasaDolar.Text = value
            End Set
        End Property

        Private Sub _btnCalcularTarifas_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
            Mouse.OverrideCursor = Cursors.Wait
            Me._presentador.RecalcularTarifas()
            Mouse.OverrideCursor = Nothing
            Me.Close()
        End Sub

        Private Sub _btnCancelar_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
            Me.Close()
        End Sub

        Public Sub Mensaje(ByVal mensaje__1 As String, ByVal tipoMensaje As Integer) Implements Contratos.TarifaServicios.IRecalcularTarifaServicios.Mensaje

            If (tipoMensaje = 0) Then
                MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
            ElseIf (tipoMensaje = 1) Then
                MessageBox.Show(mensaje__1, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning)
            ElseIf (tipoMensaje = 2) Then
                MessageBox.Show(mensaje__1, "Información", MessageBoxButton.OK, MessageBoxImage.Information)
            End If

        End Sub

    End Class
End Namespace
