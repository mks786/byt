
namespace Trascend.Bolet.Cliente.Contratos.Interesados
{
    interface IAgregarInteresado : IPaginaBase
    {
        object Interesado { get; set; }

        char TipoPersona { get; }

        object Paises { get; set; }

        object Pais { get; set; }

        object Nacionalidades { get; set; }

        object Nacionalidad { get; set; }

        object Corporaciones { get; set; }

        object Corporacion { get; set; }
    }
}
