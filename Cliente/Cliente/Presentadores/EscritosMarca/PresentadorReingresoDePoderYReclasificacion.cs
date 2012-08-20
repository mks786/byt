using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.EscritosMarca;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Documents;
using Trascend.Bolet.Cliente.Ayuda;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Presentadores.EscritosMarca
{
    class PresentadorReingresoDePoderYReclasificacion : PresentadorBase
    {
        private IReingresoDePoderYReclasificacion _ventana;

        private IAgenteServicios _agenteServicios;
        private IMarcaServicios _marcaServicios;
        private IBoletinServicios _boletinServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        Agente primerAgente = new Agente();
        Marca primerMarca = new Marca(int.MinValue);

        private IList<Agente> _Agentes;
        private IList<Marca> _marcas;
        private IList<Marca> _marcasAgregadas = new List<Marca>();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorReingresoDePoderYReclasificacion(IReingresoDePoderYReclasificacion ventana)
        {
            try
            {
                this._ventana = ventana;
                //this._ventana.Escrito = new Pais();
                this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
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
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleEscritoReingresoDePoderYReclasificacion,
                    "");
                CargarAgente();
                CargarMarca();
                CargaBoletines();
                CargaCombo();
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

        /// <summary>
        /// Metodo que genera el string de codigos a enviar al .BAT
        /// </summary>
        /// <returns></returns>
        public string GenerarString()
        {

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            string parametroMarcas = "";
            if (this._marcasAgregadas.Count != 0)
            {
                parametroMarcas = ArmarStringParametroMarcas(this._marcasAgregadas);
            }

            string StringLlleno = "";
            if (null != this._ventana.Boletin)
            {
                StringLlleno += ((Boletin)this._ventana.Boletin).Id + "  ";
                if (null != this._ventana.Resolucion)
                    StringLlleno += ((Resolucion)this._ventana.Resolucion).Id + "  ";
            }
            if (null != this._ventana.CantidadNumeralSelec)
                StringLlleno += ((ListaDatosValores)this._ventana.CantidadNumeralSelec).Valor + "  ";
            StringLlleno += this._ventana.Numerales + "  ";
            if (null != this._ventana.TipoDePoder)
                StringLlleno += ((ListaDatosValores)this._ventana.TipoDePoder).Valor + "  ";
            if (null != this._ventana.Reclasificacion)
                StringLlleno += ((ListaDatosValores)this._ventana.Reclasificacion).Valor + "  ";
            if (null != this._ventana.TipoDeEtiqueta)
                StringLlleno += ((ListaDatosValores)this._ventana.TipoDeEtiqueta).Valor + "  ";
            if (null != ((Agente)this._ventana.Agente))
                StringLlleno += ((Agente)this._ventana.Agente).Id + "  ";
            this._ventana.String = StringLlleno + "  " + parametroMarcas;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return StringLlleno;

        }


        /// <summary>
        /// Método que arma el string y llama a generar el escrito
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
                   
                    string parametroMarcas = ArmarStringParametroMarcas(this._marcasAgregadas);
                    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["RutaBatEscrito"].ToString()
                        + "\\" + ConfigurationManager.AppSettings["EscritoReingresoDePoderYReclasificacion"].ToString(),
                        ((Boletin)this._ventana.Boletin).Id + " " + ((Resolucion)this._ventana.Resolucion).Id
                        + " " + ((ListaDatosValores)this._ventana.CantidadNumeralSelec).Valor
                        + " " + this._ventana.Numerales + " " + ((ListaDatosValores)this._ventana.TipoDePoder).Valor
                        + " " + ((ListaDatosValores)this._ventana.Reclasificacion).Valor
                        + " " + ((ListaDatosValores)this._ventana.TipoDeEtiqueta).Valor                                     
                        + " " + ((Agente)this._ventana.AgenteFiltrado).Id + " " + parametroMarcas);
           
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

        /// <summary>
        /// Método que se encarga de hacer las validaciones para generar el escrito
        /// </summary>
        /// <returns></returns>
        private bool ValidarEscrito()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if ((this._ventana.AgenteFiltrado != null) && null != ((Agente)this._ventana.AgenteFiltrado).Id)
            {
                if (this._marcasAgregadas.Count != 0)
                {
                    if (this.ValidarAgenteApoderadoDeMarcas((Agente)this._ventana.AgenteFiltrado, this._marcasAgregadas))
                    {
                        if (((Boletin)this._ventana.Boletin != null) && (((Boletin)this._ventana.Boletin).Id != int.MinValue))
                        {
                            if (this._ventana.Resolucion != null)
                            {
                                if (!this._ventana.Numerales.Equals(""))
                                {
                                    retorno = true;
                                }
                                else
                                {
                                    this._ventana.MensajeAlerta(string.Format(Recursos.MensajesConElUsuario.AlertaEscritoSinNumeral));
                                }
                            }
                            else
                            {
                                this._ventana.MensajeAlerta(string.Format(Recursos.MensajesConElUsuario.AlertaEscritoSinResolucion));
                            }
                        }
                        else
                        {
                            this._ventana.MensajeAlerta(string.Format(Recursos.MensajesConElUsuario.AlertaEscritoSinBoletin));
                        }
                    }
                    else
                    {
                        this._ventana.MensajeAlerta(string.Format(Recursos.MensajesConElUsuario.AlertaAgenteNoApareceEnPoderDeMarca,
                            ((Agente)this._ventana.AgenteFiltrado).Nombre));
                    }
                }
                else
                {
                    this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaEscritoSinMarcas);
                }
            }
            else
            {
                this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaEscritoSinAgente);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
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

        #region Agente

        /// <summary>
        /// Método que se encarga de cambiar un agente
        /// </summary>
        /// <returns></returns>
        public bool CambiarAgente()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
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
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
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

        #region Marca

        /// <summary>
        /// Método que se encarga de cambiar la marca seleccionada
        /// </summary>
        /// <returns></returns>
        public bool CambiarMarca()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.MarcaFiltrado != null)
                {
                    if (((Marca)this._ventana.MarcaFiltrado).Id != int.MinValue)
                    {
                        this._ventana.Marca =
                            this._marcaServicios.ConsultarMarcaConTodo((Marca)this._ventana.MarcaFiltrado);
                        this._marcas.Add((Marca)this._ventana.MarcaFiltrado);
                    }
                    this._ventana.NombreMarca = ((Marca)this._ventana.MarcaFiltrado).Descripcion;
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
        private void CargarMarca()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._marcas = new List<Marca>();

            this._marcas.Add(this.primerMarca);
            this._ventana.Marca = this.primerMarca;
            this._ventana.MarcasFiltrados = this._marcas;
            this._ventana.MarcaFiltrado = this.primerMarca;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que se encarga de realizar la consulta de las marcas
        /// </summary>
        public void ConsultarMarca()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((!this._ventana.IdMarcaFiltrar.Equals("")) || (!this._ventana.NombreMarcaFiltrar.Equals("")))
                {
                    Marca MarcaConsulta = new Marca();
                    MarcaConsulta.Id = !this._ventana.IdMarcaFiltrar.Equals("") ?
                        int.Parse(this._ventana.IdMarcaFiltrar) : 0;
                    MarcaConsulta.Descripcion = !this._ventana.NombreMarcaFiltrar.Equals("") ?
                        this._ventana.NombreMarcaFiltrar.ToUpper() : string.Empty;

                    this._marcas = this._marcaServicios.ObtenerMarcasFiltro(MarcaConsulta);
                    this._marcas.Insert(0, this.primerMarca);
                    this._ventana.MarcasFiltrados = this._marcas;
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
        public void AgregarMarca()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((this._ventana.MarcaFiltrado != null) && (((Marca)this._ventana.MarcaFiltrado).Id != int.MinValue))
                {
                    if (null != ((Marca)this._ventana.MarcaFiltrado).Poder)
                    {
                        this._marcasAgregadas.Insert(0, (Marca)this._ventana.MarcaFiltrado);
                        this._marcas.Remove((Marca)this._ventana.MarcaFiltrado);
                    }
                    else
                        this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaSinPoder);
                }

                this._ventana.MarcasAgregadas = null;
                this._ventana.MarcasAgregadas = this._marcasAgregadas;

                this._ventana.MarcasFiltrados = null;
                this._ventana.MarcasFiltrados = this._marcas;
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
        public void EliminarMarca()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.MarcaAgregada != null)
                {
                    this._marcasAgregadas.Remove((Marca)this._ventana.MarcaAgregada);
                    this._marcas.Insert(0, (Marca)this._ventana.MarcaAgregada);
                }

                this._ventana.MarcasAgregadas = null;
                this._ventana.MarcasAgregadas = this._marcasAgregadas;

                this._ventana.MarcasFiltrados = null;
                this._ventana.MarcasFiltrados = this._marcas;
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

        #region Boletines

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
        /// Método que carga los numerales, poderes, etiquetas, reclasificaciones y resolucion en los combobox
        /// </summary>
        private void CargaCombo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<ListaDatosValores> numerales =
                this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaNumerales));
            this._ventana.CantidadNumerales = numerales;

            IList<ListaDatosValores> poderes =
            this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaTipoPoder));
            this._ventana.TipoDePoderes = poderes;

            IList<ListaDatosValores> etiquetas =
            this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaTipoEtiqueta));
            this._ventana.TipoDeEtiquetas = etiquetas;

            IList<ListaDatosValores> Reclasificaciones =
            this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaReclasificar));
            this._ventana.Reclasificaciones = Reclasificaciones;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        /// <summary>
        /// Método que se encarga en actualizar las resoluciones
        /// </summary>
        public void ActualizarResoluciones()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (((Boletin)this._ventana.Boletin).Id != int.MinValue)
            {
                IList<Resolucion> resoluciones = this._boletinServicios.ConsultarResolucionesDeBoletin((Boletin)this._ventana.Boletin);

                if (resoluciones.Count != 0)
                {
                    this._ventana.Resoluciones = resoluciones;
                    this._ventana.Resolucion = resoluciones[0];
                    GenerarString();
                }
                else
                {
                    this._ventana.Resoluciones = null;
                    this._ventana.Resolucion = null;
                }
            }
            else
            {
                this._ventana.Resoluciones = null;
                this._ventana.Resolucion = null;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        #endregion
    }
}
