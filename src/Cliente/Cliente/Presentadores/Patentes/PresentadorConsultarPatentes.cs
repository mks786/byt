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
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Patentes
{
    class PresentadorConsultarPatentes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarPatentes _ventana;
        private IPatenteServicios _patenteServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IList<Patente> _patentes;
        private IList<CesionPatente> _cesiones;
        private ICesionPatenteServicios _cesionServicios;
        
        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarPatentes(IConsultarPatentes ventana)
        {
            try
            {
                this._ventana = ventana;
                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._cesionServicios = (ICesionPatenteServicios)Activator.GetObject(typeof(ICesionPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CesionPatenteServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarCesionPatente,
                Recursos.Ids.ConsultarCesionPatente);
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

                //this._marcas = this._marcaServicios.ConsultarTodos();
                //this._ventana.Resultados = this._marcas;
         
                this._ventana.TotalHits = "0";
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

                Mouse.OverrideCursor = Cursors.Wait;                
                int filtroValido = 0;//Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                //dos filtros sean utilizados

                Patente patenteAuxiliar = new Patente();

                if (!this._ventana.Id.Equals(""))
                {
                    patenteAuxiliar.Id = int.Parse(this._ventana.Id);
                    filtroValido = 2;
                }

                if (!this._ventana.NombrePatente.Equals(""))
                {
                    patenteAuxiliar.Descripcion = this._ventana.NombrePatente;
                    filtroValido = 2;
                }

                //if ((null != this._ventana.Asociado) && (((Asociado)this._ventana.Asociado).Id != int.MinValue))
                //{
                //    PatenteAuxiliar.Asociado = (Asociado)this._ventana.Asociado;
                //    filtroValido++;
                //}

                //if ((null != this._ventana.Interesado) && (((Interesado)this._ventana.Interesado).Id != int.MinValue))
                //{
                //    PatenteAuxiliar.Interesado = (Interesado)this._ventana.Interesado;
                //    filtroValido++;
                //}


                //if (!this._ventana.FichasFiltrar.Equals(""))
                //{
                //    PatenteAuxiliar.Fichas = this._ventana.FichasFiltrar.ToUpper();
                //    filtroValido++;
                //}


                //if (!this._ventana.DescripcionFiltrar.Equals(""))
                //{
                //    filtroValido = 2;
                //    PatenteAuxiliar.Descripcion = this._ventana.DescripcionFiltrar.ToUpper();
                //}

                if (!this._ventana.Fecha.Equals(""))
                {
                    DateTime fechaPatente = DateTime.Parse(this._ventana.Fecha);
                    filtroValido = 2;
                    patenteAuxiliar.FechaRegistro = fechaPatente;
                }

                if (filtroValido >= 2)
                {
                    this._patentes = this._patenteServicios.ObtenerPatentesFiltro(patenteAuxiliar);

                    this._ventana.Resultados = this._patentes;
                    this._ventana.TotalHits = _patentes.Count.ToString();
                    if (this._patentes.Count == 0)
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto, 0);

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

        /// <summary>
        /// Método que invoca una nueva página "GestionarPatente" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrGestionarPatente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.Patente != null)
            {
                this.Navegar(new GestionarPatente(this._ventana.Patente));
            }

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

        //public void BuscarAsociado()
        //{
        //    IEnumerable<Asociado> asociadosFiltrados = (IList<Asociado>)this._asociados;

        //    if (!string.IsNullOrEmpty(this._ventana.IdAsociadoFiltrar))
        //    {
        //        asociadosFiltrados = from p in asociadosFiltrados
        //                             where p.Id == int.Parse(this._ventana.IdAsociadoFiltrar)
        //                             select p;
        //    }

        //    if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoFiltrar))
        //    {
        //        asociadosFiltrados = from p in asociadosFiltrados
        //                             where p.Nombre != null &&
        //                             p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoFiltrar.ToLower())
        //                             select p;
        //    }

        //    if (asociadosFiltrados.ToList<Asociado>().Count != 0)
        //        this._ventana.Asociados = asociadosFiltrados.ToList<Asociado>();
        //    else
        //        this._ventana.Asociados = this._asociados;
        //}

        //public void BuscarInteresado()
        //{
        //    IEnumerable<Interesado> interesadosFiltrados = (IList<Interesado>)this._interesados;

        //    if (!string.IsNullOrEmpty(this._ventana.IdInteresadoFiltrar))
        //    {
        //        interesadosFiltrados = from p in interesadosFiltrados
        //                             where p.Id == int.Parse(this._ventana.IdInteresadoFiltrar)
        //                             select p;
        //    }

        //    if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoFiltrar))
        //    {
        //        interesadosFiltrados = from p in interesadosFiltrados
        //                             where p.Nombre != null &&
        //                             p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoFiltrar.ToLower())
        //                             select p;
        //    }

        //    if (interesadosFiltrados.ToList<Interesado>().Count != 0)
        //        this._ventana.Interesados = interesadosFiltrados.ToList<Interesado>();
        //    else
        //        this._ventana.Interesados = this._interesados;
        //}

        /// <summary>
        /// Método que busca las marcas registradas
        /// </summary>
        public void BuscarPatente()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Patente patente = new Patente();
            IEnumerable<Patente> patentesFiltradas;
            patente.Descripcion = this._ventana.NombrePatente.ToUpper();
            patente.Id = this._ventana.Id.Equals("") ? 0 : int.Parse(this._ventana.Id);
            if ((!patente.Descripcion.Equals("")) || (patente.Id != 0))
                patentesFiltradas = this._patenteServicios.ObtenerPatentesFiltro(patente);
            else
                patentesFiltradas = new List<Patente>();

            if (patentesFiltradas.ToList<Patente>().Count != 0)
                this._ventana.Patentes = patentesFiltradas.ToList<Patente>();
            else
                this._ventana.Patentes = this._patentes;

            Mouse.OverrideCursor = null;
        }

        
    }
}
