using System;
using System.Collections.Generic;
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
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional;
using System.Data;

namespace Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional
{
    /// <summary>
    /// Lógica de interacción para FacInternacionalConsolidadas.xaml
    /// </summary>
    public partial class FacInternacionalConsolidadas : Page, IFacInternacionalConsolidadas
    {
        private bool _cargada;
        private PresentadorFacInternacionalConsolidadas _presentador;

        /// <summary>
        /// Constructor predeterminado que recibe las facturas aprobadas y que se van a consolidar
        /// </summary>
        /// <param name="listaFacInternacionalesAprobadas">Facturas aprobadas a consolidar</param>
        /// <param name="soloVer">Bit necesario para seleccionar si se va a ver el proceso de consolidacion</param>
        public FacInternacionalConsolidadas(object listaFacInternacionalesAprobadas, bool soloVer)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorFacInternacionalConsolidadas(this,listaFacInternacionalesAprobadas, soloVer);
        }

        /// <summary>
        /// Constructor predeterminado que recibe las facturas aprobadas y seleccionadas a consolidar y una ventana padre
        /// </summary>
        /// <param name="listaFacInternacionalesAprobadas">Facturas aprobadas a consolidar</param>
        /// <param name="soloVer">Bit necesario para seleccionar si se va a ver el proceso de consolidacion</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public FacInternacionalConsolidadas(object listaFacInternacionalesAprobadas, bool soloVer, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorFacInternacionalConsolidadas(this, listaFacInternacionalesAprobadas, soloVer, ventanaPadre);
        }

        /// <summary>
        /// Constructor por defecto que recibe un grupo de datos ya consolidados
        /// </summary>
        /// <param name="listaFacAsociadoIntCxPInternacional">Datos consolidados previamente guardados</param>
        /// <param name="listaFacInternacionalesAprobadas">Lista de Facturas Internacionales seleccionadas para consolidar</param>
        /// <param name="soloVer">Bit necesario para seleccionar si se va a ver el proceso de consolidacion</param>
        /// <param name="datosConsolidados">Bit para indicar si son datos consolidados previamente guardados</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public FacInternacionalConsolidadas(object listaFacAsociadoIntCxPInternacional, object listaFacInternacionalesAprobadas, bool soloVer, bool datosConsolidados, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorFacInternacionalConsolidadas(this, listaFacAsociadoIntCxPInternacional, listaFacInternacionalesAprobadas, soloVer, datosConsolidados, ventanaPadre);
        }


        /// <summary>
        /// Constructor predeterminado que se usa para cargar la pagina si y solo si se viene de modificar los datos de transferencia
        /// </summary>
        /// <param name="listaFacAsociadoIntCxPInternacional">Lista de consolidacion modificada</param>
        /// <param name="soloVer">Bandera que indica si se muestran los datos o si se cargan los datos definitivos de consolidacion</param>
        /// <param name="transferenciaModificada">Bandera que indica si hubo datos de transferencia modificados</param>
        /// <param name="refrescaVentana">Bandera que indica si se va a refrescar la ventana</param>
        /// <param name="ventanaPadre">Ventana FacInternacionalAprobadas</param>
        public FacInternacionalConsolidadas(object listaFacAsociadoIntCxPInternacional, bool soloVer, bool transferenciaModificada, bool refrescaVentana, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorFacInternacionalConsolidadas(this, listaFacAsociadoIntCxPInternacional, soloVer, transferenciaModificada,refrescaVentana,ventanaPadre);
        }
                

        #region IFacInternacionalConsolidadas

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
           this._btnRegresar.Focus();
        }

        public object FacturasAprobadas
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object FacturasAprobadasSoloVer
        {
            get { return this._lstResultados1.DataContext; }
            set { this._lstResultados1.DataContext = value; }
        }

        public string TotalMontoConsolidado
        {
            get { return this._txtTotalMontoConsolidado.Text; }
            set { this._txtTotalMontoConsolidado.Text = value; }
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


        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }


        private void RegistrarPagoConsolidado_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            object objeto = b.CommandParameter as object;
            this._presentador.RegistrarPagoConsolidado(objeto);

        }

        private void VerDetallePagoConsolidado_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            object objeto = b.CommandParameter as object;
            this._presentador.VerDetalleDeConsolidado(objeto);

        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarGuardarDatosConsolidacion),
                            "Consolidar CxP Internacional", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                Mouse.OverrideCursor = Cursors.Wait;
                this._presentador.Modificar();
                Mouse.OverrideCursor = null;
            } 
            
        }

        private void DatosDeTransferenciaAsociado_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            object objeto = b.CommandParameter as object;
            this._presentador.VerDatosTransferenciaAsociado(objeto);
        }


        private void _btnExportarResumen_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ExportarExcelDatosConsolidados();
        }

        #endregion

        #region Metodos

        public void HabilitarListaSoloVer()
        {
            this._lstResultados1.Visibility = System.Windows.Visibility.Visible;
            this._lstResultados.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void HabilitarBotonModificar()
        {
            this._btnModificar.Visibility = System.Windows.Visibility.Visible;
        }

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// Metodo que exportar el contenido de la pantalla a una hoja de Excel
        /// </summary>
        /// <param name="tipo">Tipo de Vista para mostrar en el titulo</param>
        /// <param name="datosResumen">Datos a exportar</param>
        public void ExportarDatosConsolidadosExcel(String tipo, DataTable datosResumen)
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
                    app.Range["A1", "Z1"].Value = this._presentador.ObtenerTituloReporte(tipo);
                    app.Range["A1", "Z1"].Font.Bold = true;
                    app.Range["A1", "Z1"].Font.Size = 12;


                    for (int i = 0; i < tablaDatos.Columns.Count; i++)
                    {
                        worksheet.Range["A3"].Offset[0, i].Value = tablaDatos.Columns[i].ColumnName;
                        worksheet.Range["A3"].Offset[0, i].HorizontalAlignment =
                            Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["A3"].Offset[0, i].VerticalAlignment =
                            Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range["A3"].Offset[0, i].Font.Bold = true;
                        worksheet.Range["A3"].Offset[0, i].Font.ColorIndex = 2;
                        worksheet.Range["A3"].Offset[0, i].Borders.LineStyle =
                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        worksheet.Range["A3"].Offset[0, i].Cells.Interior.ColorIndex = 10;
                    }

                    worksheet.Range["A4","E4"].Style.WrapText = true;
                    worksheet.Range["A4", "E4"].ColumnWidth = 50;
                    worksheet.Range["A4", "F4"].ColumnWidth = 45;
                    worksheet.Range["A4", "B4"].ColumnWidth = 45;

                    for (int Idx = 0; Idx < tablaDatos.Rows.Count; Idx++)
                    {
                        worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].Value =
                            tablaDatos.Rows[Idx].ItemArray;
                        worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].HorizontalAlignment =
                            Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignGeneral;
                        worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].VerticalAlignment = 
                            Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].Borders.LineStyle =
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
