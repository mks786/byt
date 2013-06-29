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
            //listaPrueba = servicioPrueba.ObtenerTipoDocumentoMarcaOPatente("M", "I");
            //IList<TipoCaja> listaPrueba = null;
            //TipoCajaServicios servicioPrueba = new TipoCajaServicios();
            //listaPrueba = servicioPrueba.ObtenerTipoCajaMarcaOPatente("Marca");
            //IList<Caja> listaPrueba = null;
            //CajaServicios servicioPrueba = new CajaServicios();
            //listaPrueba = servicioPrueba.ConsultarTodos();
            //IList<Almacen> listaPrueba = null;
            //AlmacenServicios servicioPrueba = new AlmacenServicios();
            //listaPrueba = servicioPrueba.ConsultarTodos();
            //IList<Archivo> listaPrueba = null;
            //ArchivoServicios servicioPrueba = new ArchivoServicios();
            //listaPrueba = servicioPrueba.ConsultarTodos();
            //Archivo archivoPrueba = new Archivo("1002","2");
            //Archivo resultado = new Archivo();
            //resultado = servicioPrueba.ConsultarPorId(archivoPrueba);
            //resultado = servicioPrueba.ObtenerArchivoDeMarcaOPatenteInternacional(archivoPrueba);
            //IList<Registrador> listaPrueba = null;
            //RegistradorServicios servicioPrueba = new RegistradorServicios();
            //listaPrueba = servicioPrueba.ConsultarTodos();

            //CertificadoMarca certificado = new CertificadoMarca(860000);
            //CertificadoMarca resultado = null;
            //CertificadoMarcaServicios servicioPrueba = new CertificadoMarcaServicios();
            //resultado = servicioPrueba.ConsultarPorId(certificado);

            //Marca marcaPrueba = new Marca();
            //MarcaServicios marcaServicios = new MarcaServicios();
            //FechaMarcaServicios fechaMarcaServicios = new FechaMarcaServicios();
            //IList<FechaMarca> lista = null;
            //marcaPrueba.Id = 860000;
            //marcaPrueba = marcaServicios.ConsultarMarcaConTodo(marcaPrueba);
            //lista = fechaMarcaServicios.ConsultarFechasPorMarca(marcaPrueba);



            //Console.ReadLine();
        
          
            #endregion

            Console.WriteLine("Se han cargado " + ConfiguracionServicios.CargarUsuarios() + " sesiones de archivo XML");
            Console.WriteLine("Configuracion cargada...");
            Console.ReadLine();

        }
    }
}
