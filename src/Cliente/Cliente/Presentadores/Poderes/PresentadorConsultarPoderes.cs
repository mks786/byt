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
using Trascend.Bolet.Cliente.Contratos.Poderes;
using Trascend.Bolet.Cliente.Ventanas.Poderes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Poderes
{
    class PresentadorConsultarPoderes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarPoderes _ventana;
        private IBoletinServicios _boletinServicios;
        private IPoderServicios _poderServicios;
        private IInteresadoServicios _interesadoServicios;
        private IList<Poder> _poderes;
        private IList<Interesado> _interesados;

        private bool _filtroValido = true;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarPoderes(IConsultarPoderes ventana)
        {
            try
            {
                this._ventana = ventana;
                this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarPoderes,
                Recursos.Ids.ConsultarPoderes);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
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

                //this._poderes = this._poderServicios.ConsultarTodos();
                //this._ventana.Resultados = this._poderes;
                //this._ventana.TotalHits = this._poderes.Count.ToString();
                this._ventana.TotalHits = "0";

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                Boletin primerBoletin = new Boletin();
                primerBoletin.Id = int.MinValue;
                boletines.Insert(0, primerBoletin);
                this._ventana.Boletines = boletines;

                //IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
                //Interesado primerInteresado = new Interesado();
                //primerInteresado.Id = int.MinValue;
                //interesados.Insert(0, primerInteresado);
                //this._ventana.Interesados = interesados;
                //this._interesados = interesados;

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
            IList<Poder> poderes = new List<Poder>();
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Poder poder = ObtenerPoderDeFiltro();

                if (_filtroValido)
                {
                    poderes = _poderServicios.ObtenerPoderesFiltro(poder);
                
                }
                this._ventana.Resultados = poderes;
                this._ventana.TotalHits = poderes.Count.ToString();

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

        private Poder ObtenerPoderDeFiltro()
        {
            Poder retorno = new Poder();
            int filtroValido = 0;
            _filtroValido = true;
            try
            {

                if (!string.IsNullOrEmpty(this._ventana.Id))
                {
                    retorno.Id = int.Parse(this._ventana.Id);
                    filtroValido++;
                }

                if (!string.IsNullOrEmpty(this._ventana.NumPoder))
                {
                    retorno.NumPoder = this._ventana.NumPoder;
                    filtroValido++;
                }

                if (this._ventana.Boletin != null && !((Boletin)this._ventana.Boletin).Id.Equals(int.MinValue))
                {
                    Boletin boletin = (Boletin)this._ventana.Boletin;
                    retorno.Boletin = boletin;
                    filtroValido++;
                }

                if (this._ventana.Interesado != null && !((Interesado)this._ventana.Interesado).Id.Equals(int.MinValue))
                {
                    Interesado interesado = (Interesado)this._ventana.Interesado;
                    retorno.Interesado = interesado;
                    filtroValido++;
                }

                //if (!string.IsNullOrEmpty(this._ventana.Facultad))
                //{
                //    retorno.Facultad = this._ventana.Facultad;
                //    filtroValido++;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.Anexo))
                //{
                //    retorno.Anexo = this._ventana.Anexo;
                //    filtroValido++;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.Observaciones))
                //{
                //    retorno.Observaciones = this._ventana.Observaciones;
                //    filtroValido++;
                //}

                if (filtroValido == 0)
                    _filtroValido = false;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return retorno;
        }

        /// <summary>
        /// Método que invoca una nueva página "ConsultarPoder" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarPoder()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ConsultarPoder(this._ventana.PoderSeleccionado,this._ventana));

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

        /// <summary>
        /// Método que se encarga de filtrar un interesado
        /// </summary>
        public void BuscarInteresado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //IEnumerable<Interesado> interesadosFiltrados = (IList<Interesado>)this._interesados;
            bool filtroInteresadoValido = false;
            IList<Interesado> interesados = new List<Interesado>();
            
            if (!string.IsNullOrEmpty(this._ventana.IdInteresadoFiltrar))
            {
                filtroInteresadoValido = true;
            }

            if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoFiltrar))
            {
                filtroInteresadoValido = true;
            }

            if (filtroInteresadoValido)
            {
                interesados = this._interesadoServicios.ObtenerInteresadosFiltro(CargarInteresadoDeFiltro());
            }
            Interesado primerInteresado = new Interesado();
            primerInteresado.Id = int.MinValue;
            interesados.Insert(0, primerInteresado);

            this._ventana.Interesados = interesados;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        private Interesado CargarInteresadoDeFiltro()
        {
            Interesado retorno = new Interesado();
            try
            {

                if (!string.IsNullOrEmpty(this._ventana.IdInteresadoFiltrar))
                {
                    retorno.Id = int.Parse(this._ventana.IdInteresadoFiltrar);
                }

                if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoFiltrar))
                {
                    retorno.Nombre = this._ventana.NombreInteresadoFiltrar;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return retorno;
        }

        /// <summary>
        /// Método que limpia los campos de búsqueda
        /// </summary>
        public void LimpiarCampos()
        {
            this._ventana.Id = null;
            this._ventana.NombreInteresadoFiltrar = null;
            this._ventana.IdInteresadoFiltrar = null;
            //this._ventana.Observaciones = null;
            this._ventana.PoderSeleccionado = null;
            //this._ventana.Anexo = null;
            this._ventana.NumPoder = null;
            //this._ventana.Facultad = null;

            this._ventana.Boletin = ((IList<Boletin>)this._ventana.Boletines)[0];
            this._ventana.Interesado = null;

            this._ventana.Resultados = this._poderes;
            this._ventana.TotalHits = this._poderes.Count().ToString();
        }
    }
}
