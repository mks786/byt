﻿using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IAgregarMarca : IPaginaBase
    {
        object Marca { get; set; }

        string IdAsociadoSolicitudFiltrar { get; }

        string IdAsociadoSDatosFiltrar { get; }

        string NombreAsociadoSolicitudFiltrar { get; }

        string NombreAsociadoDatosFiltrar { get; }

        string NombreAsociadoSolicitud { get; set; }

        string IdInteresadoSolicitudFiltrar { get; }

        string IdInteresadoSDatosFiltrar { get; }

        string NombreInteresadoSolicitudFiltrar { get; }

        string NombreInteresadoDatosFiltrar { get; }

        string NombreInteresadoSolicitud { get; set; }

        string NombreInteresadoDatos { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        object PoderesSolicitud { get; set; }

        object PoderSolicitud { get; set; }

        object PoderesDatos { get; set; }

        object PoderDatos { get; set; }

        object Agentes { get; set; }

        object Agente { get; set; }

        object BoletinesOrdenPublicacion { get; set; }

        object BoletinOrdenPublicacion { get; set; }

        object BoletinesPublicacion { get; set; }

        object BoletinPublicacion { get; set; }

        object BoletinesConcesion { get; set; }

        object BoletinConcesion { get; set; }

        object Servicios { get; set; }

        object Servicio { get; set; }

        object Detalle { get; set; }

        object Detalles { get; set; }

        object Condiciones { get; set; }

        object Condicion { get; set; }
    }
}
