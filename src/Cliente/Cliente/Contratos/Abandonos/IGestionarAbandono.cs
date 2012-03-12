using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Abandonos
{
    interface IGestionarAbandono : IPaginaBase
    {
        object Operacion { get; set; }

        #region Marcas

        string NombreMarca { set; }

        object Marca { get; set; }

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object MarcasFiltradas { get; set; }

        object MarcaFiltrada { get; set; }

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
    }
}
