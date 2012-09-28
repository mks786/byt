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
using Trascend.Bolet.Cliente.Contratos.Paises;
using Trascend.Bolet.Cliente.Ventanas.Paises;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Paises
{
    class PresentadorConsultarPaises : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IConsultarPaises _ventana;
        private IPaisServicios _paisServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IList<Pais> _paises;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarPaises(IConsultarPaises ventana)
        {
            try
            {
                this._ventana = ventana;
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Método que se encarga de actualizar el título de la ventana
        /// </summary>
        public void ActualizarTitulo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarPaises,
                Recursos.Ids.ConsultarPaises);

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

                this._paises = this._paisServicios.ConsultarTodos();
                this._ventana.Resultados = this._paises;
                this._ventana.TotalHits = this._paises.Count.ToString();
                this._ventana.PaisFiltrar = new Pais();
                this._ventana.FocoPredeterminado();

                this.CargarRegiones();

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
        /// Método que carga las regiones iniciales
        /// </summary>
        private void CargarRegiones()
        {
            ListaDatosValores listaAux = new ListaDatosValores("CONTINENTE");
            this._ventana.Regiones = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(listaAux);

            ListaDatosValores primerValor = new ListaDatosValores("NGN");
            primerValor.Descripcion = "";
            ((IList<ListaDatosValores>)this._ventana.Regiones).Insert(0, primerValor);
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

                Pais pais = (Pais)this._ventana.PaisFiltrar;
                pais.Region = !((ListaDatosValores)this._ventana.Region).Descripcion.Equals("") ? ((ListaDatosValores)this._ventana.Region).Descripcion : null;

                IEnumerable<Pais> paisesFiltrados = this._paises;

                if (!string.IsNullOrEmpty(this._ventana.Id))
                {
                    paisesFiltrados = from a in paisesFiltrados
                                      where a.Id.ToString().Contains(this._ventana.Id)
                                      select a;
                }

                if (!string.IsNullOrEmpty(pais.Codigo))
                {
                    paisesFiltrados = from p in paisesFiltrados
                                      where p.Codigo != null &&
                                      p.Codigo.ToLower().Contains(pais.Codigo.ToLower())
                                      select p;
                }

                if (!string.IsNullOrEmpty(pais.NombreIngles))
                {
                    paisesFiltrados = from p in paisesFiltrados
                                      where p.NombreIngles != null &&
                                      p.NombreIngles.ToLower().Contains(pais.NombreIngles.ToLower())
                                      select p;
                }

                if (!string.IsNullOrEmpty(pais.NombreEspanol))
                {
                    paisesFiltrados = from p in paisesFiltrados
                                      where p.NombreEspanol != null &&
                                      p.NombreEspanol.ToLower().Contains(pais.NombreEspanol.ToLower())
                                      select p;
                }

                if (!string.IsNullOrEmpty(pais.Nacionalidad))
                {
                    paisesFiltrados = from p in paisesFiltrados
                                      where p.Nacionalidad != null &&
                                      p.Nacionalidad.ToLower().Contains(pais.Nacionalidad.ToLower())
                                      select p;
                }

                if (!string.IsNullOrEmpty(pais.Region))
                {
                    paisesFiltrados = from p in paisesFiltrados
                                      where p.Region != null &&
                                      p.Region.ToLower().Contains(pais.Region.ToLower())
                                      select p;
                }

                this._ventana.Resultados = paisesFiltrados.ToList<Pais>();
                this._ventana.TotalHits = paisesFiltrados.ToList<Pais>().Count.ToString();

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
        /// Método que invoca una nueva página "ConsultarPais" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarPais()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ConsultarPais(this._ventana.PaisSeleccionado));

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


        public void LimpiarCampos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.Id = null;
            this._ventana.PaisFiltrar = new Pais();
            this._ventana.PaisSeleccionado = null;
            this._ventana.Region = ((IList<ListaDatosValores>)this._ventana.Regiones)[0];


            this._ventana.Resultados = this._paises;
            this._ventana.TotalHits = this._paises.Count.ToString();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}