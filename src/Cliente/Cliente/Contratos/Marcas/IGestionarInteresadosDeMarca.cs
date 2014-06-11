using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarInteresadosDeMarca : IPaginaBase
    {

        object Marca { get; set; }

        string IdInteresado1 { get; set; }

        string NombreInteresado1 { get; set; }

        string IdInteresado1Filtrar { get; }

        string NombreInteresado1Filtrar { get; }

        object Interesados1 { get; set; }

        object Interesado1 { get; set; }


        string IdInteresado2 { get; set; }

        string NombreInteresado2 { get; set; }

        string IdInteresado2Filtrar { get; }

        string NombreInteresado2Filtrar { get; }

        object Interesados2 { get; set; }

        object Interesado2 { get; set; }


        string IdInteresado3 { get; set; }

        string NombreInteresado3 { get; set; }

        string IdInteresado3Filtrar { get; }

        string NombreInteresado3Filtrar { get; }

        object Interesados3 { get; set; }

        object Interesado3 { get; set; }

        void ConvertirEnteroMinimoABlanco();
    }
}
