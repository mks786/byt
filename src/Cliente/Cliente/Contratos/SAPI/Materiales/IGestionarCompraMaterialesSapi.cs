using System;
using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.SAPI.Materiales
{
    interface IGestionarCompraMaterialesSapi : IPaginaBase 
    {
        object CompraSapi { get; set; }

        object Materiales { get; set; }

        object Material { get; set; }

        string IdCompraSapi { get; set; }

        string CantidadMaterial { get; set; }

        object DetallesDeCompraSapi { get; set; }

        object DetalleDeCompraSapi { get; set; }

        string PorcentajeImpuesto { get; set; }

        string MontoImporte { get; set; }

        string MontoIva { get; set; }

        string TotalCompraSapi { get; set; }

        string TextoBotonModificar { get; set; }

        string FechaCompraSapi { get; set; }

        bool HabilitarCampos { set; }

        void Mensaje(string mensaje, int opcion);

        void ActivarCampoCantidad(bool status);

        void ActivarBotonesIncluirYBorrar(bool status);

        void SeleccionarPrimerItem();

        void OcultarBotonesAlConsultar();

        void PintarBotonVerFacturaSAPI();

        void ArchivoNoEncontrado(string mensaje);

        void DeshabilitarBotonAceptar();
    }
}
