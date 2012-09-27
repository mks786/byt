using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.FechasPatente
{
    interface IAgregarFecha : IPaginaBase
    {
        object FechaPatente { get; set; }

        string FechaRegistro { get; set; }

        object Tipos { get; set; }

        object Tipo { get; set; }

        string Correspondencia { get; set; }

        string Comentario { get; set; }

        string TimeStamp { get; set; }

        void mensaje(string mensaje);
    }
}
