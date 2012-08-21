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
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Ventanas.Interesados;
using Trascend.Bolet.Cliente.Ventanas.Poderes;

namespace Trascend.Bolet.Cliente.Presentadores.Poderes
{
    class PresentadorAgregarPoder : PresentadorBase
    {
        private IAgregarPoder _ventana;
        private IBoletinServicios _boletinServicios;
        private IPoderServicios _poderServicios;
        private IInteresadoServicios _interesadoServicios;
        private IAgenteServicios _agentesServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IList<Agente> _agentes;
        private IList<Agente> _apoderados = new List<Agente>();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarPoder(IAgregarPoder ventana)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Poder = new Poder();
                this._ventana.ConInteresado = false;
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
            }
        }


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarPoder(IAgregarPoder ventana, object interesado)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Poder = new Poder();
                ((Poder)this._ventana.Poder).Interesado = (Interesado) interesado;
                this._ventana.ConInteresado = true;
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarPoder,
                    Recursos.Ids.AgregarUsuario);
                this._ventana.FocoPredeterminado();

                Poder poder = (Poder)this._ventana.Poder;

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                this._ventana.Boletines = boletines;

                _agentes = this._agentesServicios.ConsultarTodos();
                Agente primerAgente = new Agente("NGN");
                _agentes.Insert(0, primerAgente);
                this._ventana.Agentes = _agentes;

                this._ventana.Apoderados = new List<Agente>();

                if (this._ventana.ConInteresado)
                {
                    //this._ventana.Interesado = this.BuscarInteresado(interesados, poder.Interesado);
                    //this._ventana.InteresadoEsEditable = false;
                    //this._ventana.TextoBotonCancelar = Recursos.Etiquetas.btnRegresar;
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

                Poder poder = (Poder)this._ventana.Poder;

                poder.Boletin = (Boletin)this._ventana.Boletin;
                poder.Interesado = (Interesado)this._ventana.Interesado;
                poder.Operacion = "CREATE";

                poder.Agentes = this._apoderados;

                int? exitoso = this._poderServicios.InsertarOModificarPoder(poder,UsuarioLogeado.Hash);

                if (exitoso != null)
                    if (this._ventana.ConInteresado)
                        this.Navegar(new ConsultarInteresado(poder.Interesado, null));
                    else
                    {
                        poder.Id = exitoso.Value;
                        this.Navegar(new ConsultarPoder(poder));
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
