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
using Trascend.Bolet.Cliente.Contratos.SAPI.Materiales;
using Trascend.Bolet.Cliente.Presentadores.SAPI.Materiales;

namespace Trascend.Bolet.Cliente.Ventanas.SAPI.Materiales
{
    /// <summary>
    /// Lógica de interacción para ConsultarSolicitudesMateriales.xaml
    /// </summary>
    public partial class ConsultarSolicitudesMateriales : Page, IConsultarSolicitudesMateriales
    {

        private bool _cargada;
        private PresentadorConsultarSolicitudesMateriales _presentador; 
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;


        #region IConsultarSolicitudesMateriales

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._dpkFechaSolicitudSapi.Focus();
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

        
        public string FechaSolicitudSapi
        {
            get { return this._dpkFechaSolicitudSapi.Text; }
            set { this._dpkFechaSolicitudSapi.Text = value; }
        }

        public object MaterialesSapi
        {
            get { return this._cbxMaterialSolicitudSapi.DataContext; }
            set { this._cbxMaterialSolicitudSapi.DataContext = value; }
        }

        public object MaterialSapi
        {
            get { return this._cbxMaterialSolicitudSapi.SelectedItem; }
            set { this._cbxMaterialSolicitudSapi.SelectedItem = value; }
        }

        public object Departamentos
        {
            get { return this._cbxDptoSolicitudSapi.DataContext; }
            set { this._cbxDptoSolicitudSapi.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDptoSolicitudSapi.SelectedItem; }
            set { this._cbxDptoSolicitudSapi.SelectedItem = value; }
        }

        public object Usuarios
        {
            get { return this._cbxUsuarioSolicitudSapi.DataContext; }
            set { this._cbxUsuarioSolicitudSapi.DataContext = value; }
        }

        public object Usuario
        {
            get { return this._cbxUsuarioSolicitudSapi.SelectedItem; }
            set { this._cbxUsuarioSolicitudSapi.SelectedItem = value; }
        }

        public object StatusSolicitudesSapi
        {
            get { return this._cbxStatusSolicitudSapi.DataContext; }
            set { this._cbxStatusSolicitudSapi.DataContext = value; }
        }

        public object StatusSolicitudSapi
        {
            get { return this._cbxStatusSolicitudSapi.SelectedItem; }
            set { this._cbxStatusSolicitudSapi.SelectedItem = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object SolicitudSapiSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public object SolicitudSapiFiltro
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        #endregion
        

        #region Constructores

        /// <summary>
        /// Constructor predeterminado sin parametros
        /// </summary>
		public ConsultarSolicitudesMateriales()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarSolicitudesMateriales(this);
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

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            validarCamposVacios();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.VerSolicitudSapi();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        private void _btnExportar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarExportarConsulta),
                    "Exportar Consulta de Solicitudes de Materiales", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.ExportarResultadosExcel();
            }
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

            if (!this._dpkFechaSolicitudSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFechaSolicitudSapi.Focus();
            }

            if ((this._cbxMaterialSolicitudSapi.SelectedIndex != 0) && (this._cbxMaterialSolicitudSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxMaterialSolicitudSapi.Focus();
            }

            if ((this._cbxDptoSolicitudSapi.SelectedIndex != 0) && (this._cbxDptoSolicitudSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxDptoSolicitudSapi.Focus();
            }

            if ((this._cbxUsuarioSolicitudSapi.SelectedIndex != 0) && (this._cbxUsuarioSolicitudSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxUsuarioSolicitudSapi.Focus();
            }

            if ((this._cbxStatusSolicitudSapi.SelectedIndex != 0) && (this._cbxStatusSolicitudSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxStatusSolicitudSapi.Focus();
            }

            if (todosCamposVacios)
            {
                this._dpkFechaSolicitudSapi.Focus();
            }
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
                    //app.Range["A1", "Z1"].Value = this._presentador.ObtenerTituloReporte(tipo);
                    app.Range["A1", "Z1"].Value = "Solicitud de Materiales SAPI";
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
