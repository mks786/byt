using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using System.Windows;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Threading;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using System.Collections.Generic;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Documents;
using Trascend.Bolet.Cliente.Ayuda;


namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorGestionarFechaDeMarca : PresentadorBase
    {
        private IGestionarFechaDeMarca _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private FechaMarca _fechaMarca;
        private ITipoFechaServicios _tipoFechaServicios;
        private IFechaMarcaServicios _fechaMarcaServicios;
        private ICartaServicios _cartaServicios;
        private bool _nuevaFecha = false;
        private object _ventanaPadreLista;

        /// <summary>
        /// Constructor predeterminado que admite una ventana Padre
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        //public PresentadorGestionarFechaDeMarca(IGestionarFechaDeMarca ventana, object fechaMarca, object ventanaPadre)
        public PresentadorGestionarFechaDeMarca(IGestionarFechaDeMarca ventana, object fechaMarca, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._fechaMarca = (FechaMarca)fechaMarca;
                this._ventana.FechaMarca = fechaMarca;

                if (((FechaMarca)fechaMarca).Tipo == null)
                    this._nuevaFecha = true;

                this._tipoFechaServicios = (ITipoFechaServicios)Activator.GetObject(typeof(ITipoFechaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoFechaServicios"]);

                this._fechaMarcaServicios = (IFechaMarcaServicios)Activator.GetObject(typeof(IFechaMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FechaMarcaServicios"]);

                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
                
                

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
        }


        public PresentadorGestionarFechaDeMarca(IGestionarFechaDeMarca ventana, 
                                                object fechaMarca, 
                                                object ventanaPadre, 
                                                object ventanaPadreLista)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._fechaMarca = (FechaMarca)fechaMarca;
                this._ventana.FechaMarca = fechaMarca;
                this._ventanaPadreLista = ventanaPadreLista;

                if (((FechaMarca)fechaMarca).Tipo == null)
                    this._nuevaFecha = true;

                this._tipoFechaServicios = (ITipoFechaServicios)Activator.GetObject(typeof(ITipoFechaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoFechaServicios"]);

                this._fechaMarcaServicios = (IFechaMarcaServicios)Activator.GetObject(typeof(IFechaMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FechaMarcaServicios"]);

                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);



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
        }






        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();

                FechaMarca fechaMarca = this._fechaMarca;

                IList<TipoFecha> listaTiposDeFecha = this._tipoFechaServicios.ConsultarTodos();
                TipoFecha primerTipoFecha = new TipoFecha("NGN");
                listaTiposDeFecha.Insert(0, primerTipoFecha);
                this._ventana.TiposDeFechas = listaTiposDeFecha;
                this._ventana.TipoDeFecha = this.BuscarTipoFecha(listaTiposDeFecha, fechaMarca.Tipo);

                #region Codigo Comentado
                //Marca marca = this._marca;
                //CertificadoMarca certificado = this._certificado;

                //IList<Registrador> registradores = this._registradorServicios.ConsultarTodos();
                //Registrador primerRegistrador = new Registrador();
                //primerRegistrador.Id = "NGN";
                //registradores.Insert(0, primerRegistrador);
                //this._ventana.Registradores = registradores;
                //this._ventana.Registrador = this.BuscarRegistrador(registradores, certificado.CodRegistrador);

                //if (certificado.Operacion == null)
                //{
                //    this._ventana.MostrarBotonEliminar(true);
                //} 
                #endregion



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


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarFechaMarca,
                       Recursos.Ids.GestionarCertificadoMarca);
        }



        public bool Aceptar()
        {
            bool exitoso = false;
            bool existe = false;
            Carta cartaFechaMarca = null;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FechaMarca fechaMarca = (FechaMarca)this._ventana.FechaMarca;

                fechaMarca.Tipo = (this._ventana.TipoDeFecha != null) && (!((TipoFecha)this._ventana.TipoDeFecha).Id.Equals("NGN"))
                    ? (TipoFecha)this._ventana.TipoDeFecha : null;

                if ((this._ventana.IdCorrespondencia != null) && (!this._ventana.IdCorrespondencia.Equals("")))
                {
                    Carta cartaAux = new Carta();
                    cartaAux.Id = (int.Parse(this._ventana.IdCorrespondencia));
                    existe = this._cartaServicios.VerificarExistencia(cartaAux);
                    if (existe)
                    {
                        IList<Carta> listaCorrespondencia = this._cartaServicios.ObtenerCartasFiltro(cartaAux);
                        cartaFechaMarca = listaCorrespondencia[0];
                        fechaMarca.Correspondencia = cartaFechaMarca;
                    }
                    else
                    {
                        fechaMarca.Correspondencia = null;
                    }


                }

                if (fechaMarca.Tipo != null)
                {
                    exitoso = this._fechaMarcaServicios.InsertarOModificar(fechaMarca, UsuarioLogeado.Hash);
                }
                else
                    this._ventana.Mensaje("La Fecha no posee un Tipo de Fecha", 0);


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
         
              
            return exitoso;
        }



        public bool Eliminar()
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FechaMarca fechaMarcaEliminar = (FechaMarca)this._ventana.FechaMarca;

                exitoso = this._fechaMarcaServicios.Eliminar(fechaMarcaEliminar, UsuarioLogeado.Hash);

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



            return exitoso;
        }


        public void irListaFechasDeMarca()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            


            this.Navegar(new ListaFechasDeMarca(((FechaMarca)this._ventana.FechaMarca).Marca,this._ventanaPadreLista));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        
    }
}
