﻿using System;
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
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.Cartas
{
    class PresentadorConsultarCarta : PresentadorBase
    {
        private IConsultarCarta _ventana;
        private ICartaServicios _cartaServicios;
        private IResumenServicios _resumenServicios;
        private IMedioServicios _medioServicios;
        private IAsociadoServicios _asociadoServicios;
        private IUsuarioServicios _usuarioServicios;
        private IAnexoServicios _anexoServicios;
        private IContactoServicios _contactoServicios;
        private IDepartamentoServicios _departamentoServicios;
        private IAsignacionServicios _asignacionServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IList<Asociado> _asociados;
        private IList<Usuario> _receptores;
        private IList<Medio> _medios;
        private IList<Anexo> _anexos;
        private IList<Anexo> _anexosConfirmacion;
        private IList<Contacto> _personas;
        private IList<Resumen> _resumenes;
        private IList<Departamento> _departamentos;
        private IList<Usuario> _responsables;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorConsultarCarta(IConsultarCarta ventana, object carta)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Carta = carta;
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
                this._asignacionServicios = (IAsignacionServicios)Activator.GetObject(typeof(IAsignacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsignacionServicios"]);
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
            Carta carta = (Carta)this._ventana.Carta;

            try
            {
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarCarta,
                    Recursos.Ids.AgregarCarta);

                this._medios = this._medioServicios.ConsultarTodos();
                this._ventana.Medios = this._medios;
                Medio medio = new Medio();
                medio.Id = carta.Medio;
                this._ventana.Medio = this.BuscarMedio(this._medios, medio);

                IList<Medio> mediosTracking = this._medioServicios.ConsultarTodos();
                Medio primerosMediosTracking = new Medio();
                primerosMediosTracking.Id = "NGN";
                mediosTracking.Insert(0, primerosMediosTracking);
                this._ventana.MediosTrackingConfirmacion = mediosTracking;
                Medio medioConfirmacion = new Medio();
                medioConfirmacion.Id = carta.AnexoMedio;
                this._ventana.MedioTrackingConfirmacion = this.BuscarMedio(mediosTracking, medioConfirmacion); ;

                this._receptores = this._usuarioServicios.ConsultarTodos();
                this._ventana.Receptores = this._receptores;
                this._ventana.Receptor = this.BuscarReceptor(this._receptores, carta.Receptor);

                this._anexos = this._anexoServicios.ConsultarTodos();
                Anexo primerAnexo = new Anexo();
                primerAnexo.Id = "NGN";
                this._anexos.Insert(0, primerAnexo);
                this._ventana.Anexos = _anexos;
                this._anexosConfirmacion = this._anexoServicios.ConsultarTodos();
                this._anexosConfirmacion.Insert(0, primerAnexo);
                this._ventana.AnexosConfirmacion = _anexosConfirmacion;

                this._asociados = this._asociadoServicios.ConsultarTodos();
                this._ventana.Asociados = this._asociados;
                if (null != carta.Asociado)
                {
                    this._ventana.Asociado = this.BuscarAsociado(this._asociados, carta.Asociado);
                    Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.Asociado);
                    asociado.Contactos = this._contactoServicios.ConsultarContactosPorAsociado(asociado);
                    this._personas = asociado.Contactos;
                    this._ventana.Personas = this._personas;
                    this._ventana.Persona = BuscarContacto(this._personas, carta.Persona);
                    this._ventana.NombreAsociado = ((Carta)this._ventana.Carta).Asociado.Nombre;
                }

                _departamentos = this._departamentoServicios.ConsultarTodos();
                Departamento primeraTarifa = new Departamento();
                primeraTarifa.Id = "NGN";
                _departamentos.Insert(0, primeraTarifa);
                this._ventana.Departamentos = _departamentos;
                if (null != carta.Departamento)
                    this._ventana.Departamento = this.BuscarDepartamento(this._departamentos, carta.Departamento);

                this._responsables = this._usuarioServicios.ConsultarTodos();
                Usuario primerResponsable = new Usuario();
                primerResponsable.Id = "NGN";
                _responsables.Insert(0, primerResponsable);
                this._ventana.Responsables = _responsables;

                this._ventana.MediosTrackingConfirmacion = mediosTracking;

                this._resumenes = this._resumenServicios.ConsultarTodos();
                Resumen primeraResumen = new Resumen();
                primeraResumen.Id = "NGN";
                _resumenes.Insert(0, primeraResumen);
                this._ventana.Resumenes = this._resumenes;
                if (null != carta.Resumen)
                    this._ventana.Resumen = this.BuscarResumen(this._resumenes, carta.Resumen.Id);

                ((Carta)this._ventana.Carta).Asignaciones = this._asignacionServicios.ObtenerAsignacionesPorCarta((Carta)this._ventana.Carta);

                this._ventana.FocoPredeterminado();
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

        public bool CargarAnexosCarta()
        {
            bool retorno = false;
            Carta carta = (Carta)this._ventana.Carta;
            if ((null != carta.Anexos) && (carta.Anexos.Count != 0))
            {
                this._ventana.AnexosCarta = carta.Anexos;
                retorno = true;
                this.LimpiarAnexosCarta(carta);
            }
            return retorno;
        }

        private void LimpiarAnexosCarta(Carta carta)
        {
            IList<int> indices = new List<int>();
            foreach (Anexo anexos in carta.Anexos)
            {
                int index = 0;
                foreach (Anexo anexosTotal in this._anexos)
                {
                    if (anexos.Id == anexosTotal.Id)
                    {
                        indices.Insert(0, index);
                    }
                    index++;
                }
            }

            foreach (int indice in indices)
            {
                this._anexos.RemoveAt(indice);
            }

            this._ventana.Anexos = this._anexos;
        }


        public bool CargarAnexosCartaConfirmacion()
        {
            bool retorno = false;
            Carta carta = (Carta)this._ventana.Carta;
            if ((null != carta.AnexosConfirmacion) && (carta.AnexosConfirmacion.Count != 0))
            {
                this._ventana.AnexosConfirmacionCarta = carta.AnexosConfirmacion;
                retorno = true;
                this.LimpiarAnexosCartaConfirmacion(carta);
            }
            return retorno;
        }

        private void LimpiarAnexosCartaConfirmacion(Carta carta)
        {
            IList<int> indices = new List<int>();
            foreach (Anexo anexos in carta.AnexosConfirmacion)
            {
                int index = 0;
                foreach (Anexo anexosTotal in this._anexosConfirmacion)
                {
                    if (anexos.Id == anexosTotal.Id)
                    {
                        indices.Insert(0, index);
                    }
                    index++;
                }
            }

            foreach (int indice in indices)
            {
                this._anexosConfirmacion.RemoveAt(indice);
            }

            this._ventana.AnexosConfirmacion = this._anexosConfirmacion;
        }

        //public bool CargarResponsables()    
        //{
        //    bool retorno = false;
        //    Carta carta = (Carta)this._ventana.Carta;
        //    if ((null != carta.Asignaciones) && (carta.Asignaciones.Count != 0))
        //    {
        //        this._ventana.ResponsablesList = carta.Asignaciones;
        //        retorno = true;
        //        this.LimpiarResponsables(carta);
        //    }
        //    return retorno;
        //}

        //private void LimpiarResponsables(Carta carta)
        //{
        //    IList<int> indices = new List<int>();
        //    foreach (Usuario responsables in carta.Asignaciones)
        //    {
        //        int index = 0;
        //        foreach (Usuario responsablesTotal in this._responsables)
        //        {
        //            if (responsables.Id == responsablesTotal.Id)
        //            {
        //                indices.Insert(0, index);
        //            }
        //            index++;
        //        }
        //    }

        //    foreach (int indice in indices)
        //    {
        //        this._responsables.RemoveAt(indice);
        //    }

        //    this._ventana.Responsables = this._responsables;
        //}

        //public bool verificarFormato()
        //{
        //    bool trackingCorrecto = true;
        //    if (((Medio)this._ventana.MedioTracking).Formato.Length == ((Carta)this._ventana.Carta).Tracking.Length)
        //    {
        //        for (int i = 0; i < ((Medio)this._ventana.MedioTracking).Formato.Length; i++)
        //        {
        //            if (((Medio)this._ventana.MedioTracking).Formato[i] == '9')
        //            {
        //                if (!Char.IsNumber(((Carta)this._ventana.Carta).Tracking[i]))
        //                    trackingCorrecto = false;
        //            }

        //            if (((Medio)this._ventana.MedioTracking).Formato[i] == '-')
        //            {
        //                if (((Carta)this._ventana.Carta).Tracking[i] != '-')
        //                    trackingCorrecto = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        trackingCorrecto = false;
        //    }

        //    return trackingCorrecto;
        //}

        /// <summary>
        /// Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        /// </summary>
        public void Modificar()
        {
            if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
            {
                this._ventana.HabilitarCampos = true;
                this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
            }
            else
            {
                try
                {
                    bool tracking = true;

                    if (null != (((Medio)this._ventana.Medio).Formato) &&
                       (!String.IsNullOrEmpty(((Carta)this._ventana.Carta).Tracking)))

                        tracking = this.verificarFormato(((Medio)this._ventana.Medio).Formato, ((Carta)this._ventana.Carta).Tracking);

                    if (null != (((Medio)this._ventana.MedioTrackingConfirmacion)) &&
                       (null != (((Medio)this._ventana.MedioTrackingConfirmacion).Formato)) &&
                       (!String.IsNullOrEmpty(((Carta)this._ventana.Carta).AnexoTracking)))

                        tracking = this.verificarFormato(((Medio)this._ventana.MedioTrackingConfirmacion).Formato, ((Carta)this._ventana.Carta).AnexoTracking);

                    if (tracking)
                    {
                        Carta carta = (Carta)this._ventana.Carta;
                        carta.Operacion = "MODIFY";
                        if (null != this._ventana.Departamento)
                            carta.Departamento = !((Departamento)this._ventana.Departamento).Id.Equals("NGN") ? (Departamento)this._ventana.Departamento : null;
                        if (null != this._ventana.Asociado)
                            carta.Asociado = !((Asociado)this._ventana.Asociado).Id.Equals("NGN") ? (Asociado)this._ventana.Asociado : null;
                        if (null != this._ventana.Persona)
                            carta.Persona = !((Contacto)this._ventana.Persona).Id.Equals("NGN") ? ((Contacto)this._ventana.Persona).Nombre : null;
                        if (null != this._ventana.Resumen)
                            carta.Resumen = !((Resumen)this._ventana.Resumen).Id.Equals("NGN") ? ((Resumen)this._ventana.Resumen) : null;

                        carta.Medio = ((Medio)this._ventana.Medio).Id;
                        carta.AnexoMedio = ((Medio)this._ventana.MedioTrackingConfirmacion).Id;
                        carta.Receptor = ((Usuario)this._ventana.Receptor).Iniciales;
                        bool exitoso = this._cartaServicios.InsertarOModificar(carta, UsuarioLogeado.Hash);

                        if (exitoso)
                            this.Navegar(Recursos.MensajesConElUsuario.CartaModificada, false);
                    }

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
                //try
                //{
                //    bool tracking = true;

                //    if (!String.IsNullOrEmpty(((Carta)this._ventana.Carta).Tracking))
                //        tracking = this.verificarFormato(((Medio)this._ventana.Medio).Formato, ((Carta)this._ventana.Carta).Tracking);
                //    if (tracking)
                //    {
                //        Carta carta = (Carta)this._ventana.Carta;
                //        if (null != this._ventana.Departamento)
                //            carta.Departamento = !((Departamento)this._ventana.Departamento).Id.Equals("NGN") ? (Departamento)this._ventana.Departamento : null;
                //        if (null != this._ventana.Asociado)
                //            carta.Asociado = !((Asociado)this._ventana.Asociado).Id.Equals("NGN") ? (Asociado)this._ventana.Asociado : null;
                //        if (null != this._ventana.Persona)
                //            carta.Persona = !((Contacto)this._ventana.Persona).Id.Equals("NGN") ? ((Contacto)this._ventana.Persona).Nombre : null;




                //        carta.Medio = ((Medio)this._ventana.Medio).Id;
                //        carta.Receptor = ((Usuario)this._ventana.Receptor).Iniciales;

                //        carta.Medio = ((Medio)this._ventana.Medio).Id;
                //        carta.Receptor = ((Usuario)this._ventana.Receptor).Iniciales;

                //        //if (!this._cartaServicios.VerificarExistencia(carta))
                //        //{
                //        //    bool exitoso = this._cartaServicios.InsertarOModificar(carta, UsuarioLogeado.Hash);

                //        //    if (exitoso)
                //        //        this.Navegar(Recursos.MensajesConElUsuario.EntradaAlternaInsertado, false);
                //        //}
                //        //else
                //        //{
                //        //    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorAgenteRepetido);
                //        //}
                //    }

                //}
                //catch (ApplicationException ex)
                //{
                //    logger.Error(ex.Message);
                //    this.Navegar(ex.Message, true);
                //}
                //catch (RemotingException ex)
                //{
                //    logger.Error(ex.Message);
                //    this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
                //}
                //catch (SocketException ex)
                //{
                //    logger.Error(ex.Message);
                //    this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
                //}
                //catch (Exception ex)
                //{
                //    logger.Error(ex.Message);
                //    this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
                //}
            }
        }

        public void CambiarAsociado()
        {
            try
            {
                Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.Asociado);
                asociado.Contactos = this._contactoServicios.ConsultarContactosPorAsociado(asociado);
                this._ventana.NombreAsociado = ((Asociado)this._ventana.Asociado).Nombre;
                this._ventana.Personas = asociado.Contactos;
            }
            catch (ApplicationException e)
            {
                this._ventana.Personas = null;
            }
        }

        public void BuscarAsociado()
        {
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
        }

        public bool AgregarAnexoCarta()
        {
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
            return retorno;
        }

        public bool AgregarAnexoCartaConfirmacion()
        {
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
            return retorno;
        }

        public bool DeshabilitarAnexosCarta()
        {
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
            return respuesta;
        }

        public bool DeshabilitarAnexosCartaConfirmacion()
        {
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
            return respuesta;
        }

        public void CambiarFormatoTracking()
        {
            this._ventana.FormatoTracking = !((Medio)this._ventana.Medio).Id.Equals("NGN") ? "Formato: " + ((Medio)this._ventana.Medio).Formato : "Formato: ";
        }

        public void CambiarFormatoTrackingConfirmacion()
        {
            this._ventana.FormatoTrackingConfirmacion = !((Medio)this._ventana.MedioTrackingConfirmacion).Id.Equals("NGN") ? "Formato: " + ((Medio)this._ventana.MedioTrackingConfirmacion).Formato : "Formato: ";
        }

        public void Auditoria()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Auditoria auditoria = new Auditoria();
                auditoria.Fk = ((Carta)this._ventana.Carta).Id;
                auditoria.Tabla = "ENTRADA";

                IList<Auditoria> auditorias = this._cartaServicios.AuditoriaPorFkyTabla(auditoria);
                _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.PoderEliminado;
                this.Navegar(new ListaAuditorias(auditorias));


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


        public bool AgregarResponsable()
        {
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
            return retorno;

        }

        private IList<Usuario> ListAsignacionesToUsuarios(IList<Asignacion> asignaciones)
        {
            IList<Usuario> usuarios = new List<Usuario>();
            foreach (Asignacion asignacion in asignaciones)
            {
                usuarios.Add(asignacion.Responsable);
            }

            return usuarios;
        }

        public bool DeshabilitarResponsable()
        {
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
                this._ventana.Responsables = this._responsables.ToList<Usuario>();
            }

            if (((Carta)this._ventana.Carta).Asignaciones.Count == 0)
                retorno = true;

            return retorno;
        }

        private IList<Asignacion> RemoverResponsable(IList<Asignacion> asignaciones)
        {
            int index = 0;
            int borrar = 0;
            foreach (Asignacion asignacion in asignaciones)
            {
                if (asignacion.Responsable.Id == ((Usuario)this._ventana.ResponsableList).Id)
                    borrar = index;
                index++;
            }
            asignaciones.RemoveAt(borrar);

            return asignaciones;
        }

        public bool CargarResponsables()
        {
            bool retorno = false;
            IList<Usuario> responsables;
            if (null != ((Carta)this._ventana.Carta).Asignaciones)
            {
                responsables = ListAsignacionesToUsuarios(((Carta)this._ventana.Carta).Asignaciones);
                this._ventana.ResponsablesList = responsables.ToList<Usuario>();
                LimpiarResponsables(responsables);
                retorno = true;
            }

            return retorno;
        }

        public void LimpiarResponsables(IList<Usuario> responsables)
        {
            IList<int> indice = new List<int>();
            int index = 0;
            foreach (Usuario usuario in this._responsables)
            {
                foreach (Usuario usuarioCarta in responsables)
                {
                    if (usuario.Id == usuarioCarta.Id)
                        indice.Add(index);
                    index++;
                }
            }

            foreach (int posicion in indice)
            {
                this._responsables.RemoveAt(posicion);
            }
        }
        //public bool AgregarResponsable()
        //{
        //    IList<Usuario> usuariosLista;
        //    bool retorno = false;
        //    if ((null != (Usuario)this._ventana.Responsable) && (!((Usuario)this._ventana.Responsable).Id.Equals("NGN")))
        //    {
        //        if (null == ((Carta)this._ventana.Carta).Responsables)
        //            usuariosLista = new List<Usuario>();
        //        else
        //            usuariosLista = ((Carta)this._ventana.Carta).Responsables;

        //        usuariosLista.Add((Usuario)this._ventana.Responsable);
        //        ((Carta)this._ventana.Carta).Responsables = usuariosLista;
        //        this._ventana.ResponsablesList = usuariosLista.ToList<Usuario>();
        //        this._responsables.Remove((Usuario)this._ventana.Responsable);
        //        this._ventana.Responsables = this._responsables.ToList<Usuario>();
        //        retorno = true;
        //    }
        //    return retorno;
        //}

        //public bool DeshabilitarResponsable()
        //{
        //    IList<Usuario> responsables;
        //    bool respuesta = false;

        //    if (null != ((Usuario)this._ventana.ResponsableList))
        //    {
        //        if (null == ((Carta)this._ventana.Carta).Responsables)
        //            responsables = new List<Usuario>();
        //        else
        //            responsables = ((Carta)this._ventana.Carta).Responsables;

        //        responsables.Remove((Usuario)this._ventana.ResponsableList);
        //        ((Carta)this._ventana.Carta).Responsables = responsables;
        //        this._responsables.Add((Usuario)this._ventana.ResponsableList);
        //        this._ventana.ResponsablesList = responsables.ToList<Usuario>();
        //        this._ventana.Responsables = this._responsables.ToList<Usuario>();

        //        if (responsables.Count == 0)
        //            respuesta = true;

        //    }
        //    return respuesta;
        //}
    }
}
