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
using Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.FusionesPatentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.FusionesPatentes;

namespace Trascend.Bolet.Cliente.Presentadores.TraspasosPatentes.FusionesPatentes
{
    class PresentadorConsultarFusionesPatentes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarFusionesPatentes _ventana;
        private IPatenteServicios _marcaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IFusionPatenteServicios _fusionServicios;

        private IList<FusionPatente> _fusiones;
        private IList<Patente> _marcas;
        private IList<Interesado> _interesados;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarFusionesPatentes(IConsultarFusionesPatentes ventana)
        {
            try
            {
                this._ventana = ventana;
                this._marcaServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._fusionServicios = (IFusionPatenteServicios)Activator.GetObject(typeof(IFusionPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FusionPatenteServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarFusionPatente,
                Recursos.Ids.ConsultarFusiones);
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

                //this._fusiones = this._fusionServicios.ConsultarTodos();
                //this._ventana.Resultados = this._fusiones;

                //IList<Patente> marcas = this._marcaServicios.ConsultarTodos();
                //Patente primerPatente = new Patente();
                //primerPatente.Id = int.MinValue;
                //marcas.Insert(0, primerPatente);
                //this._ventana.Patentes = marcas;
                //this._marcas = marcas;

                //IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
                //Interesado primerInteresado = new Interesado();
                //primerInteresado.Id = int.MinValue;
                //interesados.Insert(0, primerInteresado);
                //this._ventana.Interesados = interesados;
                //this._interesados = interesados;

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

                FusionPatente FusionAuxiliar = new FusionPatente();

                if (!this._ventana.Id.Equals(""))
                {
                    filtroValido = 2;
                    FusionAuxiliar.Id = int.Parse(this._ventana.Id);
                }

                if ((null != this._ventana.Patente) && (((Patente)this._ventana.Patente).Id != int.MinValue))
                {
                    FusionAuxiliar.Patente = (Patente)this._ventana.Patente;
                    filtroValido = 2;
                }

                //if ((null != this._ventana.Interesado) && (((Interesado)this._ventana.Interesado).Id != int.MinValue))
                //{
                //    FusionAuxiliar.Interesado = (Interesado)this._ventana.Interesado;
                //    filtroValido++;
                //}


                //if (!this._ventana.FichasFiltrar.Equals(""))
                //{
                //    FusionAuxiliar.Fichas = this._ventana.FichasFiltrar.ToUpper();
                //    filtroValido++;
                //}


                //if (!this._ventana.DescripcionFiltrar.Equals(""))
                //{
                //    filtroValido = 2;
                //    FusionAuxiliar.Descripcion = this._ventana.DescripcionFiltrar.ToUpper();
                //}

                if (!this._ventana.Fecha.Equals(""))
                {
                    DateTime fechaPublicacion = DateTime.Parse(this._ventana.Fecha);
                    filtroValido = 2;
                    FusionAuxiliar.FechaPublicacion = fechaPublicacion;
                }

                if (filtroValido >= 2)
                {
                    this._fusiones = this._fusionServicios.ObtenerFusionPatenteFiltro(FusionAuxiliar);

                    this._ventana.Resultados = this._fusiones;
                    this._ventana.TotalHits = _fusiones.Count.ToString();
                    if (this._fusiones.Count == 0)
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
        /// Método que invoca una nueva página "ConsultarFusion" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarFusion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.FusionSeleccionada != null)
            {
                this.Navegar(new GestionarFusionPatentes(this._ventana.FusionSeleccionada));
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
        /// Método que Carga las Patentes registradas
        /// </summary>
        public void BuscarPatente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Patente marca = new Patente();
                IEnumerable<Patente> marcasFiltradas;
                marca.Descripcion = this._ventana.NombrePatenteFiltrar.ToUpper();
                marca.Id = this._ventana.IdPatenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPatenteFiltrar);
                if ((!marca.Descripcion.Equals("")) || (marca.Id != 0))
                    marcasFiltradas = this._marcaServicios.ObtenerPatentesFiltro(marca);
                else
                    marcasFiltradas = new List<Patente>();

                if (marcasFiltradas.ToList<Patente>().Count != 0)
                    this._ventana.Patentes = marcasFiltradas.ToList<Patente>();
                else
                    this._ventana.Patentes = this._marcas;

                Mouse.OverrideCursor = null;

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
        /// Método que permite seleccionar marca
        /// </summary>
        public bool ElegirPatente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;
            if (this._ventana.Patente != null)
            {
                retorno = true;
                this._ventana.NombrePatente = ((Patente)this._ventana.Patente).Descripcion;
            }
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }
    }
}
