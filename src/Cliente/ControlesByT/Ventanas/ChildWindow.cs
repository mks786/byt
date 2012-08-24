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
            this._detalle.ReadOnly = this._textBox.IsReadOnly;
            if (this._detalle.Text.Contains("\r"))
            {
                this._detalle.Text = this._detalle.Text.Replace("\r", Environment.NewLine);
            }
            else if (this._detalle.Text.Contains("\n"))
            {
                this._detalle.Text = this._detalle.Text.Replace("\n", Environment.NewLine);
            }

            
            this._detalle.SelectionStart = this._detalle.Text.Length;
            this._detalle.SelectionLength = 0;
        }

        public ChildWindow(string descripcion)
        {
            InitializeComponent();

            this._detalle.Text = descripcion;
            this._detalle.AcceptsReturn = true;
            //this._detalle.MaxLength = descripcion.TamanoMaximo;
            this._detalle.ScrollBars = ScrollBars.Vertical;
            this._detalle.ReadOnly = true;

            this._detalle.SelectionStart = this._detalle.Text.Length;
            this._detalle.SelectionLength = 0;
        }

        private void _btnAceptar_Click(object sender, EventArgs e)
        {
            if (null != this._textBox)
            {
                if (this._detalle.Text.Contains("\r"))
                {
                    this._textBox.Text = this._detalle.Text.Replace(Environment.NewLine, "\r");
                }
                else if (this._detalle.Text.Contains("\n"))
                {
                    this._textBox.Text = this._detalle.Text.Replace(Environment.NewLine, "\n");
                }
                else
                    this._textBox.Text = this._detalle.Text;
            }
            this.Close();
        }

        private void _detalle_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((null != this._textBox) && (this._textBox.SoloNumero))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                    if (!e.KeyChar.Equals('\b'))
                        e.Handled = true;
            }
            if ((null != this._textBox) && (this._textBox.SoloPorcentaje))
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
