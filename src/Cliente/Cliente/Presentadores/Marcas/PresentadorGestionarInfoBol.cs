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
using Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosDeNombre;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosDeDomicilio;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosPeticionario;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Cesiones;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Licencias;
using Trascend.Bolet.Cliente.Ventanas.Renovaciones;



namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorGestionarInfoBol : PresentadorBase
    {

        private IGestionarInfoBol _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IInfoBolServicios _infoBolServicios;
        private IBoletinServicios _boletinServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private ITipoInfobolServicios _tipoInfobolServicios;
        private IOperacionServicios _operacionServicios;
        private IList<TipoInfobol> _infoboles;
        private bool _nuevaInfoBol = false;
        private bool _tieneListaCambios = false;
        private ICambioDeNombreServicios _cambioNombreServicios;
        private ILicenciaServicios _licenciaServicios;
        private ICesionServicios _cesionServicios;
        private IFusionServicios _fusionServicios;
        private ICambioDeDomicilioServicios _cambioDomicilioServicios;
        private IRenovacionServicios _renovacionServicios;
        private ICambioPeticionarioServicios _peticionarioServicios;

        private object _ventanaPadreListaInfoboles = null;
        


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorGestionarInfoBol(IGestionarInfoBol ventana, object infoBol)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                

                this._ventana.InfoBol = null != (InfoBol)infoBol ? (InfoBol)infoBol : new InfoBol();

                /*if (((InfoBol)infoBol).Id == int.MinValue)
                {
                    this._nuevaInfoBol = true;
                    
                }*/

                if (((InfoBol)infoBol).TipoInfobol == null)
                {
                    this._nuevaInfoBol = true;

                }

                this._infoBolServicios = (IInfoBolServicios)Activator.GetObject(typeof(IInfoBolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._tipoInfobolServicios = (ITipoInfobolServicios)Activator.GetObject(typeof(ITipoInfobolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoInfobolServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                this._cambioNombreServicios = (ICambioDeNombreServicios)Activator.GetObject(typeof(ICambioDeNombreServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeNombreServicios"]);

                this._infoboles = this._tipoInfobolServicios.ConsultarTodos();
                this._ventana.Tipos = null;
                this._ventana.Tipos = this._infoboles;

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
        /// Constructor predeterminado que admite una ventana Padre
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorGestionarInfoBol(IGestionarInfoBol ventana, object infoBol, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;

                this._ventana.InfoBol = null != (InfoBol)infoBol ? (InfoBol)infoBol : new InfoBol();

                if (((InfoBol)infoBol).Id == int.MinValue)
                    this._nuevaInfoBol = true;

                this._infoBolServicios = (IInfoBolServicios)Activator.GetObject(typeof(IInfoBolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._tipoInfobolServicios = (ITipoInfobolServicios)Activator.GetObject(typeof(ITipoInfobolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoInfobolServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                this._cambioNombreServicios = (ICambioDeNombreServicios)Activator.GetObject(typeof(ICambioDeNombreServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeNombreServicios"]);
                this._licenciaServicios = (ILicenciaServicios)Activator.GetObject(typeof(ILicenciaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["LicenciaServicios"]);
                this._cesionServicios = (ICesionServicios)Activator.GetObject(typeof(ICesionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CesionServicios"]);
                this._fusionServicios = (IFusionServicios)Activator.GetObject(typeof(IFusionServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FusionServicios"]);
                this._cambioDomicilioServicios = (ICambioDeDomicilioServicios)Activator.GetObject(typeof(ICambioDeDomicilioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeDomicilioServicios"]);
                this._renovacionServicios = (IRenovacionServicios)Activator.GetObject(typeof(IRenovacionServicios),
                      ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RenovacionServicios"]);
                this._peticionarioServicios = (ICambioPeticionarioServicios)Activator.GetObject(typeof(ICambioPeticionarioServicios),
                      ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioPeticionarioServicios"]);

                this._infoboles = this._tipoInfobolServicios.ConsultarTodos();
                this._ventana.Tipos = null;
                this._ventana.Tipos = this._infoboles;

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


        public PresentadorGestionarInfoBol(IGestionarInfoBol ventana, object infoBol, object ventanaPadre, object ventanaPadreListaInfoboles)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventanaPadreListaInfoboles = ventanaPadreListaInfoboles;

                this._ventana.InfoBol = null != (InfoBol)infoBol ? (InfoBol)infoBol : new InfoBol();

                //if (((InfoBol)infoBol).Id == int.MinValue)
                //    this._nuevaInfoBol = true;

                if (((InfoBol)infoBol).TipoInfobol == null)
                {
                    this._nuevaInfoBol = true;

                }


                this._infoBolServicios = (IInfoBolServicios)Activator.GetObject(typeof(IInfoBolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._tipoInfobolServicios = (ITipoInfobolServicios)Activator.GetObject(typeof(ITipoInfobolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoInfobolServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                this._cambioNombreServicios = (ICambioDeNombreServicios)Activator.GetObject(typeof(ICambioDeNombreServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeNombreServicios"]);
                this._licenciaServicios = (ILicenciaServicios)Activator.GetObject(typeof(ILicenciaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["LicenciaServicios"]);
                this._cesionServicios = (ICesionServicios)Activator.GetObject(typeof(ICesionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CesionServicios"]);
                this._fusionServicios = (IFusionServicios)Activator.GetObject(typeof(IFusionServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FusionServicios"]);
                this._cambioDomicilioServicios = (ICambioDeDomicilioServicios)Activator.GetObject(typeof(ICambioDeDomicilioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeDomicilioServicios"]);
                this._renovacionServicios = (IRenovacionServicios)Activator.GetObject(typeof(IRenovacionServicios),
                      ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RenovacionServicios"]);
                this._peticionarioServicios = (ICambioPeticionarioServicios)Activator.GetObject(typeof(ICambioPeticionarioServicios),
                      ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioPeticionarioServicios"]);

                this._infoboles = this._tipoInfobolServicios.ConsultarTodos();
                this._ventana.Tipos = null;
                this._ventana.Tipos = this._infoboles;

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionalInfoBol,
                    Recursos.Ids.AgregarInfoBol);

                if (this._nuevaInfoBol)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                    this._ventana.OculatarControlesAlAgregar();
                }

                IList<ListaDatosDominio> tomos = this._listaDatosDominioServicios.
                ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTomo));
                ListaDatosDominio primerTomo = new ListaDatosDominio();
                primerTomo.Id = "NGN";
                tomos.Insert(0, primerTomo);
                this._ventana.Tomos = null;
                this._ventana.Tomos = tomos;

                if (!this._nuevaInfoBol)
                    this._ventana.Tomo = this.BuscarTomos(tomos, ((InfoBol)this._ventana.InfoBol).Tomo);

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                this._ventana.Boletines = null;
                this._ventana.Boletines = boletines;

                if (!this._nuevaInfoBol)
                {
                    this._ventana.Boletin = this.BuscarBoletin(boletines, ((InfoBol)this._ventana.InfoBol).Boletin);
                    this._ventana.Tipo = this.BuscarTipoInfobol(this._infoboles, ((InfoBol)this._ventana.InfoBol).TipoInfobol);
                    //Para asegurar que el Infobol tenga un tipo de Infobol
                    //((InfoBol)this._ventana.InfoBol).TipoInfobol = (TipoInfobol)this._ventana.Tipo;
                    if (((InfoBol)this._ventana.InfoBol).Cambio != 0)
                        this._ventana.TextoCambio = ((InfoBol)this._ventana.InfoBol).Cambio.ToString();
                    else
                        this._ventana.TextoCambio = null;
                }

                //this._ventana.BorrarTextoCambio();

                if ((TipoInfobol)this._ventana.Tipo != null)
                {
                    TipoInfobol tipoInfobolAux = (TipoInfobol)this._ventana.Tipo;
                    if ((tipoInfobolAux.TipoOperacion != null) && (!tipoInfobolAux.TipoOperacion.Equals(String.Empty)))
                    {
                        this._ventana.ActivarBotonRevisar(true);
                    }
                }
                

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
        /// Método que realiza toda la lógica para agregar el InfoBol dentro de la base de datos
        /// </summary>
        public bool Aceptar()
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Agregar o modificar datos
                else
                {
                    InfoBol infoBol = (InfoBol)this._ventana.InfoBol;

                    infoBol.Tomo = ((ListaDatosDominio)this._ventana.Tomo).Id;
                    infoBol.Boletin = (Boletin)this._ventana.Boletin;
                    infoBol.TimeStamp = System.DateTime.Now;
                    infoBol.Usuario = UsuarioLogeado;
                    infoBol.Cambio = !string.IsNullOrEmpty(this._ventana.TextoCambio) ? int.Parse(this._ventana.TextoCambio) : 0;

                    if (this._nuevaInfoBol)
                    {
                        infoBol.TipoInfobol = (TipoInfobol)this._ventana.Tipo;
                        ((InfoBol)this._ventana.InfoBol).Marca.InfoBoles.Add(infoBol);
                        infoBol.Id = ((InfoBol)this._ventana.InfoBol).Marca.Id;
                    }

                    exitoso = this._infoBolServicios.InsertarOModificar(infoBol, UsuarioLogeado.Hash);

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

            return exitoso;
        }


        /// <summary>
        /// Método que se encarga de eliminar un InfoBol
        /// </summary>
        /// <returns>true si se realizó correctamente</returns>
        public bool Eliminar()
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ((InfoBol)this._ventana.InfoBol).Marca.InfoBoles.Remove((InfoBol)this._ventana.InfoBol);

                if (this._infoBolServicios.Eliminar((InfoBol)this._ventana.InfoBol, UsuarioLogeado.Hash))
                {
                    exitoso = true;
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

            return exitoso;
        }


        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column, ListView ListaResultados)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Métodos que se encarga de mostrar la ventana de lista de InfoBol
        /// </summary>
        public void irListaInfoBol()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //---

            Marca marcaAux = ((InfoBol)this._ventana.InfoBol).Marca;
            IList<InfoBol> infoboles = this._infoBolServicios.ConsultarInfoBolesPorMarca(marcaAux);
            marcaAux.InfoBoles = infoboles;
            //this.Navegar(new ListaInfoBoles(marcaAux));
            this.Navegar(new ListaInfoBoles(marcaAux, this._ventanaPadreListaInfoboles));

            //---
            //this.Navegar(new ListaInfoBoles(((InfoBol)this._ventana.InfoBol).Marca));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se encarga de cambiar el Cambio de la ventana
        /// </summary>
        public void CambiarCambio()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Operacion)this._ventana.Cambio != null)
                {
                    this._ventana.TextoCambio = ((Operacion)this._ventana.Cambio).Interno.ToString();
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.BorrarTextoCambio();
            }
        }



        /// <summary>
        /// Metodo que selecciona el tipo de cambio para visualizarlo con todos sus datos
        /// </summary>
        public void SeleccionarServicio()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Operacion operacionActual = new Operacion();
            string idServicio = String.Empty;
            int idGenerico;

            if ((Operacion)this._ventana.Cambio != null)
            {
                operacionActual = (Operacion)this._ventana.Cambio;
                //El identificador del servicio que voy a visualizar
                idServicio = operacionActual.Servicio.Id;
                //Codigo interno del servicio a consultar
                idGenerico = operacionActual.Interno;
                switch (idServicio)
                {
                    //redirecciona a Cesion
                    case "CS":
                        Cesion cesion = new Cesion();
                        cesion.Id = idGenerico;
                        //this.Navegar(new GestionarCesion(this._cesionServicios.
                        //                        ObtenerCesionFiltro(cesion)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarCesion(this._cesionServicios.
                                                ObtenerCesionFiltro(cesion)[0], Visibility.Collapsed, this._ventana));

                        break;

                    //redirecciona a Fusion
                    case "FU":
                        Fusion fusion = new Fusion();
                        fusion.Id = idGenerico;
                        //this.Navegar(new GestionarFusion((this._fusionServicios.
                                                        //ObtenerFusionFiltro(fusion)[0]), Visibility.Collapsed));
                        this.Navegar(new GestionarFusion((this._fusionServicios.
                                                        ObtenerFusionFiltro(fusion)[0]), Visibility.Collapsed, this._ventana));

                        break;

                    //redirecciona a CambioDeDomicilio
                    case "CD":
                        CambioDeDomicilio cambioDeDomicilio = new CambioDeDomicilio();
                        cambioDeDomicilio.Id = idGenerico;
                        //this.Navegar(new GestionarCambioDeDomicilio(this._cambioDomicilioServicios.
                        //                   ObtenerCambioDeDomicilioFiltro(cambioDeDomicilio)[0], Visibility.Collapsed));

                        this.Navegar(new GestionarCambioDeDomicilio(this._cambioDomicilioServicios.
                                           ObtenerCambioDeDomicilioFiltro(cambioDeDomicilio)[0], Visibility.Collapsed, this._ventana));

                        break;

                    //redirecciona a CambioDeNombre
                    case "CN":
                        CambioDeNombre cambioDeNombre = new CambioDeNombre();
                        cambioDeNombre.Id = idGenerico;
                        this.Navegar(new GestionarCambioDeNombre(this._cambioNombreServicios.
                                               ObtenerCambioDeNombreFiltro(cambioDeNombre)[0], Visibility.Collapsed,this._ventana));
                        break;

                    //redirecciona a Renovacion
                    case "RN":
                        Renovacion renovacion = new Renovacion();
                        renovacion.Id = idGenerico;
                        //this.Navegar(new GestionarRenovacion(this._renovacionServicios.
                        //                        ObtenerRenovacionFiltro(renovacion)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarRenovacion(this._renovacionServicios.
                                                ObtenerRenovacionFiltro(renovacion)[0], Visibility.Collapsed, this._ventana));

                        break;

                    //redirecciona a Licencia
                    case "LU":
                        Licencia licencia = new Licencia();
                        licencia.Id = idGenerico;
                        //this.Navegar(new GestionarLicencia(this._licenciaServicios.
                        //                                    ObtenerLicenciaFiltro(licencia)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarLicencia(this._licenciaServicios.
                                                            ObtenerLicenciaFiltro(licencia)[0], Visibility.Collapsed, this._ventana));

                        break;

                    //redirecciona a CambioPeticionario
                    case "CT":
                        CambioPeticionario cambioPeticionario = new CambioPeticionario();
                        cambioPeticionario.Id = idGenerico;
                        //this.Navegar(new GestionarCambioPeticionario(this._peticionarioServicios.
                        //                    ObtenerCambioPeticionarioFiltro(cambioPeticionario)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarCambioPeticionario(this._peticionarioServicios.
                                            ObtenerCambioPeticionarioFiltro(cambioPeticionario)[0], Visibility.Collapsed, this._ventana));

                        break;

                    //redirecciona a Abandono
                    case "AB":

                        //Operacion operacion = this._operacionServicios.
                        //                                ConsultarPorId((Operacion)this._ventana.OperacionSeleccionado);
                        //operacion.Marca = new Marca(((Operacion)this._ventana.OperacionSeleccionado).CodigoAplicada);
                        //this.Navegar(new GestionarAbandono(operacion, Visibility.Collapsed));


                        break;

                }

            }



            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo que devuelve la listaCambio
        /// </summary>
        /// <returns>lista de Cambios</returns>
        public bool TieneElementosListaCambio()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion


            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return this._tieneListaCambios;
        }


        /// <summary>
        /// Método que se encarga de cargar los datos de Cambio
        /// </summary>
        public void CargarCambio()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Operacion operacion = new Operacion();
            operacion.Marca = new Marca(((InfoBol)this._ventana.InfoBol).Marca.Id);

            if (((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.CD") || ((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.CN") ||
                 ((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.FU") || ((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.TP") ||
                 ((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.REN") || ((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.LU"))
            {
                if (((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.TP"))
                {
                    operacion.Servicio = new Servicio("CS");
                }
                else if (((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.REN"))
                {
                    operacion.Servicio = new Servicio("RN");
                }
                else
                {
                    operacion.Servicio = new Servicio(((TipoInfobol)this._ventana.Tipo).Id.Substring(((TipoInfobol)this._ventana.Tipo).Id.Length - 2));
                }

                this._ventana.Cambios = null;
                this._ventana.Cambios = this._operacionServicios.ObtenerOperacionPorMarcaYServicio(operacion);

                if (((IList<Operacion>)this._ventana.Cambios).Count > 0)
                    this._tieneListaCambios = true;
                else
                    this._tieneListaCambios = false;

            }
            else
                this._tieneListaCambios = false;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo que se utiliza para levantar la ventana de acuerdo el cambio seleccionado por el usuario
        /// en la ventana de GestionarInfobol
        /// </summary>
        public void VerCambio()
        {

        }

    }
}
