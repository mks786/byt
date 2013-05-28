using System;
using System.Runtime.Remoting;
using Trascend.Bolet.Servicios;
using Trascend.Bolet.Servicios.Implementacion;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

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
            //Asociado asociado = new Asociado();
            //asociado.Id = 1292;
            //AsociadoServicios asociadoServicios = new AsociadoServicios();
            //ParametroProcedimiento parametro = new ParametroProcedimiento("nombre", "apellido");
            //bool exito = asociadoServicios.ActualizarConectividadAsociados(parametro);
            //asociado = asociadoServicios.ConsultarAsociadoConTodo(asociado);
            //Console.ReadLine();

            //---
            //Marca marcaPrueba = new Marca();
            //marcaPrueba.Id = 878217;
            //MarcaServicios _marcaServicios = new MarcaServicios();
            //marcaPrueba = _marcaServicios.ConsultarPorId(marcaPrueba);


            //---
            #endregion

            Console.WriteLine("Se han cargado " + ConfiguracionServicios.CargarUsuarios() + " sesiones de archivo XML");
            Console.WriteLine("Configuracion cargada...");
            Console.ReadLine();

        }
    }
}
