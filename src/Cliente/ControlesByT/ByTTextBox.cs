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
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F5)
            {
                if (!string.Equals("", this.Text))
                {
                    ChildWindow _ventana = new ChildWindow(this);
                    _ventana.ShowDialog();
                }
            }
            base.OnKeyDown(e);
        }
    }
}
