using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Trascend.Bolet.ControlesByT.Ventanas;

namespace Trascend.Bolet.ControlesByT
{
    /// <summary>
    /// TextBox personalizado con la funcionalidad F5, esta funcionalidad
    /// consiste que al presionar la tecla "F5" se crea un PopUp donde se
    /// puede visualizar y editar el texto de una mejor manera
    /// </summary>
    public class ByTTextBox : TextBox
    {

        private bool _soloNumero;
        private bool _soloPorcentaje;
        private int _tamanoMaximo;

        #region Propiedades

        /// <summary>
        /// Propiedad que obtiene y setea el tamaño maximo de el
        /// campo de texto
        /// </summary>
        public int TamanoMaximo
        {
            get { return this.MaxLength; }
            set { this.MaxLength = value; }
        }

        /// <summary>
        /// Propiedad que determina si el campo es de solo numero
        /// </summary>
        public bool SoloNumero
        {
            get { return _soloNumero; }
            set { _soloNumero = value; }
        }

        /// <summary>
        /// Propiedad que determina si el campo solo acepta Float
        /// </summary>
        public bool SoloPorcentaje
        {
            get { return _soloPorcentaje; }
            set { _soloPorcentaje = value; }
        }

        #endregion

        /// <summary>
        /// Sobreescritura del metodo OnKeyDown. En este metodo se implementa la funcionalidad del
        /// PopUp
        /// </summary>
        /// <param name="e"></param>
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
