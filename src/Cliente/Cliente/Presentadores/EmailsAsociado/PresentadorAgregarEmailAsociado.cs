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

namespace Trascend.Bolet.Cliente.Presentadores.EmailsAsociado
{
    class PresentadorAgregarEmailAsociado : PresentadorBase
    {

        private IAgregarEmailAsociado _ventana;
        private IEmailAsociadoServicios _emailAsociadoServicios;
        private IAsociadoServicios _asociadoServicios;
        private IDepartamentoServicios _departamentoServicios;
        private ITipoEmailAsociadoServicios _tipoEmailServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private Asociado _asociado;


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarEmailAsociado(IAgregarEmailAsociado ventana, object asociado)
        {
            try
            {
                this._ventana = ventana;
                EmailAsociado datosTransferencia = new EmailAsociado();
                datosTransferencia.Asociado = (Asociado)asociado;
                this._ventana.EmailAsociado = datosTransferencia;
                this._asociado = (Asociado)asociado;
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


                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarTipoEmail,
                Recursos.Ids.AgregarJustificacion);


                IList<TipoEmailAsociado> emails = this._tipoEmailServicios.ConsultarTodos();

                emails = FiltrarEmailsPorDepartamento(UsuarioLogeado.Departamento, emails);

                this._ventana.TiposEmail = emails;

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                Departamento primerDepartamento = new Departamento();
                primerDepartamento.Id = "NGN";
                departamentos.Insert(0, primerDepartamento);
                this._ventana.Departamentos = departamentos;

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

                if (this._ventana.TipoEmail != null)
                {
                    EmailAsociado email = (EmailAsociado)this._ventana.EmailAsociado;
                    email.TipoEmailAsociado = (TipoEmailAsociado)this._ventana.TipoEmail;
                    email.Id = int.Parse(email.Asociado.Id.ToString() + email.TipoEmailAsociado.Id);
                    email.Operacion = "INSERT";
                    if (!this.VerificarExistenciaEmailAsociado(email.TipoEmailAsociado.Id, _asociado))
                    {
                        bool exitoso = this._emailAsociadoServicios.InsertarOModificar(email, UsuarioLogeado.Hash);
                        if (exitoso)
                        {
                            ((EmailAsociado)this._ventana.EmailAsociado).Asociado.Emails.Add(email);
                            this.Navegar(new ListaEmails(((EmailAsociado)this._ventana.EmailAsociado).Asociado));
                        }
                    }
                    else
                    {
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorTipoDeEmailRepetido);
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
    }
}
