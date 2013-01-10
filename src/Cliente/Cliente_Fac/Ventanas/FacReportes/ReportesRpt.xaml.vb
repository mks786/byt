Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacReportes
    Partial Public Class ReportesRpt
        Inherits Page
        Implements IReportesRpt


        Private _presentador As PresentadorReportesRpt
        Private _cargada As Boolean

#Region "IReportesRpt"

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


        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.FacReportes.IReportesRpt.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub
#End Region

        Public Sub New(ByVal FacReportes As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorReportesRpt(Me, FacReportes)
        End Sub

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
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

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

        Public WriteOnly Property CrystalViewer() As Object Implements Contratos.FacReportes.IReportesRpt.CrystalViewer
            Set(ByVal value As Object)
                'Me._crystalReportsViewer1.
                Me._crystalReportsViewer1.ViewerCore.ReportSource = value
            End Set
        End Property
    End Class
End Namespace
