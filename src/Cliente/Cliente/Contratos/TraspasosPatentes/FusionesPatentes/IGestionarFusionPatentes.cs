using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.FusionesPatentes
{
    interface IGestionarFusionPatentes : IPaginaBase
    {
        object FusionPatente { get; set; }

        string IdAsociadoFiltrar { get; }

        string Expediente { get; set; }

        string Tipo { get; set; }

        string NombreAsociadoFiltrar { get; }

        string IdAgenteFiltrar { get; }

        string NombreAgenteFiltrar { get; }

        #region Patentes

        string NombrePatente { set; }

        string IdPatente { get; set; }

        object Patente { get; set; }

        string IdPatenteFiltrar { get; }

        string NombrePatenteFiltrar { get; }

        object PatentesFiltradas { get; set; }

        object PatenteFiltrada { get; set; }

        #endregion

        #region InteresadoEntre

        object InteresadoEntre { get; set; }

        string NombreInteresadoEntre { set; }

        string IdInteresadoEntre { get; set; }

        string IdInteresadoEntreFiltrar { get; }

        string NombreInteresadoEntreFiltrar { get; }

        object InteresadosEntreFiltrados { get; set; }

        object InteresadoEntreFiltrado { get; set; }

        #endregion

        #region InteresadoSobreviviente

        object InteresadoSobreviviente { get; set; }

        string NombreInteresadoSobreviviente { set; }

        string IdInteresadoSobreviviente { get; set; }

        string IdInteresadoSobrevivienteFiltrar { get; }

        string NombreInteresadoSobrevivienteFiltrar { get; }

        object InteresadosSobrevivienteFiltrados { get; set; }

        object InteresadoSobrevivienteFiltrado { get; set; }

        #endregion

        #region AgenteApoderado

        object AgenteApoderado { get; set; }

        string NombreAgenteApoderado { set; }

        string IdAgenteApoderado { get; set; }

        string IdAgenteApoderadoFiltrar { get; }

        string NombreAgenteApoderadoFiltrar { get; }

        object AgenteApoderadoFiltrados { get; set; }

        object AgenteApoderadoFiltrado { get; set; }

        #endregion

        #region Poder

        object Poder { get; set; }

        string IdPoder { set; get; }       

        string IdPoderFiltrar { get; }

        string FechaPoderFiltrar { get; }

        object PoderesFiltrados { get; set; }

        object PoderFiltrado { get; set; }
        
        #endregion

        object Boletines { get; set; }

        object Boletin { get; set; }

        bool HabilitarCampos { set; }

        string Region { get; set; }

        string TextoBotonModificar { get; set; }

        string TextoBotonRegresar { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ConvertirEnteroMinimoABlanco();

        void GestionarBotonConsultarInteresado(bool value);

        void GestionarBotonConsultarApoderado(bool value);

        void GestionarBotonConsultarPoder(bool value);

        void ActivarControlesAlAgregar();

        void PintarAsociado(string tipo);

        object Corporaciones { get; set; }

        object Corporacion { get; set; }

        string NombrePatenteTercero { get; set; }

        string DomicilioPatenteTercero { get; set; }

        object NacionalidadPatenteTercero { get; set; }

        object PaisPatenteTercero { get; set; }

        object NacionalidadesPatenteTercero { get; set; }

        object PaisesPatenteTercero { get; set; }

        void EsPatenteNacional(bool marcaNacional);
    }
}
