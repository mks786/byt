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
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeClientes;
using Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoDeClientes;

namespace Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoDeClientes
{
    /// <summary>
    /// Lógica de interacción para ListaDatosPivotSeguimientoClientes.xaml
    /// </summary>
    public partial class ListaDatosPivotSeguimientoClientes : Page, IListaDatosPivotSeguimientoClientes
    {
        private bool _cargada;
        private PresentadorListaDatosPivotSeguimientoClientes _presentador;

        /// <summary>
        /// Constructor por defecto que recibe los ejes para generar la tabla pívot y una ventana padre
        /// </summary>
        /// <param name="ejeX">Eje X de la tabla Pivot</param>
        /// <param name="ejeY">Eje Y de la tabla Pivot</param>
        /// <param name="ejeZ">Eje Z de la tabla Pivot</param>
        /// <param name="datos">DataTable con los datos crudos ya filtrados</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public ListaDatosPivotSeguimientoClientes(object filtroDataSaldos, object ejeX, object ejeY, object ejeZ, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaDatosPivotSeguimientoClientes(this, filtroDataSaldos, ejeX, ejeY, ejeZ, ventanaPadre);
        }


        #region IListaDatosPivotSeguimientoClientes

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object ResultadosDetalle
        {
            get { return this._lstResultadosDetalle.DataContext; }
            set { this._lstResultadosDetalle.DataContext = value; }
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


        /// <summary>
        /// Evento para seleccionar un registro del datagrid donde aparecen los datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstResultados_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) &&
                    !(dep is DataGridCell) &&
                    !(dep is System.Windows.Controls.Primitives.DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            //Se valida que se haya seleccionado una celda del DataGrid
            if (dep is DataGridCell)
            {
                //Navegamos por el arbol de elementos del datagrid para obtener el elementos seleccionado
                DataGridCell cell = dep as DataGridCell;
                
                while ((dep != null) && !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                //Se obtiene el DataRow del DataGrid seleccionado para sacar el elemento al que se le hizo click
                DataGridRow row = dep as DataGridRow;
                int indiceFila = FindRowIndex(row);

                object datoCelda = ExtractBoundValue(row, cell, indiceFila);
                String datos = datoCelda.ToString();
                //DataTable datos1 = CrearQuery(datos);
                this._presentador.CargarDatosDetalle(datos);

                /*if (datos1.Rows.Count > 0)
                {
                    ResultadoDetalle resultado = new ResultadoDetalle(datos1);
                    resultado.Show();
                }
                else
                {
                    MessageBox.Show("No hay datos para esta seleccion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }*/

            }
        }

        private object ExtractBoundValue(DataGridRow row, DataGridCell cell, int indiceFila)
        {
            String headerColumn = String.Empty;
            String valorColumnaCero = String.Empty;
            DataGridCell primeraCeldaFilaSeleccionada = null;

            //Para encontrar el primer elemento de la fila donde se encuentra la celda seleccionada
            if (row != null)
            {
                System.Windows.Controls.Primitives.DataGridCellsPresenter presenter =
                    GetVisualChild<System.Windows.Controls.Primitives.DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    this._lstResultados.ScrollIntoView(row, this._lstResultados.Columns[0]);
                    presenter = GetVisualChild<System.Windows.Controls.Primitives.DataGridCellsPresenter>(row);
                }

                primeraCeldaFilaSeleccionada = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(0);
            }


            // Se busca la columna a la que pertenece la celda seleccionada y la columna de la primera celda de la fila 
            // de la columna seleccionada
            DataGridBoundColumn col = cell.Column as DataGridBoundColumn;
            DataGridBoundColumn columnaCero = primeraCeldaFilaSeleccionada.Column as DataGridBoundColumn;

            //Aqui se sabe cual es header de la celda seleccionada y el de la primera celda de la fila a la que pertenece 
            Binding binding = col.Binding as Binding;
            Binding bindingCeldaCero = columnaCero.Binding as Binding;

            string headerColCero = bindingCeldaCero.Path.Path;
            string boundPropertyName = binding.Path.Path;

            //Se busca el objecto relacionado con la fila seleccionada
            object data = row.Item;

            // Se extrae la propiedad de cada una de las celdas
            System.ComponentModel.PropertyDescriptorCollection properties = System.ComponentModel.TypeDescriptor.GetProperties(data);

            System.ComponentModel.PropertyDescriptor property = properties[boundPropertyName];
            object value = property.GetValue(data);

            System.ComponentModel.PropertyDescriptor property1 = properties[headerColCero];
            object valor1 = property1.GetValue(data);

            //String cadena = headerColCero + "&" + valor1.ToString() + "&" + boundPropertyName + value.ToString();

            String cadena = valor1.ToString() + "&" + boundPropertyName;

            return cadena;

        }


        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }


        /// <summary>
        /// Metodo que devuelve el indice de la celda que se selecciono con el Mouse
        /// </summary>
        /// <param name="row">Fila donde se encuentra la celda</param>
        /// <returns>Indice de la celda que fue seleccionada</returns>
        private int FindRowIndex(DataGridRow row)
        {
            DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;

            int index = dataGrid.ItemContainerGenerator.
                IndexFromContainer(row);

            return index;
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


        public void VisibilidadListaDetalle()
        {
            this._lstResultadosDetalle.Visibility= System.Windows.Visibility.Visible;
        }

        #endregion
    }
}
