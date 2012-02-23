﻿using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Traspasos.Licencias
{
    interface IGestionarLicencia : IPaginaBase
    {
        object Licencia { get; set; }               
        
        //ListView Marcas

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object MarcasFiltradas { get; set; }

        object MarcaFiltrada { get; set; }

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

        //----------------

        //Poder Licenciatario

        string IdPoderLicenciatarioFiltrar { get; }

        string FechaPoderLicenciatarioFiltrar { get; }

        object PoderesLicenciatarioFiltrados { get; set; }

        object PoderLicenciatarioFiltrado { get; set; }

        //----------------
  

        bool HabilitarCampos { set; }

        string Region { get; set; }

        string TextoBotonModificar { get; set; }

        string NombreMarca { set; }

        string NombreLicenciante { set; }

        string NombreLicenciatario { set; }

        string NombreApoderadoLicenciante { set; }

        string NombreApoderadoLicenciatario { set; }        

        string IdPoderLicenciante { set; }

        string IdPoderLicenciatario { set; }

        string PaisLicenciante { set; }

        string PaisLicenciatario { set; }

        string NacionalidadLicenciante { set; }

        string NacionalidadLicenciatario { set; }        

        object Marca { get; set; }

        object InteresadoLicenciante { get; set; }

        object InteresadoLicenciatario { get; set; }

        object ApoderadoLicenciante { get; set; }

        object ApoderadoLicenciatario { get; set; }

        object PoderLicenciante { get; set; }

        object PoderLicenciatario { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}