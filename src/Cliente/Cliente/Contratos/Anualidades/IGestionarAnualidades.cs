using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Anualidades
{
    interface IGestionarAnualidades : IPaginaBase
    {
        object Patente { get; set; }

        #region Patentes

        string NombrePatente { set; }

        //object Patente { get; set; }

        string IdPatenteFiltrar { get; }

        string NombrePatenteFiltrar { get; }

        object PatentesFiltradas { get; set; }

        object PatenteFiltrada { get; set; }

        #endregion

        #region Asociados

        string IdAsociadoSolicitudFiltrar { get; }

        string IdAsociadoSolicitud { set; }

        string NombreAsociadoSolicitudFiltrar { get; }

        string NombreAsociadoSolicitud { get; set; }

        object AsociadosSolicitud { get; set; }

        object AsociadoSolicitud { get; set; }

        bool AsociadosEstanCargados { get; set; }

        #endregion

        #region Interesados

        bool InteresadosEstanCargados { get; set; }

        string NombreInteresadoSolicitud { get; set; }

        string IdInteresadoSolicitudFiltrar { get; }

        object InteresadosSolicitud { get; set; }

        object InteresadoSolicitud { get; set; }

        string NombreInteresadoSolicitudFiltrar { get; }

        #endregion

        #region Boletines y situacion

        object BoletinesPublicacion { get; set; }

        object BoletinPublicacion { get; set; }

        object BoletinesConcesion { get; set; }

        object BoletinConcesion { get; set; }

        object Situaciones { get; set; }

        object Situacion { get; set; }

        #endregion

        #region Anualidad

        void AgregarAnualidad();

        void CargarAnualidad();

        void CargarAnualidadSeleccionada();

        void DeshabilitarAnualidad();

        object Anualidades { get; set; }

        object Anualidad { get; set; }

        string Referencia { get; set; }

        string RegistroCodigo { get; set; }

        string RegistroFecha { get; set; }

        string Recibo { get; set; }

        string Id { get; set; }

        string FechaAnualidad { get; set; }

        string Voucher { get; set; }

        string FechaVoucher { get; set; }

        string Factura { get; set; }

        object ISituaciones { get; set; }

        object ISituacion { get; set; }

        string FechaFactura { get; set; }

        CheckBox ChkFactura { get; }

        #endregion

        bool HabilitarCampos { set; }

        string Region { get; set; }

        string TextoBotonModificar { get; set; }

        string TextoBotonRegresar { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ActivarControlesAlAgregar();

      //  bool AsociadosEstanCargados { get; set; }
    }
}
