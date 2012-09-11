using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Reportes
{
    interface ICarta16P : IPaginaBase
    {
        object PatenteGeneral { get; set; }

        object Idioma { get; set; }

        object Idiomas { get; set; }

        object Interesado { get; set; }

        object Interesados { get; set; }

        object Asociado { get; set; }

        object Asociados { get; set; }

        object Patente { get; set; }

        object Patentes { get; set; }

        object PatenteAgregada { get; set; }

        object PatentesAgregadas { get; set; }

        object Usuario { get; set; }

        object Usuarios { get; set; }

        bool RadioConsultarAsociado();

        bool RadioConsultarInteresado();

        bool RadioMuchasPatentes();

        bool RadioUnicaPatente();

        string IdFiltrar { get; }

        string NombreFiltrar { get; }

        string IdPatenteFiltrar { get; }

        string NombrePatenteFiltrar { get; }

        void Departamento(string texto);

        void BorrarCeros();

        void MensajeAlerta(string mensaje);

        void MensajeExito(string mensaje);

        string Fecha { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }
    }
}
