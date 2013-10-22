using System;
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
            /*
            //AsociadoServicios asociadoServicios = new AsociadoServicios();
            //Asociado asociado = new Asociado();
            //asociado.Id = 24689;
            FiltroDataCruda filtro = new FiltroDataCruda();
            //filtro.Anio = 2013;
            //filtro.Mes = 9;
            filtro.Moneda = "US";
            filtro.Ordenamiento = "ASC";
            //filtro.Asociado = asociadoServicios.ConsultarAsociadoConTodo(asociado);
            filtro.RangoSuperior = 10;*/

            /*SeguimientoClientesServicios servicioPrueba = new SeguimientoClientesServicios();
            DataTable datos = new DataTable();

            FiltroDataCruda filtro = new FiltroDataCruda();
            filtro.Moneda = "US";
            filtro.Ordenamiento = "DESC";
            filtro.RangoSuperior = 15;

            datos = servicioPrueba.ObtenerDataCruda(filtro);

            Console.ReadLine();*/
            

            #endregion

            Console.WriteLine("Se han cargado " + ConfiguracionServicios.CargarUsuarios() + " sesiones de archivo XML");
            Console.WriteLine("Configuracion cargada...");
            Console.ReadLine();

        }
    }
}
