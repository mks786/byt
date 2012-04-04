using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using System.Linq;
using Trascend.Bolet.Cliente.Contratos.Cartas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Presentadores.Cartas
{
    class PresentadorAgregarCarta : PresentadorBase
    {
        private IAgregarCarta _ventana;
        private ICartaServicios _cartaServicios;
        private IResumenServicios _resumenServicios;
        private IMedioServicios _medioServicios;
        private IAsociadoServicios _asociadoServicios;
        private IUsuarioServicios _usuarioServicios;
        private IAnexoServicios _anexoServicios;
        private IContactoServicios _contactoServicios;
        private IDepartamentoServicios _departamentoServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IList<Asociado> _asociados;
        private IList<Anexo> _anexos;
        private IList<Usuario> _responsables;
        private IList<Anexo> _anexosConfirmacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarCarta(IAgregarCarta ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventana.Carta = new Carta();
                ((Carta)this._ventana.Carta).Fecha = System.DateTime.Now;
                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
                this._resumenServicios = (IResumenServicios)Activator.GetObject(typeof(IResumenServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ResumenServicios"]);
                this._medioServicios = (IMedioServicios)Activator.GetObject(typeof(IMedioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MedioServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._anexoServicios = (IAnexoServicios)Activator.GetObject(typeof(IAnexoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AnexoServicios"]);
                this._contactoServicios = (IContactoServicios)Activator.GetObject(typeof(IContactoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ContactoServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarCarta,
                    Recursos.Ids.AgregarCarta);

                this._ventana.Medios = this._medioServicios.ConsultarTodos();

                this._ventana.Receptores = this._usuarioServicios.ConsultarTodos();

                this._anexos = this._anexoServicios.ConsultarTodos();
                Anexo primerAnexo = new Anexo();
                primerAnexo.Id = "NGN";
                this._anexos.Insert(0, primerAnexo);
                this._ventana.Anexos = _anexos;
                this._anexosConfirmacion = this._anexoServicios.ConsultarTodos();
                this._anexosConfirmacion.Insert(0, primerAnexo);
                this._ventana.AnexosConfirmacion = _anexosConfirmacion;
                //this._ventana.Personas = this._ventana.Receptores;

                this._asociados = this._asociadoServicios.ConsultarTodos();
                this._ventana.Asociados = this._asociados;

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                Departamento primeraTarifa = new Departamento();
                primeraTarifa.Id = "NGN";
                departamentos.Insert(0, primeraTarifa);
                this._ventana.Departamentos = departamentos;

                IList<Medio> mediosTracking = (IList<Medio>)this._ventana.Medios;
                Medio primerosMediosTracking = new Medio();
                primerosMediosTracking.Id = "NGN";
                mediosTracking.Insert(0, primerosMediosTracking);
                this._ventana.MediosTrackingConfirmacion = mediosTracking;

                IList<Resumen> resumenes = this._resumenServicios.ConsultarTodos();
                Resumen primeraResumen = new Resumen();
                primeraResumen.Id = "NGN";
                resumenes.Insert(0, primeraResumen);
                this._ventana.Resumenes = resumenes;

                this._responsables = this._usuarioServicios.ConsultarTodos();
                Usuario primerResponsable = new Usuario();
                primerResponsable.Id = "NGN";
                _responsables.Insert(0, primerResponsable);
                this._ventana.Responsables = _responsables;

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
        /// Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool tracking = true;

                if (null != (((Medio)this._ventana.Medio).Formato) && (!String.IsNullOrEmpty(((Carta)this._ventana.Carta).Tracking)))
                    tracking = this.verificarFormato(((Medio)this._ventana.Medio).Formato, ((Carta)this._ventana.Carta).Tracking);
                if (null != (((Medio)this._ventana.MedioTrackingConfirmacion)) && (null != (((Medio)this._ventana.MedioTrackingConfirmacion).Formato)) && (!String.IsNullOrEmpty(((Carta)this._ventana.Carta).AnexoTracking)))
                    tracking = this.verificarFormato(((Medio)this._ventana.MedioTrackingConfirmacion).Formato, ((Carta)this._ventana.Carta).AnexoTracking);

                if (tracking)
                {
                    Carta carta = (Carta)this._ventana.Carta;
                    carta.Operacion = "CREATE";
                    if (null != this._ventana.Departamento)
                        carta.Departamento = !((Departamento)this._ventana.Departamento).Id.Equals("NGN") ? (Departamento)this._ventana.Departamento : null;
                    if (null != this._ventana.Asociado)
                        carta.Asociado = !((Asociado)this._ventana.Asociado).Id.Equals("NGN") ? (Asociado)this._ventana.Asociado : null;
                    if (null != this._ventana.Persona)
                        carta.Persona = !((Contacto)this._ventana.Persona).Id.Equals("NGN") ? ((Contacto)this._ventana.Persona).Nombre : null;
                    if (null != this._ventana.Resumen)
                        carta.Resumen = !((Resumen)this._ventana.Resumen).Id.Equals("NGN") ? ((Resumen)this._ventana.Resumen): null;

                    carta.Medio = ((Medio)this._ventana.Medio).Id;
                    carta.AnexoMedio = ((Medio)this._ventana.MedioTrackingConfirmacion) == null ? "" : ((Medio)this._ventana.MedioTrackingConfirmacion).Id;
                    carta.Receptor = ((Usuario)this._ventana.Receptor).Iniciales;

                    if (!this._cartaServicios.VerificarExistencia(carta))
                    {
                        bool exitoso = this._cartaServicios.InsertarOModificar(carta, UsuarioLogeado.Hash);

                        if (exitoso)
                            this.Navegar(Recursos.MensajesConElUsuario.CartaInsertada, false);
                    }
                    else
                    {
                        this._ventana.Mensaje(String.Format(Recursos.MensajesConElUsuario.ErrorCartaRepetida,carta.Id));
                    }
                }

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
        }

        /// <summary>
        /// Método que se encarga de cambiar el asociado de la ventana
        /// </summary>
        public void CambiarAsociado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.Asociado);
                asociado.Contactos = this._contactoServicios.ConsultarContactosPorAsociado(asociado);
                this._ventana.NombreAsociado = ((Asociado)this._ventana.Asociado).Nombre;
                this._ventana.Personas = asociado.Contactos;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

            }
            catch (ApplicationException e)
            {
                this._ventana.Personas = null;
            }
        }

        /// <summary>
        /// Método que se encarga de buscar un asociado con filtros
        /// </summary>
        public void BuscarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IEnumerable<Asociado> asociadosFiltrados = this._asociados;

            if (!string.IsNullOrEmpty(this._ventana.idAsociadoFiltrar))
            {
                asociadosFiltrados = from p in asociadosFiltrados
                                     where p.Id == int.Parse(this._ventana.idAsociadoFiltrar)
                                     select p;
            }

            if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoFiltrar))
            {
                asociadosFiltrados = from p in asociadosFiltrados
                                     where p.Nombre != null &&
                                     p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoFiltrar.ToLower())
                                     select p;
            }

            if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                this._ventana.Asociados = asociadosFiltrados.ToList<Asociado>();
            else
                this._ventana.Asociados = this._asociados;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que se encarga de agregar un anexo carta
        /// </summary>
        /// <returns>retorno true si se agrego el anexo</returns>
        public bool AgregarAnexoCarta()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Anexo> anexosCarta;
            bool retorno = false;
            if ((null != (Anexo)this._ventana.Anexo) && (!((Anexo)this._ventana.Anexo).Id.Equals("NGN")))
            {
                if (null == ((Carta)this._ventana.Carta).Anexos)
                    anexosCarta = new List<Anexo>();
                else
                    anexosCarta = ((Carta)this._ventana.Carta).Anexos;

                anexosCarta.Add((Anexo)this._ventana.Anexo);
                ((Carta)this._ventana.Carta).Anexos = anexosCarta;
                this._ventana.AnexosCarta = anexosCarta.ToList<Anexo>();
                this._anexos.Remove((Anexo)this._ventana.Anexo);
                this._ventana.Anexos = this._anexos.ToList<Anexo>();
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// <summary>
        /// Método que se encarga de agregar un anexo carta confirmación
        /// </summary>
        /// <returns>retorno true si se agrego</returns>
        public bool AgregarAnexoCartaConfirmacion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Anexo> anexosCarta;
            bool retorno = false;
            if ((null != (Anexo)this._ventana.AnexoConfirmacion) && (!((Anexo)this._ventana.AnexoConfirmacion).Id.Equals("NGN")))
            {
                if (null == ((Carta)this._ventana.Carta).AnexosConfirmacion)
                    anexosCarta = new List<Anexo>();
                else
                    anexosCarta = ((Carta)this._ventana.Carta).AnexosConfirmacion;

                anexosCarta.Add((Anexo)this._ventana.AnexoConfirmacion);
                ((Carta)this._ventana.Carta).AnexosConfirmacion = anexosCarta;
                this._ventana.AnexosConfirmacionCarta = anexosCarta.ToList<Anexo>();
                this._anexosConfirmacion.Remove((Anexo)this._ventana.AnexoConfirmacion);
                this._ventana.AnexosConfirmacion = this._anexosConfirmacion.ToList<Anexo>();
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// <summary>
        /// Metodo que deshabilita los anexos de carta
        /// </summary>
        /// <returns>retorno true si se deshabilitó</returns>
        public bool DeshabilitarAnexosCarta()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Anexo> anexosCarta;
            bool respuesta = false;

            if (null != ((Anexo)this._ventana.AnexoCarta))
            {
                if (null == ((Carta)this._ventana.Carta).Anexos)
                    anexosCarta = new List<Anexo>();
                else
                    anexosCarta = ((Carta)this._ventana.Carta).Anexos;

                anexosCarta.Remove((Anexo)this._ventana.AnexoCarta);
                ((Carta)this._ventana.Carta).Anexos = anexosCarta;
                this._anexos.Add((Anexo)this._ventana.AnexoCarta);
                this._ventana.AnexosCarta = anexosCarta.ToList<Anexo>();
                this._ventana.Anexos = this._anexos.ToList<Anexo>();

                if (anexosCarta.Count == 0)
                    respuesta = true;

            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return respuesta;
        }

        /// <summary>
        /// Método que deshabilita los anexos confirmación de carta
        /// </summary>
        /// <returns>retorno true si se deshabilitó</returns>
        public bool DeshabilitarAnexosCartaConfirmacion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Anexo> anexosCarta;
            bool respuesta = false;

            if (null != ((Anexo)this._ventana.AnexoConfirmacionCarta))
            {
                if (null == ((Carta)this._ventana.Carta).AnexosConfirmacion)
                    anexosCarta = new List<Anexo>();
                else
                    anexosCarta = ((Carta)this._ventana.Carta).AnexosConfirmacion;

                anexosCarta.Remove((Anexo)this._ventana.AnexoConfirmacionCarta);
                ((Carta)this._ventana.Carta).AnexosConfirmacion = anexosCarta;
                this._anexosConfirmacion.Add((Anexo)this._ventana.AnexoConfirmacionCarta);
                this._ventana.AnexosConfirmacionCarta = anexosCarta.ToList<Anexo>();
                this._ventana.AnexosConfirmacion = this._anexosConfirmacion.ToList<Anexo>();

                if (anexosCarta.Count == 0)
                    respuesta = true;

            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return respuesta;
        }

        /// <summary>
        /// Método que cambia el formato de la pantalla entre vacío a
        /// contenido
        /// </summary>
        public void CarmbiarFormatoTracking()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.FormatoTracking = !((Medio)this._ventana.Medio).Id.Equals("NGN") ? "Formato: " + ((Medio)this._ventana.Medio).Formato : "Formato: ";

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que cambia el formato confirmación de la pantalla entre vacío       
        /// a contenido
        /// </summary>
        public void CarmbiarFormatoTrackingConfirmacion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.FormatoTrackingConfirmacion = !((Medio)this._ventana.MedioTrackingConfirmacion).Id.Equals("NGN") ? "Formato: " + ((Medio)this._ventana.MedioTrackingConfirmacion).Formato : "Formato: ";

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que se encarga de agregar un responsable a la carta
        /// </summary>
        /// <returns>retorno true si se agregó</returns>
        public bool AgregarResponsable()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;
            IList<Asignacion> asignacionLista;
            if ((null != (Usuario)_ventana.Responsable) && (!((Usuario)this._ventana.Responsable).Id.Equals("NGN")))
            {
                if (null == ((Carta)this._ventana.Carta).Asignaciones)
                {
                    asignacionLista = new List<Asignacion>();
                }
                else
                {
                    asignacionLista = ((Carta)this._ventana.Carta).Asignaciones;
                }


                Asignacion asignacionInsertar = new Asignacion((Usuario)this._ventana.Responsable, (Carta)this._ventana.Carta);
                asignacionLista.Add(asignacionInsertar);
                ((Carta)this._ventana.Carta).Asignaciones = asignacionLista;
                IList<Usuario> usuariosLista = ListAsignacionesToUsuarios(asignacionLista);
                this._ventana.ResponsablesList = usuariosLista;

                this._responsables.Remove((Usuario)this._ventana.Responsable);
                this._ventana.Responsables = this._responsables.ToList<Usuario>();


                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;

        }

        /// <summary>
        /// Metodo que convierte una lista de asignaciones a usuarios
        /// </summary>
        /// <param name="asignaciones">lista de asignaciones</param>
        /// <returns>lista de usuarios</returns>
        private IList<Usuario> ListAsignacionesToUsuarios(IList<Asignacion> asignaciones)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Usuario> usuarios = new List<Usuario>();
            foreach (Asignacion asignacion in asignaciones)
            {
                usuarios.Add(asignacion.Responsable);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return usuarios;
        }

        /// <summary>
        /// Método que deshabilita un responsable de la carta
        /// </summary>
        /// <returns>retorno true si se realizó correctamente</returns>
        public bool DeshabilitarResponsable()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;
            IList<Asignacion> asignacionLista;
            if (null != (Usuario)this._ventana.ResponsableList)
            {
                if (null == ((Carta)this._ventana.Carta).Asignaciones)
                    asignacionLista = new List<Asignacion>();
                else
                    asignacionLista = ((Carta)this._ventana.Carta).Asignaciones;

                IList<Usuario> usuariosLista = ListAsignacionesToUsuarios(asignacionLista);
                this._responsables.Add((Usuario)this._ventana.ResponsableList);


                ((Carta)this._ventana.Carta).Asignaciones = RemoverResponsable(asignacionLista);
                usuariosLista.Remove((Usuario)this._ventana.ResponsableList);
                this._ventana.ResponsablesList = usuariosLista.ToList<Usuario>();
                this._ventana.Responsables = this._responsables;
            }

            if (((Carta)this._ventana.Carta).Asignaciones.Count == 0)
                retorno = true;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// <summary>
        /// Método que elimina un responsable de la carta
        /// </summary>
        /// <param name="asignaciones">lista de asignaciones</param>
        /// <returns>lista con el responsable removido</returns>
        private IList<Asignacion> RemoverResponsable(IList<Asignacion> asignaciones)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            int index = 0;
            int borrar = 0;
            foreach (Asignacion asignacion in asignaciones)
            {
                if (asignacion.Responsable.Id == ((Usuario)this._ventana.ResponsableList).Id)
                    borrar = index;
                index++;
            }
            asignaciones.RemoveAt(borrar);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return asignaciones;

        }

    }
}
