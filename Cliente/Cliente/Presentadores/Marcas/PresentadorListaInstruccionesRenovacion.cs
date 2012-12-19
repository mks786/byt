using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Cartas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorListaInstruccionesRenovacion : PresentadorBase
    {

        private IListaInstruccionesRenovacion _ventana;
        private Marca _marca;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private ICartaServicios _cartaServicios;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaInstruccionesRenovacion(IListaInstruccionesRenovacion ventana, object marca)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;

            this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
            this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);

            this._marca = (Marca)marca;

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

                //this._ventana.BusquedaFiltrar = new Busqueda();
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleInstruccionesDeRenovacion,
                    Recursos.Ids.InstruccionesDeRenovacion);

                this._ventana.IdMarca = this._marca.Id.ToString();

                foreach (InstruccionDeRenovacion instruccionDeRenovacion in (((Marca)this._marca).InstruccionesDeRenovacion))
                {
                    instruccionDeRenovacion.Marca = this._marca;
                }

                this._ventana.Resultados = ((Marca)this._marca).InstruccionesDeRenovacion;
                this._ventana.TotalHits = ((Marca)this._marca).InstruccionesDeRenovacion.Count.ToString();
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
        public void IrGestionarInstruccionDeRenovacion(bool nuevo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (!nuevo)
            {
                ((InstruccionDeRenovacion)this._ventana.InstruccionSeleccionada).Marca = this._marca;
                this.Navegar(new GestionarInstruccionDeRenovacion(this._ventana.InstruccionSeleccionada));
            }
            else
            {
                InstruccionDeRenovacion instruccion = new InstruccionDeRenovacion();
                instruccion.Marca = this._marca;
                this.Navegar(new GestionarInstruccionDeRenovacion(instruccion));
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
        /// Método que se encarga de consultar una Busqueda
        /// </summary>
        public void Consultar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Busqueda busqueda = (Busqueda)this._ventana.BusquedaFiltrar;
                //busqueda.TipoBusqueda = ((ListaDatosDominio)this._ventana.TipoBusqueda).Id != "NGN" ? ((ListaDatosDominio)this._ventana.TipoBusqueda).Id[0] : (char?) null ;

                //IEnumerable<Busqueda> busquedasFiltradas = this._marca.Busquedas;

                //if (!string.IsNullOrEmpty(this._ventana.IdBusqueda))
                //{
                //    busquedasFiltradas = from b in busquedasFiltradas
                //                         where b.Id == int.Parse(this._ventana.IdBusqueda)
                //                         select b;
                //}

                //if (!string.IsNullOrEmpty(busqueda.FechaBusquedaDiseno.ToString()))
                //{
                //    busquedasFiltradas = from b in busquedasFiltradas
                //                         where b.FechaBusquedaDiseno != null &&
                //                         b.FechaBusquedaDiseno == busqueda.FechaBusquedaDiseno
                //                         select b;
                //}

                //if (!string.IsNullOrEmpty(busqueda.FechaBusquedaPalabra.ToString()))
                //{
                //    busquedasFiltradas = from b in busquedasFiltradas
                //                         where b.FechaBusquedaPalabra != null &&
                //                         b.FechaBusquedaPalabra == busqueda.FechaBusquedaPalabra
                //                         select b;
                //}

                //if (!string.IsNullOrEmpty(busqueda.FechaConsigDiseno.ToString()))
                //{
                //    busquedasFiltradas = from b in busquedasFiltradas
                //                         where b.FechaConsigDiseno != null &&
                //                         b.FechaConsigDiseno == busqueda.FechaConsigDiseno
                //                         select b;
                //}

                //if (!string.IsNullOrEmpty(busqueda.FechaConsigPalabra.ToString()))
                //{
                //    busquedasFiltradas = from b in busquedasFiltradas
                //                         where b.FechaConsigPalabra != null &&
                //                         b.FechaConsigPalabra == busqueda.FechaConsigPalabra
                //                         select b;
                //}

                //if (!string.IsNullOrEmpty(busqueda.FechaSolicitudPalabra.ToString()))
                //{
                //    busquedasFiltradas = from b in busquedasFiltradas
                //                         where b.FechaSolicitudPalabra != null &&
                //                         b.FechaSolicitudPalabra == busqueda.FechaSolicitudPalabra
                //                         select b;
                //}

                //if (null != busqueda.TipoBusqueda)
                //{
                //    busquedasFiltradas = from b in busquedasFiltradas
                //                         where b.TipoBusqueda != null &&
                //                         b.TipoBusqueda == busqueda.TipoBusqueda
                //                         select b;
                //}

                //this._ventana.Resultados = busquedasFiltradas.ToList<Busqueda>();
                //this._ventana.TotalHits = busquedasFiltradas.ToList<Busqueda>().Count.ToString();

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
        /// Método que invoca una nueva página "ConsultarMarca" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarMarca()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ConsultarMarca(this._marca, this._ventana.Tab));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        public void IrConsultarCarta()
        {
            Carta carta = new Carta();
            carta.Id = ((InstruccionDeRenovacion)this._ventana.InstruccionSeleccionada).Carta.Id;
            IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(carta);
            Navegar(new ConsultarCarta(cartas[0], this._ventana));
        }

    }
}
