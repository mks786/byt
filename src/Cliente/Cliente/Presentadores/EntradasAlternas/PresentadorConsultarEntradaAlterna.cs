using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.EntradasAlternas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Presentadores.EntradasAlternas
{
    class PresentadorConsultarEntradaAlterna : PresentadorBase
    {

        private IConsultarEntradaAlterna _ventana;
        private IEntradaAlternaServicios _entradaAlternaServicios;
        private IMedioServicios _medioServicios;
        private IUsuarioServicios _usuarioServicios;
        private IRemitenteServicios _remitenteServicios;
        private ICategoriaServicios _categoriaServicios;
        private IDepartamentoServicios _departamentoServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;

        private IList<Auditoria> _auditorias;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="entradaAlterna">Entrada Alterna a mostrar</param>
        public PresentadorConsultarEntradaAlterna(IConsultarEntradaAlterna ventana, object entradaAlterna)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.EntradaAlterna = entradaAlterna;
                this.CargarComboBoxTiempo(this._ventana.Horas, this._ventana.Minutos);
                this._ventana.SetTipoDestinatario = this.BuscarTipoDestinatario(((EntradaAlterna)entradaAlterna).TipoDestinatario);

                if (((EntradaAlterna)entradaAlterna).Hora != null)
                {
                    this._ventana.SetHora = ((EntradaAlterna)entradaAlterna).Hora.Value.Hour.ToString();
                    this._ventana.SetMinuto = ((EntradaAlterna)entradaAlterna).Hora.Value.Minute.ToString();
                }

                this._ventana.SetTipoDestinatario = BuscarTipoDestinatario(((EntradaAlterna)this._ventana.EntradaAlterna).TipoDestinatario);

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
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarAgente,
                    Recursos.Ids.ConsultarAgente);

                EntradaAlterna entradaAlterna = (EntradaAlterna)this._ventana.EntradaAlterna;

                IList<Medio> medios = this._medioServicios.ConsultarTodos();
                this._ventana.Medios = medios;
                this._ventana.Medio = this.BuscarMedio(medios, entradaAlterna.Medio);

                IList<Usuario> receptores = this._usuarioServicios.ConsultarTodos();
                this._ventana.Receptores = receptores;
                this._ventana.Receptor = this.BuscarReceptor(receptores, entradaAlterna.Receptor);


                IList<Remitente> remitentes = this._remitenteServicios.ConsultarTodos();
                Remitente primerRemitente = new Remitente();
                primerRemitente.Id = "NGN";
                remitentes.Insert(0, primerRemitente);
                this._ventana.Remitentes = remitentes;
                this._ventana.Remitente = this.BuscarRemitente(remitentes, entradaAlterna.Remitente);

                IList<Categoria> categorias = this._categoriaServicios.ConsultarTodos();
                Categoria primeraCategoria = new Categoria();
                primeraCategoria.Id = "NGN";
                categorias.Insert(0, primeraCategoria);
                this._ventana.Categorias = categorias;
                this._ventana.Categoria = this.BuscarCategoria(categorias, entradaAlterna.Categoria);

                IList<ListaDatosValores> listaAcuse =
                this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaEntradaAlterna));
                ListaDatosValores primerDatoValor = new ListaDatosValores();
                primerDatoValor.Id = "NGN";
                listaAcuse.Insert(0, primerDatoValor);
                this._ventana.TiposAcuse = listaAcuse;

                if (entradaAlterna.TipoAcuse != null)
                {
                    ListaDatosValores acuseBuscado = new ListaDatosValores();
                    acuseBuscado.Valor = entradaAlterna.TipoAcuse.ToString();
                    this._ventana.TipoAcuse = this.BuscarListaDeDatosValores(listaAcuse, acuseBuscado);
                }

                this._ventana.Personas = receptores;

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                this._ventana.Departamentos = departamentos;

                if (((EntradaAlterna)this._ventana.EntradaAlterna).TipoDestinatario == 'D')
                    this._ventana.Departamento = this.BuscarDepartamento(departamentos, new Departamento(((EntradaAlterna)this._ventana.EntradaAlterna).CodigoDestinatartio));
                else if (((EntradaAlterna)this._ventana.EntradaAlterna).TipoDestinatario == 'P')
                    this._ventana.Persona = this.BuscarPersonaPorInicial(receptores, ((EntradaAlterna)this._ventana.EntradaAlterna).CodigoDestinatartio);

                Auditoria auditoria = new Auditoria();
                auditoria.Fk = ((EntradaAlterna)this._ventana.EntradaAlterna).Id;
                auditoria.Tabla = "ENTRADA_ALT";
                this._auditorias = this._entradaAlternaServicios.AuditoriaPorFkyTabla(auditoria);

                if (null != this._auditorias && this._auditorias.Count > 0)
                    this._ventana.PintarAuditoria();
                
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
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// modifica los datos de la EntradaAlterna
        /// </summary>
        public void Modificar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                char[] acuse = null;

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                    this._ventana.MensajeConfirmacion(false);
                }

                //Modifica los datos del Agente
                else
                {
                    EntradaAlterna entradaAlterna = (EntradaAlterna)this._ventana.EntradaAlterna;

                    entradaAlterna.TipoDestinatario = this._ventana.GetTipoDestinatario;
                    entradaAlterna.Medio = (Medio)this._ventana.Medio;
                    entradaAlterna.Receptor = ((Usuario)this._ventana.Receptor).Iniciales;

                    if (!((ListaDatosValores)this._ventana.TipoAcuse).Id.Equals("NGN"))
                    {
                        acuse = (((ListaDatosValores)this._ventana.TipoAcuse).Valor).ToCharArray();
                        entradaAlterna.TipoAcuse = acuse[0];
                    }
                    else
                        entradaAlterna.TipoAcuse = null;

                    entradaAlterna.Operacion = "MODIFY";

                    if ((Remitente)this._ventana.Remitente != null)
                    entradaAlterna.Remitente = !((Remitente)this._ventana.Remitente).Id.Equals("NGN") ? (Remitente)this._ventana.Remitente : null;

                    if ((Categoria)this._ventana.Categoria != null)
                    entradaAlterna.Categoria = !((Categoria)this._ventana.Categoria).Id.Equals("NGN") ? (Categoria)this._ventana.Categoria : null;

                    if (!this._ventana.Hora.Equals(""))
                        entradaAlterna.Hora = new DateTime(entradaAlterna.Fecha.Value.Year, entradaAlterna.Fecha.Value.Month, entradaAlterna.Fecha.Value.Day,
                            !this._ventana.Hora.Equals(" ") ? int.Parse((string)this._ventana.Hora) : 0,
                            !this._ventana.Minuto.Equals(" ") ? int.Parse((string)this._ventana.Minuto) : 0, 0);

                    if (entradaAlterna.TipoDestinatario != ' ')
                    {
                        entradaAlterna.CodigoDestinatartio = entradaAlterna.TipoDestinatario == 'D' ? ((Departamento)_ventana.Departamento).Id : ((Usuario)_ventana.Persona).Iniciales;
                        entradaAlterna.DescripcionDestinatario = entradaAlterna.TipoDestinatario == 'D' ? ((Departamento)_ventana.Departamento).Descripcion : ((Usuario)_ventana.Persona).NombreCompleto;
                    }

                    bool exitoso = this._entradaAlternaServicios.InsertarOModificar(entradaAlterna, UsuarioLogeado.Hash);

                    if (exitoso)
                    {
                        this._ventana.MensajeConfirmacion(true);
                        this._ventana.HabilitarCampos = false;
                        this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                    }
                        //this.Navegar(Recursos.MensajesConElUsuario.EntradaAlternaModificado, false);

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
        /// Método que elimina una EntradaAlterna
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._entradaAlternaServicios.Eliminar((EntradaAlterna)this._ventana.EntradaAlterna, UsuarioLogeado.Hash))
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.EntradaAlternaEliminado;
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
        /// Método que se encarga de mostrar la ventana con la lista de Auditorías
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
                auditoria.Fk = ((EntradaAlterna)this._ventana.EntradaAlterna).Id;
                auditoria.Tabla = "ENTRADA_ALT";
                this._auditorias = this._entradaAlternaServicios.AuditoriaPorFkyTabla(auditoria);
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
    }
}
