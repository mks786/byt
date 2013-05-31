using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.MarcasTercero;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.MarcasTercero;

namespace Trascend.Bolet.Cliente.Presentadores.MarcasTercero
{
    class PresentadorInfoAdicionalInteresadosAsociadosMarcaTerceros: PresentadorBase
    {
        private IInfoAdicionalInteresadosAsociadosMarcaTerceros _ventana;
        private MarcaTercero _marca;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IInfoAdicionalServicios _infoAdicionalServicios;
        private IList<Auditoria> _auditorias;
        private bool _nuevaInfoAdicional = false;
        private IMarcaTerceroServicios _marcaTerceroServicios;


        
        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorInfoAdicionalInteresadosAsociadosMarcaTerceros(IInfoAdicionalInteresadosAsociadosMarcaTerceros ventana, object marca)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._marca = (MarcaTercero)marca;
                this._ventana.MarcaTercero = marca;

                this._marcaTerceroServicios = (IMarcaTerceroServicios)Activator.GetObject(typeof(IMarcaTerceroServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaTerceroServicios"]);
                

                #region CODIGO COMENTADO
                //this._ventana.InfoAdicional = null != ((MarcaTercero)marca).InfoAdicional ? ((MarcaTercero)marca).InfoAdicional : new InfoAdicional("M." + this._marca.Id);

                //if (null == ((MarcaTercero)marca).InfoAdicional)
                //    this._nuevaInfoAdicional = true;


                //this._infoAdicionalServicios = (IInfoAdicionalServicios)Activator.GetObject(typeof(IInfoAdicionalServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoAdicionalServicios"]);

                //this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                //this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
                //this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
                //this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                //this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                //this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                //this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                //this._servicioServicios = (IServicioServicios)Activator.GetObject(typeof(IServicioServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ServicioServicios"]);
                //this._tipoEstadoServicios = (ITipoEstadoServicios)Activator.GetObject(typeof(ITipoEstadoServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoEstadoServicios"]);
                //this._corresponsalServicios = (ICorresponsalServicios)Activator.GetObject(typeof(ICorresponsalServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CorresponsalServicios"]);
                //this._condicionServicios = (ICondicionServicios)Activator.GetObject(typeof(ICondicionServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CondicionServicios"]);
                //this._anaquaServicios = (IAnaquaServicios)Activator.GetObject(typeof(IAnaquaServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AnaquaServicios"]);
                //this._infoAdicionalServicios = (IInfoAdicionalServicios)Activator.GetObject(typeof(IInfoAdicionalServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoAdicionalServicios"]);
                //this._infoBolMarcaTerServicios = (IInfoBolMarcaTerServicios)Activator.GetObject(typeof(IInfoBolMarcaTerServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolMarcaTerServicios"]);
                //this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                //this._busquedaServicios = (IBusquedaServicios)Activator.GetObject(typeof(IBusquedaServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BusquedaServicios"]);
                //this._statusWebServicios = (IStatusWebServicios)Activator.GetObject(typeof(IStatusWebServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["StatusWebServicios"]);
                //this._planillaServicios = (IPlanillaServicios)Activator.GetObject(typeof(IPlanillaServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PlanillaServicios"]);
                //this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                //this._marcaBaseTerceroServicios = (IMarcaBaseTerceroServicios)Activator.GetObject(typeof(IMarcaBaseTerceroServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaBaseTerceroServicios"]);
                //this._estadoMarcaServicios = (IEstadoMarcaServicios)Activator.GetObject(typeof(IEstadoMarcaServicios),
                //     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["EstadoMarcaServicios"]);
                //this._tipoBaseServicios = (ITipoBaseServicios)Activator.GetObject(typeof(ITipoBaseServicios),
                //      ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoBaseServicios"]);
                //this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                #endregion

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
        /// Constructor predeterminado que recibe una ventana padre
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorInfoAdicionalInteresadosAsociadosMarcaTerceros(IInfoAdicionalInteresadosAsociadosMarcaTerceros ventana, object marca, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._marca = (MarcaTercero)marca;
                this._ventana.MarcaTercero = marca;

                this._marcaTerceroServicios = (IMarcaTerceroServicios)Activator.GetObject(typeof(IMarcaTerceroServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaTerceroServicios"]);
                               

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

                
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleInfoAdicionalInteresadosAsociadosMarcaTercero,
                    "");

                
                #region CODIGO COMENTARIO 
                //if (this._nuevaInfoAdicional)
                //{
                //    //this._ventana.HabilitarCampos = true;
                //    //this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                //    //this._ventana.OculatarControlesAlAgregar();
                //}

                //Auditoria auditoria = new Auditoria();
                //string validarTam = ((InfoAdicional)this._ventana.InfoAdicional).Id;
                //int id;
                //if (validarTam[3] == '-')
                //    id = int.Parse(((InfoAdicional)this._ventana.InfoAdicional).Id.Substring(4));
                //else
                //    id = int.Parse(((InfoAdicional)this._ventana.InfoAdicional).Id.Substring(4));
                //auditoria.Fk = id;
                //auditoria.Tabla = "MYP_ADICIONAL";

                //_auditorias = this._infoAdicionalServicios.AuditoriaPorFkyTabla(auditoria);

                //pinta el boton de Auditoria referenciado a InfoAdicional
                //if (_auditorias.Count > 0)
                //    this._ventana.PintarAuditoria();
                #endregion

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
        /// Método que invoca una nueva página "ConsultarMarca" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarMarca()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new GestionarMarcaTercero(this._marca, this._ventana.Tab));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que realiza toda la lógica para agregar la Informacion Adicional de Terceros a la base de datos en la Marca Especifica
        /// </summary>
        public string Aceptar()
        {

            string exitoso = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Habilitar campos
                //if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                //{
                //    this._ventana.HabilitarCampos = true;
                //    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                //}

                ////Agregar o modificar datos
                //else
                //{

                String prueba = String.Empty;
                prueba = "prueba";

                MarcaTercero marcaTercero = (MarcaTercero)this._ventana.MarcaTercero;

                marcaTercero.Operacion = "MODIFY";

                exitoso = this._marcaTerceroServicios.InsertarOModificarMarcaTercero(marcaTercero, UsuarioLogeado.Hash);


               // marcaTercero.Operacion = "MODIFY";

                /*
                    InfoAdicional infoAdicional = (InfoAdicional)this._ventana.InfoAdicional;

                    infoAdicional.Operacion = this._nuevaInfoAdicional ? "CREATE" : "MODIFY";

                    this._marca.InfoAdicional = infoAdicional;

                    exitoso = this._infoAdicionalServicios.InsertarOModificar(infoAdicional, UsuarioLogeado.Hash);
                */
                //}

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

            return exitoso;
        }

    }


}
