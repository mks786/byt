using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.AbandonosPatente
{
    interface IGestionarAbandonoPatente : IPaginaBase
    {
        object Operacion { get; set; }

        #region Patentes

        string NombrePatente { set; }

        object Patente { get; set; }

        string IdPatenteFiltrar { get; }

        string NombrePatenteFiltrar { get; }

        object PatentesFiltradas { get; set; }

        object PatenteFiltrada { get; set; }

        #endregion       

        #region Interesado

        object Interesado { get; set; }

        string NombreInteresado { set; }

        string IdInteresadoFiltrar { get; }

        string NombreInteresadoFiltrar { get; }

        object InteresadosFiltrados { get; set; }

        object InteresadoFiltrado { get; set; }

        void GestionarBotonConsultarInteresado(bool value);

        #endregion

        #region Asociado

        object Asociado { get; set; }

        string NombreAsociado { set; }

        string IdAsociadoFiltrar { get; }

        string NombreAsociadoFiltrar { get; }

        object AsociadosFiltrados { get; set; }

        object AsociadoFiltrado { get; set; }

        #endregion

        bool HabilitarCampos { set; }

        object Boletines { get; set; }

        object Boletin { get; set; }

        string Aplicada { get; }

        string Region { get; set; }

        string TextoBotonRegresar { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void Mensaje(string mensaje, int opcion);
      
        void ActivarControlesAlAgregar();

        void PintarAsociado(string tipo);
    }
}
