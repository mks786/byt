using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Trascend.Bolet.ControlesByT.Ventanas;

namespace Trascend.Bolet.ControlesByT
{
    public class ByTTextBox : TextBox
    {

        private bool _soloNumero;
        private bool _soloPorcentaje;


        #region Propiedades

        public int TamanoMaximo
        {
            get { return this.MaxLength; }
            set { this.MaxLength = value; }
        }

        public bool SoloNumero
        {
            get { return _soloNumero; }
            set { _soloNumero = value; }
        }

        public bool SoloPorcentaje
        {
            get { return _soloPorcentaje; }
            set { _soloPorcentaje = value; }
        }

        #endregion

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F5)
            {
                ChildWindow _ventana = new ChildWindow(this);
                _ventana.ShowDialog();
            }
            if (SoloNumero)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
                    e.Handled = true;
            }
            if (SoloPorcentaje)
            {
                if ((!this.Text.Contains(".")) || ((!e.Key.ToString().Equals("Decimal")) && (!e.Key.ToString().Equals("OemPeriod"))))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "[-+]?[0-9]*\\.?[0-9]+"))
                        if ((!e.Key.ToString().Equals("Decimal")) && (!e.Key.ToString().Equals("OemPeriod")))
                            e.Handled = true;
                }
                else
                    e.Handled = true;
            }
            base.OnKeyDown(e);
        }
    }
}
