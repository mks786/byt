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

            #region Codigo de Prueba NO BORRAR 
            
            /*CartaOut filtro = new CartaOut();
            filtro.Status = 'T';
            CartaOutServicios servicioPrueba = new CartaOutServicios();
            //IList<CartaOut> lista = servicioPrueba.ObtenerCartasOutsFiltro(filtro);
            IList<CartaOut> lista = servicioPrueba.ObtenerCartasOutsFiltroAdo(filtro);*/

            //InteresadoPatenteServicios servicioPrueba = new InteresadoPatenteServicios();
            //IList<InteresadoPatente> interesados = servicioPrueba.ConsultarTodos();

            /*InteresadoPatenteServicios servicioPrueba = new InteresadoPatenteServicios();
            PatenteServicios patenteServicios = new PatenteServicios();
            Patente patente = new Patente();
            patente = patenteServicios.ConsultarPatenteConTodo(new Patente(6000));
            IList<InteresadoPatente> interesados = servicioPrueba.ConsultarInteresadosDePatente(patente);*/

            /*int cantidadDias = 90;

            PatenteServicios servicioPrueba = new PatenteServicios();

            IList<VencimientoPrioridadPatente> patentesPorVencer = servicioPrueba.ObtenerPatentesPorVencerPrioridad(cantidadDias);*/

            /*FacInternacionalConsolidadaServicios pruebaServicio = new FacInternacionalConsolidadaServicios();
            AsociadoServicios asociadoServicios = new AsociadoServicios();
            FacInternacionalConsolidada fac = new FacInternacionalConsolidada();
            fac.Id = 18954;
            Asociado asociadoOAux = new Asociado(23157);
            Asociado asociadoO = new Asociado();
            asociadoO = asociadoServicios.ConsultarAsociadoConTodo(asociadoOAux);
            Asociado asociadoAux = new Asociado(21208);
            Asociado asociado = asociadoServicios.ConsultarAsociadoConTodo(asociadoAux);
            fac.AsociadoInt = asociadoO;
            fac.Asociado = asociado;

            bool exitoso = pruebaServicio.InsertarOModificar(fac, 123);

            FacInternacionalConsolidadaServicios servicioPrueba = new FacInternacionalConsolidadaServicios();
            IList<FacInternacionalConsolidada> lista = servicioPrueba.ConsultarTodos();*/


            /*AsociadoServicios asociadoServicio = new AsociadoServicios();
            FacAsociadoIntConsolidadoCxPIntServicios servicioPrueba = new FacAsociadoIntConsolidadoCxPIntServicios();
            FacAsociadoIntConsolidadoCxPInt facAsociado = new FacAsociadoIntConsolidadoCxPInt();

            facAsociado.Id = 2;
            facAsociado.AsociadoInt = asociadoServicio.ConsultarAsociadoConTodo(new Asociado(1292));
            facAsociado.MontoConsolidado = 500;
            facAsociado.FormaPago = "Deposito";

            bool exito1 = servicioPrueba.Eliminar(facAsociado, 123);

            //bool exito = servicioPrueba.InsertarOModificar(facAsociado, 123);

            IList<FacAsociadoIntConsolidadoCxPInt> lista = servicioPrueba.ConsultarTodos();*/

            //Console.ReadLine();


            #endregion

            Console.WriteLine("Se han cargado " + ConfiguracionServicios.CargarUsuarios() + " sesiones de archivo XML");
            Console.WriteLine("Configuracion cargada...");
            Console.ReadLine();

        }
    }
}
