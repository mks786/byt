using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.FechasPatente
{
    interface IConsultarFecha : IPaginaBase
    {
        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        object FechaPatente { get; set; }

        object Tipos { get; set; }

        object Tipo { get; set; }

        string Correspondencia { get; set; }

        string Comentario { get; set; }

        string TimeStamp { get; set; }
        
        void mensaje(string mensaje);
    }
}
