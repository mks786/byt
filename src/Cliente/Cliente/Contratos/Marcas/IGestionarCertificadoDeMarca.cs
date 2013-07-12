using System.Windows.Controls;
using System.Windows;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IGestionarCertificadoDeMarca : IPaginaBase
    {
        object Certificado { get; set; }

        object Registradores { get; set; }

        object Registrador { get; set; }

        string ReciboNumero { get; set; }

        string RegistroBs { get; set; }

        string EscrituraBs { get; set; }

        string PapelProtocolo { get; set; }

        string TotalBs { get; set; }

        string Clases { get; set; }

        string Comentario { get; set; }

        void ArchivoNoEncontrado(string mensaje);

        void Mensaje(string mensaje, int opcion);

        void MostrarBotonEliminar(bool mostrar);
    }
}
