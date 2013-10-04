﻿using System;
using System.Runtime.Remoting;
using Trascend.Bolet.Servicios;
using Trascend.Bolet.Servicios.Implementacion;
using Diginsoft.Bolet.Servicios.Implementacion;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using Diginsoft.Bolet.ObjetosComunes.Entidades;
using System.Data;




namespace Trascend.Bolet.Servidor
{
    public class Program
    {
        private const string _archivo = "Trascend.Bolet.Servidor.exe.config";

        [STAThread]
        static void Main(string[] args)
        {

            RemotingConfiguration.Configure(_archivo, false);
            UsuarioServicios servicio = new UsuarioServicios();
            Usuario usuarioPrueba = new Usuario();
            usuarioPrueba.Password = "PRUEBA";
            usuarioPrueba.Id = "PRUEBA";
            servicio.Autenticar(usuarioPrueba);

            #region Codigo de Prueba NO BORRAR - CONECTIVIDAD

            /*MaestroDePlantillaServicios servicioPrueba = new MaestroDePlantillaServicios();
            MaestroDePlantilla mp = new MaestroDePlantilla();
            Plantilla plantilla = new Plantilla();
            plantilla.Id = 1;
            mp.Plantilla = plantilla;
            IList<MaestroDePlantilla> resultados = servicioPrueba.ObtenerMaestroDePlantillaFiltro(mp);*/

            /*MaestroDePlantilla plantilla = new MaestroDePlantilla();
            plantilla.Id = 1;

            FiltroPlantillaServicios servicioPrueba = new FiltroPlantillaServicios();
            IList<FiltroPlantilla> filtros = servicioPrueba.ObtenerFiltrosDetallePlantilla(plantilla);

            Console.ReadLine();*/

            
            #endregion

            Console.WriteLine("Se han cargado " + ConfiguracionServicios.CargarUsuarios() + " sesiones de archivo XML");
            Console.WriteLine("Configuracion cargada...");
            Console.ReadLine();

        }
    }
}
