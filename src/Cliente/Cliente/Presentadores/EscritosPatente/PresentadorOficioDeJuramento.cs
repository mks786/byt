using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.EscritosPatente;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Documents;
using Trascend.Bolet.Cliente.Ayuda;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Presentadores.EscritosPatente
{
    class PresentadorOficioDeJuramento : PresentadorBase
    {

        private IOficioDeJuramento _ventana;


        private IAgenteServicios _agenteServicios;
        private IPatenteServicios _patenteservicios;
        private IBoletinServicios _boletinServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;


        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        Agente primerAgente = new Agente();
        Patente primerPatente = new Patente(int.MinValue);


        private IList<Agente> _Agentes;
        private IList<Patente> _patentes;
        private IList<Patente> _patentesAgregadas = new List<Patente>();


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorOficioDeJuramento(IOficioDeJuramento ventana)
        {
            try
            {
                this._ventana = ventana;
                //this._ventana.Escrito = new Pais();
                this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
                this._patenteservicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                      ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
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
                Mouse.OverrideCursor = Cursors.Wait;

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleEscritoOficioDeJuramento,
                    "");
                CargarAgente();
                CargarPatente();
                CargaTipoPoder();
                CargaBoletines();
                this._ventana.Fecha = DateTime.Now.ToString();
                GenerarString();
                this._ventana.FocoPredeterminado();
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


        /// Metodo que genera el string de codigos a enviar al .BAT
        /// </summary>
        /// <returns></returns>
        public string GenerarString()
        {

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            string parametroPatentes = "";
            if (this._patentesAgregadas.Count != 0)
            {
                parametroPatentes = ArmarStringParametroPatentes(this._patentesAgregadas);
            }

            string StringLlleno = "";


            if (null != this._ventana.Boletin)
            {
                StringLlleno += ((Boletin)this._ventana.Boletin).Id + "  ";

            }

            StringLlleno += DateTime.Parse(this._ventana.Fecha).ToShortDateString() + "  ";



            if (null != ((ListaDatosValores)this._ventana.TipoPoder))
                StringLlleno += ((ListaDatosValores)this._ventana.TipoPoder).Valor + "  ";


            if (null != ((Agente)this._ventana.Agente))
                StringLlleno += ((Agente)this._ventana.Agente).Id + "  ";


            this._ventana.String = StringLlleno + "  " + parametroPatentes;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return StringLlleno;

        }


        /// <summary>
        /// Método que realiza toda la lógica para agregar al País dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (ValidarEscrito())
                {
                    string parametroPatentes = ArmarStringParametroPatentes(this._patentesAgregadas);
                    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["RutaBatEscrito"].ToString()
                        + "\\" + ConfigurationManager.AppSettings["EscritoOficioDeJuramento"].ToString(),
                        ((Boletin)this._ventana.Boletin).Id + " "
                        + DateTime.Parse(this._ventana.Fecha).ToShortDateString() + " " +
                        ((ListaDatosValores)this._ventana.TipoPoder).Valor + " " +
                        ((Agente)this._ventana.AgenteFiltrado).Id + " " + parametroPatentes);
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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


        /// <summary>
        /// Metodo de hacer las validaciones necesarias antes de ejecutar el .bat
        /// </summary>
        /// <returns>true si es correcto, false en caso contrario</returns>
        private bool ValidarEscrito()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if ((this._ventana.AgenteFiltrado != null) && null != ((Agente)this._ventana.AgenteFiltrado).Id)
            {
                if (this._patentesAgregadas.Count != 0)
                {
                    if (((Boletin)this._ventana.Boletin != null) && (((Boletin)this._ventana.Boletin).Id != int.MinValue))
                    {
                        if (this.ValidarAgenteApoderadoDePatentes((Agente)this._ventana.AgenteFiltrado, this._patentesAgregadas))
                        {
                            retorno = true;
                        }
                        else
                        {
                            this._ventana.MensajeAlerta(string.Format(Recursos.MensajesConElUsuario.AlertaAgenteNoApareceEnPoderDePatente,
                                ((Agente)this._ventana.AgenteFiltrado).Nombre));
                        }
                    }
                    else
                    {
                        this._ventana.MensajeAlerta(string.Format(Recursos.MensajesConElUsuario.AlertaEscritoSinBoletin));
                    }
                }
                else
                {
                    this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaEscritoSinPatentes);
                }
            }
            else
            {
                this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaEscritoSinAgente);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }


        #region Boletin y Tipo de Poder

        /// <summary>
        /// Método que carga los boletines registrados
        /// </summary>
        private void CargaBoletines()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Boletin primerBoletin = new Boletin(int.MinValue);
            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            boletines.Insert(0, primerBoletin);
            this._ventana.Boletines = boletines;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        /// <summary>
        /// Método que carga los tipo poder registrados
        /// </summary>
        private void CargaTipoPoder()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<ListaDatosValores> tiposEscritos =
               this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaTipoPoderPatente));
            this._ventana.TiposPoder = tiposEscritos;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        #endregion


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


        #region Agente

        /// <summary>
        /// Método que se encarga de cambiar un agente
        /// </summary>
        /// <returns></returns>
        public bool CambiarAgente()
        {
            bool retorno = false;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.Agente != null)
                {
                    //this._ventana.Agente =
                    //    this._agenteServicios.ConsultarPorId((Agente)this._ventana.AgenteFiltrado);
                    this._ventana.Agente = this._ventana.AgenteFiltrado;
                    this._ventana.NombreAgente = ((Agente)this._ventana.AgenteFiltrado).Nombre;
                    this._Agentes.Add((Agente)this._ventana.AgenteFiltrado);
                    GenerarString();
                    retorno = true;
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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }

        /// <summary>
        /// Método que se encarga de Cargar el agente al iniciar la pantalla
        /// </summary>
        private void CargarAgente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._Agentes = new List<Agente>();
            this._Agentes = this._agenteServicios.ObtenerAgentesSinPoderesFiltro(new Agente("MP"));
            this._Agentes.Insert(0, this.primerAgente);
            this._ventana.AgentesFiltrados = this._Agentes;
            this._ventana.AgenteFiltrado = this.BuscarAgente(this._Agentes, this._Agentes[1]);

            this._ventana.Agente = this._Agentes[1];


            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que consulta los agentes que cumplan con el filtro
        /// </summary>
        public void ConsultarAgente()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((!this._ventana.IdAgenteFiltrar.Equals("")) || (!this._ventana.NombreAgenteFiltrar.Equals("")))
                {
                    Agente AgenteConsulta = new Agente();
                    AgenteConsulta.Id = !this._ventana.IdAgenteFiltrar.Equals("") ?
                        this._ventana.IdAgenteFiltrar : string.Empty;
                    AgenteConsulta.Nombre = !this._ventana.NombreAgenteFiltrar.Equals("") ?
                        this._ventana.NombreAgenteFiltrar.ToUpper() : string.Empty;

                    this._Agentes = this._agenteServicios.ObtenerAgentesSinPoderesFiltro(AgenteConsulta);
                    this._Agentes.Insert(0, this.primerAgente);
                    this._ventana.AgentesFiltrados = this._Agentes;
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        #endregion


        #region Patente

        /// <summary>
        /// Método que se encarga de cambiar la marca seleccionada
        /// </summary>
        /// <returns></returns>
        public bool CambiarPatente()
        {
            bool retorno = false;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.PatenteFiltrado != null)
                {
                    if (((Patente)this._ventana.PatenteFiltrado).Id != int.MinValue)
                    {
                        this._ventana.Patente =
                            this._patenteservicios.ConsultarPatenteConTodo((Patente)this._ventana.PatenteFiltrado);
                    }
                    this._ventana.NombrePatente = ((Patente)this._ventana.PatenteFiltrado).Descripcion;
                    retorno = true;
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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }

        /// <summary>
        /// Método que realiza la carga inicial de las marcas
        /// </summary>
        private void CargarPatente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._patentes = new List<Patente>();

            this._patentes.Add(this.primerPatente);
            this._ventana.Patente = this.primerPatente;
            this._ventana.PatentesFiltrados = this._patentes;
            this._ventana.PatenteFiltrado = this.primerPatente;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        /// <summary>
        /// Método que se encarga de realizar la consulta de las marcas
        /// </summary>
        public void ConsultarPatente()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((!this._ventana.IdPatenteFiltrar.Equals("")) || (!this._ventana.NombrePatenteFiltrar.Equals("")))
                {
                    Patente PatenteConsulta = new Patente();
                    PatenteConsulta.Id = !this._ventana.IdPatenteFiltrar.Equals("") ?
                        int.Parse(this._ventana.IdPatenteFiltrar) : 0;
                    PatenteConsulta.Descripcion = !this._ventana.NombrePatenteFiltrar.Equals("") ?
                        this._ventana.NombrePatenteFiltrar.ToUpper() : string.Empty;

                    this._patentes = this._patenteservicios.ObtenerPatentesFiltro(PatenteConsulta);
                    this._patentes.Insert(0, this.primerPatente);
                    this._ventana.PatentesFiltrados = this._patentes;
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Método que se encarga de agregar la marca seleccionada a la lista de marcas agregadas
        /// </summary>
        public void AgregarPatente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((this._ventana.PatenteFiltrado != null) && (((Patente)this._ventana.PatenteFiltrado).Id != int.MinValue))
                {
                    if (null != ((Patente)this._ventana.PatenteFiltrado).Poder)
                    {
                        this._patentesAgregadas.Insert(0, (Patente)this._ventana.PatenteFiltrado);
                        this._patentes.Remove((Patente)this._ventana.PatenteFiltrado);
                    }
                    else
                        this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.PatenteSinPoder);
                }

                this._ventana.PatentesAgregadas = null;
                this._ventana.PatentesAgregadas = this._patentesAgregadas;

                this._ventana.PatentesFiltrados = null;
                this._ventana.PatentesFiltrados = this._patentes;
                GenerarString();

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
        /// Método que se encarga de eliminar la marca seleccionada de la lista de marcas agregadas
        /// </summary>
        public void EliminarPatente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.PatenteAgregada != null)
                {
                    this._patentesAgregadas.Remove((Patente)this._ventana.PatenteAgregada);
                    this._patentes.Insert(0, (Patente)this._ventana.PatenteAgregada);
                }

                this._ventana.PatentesAgregadas = null;
                this._ventana.PatentesAgregadas = this._patentesAgregadas;

                this._ventana.PatentesFiltrados = null;
                this._ventana.PatentesFiltrados = this._patentes;
                GenerarString();

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

        #endregion

    }
}
