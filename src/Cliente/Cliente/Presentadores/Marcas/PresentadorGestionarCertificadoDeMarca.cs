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
    class PresentadorGestionarCertificadoDeMarca : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ICertificadoMarcaServicios _certificadoMarcaServicios;
        private IRegistradorServicios _registradorServicios;
        private IFechaMarcaServicios _fechaMarcaServicios;
        private IUsuarioServicios _usuarioServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IGestionarCertificadoDeMarca _ventana;
        private Marca _marca;
        private CertificadoMarca _certificado;
        private Usuario _usuario;
        private IList<Auditoria> _auditorias;


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="marca">Marca seleccionada</param>       
        /// <param name="ventanaPadre">Ventana precedente a esta ventana</param>
        public PresentadorGestionarCertificadoDeMarca(IGestionarCertificadoDeMarca ventana, object certificado, object marca, object ventanaPadre)
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
                                
                this._ventana.Certificado = certificado; 
                this._certificado = (CertificadoMarca)certificado;

                this._certificadoMarcaServicios = (ICertificadoMarcaServicios)Activator.GetObject(typeof(ICertificadoMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CertificadoMarcaServicios"]);
                this._registradorServicios = (IRegistradorServicios)Activator.GetObject(typeof(IRegistradorServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RegistradorServicios"]);
                this._fechaMarcaServicios = (IFechaMarcaServicios)Activator.GetObject(typeof(IFechaMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FechaMarcaServicios"]);
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
                CertificadoMarca certificado = this._certificado;

                IList<Registrador> registradores = this._registradorServicios.ConsultarTodos();
                Registrador primerRegistrador = new Registrador();
                primerRegistrador.Id = "NGN";
                registradores.Insert(0, primerRegistrador);
                this._ventana.Registradores = registradores;
                this._ventana.Registrador = this.BuscarRegistrador(registradores, certificado.CodRegistrador);

                if (certificado.Operacion == null)
                {
                    this._ventana.MostrarBotonEliminar(true);
                }
                


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
                       Recursos.Ids.GestionarCertificadoMarca);
        }

        /// <summary>
        /// Metodo que carga la imagen del Certificado; en caso contrario, presenta un mensaje de error.
        /// </summary>
        public void VerImagenCertificado()
        {
            
            String rutaRaizCertificado = ConfigurationManager.AppSettings["rutaCertificados"].ToString();
            String rutaServerCertificado = rutaRaizCertificado + this._marca.CodigoRegistro + ".pdf";

            if(File.Exists(rutaServerCertificado))
                System.Diagnostics.Process.Start(rutaServerCertificado);
            else
                this._ventana.ArchivoNoEncontrado(Recursos.MensajesConElUsuario.ErrorCertificadoNoEncontrado);


            //System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["rutaCertificados"].ToString() + ((Marca)this._ventana.Marca).CodigoRegistro + ".pdf");
        }



        /// <summary>
        /// Metodo para visualizar la Auditoria de la tabla MYP_MCERTIFICADOS con la marca especifica
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
                auditoria.Fk = ((CertificadoMarca)this._ventana.Certificado).IdMarca;
                auditoria.Tabla = "MYP_MCERTIFICADOS";
                this._auditorias = this._certificadoMarcaServicios.AuditoriaPorFkyTabla(auditoria);

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



        public bool Modificar()
        {
            bool exitoso = false;
            CertificadoMarca certificado = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                certificado = CargarCertificadoDeLaPantalla();

                exitoso = this._certificadoMarcaServicios.InsertarOModificar(certificado, UsuarioLogeado.Hash);



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


        public CertificadoMarca CargarCertificadoDeLaPantalla()
        {
            CertificadoMarca certificado = (CertificadoMarca)this._ventana.Certificado;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            try
            {


                if(certificado.Operacion == null)
                    certificado.Operacion = "MODIFY";

                certificado.IdMarca = ((CertificadoMarca)this._ventana.Certificado).IdMarca;

                certificado.CodRegistrador = (this._ventana.Registrador != null) && (!((Registrador)this._ventana.Registrador).Id.Equals("NGN"))
                    ? (Registrador)this._ventana.Registrador : null;
                
                certificado.NumeroRecibo = ((this._ventana.ReciboNumero != null) && (!this._ventana.ReciboNumero.Equals(""))) 
                    ? this._ventana.ReciboNumero : null;

                certificado.RegistroBs = (this._ventana.RegistroBs != null) && (!this._ventana.RegistroBs.Equals(""))
                    ? this._ventana.RegistroBs : null;

                certificado.EscrituraBs = (this._ventana.EscrituraBs != null) && (!this._ventana.EscrituraBs.Equals(""))
                    ? this._ventana.EscrituraBs : null;

                certificado.PapelProtocolo = (this._ventana.PapelProtocolo != null) && (!this._ventana.PapelProtocolo.Equals(""))
                    ? this._ventana.PapelProtocolo : null;

                certificado.TotalBs = (this._ventana.TotalBs != null) && (!this._ventana.TotalBs.Equals(""))
                    ? this._ventana.TotalBs : null;

                certificado.Clases = (this._ventana.Clases != null) && (!this._ventana.Clases.Equals(""))
                    ? this._ventana.Clases : null;

                certificado.Comentario = (this._ventana.Comentario != null) && (!this._ventana.Comentario.Equals(""))
                    ? this._ventana.Comentario : null;


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

            return certificado;
        }


        public string ObtenerIdMarca()
        {
            return ((Marca)this._marca).Id.ToString();
        }


        public void IrFechas()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaFechasDeMarca(this._marca, this._ventana));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        public void Eliminar()
        {
            bool exitoso = false;
            CertificadoMarca certificado = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ((CertificadoMarca)this._ventana.Certificado).Operacion = "DELETE";

                certificado = CargarCertificadoDeLaPantalla();

                exitoso = this._certificadoMarcaServicios.Eliminar(certificado, UsuarioLogeado.Hash);

                if (exitoso)
                {
                    this._ventana.Mensaje("El Certificado de la marca fue eliminado exitosamente", 1);
                    this.RegresarVentanaPadre();

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
