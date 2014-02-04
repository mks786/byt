using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Presentadores.Patentes;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Patentes
{
    /// <summary>
    /// Lógica de interacción para ListaPatentesPrioridadVencida.xaml
    /// </summary>
    public partial class ListaPatentesPrioridadVencida : Page, IListaPatentesPrioridadVencida
    {

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaPatentesPrioridadVencida _presentador;
        private bool _cargada;

        public ListaPatentesPrioridadVencida()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaPatentesPrioridadVencida(this);
        }



        #region IListaPatentesPrioridadVencida

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
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

        public object PatentesPorVencerPrioridad
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object PatenteSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
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
        }
               

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.ConsultarPatenteSeleccionada();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnExportarListado_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarExportarReporteAExcel),
                    "Generar Reporte de Patentes por Vencer", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.ExportarAExcel();
            }
        }

        #endregion
        
        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void ExportarListadoDePatentesPorVencer(DataTable datos)
        {
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

                    DataView tablaDatos = datos.DefaultView;

                    app.Range["A1", "Z1"].Merge();
                    app.Range["A1", "Z1"].Value = this._presentador.ObtenerTituloReporte();
                    app.Range["A1", "Z1"].Font.Bold = true;
                    app.Range["A1", "Z1"].Font.Size = 12;

                    for (int i = 0; i < datos.Columns.Count; i++)
                    {
                        worksheet.Range["A3"].Offset[0, i].Value = datos.Columns[i].ColumnName;
                        worksheet.Range["A3"].Offset[0, i].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["A3"].Offset[0, i].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["A3"].Offset[0, i].Font.Bold = true;
                        worksheet.Range["A3"].Offset[0, i].Font.ColorIndex = 2;
                        worksheet.Range["A3"].Offset[0, i].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        worksheet.Range["A3"].Offset[0, i].Cells.Interior.ColorIndex = 10;
                    }

                    for (int Idx = 0; Idx < datos.Rows.Count; Idx++)
                    {
                        worksheet.Range["A4"].Offset[Idx].Resize[1, datos.Columns.Count].Value = datos.Rows[Idx].ItemArray;
                        worksheet.Range["A4"].Offset[Idx].Resize[1, datos.Columns.Count].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["A4"].Offset[Idx].Resize[1, datos.Columns.Count].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    }

                    app.Columns.AutoFit();

                    workbook.SaveAs(archivo.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);

                    //workbook.Close(true);
                    app.Visible = true;
                    //app.Quit();


                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeshabilitarBotonReportes()
        {
            this._btnExportarListado.IsEnabled = false;
        }

        private DataTable ListViewToDataTable()
        {
            //ListView lvResults = e.Source as ListView;

            //GridView view = lvResults.View as GridView;

            System.Data.DataView theView = ((System.Data.DataRowView)_lstResultados.Items[0]).DataView;

            System.Data.DataTable dt = theView.Table;

            return dt;
        }

        #endregion

        

    }
}
