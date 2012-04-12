using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.LicenciasPatentes
{
    interface IGestionarLicenciaPatentes : IPaginaBase
    {
        object LicenciaPatente { get; set; }               
        
        //ListView Patentes

        string IdPatenteFiltrar { get; }

        string NombrePatenteFiltrar { get; }

        object PatentesFiltradas { get; set; }

        object PatenteFiltrada { get; set; }

        //-----------
        
        //ListView Licenciante
        string IdLicencianteFiltrar { get; }

        string NombreLicencianteFiltrar { get; }

        object LicenciantesFiltrados { get; set; }

        object LicencianteFiltrado { get; set; }
       
        //----------------

        //ListView Licenciatario

        string IdLicenciatarioFiltrar { get; }

        string NombreLicenciatarioFiltrar { get; }

        object LicenciatariosFiltrados { get; set; }

        object LicenciatarioFiltrado { get; set; }

        //----------------

        //Apoderado Licenciante

        string IdApoderadoLicencianteFiltrar { get; }

        string NombreApoderadoLicencianteFiltrar { get; }
        
        object ApoderadosLicencianteFiltrados { get; set; }

        object ApoderadoLicencianteFiltrado { get; set; }

        //----------------

        //Apoderado Licenciatario

        string IdApoderadoLicenciatarioFiltrar { get; }

        string NombreApoderadoLicenciatarioFiltrar { get; }

        object ApoderadosLicenciatarioFiltrados { get; set; }

        object ApoderadoLicenciatarioFiltrado { get; set; }

        //----------------

        //Poder Licenciante

        string IdPoderLicencianteFiltrar { get; }

        string FechaPoderLicencianteFiltrar { get; }

        object PoderesLicencianteFiltrados { get; set; }

        object PoderLicencianteFiltrado { get; set; }

        string IdPoderLicenciante { set; get; }

        //----------------

        //Poder Licenciatario

        string IdPoderLicenciatarioFiltrar { get; }

        string FechaPoderLicenciatarioFiltrar { get; }

        object PoderesLicenciatarioFiltrados { get; set; }

        object PoderLicenciatarioFiltrado { get; set; }

        string IdPoderLicenciatario { set; get; }

        //----------------
  

        bool HabilitarCampos { set; }

        string Region { get; set; }

        string TextoBotonModificar { get; set; }

        string TextoBotonRegresar { get; set; }

        string NombrePatente { set; }

        string NombreLicenciante { set; }

        string NombreLicenciatario { set; }

        string NombreApoderadoLicenciante { set; }

        string NombreApoderadoLicenciatario { set; }

        string PaisLicenciante { set; }

        string PaisLicenciatario { set; }

        string NacionalidadLicenciante { set; }

        string NacionalidadLicenciatario { set; }        

        object Patente { get; set; }

        object InteresadoLicenciante { get; set; }

        object InteresadoLicenciatario { get; set; }

        object ApoderadoLicenciante { get; set; }

        object ApoderadoLicenciatario { get; set; }

        object PoderLicenciante { get; set; }

        object PoderLicenciatario { get; set; }

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
