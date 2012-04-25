using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Anualidades
{
    interface IGestionarAnualidades : IPaginaBase
    {
        object Patente { get; set; }

        #region Patentes

        string NombrePatente { set; }

        //object Patente { get; set; }

        string IdPatenteFiltrar { get; }

        string NombrePatenteFiltrar { get; }

        object PatentesFiltradas { get; set; }

        object PatenteFiltrada { get; set; }

        #endregion

        #region Asociados
        //string IdAsociadoSolicitudFiltrar { get; }

        //string IdAsociadoSolicitud { set; }

        //string NombreAsociadoSolicitudFiltrar { get; }

        //string NombreAsociadoSolicitud { get; set; }

        //object AsociadosSolicitud { get; set; }

        //object AsociadoSolicitud { get; set; }
        #endregion

        bool HabilitarCampos { set; }

        string Region { get; set; }

        string TextoBotonModificar { get; set; }

        string TextoBotonRegresar { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ActivarControlesAlAgregar();

      //  bool AsociadosEstanCargados { get; set; }
    }
}
