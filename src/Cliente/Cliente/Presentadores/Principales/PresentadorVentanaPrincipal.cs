using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Principales;
using Trascend.Bolet.Cliente.Ventanas.Abandonos;
using Trascend.Bolet.Cliente.Ventanas.AbandonosPatente;
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
using Trascend.Bolet.Cliente.Ventanas.Recordatorios;
using Trascend.Bolet.Cliente.Ventanas.Resumenes;
using Trascend.Bolet.Cliente.Ventanas.Remitentes;
using Trascend.Bolet.Cliente.Ventanas.EntradasAlternas;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Cesiones;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Licencias;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.FusionesPatentes;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CesionesPatentes;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.LicenciasPatentes;
using Trascend.Bolet.Cliente.Ventanas.EstadosMarca;
using Trascend.Bolet.Cliente.Ventanas.TiposBase;
using Trascend.Bolet.Cliente.Ventanas.MarcasTercero;
using Trascend.Bolet.Cliente.Ventanas;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosDeDomicilio;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosDeNombre;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosPeticionario;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CambiosDeDomicilioPatentes;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CambiosDeNombrePatentes;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CambiosPeticionarioPatentes;
using Trascend.Bolet.Cliente.Ventanas.Renovaciones;
using Trascend.Bolet.Cliente.Ventanas.EscritosMarca;
using Trascend.Bolet.Cliente.Ventanas.EscritosPatente;
using Trascend.Bolet.Cliente.Ventanas.Logines;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Anualidades;
using System.Diagnostics;
using System.Collections.Generic;

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
            this._ventana.Contenedor.Navigate(new ConsultarAsociados(null,null));
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
        /// Método que coloca la página "GestionarCesion" en el Frame principal
        /// </summary>
        public void AgregarCesion()
        {
            this._ventana.Contenedor.Navigate(new GestionarCesion(null));
        }

        /// <summary>
        /// Método que coloca la página "Fusiones" en el Frame principal
        /// </summary>
        public void ConsultarFusiones()
        {
            this._ventana.Contenedor.Navigate(new ConsultarFusiones());
        }

        public void AgregarFusion()
        {
            this._ventana.Contenedor.Navigate(new GestionarFusion(null));
        }

        /// <summary>
        /// Método que coloca la página "CambiosDeDomicilio" en el Frame principal
        /// </summary>
        public void ConsultarCambiosDeDomicilio()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCambiosDeDomicilio());
        }

        /// <summary>
        /// Método que coloca la página "AgregarCambioDeDomicilio" en el Frame principal
        /// </summary>
        public void AgregarCambioDeDomicilio()
        {
            this._ventana.Contenedor.Navigate(new GestionarCambioDeDomicilio(null));
        }

        /// <summary>
        /// Método que coloca la página "CambiosDeNombre" en el Frame principal
        /// </summary>
        public void ConsultarCambiosDeNombre()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCambiosDeNombre());
        }

        /// <summary>
        /// Método que coloca la página "AgregarCambioDeNombre" en el Frame principal
        /// </summary>
        public void AgregarCambioDeNombre()
        {
            this._ventana.Contenedor.Navigate(new GestionarCambioDeNombre(null));
        }

        /// <summary>
        /// Método que coloca la página "Cambio de Nombre" en el Frame principal
        /// </summary>
        public void CambioDeNombre()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que coloca la página "CambiosDeDomicilio" en el Frame principal
        /// </summary>
        public void ConsultarCambiosDePeticionario()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCambiosPeticionario());
        }

        /// <summary>
        /// Método que coloca la página "AgregarCambioPeticionario" en el Frame principal
        /// </summary>
        public void AgregarCambioPeticionario()
        {
            this._ventana.Contenedor.Navigate(new GestionarCambioPeticionario(null));
        }

        /// <summary>
        /// Método que coloca la página "Licencias" en el Frame principal
        /// </summary>
        public void IrConsultarLicencias()
        {
            this._ventana.Contenedor.Navigate(new ConsultarLicencias());
        }

        /// <summary>
        /// Método que coloca la página "Gestionar Licencias" en el Frame principal
        /// </summary>
        public void AgregarLicencia()
        {
            this._ventana.Contenedor.Navigate(new GestionarLicencia(null));
        }

        /// <summary>
        /// Método que coloca la página "Gestionar Renovaciones" en el Frame principal
        /// </summary>
        public void ConsultarRenovacionMarca()
        {
            this._ventana.Contenedor.Navigate(new ConsultarRenovaciones());
        }

        /// <summary>
        /// Método que coloca la página "Gestionar Licencias" en el Frame principal
        /// </summary>
        public void GestionarRenovacionMarca()
        {
            this._ventana.Contenedor.Navigate(new GestionarRenovacion(null));
        }

        /// <summary>
        /// Método que coloca la página "ConsultarRecordatorios" en el Frame principal
        /// </summary>
        public void Recordatorios()
        {
            this._ventana.Contenedor.Navigate(new ConsultarRecordatorios());
        }

        /// <summary>
        /// Método que coloca la página "Consultar Estados De Marcas" en el Frame principal
        /// </summary>
        public void ConsultarEstadosMarca()
        {
            this._ventana.Contenedor.Navigate(new ConsultarEstadosMarcas());
        }

        /// <summary>
        /// Método que coloca la página "Agregar Estado De Marca" en el Frame principal
        /// </summary>
        public void AgregarEstadosMarcas()
        {
            this._ventana.Contenedor.Navigate(new AgregarEstadoMarca());
        }

        /// <summary>
        /// Método que coloca la página "Gestionar Licencias" en el Frame principal
        /// </summary>
        public void GestionarEstadosMarca()
        {
            this._ventana.Contenedor.Navigate(new AgregarEstadoMarca());
        }

        /// <summary>
        /// Método que coloca la página "Gestionar Licencias" en el Frame principal
        /// </summary>
        public void ConsultarTipoBase()
        {
            this._ventana.Contenedor.Navigate(new ConsultarTiposBase());
        }

        /// <summary>
        /// Método que coloca la página "Gestionar Licencias" en el Frame principal
        /// </summary>
        public void GestionarTipoBase()
        {
            this._ventana.Contenedor.Navigate(new AgregarTipoBase());
        }

        /// <summary>
        /// Método que coloca la página "Gestionar Licencias" en el Frame principal
        /// </summary>
        public void ConsultarAbandonos()
        {
            this._ventana.Contenedor.Navigate(new ConsultarAbandonos());
        }
        /// <summary>
        /// Método que coloca la página "Gestionar Licencias" en el Frame principal
        /// </summary>
        public void GestionarAbandonos()
        {
            this._ventana.Contenedor.Navigate(new GestionarAbandono(null));
        }

        /// <summary>
        /// Método que coloca la página "Consultar MarcaTercero" en el Frame principal
        /// </summary>
        public void ConsultarMarcaATerceros()
        {
            //this._ventana.Contenedor.Navigate(new ConsultarMarcasTercero());
            this._ventana.Contenedor.Navigate(new ConsultaMarcasTercero());
        }

        /// <summary>
        /// Método que coloca la página "Consultar Inventores" en el frame principal
        /// </summary>
        public void IrConsultarInventores()
        {
            Patente patente = new Patente(9607);
            this._ventana.Contenedor.Navigate(new ListaInventores(patente));
        }

        /// <summary>
        /// Método que coloca la página "Gestionar Marca Tercero" en el Frame principal
        /// </summary>
        public void GestionarMarcaATercero()
        {
            this._ventana.Contenedor.Navigate(new GestionarMarcaTercero(null));
        }

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

        /// <summary>
        /// Método que se encarga de realizar el llamado a las pantallas de los escritos
        /// </summary>
        /// <param name="nombreBoton">nombre del boton que creo el evento</param>
        public void IrEscrito(string nombreBoton)
        {
            switch (nombreBoton)
            {
                case "_menuItemCopiaCertificada":
                    this._ventana.Contenedor.Navigate(new CopiaCertificada());
                    break;
                case "_menuItemCorreccionErrorMaterial":
                    this._ventana.Contenedor.Navigate(new CorreccionErrorMaterial());
                    break;
                case "_menuItemCorreccionErrorMaterialDistingue":
                    this._ventana.Contenedor.Navigate(new CorreccionErrorMaterialDistingue());
                    break;
                case "_menuItemCertificadoDeOrigen":
                    this._ventana.Contenedor.Navigate(new CertificadoDeOrigen());
                    break;
                case "_menuItemContinuacionDeTramite":
                    this._ventana.Contenedor.Navigate(new ContinuacionDeTramite());
                    break;
                case "_menuItemCorreccionDelDistingue":
                    this._ventana.Contenedor.Navigate(new CorreccionDelDistingue());
                    break;
                case "_menuItemCambioDeTramitante":
                    this._ventana.Contenedor.Navigate(new CambioDeTramitante());
                    break;
                case "_menuItemDesistimiento":
                    this._ventana.Contenedor.Navigate(new DeDesistimiento());
                    break;
                case "_menuItemLimitacionDelDistingue":
                    this._ventana.Contenedor.Navigate(new LimitacionDelDistingue());
                    break;
                case "_menuItemNumeracionDePoderPorMarca":
                    this._ventana.Contenedor.Navigate(new NumeracionDePoderPorMarca());
                    break;
                case "_menuItemNumeracionDePoderPorInteresado":
                    this._ventana.Contenedor.Navigate(new NumeracionDePoderPorInteresado());
                    break;
                case "_menuItemPrioridadExtranjera":
                    this._ventana.Contenedor.Navigate(new PrioridadExtranjera());
                    break;
                case "_menuItemReingresoDeClasificacion":
                    this._ventana.Contenedor.Navigate(new ReingresoDeClasificacion());
                    break;
                case "_menuItemReingresoDeDistingue":
                    this._ventana.Contenedor.Navigate(new ReingresoDeDistingue());
                    break;
                case "_menuItemReingresoDePoderAnexo":
                    this._ventana.Contenedor.Navigate(new ReingresoDePoderAnexo());
                    break;
                case "_menuItemReingresoDeNombreDeMarca":
                    this._ventana.Contenedor.Navigate(new ReingresoDeNombreDeMarca());
                    break;
                case "_menuItemReingresoDePoderDistingueConSinClasificacion":
                    this._ventana.Contenedor.Navigate(new ReingresoDePoderDistingueConSinClasificacion());
                    break;
                case "_menuItemReingresoDePoderPresentado":
                    this._ventana.Contenedor.Navigate(new ReingresoDePoderPresentado());
                    break;
                case "_menuItemReingresoDePoderYReclasificacion":
                    this._ventana.Contenedor.Navigate(new ReingresoDePoderYReclasificacion());
                    break;
                case "_menuItemReingresoDePoderYPrioridad":
                    this._ventana.Contenedor.Navigate(new ReingresoDePoderYPrioridad());
                    break;
                case "_menuItemRenunciaParcial":
                    this._ventana.Contenedor.Navigate(new RenunciaParcial());
                    break;
                case "_menuItemConsignacionDeBusqueda":
                    this._ventana.Contenedor.Navigate(new ConsignacionDeBusqueda());
                    break;
                case "_menuItemNotificacionDeIngresoDePoder":
                    this._ventana.Contenedor.Navigate(new NotificacionDeIngresoDePoder());
                    break;
                case "":
                    //this._ventana.Contenedor.Navigate(new CertificadoDeOrigen());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Método que se encarga de realizar el llamado a las pantallas de los escritos
        /// de patentes
        /// </summary>
        /// <param name="nombreBoton">nombre del boton que creo el evento</param>
        public void IrEscritoPatentes(string nombreBoton)
        {
            switch (nombreBoton)
            {
                case "_menuItemConsignacionDeJuramentoYPoder":
                    this._ventana.Contenedor.Navigate(new ConsignacionDeJuramentoYPoder());
                    break;
                case "_menuItemConsignacionDeJuramento":
                    this._ventana.Contenedor.Navigate(new ConsignacionDeJuramento());
                    break;
                case "_menuItemConsignacionDePoder":
                    this._ventana.Contenedor.Navigate(new ConsignacionDePoder());
                    break;
                case "_menuItemOficioDeJuramentoYPoderSinConsignar":
                    this._ventana.Contenedor.Navigate(new OficioDeJuramentoYPoder());
                    break;
                case "_menuItemOficioDeJuramento":
                    this._ventana.Contenedor.Navigate(new OficioDeJuramento());
                    break;
                case "_menuItemOficioDePoder":
                    this._ventana.Contenedor.Navigate(new OficioDePoder());
                    break;
                case "_menuItemConsignacionDePrioridadExtranjera":
                    this._ventana.Contenedor.Navigate(new ConsignacionDePrioridadExtranjera());
                    break;
                case "_menuItemExamenDePatentabilidad":
                    this._ventana.Contenedor.Navigate(new ExamenDePatentabilidad());
                    break;
                case "_menuItemNumeracionDePoderPorPatente":
                    this._ventana.Contenedor.Navigate(new NumeracionDePoder());
                    break;
                case "_menuItemProrrogaDeFondo":
                    this._ventana.Contenedor.Navigate(new ProrrogaDeFondo());
                    break;
                case "_menuItemProrrogaDeForma":
                    this._ventana.Contenedor.Navigate(new ProrrogaDeForma());
                    break;
                case "_menuItemProrrogaDeContestacionAOposicion":
                    this._ventana.Contenedor.Navigate(new ProrrogaDeContestacionDeOposicion());
                    break;
                case "_menuItemOficioDeFondo":
                    this._ventana.Contenedor.Navigate(new OficioDeFondo());
                    break;
                case "_menuItemCorreccionVoluntaria":
                    this._ventana.Contenedor.Navigate(new CorreccionVoluntaria());
                    break;
                case "_menuItemReconsideracionPerencion":
                    this._ventana.Contenedor.Navigate(new ReconsideracionDePerencion());
                    break;
                case "_menuItemReconsideracionPerimidaVariante":
                    this._ventana.Contenedor.Navigate(new ReconsideracionPerimidaVariante());
                    break;
                case "_menuItemPagoAnualidadPatente":
                    this._ventana.Contenedor.Navigate(new PagoDeAnualidad());
                    break;
                case "_menuItemReconsideracionPrioridadExtinguida":
                    this._ventana.Contenedor.Navigate(new ReconsideracionPrioridadExtinguida());
                    break;
                case "_menuItemContestacionAOposicion":
                    this._ventana.Contenedor.Navigate(new ContestacionAOposicion());
                    break;
                case "_menuItemCorreccionErrorDePublicacionEnPrensa":
                    this._ventana.Contenedor.Navigate(new CorreccionErrorDePublicacionEnPrensa());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Método que coloca la página "Patentes" en el Frame principal
        /// </summary>
        public void ConsultarPatentes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarPatentes());
        }

        /// <summary>
        /// Método que coloca la página "Patentes" en el Frame principal
        /// </summary>
        public void AgregarPatente()
        {
            this._ventana.Contenedor.Navigate(new GestionarPatente(null,null));
        }

        /// <summary>
        /// Método que coloca la página "CesionesPatentes" en el Frame principal
        /// </summary>
        public void ConsultarCesionesPatentes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCesionesPatentes());
        }

        /// <summary>
        /// Método que coloca la página "GestionarCesionPatentes" en el Frame principal
        /// </summary>
        public void AgregarCesionPatentes()
        {
            this._ventana.Contenedor.Navigate(new GestionarCesionPatentes(null));
        }

        /// <summary>
        /// Método que coloca la página "FusionesPatentes" en el Frame principal
        /// </summary>
        public void ConsultarFusionesPatentes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarFusionesPatentes());
        }

        /// <summary>
        /// Método que coloca la página "GestionarFusionPatentes" en el Frame principal
        /// </summary>
        public void AgregarFusionPatentes()
        {
            this._ventana.Contenedor.Navigate(new GestionarFusionPatentes(null));
        }

        /// <summary>
        /// Método que coloca la página "CambiosDeDomicilioPatentes" en el Frame principal
        /// </summary>
        public void ConsultarCambiosDeDomicilioPatentes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCambiosDeDomicilioPatentes());
        }

        /// <summary>
        /// Método que coloca la página "AgregarCambioDeDomicilioPatentes" en el Frame principal
        /// </summary>
        public void AgregarCambioDeDomicilioPatentes()
        {
            this._ventana.Contenedor.Navigate(new GestionarCambioDeDomicilioPatentes(null));
        }

        /// <summary>
        /// Método que coloca la página "CambiosDeNombrePatentes" en el Frame principal
        /// </summary>
        public void ConsultarCambiosDeNombrePatentes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCambiosDeNombrePatentes());
        }

        /// <summary>
        /// Método que coloca la página "AgregarCambioDeNombrePatentes" en el Frame principal
        /// </summary>
        public void AgregarCambioDeNombrePatentes()
        {
            this._ventana.Contenedor.Navigate(new GestionarCambioDeNombrePatentes(null));
        }

        /// <summary>
        /// Método que coloca la página "CambiosDeDomicilioPatentes" en el Frame principal
        /// </summary>
        public void ConsultarCambioPeticionarioPatentes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarCambiosPeticionarioPatentes());
        }

        /// <summary>
        /// Método que coloca la página "AgregarCambioPeticionarioPatentes" en el Frame principal
        /// </summary>
        public void AgregarCambioPeticionarioPatentes()
        {
            this._ventana.Contenedor.Navigate(new GestionarCambioPeticionarioPatentes(null));
        }

        /// <summary>
        /// Método que coloca la página "LicenciasPatentes" en el Frame principal
        /// </summary>
        public void IrConsultarLicenciasPatentes()
        {
            this._ventana.Contenedor.Navigate(new ConsultarLicenciasPatentes());
        }

        /// <summary>
        /// Método que coloca la página "GestionarLicenciasPatentes" en el Frame principal
        /// </summary>
        public void AgregarLicenciaPatentes()
        {
            this._ventana.Contenedor.Navigate(new GestionarLicenciaPatentes(null));
        }

        /// <summary>
        /// Método que coloca la página "ConsultarAnualidades" en el Frame principal
        /// </summary>
        public void ConsultarAnualidades()
        {
            this._ventana.Contenedor.Navigate(new ConsultarAnualidades());
        }
        /// <summary>
        /// Método que coloca la página "Consultar Abandonos Patente" en el Frame principal
        /// </summary>
        public void ConsultarAbandonosPatente()
        {
            this._ventana.Contenedor.Navigate(new ConsultarAbandonosPatente());
        }
        /// <summary>
        /// Método que coloca la página "Gestionar Abandono Patente" en el Frame principal
        /// </summary>
        public void GestionarAbandonoPatente()
        {
            this._ventana.Contenedor.Navigate(new GestionarAbandonoPatente(null));
        }

        /// <summary>
        /// Método que coloca la página "ConsultarAnualidades" en el Frame principal
        /// </summary>
        public void GestionarAnualidades()
        {
            this._ventana.Contenedor.Navigate(new GestionarAnualidades(null));
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
                EliminarSesionesRestantes(ConfigurationManager.AppSettings["NombreProceso"]);
                EliminarSesionesRestantes(ConfigurationManager.AppSettings["NombreProcesoVSHost"]);

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

        private void EliminarSesionesRestantes(string nombreDeImagen)
        {

            IList<Process> procesos = Process.GetProcessesByName(nombreDeImagen);
            int id = Process.GetCurrentProcess().Id;
            //IList<Process> procesos1 = Process.GetProcesses();
            foreach (Process proceso in procesos)
            {
                try
                {
                    if (proceso.Id != id)
                    {
                        this.EjecutarComandoDeConsola("/c " + "Taskkill /PID " + proceso.Id + " /F", "Cerrar sesiones restantes");
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException();
                }
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
                        {
                            switch (itemNivel2.Name)
                            {


                                #region Tablas [LISTO]

                                case "_menuItemAgente":
                                    if (objeto.Id.Equals(Recursos.Ids.Agente))
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
                                case "_menuItemInternacional":
                                    if (objeto.Id.Equals(Recursos.Ids.Internacional))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemNacional":
                                    if (objeto.Id.Equals(Recursos.Ids.Nacional))
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
                                case "_menuItemResolucion":
                                    if (objeto.Id.Equals(Recursos.Ids.Resolucion))
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
                                case "_menuItemUsuario":
                                    if (objeto.Id.Equals(Recursos.Ids.Usuario))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;

                                #endregion


                                #region Traspasos

                                //case "_menuItemCambiosDeDomicilio":
                                //    if (objeto.Id.Equals(Recursos.Ids.ConsultarCambiosDeDomicilio))
                                //        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                //    break;
                                //case "_menuItemCesiones":
                                //    if (objeto.Id.Equals(Recursos.Ids.ConsultarCesiones))
                                //        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                //    break;
                                //case "_menuItemFusiones":
                                //    if (objeto.Id.Equals(Recursos.Ids.ConsultarFusiones))
                                //        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                //    break;
                                //case "_menuItemCambioDeNombre":
                                //    if (objeto.Id.Equals(Recursos.Ids.ConsultarCambiosDeNombre))
                                //        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                //    break;
                                //case "_menuItemCambioPeticionario":
                                //    if (objeto.Id.Equals(Recursos.Ids.ConsultarCambioPeticionarios))
                                //        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                //    break;
                                //case "_menuItemLicencias":
                                //    if (objeto.Id.Equals(Recursos.Ids.ConsultarLicencia))
                                //        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                //    break;
                                //case "_menuItemCambiosPeticionario":
                                //    if (objeto.Id.Equals(Recursos.Ids.ConsultarCambioPeticionarios))
                                //        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                //    break;

                                #endregion


                                #region Correspondencia

                                case "_menuItemCarta":
                                    if (objeto.Id.Equals(Recursos.Ids.ConsultarCartas))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemEntradaAlterna":
                                    if (objeto.Id.Equals(Recursos.Ids.EntradaAlterna))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemTablasCorrespondencia":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemTransferirPlantilla":
                                    if (objeto.Id.Equals(Recursos.Ids.Asociado))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;



                                #endregion


                                #region Patentes

                                case "_menuItemGestionDePatentes":
                                    if (objeto.Id.Equals(Recursos.Ids.AgregarPatentes))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemAnualidadesPatentes":
                                    if (objeto.Id.Equals(Recursos.Ids.ConsultarAnualidad))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemAbandonoPatente":
                                    if (objeto.Id.Equals(Recursos.Ids.ConsultarAbandonosPatente))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemEscritosPatente":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemTraspasosPatente":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemEstadosPatente":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemTipoBasePatente":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;

                                #endregion


                                #region Marcas

                                case "_menuItemGestionDeMarcas":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemRenovaciones":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemRenovacion":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemRecordatorios":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemAbandono":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemEscritos":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemTraspasos":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemMarcasATerceros":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemEstadosMarca":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "_menuItemTipoBase":
                                    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                    break;


                                #endregion


                                #region Extra
                                //case "_menuItemLimitacionDelDistingue":
                                //    if (objeto.Id.Equals(Recursos.Ids.Marca))
                                //        itemNivel2.Visibility = System.Windows.Visibility.Visible;
                                //    break;
                                #endregion
                            }


                            #region Items Internos de Correspondencia
                            ItemCollection menuNivel3 = itemNivel2.Items;
                            foreach (ItemsControl itemNivel3 in menuNivel3)
                                switch (itemNivel3.Name)
                                {
                                    case "_menuItemCategoria":
                                        if (objeto.Id.Equals(Recursos.Ids.Categoria))
                                            itemNivel3.Visibility = System.Windows.Visibility.Visible;
                                        break;
                                    case "_menuItemAnexo":
                                        if (objeto.Id.Equals(Recursos.Ids.Anexo))
                                            itemNivel3.Visibility = System.Windows.Visibility.Visible;
                                        break;
                                    case "_menuItemMedio":
                                        if (objeto.Id.Equals(Recursos.Ids.Medio))
                                            itemNivel3.Visibility = System.Windows.Visibility.Visible;
                                        break;
                                    case "_menuItemRemitente":
                                        if (objeto.Id.Equals(Recursos.Ids.Remitente))
                                            itemNivel3.Visibility = System.Windows.Visibility.Visible;
                                        break;
                                    case "_menuItemResumen":
                                        if (objeto.Id.Equals(Recursos.Ids.Resumen))
                                            itemNivel3.Visibility = System.Windows.Visibility.Visible;
                                        break;

                                }
                            #endregion
                        }
                    }
            }
        }
    }
}
