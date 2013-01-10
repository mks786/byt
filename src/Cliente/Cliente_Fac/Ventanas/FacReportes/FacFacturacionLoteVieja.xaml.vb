Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.ControlesByT
Namespace Ventanas.FacReportes
    Partial Public Class FacFacturacionLoteVieja
        Inherits Page
        Implements IFacFacturacionLoteVieja

        Private _presentador As PresentadorFacFacturacionLoteVieja
        Private _cargada As Boolean

#Region "IFacFacturacionLoteVieja"

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

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.FacReportes.IFacFacturacionLoteVieja.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub
#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorFacFacturacionLoteVieja(Me)
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
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

        Public Property Desde As String Implements Contratos.FacReportes.IFacFacturacionLoteVieja.desde
            Get
                Return _txtDesde.Text
            End Get
            Set(ByVal value As String)
                _txtDesde.Text = value
            End Set
        End Property

        Public Property Hasta As String Implements Contratos.FacReportes.IFacFacturacionLoteVieja.hasta
            Get
                Return _txtHasta.Text
            End Get
            Set(ByVal value As String)
                _txtHasta.Text = value
            End Set
        End Property

        Public ReadOnly Property Tipo As Integer Implements Contratos.FacReportes.IFacFacturacionLoteVieja.Tipo
            Get
                Dim valor As Integer
                If _cbxTipo.Text = "F " Then
                    valor = 1
                Else
                    If _cbxTipo.Text = "S-F (Statement)" Then
                        valor = 21
                    Else
                        If _cbxTipo.Text = "S-F(Factura)" Then
                            valor = 22
                        Else
                            If _cbxTipo.Text = "S " Then
                                valor = 3
                            End If
                        End If
                    End If
                End If
                Return valor
            End Get
        End Property

        Public Sub NombreCampo_Click()

            If _cbxTipo.Text = "F " Then
                _lblNombre.Text = "Facturas"
                _lblFechaInicio.Text = "Seniat Desde"
                _lblFechaFin.Text = "Seniat Hasta"
            Else
                If _cbxTipo.Text = "S-F (Statement)" Then
                    _lblNombre.Text = "Statement"
                    _lblFechaInicio.Text = "Factura Desde"
                    _lblFechaFin.Text = "Factura Hasta"
                Else
                    If _cbxTipo.Text = "S-F(Factura)" Then
                        _lblNombre.Text = "Facturas"
                        _lblFechaInicio.Text = "Seniat Desde"
                        _lblFechaFin.Text = "Seniat Hasta"
                    Else
                        If _cbxTipo.Text = "S " Then
                            _lblNombre.Text = "Statement"
                            _lblFechaInicio.Text = "Factura Desde"
                            _lblFechaFin.Text = "Factura Hasta"
                        End If
                    End If
                End If
            End If
        End Sub



    End Class
End Namespace
