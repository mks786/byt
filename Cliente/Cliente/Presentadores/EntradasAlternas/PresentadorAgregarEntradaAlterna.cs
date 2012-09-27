using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.EntradasAlternas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Presentadores.EntradasAlternas
{
    class PresentadorAgregarEntradaAlterna : PresentadorBase
    {
        private IAgregarEntradaAlterna _ventana;
        private IEntradaAlternaServicios _entradaAlternaServicios;
        private IMedioServicios _medioServicios;
        private IUsuarioServicios _usuarioServicios;
        private IRemitenteServicios _remitenteServicios;
        private ICategoriaServicios _categoriaServicios;
        private IDepartamentoServicios _departamentoServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarEntradaAlterna(IAgregarEntradaAlterna ventana)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.EntradaAlterna = new EntradaAlterna();
                this._entradaAlternaServicios = (IEntradaAlternaServicios)Activator.GetObject(typeof(IEntradaAlternaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["EntradaAlternaServicios"]);
                this._medioServicios = (IMedioServicios)Activator.GetObject(typeof(IMedioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MedioServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._remitenteServicios = (IRemitenteServicios)Activator.GetObject(typeof(IRemitenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RemitenteServicios"]);
                this._categoriaServicios = (ICategoriaServicios)Activator.GetObject(typeof(ICategoriaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CategoriaServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
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

           
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarEntradaAlterna,
                    Recursos.Ids.AgregarEntradaAlterna);

                this._ventana.Medios = this._medioServicios.ConsultarTodos();

                this._ventana.Receptores = this._usuarioServicios.ConsultarTodos();

                IList<Remitente> remitentes = this._remitenteServicios.ConsultarTodos();
                Remitente primerRemitente = new Remitente();
                primerRemitente.Id = "NGN";
                remitentes.Insert(0, primerRemitente);
                this._ventana.Remitentes = remitentes;

                IList<Categoria> categorias = this._categoriaServicios.ConsultarTodos();
                Categoria primeraCategoria = new Categoria();
                primeraCategoria.Id = "NGN";
                categorias.Insert(0, primeraCategoria);
                this._ventana.Categorias = categorias;

                this._ventana.Personas = this._ventana.Receptores;

                this._ventana.Departamentos = this._departamentoServicios.ConsultarTodos();

                this.CargarComboBoxTiempo(this._ventana.Horas, this._ventana.Minutos);

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
        /// Método que realiza toda la lógica para agregar a la EntradaAlterna dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                EntradaAlterna entradaAlterna = (EntradaAlterna)this._ventana.EntradaAlterna;

                entradaAlterna.Medio = (Medio)this._ventana.Medio;
                entradaAlterna.Receptor = ((Usuario)this._ventana.Receptor).Iniciales;
                entradaAlterna.Remitente = !((Remitente)this._ventana.Remitente).Id.Equals("NGN") ? (Remitente)this._ventana.Remitente : null;
                entradaAlterna.Categoria = !((Categoria)this._ventana.Categoria).Id.Equals("NGN") ? (Categoria)this._ventana.Categoria : null;
                entradaAlterna.TipoDestinatario = this._ventana.TipoDestinatario;

                if (!this._ventana.Hora.Equals(""))
                    entradaAlterna.Hora = new DateTime(entradaAlterna.Fecha.Value.Year, entradaAlterna.Fecha.Value.Month, entradaAlterna.Fecha.Value.Day,
                        !this._ventana.Hora.Equals(" ") ? int.Parse((string)this._ventana.Hora) : 0,
                        !this._ventana.Minuto.Equals(" ") ? int.Parse((string)this._ventana.Minuto) : 0, 0);

                if (entradaAlterna.TipoDestinatario != ' ')
                {
                    entradaAlterna.CodigoDestinatartio = entradaAlterna.TipoDestinatario == 'D' ? ((Departamento)_ventana.Departamento).Id : ((Usuario)_ventana.Persona).Iniciales;
                    entradaAlterna.DescripcionDestinatario = entradaAlterna.TipoDestinatario == 'D' ? ((Departamento)_ventana.Departamento).Descripcion : ((Usuario)_ventana.Persona).NombreCompleto;
                }

                if (!this._entradaAlternaServicios.VerificarExistencia(entradaAlterna))
                {
                    bool exitoso = this._entradaAlternaServicios.InsertarOModificar(entradaAlterna, UsuarioLogeado.Hash);

                    if (exitoso)
                        this.Navegar(Recursos.MensajesConElUsuario.EntradaAlternaInsertado, false);
                }
                else
                {
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorAgenteRepetido);
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
    }
}
