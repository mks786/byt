using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Pirateria.Casos
{
    interface IGestionarCaso : IPaginaBase
    {
        object Caso { get; set; }

        string IdCaso { get; set; }

        string FechaCaso { get; set; }

        object OrigenesCaso { get; set; }

        object OrigenCaso { get; set; }

        string DescripcionCaso { get; set; }

        string IdAsociadoCaso { get; set; }

        string AsociadoCaso { get; set; }

        string IdAsociadoConsultar { get; set; }

        string AsociadoConsultar { get; set; }

        object AsociadosConsultados { get; set; }

        object AsociadoSeleccionado { get; set; }

        string IdInteresadoCaso { get; set; }

        string InteresadoCaso { get; set; }

        string IdInteresadoConsultar { get; set; }

        string InteresadoConsultar { get; set; }

        object InteresadosConsultados { get; set; }

        object InteresadoSeleccionado { get; set; }

        string InteresadoCiudad { get; set; }

        string PrimeraReferencia { get; set; }

        string ComentariosCaso { get; set; }

        object SituacionesCaso { get; set; }

        object SituacionCaso { get; set; }

        string SituacionDescripcion { get; set; }

        object TiposDeCaso { get; set; }

        object TipoDeCaso { get; set; }

        object ListaTiposCaso { get; set; }

        object ListaTipoCaso { get; set; }

        object AccionesCaso { get; set; }

        object AccionCaso { get; set; }

        object ListaAccionesCaso { get; set; }

        object ListaAccionCaso { get; set; }

        bool HabilitarCampos { set; }

        bool AsociadosCargados { get; set; }

        bool InteresadosCargados { get; set; }

        object TiposBase { get; set; }

        object TipoBase { get; set; }

        string TextoBotonModificar { get; set; }

        bool? ByT { get; }

        string IdCasoBase { get; set; }

        string ITipoCasoBase { get; set; }

        string ClaseInternacionalCasoBase { get; set; }

        string ClaseNacionalCasoBase { get; set; }

        object CasosBases { get; set; }

        object CasoBaseSeleccionado { get; set; }

        string IdMarcaPatenteFiltrar { get; set; }

        string NombreMarcaPatenteFiltrar { get; set; }

        object ListaMarcasPatentes { get; set; }

        object ListaMarcaOPatente { get; set; }

        void Mensaje(string mensaje, int opcion);

        void PintarAsociado(string tipo);

        void ConvertirEnteroMinimoABlanco();

        void ocultarListaCasosBase();

        void ActivarBotones(bool flag);

        void mostrarListaCasosBase();

    }
}
