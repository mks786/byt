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
using Trascend.Bolet.Cliente.Contratos.Corresponsales;
using Trascend.Bolet.Cliente.Ventanas.Corresponsales;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Corresponsales
{
    class PresentadorConsultarCorresponsales : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IConsultarCorresponsales _ventana;


        private ICorresponsalServicios _corresponsalServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private IPaisServicios _paisServicios;
        private IIdiomaServicios _idiomaServicios;


        private IList<Corresponsal> _corresponsales;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarCorresponsales(IConsultarCorresponsales ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._corresponsalServicios = (ICorresponsalServicios)Activator.GetObject(typeof(ICorresponsalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CorresponsalServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._idiomaServicios = (IIdiomaServicios)Activator.GetObject(typeof(IIdiomaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["IdiomaServicios"]);

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
        /// Método que actualiza el título de la ventana a Consultar Corresponsal
        /// </summary>
        public void ActualizarTitulo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarCorresponsales,
            //Recursos.Ids.ConsultarCorresponsales);
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

                this._corresponsales = this._corresponsalServicios.ConsultarTodos();

                this._ventana.Resultados = this._corresponsales;
                this._ventana.CorresponsalFiltrar = new Corresponsal();
                this._ventana.TotalHits = this._corresponsales.Count.ToString();

                //IList<Idioma> idiomas = this._idiomaServicios.ConsultarTodos();
                //this._ventana.Idiomas = idiomas;

                //IList<Pais> paises = this._paisServicios.ConsultarTodos();
                //this._ventana.Paises = paises;

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

                Corresponsal corresponsal = (Corresponsal)this._ventana.CorresponsalFiltrar;
                corresponsal.Id = !this._ventana.GetIdCorresponsal.Equals(string.Empty) ? int.Parse(this._ventana.GetIdCorresponsal) : 0; 

                IEnumerable<Corresponsal> corresponsalesFiltrados = this._corresponsales;

                if (corresponsal.Id != 0)
                {
                    corresponsalesFiltrados = from a in corresponsalesFiltrados
                                       where a.Id==corresponsal.Id
                                       select a;
                }

                if (!string.IsNullOrEmpty(corresponsal.Descripcion))
                {
                    corresponsalesFiltrados = from a in corresponsalesFiltrados
                                              where a.Descripcion != null &&
                                       a.Descripcion.ToLower().Contains(corresponsal.Descripcion.ToLower())
                                       select a;
                }

                //if (!string.IsNullOrEmpty(corresponsal.Domicilio))
                //{
                //    corresponsalesFiltrados = from a in corresponsalesFiltrados
                //                       where a.Domicilio != null &&
                //                       a.Domicilio.ToLower().Contains(corresponsal.Domicilio.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(corresponsal.Telefono1))
                //{
                //    corresponsalesFiltrados = from a in corresponsalesFiltrados
                //                              where a.Telefono1 != null && a.Telefono1.ToLower().Contains(corresponsal.Telefono1.ToLower())
                //                              select a;
                //}

                //if (!string.IsNullOrEmpty(corresponsal.Telefono2))
                //{
                //    corresponsalesFiltrados = from a in corresponsalesFiltrados
                //                              where a.Telefono2 != null && a.Telefono2.ToLower().Contains(corresponsal.Telefono2.ToLower())
                //                              select a;
                //}

                //if (!((ListaDatosDominio)this._ventana.EstadoCivil).Id.Equals("NGN"))
                //{
                //    corresponsalesFiltrados = from a in corresponsalesFiltrados
                //                       where a.EstadoCivil == ((ListaDatosDominio)this._ventana.EstadoCivil).Id[0]
                //                       select a;
                //}

                //if (this._ventana.Sexo != null && !((ListaDatosValores)this._ventana.Sexo).Id.Equals("NGN"))
                //{
                //    corresponsalesFiltrados = from a in corresponsalesFiltrados
                //                       where a.Sexo.ToString().ToLower().Contains(((ListaDatosValores)this._ventana.Sexo).Valor.ToLower())
                //                       select a;

                //}

                //if (!string.IsNullOrEmpty(corresponsal.NumeroAbogado))
                //{
                //    corresponsalesFiltrados = from a in corresponsalesFiltrados
                //                       where a.NumeroAbogado != null &&
                //                       a.NumeroAbogado.ToLower().Contains(corresponsal.NumeroAbogado.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(corresponsal.NumeroImpresoAbogado))
                //{
                //    corresponsalesFiltrados = from a in corresponsalesFiltrados
                //                       where a.NumeroImpresoAbogado != null &&
                //                       a.NumeroImpresoAbogado.ToLower().Contains(corresponsal.NumeroImpresoAbogado.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(corresponsal.NumeroPropiedad))
                //{
                //    corresponsalesFiltrados = from a in corresponsalesFiltrados
                //                       where a.NumeroPropiedad != null &&
                //                       a.NumeroPropiedad.ToLower().Contains(corresponsal.NumeroPropiedad.ToLower())
                //                       select a;
                //}

                //if (!string.IsNullOrEmpty(corresponsal.CCI))
                //{
                //    corresponsalesFiltrados = from a in corresponsalesFiltrados
                //                       where a.CCI != null &&
                //                       a.CCI.ToLower().Contains(corresponsal.CCI.ToLower())
                //                       select a;
                //}
                int cantidadDeCorresponsalesFiltrados = corresponsalesFiltrados.ToList<Corresponsal>().Count;
                if (cantidadDeCorresponsalesFiltrados != 0)
                {
                    this._ventana.Resultados = corresponsalesFiltrados.ToList<Corresponsal>();
                    this._ventana.TotalHits = corresponsalesFiltrados.ToList<Corresponsal>().Count.ToString();
                }
                else
                {
                    this._ventana.Resultados = _corresponsales;
                    this._ventana.TotalHits = _corresponsales.Count.ToString();
                }
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
        /// Método que invoca una nueva página "ConsultarCorresponsal" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarCorresponsal()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new AgregarCorresponsal(this._ventana, this._ventana.CorresponsalSeleccionado));

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
        /// Método que limpia los campos de búsqueda
        /// </summary>
        public void LimpiarCampos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.CorresponsalFiltrar = new Corresponsal();
            //this._ventana.Pais = ((IList<Pais>)this._ventana.Paises)[0];
            //this._ventana.Idioma = ((IList<Idioma>)this._ventana.Idiomas)[0];

            this._ventana.Resultados = this._corresponsales;
            this._ventana.TotalHits = (this._corresponsales).Count.ToString();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

    }
}
