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
using Trascend.Bolet.Cliente.Contratos.CartaOuts;
using Trascend.Bolet.Cliente.Ventanas.Anexos;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Anexos
{
    class PresentadorConsultarCartaOuts : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarCartaOuts _ventana;
        private ICartaOutServicios _cartaOutServicios;
        private IList<CartaOut> _cartas;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarCartaOuts(IConsultarCartaOuts ventana)
        {
            try
            {
                this._ventana = ventana;
                this._cartaOutServicios = (ICartaOutServicios)Activator.GetObject(typeof(ICartaOutServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaOutServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        /// <summary>
        /// Metodo que se encarga de cambiar el titulo de la ventana
        /// </summary>
        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarAnexos, "");
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

                this._cartas = this._cartaOutServicios.ConsultarTodos();
                this._ventana.Resultados = this._cartas;
                this._ventana.CartaOutFiltrar = new CartaOut();
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

                CartaOut cartaOut = (CartaOut)this._ventana.CartaOutFiltrar;

                IEnumerable<CartaOut> cartasOutFiltrados = this._cartas;

                if (!string.Equals("", this._ventana.Id))
                {
                    cartasOutFiltrados = from a in cartasOutFiltrados
                                      where a.Id == this._ventana.Id
                                      select a;
                }

                if (!string.IsNullOrEmpty(((CartaOut)this._ventana.CartaOutFiltrar).Medio))
                {
                    cartasOutFiltrados = from p in cartasOutFiltrados
                                         where p.Medio != null &&
                                          p.Medio.Contains(cartaOut.Medio)
                                         select p;
                }

                if (!string.IsNullOrEmpty((this._ventana.IdAsociado).ToString()))
                {
                    cartasOutFiltrados = from p in cartasOutFiltrados
                                         where p.Id != null &&
                                          p.Asociado.Id == this._ventana.IdAsociado
                                         select p;
                }

                if (!string.IsNullOrEmpty(((CartaOut)this._ventana.CartaOutFiltrar).DescripcionDepartamento))
                {
                    cartasOutFiltrados = from p in cartasOutFiltrados
                                         where p.DescripcionDepartamento != null &&
                                          p.DescripcionDepartamento.Contains(cartaOut.DescripcionDepartamento)
                                         select p;
                }

                if (!string.IsNullOrEmpty(((CartaOut)this._ventana.CartaOutFiltrar).Persona))
                {
                    cartasOutFiltrados = from p in cartasOutFiltrados
                                         where p.Persona != null &&
                                          p.Persona.Contains(cartaOut.Persona)
                                         select p;
                }

                if (!string.IsNullOrEmpty(((CartaOut)this._ventana.CartaOutFiltrar).Referencia))
                {
                    cartasOutFiltrados = from p in cartasOutFiltrados
                                         where p.Referencia != null &&
                                          p.Referencia.Contains(cartaOut.Referencia)
                                         select p;
                }

                if (!string.IsNullOrEmpty(((CartaOut)this._ventana.CartaOutFiltrar).Receptor))
                {
                    cartasOutFiltrados = from p in cartasOutFiltrados
                                         where p.Receptor != null &&
                                          p.Receptor.Contains(cartaOut.Receptor)
                                         select p;
                }

                //if (!string.IsNullOrEmpty(((CartaOut)this._ventana.CartaOutFiltrar).))
                //{
                //    cartasOutFiltrados = from p in cartasOutFiltrados
                //                         where p.Receptor != null &&
                //                          p.Receptor.Contains(cartaOut.Receptor)
                //                         select p;
                //}

                this._ventana.Resultados = cartasOutFiltrados.ToList<CartaOut>();

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

        ///<summary>
        //Método que invoca una nueva página "ConsultarAnexo" y la instancia con el objeto seleccionado
        /// </summary>
        //public void IrConsultarAnexo()
        //{
        //    #region trace
        //    if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
        //        logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //    #endregion

        //    this.Navegar(new ConsultarAnexo(this._ventana.CartaOutSeleccionado));

        //    #region trace
        //    if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
        //        logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //    #endregion
        //}

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
