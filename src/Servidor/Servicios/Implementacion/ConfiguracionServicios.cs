using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public static class ConfiguracionServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static int CargarUsuarios()
        {
            return ControladorBase.CargarUsuariosXML();
        }

    }
}
