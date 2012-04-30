using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Patentes
{
    interface IGestionarPatente : IPaginaBase
    {
        object Patente { get; set; }

        string IdAsociadoSolicitudFiltrar { get; }

        string IdAsociadoSolicitud { get;  set; }

        string NombreAsociadoSolicitudFiltrar { get; }

        string NombreAsociadoSolicitud { get; set; }

        string IdInteresadoSolicitudFiltrar { get; }

        string IdInteresadoSolicitud { set; }

        string NombreInteresadoSolicitudFiltrar { get; }

        string NombreInteresadoSolicitud { get; set; }

        string NumPoderSolicitud { get; set; }

        object AsociadosSolicitud { get; set; }

        object AsociadoSolicitud { get; set; }

        object InteresadosSolicitud { get; set; }

        object InteresadoSolicitud { get; set; }

        string InteresadoPaisSolicitud { get; set; }

        string InteresadoEstadoSolicitud { get; set; }

        object PoderesSolicitud { get; set; }

        object PoderSolicitud { get; set; }

        object Agentes { get; set; }

        object Agente { get; set; }

        object PaisesSolicitud { get; set; }

        object PaisSolicitud { get; set; }

        object TipoPatenteSolicitud { get; set; }

        object TipoPatentesSolicitud { get; set; }

        void Mensaje(string mensaje);

        string TextoBotonModificar { get; set; }

        bool HabilitarCampos { set; }

        bool AsociadosEstanCargados { get; set; }

        bool InteresadosEstanCargados { get; set; }

        bool CorresponsalesEstanCargados { get; set; }

        bool PoderesEstanCargados { get; set; }

        void PintarDocumentosSolicitud();

        void PintarCasoEspecialSolicitud();

        void PintarDisenoSolicitud();

        void PintarDisenoReporteSolicitud();

        void PintarInventoresSolicitud();

        void PintarInfoAdicional();

        void PintarImprimirEdoDeCuenta();

        void PintarSaldos();

        bool MensajeAlerta(string mensaje);

        void ArchivoNoEncontrado();

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

    }
}
