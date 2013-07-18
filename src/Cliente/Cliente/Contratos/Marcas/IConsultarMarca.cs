using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IConsultarMarca : IPaginaBase
    {
        object Marca { get; set; }

        bool MarcaOrigenCargada { get; set; }

        string IdAsociadoSolicitudFiltrar { get; }

        string IdAsociadoSolicitud { get;  set; }

        string IdAsociadoDatosFiltrar { get; }

        string IdAsociadoDatos { get; set; }

        string SituacionDescripcion { set; }

        string DetalleDescripcion { set; }

        string NombreAsociadoSolicitudFiltrar { get; }

        string NombreAsociadoDatosFiltrar { get; }

        string NombreAsociadoSolicitud { get; set; }

        string NombreAsociadoDatos { get; set; }

        string IdInteresadoSolicitudFiltrar { get; }

        string IdInteresadoSolicitud { get; set; }

        string IdInteresadoDatosFiltrar { get; }

        string IdInteresadoDatos { set; }

        string NombreInteresadoSolicitudFiltrar { get; }

        string NombreInteresadoDatosFiltrar { get; }

        string NombreInteresadoSolicitud { get; set; }

        string NombreInteresadoDatos { get; set; }

        string NumPoderDatos { get; set; }

        //string NumPoderSolicitud { get; set; }

        string IdPoderDatos { get; set; }

        string IdPoderSolicitud { get; set; }

        string IdCorresponsalSolicitudFiltrar { get; }

        string IdCorresponsalSolicitud { get; set; }

        string IdCartaOrdenSolicitud { get; set; }

        object CartasOrdenSolicitud { get; set; }

        object CartaOrdenSolicitud { get; set; }

        string IdCartaOrdenSolicitudFiltrar { get; set; }

        string DescripcionCartaOrdenSolicitudFiltrar { get; set; }

        string IdCartaOrdenDatos { get; set; }

        object CartasOrdenDatos { get; set; }

        object CartaOrdenDatos { get; set; }

        string IdCartaOrdenDatosFiltrar { get; set; }

        string DescripcionCartaOrdenDatosFiltrar { get; set; }

        string IdCorresponsalDatosFiltrar { get; }

        string IdCorresponsalDatos { get; set; }

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

        object TiposClaseNacional { get; set; }

        object TipoClaseNacional { get; set; }

        void Mensaje(string mensaje);

        string TextoBotonModificar { get; set; }

        string IdInternacional { get; set; }

        string IdNacional { get; set; }

        bool HabilitarCampos { set; }

        bool AsociadosEstanCargados { get; set; }

        bool InteresadosEstanCargados { get; set; }

        bool CorresponsalesEstanCargados { get; set; }

        bool PoderesEstanCargados { get; set; }

        void DeshabilitarBotonModificar();

        void PintarInfoAdicional();

        void PintarAnaqua();

        void PintarInfoBoles();

        void PintarOperaciones();

        void PintarBusquedas();

        void PintarAuditoria();

        void PintarArchivo();

        void PintarRenovacion();

        void PintarInstRenovacion();

        void PintarLblMarcaOrigen(bool confirmacion);

        void BorrarCeros();

        void PintarEtiqueta();

        void PintarAsociado(string tipo);

        bool MensajeAlerta(string mensaje);

        void ArchivoNoEncontrado(string mensaje);

        string ClaseInternacional { get; }

        string ClaseNacional { get; }

        string DistingueSolicitud { get;  set; }

        string DistingueDatos { get;  set; }
        
        void ConvertirEnteroMinimoABlanco();

        void Mensaje(string tipo, int mensaje);

        void mostrarLstPoderSolicitud();

        void BloquearModificacion();

        string IdAsociadoInternacionalFiltrar { get; set; }

        string NombreAsociadoInternacionalFiltrar { get; set; }

        object AsociadosInternacionales { get; set; }

        object AsociadoInternacional { get; set; }

        string IdAsociadoInternacionalFiltrarDatos { get; set; }

        string NombreAsociadoInternacionalFiltrarDatos { get; set; }

        object AsociadosInternacionalesDatos { get; set; }

        object AsociadoInternacionalDatos { get; set; }

        string TextoAsociadoInternacional { set; }

        object PaisesInternacionales { get; set; }

        object PaisInternacional { get; set; }

        object PaisesInternacionalesDatos { get; set; }

        object PaisInternacionalDatos { get; set; }

        object TipoClaseInternacionales { get; set; }

        object TipoClaseInternacional { get; set; }

        object TipoClaseInternacionalesDatos { get; set; }

        object TipoClaseInternacionalDatos { get; set; }

        bool EsMarcaNacional { get; }

        void MarcarRadioMarcaNacional(bool esNacional);

        string ClaseInternacionalMarca { get; }

        string SaldoVencidoSolicitud { set; }

        string SaldoPorVencerSolicitud { set; }

        string TotalSolicitud { set; }
        

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        string IdMarcaOrigenSolicitud { get; set; }

        string IdMarcaOrigenDatos { get; set; }

        string IdMarcaOrigenSolicitudFiltrar { get; set; }

        string IdMarcaOrigenDatosFiltrar { get; set; }

        object MarcaOrigenSolicitud { get; set; }

        object MarcasOrigenSolicitud { get; set; }

        object MarcaOrigenDatos { get; set; }

        object MarcasOrigenDatos { get; set; }

        string IdExpTraspasoRenovacionDatos { get; set; }

        string IdExpTraspasoRenovacionSolicitud { get; set; }

        void PintarCertificado();

    }
}
