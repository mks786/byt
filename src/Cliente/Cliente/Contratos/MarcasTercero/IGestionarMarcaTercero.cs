using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.MarcasTercero
{
    interface IGestionarMarcaTercero : IPaginaBase
    {
        object MarcaTercero { get; set; }

        string IdAsociadoSolicitudFiltrar { get; }

        string IdMarcaTercero { get; set; }

        string IdInteresado { get; set; }

        string IdAsociado { get; set; }

        object Marca { get; set; }

        object GridByt { get; set; }

        object TiposCbx { get; set; }

        object TipoCbx { get; set; }

        object TipoDeCaso { get; set; }

        object TiposDeCasos { get; set; }

        object MarcasFiltradas { get; set; }

        object MarcaFiltrada { get; set; }

        string TipoBaseTxt { set; get; }

        string NombreMarcaFiltrar { get; }

        string IdMarcaFiltrar { get; }

        string NombreMarca { set; get; }

        string FechaRenovacion { set; get; }

        string FechaPublicacion { set; get; }

        string Caso { set; get; }

        string SituacionDescripcion { set; }

        string CNacional { set; get; }

        string CInternacional { set; get; }

        string Anexo { set; get; }

        //string IdAsociadoDatosFiltrar { get; }

        string NombreAsociadoSolicitudFiltrar { get; }

        //string NombreAsociadoDatosFiltrar { get; }

        string NombreAsociadoSolicitud { get; set; }

        //string NombreAsociadoDatos { get; set; }

        string IdInteresadoSolicitudFiltrar { get; }

        //string IdInteresadoDatosFiltrar { get; }

        string NombreInteresadoSolicitudFiltrar { get; }

        //string NombreInteresadoDatosFiltrar { get; }

        string NombreInteresadoSolicitud { get; set; }

        //string NombreInteresadoDatos { get; set; }

        //string NumPoderDatos { get; set; }


        //string IdCorresponsalSolicitudFiltrar { get; }

        //string IdCorresponsalDatosFiltrar { get; }

        //string DescripcionCorresponsalSolicitudFiltrar { get; }

        //string DescripcionCorresponsalDatosFiltrar { get; }

        //string DescripcionCorresponsalSolicitud { get; set; }

        //string DescripcionCorresponsalDatos { get; set; }

        object AsociadosSolicitud { get; set; }

        object AsociadoSolicitud { get; set; }

        //object AsociadosDatos { get; set; }

        //object AsociadoDatos { get; set; }

        object InteresadosSolicitud { get; set; }

        object InteresadoSolicitud { get; set; }

        //object InteresadosDatos { get; set; }

        //object InteresadoDatos { get; set; }

        string InteresadoPaisSolicitud { get; set; }

        string InteresadoCiudadSolicitud { get; set; }

        //object CorresponsalesSolicitud { get; set; }

        //object CorresponsalSolicitud { get; set; }

        //object CorresponsalesDatos { get; set; }

        //object CorresponsalDatos { get; set; }



        //object PoderesDatos { get; set; }

        //object PoderDatos { get; set; }


        object BoletinesOrdenPublicacion { get; set; }

        object BoletinOrdenPublicacion { get; set; }

        object BoletinesPublicacion { get; set; }

        object BoletinPublicacion { get; set; }

        object BoletinesConcesion { get; set; }

        object BoletinConcesion { get; set; }

        object Situaciones { get; set; }

        object Situacion { get; set; }

        object Estado { get; set; }

        object Estados { get; set; }

        object TipoBaseSolicitud { get; set; }

        object PaisesSolicitud { get; set; }

        object TiposBaseSolicitud { get; set; }

        object PaisSolicitud { get; set; }

        string TextoBotonModificar { get; set; }

        string IdInternacionalByt { get; set; }

        string IdNacionalByt { get; set; }

        CheckBox Byt { get; }

        //string IdNacional { get; set; }

        bool HabilitarCampos { set; }

        bool BytTipoDeBaseVisible { set; }

        bool AsociadosEstanCargados { get; set; }

        bool InteresadosEstanCargados { get; set; }

        //bool CorresponsalesEstanCargados { get; set; }

        bool PoderesEstanCargados { get; set; }

        void PintarInfoAdicional();

        //void PintarAnaqua();

        void PintarInfoBoles();

        //void PintarOperaciones();

        //void PintarBusquedas();

        void AgregarMarcaByt();

        void CargarMarcasByt();

        void LimpiarMarcasByt();

        void DeshabilitarMarcasByt();

        void PintarAuditoria();

        string ComentarioClienteEspanol { set; get; }

        string ComentarioClienteIngles { set; get; }

        void BorrarCeros();

        object MarcasByt{get; set;}

        object MarcaByt {get; set;}

        bool MensajeAlerta(string mensaje);

        //string ClaseInternacional { get; }

        //string ClaseNacional { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void Mensaje(string mensaje, int opcion);

        void MostrarByt();

        void PintarAsociado(string tipo);

        void ConvertirEnteroMinimoABlanco();

        string SaldoVencido { set; }

        string SaldoPorVencer { set; }

        string Total { set; }

    }
}
