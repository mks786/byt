using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Traspasos.CambiosDePeticionario
{
    interface IGestionarCambioDePeticionario : IPaginaBase
    {
        object CambioPeticionario { get; set; }               
        
        //ListView Marcas

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object MarcasFiltradas { get; set; }

        object MarcaFiltrada { get; set; }

        //-----------
        
        //ListView Anterior
        string IdAnteriorFiltrar { get; }

        string NombreAnteriorFiltrar { get; }

        object AnteriorsFiltrados { get; set; }

        object AnteriorFiltrado { get; set; }
       
        //----------------

        //ListView Actual

        string IdActualFiltrar { get; }

        string NombreActualFiltrar { get; }

        object ActualsFiltrados { get; set; }

        object ActualFiltrado { get; set; }

        //----------------

        //Apoderado Anterior

        string IdApoderadoAnteriorFiltrar { get; }

        string NombreApoderadoAnteriorFiltrar { get; }
        
        object ApoderadosAnteriorFiltrados { get; set; }

        object ApoderadoAnteriorFiltrado { get; set; }

        //----------------

        //Apoderado Actual

        string IdApoderadoActualFiltrar { get; }

        string NombreApoderadoActualFiltrar { get; }

        object ApoderadosActualFiltrados { get; set; }

        object ApoderadoActualFiltrado { get; set; }

        //----------------

        //Poder Anterior

        string IdPoderAnteriorFiltrar { get; }

        string FechaPoderAnteriorFiltrar { get; }

        object PoderesAnteriorFiltrados { get; set; }

        object PoderAnteriorFiltrado { get; set; }

        //----------------

        //Poder Actual

        string IdPoderActualFiltrar { get; }

        string FechaPoderActualFiltrar { get; }

        object PoderesActualFiltrados { get; set; }

        object PoderActualFiltrado { get; set; }

        //----------------
  

        bool HabilitarCampos { set; }

        string Region { get; set; }

        string TextoBotonModificar { get; set; }
        
        string TextoBotonRegresar { get; set; }

        string NombreMarca { set; }

        string NombreAnterior { set; }

        string NombreActual { set; }

        string NombreApoderadoAnterior { set; }

        string NombreApoderadoActual { set; }

        string IdPoderAnterior { set; get; }

        string IdPoderActual { set; get; }

        string PaisAnterior { set; }

        string PaisActual { set; }

        string NacionalidadAnterior { set; }

        string NacionalidadActual { set; }        

        object Marca { get; set; }

        object InteresadoAnterior { get; set; }

        object InteresadoActual { get; set; }

        object ApoderadoAnterior { get; set; }

        object ApoderadoActual { get; set; }

        object PoderAnterior { get; set; }

        object PoderActual { get; set; }

        object Boletines { get; set; }

        object Boletin { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ConvertirEnteroMinimoABlanco(string tipo);

        void GestionarBotonConsultarInteresados(string tipo, bool value);
        
        void GestionarBotonConsultarApoderados(string tipo, bool value);
        
        void GestionarBotonConsultarPoderes(string tipo, bool value);       

        void ActivarControlesAlAgregar();
    }
}
