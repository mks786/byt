using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Interesados;
using Trascend.Bolet.Cliente.Ventanas.Justificaciones;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorListaInteresadosMarca : PresentadorBase
    {
        private IListaInteresadosMarca _ventana;
        private Marca _marca;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IMarcaServicios _marcaServicios;
        private IInteresadoMultipleServicios _interesadoMultipleServicios;
        private InteresadoMultiple _interesadoMultiple;
        private object _ventanaConsultarMarcas;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaInteresadosMarca(IListaInteresadosMarca ventana, object marca, object ventanaPadre, object ventanaConsultarMarcas)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._marca = (Marca)marca;
                if (ventanaConsultarMarcas != null)
                    this._ventanaConsultarMarcas = ventanaConsultarMarcas;

                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._interesadoMultipleServicios = (IInteresadoMultipleServicios)Activator.GetObject(typeof(IInteresadoMultipleServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoMultipleServicios"]);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ":" + ex.Message, true);
            }
        }

        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            IList<Interesado> interesadosDeMarca = new List<Interesado>();

            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleInteresadosMultiplesMarca,
                    Recursos.Ids.InteresadoPatente);

                this._marca = this._marcaServicios.ConsultarMarcaConTodo(_marca);

                IList<InteresadoMultiple> listaInteresados = this._interesadoMultipleServicios.ConsultarInteresadosDeMarca(this._marca);

                if (listaInteresados.Count > 0)
                {
                    InteresadoMultiple intMarca = listaInteresados[0];
                    this._interesadoMultiple = intMarca;

                    if (intMarca.Interesado != null)
                        interesadosDeMarca.Add(intMarca.Interesado);
                    if (intMarca.Interesado1 != null)
                        interesadosDeMarca.Add(intMarca.Interesado1);
                    if (intMarca.Interesado2 != null)
                        interesadosDeMarca.Add(intMarca.Interesado2);
                    if (intMarca.Interesado3 != null)
                        interesadosDeMarca.Add(intMarca.Interesado3);

                    this._ventana.InteresadosDeMarca = interesadosDeMarca;
                    this._ventana.TotalHits = interesadosDeMarca.Count.ToString();
                }
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ":" + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ":" + ex.Message, true);
            }
        }


        public void IrVentanaConsultarMarca()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ConsultarMarca(this._marca, this._ventanaConsultarMarcas));

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ":" + ex.Message, true);
            }
        }


        /// <summary>
        /// Metodo que visualiza los datos de un Interesado seleccionado en la lista de Interesados de una Marca
        /// </summary>
        public void IrVerInteresadoSeleccionado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.InteresadoSeleccionado != null)
                {
                    Interesado interesadoSeleccionado = (Interesado)this._ventana.InteresadoSeleccionado;
                    this.Navegar(new ConsultarInteresado(interesadoSeleccionado, this._ventana));
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ":" + ex.Message, true);
            }
        }


        /// <summary>
        /// Metodo que muestra los Interesados de una Marca (Interesados Multiples)
        /// </summary>
        public void IrGestionarInteresadosDeMarca()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._interesadoMultiple != null)
                    this.Navegar(new GestionarInteresadosDeMarca(this._marca, this._interesadoMultiple, this._ventana, this._ventanaPadre, this._ventanaConsultarMarcas));
                else
                    this.Navegar(new GestionarInteresadosDeMarca(this._marca, null, this._ventana, this._ventanaPadre, this._ventanaConsultarMarcas));


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ":" + ex.Message, true);
            }
        }
    }
}
