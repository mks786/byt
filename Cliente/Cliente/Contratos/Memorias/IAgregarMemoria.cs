
namespace Trascend.Bolet.Cliente.Contratos.Memorias
{
    interface IAgregarMemoria : IPaginaBase
    {
        string SetPatente { set; }

        object Memoria { get; set; }

        object TipoMensaje { get; set; }

        object TiposMensajes { get; set; }

        object FormatosDocumentos { get; set; }

        object FormatoDocumento { get; set; }

        void Mensaje(string mensaje);
    }
}
