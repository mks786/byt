using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trascend.Bolet.ControlesByT.Ventanas
{
    public partial class Impresion : Form
    {
        private bool _clickImprimir = false;

        public bool ClickImprimir
        {
            get { return _clickImprimir; }
            set { _clickImprimir = value; }
        }

        public Impresion(string titulo, string folio)
        {
            InitializeComponent();
            this._folio.ScrollBars = ScrollBars.Vertical;
            this.Text = titulo;
            this._folio.Text = folio;
        }

        private void _btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _imprimir_Click(object sender, EventArgs e)
        {
            //Genero el .txt utilizado para ejecutar el .bat
            System.IO.File.WriteAllText(@"C:\print.txt", this._folio.Text);
            ClickImprimir = true;

            this.Close();

        }

    }
}
