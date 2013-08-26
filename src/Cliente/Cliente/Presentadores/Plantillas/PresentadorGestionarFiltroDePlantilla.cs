using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;
using Diginsoft.Bolet.ObjetosComunes.Entidades;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Plantillas;
using Trascend.Bolet.Cliente.Ventanas.Plantillas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Plantillas
{
    class PresentadorGestionarFiltroDePlantilla: PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IGestionarFiltroDePlantilla _ventana;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IFiltroPlantillaServicios _filtroPlantillaServicios;
        private FiltroPlantilla _filtro;
        private bool _nuevoFiltro = false;
        private object _ventanaPadreMaestroPlantillas;


        public PresentadorGestionarFiltroDePlantilla(IGestionarFiltroDePlantilla ventana, object filtro, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._filtro = (FiltroPlantilla)filtro;

                if(((FiltroPlantilla)filtro).NombreCampoFiltro == null)
                {
                    this._nuevoFiltro = true;
                }

                this._filtroPlantillaServicios = (IFiltroPlantillaServicios)Activator.GetObject(typeof(IFiltroPlantillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FiltroPlantillaServicios"]);

                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
               

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        /// <summary>
        /// Constructor por defecto que recibe un filtro nuevo o seleccionado de la lista de filtros
        /// Recibe la ventana actual, la ventana padre y la ventana de Gestion de Maestro de Plantillas
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="filtro">Filtro seleccionado, existente o uno nuevo</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        /// <param name="ventanaPadreMaestroPlantillas">Ventana que precede al listado de filtros de la plantilla</param>
        public PresentadorGestionarFiltroDePlantilla(IGestionarFiltroDePlantilla ventana, object filtro, object ventanaPadre, object ventanaPadreMaestroPlantillas)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventanaPadreMaestroPlantillas = ventanaPadreMaestroPlantillas;
                this._filtro = (FiltroPlantilla)filtro;

                this._ventana.FiltroPlantilla = (FiltroPlantilla)filtro;

                if (((FiltroPlantilla)filtro).NombreCampoFiltro == null)
                {
                    this._nuevoFiltro = true;
                }
                
                this._filtroPlantillaServicios = (IFiltroPlantillaServicios)Activator.GetObject(typeof(IFiltroPlantillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FiltroPlantillaServicios"]);

                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Metodo que se encarga de cargar los valores en la pagina y mostrarlos al usuario
        /// </summary>
        public void CargarPagina()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();
                                              
                IList<ListaDatosValores> tiposDeDatosDeFiltros = this._listaDatosValoresServicios.
                   ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiTipoDatoFiltroPlantilla));
                this._ventana.TiposDeDatosFiltro = tiposDeDatosDeFiltros;
                if (!this._nuevoFiltro)
                {
                    ListaDatosValores tipoDeDatos = new ListaDatosValores();
                    tipoDeDatos.Valor = this._filtro.TipoDatoCampoFiltro;
                    //this._ventana.TipoDeDatosFiltro = this.BuscarListaDeDatosValores(tiposDeDatosDeFiltros, tipoDeDatos);
                    tipoDeDatos = this.BuscarListaDeDatosValores((IList<ListaDatosValores>)this._ventana.TiposDeDatosFiltro, tipoDeDatos);
                    this._ventana.TipoDeDatosFiltro = tipoDeDatos;
                }

                IList<ListaDatosValores> tiposDeFiltros = this._listaDatosValoresServicios.
                    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiTipoDeFiltroPlantilla));
                this._ventana.TiposDeFiltro = tiposDeFiltros;
                if (!this._nuevoFiltro)
                {
                    ListaDatosValores tipoDeFiltro = new ListaDatosValores();
                    tipoDeFiltro.Valor = this._filtro.TipoDeFiltro;
                    this._ventana.TipoDeFiltro = this.BuscarListaDeDatosValores(tiposDeFiltros, tipoDeFiltro);
                }
                
                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListadoFiltrosEncabezadoPlantilla,
                Recursos.Ids.ListaFiltrosPlantilla);
        }



        /// <summary>
        /// Metodo para actualizar o insertar un filtro de encabezado 
        /// </summary>
        /// <returns></returns>
        public bool Aceptar()
        {
            bool exitoso = false;
            int contador, proximoValor;
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

                    FiltroPlantilla filtroPlantilla = (FiltroPlantilla)this._ventana.FiltroPlantilla;

                    filtroPlantilla.TipoDatoCampoFiltro = this._ventana.TipoDeDatosFiltro != null ? 
                        ((ListaDatosValores)this._ventana.TipoDeDatosFiltro).Descripcion : null;

                    if (this._nuevoFiltro)
                    {
                        IList<FiltroPlantilla> listaFiltros = this._filtroPlantillaServicios.ConsultarTodos();
                        contador = listaFiltros.Count;
                        proximoValor = contador+1;
                        filtroPlantilla.Id = proximoValor;
                    }

                    if (filtroPlantilla.NombreCampoFiltro != null && !filtroPlantilla.NombreCampoFiltro.Equals(String.Empty))
                    {
                        if ((filtroPlantilla.NombreVariableFiltro != null) && (!filtroPlantilla.NombreVariableFiltro.Equals(String.Empty)))
                        {
                            if (this._ventana.TipoDeDatosFiltro != null)
                            {
                                exitoso = this._filtroPlantillaServicios.InsertarOModificar(filtroPlantilla, UsuarioLogeado.Hash);
                            }
                            else
                            {
                                this._ventana.MensajeAlerta("Asigne un tipo de datos al campo filtro", 0);
                            }
                        }
                        else
                        {
                            this._ventana.MensajeAlerta("Indique el nombre para la variable del filtro", 0);
                        }
                    }
                    else
                    {
                        this._ventana.MensajeAlerta("Indique el campo filtro para este Encabezado", 0);
                    }


                    #region CODIGO COMENTADO
                    //InfoBol infoBol = (InfoBol)this._ventana.InfoBol;

                    //infoBol.Tomo = ((ListaDatosDominio)this._ventana.Tomo).Id;
                    //infoBol.Boletin = (Boletin)this._ventana.Boletin;
                    //infoBol.TimeStamp = System.DateTime.Now;
                    //infoBol.Usuario = UsuarioLogeado;
                    //infoBol.Cambio = !string.IsNullOrEmpty(this._ventana.TextoCambio) ? int.Parse(this._ventana.TextoCambio) : 0;

                    //if (this._nuevaInfoBol)
                    //{
                    //    infoBol.TipoInfobol = (TipoInfobol)this._ventana.Tipo;
                    //    ((InfoBol)this._ventana.InfoBol).Marca.InfoBoles.Add(infoBol);
                    //    infoBol.Id = ((InfoBol)this._ventana.InfoBol).Marca.Id;
                    //}

                    //exitoso = this._infoBolServicios.InsertarOModificar(infoBol, UsuarioLogeado.Hash); 
                    #endregion

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
        /// Metodo que vuelve a cargar la lista de los filtros de encabezados despues de hacer una actualizacion en la base de datos
        /// </summary>
        public void irListaListaFiltros()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String tipoFiltro = String.Empty;

            #region CODIGO COMENTADO
            //---

            //Marca marcaAux = ((InfoBol)this._ventana.InfoBol).Marca;
            //IList<InfoBol> infoboles = this._infoBolServicios.ConsultarInfoBolesPorMarca(marcaAux);
            //marcaAux.InfoBoles = infoboles;
            ////this.Navegar(new ListaInfoBoles(marcaAux));
            //this.Navegar(new ListaInfoBoles(marcaAux, this._ventanaPadreListaInfoboles));

            //---
            //this.Navegar(new ListaInfoBoles(((InfoBol)this._ventana.InfoBol).Marca)); 
            #endregion

            tipoFiltro = ((FiltroPlantilla)this._ventana.FiltroPlantilla).TipoDeFiltro;
            Plantilla plantilla = ((FiltroPlantilla)this._ventana.FiltroPlantilla).Plantilla;

            if (tipoFiltro.Equals("E"))
            {
                this.Navegar(new ListaValoresEncabezado(plantilla, _ventanaPadreMaestroPlantillas)); 
            }
            else if (tipoFiltro.Equals("D"))
            {
                this.Navegar(new ListaValoresDetalle(plantilla, _ventanaPadreMaestroPlantillas));
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo para eliminar un filtro de plantilla seleccionado
        /// </summary>
        /// <returns></returns>
        public bool Eliminar()
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._filtroPlantillaServicios.Eliminar((FiltroPlantilla)this._ventana.FiltroPlantilla, UsuarioLogeado.Hash))
                {
                    exitoso = true;
                }


                //((InfoBol)this._ventana.InfoBol).Marca.InfoBoles.Remove((InfoBol)this._ventana.InfoBol);

                //if (this._infoBolServicios.Eliminar((InfoBol)this._ventana.InfoBol, UsuarioLogeado.Hash))
                //{
                //    exitoso = true;
                //}

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



    }
}
