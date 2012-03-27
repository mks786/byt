using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IConsultarMarca : IPaginaBase
    {
        object Marca { get; set; }

        string IdAsociadoSolicitudFiltrar { get; }

        string IdAsociadoSolicitud { get;  set; }

        string IdAsociadoDatosFiltrar { get; }

        string IdAsociadoDatos { set; }

        string NombreAsociadoSolicitudFiltrar { get; }

        string NombreAsociadoDatosFiltrar { get; }

        string NombreAsociadoSolicitud { get; set; }

        string NombreAsociadoDatos { get; set; }

        string IdInteresadoSolicitudFiltrar { get; }

        string IdInteresadoSolicitud { set; }

        string IdInteresadoDatosFiltrar { get; }

        string IdInteresadoDatos { set; }

        string NombreInteresadoSolicitudFiltrar { get; }

        string NombreInteresadoDatosFiltrar { get; }

        string NombreInteresadoSolicitud { get; set; }

        string NombreInteresadoDatos { get; set; }

        string NumPoderDatos { get; set; }

        string NumPoderSolicitud { get; set; }

        string IdCorresponsalSolicitudFiltrar { get; }

        string IdCorresponsalDatosFiltrar { get; }

        string DescripcionCorresponsalSolicitudFiltrar { get; }

        string DescripcionCorresponsalDatosFiltrar { get; }

        string DescripcionCorresponsalSolicitud { get; set; }

        string DescripcionCorresponsalDatos { get; set; }

        object AsociadosSolicitud { get; set; }

        object AsociadoSolicitud { get; set; }

        object AsociadosDatos { get; set; }

        object AsociadoDatos { get; set; }

        object InteresadosSolicitud { get; set; }

        object InteresadoSolicitud { get; set; }

        object InteresadosDatos { get; set; }

        object InteresadoDatos { get; set; }

        string InteresadoPaisSolicitud { get; set; }

        string InteresadoCiudadSolicitud { get; set; }

        object CorresponsalesSolicitud { get; set; }

        object CorresponsalSolicitud { get; set; }

        object CorresponsalesDatos { get; set; }

        object CorresponsalDatos { get; set; }

        object PoderesSolicitud { get; set; }

        object PoderSolicitud { get; set; }

        object PoderesDatos { get; set; }

        object PoderDatos { get; set; }

        object Agentes { get; set; }

        object Agente { get; set; }

        object BoletinesOrdenPublicacion { get; set; }

        object BoletinOrdenPublicacion { get; set; }

        object BoletinesPublicacion { get; set; }

        object BoletinPublicacion { get; set; }

        object BoletinesConcesion { get; set; }

        object BoletinConcesion { get; set; }

        object Servicios { get; set; }

        object Servicio { get; set; }

        object Detalle { get; set; }

        object Detalles { get; set; }

        object Condiciones { get; set; }

        object Condicion { get; set; }

        object PaisesSolicitud { get; set; }

        object PaisSolicitud { get; set; }

        object TipoMarcaSolicitud { get; set; }

        object TipoMarcasSolicitud { get; set; }

        object TipoMarcaDatos { get; set; }

        object TipoMarcasDatos { get; set; }

        object TipoReproduccion { get; set; }

        object TipoReproducciones { get; set; }

        object Sector { get; set; }

        object Sectores { get; set; }

        object StatusWeb { get; set; }

        object StatusWebs { get; set; }

        void Mensaje(string mensaje);

        string TextoBotonModificar { get; set; }

        string IdInternacional { get; set; }

        string IdNacional { get; set; }

        bool HabilitarCampos { set; }

        bool AsociadosEstanCargados { get; set; }

        bool InteresadosEstanCargados { get; set; }

        bool CorresponsalesEstanCargados { get; set; }

        bool PoderesEstanCargados { get; set; }

        void PintarInfoAdicional();

        void PintarAnaqua();

        void PintarInfoBoles();

        void PintarOperaciones();

        void PintarBusquedas();

        void PintarAuditoria();

        void BorrarCeros();

        bool MensajeAlerta(string mensaje);

        void ArchivoNoEncontrado();

        string ClaseInternacional { get; }

        string ClaseNacional { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

    }
}
