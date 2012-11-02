using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Poderes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;
using Trascend.Bolet.Cliente.Ayuda;
using System.Linq;
using Trascend.Bolet.Cliente.Ventanas.Poderes;

namespace Trascend.Bolet.Cliente.Presentadores.Poderes
{
    class PresentadorConsultarPoder : PresentadorBase
    {
        private IConsultarPoder _ventana;
        private IBoletinServicios _boletinServicios;
        private IPoderServicios _poderServicios;
        private IInteresadoServicios _interesadoServicios;
        private IAgenteServicios _agentesServicios;
        private IList<Boletin> _boletines;
        private IList<Interesado> _interesados;
        private bool _conInteresado;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IList<Agente> _agentes;
        private IList<Agente> _apoderados = new List<Agente>();
        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorConsultarPoder(IConsultarPoder ventana, object poder,object ventanaPadre)
        {
            try
            {
                this._ventana = ventana;
                this._conInteresado = true;
                this._ventana.Poder = poder;
                this._ventanaPadre = ventanaPadre;
                this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._agentesServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        /// <summary>
        /// Constructor que posee lista de boletines e interesados
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="poder">Poder a mostrar</param>
        /// <param name="boletines">Lista de boletines</param>
        /// <param name="interesados">Lista de interesados</param>
        public PresentadorConsultarPoder(IConsultarPoder ventana, object poder, object boletines, object interesados)
        {
            try
            {
                this._ventana = ventana;
                this._conInteresado = false;
                this._ventana.Poder = poder;
                this._boletines = ((IList<Boletin>)boletines).ToList<Boletin>();
                this._interesados = ((IList<Interesado>)interesados).ToList<Interesado>();


                this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._agentesServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarPoder,
                    Recursos.Ids.ConsultarPoder);

                Poder poder = (Poder)this._ventana.Poder;

                //if (this._conInteresado)
                //{
                //    this._boletines = this._boletinServicios.ConsultarTodos();
                //    //this._interesados = this._interesadoServicios.ConsultarTodos();
                //}
                //else
                //{
                //    this._boletines.RemoveAt(0);
                //    this._interesados.RemoveAt(0);
                //}

                _apoderados = poder.Agentes;

                this._ventana.Apoderados = null;
                this._ventana.Apoderados = _apoderados;

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                this._ventana.Boletines = boletines;
                this._ventana.Boletin = this.BuscarBoletin((IList<Boletin>)this._ventana.Boletines, poder.Boletin);

                _agentes = this._agentesServicios.ConsultarTodos();
                Agente primerAgente = new Agente("NGN");
                _agentes.Insert(0, primerAgente);
                _agentes = LimpiarListaDeAgentes();
                this._ventana.Agentes = _agentes;

                //this._ventana.Apoderados = new List<Agente>();

                IList<Interesado> interesados = new List<Interesado>();
                interesados.Add(poder.Interesado);
                this._ventana.NombreInteresado = poder.Interesado.Nombre;
                this._ventana.Interesados = interesados;
                this._ventana.Interesado = poder.Interesado;
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

        private IList<Agente> LimpiarListaDeAgentes()
        {

            IList<Agente> agentesADevolver = this._agentesServicios.ConsultarTodos();

            IList<int> posicionesABorrar = new List<int>();
            foreach (Agente apoderado in _apoderados)
            {
                int i = 0;
                foreach (Agente agente in agentesADevolver)
                {
                    if (apoderado.Id == agente.Id)
                    {
                        posicionesABorrar.Insert(0, i);
                        //agentesADevolver.Remove(agente);
                    }
                    i++;
                }
            }

            foreach (int posicion in posicionesABorrar)
            {
                agentesADevolver.RemoveAt(posicion);
            }
            return agentesADevolver;
        }

        /// <summary>
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// modifica los datos del usuario
        /// </summary>
        public void Modificar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Modifica los datos del Poder
                else
                {
                    if ((this._ventana.ConfirmarModificacion(string.Format(Recursos.MensajesConElUsuario.ConfirmarModificarPoder, ((Poder)this._ventana.Poder).Id))))
                    {
                        Poder poder = (Poder)this._ventana.Poder;

                        poder.Boletin = (Boletin)this._ventana.Boletin;
                        poder.Interesado = (Interesado)this._ventana.Interesado;
                        poder.Operacion = "MODIFY";

                        poder.Agentes = _apoderados;
                        bool exitoso = this._poderServicios.InsertarOModificar(poder, UsuarioLogeado.Hash);
                        if (exitoso)
                        {
                            //_paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.PoderModificado;
                            //this.Navegar(_paginaPrincipal);
                            this._ventana.HabilitarCampos = false;
                            this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                        }
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

        public void OrdenarColumna(GridViewColumnHeader column, ListView ListaResultados)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
        /// <summary>
        /// Método que elimina un poder de la base de datos
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                Poder poder = (Poder)this._ventana.Poder;

                poder.Boletin = (Boletin)this._ventana.Boletin;
                poder.Operacion = "DELETE";

                bool exitoso = this._poderServicios.Eliminar(poder, UsuarioLogeado.Hash);
                if (exitoso)
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.PoderEliminado;
                    this.Navegar(_paginaPrincipal);
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
        /// Método que llama a la pantalla de auditoría
        /// </summary>
        public void Auditoria()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Auditoria auditoria = new Auditoria();
                auditoria.Fk = ((Poder)this._ventana.Poder).Id;
                auditoria.Tabla = "MYP_PODERES";

                IList<Auditoria> auditorias = this._poderServicios.AuditoriaPorFkyTabla(auditoria);
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

        /// <summary>
        /// Método que exporta el poder en un pdf
        /// </summary>
        public void AbrirPoder()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string rutaArchivo = ConfigurationManager.AppSettings["rutaPoderes"].ToString() + ((Poder)this._ventana.Poder).Id + ".pdf";
                System.Diagnostics.Process.Start(rutaArchivo);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Win32Exception ex)
            {
                logger.Error(ex.Message);
                this._ventana.ArchivoNoEncontrado();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
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
        /// Método que se encarga de cambiar el interesado seleccionado en la pantalla
        /// </summary>
        public void cambiarInteresado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.NombreInteresado = ((Interesado)this._ventana.Interesado).Nombre;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        public void EliminarAgente()
        {
            if (((Agente)this._ventana.Apoderado != null) && !((Agente)this._ventana.Apoderado).Id.Equals("NGN"))
            {
                _agentes.Add((Agente)this._ventana.Apoderado);
                this._ventana.Agentes = null;
                this._ventana.Agentes = _agentes;

                _apoderados = ((IList<Agente>)this._ventana.Apoderados);
                _apoderados.Remove((Agente)this._ventana.Apoderado);
                this._ventana.Apoderados = null;
                this._ventana.Apoderados = _apoderados;

            }
        }

        public void AgregarAgente()
        {
            if (!((Agente)this._ventana.Agente).Id.Equals("NGN"))
            {
                _apoderados = ((IList<Agente>)this._ventana.Apoderados);
                _apoderados.Insert(0, (Agente)this._ventana.Agente);
                this._ventana.Apoderados = null;
                this._ventana.Apoderados = _apoderados;

                //_agentes = ((IList<Agente>)this._ventana.Agentes);
                _agentes.Remove((Agente)this._ventana.Agente);
                this._ventana.Agentes = null;
                this._ventana.Agentes = _agentes;
            }
        }

        public void ConsultarInteresadoFiltro()
        {
            if ((!this._ventana.NombreInteresadoConsultar.Equals("")) || (!this._ventana.IdInteresadoConsultar.Equals("")))
            {
                Interesado interesadoAFiltrar = new Interesado();
                interesadoAFiltrar.Id = !this._ventana.IdInteresadoConsultar.Equals("") ? int.Parse(this._ventana.IdInteresadoConsultar) : 0;
                interesadoAFiltrar.Nombre = !this._ventana.NombreInteresadoConsultar.Equals("") ? this._ventana.NombreInteresadoConsultar : "";


                this._ventana.Interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoAFiltrar);
            }
        }

        public void CambiarInteresado()
        {
            this._ventana.NombreInteresado = ((Interesado)this._ventana.Interesado).Nombre;
        }
    }
}
