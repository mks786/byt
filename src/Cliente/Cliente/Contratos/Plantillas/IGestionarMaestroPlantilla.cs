using System;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Plantillas
{
    interface IGestionarMaestroPlantilla: IPaginaBase
    {
        object Plantillas { get; set; }

        object Plantilla { get; set; }
        
        object Idioma { get; set; }

        object Idiomas { get; set; }

        object Departamentos { get; set; }

        object Departamento { get; set; }

        object Usuarios { get; set; }

        object Usuario { get; set; }

        object Referencias { get; set; }

        object Referencia { get; set; }

        object Criterios { get; set; }

        object Criterio { get; set; }

        object ArchivosEncabezado { get; set; }

        object ArchivoEncabezado { get; set; }

        object ArchivosBat { get; set; }

        object ArchivoBat { get; set; }

        object ArchivosDetalle { get; set; }

        object ArchivoDetalle { get; set; }

        object ArchivosBatDetalle { get; set; }

        object ArchivoBatDetalle { get; set; }

        void MensajeAlerta(string mensaje, int opcion);


    }
}
