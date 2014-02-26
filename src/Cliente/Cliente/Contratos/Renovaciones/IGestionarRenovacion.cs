using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Renovaciones
{
    interface IGestionarRenovacion : IPaginaBase
    {
        object Renovacion { get; set; }

        string IdAsociadoFiltrar { get; }

        string NombreAsociadoFiltrar { get; }

        string IdMarca { set; get; }

        string IdInteresado { set; get; }

        string IdAgente { set; get; }

        string PeriodoDeGracia { set; get; }

        void BorrarCeros();

        void EsMarcaNacional(bool valor);
        string TipoClase { set; }

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

        #endregion

        #region Agente

        object Agente { get; set; }

        string NombreAgente { set; }

        string IdAgenteFiltrar { get; }

        string NombreAgenteFiltrar { get; }

        object AgentesFiltrados { get; set; }

        object AgenteFiltrado { get; set; }

        #endregion

        #region Poder

        object Poder { get; set; }

        string IdPoder { set; get; }

        string NumPoder { get; set; }

        string Tipo { set; get; }

        string IdPoderFiltrar { get; set; }

        string FechaPoderFiltrar { get; }

        object PoderesFiltrados { get; set; }

        object PoderFiltrado { get; set; }

        DatePicker FechaRenovacion { get; set; }

        #endregion

        bool HabilitarCampos { set; }

        string Region { get; set; }

        string TextoBotonRegresar { get; set; }

        string Otros { get; set; }

        object TiposRenovaciones { get; set; }

        string ProximaRenovacion { get; set; }

        object TipoRenovacion { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ConvertirEnteroMinimoABlanco();

        void GestionarBotonConsultarInteresado(bool value);

        void GestionarBotonConsultarAgente(bool value);

        void GestionarBotonConsultarPoder(bool value);

        void ActivarControlesAlAgregar();

        void PintarAsociado(string tipo);

        void BorrarCerosInternacional();

        void MostrarBotonNuevaRenovacion();

        void HabilitarBotonNuevaRenovacion();

    }
}
