using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Asociados
{
    class PresentadorConsultarAsociados : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarAsociados _ventana;
        private IAsociadoServicios _asociadoServicios;
        private IList<Asociado> _asociados;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarAsociados(IConsultarAsociados ventana)
        {
            try
            {
                this._ventana = ventana;
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarAsociados,
            Recursos.Ids.ConsultarAsociados);
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

                this._asociados = this._asociadoServicios.ConsultarTodos();
                this._ventana.Resultados = this._asociados;
                this._ventana.AsociadoFiltrar = new Asociado();
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

                //Agente agente = (Agente) this._ventana.AgenteFiltrar;

                //IEnumerable<Agente> agentesFiltrados = this._agentes;

                //if (!string.IsNullOrEmpty(agente.Id))
                //{
                //    agentesFiltrados = from a in agentesFiltrados
                //                       where a.Id.ToLower().Contains(agente.Id.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(agente.Nombre))
                //{
                //    agentesFiltrados = from a in agentesFiltrados
                //                       where a.Nombre != null && 
                //                       a.Nombre.ToLower().Contains(agente.Nombre.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(agente.Domicilio))
                //{
                //    agentesFiltrados = from a in agentesFiltrados
                //                       where a.Domicilio != null && 
                //                       a.Domicilio.ToLower().Contains(agente.Domicilio.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(agente.Telefono))
                //{
                //    agentesFiltrados = from a in agentesFiltrados
                //                       where a.Telefono != null && a.Telefono.ToLower().Contains(agente.Telefono.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.EstadoCivil.ToString()) && !this._ventana.EstadoCivil.Equals(' '))
                //{
                //    agentesFiltrados = from a in agentesFiltrados
                //                       where a.EstadoCivil.ToString().ToLower().Contains(agente.EstadoCivil.ToString().ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.Sexo.ToString()) && !this._ventana.Sexo.Equals(' '))
                //{
                //    agentesFiltrados = from a in agentesFiltrados
                //                       where a.Sexo.ToString().ToLower().Contains(agente.Sexo.ToString().ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(agente.NumeroAbogado))
                //{
                //    agentesFiltrados = from a in agentesFiltrados
                //                       where a.NumeroAbogado != null && 
                //                       a.NumeroAbogado.ToLower().Contains(agente.NumeroAbogado.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(agente.NumeroImpresoAbogado))
                //{
                //    agentesFiltrados = from a in agentesFiltrados
                //                       where a.NumeroImpresoAbogado != null && 
                //                       a.NumeroImpresoAbogado.ToLower().Contains(agente.NumeroImpresoAbogado.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(agente.NumeroPropiedad))
                //{
                //    agentesFiltrados = from a in agentesFiltrados
                //                       where a.NumeroPropiedad != null && 
                //                       a.NumeroPropiedad.ToLower().Contains(agente.NumeroPropiedad.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(agente.CCI))
                //{
                //    agentesFiltrados = from a in agentesFiltrados
                //                       where a.CCI != null && 
                //                       a.CCI.ToLower().Contains(agente.CCI.ToLower())
                //                       select a;
                //}

                //this._ventana.Resultados = agentesFiltrados.ToList<Agente>();

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

            //this.Navegar(new ConsultarAgente(this._ventana.AgenteSeleccionado));

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
