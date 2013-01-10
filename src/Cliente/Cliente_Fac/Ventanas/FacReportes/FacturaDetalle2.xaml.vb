Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacReportes
    Partial Public Class FacturaDetalle
        Inherits Page
        Implements IFacturaDetalle

        Private _presentador As PresentadorFacturaDetalle
        Private _cargada As Boolean

#Region "IFacturaDetalle"

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

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.FacReportes.IFacturaDetalle.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub
#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorFacturaDetalle(Me)
        End Sub

        Private Sub _btnImprimir_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Imprimir()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True

            End If
        End Sub

        Public ReadOnly Property Fecha1 As Object Implements Contratos.FacReportes.IFacturaDetalle.Fecha1
            Get
                Return _dpkFecha1.Text
            End Get
        End Property

        Public ReadOnly Property Fecha2 As Object Implements Contratos.FacReportes.IFacturaDetalle.Fecha2
            Get
                Return _dpkFecha2.Text
            End Get
        End Property
    End Class
End Namespace
