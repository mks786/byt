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
    class PresentadorNumeracionDePoderPorInteresado : PresentadorBase
    {
        private INumeracionDePoderPorInteresado _ventana;

        private IAgenteServicios _agenteServicios;        
        private IInteresadoServicios _interesadoServicios;

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        Agente primerAgente = new Agente();
        Interesado primerInteresado = new Interesado(int.MinValue);

        private IList<Agente> _Agentes;
        private IList<Interesado> _interesados;
        private IList<Interesado> _interesadosAgregados = new List<Interesado>();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorNumeracionDePoderPorInteresado(INumeracionDePoderPorInteresado ventana)
        {
            try
            {
                this._ventana = ventana;
                //this._ventana.Escrito = new Pais();
                this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);               
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleEscritoNumeracionDePoderPorInteresado,
                    "");

                CargarAgente();
                CargarInteresado();
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
                if (this._ventana.BotonModificar.Equals(Recursos.Etiquetas.btnModificar))
                {
                    this._ventana.HabilitarCampos = true;
                }
                else
                {
                    if ((this._ventana.AgenteFiltrado != null) && null != ((Agente)this._ventana.AgenteFiltrado).Id)
                    {
                        if (this._interesadosAgregados.Count != 0)
                        {
                            if (this.ValidarPoderesAgenteConPoderesInteresados((Agente)this._ventana.AgenteFiltrado, this._interesadosAgregados))
                            {
                                string parametroInteresados = ArmarStringParametroInteresados(this._interesadosAgregados);
                                this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["RutaBatEscrito"].ToString()
                                    + "\\" + ConfigurationManager.AppSettings["EscritoNumeracionDePoderPorInteresado"].ToString(),
                                    ((Agente)this._ventana.AgenteFiltrado).Id+" "+ parametroInteresados);
                            }
                            else 
                            {
                                this._ventana.MensajeAlerta(string.Format(Recursos.MensajesConElUsuario.AlertaAgenteNoPoseePoderConInteresado, 
                                    ((Agente)this._ventana.AgenteFiltrado).Nombre));
                            }
                        }
                        else
                        {
                            this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaEscritoSinInteresados);
                        }
                    }
                    else
                    {
                        this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaEscritoSinAgente);
                    }

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

                    this._Agentes = this._agenteServicios.ObtenerAgentesFiltro(AgenteConsulta);
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

        #region Interesado

        /// <summary>
        /// Método que se encarga de cambiar el interesado seleccionado
        /// </summary>
        /// <returns></returns>
        public bool CambiarInteresado()
        {           
            bool retorno = false;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.InteresadoFiltrado != null)
                {
                    if (((Interesado)this._ventana.InteresadoFiltrado).Id != int.MinValue)
                    {
                        this._ventana.Interesado =
                            this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoFiltrado);                        
                    }
                    this._ventana.NombreInteresado = ((Interesado)this._ventana.InteresadoFiltrado).Nombre;
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
        /// Método que realiza la carga inicial de los Interesados
        /// </summary>
        private void CargarInteresado()
        {

            this._interesados = new List<Interesado>();

            this._interesados.Add(this.primerInteresado);
            this._ventana.Interesado = this.primerInteresado;
            this._ventana.InteresadosFiltrados = this._interesados;
            this._ventana.InteresadoFiltrado = this.primerInteresado;

        }

        /// <summary>
        /// Método que se encarga de realizar la consulta de los interesados
        /// </summary>
        public void ConsultarInteresado()
        {           
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((!this._ventana.IdInteresadoFiltrar.Equals("")) || (!this._ventana.NombreInteresadoFiltrar.Equals("")))
                {
                    Interesado interesadoConsulta = new Interesado();
                    interesadoConsulta.Id = !this._ventana.IdInteresadoFiltrar.Equals("") ?
                        int.Parse(this._ventana.IdInteresadoFiltrar) : 0;
                    interesadoConsulta.Nombre = !this._ventana.NombreInteresadoFiltrar.Equals("") ?
                        this._ventana.NombreInteresadoFiltrar.ToUpper() : string.Empty;

                    this._interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoConsulta);
                    this._interesados.Insert(0, this.primerInteresado);
                    this._ventana.InteresadosFiltrados = this._interesados;
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
        /// Método que se encarga de agregar el interesado seleccionada a la lista de interesados agregados
        /// </summary>
        public void AgregarInteresado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((this._ventana.InteresadoFiltrado != null) && (((Interesado)this._ventana.InteresadoFiltrado).Id != int.MinValue))
                {
                    this._interesadosAgregados.Insert(0, (Interesado)this._ventana.InteresadoFiltrado);
                    this._interesados.Remove((Interesado)this._ventana.InteresadoFiltrado);
                }

                this._ventana.InteresadosAgregados = null;
                this._ventana.InteresadosAgregados = this._interesadosAgregados;

                this._ventana.InteresadosFiltrados = null;
                this._ventana.InteresadosFiltrados = this._interesados;

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
        /// Método que se encarga de eliminar el interesado seleccionada de la lista de interesados agregados
        /// </summary>
        public void EliminarInteresado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.InteresadoAgregado != null)
                {
                    this._interesadosAgregados.Remove((Interesado)this._ventana.InteresadoAgregado);
                    this._interesados.Insert(0, (Interesado)this._ventana.InteresadoAgregado);
                }

                this._ventana.InteresadosAgregados = null;
                this._ventana.InteresadosAgregados = this._interesadosAgregados;

                this._ventana.InteresadosFiltrados = null;
                this._ventana.InteresadosFiltrados = this._interesados;

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
