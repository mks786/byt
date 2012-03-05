using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Agentes;
using Trascend.Bolet.Cliente.Ventanas.Agentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Agentes
{
    class PresentadorConsultarAgentes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarAgentes _ventana;
        private IAgenteServicios _agenteServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private IList<Agente> _agentes;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarAgentes(IConsultarAgentes ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
            }
        }

        /// <summary>
        /// Método que actualiza el título de la ventana a Consultar Agente
        /// </summary>
        public void ActualizarTitulo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarAgentes,
            Recursos.Ids.ConsultarAgentes);
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

                this._agentes = this._agenteServicios.ConsultarTodos();
                this._ventana.Resultados = this._agentes;
                this._ventana.AgenteFiltrar = new Agente();
                this._ventana.TotalHits = this._agentes.Count.ToString();

                IList<ListaDatosDominio> estadosCiviles = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCategoriaEstadoCivil));
                ListaDatosDominio primeraListaDatosDominios = new ListaDatosDominio();
                primeraListaDatosDominios.Id = "NGN";
                estadosCiviles.Insert(0, primeraListaDatosDominios);
                this._ventana.EstadosCivil = estadosCiviles;

                IList<ListaDatosValores> sexos = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores("SEXO"));
                ListaDatosValores primeraListaDatosValores = new ListaDatosValores("NGN");
                sexos.Insert(0, primeraListaDatosValores);
                this._ventana.Sexos = sexos;

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
        /// Método que realiza una consulta al servicio, con el fin de filtrar los datos que se muestran 
        /// por pantalla
        /// </summary>
        public void Consultar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Agente agente = (Agente) this._ventana.AgenteFiltrar;

                IEnumerable<Agente> agentesFiltrados = this._agentes;

                if (!string.IsNullOrEmpty(agente.Id))
                {
                    agentesFiltrados = from a in agentesFiltrados
                                       where a.Id.ToLower().Contains(agente.Id.ToLower())
                                       select a;
                }

                if (!string.IsNullOrEmpty(agente.Nombre))
                {
                    agentesFiltrados = from a in agentesFiltrados
                                       where a.Nombre != null && 
                                       a.Nombre.ToLower().Contains(agente.Nombre.ToLower())
                                       select a;
                }

                if (!string.IsNullOrEmpty(agente.Domicilio))
                {
                    agentesFiltrados = from a in agentesFiltrados
                                       where a.Domicilio != null && 
                                       a.Domicilio.ToLower().Contains(agente.Domicilio.ToLower())
                                       select a;
                }

                if (!string.IsNullOrEmpty(agente.Telefono))
                {
                    agentesFiltrados = from a in agentesFiltrados
                                       where a.Telefono != null && a.Telefono.ToLower().Contains(agente.Telefono.ToLower())
                                       select a;
                }

                if (!((ListaDatosDominio)this._ventana.EstadoCivil).Id.Equals("NGN"))
                {
                    agentesFiltrados = from a in agentesFiltrados
                                       where a.EstadoCivil == ((ListaDatosDominio)this._ventana.EstadoCivil).Id[0]
                                       select a;
                }

                if (this._ventana.Sexo != null && !((ListaDatosValores)this._ventana.Sexo).Id.Equals("NGN"))
                {
                    agentesFiltrados = from a in agentesFiltrados
                                       where a.Sexo.ToString().ToLower().Contains(((ListaDatosValores)this._ventana.Sexo).Valor.ToLower())
                                       select a;

                }

                if (!string.IsNullOrEmpty(agente.NumeroAbogado))
                {
                    agentesFiltrados = from a in agentesFiltrados
                                       where a.NumeroAbogado != null && 
                                       a.NumeroAbogado.ToLower().Contains(agente.NumeroAbogado.ToLower())
                                       select a;
                }

                if (!string.IsNullOrEmpty(agente.NumeroImpresoAbogado))
                {
                    agentesFiltrados = from a in agentesFiltrados
                                       where a.NumeroImpresoAbogado != null && 
                                       a.NumeroImpresoAbogado.ToLower().Contains(agente.NumeroImpresoAbogado.ToLower())
                                       select a;
                }

                if (!string.IsNullOrEmpty(agente.NumeroPropiedad))
                {
                    agentesFiltrados = from a in agentesFiltrados
                                       where a.NumeroPropiedad != null && 
                                       a.NumeroPropiedad.ToLower().Contains(agente.NumeroPropiedad.ToLower())
                                       select a;
                }

                if (!string.IsNullOrEmpty(agente.CCI))
                {
                    agentesFiltrados = from a in agentesFiltrados
                                       where a.CCI != null && 
                                       a.CCI.ToLower().Contains(agente.CCI.ToLower())
                                       select a;
                }

                this._ventana.Resultados = agentesFiltrados.ToList<Agente>();
                this._ventana.TotalHits = agentesFiltrados.ToList<Agente>().Count.ToString();

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
        /// Método que invoca una nueva página "ConsultarAgente" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarAgente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ConsultarAgente(this._ventana.AgenteSeleccionado));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                this._ventana.ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            this._ventana.ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
