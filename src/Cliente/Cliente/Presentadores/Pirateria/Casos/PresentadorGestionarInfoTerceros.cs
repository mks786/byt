using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Pirateria.Casos;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Pirateria.Casos
{
    class PresentadorGestionarInfoTerceros : PresentadorBase
    {
        private IGestionarInfoTerceros _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ICasoServicios _casoServicios;
        private Caso _caso;

        /// <summary>
        /// Presentador predeterminado
        /// </summary>
        /// <param name="ventana">Ventana Actual</param>
        /// <param name="caso">Caso actual</param>
        /// <param name="ventanaPadre">Ventana que antecede a esta ventana</param>
        public PresentadorGestionarInfoTerceros(IGestionarInfoTerceros ventana, object caso, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                if (ventanaPadre != null)
                    this._ventanaPadre = ventanaPadre;
                this._caso = (Caso)caso;
                this._ventana.Caso = this._caso;
                
                
                this._casoServicios = (ICasoServicios)Activator.GetObject(typeof(ICasoServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CasoServicios"]);
                

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleInfoTercerosCaso,
                Recursos.Ids.CasosPirateria);
        }


        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.ActualizarTitulo();
                
                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }

        }


        /// <summary>
        /// Metodo que actualiza el Caso actual con la informacion de Terceros de Interesado y Asociado
        /// </summary>
        /// <returns>True si la operacion fue realizada correctamente;  False, en caso contrario</returns>
        public bool Aceptar()
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Caso caso = (Caso)this._ventana.Caso;

                caso.Operacion = "MODIFY";

                exitoso = this._casoServicios.InsertarOModificar(caso, UsuarioLogeado.Hash);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }

            return exitoso;
        }
    }
}
