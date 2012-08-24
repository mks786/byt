using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Inventores;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Presentadores.Inventores
{
    class PresentadorAgregarInventor : PresentadorBase
    {
        private IAgregarInventor _ventana;

        private IPatenteServicios _marcaServicios;
        private ICartaServicios _cartaServicios;
        private IInventorServicios _inventorServicios;
        private IPaisServicios _paisServicios;

        private Patente _patente;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarInventor(IAgregarInventor ventana, object patente)
        {
            try
            {
                this._ventana = ventana;
                this._patente = (Patente)patente;
                this._ventana.Inventor = new Inventor();

                this._inventorServicios = (IInventorServicios)Activator.GetObject(typeof(IInventorServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InventorServicios"]);
                this._marcaServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            try
            {                            
                #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarInventor,
                        "");
                this._ventana.Paises = null;
                this._ventana.Nacionalidades = null;
                //this._ventana.borrarId();

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                paises.Insert(0, new Pais(int.MinValue));
                this._ventana.Paises = paises;
                //this._ventana.Pais = this.BuscarPais(paises, this._inventor.Pais);

                IList<Pais> nacionalidades = this._paisServicios.ConsultarTodos();
                nacionalidades.Insert(0, new Pais(int.MinValue));
                this._ventana.Nacionalidades = nacionalidades;
                //this._ventana.Nacionalidad = this.BuscarPais(nacionalidades, this._inventor.Nacionalidad);

                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
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

        /// <summary>
        /// Método que realiza toda la lógica para agregar al contacto dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = false;

                Inventor inventor = (Inventor)this._ventana.Inventor;
                //inventor.Pais = new Pais();
                //inventor.Nacionalidad = new Pais();

                if (true)
                {
                    inventor.Pais = ((Pais)this._ventana.Pais).Id.Equals(int.MinValue) ? null : (Pais)this._ventana.Pais;
                    inventor.Nacionalidad = ((Pais)this._ventana.Nacionalidad).Id.Equals(int.MinValue) ? null : (Pais)this._ventana.Nacionalidad;
                    inventor.Patente = this._patente;

                    inventor.Id = IdMayorDeInventorPorPatente();
                    exitoso = this._inventorServicios.InsertarOModificar(inventor, UsuarioLogeado.Hash);

                    if (exitoso)
                        this.Navegar(new ListaInventores(((Inventor)this._ventana.Inventor).Patente));
                }
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        private int IdMayorDeInventorPorPatente()
        {
            int retorno = 0;
            foreach (Inventor inventor in this._patente.Inventores)
            {
                if (inventor.Id > retorno)
                    retorno = inventor.Id;
            }
            retorno++;

            return retorno;
        }
    }
}
