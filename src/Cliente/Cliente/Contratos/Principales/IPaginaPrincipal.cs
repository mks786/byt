using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Principales
{
    interface IPaginaPrincipal : IPaginaBase
    {
        string MensajeError { get; set; }

        string MensajeUsuario { get; set; }

        void Mensaje(string mensaje, int opcion);

        bool ConfirmarAccion(string Titulo, string Mensaje);
    }
}
