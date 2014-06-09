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
    /// Lógica de interacción para ConsultarExistenciaMaterial.xaml
    /// </summary>
    public partial class ConsultarExistenciaMaterial : Page, IConsultarExistenciaMaterial
    {

        private bool _cargada;
        private PresentadorConsultarExistenciaMaterial _presentador;

        #region Constructores

        public ConsultarExistenciaMaterial()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarExistenciaMaterial(this);
        } 

        #endregion


        #region IConsultarExistenciaMaterial

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public object MaterialSapi
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object MaterialIds
        {
            get { return this._cbxMaterialSapiCodigo.DataContext; }
            set { this._cbxMaterialSapiCodigo.DataContext = value; }
        }

        public object MaterialId
        {
            get { return this._cbxMaterialSapiCodigo.SelectedItem; }
            set { this._cbxMaterialSapiCodigo.SelectedItem = value; }
        }

        public object MaterialDescripciones
        {
            get { return this._cbxMaterialSapiDescripcion.DataContext; }
            set { this._cbxMaterialSapiDescripcion.DataContext = value; }
        }

        public object MaterialDescripcion
        {
            get { return this._cbxMaterialSapiDescripcion.SelectedItem; }
            set { this._cbxMaterialSapiDescripcion.SelectedItem = value; }
        }

        public object MaterialTipos
        {
            get { return this._cbxMaterialSapiTipoMaterial.DataContext; }
            set { this._cbxMaterialSapiTipoMaterial.DataContext = value; }
        }

        public object MaterialTipo
        {
            get { return this._cbxMaterialSapiTipoMaterial.SelectedItem; }
            set { this._cbxMaterialSapiTipoMaterial.SelectedItem = value; }
        }

        public object MaterialDepartamentos
        {
            get { return this._cbxMaterialSapiDepartmaento.DataContext; }
            set { this._cbxMaterialSapiDepartmaento.DataContext = value; }
        }

        public object MaterialDepartamento
        {
            get { return this._cbxMaterialSapiDepartmaento.SelectedItem; }
            set { this._cbxMaterialSapiDepartmaento.SelectedItem = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
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

        private void _btnEjecutarReporte_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerReporteExistenciasExcel();
        }

        private void _btnBuscarMateriales_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarMateriales();
        }

        private void _btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _cbxMaterialSapiCodigo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this._btnBuscarMateriales.Focus();
            if (!this._btnBuscarMateriales.IsFocused)
                this._btnBuscarMateriales.Focus();
        }

        private void _cbxMaterialSapiDescripcion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this._btnBuscarMateriales.IsFocused)
                this._btnBuscarMateriales.Focus();
        }

        private void _cbxMaterialSapiTipoMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this._btnBuscarMateriales.IsFocused)
                this._btnBuscarMateriales.Focus();
        }

        private void _cbxMaterialSapiDepartmaento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this._btnBuscarMateriales.IsFocused)
                this._btnBuscarMateriales.Focus();
        }
                
        #endregion

        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if(opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if(opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        public void ExportarDatosExcel(DataTable datosFiltrados)
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


                    tablaDatos = datosFiltrados;

                    app.Range["A1", "H1"].Merge();
                    app.Range["A1", "H1"].Value = "Reporte de Existencia de Material SAPI";
                    app.Range["A1", "H1"].Font.Bold = true;
                    app.Range["A1", "H1"].Font.Size = 12;


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

                    worksheet.Range["A4", "E4"].Style.WrapText = true;
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
