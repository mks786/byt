using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Trascend.Bolet.ControlesByT.Ventanas
{
    public partial class EtiquetaMarca : Form
    {

        private string _urlImagen;

        public EtiquetaMarca(string urlImagen, string nombreMarca)
        {
            //urlImagen = "C:\\nike.jpg";
            InitializeComponent();
            this.Text += nombreMarca;
            _urlImagen = urlImagen;
            MostrarImagen();
        }
        public void MostrarImagen()
        {
            try
            {
                this._foto.Size = new System.Drawing.Size(268, 213);
                this._foto.SizeMode = PictureBoxSizeMode.CenterImage;
                this._foto.BorderStyle = BorderStyle.Fixed3D;

                this._foto.ImageLocation = _urlImagen;
            }
            catch (IOException ex)
            {
                this.Close();
            }
        }



        private void _btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _foto_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(_urlImagen);
            }
            catch (Win32Exception ex)
            {

            }
            catch (Exception ex) 
            { 
            }
        }

        private void _btnCopiar_Click(object sender, EventArgs e)
        {
            Image imagen = this._foto.Image;
            Clipboard.SetDataObject(imagen);
        }


    }
}
