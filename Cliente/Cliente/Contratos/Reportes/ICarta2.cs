using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Reportes
{
    interface ICarta2 : IPaginaBase
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

        bool RadioMuchasMarcas();

        bool RadioUnicaMarca();

        string IdFiltrar { get; }

        string NombreFiltrar { get; }

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        void Departamento(string texto);

        void BorrarCeros();

        void MensajeAlerta(string mensaje);

        void MensajeExito(string mensaje);

        string Fecha { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }
    }
}
