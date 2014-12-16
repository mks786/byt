Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Diginsoft.Bolet.Cliente.Fac.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.ViGestionAsociados
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.ViGestionAsociados
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Imports Trascend.Bolet.ControlesByT
Imports System.Data

Namespace Ventanas.ViGestionAsociados
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ConsultarFacGestionesAsociado
        Inherits Page
        Implements IConsultarFacGestionesAsociado

        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorConsultarFacGestionesAsociado
        Private _cargada As Boolean

#Region "IConsultarFacGestionesAsociado"

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

        'Public Property FacGestionFiltrar() As Object Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.FacGestionFiltrar
        '    Get
        '        Return Me._splFiltro.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._splFiltro.DataContext = value
        '    End Set
        'End Property

        Public ReadOnly Property FacGestionseleccionado() As Object Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.FacGestionSeleccionado
            Get
                Return Me._lstResultados.SelectedItem
            End Get
        End Property


        Public Property Resultados() As Object Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Resultados
            Get
                Return Me._lstResultados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultados.DataContext = value
            End Set
        End Property

        'Public Property Id() As Integer Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Id
        '    Get
        '        Return Me._lstId.Text
        '    End Get
        '    Set(ByVal value As Integer)
        '        Me._lstId.Text = value
        '    End Set
        'End Property

        Public Property CurSortCol() As System.Windows.Controls.GridViewColumnHeader Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.CurSortCol
            Get
                Return _CurSortCol
            End Get
            Set(ByVal value As GridViewColumnHeader)
                _CurSortCol = value
            End Set
        End Property

        Public Property CurAdorner() As SortAdorner Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.CurAdorner
            Get
                Return _CurAdorner
            End Get
            Set(ByVal value As SortAdorner)
                _CurAdorner = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.ListaResultados
            Get
                Return Me._lstResultados
            End Get
            Set(ByVal value As ListView)
                Me._lstResultados = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal Asociado As Object)
            InitializeComponent()
            Me._cargada = False

            Me._presentador = New PresentadorConsultarFacGestionesAsociado(Me, Asociado)
        End Sub

        ' Constuctor predeterminado que carga una ventanaPadre
        Public Sub New(ByVal Asociado As Object, ByVal ventanaPadre As Object)
            InitializeComponent()
            Me._cargada = False

            Me._presentador = New PresentadorConsultarFacGestionesAsociado(Me, Asociado, ventanaPadre)
        End Sub


        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            Else
                Me._presentador.ActualizarTitulo()
            End If
            '   PintarAsociado()
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
            'Me._presentador.RegresarVentanaPadre()
        End Sub

        'Private Sub _btnConsultar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '    Me._btnConsultar.Focus()
        '    Me._presentador.Consultar()
        'End Sub

        Private Sub _lstResultados_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If Me._lstResultados.SelectedItem IsNot Nothing Then
                Me._presentador.IrConsultarFacGestion()
            End If
        End Sub

        Private Sub _Ordenar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.OrdenarColumna(TryCast(sender, GridViewColumnHeader))
        End Sub


        Private Sub _dpkFechaUltima_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            'If Not String.IsNullOrEmpty(Me._dpkFechaUltima.Text) Then
            '    Me._presentador.DeshabilitarDias(Me._dpkFechaBoletinVence, Me._dpkFechaBoletin.SelectedDate.Value.AddDays(-1))
            'End If
        End Sub

        'Public ReadOnly Property FechaGestion() As String Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.FechaGestion
        '    Get
        '        Return Me._dpkFechaGestion.SelectedDate.ToString()
        '    End Get
        'End Property

        'Public ReadOnly Property Id() As String Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Id
        '    Get
        '        Return Me._txtId.Text
        '    End Get
        'End Property

        'Public Property Asociado As Object Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Asociado

        '    Get
        '        Return Me._lstAsociados.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._lstAsociados.SelectedItem = value
        '        Me._lstAsociados.ScrollIntoView(value)
        '    End Set
        'End Property

        'Public Property Asociados As Object Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Asociados
        '    Get
        '        Return Me._lstAsociados.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._lstAsociados.DataContext = value
        '    End Set
        'End Property

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

        'Private Sub _btnConsultarAsociado_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '    Me._presentador.BuscarAsociado2()
        'End Sub
        'Private Sub _Consultar_Enter(ByVal sender As Object, ByVal e As KeyEventArgs)
        '    If (e.Key = Key.Enter) Then
        '        Dim nom As String = DirectCast(sender, ByTTextBox).Name.ToString
        '        If nom = "_txtIdAsociado" Or nom = "_txtNombreAsociado" Then
        '            Me._presentador.BuscarAsociado2()
        '        End If
        '    End If
        'End Sub

        'Private Sub _txtAsociado_MouseDoubleClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '    ControlesMostrarAsociado()
        'End Sub

        'Private Sub ControlesMostrarAsociado()
        '    Me._txtAsociado.Visibility = System.Windows.Visibility.Collapsed
        '    Me._lblasociado2.Visibility = System.Windows.Visibility.Collapsed
        '    'Me._txtAsociadoId.Visibility = System.Windows.Visibility.Collapsed
        '    Me._lstAsociados.Visibility = System.Windows.Visibility.Visible
        '    Me._lstAsociados.IsEnabled = True
        '    Me._btnConsultarAsociado.Visibility = System.Windows.Visibility.Visible
        '    Me._txtIdAsociado.Visibility = System.Windows.Visibility.Visible
        '    Me._txtNombreAsociado.Visibility = System.Windows.Visibility.Visible
        '    Me._lblIdAsociado.Visibility = System.Windows.Visibility.Visible
        '    Me._lblNombreAsociado.Visibility = System.Windows.Visibility.Visible
        '    Me._lblasociado.Visibility = System.Windows.Visibility.Visible
        'End Sub

        'Private Sub _lstAsociados_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        '    If Me._lstAsociados.SelectedItem IsNot Nothing Then
        '        Me._presentador.CambiarAsociado()
        '        ControlesOcultarAsociado()
        '    End If
        'End Sub

        'Private Sub ControlesOcultarAsociado()
        '    Me._lstAsociados.Visibility = System.Windows.Visibility.Collapsed
        '    Me._btnConsultarAsociado.Visibility = System.Windows.Visibility.Collapsed
        '    Me._txtIdAsociado.Visibility = System.Windows.Visibility.Collapsed
        '    Me._txtNombreAsociado.Visibility = System.Windows.Visibility.Collapsed
        '    Me._lblasociado.Visibility = System.Windows.Visibility.Collapsed
        '    Me._txtAsociado.Visibility = System.Windows.Visibility.Visible
        '    Me._lblasociado2.Visibility = System.Windows.Visibility.Visible
        '    'Me._txtAsociadoId.Visibility = System.Windows.Visibility.Visible
        '    Me._lblIdAsociado.Visibility = System.Windows.Visibility.Collapsed
        '    Me._lblNombreAsociado.Visibility = System.Windows.Visibility.Collapsed
        'End Sub

        'Public Sub PintarAsociado()
        '    Me._txtAsociado.BorderBrush = New SolidColorBrush(Colors.LightGreen)
        'End Sub

        'Public Property NombreAsociado() As String Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.NombreAsociado
        '    Get
        '        Return Me._txtAsociado.Text
        '    End Get
        '    Set(ByVal value As String)
        '        Me._txtAsociado.Text = value
        '    End Set
        'End Property

        'Public Property idAsociadoFiltrar() As String Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.idAsociadoFiltrar
        '    Get
        '        Return Me._txtIdAsociado.Text
        '    End Get
        '    Set(ByVal value As String)
        '        Me._txtIdAsociado.Text = value
        '    End Set
        'End Property

        'Public Property NombreAsociadoFiltrar() As String Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.NombreAsociadoFiltrar
        '    Get
        '        Return Me._txtNombreAsociado.Text
        '    End Get
        '    Set(ByVal value As String)
        '        Me._txtNombreAsociado.Text = value
        '    End Set
        'End Property


        'Public Property Medio As Object Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Medio
        '    Get
        '        Return Me._cbxMedioGestion.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxMedioGestion.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Medios As Object Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Medios
        '    Get
        '        Return Me._cbxMedioGestion.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxMedioGestion.DataContext = value
        '    End Set
        'End Property

        'Public Property Concepto As Object Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Concepto
        '    Get
        '        Return Me._cbxConceptoGestion.SelectedItem
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxConceptoGestion.SelectedItem = value
        '    End Set
        'End Property

        'Public Property Conceptos As Object Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Conceptos
        '    Get
        '        Return Me._cbxConceptoGestion.DataContext
        '    End Get
        '    Set(ByVal value As Object)
        '        Me._cbxConceptoGestion.DataContext = value
        '    End Set
        'End Property

        'Public Property Id As String Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Id
        '    Get
        '        Return (_txtId.Text)
        '    End Get
        '    Set(ByVal value As String)
        '        _txtId.Text = value
        '    End Set
        'End Property

        'Public WriteOnly Property Count As Integer Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Count
        '    Set(ByVal value As Integer)
        '        _lblHits.Text = value
        '    End Set
        'End Property

        Public Sub al_Respuesta_Item(ByVal sender As Object, ByVal args As ExecutedRoutedEventArgs)
            Dim idcarta As Integer = args.Parameter
            Me._presentador.Ver_Carta(idcarta)
        End Sub

        Public Sub al_Gestion_Item(ByVal sender As Object, ByVal args As ExecutedRoutedEventArgs)
            Dim idcarta As Integer = args.Parameter
            Me._presentador.Ver_Carta(idcarta)
        End Sub

        Public Sub al_VerProforma_Item(ByVal sender As Object, ByVal args As ExecutedRoutedEventArgs)
            Dim cproforma As Integer = args.Parameter
        End Sub

        Public Shared Respuesta_Item As New RoutedCommand("Respuesta_Item", GetType(ConsultarFacGestionesAsociado))
        Public Shared Gestion_Item As New RoutedCommand("Gestion_Item", GetType(ConsultarFacGestionesAsociado))

        Public WriteOnly Property Asociado As String Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Asociado
            Set(ByVal value As String)
                _txtAsociado.Text = value
            End Set
        End Property

        Public WriteOnly Property AsociadoDomicilio As String Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.AsociadoDomicilio
            Set(ByVal value As String)
                _txtAsociadoDomicilio.Text = value
            End Set
        End Property

        Public WriteOnly Property AsociadoTipo As String Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.AsociadoTipo
            Set(ByVal value As String)
                _txtAsociadoTipo.Text = value
            End Set
        End Property

        Private Sub _btnNuevaGestion_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
            Me._presentador.AgregarNuevaGestion()
        End Sub

        Private Sub _btnAsociado_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
            Me._presentador.IrConsultarAsociado()
        End Sub

        Private Sub _btnExportarGestiones_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
            Me._presentador.ExportarGestionesExcel()
        End Sub


        Public Sub ExportarListView(ByVal datos As DataTable) Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.ExportarListView

            Dim tablaDatos As New DataTable()
            tablaDatos = datos
            Try

                Dim archivo As New Microsoft.Win32.SaveFileDialog()
                archivo.Filter = "Excel (*.xls)|*.xls"
                archivo.DefaultExt = ".xls"


                If archivo.ShowDialog() = True Then

                    Dim xlObject As Microsoft.Office.Interop.Excel.Application = Nothing
                    Dim xlWB As Microsoft.Office.Interop.Excel.Workbook = Nothing
                    Dim xlSh As Microsoft.Office.Interop.Excel.Worksheet = Nothing
                    Dim rg As Microsoft.Office.Interop.Excel.Range = Nothing
                    xlObject = New Microsoft.Office.Interop.Excel.Application()
                    xlObject.AlertBeforeOverwriting = False
                    xlObject.DisplayAlerts = False
                    xlWB = xlObject.Workbooks.Add(Type.Missing)
                    xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)

                    xlObject.Range("A1", "Z1").Merge()
                    xlObject.Range("A1", "Z1").Value = Me._presentador.ObtenerTituloReporte()
                    xlObject.Range("A1", "Z1").Font.Bold = True
                    xlObject.Range("A1", "Z1").Font.Size = 12

                    For i As Integer = 0 To tablaDatos.Columns.Count - 1
                        xlSh.Range("A3").Offset(0, i).Value = tablaDatos.Columns(i).ColumnName
                        xlSh.Range("A3").Offset(0, i).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                        xlSh.Range("A3").Offset(0, i).VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                        xlSh.Range("A3").Offset(0, i).Font.Bold = True
                        xlSh.Range("A3").Offset(0, i).Font.ColorIndex = 2
                        xlSh.Range("A3").Offset(0, i).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                        xlSh.Range("A3").Offset(0, i).Cells.Interior.ColorIndex = 10
                    Next

                    For Idx As Integer = 0 To tablaDatos.Rows.Count - 1
                        xlSh.Range("A4").Offset(Idx).Resize(1, tablaDatos.Columns.Count).Value = tablaDatos.Rows(Idx).ItemArray
                        'worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        xlSh.Range("A4").Offset(Idx).Resize(1, tablaDatos.Columns.Count).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignGeneral

                        xlSh.Range("A4").Offset(Idx).Resize(1, tablaDatos.Columns.Count).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    Next


                    xlObject.Columns.AutoFit()

                    xlWB.SaveAs(archivo.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal)
                    xlObject.Visible = True

                End If

            Catch ex As Exception
                Throw
            End Try
        End Sub


        Public Sub Mensaje(ByVal mensaje__1 As String, ByVal tipoMensaje As Integer) Implements Contratos.ViGestionAsociados.IConsultarFacGestionesAsociado.Mensaje

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
