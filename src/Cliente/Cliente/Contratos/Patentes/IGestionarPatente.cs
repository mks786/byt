using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Patentes
{
    interface IGestionarPatente : IPaginaBase
    {
        object Patente { get; set; }

        bool PatenteMadreCargada { get; set; }

        #region Solicitud

        string IdAsociadoSolicitudFiltrar { get; }

        string IdAsociadoSolicitud { get;  set; }

        string NombreAsociadoSolicitudFiltrar { get; }

        string NombreAsociadoSolicitud { get; set; }

        string IdInteresadoSolicitudFiltrar { get; }

        string IdInteresadoSolicitud { get; set; }

        string NombreInteresadoSolicitudFiltrar { get; }

        string NombreInteresadoSolicitud { get; set; }

        string NumPoderSolicitud { get; set; }

        string PoderSolicitud { get; set; }

        object PoderesSolicitudFiltrar { get; set; }
        
        object PoderSolicitudFiltrar { get; set; }

        object AsociadosSolicitud { get; set; }

        object AsociadoSolicitud { get; set; }

        object InteresadosSolicitud { get; set; }

        object InteresadoSolicitud { get; set; }

        string InteresadoPaisSolicitud { get; set; }

        string InteresadoEstadoSolicitud { get; set; }

        string IdAgenteSolicitud { get; set; }

        string AgenteSolicitud { get; set; }

        object AgentesSolicitudFiltrar { get; set; }

        object AgenteSolicitudFiltrar { get; set; }

        object PaisesSolicitud { get; set; }

        object PaisSolicitud { get; set; }

        object TipoPatenteSolicitud { get; set; }

        object TiposPatenteSolicitud { get; set; }

        object PresentacionesPatenteSolicitud { get; set; }

        object PresentacionPatenteSolicitud { get; set; }

        void PintarDocumentosSolicitud();

        void PintarCasoEspecialSolicitud();

        void PintarDisenoSolicitud();

        //void PintarDisenoReporteSolicitud();

        void PintarInventoresSolicitud();

        void PintarInfoAdicionalSolicitud();

        void PintarImprimirEdoDeCuenta();

        void PintarSaldos();

        void PintarLblPatenteMadre(bool confirmacion);

        string IdAgenteSolicitudFiltrar { get; set; }

        string NombreAgenteSolicitudFiltrar { get; set; }

        #endregion

        #region Datos

        string IdAsociadoDatosFiltrar { get; }

        string IdAsociadoDatos { get; set; }

        string NombreAsociadoDatosFiltrar { get; }

        string NombreAsociadoDatos { get; set; }

        string IdInteresadoDatosFiltrar { get; }

        string IdInteresadoDatos { get; set; }

        string NombreInteresadoDatosFiltrar { get; }

        string NombreInteresadoDatos { get; set; }

        string PoderDatos { get; set; }

        object PoderesDatosFiltrar { get; set; }

        object PoderDatosFiltrar { get; set; }

        object AsociadosDatos { get; set; }

        object AsociadoDatos { get; set; }

        object InteresadosDatos { get; set; }

        object InteresadoDatos { get; set; }

        string InteresadoPaisDatos { get; set; }

        string InteresadoEstadoDatos { get; set; }

        object PaisesDatos { get; set; }

        object PaisDatos { get; set; }

        object TipoPatenteDatos { get; set; }

        object TiposPatenteDatos { get; set; }

        object PresentacionesPatenteDatos { get; set; }

        object PresentacionPatenteDatos { get; set; }

        object StatusesWebDatos { get; set; }

        object StatusWebDatos { get; set; }

        object DetallesDatos { get; set; }

        object DetalleDatos { get; set; }

        string AbandonoDatos { get; set; }
        
        string AnualidadDatos { get; set; }

        object BoletinesOrdenPublicacionDatos { get; set; }

        object BoletinOrdenPublicacionDatos { get; set; }

        object BoletinesPublicacionDatos { get; set; }

        object BoletinPublicacionDatos { get; set; }

        object BoletinesConcesionDatos { get; set; }

        object BoletinConcesionDatos { get; set; }

        object SituacionesDatos { get; set; }

        object SituacionDatos { get; set; }

        string FechaTermino { get; set; }

        string Duracion { get; set; }

        void PintarAsociado(string tipo);

        void PintarDisenoDatos();

        void PintarInventoresDatos();

        void PintarAnualidadesDatos();

        void PintarInfoBolDatos();

        void PintarAuditoriaDatos();

        void PintarMemoriaDatos();

        void PintarFechasDatos();

        void PintarOperacionesDatos();

        #endregion

        void OcultarTabSolicitud();

        void SeleccionarTabSolicitud();

        void Mensaje(string mensaje);

        bool HabilitarCampos { set; }

        bool AsociadosEstanCargados { get; set; }

        bool InteresadosEstanCargados { get; set; }

        bool CorresponsalesEstanCargados { get; set; }

        bool PoderesEstanCargados { get; set; }

        bool AgentesEstanCargados { get; set; }

        string TextoBotonModificar { get; set; }

        string TextoBotonRegresar { get; set; }

        string SituacionDescripcion { set; }

        bool MensajeAlerta(string mensaje);

        void Mensaje(string mensaje, int opcion);

        void ArchivoNoEncontrado();

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void ActivarControlesAlAgregar();

        void ConvertirEnteroMinimoABlanco();

        void MostrarLstPoderSolicitud();

        bool ConfirmarAccion(string Titulo, string Mensaje);

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

        bool EsPatenteNacional { get; }

        void MarcarRadioPatenteNacional(bool esNacional);

        string SaldoVencidoSolicitud { set; }

        string SaldoVencidoDatos { set; }

        string SaldoPorVencerSolicitud { set; }

        string SaldoPorVencerDatos { set; }

        string TotalSolicitud { set; }

        string TotalDatos { set; }

        void BorrarCeros();

        void CambiarLabelsPorBotones();

        string IdPatenteMadreSolicitud { get; set; }

        string IdPatenteMadreDatos { get; set; }

        object PatenteMadreSolicitud { get; set; }

        object PatentesMadreSolicitud { get; set; }

        string IdPatenteMadreSolicitudFiltrar { get; set; }

        object PatenteMadreDatos { get; set; }

        object PatentesMadreDatos { get; set; }

        string IdPatenteMadreDatosFiltrar { get; set; }

        void PintarArchivo();

        void PintarBotonInteresadosDePatente(bool flag);

        void MostrarBotonInteresadosDePatente(bool flag);

        string DiasRecordatorioVencimiento { get; set; }

        void ActivarRecordatorioPresentacionPrioridad(bool flag);

        string FechaTopePresentacionPrioridad { get; set; }

        void PintarBotonMemoriaEspanol(bool flag);

        void PintarBotonMemoriaIngles(bool flag);

        void PintarFacturacion();

        bool ChkPrioridadPresentada { get; }

        void PintarDetalleMemorias();

        object OrigenPatentesSolicitud { get; set; }

        object OrigenPatenteSolicitud { get; set; }

        object OrigenPatentesDatos { get; set; }

        object OrigenPatenteDatos { get; set; }

    }
}
