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
using Trascend.Bolet.Cliente.Contratos.Renovaciones;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Renovaciones;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Renovaciones
{
    class PresentadorConsultarRenovaciones : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarRenovaciones _ventana;
        private IMarcaServicios _marcaServicios;
        private IRenovacionServicios _renovacionServicios;    
        private IList<Marca> _marcas;
        private IList<Renovacion> _renovaciones;        

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarRenovaciones(IConsultarRenovaciones ventana)
        {
            try
            {
                this._ventana = ventana;
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._renovacionServicios = (IRenovacionServicios)Activator.GetObject(typeof(IRenovacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RenovacionServicios"]);                
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarRenovaciones,
                Recursos.Ids.ConsultarRenovaciones);
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

                IList<Marca> marcas = new List<Marca>();
                Marca primeraMarca = new Marca();
                primeraMarca.Id = int.MinValue;
                marcas.Insert(0, primeraMarca);
                this._ventana.Marcas = marcas;
                this._marcas = marcas;

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

                Renovacion RenovacionAuxiliar = new Renovacion();

                if (!this._ventana.Id.Equals(""))
                {
                    filtroValido = 2;
                    RenovacionAuxiliar.Id = int.Parse(this._ventana.Id);
                }

                if ((null != this._ventana.Marca) && (((Asociado)this._ventana.Marca).Id != int.MinValue))
                {
                    RenovacionAuxiliar.Marca = (Marca)this._ventana.Marca;
                    filtroValido = 2;
                }                             

                if (filtroValido >= 2)
                {
                    this._renovaciones = this._renovacionServicios.ObtenerRenovacionFiltro(RenovacionAuxiliar);

                    IList<Renovacion> renovacionesDesinfladas = new List<Renovacion>();

                    foreach (var renovacion in this._renovaciones)
                    {
                        RenovacionAuxiliar = new Renovacion(renovacion.Id);
                        Marca marcaAuxiliar = new Marca();
                        Interesado interesadoAuxiliar = new Interesado();                        

                        if ((renovacion.Marca != null) && (!string.IsNullOrEmpty(renovacion.Marca.Descripcion)))
                        {
                            marcaAuxiliar.Descripcion = renovacion.Marca.Descripcion;
                            RenovacionAuxiliar.Marca = marcaAuxiliar;
                        }

                        if ((renovacion.Interesado != null) && (!string.IsNullOrEmpty(renovacion.Interesado.Nombre)))
                        {
                            interesadoAuxiliar.Nombre = renovacion.Interesado.Nombre;
                            RenovacionAuxiliar.Interesado = interesadoAuxiliar;
                        }

                        RenovacionAuxiliar.FechaRenovacion = renovacion.FechaRenovacion != null ? renovacion.FechaRenovacion : null;

                        RenovacionAuxiliar.FechaRenovacionProxima = renovacion.FechaRenovacionProxima != null ? renovacion.FechaRenovacionProxima : null;

                        renovacionesDesinfladas.Add(RenovacionAuxiliar);

                    }

                    this._ventana.Resultados = renovacionesDesinfladas;
                    this._ventana.TotalHits = renovacionesDesinfladas.Count.ToString();
                    if (renovacionesDesinfladas.Count == 0)
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
        /// Método que invoca una nueva página "ConsultarRenovacion" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarRenovacion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.RenovacionSeleccionada != null)
            {
                Renovacion renovacionesParaNavegar = null;
                bool encontrada = false;
                int cont = 0;
                while (!encontrada)
                {
                    Renovacion renovacion = this._renovaciones[cont];
                    if (renovacion.Id == ((Marca)this._ventana.RenovacionSeleccionada).Id)
                    {
                        renovacionesParaNavegar = renovacion;
                        encontrada = true;
                    }
                    cont++;
                }


                //this.Navegar(new ConsultarRenovacion(renovacionesParaNavegar));
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
        /// Método que se encarga de buscar la renovacion definido en el filtro
        /// </summary>
        public void BuscarRenovacion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Renovacion renovacionABuscar = new Renovacion();

            renovacionABuscar.Id = !this._ventana.Id.Equals("") ?
                                   int.Parse(this._ventana.Id) : 0;

            if (renovacionABuscar.Id != 0)
            {
                IList<Renovacion> renovaciones = this._renovacionServicios.ObtenerRenovacionFiltro(renovacionABuscar);
                renovaciones.Insert(0, new Renovacion(int.MinValue));
                this._ventana.Resultados = renovaciones;
            }
            else
            {
                this._ventana.Resultados = this._renovaciones;
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        /// <summary>
        /// Método que se encarga de buscar la Marca definida en el filtro
        /// </summary>
        public void BuscarMarca()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            Marca marcaABuscar = new Marca();

            marcaABuscar.Id = !this._ventana.IdMarcaFiltrar.Equals("") ?
                                 int.Parse(this._ventana.IdMarcaFiltrar) : 0;

            marcaABuscar.Descripcion = !this._ventana.NombreMarcaFiltrar.Equals("") ?
                                     this._ventana.NombreMarcaFiltrar.ToUpper() : "";

            if ((marcaABuscar.Id != 0) || !(marcaABuscar.Descripcion.Equals("")))
            {
                IList<Marca> marcas = this._marcaServicios.ObtenerMarcasFiltro(marcaABuscar);
                marcas.Insert(0, new Marca(int.MinValue));
                this._ventana.Marcas = marcas;

            }
            else
            {
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                this._ventana.Marcas = this._marcas;
            }

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Metodo que cambia el texto de la Marca en la interfaz
        /// </summary>
        /// <returns>true en caso de que la Marca haya sido valido, false en caso contrario</returns>
        public bool CambiarMarca()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (this._ventana.Marca != null)
            {
                this._ventana.MarcaFiltrada = ((Marca)this._ventana.Marca).Descripcion;
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }
    }
}
