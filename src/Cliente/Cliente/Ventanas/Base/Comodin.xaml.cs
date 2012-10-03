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
using System.Windows.Shapes;

namespace Trascend.Bolet.Cliente.Ventanas.Base
{
    /// <summary>
    /// Interaction logic for Comodin.xaml
    /// </summary>
    public partial class Comodin : Window
    {
        public Comodin(Page pagina)
        {
            InitializeComponent();

            this.AddChild(pagina);
        }
    }
}
