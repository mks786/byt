using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Justificaciones;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Ventanas.Asociados;

namespace Trascend.Bolet.Cliente.Presentadores.Justificaciones
{
    class PresentadorAgregarJustificacion : PresentadorBase
    {
        private IAgregarJustificacion _ventana;
        private IConceptoServicios _conceptoServicios;
        private IJustificacionServicios _justificacionServicios;
        private Asociado _asociado;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarJustificacion(IAgregarJustificacion ventana, object asociado)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Justificacion = new Justificacion();
                this._asociado = (Asociado)asociado;
                this._conceptoServicios = (IConceptoServicios)Activator.GetObject(typeof(IConceptoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ConceptoServicios"]);
                this._justificacionServicios = (IJustificacionServicios)Activator.GetObject(typeof(IJustificacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["JustificacionServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
            }
        }

        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarJustificacion,
                Recursos.Ids.AgregarJustificacion);
                this._ventana.FocoPredeterminado();


                IList<Concepto> conceptos = this._conceptoServicios.ConsultarTodos();
                Concepto primerConcepto = new Concepto();
                primerConcepto.Id = "NGN";
                conceptos.Insert(0, primerConcepto);
                this._ventana.Conceptos = conceptos;
                this._ventana.BorrarId();

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
        /// Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                Justificacion justificacion = (Justificacion)this._ventana.Justificacion;
                justificacion.Concepto = !((Concepto)this._ventana.Concepto).Id.Equals("NGN") ? (Concepto)this._ventana.Concepto : null;
                justificacion.Asociado = (Asociado)this._asociado;

                bool exitoso = this._justificacionServicios.InsertarOModificar(justificacion, UsuarioLogeado.Hash);

                if (exitoso)
                    this.Navegar(new ListaJustificaciones(this._asociado));
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
        }


    }
}
