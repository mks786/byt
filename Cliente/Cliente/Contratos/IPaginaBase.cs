using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos
{
    public interface IPaginaBase
    {
        bool EstaCargada { get; set; }

        void FocoPredeterminado();
    }
}
