using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Cartas
{
    interface IConsultarContactosPorAsociado : IPaginaBase
    {

        #region Asociado

        string AsociadoFiltrar { set; }

        string IdAsociado { get; set; }

        string NombreAsociado { get; set; }

        string TelefonoAsociado { get; set; }

        string FaxAsociado { get; set; }

        string DomicilioAsociado { get; set; }

        string WebAsociado { get; set; }

        string EmailAsociado { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        #endregion


        #region Contacto

        //string ContactoFiltrar { set; }

        string NombreContacto { get; set; }

        string TelefonoContacto { get; set; }

        string FaxContacto { get; set; }

        object Departamentos { get; set; }

        object Departamento { get; set; }

        string EmailContacto { get; set; }

        //object Contactos { get; set; }

        //object Contacto { get; set; }

        #endregion


        object Resultados { get; set; }

        object ContactoSeleccionado { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }

        void Mensaje(string mensaje, int opcion);
    }
}
