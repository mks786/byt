using System;
using System.Configuration;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Principales;
using Trascend.Bolet.Cliente.Ventanas.Principales;

namespace Trascend.Bolet.Cliente.Presentadores.Principales
{
    class PresentadorPaginaPrincipal : PresentadorBase
    {
        private IPaginaPrincipal _ventana;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public PresentadorPaginaPrincipal(IPaginaPrincipal ventana)
        {
            this._ventana = ventana;
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titlePaginaPrincipal,
                    string.Empty);
                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                this._ventana.MensajeError = ex.Message;
                logger.Error(ex.Message);
                //this.Navegar(this._ventana);
            }
            catch (Exception ex)
            {
                this._ventana.MensajeError = Recursos.MensajesConElUsuario.ErrorInesperado;
                logger.Error(ex.Message);
                //this.Navegar(this._paginaPrincipal);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }
    }
}
