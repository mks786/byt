using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.EmailsAsociado;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.EmailsAsociado
{
    class PresentadorConsultarEmailAsociado : PresentadorBase
    {

        private IConsultarEmailAsociado _ventana;
        private IEmailAsociadoServicios _emailAsociadoServicios;
        private IAsociadoServicios _asociadoServicios;
        private IDepartamentoServicios _departamentoServicios;
        private ITipoEmailAsociadoServicios _tipoEmailServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private Asociado _asociado;
        private IList<Auditoria> _auditorias;
        private string _tipoEmailInicial;


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorConsultarEmailAsociado(IConsultarEmailAsociado ventana, object email, object asociado, object ventanaPadre)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.EmailAsociado = email;
                this._asociado = (Asociado)asociado;
                this._ventanaPadre = ventanaPadre;

                this._emailAsociadoServicios = (IEmailAsociadoServicios)Activator.GetObject(typeof(IEmailAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["EmailAsociadoServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
                this._tipoEmailServicios = (ITipoEmailAsociadoServicios)Activator.GetObject(typeof(ITipoEmailAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoEmailAsociadoServicios"]);
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
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;


                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarEmailAsociado,
                Recursos.Ids.AgregarJustificacion);


                IList<TipoEmailAsociado> emails = this._tipoEmailServicios.ConsultarTodos();

                //emails = FiltrarEmailsPorDepartamento(UsuarioLogeado.Departamento, emails);
                this._ventana.TiposEmail = emails;


                this._ventana.TipoEmail = BuscarTipoEmail(emails, ((EmailAsociado)this._ventana.EmailAsociado).TipoEmailAsociado);
                _tipoEmailInicial = ((TipoEmailAsociado)this._ventana.TipoEmail).Id;


                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                Departamento primerDepartamento = new Departamento();
                primerDepartamento.Id = "NGN";
                departamentos.Insert(0, primerDepartamento);
                this._ventana.Departamentos = departamentos;
                this._ventana.Funcion = ((EmailAsociado)this._ventana.EmailAsociado).TipoEmailAsociado.Funcion;
                this._ventana.Descripcion = ((EmailAsociado)this._ventana.EmailAsociado).TipoEmailAsociado.Descripcion;


                this._ventana.Departamento = this.BuscarDepartamento(departamentos, ((EmailAsociado)this._ventana.EmailAsociado).TipoEmailAsociado.Departamento);


                if (UsuarioLogeado.Departamento.Id.Equals(((EmailAsociado)this._ventana.EmailAsociado).TipoEmailAsociado.Departamento.Id))
                    this._ventana.MostrarBotones();


                Auditoria auditoria = new Auditoria();
                auditoria.Fk = ((EmailAsociado)this._ventana.EmailAsociado).Id;
                auditoria.Tabla = "FAC_ASOCIADO_EMAILS";
                _auditorias = this._emailAsociadoServicios.AuditoriaPorFkyTabla(auditoria);


                if (_auditorias.Count > 0)
                    this._ventana.PintarAuditoria();

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
        /// Método que realiza toda la lógica para agregar los EmailAsociado dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool correcto;

                EmailAsociado email = (EmailAsociado)this._ventana.EmailAsociado;
                email.TipoEmailAsociado = (TipoEmailAsociado)this._ventana.TipoEmail;
                email.Operacion = "UPDATE";

                if (_tipoEmailInicial.Equals(email.TipoEmailAsociado.Id))
                    correcto = true;
                else
                    correcto = !this.VerificarExistenciaEmailAsociado(_tipoEmailInicial, _asociado);

                if (correcto)
                {
                    bool exitoso = this._emailAsociadoServicios.InsertarOModificar(email, UsuarioLogeado.Hash);
                    if (exitoso)
                    {
                        //this.Navegar(new ListaEmails(((EmailAsociado)this._ventana.EmailAsociado).Asociado, this));
                        ((ListaEmails)this._ventanaPadre).RefrescarPagina();
                        this.Navegar((ListaEmails)this._ventanaPadre);
                    }
                }
                else
                {
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorTipoDeEmailRepetido);
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


        public bool CambiarTipoEmail()
        {
            bool retorno = false;

            if (this._ventana.TipoEmail != null)
            {
                retorno = true;
                this._ventana.Funcion = ((TipoEmailAsociado)this._ventana.TipoEmail).Funcion;
                this._ventana.Descripcion = ((TipoEmailAsociado)this._ventana.TipoEmail).Descripcion;

                this._ventana.Departamento = this.BuscarDepartamento((IList<Departamento>)this._ventana.Departamentos,
                    ((TipoEmailAsociado)this._ventana.TipoEmail).Departamento);
            }

            return retorno;
        }


        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                EmailAsociado email = (EmailAsociado)this._ventana.EmailAsociado;
                email.TipoEmailAsociado = (TipoEmailAsociado)this._ventana.TipoEmail;
                email.Operacion = "DELETE";

                bool exitoso = this._emailAsociadoServicios.Eliminar(email, UsuarioLogeado.Hash);
                if (exitoso)
                {
                    ((EmailAsociado)this._ventana.EmailAsociado).Asociado.Emails = Eliminar(email, ((EmailAsociado)this._ventana.EmailAsociado).Asociado.Emails);
                    this.Navegar(new ListaEmails(((EmailAsociado)this._ventana.EmailAsociado).Asociado));

                    //this.Navegar(new ListaEmails(((EmailAsociado)this._ventana.EmailAsociado).Asociado));
                    //((ListaEmails)this._ventanaPadre).RefrescarPagina();
                    //this.Navegar((ListaEmails)this._ventanaPadre);
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


        private IList<EmailAsociado> Eliminar(EmailAsociado emailBuscado, IList<EmailAsociado> lista)
        {
            int i = 0;
            IList<int> posiciones = new List<int>();

            foreach (EmailAsociado email in lista)
            {
                if (email.Id == emailBuscado.Id)
                {
                    posiciones.Add(i);
                }
                i++;
            }

            foreach (int posicion in posiciones)
            {
                lista.RemoveAt(posicion);
            }

            return lista;
        }


        /// <summary>
        /// Método que muestra la ventana de Auditoría de un Asociado
        /// </summary>
        public void Auditoria()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ListaAuditorias(_auditorias));

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


        public void Regresar()
        {
            //Navegar(new ListaEmails(this._asociado));
            this.RegresarVentanaPadre();
        }
    }
}
