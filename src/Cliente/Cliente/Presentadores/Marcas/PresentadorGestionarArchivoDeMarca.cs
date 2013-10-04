using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using Trascend.Bolet.Cliente.Ventanas.Corresponsales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Renovaciones;
using Trascend.Bolet.ControlesByT.Ventanas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorGestionarArchivoDeMarca : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IArchivoServicios _archivoServicios;
        private ITipoDocumentoServicios _tipoDocumentoServicios;
        private ITipoCajaServicios _tipoCajaServicios;
        private ICajaServicios _cajaServicios;
        private IAlmacenServicios _almacenServicios;
        private IUsuarioServicios _usuarioServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IGestionarArchivoDeMarca _ventana;
        private Marca _marca;
        private Archivo _archivo;
        private Usuario _usuario;
       

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="marca">Marca seleccionada</param>       
        /// <param name="ventanaPadre">Ventana precedente a esta ventana</param>
        public PresentadorGestionarArchivoDeMarca(IGestionarArchivoDeMarca ventana, object archivo, object marca, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._marca = (Marca)marca;
                //this._ventana.Marca = marca;
                //this._archivo.Marca = (Marca)marca;
                this._ventana.Archivo = archivo;
                this._archivo = (Archivo)archivo;

                this._archivoServicios = (IArchivoServicios)Activator.GetObject(typeof(IArchivoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ArchivoServicios"]);
                this._tipoDocumentoServicios = (ITipoDocumentoServicios)Activator.GetObject(typeof(ITipoDocumentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoDocumentoServicios"]);
                this._tipoCajaServicios = (ITipoCajaServicios)Activator.GetObject(typeof(ITipoCajaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoCajaServicios"]);
                this._cajaServicios = (ICajaServicios)Activator.GetObject(typeof(ICajaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CajaServicios"]);
                this._almacenServicios = (IAlmacenServicios)Activator.GetObject(typeof(IAlmacenServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AlmacenServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);

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

                ActualizarTitulo();

                Marca marca = this._marca;
                Archivo archivo = this._archivo;
                

                //if (marca.CodigoMarcaInternacional != 0)
                //    this._ventana.AuxArchivo = marca.CodigoMarcaInternacional.ToString();
                //else
                //    this._ventana.AuxArchivo = "1";

                this._ventana.IdMarcaArchivo = marca.Id.ToString();


                //IList<TipoDocumento> documentos = this._tipoDocumentoServicios.ConsultarTodos();
                IList<TipoDocumento> documentos = this._tipoDocumentoServicios.ObtenerTipoDocumentoMarcaOPatente("M", "I");
                this._ventana.Documentos = documentos;
                if (null != archivo.Documento)
                {
                    TipoDocumento primerDocumento = new TipoDocumento("NGN");
                    documentos.Insert(0, primerDocumento);
                    this._ventana.Documento = this.BuscarDocumento(documentos, archivo.Documento);
                }
                else
                {

                    TipoDocumento primerDocumento = new TipoDocumento("NGN");
                    documentos.Insert(0, primerDocumento);
                    #region Codigo Comentado
                    //this._ventana.Documento = this.BuscarDocumento(documentos, documentoABuscar);
                    //archivo.Documento = (TipoDocumento)this._ventana.Documento;
                    //((Archivo)this._ventana.Archivo).Documento = archivo.Documento;
                    #endregion
                }

                
                IList<ListaDatosValores> tipoDocumentos = this._listaDatosValoresServicios.
                    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiTipoDocumentoArchivoMarca));
                ListaDatosValores primerTipoDocumento = new ListaDatosValores("NGN");
                tipoDocumentos.Insert(0, primerTipoDocumento);
                this._ventana.TipoDocumentos = tipoDocumentos;
                if (archivo.TipoDeDocumento != null)
                {
                    ListaDatosValores tipoDocumentoABuscar = new ListaDatosValores(archivo.TipoDeDocumento);
                    this._ventana.TipoDocumento = this.BuscarTipoDocumento(tipoDocumentos, tipoDocumentoABuscar);
                }
                


                //IList<TipoCaja> tiposDeCaja = this._tipoCajaServicios.ConsultarTodos();
                IList<TipoCaja> tiposDeCaja = this._tipoCajaServicios.ObtenerTipoCajaMarcaOPatente(Recursos.Etiquetas.filterArchivoTipoCajaMarcas);
                TipoCaja primerTipoCaja = new TipoCaja(0);
                tiposDeCaja.Insert(0, primerTipoCaja);
                this._ventana.TipoCajas = tiposDeCaja;
                TipoCaja tipoCajaBuscar = new TipoCaja(archivo.TipoDeCaja);
                this._ventana.TipoCaja = this.BuscarTipoCaja(tiposDeCaja, tipoCajaBuscar);
                

                IList<Caja> cajas = this._cajaServicios.ConsultarTodos();
                Caja primeraCaja = new Caja(0);
                cajas.Insert(0, primeraCaja);
                this._ventana.Cajas = cajas;
                //if (archivo.CajaArchivo != 0)
                //{
                    Caja cajaBuscar = new Caja(archivo.CajaArchivo);
                    this._ventana.Caja = this.BuscarCaja(cajas, cajaBuscar);
                //}
                

                IList<Almacen> almacenes = _almacenServicios.ConsultarTodos();
                Almacen primerAlmacen = new Almacen("NGN");
                almacenes.Insert(0, primerAlmacen);
                this._ventana.Almacenes = almacenes;
                if(archivo.AlmacenArchivo != null)
                    this._ventana.Almacen = this.BuscarAlmacen(almacenes, archivo.AlmacenArchivo);

                IList<Usuario> usuarios = this._usuarioServicios.ConsultarTodos();
                Usuario primerUsuario = new Usuario("NGN");
                primerUsuario.Iniciales = "";
                primerUsuario.NombreCompleto = "";
                usuarios = this.FiltrarUsuariosRepetidos(usuarios);
                usuarios.Insert(0, primerUsuario);
                this._ventana.Usuarios = usuarios;
                if (archivo.Usuario != null)
                {
                    Usuario usuarioBuscar = new Usuario();
                    usuarioBuscar.Iniciales = archivo.Usuario;
                    this._ventana.Usuario = this.BuscarUsuarioPorIniciales(usuarios, usuarioBuscar);
                }

                this._ventana.UbicacionMarca = marca.Ubicacion;

                this._ventana.ActivarBotonModificar(UsuarioLogeado.BArchivo);

                #region CODIGO COMENTADO

                //Archivo archivoPrueba = new Archivo(marca.Id.ToString());
                //Archivo resultado = this._archivoServicios.ConsultarPorId(archivoPrueba);

                

                   

                //IList<Pais> paises = this._paisServicios.ConsultarTodos();
                //IList<Estado> estados = this._estadoServicios.ConsultarTodos();
                //this._ventana.Paises = paises;
                //this._ventana.Nacionalidades = paises;
                //this._ventana.Corporaciones = estados;

                //this._ventana.Pais = this.BuscarPais(paises, interesado.Pais);
                //this._ventana.Nacionalidad = this.BuscarPais(paises, interesado.Nacionalidad);
                //this._ventana.Corporacion = this.BuscarEstado(estados, interesado.Corporacion);
                //IList<ListaDatosDominio> tiposPersona = this._listaDatosDominioServicios.ConsultarListaDatosDominioPorParametro(new ListaDatosDominio("PERSONA"));
                //this._ventana.TipoPersonas = tiposPersona;
                //this._ventana.TipoPersona = BuscarTipoPersona(interesado.TipoPersona, (IList<ListaDatosDominio>)this._ventana.TipoPersonas);
                #endregion


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


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarInteresado,
                       Recursos.Ids.ConsultarInteresado);
        }

        public string ObtenerIdMarca()
        {
            return ((Marca)this._marca).Id.ToString();
        }


        public bool Modificar()
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

               

                Archivo archivo = CargarArchivoDeLaPantalla();

                if ((archivo.Id != null) && (!archivo.Id.Equals("")))
                {
                    if((archivo.Aux != null) && (!archivo.Aux.Equals("")))
                    {
                        if ((archivo.Documento != null) && (!((TipoDocumento)archivo.Documento).Id.Equals("NGN")))
                        {
                            if ((archivo.TipoDeDocumento != null) && (!archivo.TipoDeDocumento.Equals("")))
                            {
                                exitoso = this._archivoServicios.InsertarOModificar(archivo, UsuarioLogeado.Hash);
                                //if (exitoso)
                                //{
                                //    this._ventana.MostarMensajeCompletadoConExito();
                                //    //RegresarVentanaPadre();
                                //}
                            }
                            else
                                this._ventana.Mensaje("El Archivo de la marca debe tener un Tipo de Documento asignado", 0);
                        }
                        else
                            this._ventana.Mensaje("El Archivo de la marca debe tener un Documento asignado", 0);
                    }
                    else
                        this._ventana.Mensaje("El archivo de la marca debe tener un Codigo Auxiliar", 0);
                }
                else
                    this._ventana.Mensaje("El Archivo de la marca debe tener un Codigo de Expediente", 0);


                #region Codigo Comentado
                //if (ValidarMarcaInternacional())
                        //{

                        //    if (!this._ventana.EsMarcaNacional)
                        //        marca = AgregarDatosInternacionales(marca);
                        //    else
                        //        marca.LocalidadMarca = "N";

                        //    if (marca.Interesado != null)
                        //    {
                        //        //if (marca.Poder != null)
                        //        //{
                        //        bool exitoso = this._marcaServicios.InsertarOModificar(marca, UsuarioLogeado.Hash);

                        //        if (exitoso)
                        //        {
                        //            this._ventana.HabilitarCampos = false;
                        //            this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;

                                    //if (marca.Servicio.Id.Equals("AB"))
                                    //{
                                    //    this._ventana.DeshabilitarBotonModificar();
                                    //}
                                //}
                                //}
                                //else
                                //    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorSinPoder, 0);
                        //    }
                        //    else
                        //        this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorSinInteresado, 0);
                        //}
                        //else
                        //    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorMarcaInternacional, 0);
                    //}
                    //else
                    //    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderesConAgente, 0);

                //}

                #endregion

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


        public Archivo CargarArchivoDeLaPantalla()
        {
            Archivo archivo = (Archivo)this._ventana.Archivo;
            
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            try
            {
                

                archivo.Operacion = "MODIFY";

                archivo.Id = this._ventana.IdArchivo;

                archivo.Aux = (!this._ventana.AuxArchivo.Equals("")) ? this._ventana.AuxArchivo : null;

                archivo.Documento = ((this._ventana.Documento != null) && (!((TipoDocumento)this._ventana.Documento).Id.Equals("")))
                    ? (TipoDocumento)this._ventana.Documento : null;

                archivo.TipoDeDocumento = (this._ventana.TipoDocumento != null) && (!((ListaDatosValores)this._ventana.TipoDocumento).Valor.Equals(""))
                    ? ((ListaDatosValores)this._ventana.TipoDocumento).Valor : null;

                archivo.TipoDeCaja = ((this._ventana.TipoCaja != null) && (((TipoCaja)this._ventana.TipoCaja).Id != 0))
                    ? ((TipoCaja)this._ventana.TipoCaja).Id : 0;

                archivo.CajaArchivo = ((this._ventana.Caja != null) && (((Caja)this._ventana.Caja).Id != 0))
                    ? ((Caja)this._ventana.Caja).Id : 0;

                //archivo.AlmacenArchivo = (this._ventana.Almacen != null) ? (Almacen)this._ventana.Almacen : null;
                archivo.AlmacenArchivo = (this._ventana.Almacen != null) && (!((Almacen)this._ventana.Almacen).Id.Equals("NGN")) ? (Almacen)this._ventana.Almacen : null;

                archivo.Usuario = (this._ventana.Usuario != null) ? ((Usuario)this._ventana.Usuario).Iniciales : null;

                
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

            
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return archivo;
            
        }
        
    }
}
