using System;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Plantillas
{
    interface IAnalizarDetallePlantilla : IPaginaBase
    {

        object DetallePlantilla { get; set; }
        
        string SQLDetalle { get; set; }

        object Variables { get; set; }

        string ComandoSQLPlus { get; set; }

        string ComandoBat { get; set; }

        void DesactivarListaVariables();

        void GestionarVisibilidadBat();

        void MensajeAlerta(string mensaje, int opcion);
    }
}
