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
using Trascend.Bolet.Cliente.Contratos.Abandonos;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Abandonos;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Abandonos
{
    class PresentadorConsultarAbandonos : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarAbandonos _ventana;
        private IMarcaServicios _marcaServicios;        
        private IList<Marca> _marcas;
        private IList<Operacion> _abandonos;
        private IOperacionServicios _operacionServicios;
        
        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarAbandonos(IConsultarAbandonos ventana)
        {
            try
            {
                this._ventana = ventana;
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
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

                Operacion operacionAuxiliar = new Operacion();

                if (!this._ventana.Id.Equals(""))
                {
                    operacionAuxiliar.Id = int.Parse(this._ventana.Id);
                    filtroValido = 2;
                }

                if ((null != this._ventana.Marca) && (((Marca)this._ventana.Marca).Id != int.MinValue))
                {
                    operacionAuxiliar.Marca = (Marca)this._ventana.Marca;
                    filtroValido = 2;
                }

                if ((null != this._ventana.Fecha) && (!this._ventana.Fecha.Equals("")))
                {
                    DateTime fechaCesion = DateTime.Parse(this._ventana.Fecha);
                    filtroValido = 2;
                    operacionAuxiliar.Fecha = fechaCesion;
                }

                operacionAuxiliar.Aplicada = 'M';

                Servicio servicioAuxiliar = new Servicio();
                servicioAuxiliar.Id = "AB";

                operacionAuxiliar.Servicio = servicioAuxiliar;

                if (filtroValido >= 2)
                {
                    Marca marcaAuxiliar = new Marca();
                    marcaAuxiliar.Id = operacionAuxiliar.CodigoAplicada;

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
                this.Navegar(new GestionarAbandono(this._ventana.AbandonoSeleccionado));
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
        /// Método que busca las marcas registradas
        /// </summary>
        public void BuscarMarca()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Marca marca = new Marca();
            IEnumerable<Marca> marcasFiltradas;
            marca.Descripcion = this._ventana.NombreMarcaFiltrar.ToUpper();
            marca.Id = this._ventana.IdMarcaFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdMarcaFiltrar);
            if ((!marca.Descripcion.Equals("")) || (marca.Id != 0))
                marcasFiltradas = this._marcaServicios.ObtenerMarcasFiltro(marca);
            else
                marcasFiltradas = new List<Marca>();

            if (marcasFiltradas.ToList<Marca>().Count != 0)
                this._ventana.Marcas = marcasFiltradas.ToList<Marca>();
            else
                this._ventana.Marcas = this._marcas;

            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// Método que permite seleccionar marcas
        /// </summary>
        public bool ElegirMarca()
        {
            bool retorno = false;
            if (this._ventana.Marca != null)
            {
                retorno = true;
                this._ventana.NombreMarca = ((Marca)this._ventana.Marca).Descripcion;
                this._ventana.IdMarca = ((Marca)this._ventana.Marca).Id.ToString();
            }

            return retorno;
        }

        /// <summary>
        /// Método que limpia los campos de búsqueda
        /// </summary>
        public void LimpiarCampos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.Id = null;
            this._ventana.IdMarca = null;
            this._ventana.Fecha = null;
            this._ventana.AbandonoSeleccionado = null;
            this._ventana.IdMarcaFiltrar = null;
            this._ventana.Marca = null;
            this._ventana.Marcas = null;
            this._ventana.NombreMarca = null;
            this._ventana.NombreMarcaFiltrar = null;
            this._ventana.Resultados = null;
            this._ventana.TotalHits = "0";

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }

}
