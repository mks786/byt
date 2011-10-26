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
    public partial class ChildWindow : Form
    {
        ByTTextBox _textBox;
        public ChildWindow(ByTTextBox descripcion)
        {
            InitializeComponent();
            this._textBox = descripcion;
            this._detalle.Text = this._textBox.Text;
            this._detalle.AcceptsReturn = true;
            this._detalle.ScrollBars = ScrollBars.Vertical;
            this._detalle.WordWrap = true;
        }

        private void _btnAceptar_Click(object sender, EventArgs e)
        {
            this._textBox.Text = this._detalle.Text;
            this.Close();
        }

        private void ChildWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
