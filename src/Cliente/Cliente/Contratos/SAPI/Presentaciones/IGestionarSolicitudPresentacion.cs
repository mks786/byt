using System;
using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.SAPI.Presentaciones
{
    interface IGestionarSolicitudPresentacion : IPaginaBase
    {
        object EncabezadoPresentacion { get; set; }

        string IdPresentacionSapi { get; set; }

        object Usuarios { get; set; }

        object Usuario { get; set; }

        object Departamentos { get; set; }

        object Departamento { get; set; }

        string FechaPresentacion { get; set; }

        object Documentos { get; set; }

        object Documento { get; set; }

        string CodigoExpedientePresentacion { get; set; }

        object DetallePresentacion { get; set; }

        object DetallePresentacionSeleccionado { get; set; }

        string CantidadDocumentos { get; set; }

        string TextoBotonModificar { get; set; }

        bool HabilitarCampos { set; }
        
        void Mensaje(string mensaje, int opcion);

        void SeleccionarPrimerItem();
    }
}
