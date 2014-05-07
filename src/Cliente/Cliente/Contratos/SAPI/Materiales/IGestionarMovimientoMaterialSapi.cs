using System;
using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.SAPI.Materiales
{
    interface IGestionarMovimientoMaterialSapi : IPaginaBase
    {
        //object MovimientoMaterialesSapi { get; set; }

        string IdSolicitudMaterial { get; set; }

        string FechaSolicitudMaterial { get; set; }

        object UsuariosSolicitantes { get; set; }

        object UsuarioSolicitante { get; set; }

        object Departamentos { get; set; }

        object Departamento { get; set; }

        object MaterialesSAPI { get; set; }

        object MaterialSAPI { get; set; }

        object DetallesSolicitudMaterial { get; set; }

        object DetalleSolicitudMaterial { get; set; }

        bool HabilitarCampos { set; }

        object TiposMovimientosMaterial { get; set; }

        object TipoMovimientoMaterial { get; set; }

        string TextoBotonModificar { get; set; }

        void Mensaje(string mensaje, int opcion);

        void SeleccionarPrimerItem();

        void MostrarBotonEliminar(bool flag);
    }
}
