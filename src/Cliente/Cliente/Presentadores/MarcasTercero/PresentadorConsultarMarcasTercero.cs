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
using Trascend.Bolet.Cliente.Contratos.MarcasTercero;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.MarcasTercero;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.MarcasTercero
{
    class PresentadorConsultarMarcasTercero : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarMarcasTercero _ventana;
        private IMarcaTerceroServicios _marcaTerceroServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IList<MarcaTercero> _marcasTercero;
        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarMarcasTercero(IConsultarMarcasTercero ventana)
        {
            try
            {
                this._ventana = ventana;
                this._marcaTerceroServicios = (IMarcaTerceroServicios)Activator.GetObject(typeof(IMarcaTerceroServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaTerceroServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarcasTercero,
                Recursos.Ids.ConsultarMarcasTercero);
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
                this._ventana.Interesados = interesados;
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

                MarcaTercero MarcaTerceroAuxiliar = new MarcaTercero();

                if (!this._ventana.Id.Equals(""))
                {
                    filtroValido = 2;
                    MarcaTerceroAuxiliar.Id = this._ventana.Id; 
                }

                if ((null != this._ventana.Asociado) && (((Asociado)this._ventana.Asociado).Id != int.MinValue))
                {
                    MarcaTerceroAuxiliar.Asociado = (Asociado)this._ventana.Asociado;
                    filtroValido = 2;
                }

                if ((null != this._ventana.Interesado) && (((Interesado)this._ventana.Interesado).Id != int.MinValue))
                {
                    MarcaTerceroAuxiliar.Interesado = (Interesado)this._ventana.Interesado;
                    filtroValido = 2;
                }


                if (!this._ventana.FichasFiltrar.Equals(""))
                {
                    MarcaTerceroAuxiliar.Fichas = this._ventana.FichasFiltrar.ToUpper();
                    filtroValido++;
                }


                if (!this._ventana.DescripcionFiltrar.Equals(""))
                {
                    filtroValido = 2;
                    MarcaTerceroAuxiliar.Descripcion = this._ventana.DescripcionFiltrar.ToUpper();
                }

                if (!this._ventana.Fecha.Equals(""))
                {
                    DateTime fechaPublicacion = DateTime.Parse(this._ventana.Fecha);
                    filtroValido = 2;
                    MarcaTerceroAuxiliar.FechaPublicacion = fechaPublicacion;
                }

                if (filtroValido >= 2)
                {
                    this._marcasTercero = this._marcaTerceroServicios.ObtenerMarcaTerceroFiltro(MarcaTerceroAuxiliar);
                    if (_marcasTercero.Count() != 0)
                    {
                        MarcaTercero MarcaTerceroComparadora = _marcasTercero[0];
                    }
                    int comprobador = 0;
                    IList<MarcaTercero> marcasTerceroDesinfladas = new List<MarcaTercero>();

                    foreach (var marcaTercero in this._marcasTercero)
                    {
                        MarcaTerceroAuxiliar = new MarcaTercero(marcaTercero.Id);
                        Asociado asociadoAuxiliar = new Asociado();
                        Interesado interesadoAuxiliar = new Interesado();

                        MarcaTerceroAuxiliar.Descripcion = marcaTercero.Descripcion != null ? marcaTercero.Descripcion : "";
                        MarcaTerceroAuxiliar.Anexo = marcaTercero.Anexo;
                        if ((marcaTercero.Asociado != null) && (!string.IsNullOrEmpty(marcaTercero.Asociado.Nombre)))
                        {
                            asociadoAuxiliar.Nombre = marcaTercero.Asociado.Nombre;
                            MarcaTerceroAuxiliar.Asociado = asociadoAuxiliar;
                        }

                        if ((marcaTercero.Interesado != null) && (!string.IsNullOrEmpty(marcaTercero.Interesado.Nombre)))
                        {
                            interesadoAuxiliar.Nombre = marcaTercero.Interesado.Nombre;
                            MarcaTerceroAuxiliar.Interesado = interesadoAuxiliar;
                        }


                        MarcaTerceroAuxiliar.FechaPublicacion = marcaTercero.FechaPublicacion != null ? marcaTercero.FechaPublicacion : null;
                        marcasTerceroDesinfladas.Add(MarcaTerceroAuxiliar);
                        //Filtra las busquedas repetidas
                        //if ((MarcaTerceroComparadora.Id == MarcaTerceroAuxiliar.Id) && (MarcaTerceroComparadora.Anexo == MarcaTerceroAuxiliar.Anexo))
                        //{
                        //    if (comprobador <= 0)
                        //    {
                        //        marcasTerceroDesinfladas.Add(MarcaTerceroAuxiliar);
                        //        comprobador++;
                        //    }
                        //}
                        //else
                        //{
                        //    marcasTerceroDesinfladas.Add(MarcaTerceroAuxiliar);
                        //}
                    }

                    this._ventana.Resultados = marcasTerceroDesinfladas;
                    this._ventana.TotalHits = marcasTerceroDesinfladas.Count.ToString();
                    if (marcasTerceroDesinfladas.Count == 0)
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
        public void IrConsultarMarcaTercero()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.MarcaTerceroSeleccionada != null)
            {
                MarcaTercero marcaTerceroParaNavegar = null;
                bool encontrada = false;
                int cont = 0;
                while (!encontrada)
                {
                    MarcaTercero marcaTercero = this._marcasTercero[cont];
                    if ((marcaTercero.Id == ((MarcaTercero)this._ventana.MarcaTerceroSeleccionada).Id) &&
                        (marcaTercero.Anexo == ((MarcaTercero)this._ventana.MarcaTerceroSeleccionada).Anexo))
                    {
                        marcaTerceroParaNavegar = marcaTercero;
                        encontrada = true;
                    }
                    cont++;
                }


                this.Navegar(new ConsultarMarcaTercero(marcaTerceroParaNavegar));
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
        /// Método que se encarga de buscar el interesado definido en el filtro
        /// </summary>
        public void BuscarInteresado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Interesado interesadoABuscar = new Interesado();

            interesadoABuscar.Id = !this._ventana.IdInteresadoFiltrar.Equals("") ?
                                   int.Parse(this._ventana.IdInteresadoFiltrar) : 0;

            interesadoABuscar.Nombre = !this._ventana.NombreInteresadoFiltrar.Equals("") ?
                                       this._ventana.NombreInteresadoFiltrar.ToUpper() : "";

            if ((interesadoABuscar.Id != 0) || !(interesadoABuscar.Nombre.Equals("")))
            {
                IList<Interesado> interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoABuscar);
                interesados.Insert(0,new Interesado(int.MinValue));
                this._ventana.Interesados = interesados;
            }
            else
            {
                this._ventana.Interesados = this._interesados;
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

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
                asociados.Insert(0,new Asociado(int.MinValue));
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
        /// Metodo que cambia el texto del interesado en la interfaz
        /// </summary>
        /// <returns>true en caso de que el interesado haya sido valido, false en caso contrario</returns>
        public bool CambiarInteresado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (this._ventana.Interesado != null)
            {
                this._ventana.InteresadoFiltro = ((Interesado)this._ventana.Interesado).Nombre;
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;

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
                retorno =  true;
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
            this._ventana.DescripcionFiltrar = null;
            this._ventana.FichasFiltrar = null;
            this._ventana.Fecha = null;

            this._ventana.Asociados = null;
            this._ventana.IdAsociadoFiltrar = null;
            this._ventana.NombreAsociadoFiltrar = null;
            this._ventana.AsociadoFiltro = null;

            this._ventana.Interesados = null; 
            this._ventana.IdInteresadoFiltrar = null;
            this._ventana.NombreInteresadoFiltrar = null;
            this._ventana.InteresadoFiltro = null;

            this._ventana.MarcaTerceroSeleccionada = null;

            this._ventana.Resultados = null;
            this._ventana.TotalHits = "0";

        }
    }
}
