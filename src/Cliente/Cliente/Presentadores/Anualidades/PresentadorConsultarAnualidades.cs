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
using Trascend.Bolet.Cliente.Contratos.Anualidades;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Anualidades;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Anualidades
{
    class PresentadorConsultarAnualidades : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IConsultarAnualidades _ventana;
        private IAnualidadServicios _anualidadServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IPatenteServicios _patenteServicios;
        private IList<Patente> _patentes;
        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarAnualidades(IConsultarAnualidades ventana)
        {
            try
            {
                this._ventana = ventana;
                this._anualidadServicios = (IAnualidadServicios)Activator.GetObject(typeof(IAnualidadServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AnualidadServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarAnualidades,
                Recursos.Ids.ConsultarAnualidad);
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

                IList<Asociado> asociados = new List<Asociado>();
                Asociado primerAsociado = new Asociado();
                primerAsociado.Id = int.MinValue;
                asociados.Insert(0, primerAsociado);
                this._ventana.Asociados = asociados;
                this._asociados = asociados;

                IList<Interesado> interesados = new List<Interesado>();
                Interesado primerInteresado = new Interesado();
                primerInteresado.Id = int.MinValue;
                interesados.Insert(0, primerInteresado);
                this._interesados = interesados;

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
                bool consultaResumen = false;
                int filtroValido = 0;//Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                //dos filtros sean utilizados

                Patente PatenteAuxiliar = new Patente();

                if (!this._ventana.Id.Equals(""))
                {
                    filtroValido = 2;
                    PatenteAuxiliar.Id = int.Parse(this._ventana.Id);
                }

                if ((null != this._ventana.Asociado) && (((Asociado)this._ventana.Asociado).Id != int.MinValue))
                {
                    PatenteAuxiliar.Asociado = (Asociado)this._ventana.Asociado;
                    filtroValido = 2;
                }


                //if (!this._ventana.DescripcionFiltrar.Equals(""))
                //{
                //    filtroValido = 2;
                //    PatenteAuxiliar.Descripcion = this._ventana.DescripcionFiltrar.ToUpper();
                //}

                //if (!this._ventana.Fecha.Equals(""))
                //{
                //    DateTime fechaPublicacion = DateTime.Parse(this._ventana.Fecha);
                //    filtroValido = 2;
                //    PatenteAuxiliar.FechaPublicacion = fechaPublicacion;
                //}

                if (filtroValido >= 2)
                {
                    this._patentes = this._patenteServicios.ObtenerPatentesFiltro(PatenteAuxiliar);
                    if (_patentes.Count() != 0)
                    {
                        Patente PatenteComparadora = _patentes[0];
                    }
                    int comprobador = 0;
                    IList<Patente> patentesDesinfladas = new List<Patente>();

                    //foreach (var patente in this._patentes)
                    //{
                    //    PatenteAuxiliar = new Patente(patente.Id);
                    //    Asociado asociadoAuxiliar = new Asociado();
                    //    Interesado interesadoAuxiliar = new Interesado();

                    //    PatenteAuxiliar.Descripcion = patente.Descripcion != null ? patente.Descripcion : "";
                    //    if ((null != patente.Asociado) && (!string.IsNullOrEmpty(patente.Asociado.Nombre)))
                    //    {
                    //        //asociadoAuxiliar.Nombre = patente.Asociado.Nombre;
                    //        PatenteAuxiliar.Asociado = patente.Asociado;
                    //    }

                    //    if ((null != patente.Interesado) && (!string.IsNullOrEmpty(patente.Interesado.Nombre)))
                    //    {
                    //        //interesadoAuxiliar.Nombre = patente.Interesado.Nombre;
                    //        PatenteAuxiliar.Interesado = patente.Interesado;
                    //    }


                    //    PatenteAuxiliar.FechaPublicacion = patente.FechaPublicacion != null ? patente.FechaPublicacion : null;
                    //    patentesDesinfladas.Add(PatenteAuxiliar);
                    //    Filtra las busquedas repetidas
                    //    if ((PatenteComparadora.Id == PatenteAuxiliar.Id) && (PatenteComparadora.Anexo == PatenteAuxiliar.Anexo))
                    //    {
                    //        if (comprobador <= 0)
                    //        {
                    //            patentesDesinfladas.Add(PatenteAuxiliar);
                    //            comprobador++;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        patentesDesinfladas.Add(PatenteAuxiliar);
                    //    }
                    //}

                    this._ventana.Resultados = this._patentes;
                    this._ventana.TotalHits = this._patentes.Count.ToString();
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
        /// Método que invoca una nueva página "ConsultarPoder" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarAnualidades()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.AnualidadSeleccionada != null)
            {
                //Patente anualidadParaNavegar = null;
                //bool encontrada = false;
                //int cont = 0;
                //while (!encontrada)
                //{
                //    Patente anualidad = this._patentes[cont];
                //    if (anualidad.Id == ((Patente)this._ventana.AnualidadSeleccionada).Id) 
                //    {
                //        anualidadParaNavegar = anualidad;
                //        encontrada = true;
                //    }
                //    cont++;
                //}


                ((Patente)this._ventana.AnualidadSeleccionada).Anualidades =
                        this._anualidadServicios.ConsultarAnualidadesPorPatente((Patente)this._ventana.AnualidadSeleccionada);
                this.Navegar(new GestionarAnualidades(this._ventana.AnualidadSeleccionada));




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
        /// Método que se encarga de buscar el asociado definido en el filtro
        /// </summary>
        public void BuscarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            Asociado asociadoABuscar = new Asociado();

            asociadoABuscar.Id = !this._ventana.IdAsociadoFiltrar.Equals("") ?
                                 int.Parse(this._ventana.IdAsociadoFiltrar) : 0;

            asociadoABuscar.Nombre = !this._ventana.NombreAsociadoFiltrar.Equals("") ?
                                     this._ventana.NombreAsociadoFiltrar.ToUpper() : "";

            if ((asociadoABuscar.Id != 0) || !(asociadoABuscar.Nombre.Equals("")))
            {
                IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoABuscar);
                asociados.Insert(0, new Asociado(int.MinValue));
                this._ventana.Asociados = asociados;

            }
            else
            {
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                this._ventana.Asociados = this._asociados;
            }

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo que cambia el texto del Asociado en la interfaz
        /// </summary>
        /// <returns>true en caso de que el Asociado haya sido valido, false en caso contrario</returns>
        public bool CambiarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (this._ventana.Asociado != null)
            {
                this._ventana.AsociadoFiltro = ((Asociado)this._ventana.Asociado).Nombre;
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }


        public void LimpiarCampos()
        {
            this._ventana.Id = null;
            //this._ventana.DescripcionFiltrar = null;
            //this._ventana.Fecha = null;

            this._ventana.Asociados = null;
            this._ventana.IdAsociadoFiltrar = null;
            this._ventana.NombreAsociadoFiltrar = null;
            this._ventana.AsociadoFiltro = null;

            this._ventana.AnualidadSeleccionada = null;

            this._ventana.Resultados = null;
            this._ventana.TotalHits = "0";

        }

    }
}
