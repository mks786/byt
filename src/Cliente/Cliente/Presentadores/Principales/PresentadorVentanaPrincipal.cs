using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Principales;
using Trascend.Bolet.Cliente.Ventanas.Agentes;
using Trascend.Bolet.Cliente.Ventanas.Anexos;
using Trascend.Bolet.Cliente.Ventanas.Categorias;
using Trascend.Bolet.Cliente.Ventanas.Cartas;
using Trascend.Bolet.Cliente.Ventanas.CartasOuts;
using Trascend.Bolet.Cliente.Ventanas.Boletines;
using Trascend.Bolet.Cliente.Ventanas.Estados;
using Trascend.Bolet.Cliente.Ventanas.Internacionales;
using Trascend.Bolet.Cliente.Ventanas.Nacionales;
using Trascend.Bolet.Cliente.Ventanas.Medios;
using Trascend.Bolet.Cliente.Ventanas.Objetos;
using Trascend.Bolet.Cliente.Ventanas.Paises;
using Trascend.Bolet.Cliente.Ventanas.Resoluciones;
using Trascend.Bolet.Cliente.Ventanas.Roles;
using Trascend.Bolet.Cliente.Ventanas.TipoInfoboles;
using Trascend.Bolet.Cliente.Ventanas.Usuarios;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.TipoFechas;
using Trascend.Bolet.Cliente.Ventanas.Estatuses;
using Trascend.Bolet.Cliente.Ventanas.Poderes;
using Trascend.Bolet.Cliente.Ventanas.Interesados;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Resumenes;
using Trascend.Bolet.Cliente.Ventanas.Remitentes;
using Trascend.Bolet.Cliente.Ventanas.EntradasAlternas;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Cesiones;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Licencias;
using Trascend.Bolet.Cliente.Ventanas;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosDeDomicilio;

namespace Trascend.Bolet.Cliente.Presentadores.Principales
{
    class PresentadorVentanaPrincipal : PresentadorBase
    {
        private IVentanaPrincipal _ventana;
        private IUsuarioServicios _usuarioServicios;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public PresentadorVentanaPrincipal(IVentanaPrincipal ventana)
        {
            try
            {
                this._ventana = ventana;
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                            ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                Usuario u = UsuarioLogeado;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        #region Métodos de eventos del menú

        /// <summary>
        /// Método que coloca la página "AgregarAgente" en el Frame principal
        /// </summary>
        public void AgregarAgente()
        {
            this._ventana.Contenedor.Navigate(new AgregarAgente());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarAgentes" en el Frame principal
        /// </summary>
        public void ConsultarAgentes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarAgentes());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarEstados" en el Frame principal
        /// </summary>
        public void ConsultarEstados()
        {
            this._ventana.Contenedor.Navigate(new ConsultarEstados());
        }

        /// <summary>
        /// Método que coloca la página "AgregarEstado" en el Frame principal
        /// </summary>
        public void AgregarEstados()
        {
            this._ventana.Contenedor.Navigate(new AgregarEstado());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarInternacionales" en el Frame principal
        /// </summary>
        public void ConsultarInternacionales()
        {
            this._ventana.Contenedor.Navigate(new ConsultarInternacionales());
        }

        /// <summary>
        /// Método que coloca la página "AgregarInternacional" en el Frame principal
        /// </summary>
        public void AgregarInternacional()
        {
            this._ventana.Contenedor.Navigate(new AgregarInternacional());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarNacionales" en el Frame principal
        /// </summary>
        public void ConsultarNacionales()
        {
            this._ventana.Contenedor.Navigate(new ConsultarNacionales());
        }

        /// <summary>
        /// Método que coloca la página "AgregarNacional" en el Frame principal
        /// </summary>
        public void AgregarNacionales()
        {
            this._ventana.Contenedor.Navigate(new AgregarNacional());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarUsuario" en el Frame principal
        /// </summary>
        public void AgregarObjeto()
        {
            this._ventana.Contenedor.Navigate(new AgregarObjeto());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarObjetos" en el Frame principal
        /// </summary>
        public void ConsultarObjetos()
        {
            this._ventana.Contenedor.Navigate(new ConsultarObjetos());
        }

        /// <summary>
        /// Método que coloca la página "GestionarObjetosXRoles" en el Frame principal
        /// </summary>
        public void GestionarObjetosXRoles()
        {
            this._ventana.Contenedor.Navigate(new GestionarObjetosXRoles());
        }

        /// <summary>
        /// Método que coloca la página "AgregarPais" en el Frame principal
        /// </summary>
        public void AgregarPais()
        {
            this._ventana.Contenedor.Navigate(new AgregarPais());
        }

        /// <summary>
        /// Método que coloca la página "Consultarresoluciones" en el Frame principal
        /// </summary>
        public void ConsultarResoluciones()
        {
            this._ventana.Contenedor.Navigate(new ConsultarResoluciones());
        }

        /// <summary>
        /// Método que coloca la página "AgregarResolucion" en el Frame principal
        /// </summary>
        public void AgregarResolucion()
        {
            this._ventana.Contenedor.Navigate(new AgregarResolucion());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarPaises" en el Frame principal
        /// </summary>
        public void ConsultarPais()
        {
            this._ventana.Contenedor.Navigate(new ConsultarPaises());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarRoles" en el Frame principal
        /// </summary>
        public void ConsultarRoles()
        {
            this._ventana.Contenedor.Navigate(new ConsultarRoles());
        }

        /// <summary>
        /// Método que coloca la página "AgregarRol" en el Frame principal
        /// </summary>
        public void AgregarRol()
        {
            this._ventana.Contenedor.Navigate(new AgregarRol());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarTipoFecha" en el Frame principal
        /// </summary>
        public void ConsultarTipoFechas()
        {
            this._ventana.Contenedor.Navigate(new ConsultarTipoFechas());
        }

        /// <summary>
        /// Método que coloca la página "AgregarTipoFecha" en el Frame principal
        /// </summary>
        public void AgregarTipoFecha()
        {
            this._ventana.Contenedor.Navigate(new AgregarTipoFecha());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarUsuario" en el Frame principal
        /// </summary>
        public void ConsultarTipoInfoboles()
        {
            this._ventana.Contenedor.Navigate(new ConsultarTipoInfoboles());
        }

        /// <summary>
        /// Método que coloca la página "AgregarUsuario" en el Frame principal
        /// </summary>
        public void AgregarTipoInfobol()
        {
            this._ventana.Contenedor.Navigate(new AgregarTipoInfobol());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarUsuario" en el Frame principal
        /// </summary>
        public void ConsultarUsuarios()
        {
            this._ventana.Contenedor.Navigate(new ConsultarUsuarios());
        }

        /// <summary>
        /// Método que coloca la página "AgregarUsuario" en el Frame principal
        /// </summary>
        public void AgregarUsuarios()
        {
            this._ventana.Contenedor.Navigate(new AgregarUsuario());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarBoletines" en el Frame principal
        /// </summary>
        public void ConsultarBoletines()
        {
            this._ventana.Contenedor.Navigate(new ConsultarBoletines());
        }

        /// <summary>
        /// Método que coloca la página "AgregarBoletin" en el Frame principal
        /// </summary>
        public void AgregarBoletin()
        {
            this._ventana.Contenedor.Navigate(new AgregarBoletin());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarEstatuses" en el Frame principal
        /// </summary>
        public void ConsultarEstatuses()
        {
            this._ventana.Contenedor.Navigate(new ConsultarEstatuses());
        }

        /// <summary>
        /// Método que coloca la página "AgregarEstatus" en el Frame principal
        /// </summary>
        public void AgregarEstatus()
        {
            this._ventana.Contenedor.Navigate(new AgregarEstatus());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarPoderes" en el Frame principal
        /// </summary>
        public void ConsultarPoderes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarPoderes());
        }

        /// <summary>
        /// Método que coloca la página "AgregarPoder" en el Frame principal
        /// </summary>
        public void AgregarPoder()
        {
            this._ventana.Contenedor.Navigate(new AgregarPoder());
        }

        /// <summary>
        /// Método que coloca la página "GestionarPoderesXAgentes" en el Frame principal
        /// </summary>
        public void GestionarPoderXAgente()
        {
            this._ventana.Contenedor.Navigate(new GestionarPoderesXAgentes());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarInteresados" en el Frame principal
        /// </summary>
        public void ConsultarInteresados()
        {
            this._ventana.Contenedor.Navigate(new ConsultarInteresados());
        }

        /// <summary>
        /// Método que coloca la página "AgregarInteresado" en el Frame principal
        /// </summary>
        public void AgregarInteresado()
        {
            this._ventana.Contenedor.Navigate(new AgregarInteresado());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarAsociados" en el Frame principal
        /// </summary>
        public void ConsultarAsociados()
        {
            this._ventana.Contenedor.Navigate(new ConsultarAsociados());
        }

        /// <summary>
        /// Método que coloca la página "AgregarAsociado" en el Frame principal
        /// </summary>
        public void AgregarAsociado()
        {
            this._ventana.Contenedor.Navigate(new AgregarAsociado());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarMarcas" en el Frame principal
        /// </summary>
        public void ConsultarMarcas()
        {
            this._ventana.Contenedor.Navigate(new ConsultarMarcas());
        }

        /// <summary>
        /// Método que coloca la página "AgregarMarca" en el Frame principal
        /// </summary>
        public void AgregarMarca()
        {
            this._ventana.Contenedor.Navigate(new AgregarMarca(null));
        }

        /// <summary>
        /// Método que coloca la página "Cesiones" en el Frame principal
        /// </summary>
        public void ConsultarCesiones()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCesiones());       
        }

        /// <summary>
        /// Método que coloca la página "Fusiones" en el Frame principal
        /// </summary>
        public void ConsultarFusiones()
        {
            this._ventana.Contenedor.Navigate(new ConsultarFusiones());
        }

        /// <summary>
        /// Método que coloca la página "CambiosDeDomicilio" en el Frame principal
        /// </summary>
        public void ConsultarCambiosDeDomicilio()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCambiosDeDomicilio());
        }

        /// <summary>
        /// Método que coloca la página "Cambio de Nombre" en el Frame principal
        /// </summary>
        public void CambioDeNombre()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que coloca la página "Cambio Peticionario" en el Frame principal
        /// </summary>
        public void CambioPeticionario()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que coloca la página "Licencias" en el Frame principal
        /// </summary>
        public void IrConsultarLicencias()
        {
            this._ventana.Contenedor.Navigate(new ConsultarLicencias());
        }

        /// Método que coloca la página "Licencias" en el Frame principal Gestionar   
      //  public void IrGestionarLicencia()
      //  {
      //      this._ventana.Contenedor.Navigate(new GestionarLicencia());
      //  }

        /// <summary>
        /// Método que coloca la página "ConsultarResumenes" en el Frame principal
        /// </summary>
        public void ConsultarResumenes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarResumenes());
        }

        /// <summary>
        /// Método que coloca la página "AgregarResumen" en el Frame principal
        /// </summary>
        public void AgregarResumen()
        {
            this._ventana.Contenedor.Navigate(new AgregarResumen());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarRemitentes" en el Frame principal
        /// </summary>
        public void ConsultarRemitentes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarRemitentes());
        }

        /// <summary>
        /// Método que coloca la página "AgregarRemitente" en el Frame principal
        /// </summary>
        public void AgregarRemitente()
        {
            this._ventana.Contenedor.Navigate(new AgregarRemitente());
        }

        /// <summary>
        /// Método que coloca la página "AgregarAnexo" en el Frame principal
        /// </summary>
        public void AgregarAnexo()
        {
            this._ventana.Contenedor.Navigate(new AgregarAnexo());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarAnexos" en el Frame principal
        /// </summary>
        public void ConsultarAnexos()
        {
            this._ventana.Contenedor.Navigate(new ConsultarAnexos());
        }

        /// <summary>
        /// Método que coloca la página "AgregarAnexo" en el Frame principal
        /// </summary>
        public void AgregarMedio()
        {
            this._ventana.Contenedor.Navigate(new AgregarMedio());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarAnexos" en el Frame principal
        /// </summary>
        public void ConsultarMedios()
        {
            this._ventana.Contenedor.Navigate(new ConsultarMedios());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarAnexos" en el Frame principal
        /// </summary>
        public void ConsultarCategorias()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCategorias());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarAnexos" en el Frame principal
        /// </summary>
        public void AgregarEntradaAlterna()
        {
            this._ventana.Contenedor.Navigate(new AgregarEntradaAlterna());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarAnexos" en el Frame principal
        /// </summary>
        public void ConsultarEntradasAlternas()
        {
            this._ventana.Contenedor.Navigate(new ConsultarEntradasAlternas());
        }

        /// <summary>
        /// Método que coloca la página "ConsultarAnexos" en el Frame principal
        /// </summary>
        public void AgregarCategoria()
        {
            this._ventana.Contenedor.Navigate(new AgregarCategoria());
        }

        public void TransferirPlantilla()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCartasOuts());
        }

        public void ConsultarCartas()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCartas());
        }

        public void AgregarCarta()
        {
            this._ventana.Contenedor.Navigate(new AgregarCarta());
        }

        #endregion

        /// <summary>
        /// Método que ejecuta toda la lógica del cerrado de la aplicación
        /// </summary>
        public void Salir()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._usuarioServicios.CerrarSession(UsuarioLogeado.Hash);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                App.Current.Shutdown();
            }
        }

        /// <summary>
        /// Método que aplica la permisología correspondiente al usuario que se está logeando
        /// </summary>
        public void AplicarPermisologia()
        {
            ItemCollection menuNivel1 = this._ventana.Menu.Items;

            foreach (Objeto objeto in UsuarioLogeado.Rol.Objetos)
            {
                if (objeto != null)
                    foreach (ItemsControl itemNivel1 in menuNivel1)
                    {
                        ItemCollection menuNivel2 = itemNivel1.Items;

                        foreach (ItemsControl itemNivel2 in menuNivel2)
                            switch (itemNivel2.Name)
                            {
                                case "_menuItemAgente":
                                    if (objeto.Id.Equals(Recursos.Ids.Agente))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemCambiosDeDomicilio":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemCesiones":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemFusiones":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemCambioDeDomicilio":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemCambioDeNombre":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemCambioPeticionario":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemLicencias":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemTraspasos":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemAnexo":
                                    if (objeto.Id.Equals(Recursos.Ids.Anexo))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemAsociado":
                                    if (objeto.Id.Equals(Recursos.Ids.Asociado))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemBoletin":
                                    if (objeto.Id.Equals(Recursos.Ids.Boletin))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemCategoria":
                                    if (objeto.Id.Equals(Recursos.Ids.Categoria))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemEntradaAlterna":
                                    if (objeto.Id.Equals(Recursos.Ids.EntradaAlterna))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemCarta":
                                    if (objeto.Id.Equals(Recursos.Ids.Categoria))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemEstado":
                                    if (objeto.Id.Equals(Recursos.Ids.Estado))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemEstatus":
                                    if (objeto.Id.Equals(Recursos.Ids.Estado))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemInteresado":
                                    if (objeto.Id.Equals(Recursos.Ids.Interesado))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemInternacional":
                                    if (objeto.Id.Equals(Recursos.Ids.Internacional))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemGestionDeMarcas":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemMedio":
                                    if (objeto.Id.Equals(Recursos.Ids.Medio))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemNacional":
                                    if (objeto.Id.Equals(Recursos.Ids.Nacional))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemObjeto":
                                    if (objeto.Id.Equals(Recursos.Ids.Objeto))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemPais":
                                    if (objeto.Id.Equals(Recursos.Ids.Pais))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemPoder":
                                    if (objeto.Id.Equals(Recursos.Ids.Poder))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemRemitente":
                                    if (objeto.Id.Equals(Recursos.Ids.Remitente))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemResolucion":
                                    if (objeto.Id.Equals(Recursos.Ids.Resolucion))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemResumen":
                                    if (objeto.Id.Equals(Recursos.Ids.Resumen))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemRol":
                                    if (objeto.Id.Equals(Recursos.Ids.Rol))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemTipoFecha":
                                    if (objeto.Id.Equals(Recursos.Ids.TipoFecha))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemTipoInfobol":
                                    if (objeto.Id.Equals(Recursos.Ids.TipoInfobol))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemTransferirPlantilla":
                                    if (objeto.Id.Equals(Recursos.Ids.Asociado))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemUsuario":
                                    if (objeto.Id.Equals(Recursos.Ids.Usuario))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                            }
                    }
            }
        }
    }
}
