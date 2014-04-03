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
using System.Windows;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.AbandonosPatente;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.AbandonosPatente;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.AbandonosPatente
{
    class PresentadorConsultarAbandonosPatente : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IConsultarAbandonosPatente _ventana;
        private IPatenteServicios _patenteServicios;
        private IList<Patente> _patentes;
        private IList<Operacion> _abandonos;
        private IOperacionServicios _operacionServicios;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarAbandonosPatente(IConsultarAbandonosPatente ventana)
        {
            try
            {
                this._ventana = ventana;
                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarAbandonosPatente,
                Recursos.Ids.ConsultarAbandonosPatente);
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

                IList<Patente> patentesAux = new List<Patente>();
                Patente primerPatente = new Patente(int.MinValue);
                patentesAux.Add(primerPatente);

                this._ventana.Patentes = patentesAux;
                this._ventana.Patente = this.BuscarPatente(patentesAux, primerPatente);

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

                Operacion operacionAuxiliar = new Operacion();

                if (!this._ventana.Id.Equals(""))
                {
                    operacionAuxiliar.Id = int.Parse(this._ventana.Id);
                    filtroValido = 2;
                }

                if ((null != this._ventana.Patente) && (((Patente)this._ventana.Patente).Id != int.MinValue))
                {
                    operacionAuxiliar.Patente = (Patente)this._ventana.Patente;
                    filtroValido = 2;
                }

                if (!this._ventana.Fecha.Equals(""))
                {
                    DateTime fechaCesion = DateTime.Parse(this._ventana.Fecha);
                    filtroValido = 2;
                    operacionAuxiliar.Fecha = fechaCesion;
                }

                operacionAuxiliar.Aplicada = 'P';

                Servicio servicioAuxiliar = new Servicio();
                servicioAuxiliar.Id = "AB";

                operacionAuxiliar.Servicio = servicioAuxiliar;

                if (filtroValido >= 2)
                {
                    Patente patenteAuxiliar = new Patente();
                    patenteAuxiliar.Id = operacionAuxiliar.CodigoAplicada;

                    this._abandonos = this._operacionServicios.ObtenerOperacionFiltro(operacionAuxiliar);

                    this._ventana.Resultados = this._abandonos;
                    this._ventana.TotalHits = _abandonos.Count.ToString();
                    if (this._abandonos.Count == 0)
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
        /// Método que invoca una nueva página "ConsultarAbandonos" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarAbandono()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.AbandonoSeleccionado != null)
            {
                //this.Navegar(new GestionarAbandonoPatente(this._ventana.AbandonoSeleccionado));
                this.Navegar(new GestionarAbandonoPatente(this._ventana.AbandonoSeleccionado, Visibility.Visible, this._ventana));
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


        /// <summary>
        /// Método que busca las patentes registradas
        /// </summary>
        public void ConsultarPatente()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Patente patente = new Patente();
            IList<Patente> patentesFiltradas;
            patente.Descripcion = this._ventana.NombrePatenteFiltrar.ToUpper();

            patente.Id = this._ventana.IdPatenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPatenteFiltrar);

            if ((!patente.Descripcion.Equals("")) || (patente.Id != 0))
                patentesFiltradas = this._patenteServicios.ObtenerPatentesFiltro(patente);
            else
                patentesFiltradas = new List<Patente>();

            patentesFiltradas.Insert(0, new Patente(int.MinValue));

            if (patentesFiltradas.ToList<Patente>().Count != 0)
                this._ventana.Patentes = patentesFiltradas.ToList<Patente>();
            else
                this._ventana.Patentes = this._patentes;

            Mouse.OverrideCursor = null;
        }


        /// <summary>
        /// Método que permite seleccionar patente
        /// </summary>
        public bool ElegirPatente()
        {
            bool retorno = false;
            if (this._ventana.Patente != null)
            {
                retorno = true;
                this._ventana.NombrePatente = ((Patente)this._ventana.Patente).Descripcion;
            }

            return retorno;
        }

    }
}
