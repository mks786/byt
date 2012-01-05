using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
//using Trascend.Bolet.Cliente.Ventanas.InfoBoles;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorListaBusquedas : PresentadorBase
    {
        private IListaBusquedas _ventana;
        private Marca _marca;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaBusquedas(IListaBusquedas ventana, object marca)
        {
            this._ventana = ventana;
            this._marca = (Marca)marca;
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaInfoBol,
                    Recursos.Ids.InfoBol);
                
                this._ventana.InfoBoles = ((Marca)this._marca).InfoBoles;
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
        /// Método que invoca una nueva página "GestionarInfoBol" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrGestionarInfoBol(bool nuevo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (!nuevo)
            {
                ((InfoBol)this._ventana.InfoBolSeleccionado).Marca = this._marca;
                this.Navegar(new GestionarInfoBol(this._ventana.InfoBolSeleccionado));
            }
            else
            {
                InfoBol infoBol = new InfoBol();
                infoBol.Marca = this._marca;
                infoBol.Id = int.MinValue;
                this.Navegar(new GestionarInfoBol(infoBol));
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

                //IEnumerable<Poder> poderesFiltrados = this._poderes;

                //if (!string.IsNullOrEmpty(this._ventana.Id))
                //{
                //    poderesFiltrados = from p in poderesFiltrados
                //                       where p.Id == Int32.Parse(this._ventana.Id)
                //                       select p;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.NumPoder))
                //{
                //    poderesFiltrados = from p in poderesFiltrados
                //                       where p.NumPoder != null &&
                //                       p.NumPoder.ToLower().Contains(this._ventana.NumPoder.ToLower())
                //                       select p;
                //}

                //if (this._ventana.Boletin != null && !((Boletin)this._ventana.Boletin).Id.Equals(int.MinValue))
                //{
                //    Boletin boletin = (Boletin)this._ventana.Boletin;
                //    poderesFiltrados = from p in poderesFiltrados
                //                       where p.Boletin != null &&
                //                       p.Boletin.Id.ToString().ToLower().Contains(boletin.Id.ToString().ToLower())
                //                       select p;
                //}

                //if (this._ventana.Interesado != null && !((Interesado)this._ventana.Interesado).Id.Equals(int.MinValue))
                //{
                //    Interesado interesado = (Interesado)this._ventana.Interesado;
                //    poderesFiltrados = from p in poderesFiltrados
                //                       where p.Interesado != null &&
                //                       p.Interesado.Id.ToString().ToLower().Contains(interesado.Id.ToString().ToLower())
                //                       select p;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.Facultad))
                //{
                //    poderesFiltrados = from p in poderesFiltrados
                //                       where p.Facultad != null &&
                //                       p.Facultad.ToLower().Contains(this._ventana.Facultad.ToLower())
                //                       select p;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.Anexo))
                //{
                //    poderesFiltrados = from p in poderesFiltrados
                //                       where p.Anexo != null &&
                //                       p.Anexo.ToLower().Contains(this._ventana.Anexo.ToLower())
                //                       select p;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.Observaciones))
                //{
                //    poderesFiltrados = from p in poderesFiltrados
                //                       where p.Observaciones != null &&
                //                       p.Observaciones.ToLower().Contains(this._ventana.Observaciones.ToLower())
                //                       select p;
                //}

                //this._ventana.Resultados = poderesFiltrados.ToList<Poder>();

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
    }
}
