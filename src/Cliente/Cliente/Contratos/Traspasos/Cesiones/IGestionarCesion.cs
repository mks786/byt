using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Traspasos.Cesiones
{
    interface IGestionarCesion : IPaginaBase
    {
        object Cesion { get; set; }

        string IdAsociadoFiltrar { get; }

        string NombreAsociadoFiltrar { get; }       
        
        //ListView Marcas

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object MarcasFiltradas { get; set; }

        object MarcaFiltrada { get; set; }

        //-----------
        
        //ListView Cedente
        string IdCedenteFiltrar { get; }

        string NombreCedenteFiltrar { get; }

        object CedentesFiltrados { get; set; }

        object CedenteFiltrado { get; set; }
       
        //----------------

        //ListView Cesionario

        string IdCesionarioFiltrar { get; }

        string NombreCesionarioFiltrar { get; }

        object CesionariosFiltrados { get; set; }

        object CesionarioFiltrado { get; set; }

        //----------------

        //Apoderado Cedente

        string IdApoderadoCedenteFiltrar { get; }

        string NombreApoderadoCedenteFiltrar { get; }
        
        object ApoderadosCedenteFiltrados { get; set; }

        object ApoderadoCedenteFiltrado { get; set; }

        //----------------

        //Apoderado Cesionario

        string IdApoderadoCesionarioFiltrar { get; }

        string NombreApoderadoCesionarioFiltrar { get; }

        object ApoderadosCesionarioFiltrados { get; set; }

        object ApoderadoCesionarioFiltrado { get; set; }

        //----------------

        //Poder Cedente

        string IdPoderCedenteFiltrar { get; }

        string NombrePoderCedenteFiltrar { get; }

        object PoderesCedenteFiltrados { get; set; }

        object PoderCedenteFiltrado { get; set; }

        //----------------

        //Poder Cesionario

        string IdPoderCesionarioFiltrar { get; }

        string NombrePoderCesionarioFiltrar { get; }

        object PoderesCesionarioFiltrados { get; set; }

        object PoderCesionarioFiltrado { get; set; }

        //----------------
  

        bool HabilitarCampos { set; }

        string Region { get; set; }

        string TextoBotonModificar { get; set; }

        string NombreMarca { set; }

        string NombreCedente { set; }

        object Marca { get; set; }

        object InteresadoCedente { get; set; }

        object InteresadoCesionario { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }
    }
}
