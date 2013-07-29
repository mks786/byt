using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.MarcasTercero;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.MarcasTercero;
//using Trascend.Bolet.Cliente.Ventanas.InfoBolMarcaTeres;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Presentadores.MarcasTercero
{
    class PresentadorListaInfoBolMarcaTeres : PresentadorBase
    {

        private IListaInfoBolMarcaTeres _ventana;
        private MarcaTercero _marca;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ITipoInfobolServicios _tipoInfobolServicios;
        private IInfoBolMarcaTerServicios _infobolServicios;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaInfoBolMarcaTeres(IListaInfoBolMarcaTeres ventana, object marca)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._marca = (MarcaTercero)marca;

            this._tipoInfobolServicios = (ITipoInfobolServicios)Activator.GetObject(typeof(ITipoInfobolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoInfobolServicios"]);

            this._infobolServicios = (IInfoBolMarcaTerServicios)Activator.GetObject(typeof(IInfoBolMarcaTerServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolMarcaTerServicios"]);

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

            TipoInfobol aux = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaInfoBol,
                    Recursos.Ids.InfoBolMarcaTer);

                IList<TipoInfobol> tipoInfobol = this._tipoInfobolServicios.ConsultarTodos();

                IList<InfoBolMarcaTer> listaInfobolesMarca = ((MarcaTercero)this._marca).InfoBoles;

                foreach (InfoBolMarcaTer item in listaInfobolesMarca)
                {

                    TipoInfobol tipoInfobolMarca = item.TipoInfobol;
                    aux = this.BuscarTipoInfobol(tipoInfobol, tipoInfobolMarca);
                    if (aux != null)
                    {
                        //tipoInfobolMarca = null;
                        //tipoInfobolMarca = aux;
                        item.TipoInfobol = aux;
                    }

                }

                ((MarcaTercero)this._marca).InfoBoles = listaInfobolesMarca;

                this._ventana.InfoBolMarcaTeres = ((MarcaTercero)this._marca).InfoBoles;


                
                if (((MarcaTercero)this._marca).InfoBoles != null)
                    this._ventana.TotalHits = ((MarcaTercero)this._marca).InfoBoles.Count.ToString();
                else
                    this._ventana.TotalHits = "0";

                this._ventana.FocoPredeterminado();

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
        /// Método que invoca una nueva página "GestionarInfoBolMarcaTer" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrGestionarInfoBolMarcaTer(bool nuevo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (!nuevo)
            {
                ((InfoBolMarcaTer)this._ventana.InfoBolMarcaTerSeleccionado).Marca = this._marca;

                IList<InfoBolMarcaTer> infoboles = this._infobolServicios.ConsultarInfoBolMarcaTeresPorMarca(this._marca);
                InfoBolMarcaTer infobol = this.BuscarInfobolMarcaTer(infoboles, (InfoBolMarcaTer)this._ventana.InfoBolMarcaTerSeleccionado);

                ((InfoBolMarcaTer)this._ventana.InfoBolMarcaTerSeleccionado).TipoInfobol = infobol.TipoInfobol;
                ((InfoBolMarcaTer)this._ventana.InfoBolMarcaTerSeleccionado).Marca = this._marca;
                
                this.Navegar(new GestionarInfoBolMarcaTer(this._ventana.InfoBolMarcaTerSeleccionado));
            }
            else
            {
                InfoBolMarcaTer infoBol = new InfoBolMarcaTer();
                infoBol.Marca = this._marca;
                //infoBol.Id = null;
                this.Navegar(new GestionarInfoBolMarcaTer(infoBol));
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que invoca una nueva página "ConsultarMarca" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarMarca()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new GestionarMarcaTercero(this._marca));//,Recursos.Etiquetas.tabDatos));

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
    }
}
