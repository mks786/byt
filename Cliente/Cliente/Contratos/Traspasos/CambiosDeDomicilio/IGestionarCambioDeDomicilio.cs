using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Traspasos.CambiosDeDomicilio
{
    interface IGestionarCambioDeDomicilio : IPaginaBase
    {
        object CambioDeDomicilio { get; set; }

        string Fecha { get; }

        string IdAsociadoFiltrar { get; }

        string NombreAsociadoFiltrar { get; }

        string IdAgenteFiltrar { get; }

        string NombreAgenteFiltrar { get; }

        #region Marcas

        string NombreMarca { set; }

        string IdMarca { get; set; }

        string TipoClaseNacional { get; set; }

        object Marca { get; set; }

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object MarcasFiltradas { get; set; }

        object MarcaFiltrada { get; set; }

        #endregion

        #region InteresadoAnterior

        object InteresadoAnterior { get; set; }

        string NombreInteresadoAnterior { set; }

        string IdInteresadoAnterior { get; set; }

        string IdInteresadoAnteriorFiltrar { get; }

        string NombreInteresadoAnteriorFiltrar { get; }

        object InteresadosAnteriorFiltrados { get; set; }

        object InteresadoAnteriorFiltrado { get; set; }

        #endregion

        #region InteresadoActual

        object InteresadoActual { get; set; }

        string NombreInteresadoActual { set; }

        string IdInteresadoActual { get; set; }

        string IdInteresadoActualFiltrar { get; }

        string NombreInteresadoActualFiltrar { get; }

        object InteresadosActualFiltrados { get; set; }

        object InteresadoActualFiltrado { get; set; }

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

        void GestionarBotonConsultarInteresado(bool value);

        void GestionarBotonConsultarApoderado(bool value);

        void GestionarBotonConsultarPoder(bool value);

        void ActivarControlesAlAgregar();

        void PintarAsociado(string tipo);
    }
}
