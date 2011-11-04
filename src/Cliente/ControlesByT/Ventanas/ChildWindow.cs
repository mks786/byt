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
            this._detalle.MaxLength = descripcion.TamanoMaximo;
            this._detalle.ScrollBars = ScrollBars.Vertical;
        }

        private void _btnAceptar_Click(object sender, EventArgs e)
        {
            this._textBox.Text = this._detalle.Text;
            this.Close();
        }

        private void _detalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this._textBox.SoloNumero)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                    if (!e.KeyChar.Equals('\b'))
                        e.Handled = true;
            }
            if (this._textBox.SoloPorcentaje)
            {
                if ((!this._detalle.Text.Contains(".")) || ((!e.KeyChar.ToString().Equals("."))))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[-+]?[0-9]*\\.?[0-9]+"))
                        if ((!e.KeyChar.ToString().Equals(".")) && (!e.KeyChar.Equals('\b')))
                            e.Handled = true;
                }
                else
                    e.Handled = true;
            }
            base.OnKeyPress(e);
        }

    }
}
