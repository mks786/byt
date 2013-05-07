using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Cliente.Contratos.Inventores;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using System.Configuration;
using System.Windows.Input;

namespace Trascend.Bolet.Cliente.Presentadores.Inventores
{
    class PresentadorQueryInventor : PresentadorBase
    {
        #region Propiedades del Presentador
        /* Conjunto de propiedades que se utilizan para modificar y afectar la interfaz de usuario (UI)*/

        //Referencia al contrato - Interfaz de la UI - Para hacer referencia a la ventana
        private IQueryInventor _ventana;
        //Instancia de la ventana principal
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        //Se obtiene el logger para el trace
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //Referencia al objeto Inventor
        private Inventor _inventor;
        //Referencias a los servicios - COMUNICACION CON EL SERVIDOR
        private IPatenteServicios _marcaServicios;
        private ICartaServicios _cartaServicios;
        private IInventorServicios _inventorServicios;
        private IPaisServicios _paisServicios;
        private Ventanas.Inventores.QueryInventor queryInventor;
        private object inventor;

        
        #endregion


        #region Constructor del Presentador

        /// <summary>
        /// Constructor del Presentador de la ventana QueryInventor
        /// </summary>
        /// <param name="ventana"></param>
        /// <param name="inventor"></param>
        public PresentadorQueryInventor(IQueryInventor ventana, object inventor)
        {
            //Seteo la ventana del UI tomando en cuenta el contrato
            this._ventana = ventana;
            //Seteo el objeto inventor
            this._inventor = (Inventor)inventor;
            //Objeto del Grid que esta Binding 
            this._ventana.Inventor = this._inventor;
            this._ventana.Paises = null;
            this._ventana.Nacionalidades = null;

            //Cargando el enlace con los servicios del Servidor
            this._inventorServicios = (IInventorServicios)Activator.GetObject(typeof(IInventorServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InventorServicios"]);
            this._marcaServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
            this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
            this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);


        }

        #endregion


        #region Metodos del Presentador

        public void CargarPagina()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarInventor, "");

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                paises.Insert(0, new Pais(int.MinValue));
                this._ventana.Paises = paises;
                if(this._inventor != null)
                    this._ventana.Pais = this.BuscarPais(paises, this._inventor.Pais);
                

                IList<Pais> nacionalidades = this._paisServicios.ConsultarTodos();
                nacionalidades.Insert(0, new Pais(int.MinValue));
                this._ventana.Nacionalidades = nacionalidades;
                if(this._inventor != null)
                    this._ventana.Nacionalidad = this.BuscarPais(nacionalidades, this._inventor.Nacionalidad);

                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        #endregion



        
    }
}
