using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Traspasos.Fusiones
{
    interface IGestionarFusion : IPaginaBase
    {
        object Fusion { get; set; }

        string IdAsociadoFiltrar { get; }

        string NombreAsociadoFiltrar { get; }

        string IdAgenteFiltrar { get; }

        string NombreAgenteFiltrar { get; }

        string IdMarca { get; set; }

        string IdInteresadoEntre{ get; set; }

        string IdInteresadoSobreviviente { get; set; }

        string IdApoderado { get; set; }

        #region Marcas

        string NombreMarca { set; }

        object Marca { get; set; }

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object MarcasFiltradas { get; set; }

        object MarcaFiltrada { get; set; }

        #endregion

        #region InteresadoEntre

        object InteresadoEntre { get; set; }

        string NombreInteresadoEntre { set; }

        string IdInteresadoEntreFiltrar { get; }

        string NombreInteresadoEntreFiltrar { get; }

        object InteresadosEntreFiltrados { get; set; }

        object InteresadoEntreFiltrado { get; set; }

        #endregion

        #region InteresadoSobreviviente

        object InteresadoSobreviviente { get; set; }

        string NombreInteresadoSobreviviente { set; }

        string IdInteresadoSobrevivienteFiltrar { get; }

        string NombreInteresadoSobrevivienteFiltrar { get; }

        object InteresadosSobrevivienteFiltrados { get; set; }

        object InteresadoSobrevivienteFiltrado { get; set; }

        #endregion

        #region AgenteApoderado

        object AgenteApoderado { get; set; }

        string NombreAgenteApoderado { set; }

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
    }
}
