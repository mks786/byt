using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.SAPI.Presentaciones;
using Trascend.Bolet.Cliente.Presentadores.SAPI.Presentaciones;

namespace Trascend.Bolet.Cliente.Ventanas.SAPI.Presentaciones
{
    /// <summary>
    /// Lógica de interacción para ControlarSolicitudesPresentaciones.xaml
    /// </summary>
    public partial class ControlarSolicitudesPresentaciones : Page, IControlarSolicitudesPresentaciones
    {
        private PresentadorControlarSolicitudesPresentaciones _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IControlarSolicitudesPresentaciones

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public GridViewColumnHeader CurSortCol
        {
            get { return _CurSortCol; }
            set { _CurSortCol = value; }
        }

        public SortAdorner CurAdorner
        {
            get { return _CurAdorner; }
            set { _CurAdorner = value; }
        }

        public ListView ListaResultados
        {
            get { return this._lstResultados; }
            set { this._lstResultados = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        public string FechaSolicitudPresentacion
        {
            get { return this._dpkFechaSolicitudPresentacionSapi.Text; }
            set { this._dpkFechaSolicitudPresentacionSapi.Text = value; }
        }

        public string FechaPresentacionAnteSAPI
        {
            get { return this._dpkFechaPresentacionAnteSapi.Text; }
            set { this._dpkFechaPresentacionAnteSapi.Text = value; }
        }

        public object Dptos
        {
            get { return this._cbxDptoPresentacionSapi.DataContext; }
            set { this._cbxDptoPresentacionSapi.DataContext = value; }
        }

        public object Dpto
        {
            get { return this._cbxDptoPresentacionSapi.SelectedItem; }
            set { this._cbxDptoPresentacionSapi.SelectedItem = value; }
        }

        public object Documentos
        {
            get { return this._cbxDctoPresentacionSapi.DataContext; }
            set { this._cbxDctoPresentacionSapi.DataContext = value; }
        }

        public object Documento
        {
            get { return this._cbxDctoPresentacionSapi.SelectedItem; }
            set { this._cbxDctoPresentacionSapi.SelectedItem = value; }
        }

        public string CodigoExpPresentacion
        {
            get { return this._txtCodExpPresentacionSapi.Text; }
            set { this._txtCodExpPresentacionSapi.Text = value; }
        }

        public object StatusTodos
        {
            get { return this._cbxStatusPresentSapi.DataContext; }
            set { this._cbxStatusPresentSapi.DataContext = value; }
        }

        public object StatusSeleccionado
        {
            get { return this._cbxStatusPresentSapi.SelectedItem; }
            set { this._cbxStatusPresentSapi.SelectedItem = value; }
        }

        public object Usuarios
        {
            get { return this._cbxUsuarioPresentSapi.DataContext; }
            set { this._cbxUsuarioPresentSapi.DataContext = value; }
        }

        public object Usuario
        {
            get { return this._cbxUsuarioPresentSapi.SelectedItem; }
            set { this._cbxUsuarioPresentSapi.SelectedItem = value; }
        }

        public object Gestores
        {
            get { return this._cbxGestorPresentSapi.DataContext; }
            set { this._cbxGestorPresentSapi.DataContext = value; }
        }

        public object Gestor
        {
            get { return this._cbxGestorPresentSapi.SelectedItem; }
            set { this._cbxGestorPresentSapi.SelectedItem = value; }
        }

        public object GestoresRegistro
        {
            get { return this._cbxGestoresProceso.DataContext; }
            set { this._cbxGestoresProceso.DataContext = value; }
        }

        public object GestorRegistro
        {
            get { return this._cbxGestoresProceso.SelectedItem; }
            set { this._cbxGestoresProceso.SelectedItem = value; }
        }

        public string FechaConfirmacion
        {
            get { return this._dpkFechaEvento.Text; }
            set { this._dpkFechaEvento.Text = value; }
        }

        public string IdFactura
        {
            get { return this._txtFactura.Text; }
            set { this._txtFactura.Text = value; }
        }

        public string Ourref
        {
            get { return this._txtOurref.Text; }
            set { this._txtOurref.Text = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        #endregion

        #region Constructores

        public ControlarSolicitudesPresentaciones()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorControlarSolicitudesPresentaciones(this);
        }

        #endregion

        #region Eventos

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
            else
                this._presentador.ActualizarTitulo();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            validarCamposVacios();
        }

        private void _RegistarEvento_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            String nombreBotonPresionado = boton.Name;
            this._presentador.RegistrarControlDePresentaciones(nombreBotonPresionado);
        }
               

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegistrarEventoPresentacionDocumentos();
        }

        private void _btnSuspender_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.SuspenderRegistroEvento();
        }

        private void _btnExportarDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarExportarConsulta),
                    "Exportar Detalle de Eventos de Presentación", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.ExportarResultadosExcel();
            }
        }

        private void _txtFactura_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox cuadroTexto = (TextBox)sender;
            String nombreCuadroTexto = cuadroTexto.Name;
            this._presentador.VerFacturaDocumento(nombreCuadroTexto);
        }

        private void _btnConfirmarFacturacion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegistrarEventoFacturacion();
        }

        


        #endregion


        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filto
        /// </summary>
        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;

            if (!this._dpkFechaSolicitudPresentacionSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFechaSolicitudPresentacionSapi.Focus();
            }

            if (!this._dpkFechaPresentacionAnteSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFechaPresentacionAnteSapi.Focus();
            }

            if ((this._cbxDptoPresentacionSapi.SelectedIndex != 0) && (this._cbxDptoPresentacionSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxDptoPresentacionSapi.Focus();
            }

            if ((this._cbxDctoPresentacionSapi.SelectedIndex != 0) && (this._cbxDctoPresentacionSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxDctoPresentacionSapi.Focus();
            }

            if ((this._cbxStatusPresentSapi.SelectedIndex != 0) && (this._cbxStatusPresentSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxStatusPresentSapi.Focus();
            }

            if ((this._cbxUsuarioPresentSapi.SelectedIndex != 0) && (this._cbxUsuarioPresentSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxUsuarioPresentSapi.Focus();
            }

            if ((this._cbxGestorPresentSapi.SelectedIndex != 0) && (this._cbxGestorPresentSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxGestorPresentSapi.Focus();
            }

            if (!this._txtCodExpPresentacionSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtCodExpPresentacionSapi.Focus();
            }

            if (todosCamposVacios)
            {
                this._dpkFechaSolicitudPresentacionSapi.Focus();
            }
        }


        public void MostrarCamposRegistroEvento(string tipoBandera)
        {
            this._btnRecepcionPorGestor.Visibility = System.Windows.Visibility.Collapsed;
            this._btnPresentacionEnSAPI.Visibility = System.Windows.Visibility.Collapsed;
            this._btnRecepcionDeSAPI.Visibility = System.Windows.Visibility.Collapsed;
            this._btnRecepcionDpto.Visibility = System.Windows.Visibility.Collapsed;
            this._btnLimpiarCampos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnCancelar.Visibility = System.Windows.Visibility.Collapsed;

            if (!tipoBandera.Equals("5"))
            {
                this._lblGestor.Visibility = System.Windows.Visibility.Visible;
                this._cbxGestoresProceso.Visibility = System.Windows.Visibility.Visible; 
            }

            switch (tipoBandera)
            {
                case "1":
                    this._lblFechaRecepcionMaterial.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "2":
                    this._lblFechaPresentacionSAPI.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "3":
                    this._lblFechaRecepcionSAPI.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "4":
                    this._lblFechaRecepcionDpto.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "5":
                    this._lblCodigoFactura.Visibility = System.Windows.Visibility.Visible;
                    this._txtFactura.Visibility = System.Windows.Visibility.Visible;
                    this._lblReferencia.Visibility = System.Windows.Visibility.Visible;
                    this._txtOurref.Visibility = System.Windows.Visibility.Visible;
                    break;
            }

            if (!tipoBandera.Equals("5"))
            {
                this._dpkFechaEvento.Visibility = System.Windows.Visibility.Visible;
                this._btnConfirmar.Visibility = System.Windows.Visibility.Visible;
            }
            else
                this._btnConfirmarFacturacion.Visibility = System.Windows.Visibility.Visible;
            
            this._btnSuspender.Visibility = System.Windows.Visibility.Visible;
        }


        public void OcultarCamposRegistroEvento(string tipoBandera)
        {
            this._lblGestor.Visibility = System.Windows.Visibility.Collapsed;
            this._cbxGestoresProceso.Visibility = System.Windows.Visibility.Collapsed;
            switch (tipoBandera)
            {
                case "1":
                    this._lblFechaRecepcionMaterial.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "2":
                    this._lblFechaPresentacionSAPI.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "3":
                    this._lblFechaRecepcionSAPI.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "4":
                    this._lblFechaRecepcionDpto.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "5":
                    this._lblCodigoFactura.Visibility = System.Windows.Visibility.Collapsed;
                    this._txtFactura.Visibility = System.Windows.Visibility.Collapsed;
                    this._lblReferencia.Visibility = System.Windows.Visibility.Collapsed;
                    this._txtOurref.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }

            this._dpkFechaEvento.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConfirmar.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConfirmarFacturacion.Visibility = System.Windows.Visibility.Collapsed;
            this._btnSuspender.Visibility = System.Windows.Visibility.Collapsed;

            this._btnRecepcionPorGestor.Visibility = System.Windows.Visibility.Visible;
            this._btnPresentacionEnSAPI.Visibility = System.Windows.Visibility.Visible;
            this._btnRecepcionDeSAPI.Visibility = System.Windows.Visibility.Visible;
            this._btnRecepcionDpto.Visibility = System.Windows.Visibility.Visible;
            this._btnLimpiarCampos.Visibility = System.Windows.Visibility.Visible;
            this._btnCancelar.Visibility = System.Windows.Visibility.Visible;

        }


        /// <summary>
        /// Metodo que recibe un DataTable para mostrar su contenido en una hoja Excel
        /// </summary>
        /// <param name="datosResumen">Datos a mostrar en la hoja de Excel</param>
        public void ExportarDatosConsolidadosExcel(DataTable datosResumen)
        {

            Mouse.OverrideCursor = Cursors.Wait;

            DataTable tablaDatos = null;

            try
            {
                System.Windows.Forms.SaveFileDialog archivo = new System.Windows.Forms.SaveFileDialog();
                archivo.Filter = "Excel (*.xls)|*.xls";

                if (archivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel._Workbook workbook;
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    object misValue = System.Reflection.Missing.Value;

                    workbook = app.Workbooks.Add(misValue);
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);


                    tablaDatos = datosResumen;

                    app.Range["A1", "Z1"].Merge();
                    app.Range["A1", "Z1"].Value = "Status de Documentos de Solicitudes de Presentación SAPI";
                    app.Range["A1", "Z1"].Font.Bold = true;
                    app.Range["A1", "Z1"].Font.Size = 12;

                    app.Range["A3", "Z2"].Merge();
                    app.Range["A3", "Z2"].Value = "Generado por: " + this._presentador.ObtenerNombreUsuario();
                    app.Range["A3", "Z2"].Font.Size = 10;


                    for (int i = 0; i < tablaDatos.Columns.Count; i++)
                    {
                        worksheet.Range["A5"].Offset[0, i].Value = tablaDatos.Columns[i].ColumnName;
                        worksheet.Range["A5"].Offset[0, i].HorizontalAlignment =
                            Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["A5"].Offset[0, i].VerticalAlignment =
                            Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["A5"].Offset[0, i].Font.Bold = true;
                        worksheet.Range["A5"].Offset[0, i].Font.ColorIndex = 2;
                        worksheet.Range["A5"].Offset[0, i].Borders.LineStyle =
                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        worksheet.Range["A5"].Offset[0, i].Cells.Interior.ColorIndex = 10;
                    }

                    worksheet.Range["A6", "E6"].Style.WrapText = true;
                    worksheet.Range["A6", "E6"].ColumnWidth = 50;
                    worksheet.Range["A6", "F6"].ColumnWidth = 45;
                    worksheet.Range["A6", "B6"].ColumnWidth = 45;

                    for (int Idx = 0; Idx < tablaDatos.Rows.Count; Idx++)
                    {
                        worksheet.Range["A6"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].Value =
                            tablaDatos.Rows[Idx].ItemArray;
                        worksheet.Range["A6"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].HorizontalAlignment =
                            Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignGeneral;
                        worksheet.Range["A6"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].VerticalAlignment =
                            Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        worksheet.Range["A6"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].Borders.LineStyle =
                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    }

                    app.Columns.AutoFit();
                    workbook.SaveAs(archivo.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    Mouse.OverrideCursor = null;
                    app.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        
        
        

    }
}
