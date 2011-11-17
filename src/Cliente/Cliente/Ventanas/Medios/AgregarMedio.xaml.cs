using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Medios;
using Trascend.Bolet.Cliente.Presentadores.Medios;
using System.IO;
using System.Windows.Forms;

namespace Trascend.Bolet.Cliente.Ventanas.Medios
{
    /// <summary>
    /// Interaction logic for AgregarAnexo.xaml
    /// </summary>
    public partial class AgregarMedio : Page, IAgregarMedio
    {
        private PresentadorAgregarMedio _presentador;
        private bool _cargada;
        private Stream _imagenBytes;
        private FileInfo _infoImagen;

        #region IAgregarEstado

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Medio
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            System.Windows.MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public Stream Imagen
        {
            get { return this._imagenBytes; }
            set { this._imagenBytes = value; }
        }

        public FileInfo InformacionImagen
        {
            get { return this._infoImagen; }
            set { this._infoImagen = value; }
        }
        #endregion

        public AgregarMedio()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarMedio(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarMedio();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {
            FileInfo f = null;

            OpenFileDialog dlg;


            dlg = new OpenFileDialog();

            //Open the Pop-Up Window to select the file


            DialogResult result = dlg.ShowDialog();

            if (result == DialogResult.OK)
            {

                _infoImagen = new FileInfo(dlg.FileName);

                using (_imagenBytes = dlg.OpenFile())
                {
                    ////Proceso
                    //TextReader reader = new StreamReader(_imagenBytes);

                    //string st = reader.ReadToEnd();

                    this._filImagen.Text = f.Name;
                }

            }
            else
            {
                dlg.Dispose();
            }
        }

    }
}
