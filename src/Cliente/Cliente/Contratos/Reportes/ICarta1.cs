using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Reportes
{
    interface ICarta1 : IPaginaBase
    {
        object MarcaGeneral { get; set; }

        object Idioma { get; set; }

        object Idiomas { get; set; }

        object Interesado { get; set; }

        object Interesados { get; set; }

        object Asociado { get; set; }

        object Asociados { get; set; }

        object Marca { get; set; }

        object Marcas { get; set; }

        object MarcaAgregada { get; set; }

        object MarcasAgregadas { get; set; }

        object Usuario { get; set; }

        object Usuarios { get; set; }

        bool RadioConsultarAsociado();

        bool RadioConsultarInteresado();

        string IdFiltrar { get; }

        string NombreFiltrar { get; }

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        void Departamento(string texto);

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }
    }
}
