

namespace Trascend.Bolet.Cliente.Contratos.ReportesPatente
{
    public interface IReportePatente : IPaginaBase
    {

        void MensajeAlerta(string mensaje);

        void MensajeExito(string mensaje);

    }
}
