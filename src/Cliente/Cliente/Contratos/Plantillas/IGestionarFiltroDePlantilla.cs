using System;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Plantillas
{
    interface IGestionarFiltroDePlantilla: IPaginaBase
    {

        object FiltroPlantilla { get; set; }

        string CodigoDePlantilla { get; set; }

        string NombreFiltro { get; set; }

        string NombreVariableFiltro { get; set; }

        object TiposDeDatosFiltro { get; set; }

        object TipoDeDatosFiltro { get; set; }

        object TiposDeFiltro { get; set; }

        object TipoDeFiltro { get; set; }

        string TextoBotonModificar { get; set; }

        bool HabilitarCampos { set; }

        bool IncluirEnBat { get; }

        void MensajeAlerta(string mensaje, int opcion);

        void MarcarCheckAplica();

    }
}
