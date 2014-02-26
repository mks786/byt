using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional;

namespace Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional
{
    /// <summary>
    /// Lógica de interacción para FacInternacionalAprobadas.xaml
    /// </summary>
    public partial class FacInternacionalAprobadas : Page, IFacInternacionalAprobadas
    {

        private bool _cargada;
        private PresentadorFacInternacionalAprobadas _presentador;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;


        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="proformasAprobadas">Lista de proformas internacionales aprobadas</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public FacInternacionalAprobadas(object proformasAprobadas, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorFacInternacionalAprobadas(this, proformasAprobadas, ventanaPadre);
        }



        #region IFacInternacionalAprobadas

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        public object FacturasAutorizadas
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object FacturasSeleccionadas
        {
            get { return this._lstResultados.SelectedItems; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string TotalMontoAprobado
        {
            get { return this._txtTotalMonto.Text; }
            set { this._txtTotalMonto.Text = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
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

        private void _btnConsolidar_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            String nombreBoton = boton.Name;
            bool hayDatos = false;

            if (nombreBoton.Equals("_btnConsolidar"))
            {
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarConsolidacionCxPInternacional),
                            "Consolidar CxP Internacional", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    
                    hayDatos = this._presentador.VerificarFacAsociadoConsolidadoGuardado();

                    if (hayDatos)
                    {
                        if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarHayDatosConsolidacionCxPInternacional),
                            "Consolidar CxP Internacional", MessageBoxButton.YesNo, MessageBoxImage.Question))
                        {
                            Mouse.OverrideCursor = Cursors.Wait;
                            this._presentador.CargarDatosConsolidacion(nombreBoton);
                            Mouse.OverrideCursor = null;
                        }
                        else
                        {
                            Mouse.OverrideCursor = Cursors.Wait;
                            this._presentador.IrConsolidarFacturasSeleccionadas(nombreBoton);
                            Mouse.OverrideCursor = null;
                        }
                    }
                    else
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        this._presentador.IrConsolidarFacturasSeleccionadas(nombreBoton);
                        Mouse.OverrideCursor = null;
                    }

                }
            }
            else if(nombreBoton.Equals("_btnVerDatosConsolidar"))
            {
                hayDatos = this._presentador.VerificarFacAsociadoConsolidadoGuardado();

                if (hayDatos)
                {
                    if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.AlertaVistaPreviaConsolidacionCxPInt),
                        "Vista Previa de Consolidación", MessageBoxButton.YesNo, MessageBoxImage.Question))
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        this._presentador.CargarDatosConsolidacion(nombreBoton);
                        Mouse.OverrideCursor = null;
                    }
                    else
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        this._presentador.IrConsolidarFacturasSeleccionadas(nombreBoton);
                        Mouse.OverrideCursor = null;
                    }
                }
                else
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    this._presentador.IrConsolidarFacturasSeleccionadas(nombreBoton);
                    Mouse.OverrideCursor = null;
                }
            }
            
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.AlertaCambiarStatusFacCxPInternacional),
                            "Actualizar Status Factura Internacional", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.RegistrarPago();
            }
            
        }

        private void _btnActualizarFactInternacional_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ActualizarListadoFacturasAprobadas();
        }

        private void _btnExportar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ExportarFacturasSeleccionadasExcel();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        #endregion


        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void HabilitarBotonActualizar(bool estado)
        {
            this._btnActualizarFactInternacional.IsEnabled = estado;
        }


        public bool ExportarListadoFacturasAprobadas(string tituloReporte, DataTable datosExportar)
        {
            bool retorno = false;

            DataTable tablaDatos = datosExportar;

            try
            {
                System.Windows.Forms.SaveFileDialog archivo = new System.Windows.Forms.SaveFileDialog();
                archivo.FileName = "FacInternacionalAprobadas";
                archivo.Filter = "Excel (*.xls)|*.xls";

                if (archivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel._Workbook workbook;
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    object misValue = System.Reflection.Missing.Value;

                    workbook = app.Workbooks.Add(misValue);
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);

                    app.Range["A1", "Z1"].Merge();
                    app.Range["A1", "Z1"].Value = tituloReporte;
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

                    for (int Idx = 0; Idx < tablaDatos.Rows.Count; Idx++)
                    {
                        worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].Value =
                            tablaDatos.Rows[Idx].ItemArray;
                        worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].HorizontalAlignment =
                            Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignGeneral;
                        worksheet.Range["A4"].Offset[Idx].Resize[1, tablaDatos.Columns.Count].Borders.LineStyle =
                            Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    }

                    app.Columns.AutoFit();
                    workbook.SaveAs(archivo.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    app.Visible = true;

                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }


            return retorno;
        }

        #endregion

        

        

        

        

        
    }
}
