using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.ReportesMaestro;
using Trascend.Bolet.Cliente.Presentadores.ReportesMaestro;
using System.Data;

namespace Trascend.Bolet.Cliente.Ventanas.ReportesMaestro
{
    /// <summary>
    /// Lógica de interacción para VisualizarReporte.xaml
    /// </summary>
    public partial class VisualizarReporte : Page, IVisualizarReporte
    {
        private bool _cargada;
        private PresentadorVisualizarReporte _presentador;

        /// <summary>
        /// Constructor por defecto que recibe una ventana padre
        /// </summary>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public VisualizarReporte(object ventanaPadre, object reporte, object resultado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorVisualizarReporte(this, reporte, resultado, ventanaPadre);
        }


        #region IVisualizarReporte

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public object Resultados
        {
            get { return this._grid.DataContext; }
            set { this._grid.DataContext = value; }
        }

        public object Reporte
        {
            get { return this._stckDatos.DataContext; }
            set { this._stckDatos.DataContext = value; }
        }

        public string TituloReporte
        {
            get { return this._txtTituloReporte.Text; }
            set { this._txtTituloReporte.Text = value; }
        }

        public string AutorReporte
        {
            get { return this._txtUsuarioReporte.Text; }
            set { this._txtUsuarioReporte.Text = value; }
        }

        public object FiltrosReporte
        {
            get { return this._lstFiltrosReporte.DataContext; }
            set { this._lstFiltrosReporte.DataContext = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
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

        private void _btnExportarReporte_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarExportarReporteAExcel),
                    "Exportar Reporte", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.ExportarAExcel();
            }
        }

        #endregion


        /// <summary>
        /// Metodo para llenar el DataGridView de la ventana
        /// </summary>
        /// <param name="datos">DataTable con los datos para llenar el grid de datos</param>
        public void LlenarDataGrid(DataTable datos)
        {

            IList<String> columnas = new List<String>();
            String nombreColumna = String.Empty;
                      
            
            foreach (DataColumn columna in datos.Columns)
            {
                nombreColumna = columna.ColumnName;
                columnas.Add(nombreColumna);
            }

            //Agrego al nuevo DataTable las columnas 
            foreach (String item in columnas)
            {
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = item;
                textColumn.Binding = new System.Windows.Data.Binding(item);
                if(textColumn.Header.Equals("No"))
                    textColumn.Width=100;
                else
                    textColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                this._dataGrid.Columns.Add(textColumn);
                
            }

            this._dataGrid.DataContext = datos;


        }



        /// <summary>
        /// Metodo que exporta el contenido del DataGrid a Excel
        /// </summary>
        public void ExportarDataGrid()
        {

            try
            {
                SaveFileDialog archivo = new SaveFileDialog();
                archivo.Filter = "Excel (*.xls)|*.xls";

                if (archivo.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel._Workbook workbook;
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    object misValue = System.Reflection.Missing.Value;

                    workbook = app.Workbooks.Add(misValue);
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);

                    DataTable tablaDatos = (DataTable)this._dataGrid.DataContext;
                    
                    //DataView tabla1 = (DataView)this._dataGrid.ItemsSource;

                    
                    app.Range["A1", "F1"].Merge();
                    app.Range["A1", "F1"].Value = this._presentador.ObtenerTituloReporte();
                    app.Range["A1", "F1"].Font.Bold = true;
                    app.Range["A1", "F1"].Font.Size = 12;

                    
                    for (int i = 0; i < tablaDatos.Columns.Count; i++)
                    {
                        worksheet.Range["A3"].Offset[0, i].Value = tablaDatos.Columns[i].ColumnName;
                        worksheet.Range["A3"].Offset[0, i].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["A3"].Offset[0, i].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["A3"].Offset[0, i].Font.Bold = true;
                        worksheet.Range["A3"].Offset[0, i].Font.ColorIndex = 2;
                        worksheet.Range["A3"].Offset[0, i].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        worksheet.Range["A3"].Offset[0, i].Cells.Interior.ColorIndex = 10;
                    }

                    for (int Idx = 0; Idx < tablaDatos.Rows.Count; Idx++)
                    {
                        worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].Value = tablaDatos.Rows[Idx].ItemArray;
                        worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    }

                    app.Columns.AutoFit();

                    workbook.SaveAs(archivo.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);

                    //workbook.Close(true);
                    app.Visible = true;
                    //app.Quit();
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }


        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                System.Windows.MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                System.Windows.MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        

        
    }
}
