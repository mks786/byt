Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacReportes
    Partial Public Class FacturasDigitales
        Inherits Page
        Implements IFacturasDigitales

        Private _presentador As PresentadorFacturasDigitales
        Private _cargada As Boolean

#Region "IFacturasDigitales"

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorFacturasDigitales(Me)
        End Sub

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

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.FacReportes.IFacturasDigitales.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub
#End Region

        'Public Sub New(ByVal FacFactura As Object)
        '    InitializeComponent()
        '    Me._cargada = False
        '    Me._presentador = New PresentadorFacturasDigitales()
        'End Sub

        Private Sub _btnImprimir_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Reporte()
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

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Public ReadOnly Property Fecha1 As Date? Implements Contratos.FacReportes.IFacturasDigitales.Fecha1
            Get
                Return _dpkFecha1.Text
            End Get
        End Property

        Public ReadOnly Property Fecha2 As Date? Implements Contratos.FacReportes.IFacturasDigitales.Fecha2
            Get
                Return _dpkFecha2.Text
            End Get
        End Property

        Public ReadOnly Property TipoFactura As String Implements Contratos.FacReportes.IFacturasDigitales.TipoFactura
            Get
                Return _cbxTipoFactura.Text
            End Get
        End Property

        Public ReadOnly Property TipoMoneda As String Implements Contratos.FacReportes.IFacturasDigitales.TipoMoneda
            Get                
                Return _cbxTipoMoneda.Text
            End Get
        End Property

        Public ReadOnly Property MayorMenor As Object Implements Contratos.FacReportes.IFacturasDigitales.MayorMenor
            Get
                If RbMayor.IsChecked = True Then
                    Return "Ma"
                Else
                    If RbMayor.IsChecked = True Then
                        Return "ME"
                    Else
                        Return ""
                    End If
                End If
            End Get
        End Property
    End Class
End Namespace
