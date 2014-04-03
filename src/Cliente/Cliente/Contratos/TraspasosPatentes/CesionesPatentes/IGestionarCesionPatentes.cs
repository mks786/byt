using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.CesionesPatentes
{
    interface IGestionarCesionPatentes : IPaginaBase
    {

        object CesionPatente { get; set; }

        void BorrarCerosInternacional();

        string Tipo { set; get; }

        string Expediente { get; set; }

        string Ubicacion { get; set; }

        #region Patente

        object Patente { get; set; }

        string NombrePatente { set; }

        string IdPatente { get; set; }

        string IdPatenteFiltrar { get; }

        string NombrePatenteFiltrar { get; }

        object PatentesFiltradas { get; set; }

        object PatenteFiltrada { get; set; }      

        #endregion

        #region Cedente

        string NombreCedente { set; }

        string IdCedente { get;  set; }

        object InteresadoCedente { get; set; }

        string PaisCedente { set; }

        string NacionalidadCedente { set; }

        string IdCedenteFiltrar { get; }

        string NombreCedenteFiltrar { get; }

        object CedentesFiltrados { get; set; }

        object CedenteFiltrado { get; set; }

        string NombreApoderadoCedente { set; }

        string IdApoderadoCedente { get; set; }

        object ApoderadoCedente { get; set; }

        string IdApoderadoCedenteFiltrar { get; }

        string NombreApoderadoCedenteFiltrar { get; }

        object ApoderadosCedenteFiltrados { get; set; }

        object ApoderadoCedenteFiltrado { get; set; }

        string IdPoderCedente { set; get; }

        object PoderCedente { get; set; }

        string IdPoderCedenteFiltrar { get; }

        string FechaPoderCedenteFiltrar { get; }

        object PoderesCedenteFiltrados { get; set; }

        object PoderCedenteFiltrado { get; set; }

        #endregion

        #region Cesionario

        string NombreCesionario { set; }

        string IdCesionario { get; set; }

        object InteresadoCesionario { get; set; }

        string PaisCesionario { set; }

        string NacionalidadCesionario { set; }

        string IdCesionarioFiltrar { get; }

        string NombreCesionarioFiltrar { get; }

        object CesionariosFiltrados { get; set; }

        object CesionarioFiltrado { get; set; }

        string NombreApoderadoCesionario { set; }

        string IdApoderadoCesionario { get; set; }

        object ApoderadoCesionario { get; set; }

        string IdApoderadoCesionarioFiltrar { get; }

        string NombreApoderadoCesionarioFiltrar { get; }

        object ApoderadosCesionarioFiltrados { get; set; }

        object ApoderadoCesionarioFiltrado { get; set; }

        string IdPoderCesionario { set; get; }

        object PoderCesionario { get; set; }

        string IdPoderCesionarioFiltrar { get; }

        string FechaPoderCesionarioFiltrar { get; }

        object PoderesCesionarioFiltrados { get; set; }

        object PoderCesionarioFiltrado { get; set; }

        #endregion

        bool HabilitarCampos { set; }

        string Region { get; set; }

        string TextoBotonModificar { get; set; }

        string TextoBotonRegresar { get; set; }

        object Boletines { get; set; }

        object Boletin { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ConvertirEnteroMinimoABlanco();

        void GestionarBotonConsultarInteresados(string tipo, bool value);
        
        void GestionarBotonConsultarApoderados(string tipo, bool value);
        
        void GestionarBotonConsultarPoderes(string tipo, bool value);

        void ActivarControlesAlAgregar();

        void PintarAsociado(string tipo);

        void EsPatenteNacional(bool marcaNacional);
    }
}
