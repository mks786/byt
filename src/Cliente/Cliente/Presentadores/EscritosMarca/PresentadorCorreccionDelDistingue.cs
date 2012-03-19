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
    class PresentadorCorreccionDelDistingue : PresentadorBase
    {
        private ICorreccionDelDistingue _ventana;

        private IAgenteServicios _agenteServicios;
        private IMarcaServicios _marcaServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        Agente primerAgente = new Agente();
        Marca primerMarca = new Marca(int.MinValue);

        private IList<Agente> _Agentes;
        private IList<Marca> _marcas;
        private IList<Marca> _marcasAgregadas = new List<Marca>();
        private IList<ListaDatosValores> _listaRecordatorios;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorCorreccionDelDistingue(ICorreccionDelDistingue ventana)
        {
            try
            {
                this._ventana = ventana;
                //this._ventana.Escrito = new Pais();
                this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
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


                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleEscritoCorreccionDelDistingue,
                    "");
                CargarAgente();
                CargarMarca();
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
        /// Método que realiza toda la lógica para agregar al País dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                if (ValidarEscrito())
                {
                    string parametroMarcas = ArmarStringParametroMarcas(this._marcasAgregadas);
                    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["RutaBatEscrito"].ToString()
                        + "\\" + ConfigurationManager.AppSettings["EscritoCorreccionDelDistingue"].ToString(),
                        ((Agente)this._ventana.AgenteFiltrado).Id + " " + parametroMarcas + " " + this._ventana.Error
                        + " " + this._ventana.Correccion);
                }
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

        private bool ValidarEscrito()
        {
            bool retorno = false;

            if ((this._ventana.AgenteFiltrado != null) && null != ((Agente)this._ventana.AgenteFiltrado).Id)
            {
                if (this._marcasAgregadas.Count != 0)
                {
                    if (this.ValidarAgenteApoderadoDeMarcas((Agente)this._ventana.AgenteFiltrado, this._marcasAgregadas))
                    {
                        if (!this._ventana.Error.Equals(""))
                            if (!this._ventana.Correccion.Equals(""))
                                retorno = true;
                            else
                                this._ventana.MensajeAlerta(string.Format(Recursos.MensajesConElUsuario.AlertaEscritoSinCorreccion, ""));
                        else
                            this._ventana.MensajeAlerta(string.Format(Recursos.MensajesConElUsuario.AlertaEscritoSinError, ""));
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

            this._Agentes = new List<Agente>();

            this._Agentes.Add(this.primerAgente);
            this._ventana.Agente = this.primerAgente;
            this._ventana.AgentesFiltrados = this._Agentes;
            this._ventana.AgenteFiltrado = this.primerAgente;

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

        #region Marca

        /// <summary>
        /// Método que se encarga de cambiar la marca seleccionada
        /// </summary>
        /// <returns></returns>
        public bool CambiarMarca()
        {
            bool retorno = false;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

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

            this._marcas = new List<Marca>();

            this._marcas.Add(this.primerMarca);
            this._ventana.Marca = this.primerMarca;
            this._ventana.MarcasFiltrados = this._marcas;
            this._ventana.MarcaFiltrado = this.primerMarca;

        }

        /// <summary>
        /// Método que se encarga de realizar la consulta de las marcas
        /// </summary>
        public void ConsultarMarca()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

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
                    this._marcasAgregadas.Insert(0, (Marca)this._ventana.MarcaFiltrado);
                    this._marcas.Remove((Marca)this._ventana.MarcaFiltrado);
                }

                this._ventana.MarcasAgregadas = null;
                this._ventana.MarcasAgregadas = this._marcasAgregadas;

                this._ventana.MarcasFiltrados = null;
                this._ventana.MarcasFiltrados = this._marcas;

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
