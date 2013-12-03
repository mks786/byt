using System;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Plantillas
{
    interface IAnalizarEncabezadoPlantilla: IPaginaBase
    {

        object EncabezadoPlantilla { get; set; }
        
        string SQLEncabezado { get; set; }

        object Variables { get; set; }

        string ComandoSQLPlus { get; set; }

        string ComandoBat { get; set; }

        void DesactivarListaVariables();

        void MensajeAlerta(string mensaje, int opcion);

        void GestionarVisibilidadBat();

        void AplicarVisibilidadControles();

    }
}
