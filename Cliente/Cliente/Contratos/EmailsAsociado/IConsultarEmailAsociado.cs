
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.EmailsAsociado
{
    interface IConsultarEmailAsociado : IPaginaBase
    {
        void Mensaje(string mensaje);

        object EmailAsociado { get; set; }

        object Departamento { get; set; }

        object Departamentos { get; set; }

        object TiposEmail { get; set; }

        object TipoEmail { get; set; }

        string Funcion { set; }

        string Descripcion { set; }

        void PintarAuditoria();

        void MostrarBotones();

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }
    }
}
