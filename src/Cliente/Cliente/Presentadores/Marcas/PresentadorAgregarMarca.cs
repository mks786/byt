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
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorAgregarMarca : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IAgregarMarca _ventana;
        private IMarcaServicios _marcaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IList<Marca> _marcas;
        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorAgregarMarca(IAgregarMarca ventana)
        {
            try
            {
                this._ventana = ventana;
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarMarca,
                Recursos.Ids.AgregarMarca);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarAnexo, "");
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
        /// Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                //bool tracking = true;

                //if (null != (((Medio)this._ventana.Medio).Formato) && (!String.IsNullOrEmpty(((Carta)this._ventana.Carta).Tracking)))
                //    tracking = this.verificarFormato(((Medio)this._ventana.Medio).Formato, ((Carta)this._ventana.Carta).Tracking);
                //if (null != (((Medio)this._ventana.MedioTrackingConfirmacion)) && (null != (((Medio)this._ventana.MedioTrackingConfirmacion).Formato)) && (!String.IsNullOrEmpty(((Carta)this._ventana.Carta).AnexoTracking)))
                //    tracking = this.verificarFormato(((Medio)this._ventana.MedioTrackingConfirmacion).Formato, ((Carta)this._ventana.Carta).AnexoTracking);

                //if (tracking)
                //{
                //    Carta carta = (Carta)this._ventana.Carta;
                //    carta.Operacion = "CREATE";
                //    if (null != this._ventana.Departamento)
                //        carta.Departamento = !((Departamento)this._ventana.Departamento).Id.Equals("NGN") ? (Departamento)this._ventana.Departamento : null;
                //    if (null != this._ventana.Asociado)
                //        carta.Asociado = !((Asociado)this._ventana.Asociado).Id.Equals("NGN") ? (Asociado)this._ventana.Asociado : null;
                //    if (null != this._ventana.Persona)
                //        carta.Persona = !((Contacto)this._ventana.Persona).Id.Equals("NGN") ? ((Contacto)this._ventana.Persona).Nombre : null;
                //    if (null != this._ventana.Resumen)
                //        carta.Resumen = !((Resumen)this._ventana.Resumen).Id.Equals("NGN") ? ((Resumen)this._ventana.Resumen) : null;

                //    carta.Medio = ((Medio)this._ventana.Medio).Id;
                //    carta.AnexoMedio = ((Medio)this._ventana.MedioTrackingConfirmacion).Id;
                //    carta.Receptor = ((Usuario)this._ventana.Receptor).Iniciales;

                //    if (!this._cartaServicios.VerificarExistencia(carta))
                //    {
                //        bool exitoso = this._cartaServicios.InsertarOModificar(carta, UsuarioLogeado.Hash);

                //        if (exitoso)
                //            this.Navegar(Recursos.MensajesConElUsuario.CartaInsertada, false);
                //    }
                //    else
                //    {
                //        this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorAgenteRepetido);
                //    }
                //}

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

        #region Metodos de los filstros de asociados

        public void CambiarAsociadoSolicitud()
        {
            try
            {
                if (null != (Asociado)this._ventana.AsociadoSolicitud)
                {
                    Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.AsociadoSolicitud);
                    this._ventana.NombreAsociadoSolicitud = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
                    this._ventana.AsociadoDatos = (Asociado)this._ventana.AsociadoSolicitud;
                    this._ventana.NombreAsociadoDatos = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
                }
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreAsociadoSolicitud = "";
                this._ventana.NombreAsociadoDatos = "";
            }
        }

        public void CambiarAsociadoDatos()
        {
            try
            {
                if ((Asociado)this._ventana.AsociadoDatos != null)
                {
                    Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.AsociadoDatos);
                    this._ventana.NombreAsociadoDatos = ((Asociado)this._ventana.AsociadoDatos).Nombre;
                    this._ventana.AsociadoSolicitud = (Asociado)this._ventana.AsociadoDatos;
                    this._ventana.NombreAsociadoSolicitud = ((Asociado)this._ventana.AsociadoDatos).Nombre;
                }
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreAsociadoSolicitud = "";
                this._ventana.NombreAsociadoDatos = "";
            }
        }

        public void BuscarAsociado(int filtrarEn)
        {
            IEnumerable<Asociado> asociadosFiltrados = this._asociados;

            if (!string.IsNullOrEmpty(this._ventana.IdAsociadoSolicitudFiltrar))
            {
                asociadosFiltrados = from p in asociadosFiltrados
                                     where p.Id == int.Parse(this._ventana.IdAsociadoSolicitudFiltrar)
                                     select p;
            }

            if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoSolicitud))
            {
                asociadosFiltrados = from p in asociadosFiltrados
                                     where p.Nombre != null &&
                                     p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoSolicitud.ToLower())
                                     select p;
            }

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                    this._ventana.AsociadosSolicitud = asociadosFiltrados.ToList<Asociado>();
                else
                    this._ventana.AsociadosSolicitud = this._asociados;
            }
            else
            {
                if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                    this._ventana.AsociadosDatos = asociadosFiltrados.ToList<Asociado>();
                else
                    this._ventana.AsociadosDatos = this._asociados;
            }
        }

        #endregion

        #region Metodos de los filstros de interesados

        public void CambiarInteresadoSolicitud()
        {
            try
            {
                if ((Asociado)this._ventana.InteresadoSolicitud != null)
                {
                    this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
                    this._ventana.InteresadoDatos = (Interesado)this._ventana.InteresadoSolicitud;
                    this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
                }
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreInteresadoSolicitud = "";
                this._ventana.NombreInteresadoDatos = "";
            }
        }

        public void CambiarInteresadoDatos()
        {
            try
            {
                if ((Asociado)this._ventana.InteresadoDatos != null)
                {
                    this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoDatos).Nombre;
                    this._ventana.InteresadoSolicitud = (Interesado)this._ventana.InteresadoDatos;
                    this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoDatos).Nombre;
                }
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreInteresadoSolicitud = "";
                this._ventana.NombreInteresadoDatos = "";
            }
        }

        public void BuscarInteresado(int filtrarEn)
        {
            IEnumerable<Interesado> interesadosFiltrados = this._interesados;

            if (!string.IsNullOrEmpty(this._ventana.IdInteresadoSolicitudFiltrar))
            {
                interesadosFiltrados = from p in interesadosFiltrados
                                     where p.Id == int.Parse(this._ventana.IdInteresadoSolicitudFiltrar)
                                     select p;
            }

            if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoSolicitud))
            {
                interesadosFiltrados = from p in interesadosFiltrados
                                     where p.Nombre != null &&
                                     p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoSolicitud.ToLower())
                                     select p;
            }

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                    this._ventana.InteresadosSolicitud = interesadosFiltrados.ToList<Interesado>();
                else
                    this._ventana.InteresadosSolicitud = this._interesados;
            }
            else
            {
                if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                    this._ventana.InteresadosDatos = interesadosFiltrados.ToList<Interesado>();
                else
                    this._ventana.InteresadosDatos = this._interesados;
            }
        }

        #endregion
    }
}
