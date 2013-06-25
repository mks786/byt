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
            //IList<TipoDocumento> listaPrueba = null;
            //TipoDocumentoServicios servicioPrueba = new TipoDocumentoServicios();
            //listaPrueba = servicioPrueba.ConsultarTodos();
            //IList<TipoCaja> listaPrueba = null;
            //TipoCajaServicios servicioPrueba = new TipoCajaServicios();
            //listaPrueba = servicioPrueba.ConsultarTodos();
            //IList<Caja> listaPrueba = null;
            //CajaServicios servicioPrueba = new CajaServicios();
            //listaPrueba = servicioPrueba.ConsultarTodos();
            //IList<Almacen> listaPrueba = null;
            //AlmacenServicios servicioPrueba = new AlmacenServicios();
            //listaPrueba = servicioPrueba.ConsultarTodos();
            //IList<Archivo> listaPrueba = null;
            //ArchivoServicios servicioPrueba = new ArchivoServicios();
            //listaPrueba = servicioPrueba.ConsultarTodos();
            //Archivo archivoPrueba = new Archivo("860000");
            //Archivo resultado = new Archivo();
            //resultado = servicioPrueba.ConsultarPorId(archivoPrueba);
            //Console.ReadLine();
        
          
            #endregion

            Console.WriteLine("Se han cargado " + ConfiguracionServicios.CargarUsuarios() + " sesiones de archivo XML");
            Console.WriteLine("Configuracion cargada...");
            Console.ReadLine();

        }
    }
}
