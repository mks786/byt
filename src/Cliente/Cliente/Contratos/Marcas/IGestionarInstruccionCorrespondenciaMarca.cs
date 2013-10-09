using System.Windows.Controls;
using System.Windows;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarInstruccionCorrespondenciaMarca : IPaginaBase
    {
        
        object Marca { get; set; }

        string IdMarca { get; set; }

        string DescripcionMarca { get; set; }

        string TipoDeMarca { get; set; }

        string ClaseNacionalMarca { get; set; }

        string ClaseInternacionalMarca { get; set; }

        object InstruccionCorrespondenciaEnvioEmail { get; set; }

        string IdInstruccionCorrespondenciaEnvioEmails { get; set; }

        string IdCorrespondenciaEnvioEmails { get; set; }

        object TiposDeInstruccionEnvioEmails { get; set; }

        object TipoInstruccionEnvioEmails { get; set; }

        string NombreEmailEnvioEmails { get; set; }

        string EmailParaEnvioEmails { get; set; }

        string EmailCCEnvioEmails { get; set; }

        object InstruccionCorrespondenciaEnvioOriginales { get; set; }

        string IdInstruccionCorrespondenciaEnvioOriginales { get; set; }

        string NombreInstruccionEnvioOriginales { get; set; }

        string OtraDireccion { get; set; }

        //Datos del Asociado Envio de Originales

        string IdAsociadoEnvioOriginales { get; set; }

        string AsociadoEnvioOriginales { get; set; }

        string IdCorrespondencia_Asociado { get; set; }

        string Domicilio_Asociado { get; set; }

        //Datos del Asociado para Filtrar

        string IdAsociadoFiltrar { get; set; }

        string NombreAsociadoFiltrar { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        //Datos del Interesado Envio de Originales

        string IdInteresadoEnvioOriginales { get; set; }

        string InteresadoEnvioOriginales { get; set; }

        string IdCorrespondencia_Interesado { get; set; }

        string Domicilio_Interesado { get; set; }

        //Datos del Interesado para filtrar

        string IdInteresadoFiltrar { get; set; }

        string NombreInteresadoFiltrar { get; set; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ConvertirEnteroMinimoABlanco();
    }
}
