using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Ventanas.DatosTransferencias;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.EmailsAsociado;
using Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional;

namespace Trascend.Bolet.Cliente.Presentadores.Asociados
{
    class PresentadorListaDatosTransferencia : PresentadorBase
    {

        private IListaDatosTransferencia _ventana;
        private Asociado _asociado;
        private IList<FacAsociadoIntConsolidadoCxPInt> _datosConsolidadosTodos;
        private bool _soloVerConsolidado;

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private bool _consolida;
        private object _ventanaFacAprobadas;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaDatosTransferencia(IListaDatosTransferencia ventana, object asociado)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._asociado = (Asociado)asociado;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Constructor predeterminado que recibe una ventana padre
        /// </summary>
        /// <param name="ventana"></param>
        /// <param name="asociado"></param>
        /// <param name="ventanaPadre"></param>
        public PresentadorListaDatosTransferencia(IListaDatosTransferencia ventana, object asociado, object ventanaPadre)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._ventanaPadre = ventanaPadre;
            this._asociado = (Asociado)asociado;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Constructor predeterminado que acepta una ventana padre y un status de consolidacion
        /// </summary>
        /// <param name="ventana"></param>
        /// <param name="asociado"></param>
        /// <param name="ventanaPadre"></param>
        /// <param name="consolida"></param>
        public PresentadorListaDatosTransferencia(IListaDatosTransferencia ventana, 
                                                  object asociado, 
                                                  object datosConsolidados, 
                                                  object ventanaPadre, 
                                                  bool consolida, 
                                                  bool soloVerConsolidado,
                                                  object ventanaFacAprobadas)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._ventanaPadre = ventanaPadre;
            this._ventanaFacAprobadas = ventanaFacAprobadas;
            this._consolida = consolida;
            this._soloVerConsolidado = soloVerConsolidado;
            this._datosConsolidadosTodos = (IList<FacAsociadoIntConsolidadoCxPInt>)datosConsolidados;
            this._asociado = (Asociado)asociado;

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaDatosTransferencia,
                    Recursos.Ids.ConsultarDatosTransferencia);

                this._ventana.DatosTransferencias = this._asociado.DatosTransferencias;
                this._ventana.TotalHits = this._asociado.DatosTransferencias.Count.ToString();

                if (this._consolida)
                {
                    this._ventana.PresentarBotonSeleccionarDatos();
                }

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
        /// Método que invoca una nueva página "ConsultarDatosTransferencia" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarDatosTransferencia()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion


            ((DatosTransferencia)this._ventana.DatosTransferenciaSeleccionada).Asociado = this._asociado;

            this.Navegar(new ConsultarDatosTransferencia(this._ventana.DatosTransferenciaSeleccionada));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que invova una nueva página "AgregarDatosTransferencia" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrAgregarDatosTransferencia()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //this.Navegar(new AgregarDatosTransferencia(this._asociado));
            this.Navegar(new AgregarDatosTransferencia(this._asociado, this._ventana, this._ventanaPadre));

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
        /// Metodo que sirve para seleccionar los datos de transferencia para el proceso de consolidacion
        /// </summary>
        public void SeleccionarDatosTransferenciaConsolidacion()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                DatosTransferencia datoSeleccionado = (DatosTransferencia)this._ventana.DatosTransferenciaSeleccionada;

                Asociado asociado = datoSeleccionado.Asociado;

                foreach (FacAsociadoIntConsolidadoCxPInt item in _datosConsolidadosTodos)
                {
                    if (item.AsociadoInt.Id == asociado.Id)
                    {
                        item.DatosTransferencia = datoSeleccionado;
                        item.DatosBancariosStr = datoSeleccionado.BancoBenef + Environment.NewLine + datoSeleccionado.Direccion +
                                    Environment.NewLine + datoSeleccionado.Cuenta + Environment.NewLine + datoSeleccionado.Aba +
                                    Environment.NewLine + datoSeleccionado.Swif;
                        item.FormaPago = "Transferencia";
                        item.NumeroSecuenciaTransferencia = datoSeleccionado.Id;
                        break;
                    }
                    else
                        continue;

                }

                this.Navegar(new FacInternacionalConsolidadas(_datosConsolidadosTodos, this._soloVerConsolidado, true, true,this._ventanaFacAprobadas));

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
