using System.Collections.Generic;
using System.Windows.Controls;
using System.Diagnostics;
using Trascend.Bolet.Cliente.Contratos.Principales;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using System.Configuration;
using Trascend.Bolet.ControlesByT.Ventanas;
using NLog;
using System.Windows.Input;
using System.Net.Sockets;
using System.Runtime.Remoting;

namespace Trascend.Bolet.Cliente.Presentadores
{
    class PresentadorBase
    {
        private static IVentanaPrincipal _ventanaPrincipal = VentanaPrincipal.ObtenerInstancia;
        private static IPaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Usuario _usuarioLogeado;

        private IPlanillaServicios _planillaServicios;
        private IMarcaServicios _marcaServicios;
        private IAgenteServicios _agenteServicios;
        private IInteresadoServicios _interesadoServicios;
        private IPoderServicios _poderServicios;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public PresentadorBase()
        {
            this._planillaServicios = (IPlanillaServicios)Activator.GetObject(typeof(IPlanillaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PlanillaServicios"]);
            this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
            this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
            this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
            this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
            
        }

        /// <summary>
        /// Propiedad que representa el usuario logeado en el sistema
        /// </summary>
        public static Usuario UsuarioLogeado
        {
            get { return _usuarioLogeado; }
            set { _usuarioLogeado = value; }
        }

        /// <summary>
        /// Método que permite navegar hasta la página principal
        /// </summary>
        public void Navegar()
        {
            _paginaPrincipal.MensajeError = "";
            _paginaPrincipal.MensajeUsuario = "";
            _ventanaPrincipal.Contenedor.Navigate(_paginaPrincipal);
        }

        /// <summary>
        /// Método que permite navegar hasta la página principal y 
        /// colocar un mensaje de error en dicha página
        /// </summary>
        /// <param name="mensaje"></param>
        public void Navegar(string mensaje, bool error)
        {
            if (error)
                _paginaPrincipal.MensajeError = mensaje;
            else
                _paginaPrincipal.MensajeUsuario = mensaje;

            _ventanaPrincipal.Contenedor.Navigate(_paginaPrincipal);
        }

        /// <summary>
        /// Método que permite navegar a una página específica
        /// </summary>
        /// <param name="pagina">Página que se quiere mostrar</param>
        public void Navegar(Page pagina)
        {
            _ventanaPrincipal.Contenedor.Navigate(pagina);
        }

        /// <summary>
        /// Método que permite navegar hasta la página principal
        /// </summary>
        public void Cancelar()
        {
            this.Navegar();
        }

        /// <summary>
        /// Método que verifica si se puede regresar a la página anterior,
        /// Si se puede muestra la página anterior, en caso contrario muestra
        /// la página principal
        /// </summary>
        public void Regresar()
        {
            if (_ventanaPrincipal.Contenedor.CanGoBack)
                _ventanaPrincipal.Contenedor.GoBack();
            else
                this.Navegar();
        }

        /// <summary>
        /// Método que actualiza el título de la ventana principal
        /// </summary>
        /// <param name="titulo">Título de la ventana que se está cargando</param>
        /// <param name="id">Id de la ventana que se está cargando</param>
        public void ActualizarTituloVentanaPrincipal(string titulo, string id)
        {
            VentanaPrincipal ventanaPrincipal = VentanaPrincipal.ObtenerInstancia;
            string tituloAColocar = Recursos.Etiquetas.titlePrincipal + " - " + titulo;
            if (!string.IsNullOrEmpty(id))
                tituloAColocar += " (" + id + ")";
            ventanaPrincipal.Title = tituloAColocar;
        }

        /// <summary>
        /// Método que busca un Rol dentro de una lista de roles
        /// </summary>
        /// <param name="roles">Lista de roles</param>
        /// <param name="rolBuscado">rol a buscar</param>
        /// <returns>Rol dentro de la lista</returns>
        public Rol BuscarRol(IList<Rol> roles, Rol rolBuscado)
        {
            Rol retorno = null;

            if (rolBuscado != null)
                foreach (Rol rol in roles)
                {
                    if (rol.Id == rolBuscado.Id)
                    {
                        retorno = rol;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Departamento dentro de una lista de Departamentos
        /// </summary>
        /// <param name="roles">Lista de departamentos</param>
        /// <param name="departamentoBuscado">Departamento a buscar</param>
        /// <returns>Departamento dentro de la lista</returns>
        public Departamento BuscarDepartamento(IList<Departamento> departamentos, Departamento departamentoBuscado)
        {
            Departamento retorno = null;

            if (departamentos != null)
                foreach (Departamento departamento in departamentos)
                {
                    if (departamento.Id == departamentoBuscado.Id)
                    {
                        retorno = departamento;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Departamento dentro de una lista de usuarios
        /// </summary>
        /// <param name="usuarios">Lista de usuarios</param>
        /// <param name="iniciales">Iniciales a buscar</param>
        /// <returns>Usuario dentro de la lista</returns>
        public Usuario BuscarPersonaPorInicial(IList<Usuario> usuarios, string iniciales)
        {
            Usuario retorno = null;

            if (usuarios != null)
                foreach (Usuario usuario in usuarios)
                {
                    if (usuario.
                        Iniciales.Equals(iniciales))
                    {
                        retorno = usuario;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Interesado dentro de una lista de interesados
        /// </summary>
        /// <param name="interesados">Lista de interesados</param>
        /// <param name="interesadoBuscado">Interesado a buscar</param>
        /// <returns>Interesado dentro de la lista</returns>
        public Interesado BuscarInteresado(IList<Interesado> interesados, Interesado interesadoBuscado)
        {
            Interesado retorno = null;

            if (interesadoBuscado != null)
                foreach (Interesado interesado in interesados)
                {
                    if (interesado.Id == interesadoBuscado.Id)
                    {
                        retorno = interesado;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Boletin dentro de una lista de Boletines
        /// </summary>
        /// <param name="boletines">Lista de boletines</param>
        /// <param name="boletinBuscado">Boletin a buscar</param>
        /// <returns>Boletin dentro de la lista</returns>
        public Boletin BuscarBoletin(IList<Boletin> boletines, Boletin boletinBuscado)
        {
            Boletin retorno = null;

            if (boletinBuscado != null)
                foreach (Boletin boletin in boletines)
                {
                    if (boletin.Id == boletinBuscado.Id)
                    {
                        retorno = boletin;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Corresponsal dentro de una lista de Corresponsales
        /// </summary>
        /// <param name="boletines">Lista de Corresponsales</param>
        /// <param name="corresponsalBuscado">Corresponsal a buscar</param>
        /// <returns>Corresponsal dentro de la lista</returns>
        public Corresponsal BuscarCorresponsal(IList<Corresponsal> corresponsales, Corresponsal corresponsalBuscado)
        {
            Corresponsal retorno = null;

            if (corresponsalBuscado != null)
                foreach (Corresponsal corresponsal in corresponsales)
                {
                    if (corresponsal.Id == corresponsalBuscado.Id)
                    {
                        retorno = corresponsal;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Asociado dentro de una lista de Asociados
        /// </summary>
        /// <param name="asociados">Lista de boletines</param>
        /// <param name="asociadoBuscado">Boletin a buscar</param>
        /// <returns>Boletin dentro de la lista</returns>
        public Asociado BuscarAsociado(IList<Asociado> asociados, Asociado asociadoBuscado)
        {
            Asociado retorno = null;

            if (asociadoBuscado != null)
                foreach (Asociado asociado in asociados)
                {
                    if (asociado.Id == asociadoBuscado.Id)
                    {
                        retorno = asociado;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Busca la el sexo (género) correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="sexo">Inicial del sexo (género)</param>
        /// <returns>El sexo (género) correspondiente</returns>
        public ListaDatosValores BuscarSexo(IList<ListaDatosValores> listasDatosValores, ListaDatosValores listaDatosValorBuscado)
        {
            ListaDatosValores retorno = null;

            if (listaDatosValorBuscado != null)
                foreach (ListaDatosValores listaDatosValores in listasDatosValores)
                {
                    if (listaDatosValores.Valor == listaDatosValorBuscado.Valor)
                    {
                        retorno = listaDatosValores;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Busca la la Condición correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="condicion">Inicial de la condicion</param>
        /// <returns>La condición correspondiente</returns>
        public Condicion BuscarCondicion(IList<Condicion> listaCondiciones, Condicion condicionBuscada)
        {
            Condicion retorno = null;

            if (listaCondiciones != null)
                foreach (Condicion condicion in listaCondiciones)
                {
                    if (condicion.Id == condicionBuscada.Id)
                    {
                        retorno = condicion;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Busca la el Tipo renovación correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="listasTipoRenovacion">Lista de tipos de renovación</param>        
        /// <param name="listaDatosValorBuscado">Tipo de renovación buscado</param>
        /// <returns>El tipo de renovación correspondiente</returns>
        public ListaDatosValores BuscarTipoRenovacion(IList<ListaDatosValores> listasTipoRenovacion, ListaDatosValores listaDatosValorBuscado)
        {
            ListaDatosValores retorno = null;

            if (listaDatosValorBuscado != null)
                foreach (ListaDatosValores listaDatosValores in listasTipoRenovacion)
                {
                    if (listaDatosValores.Valor == listaDatosValorBuscado.Valor)
                    {
                        retorno = listaDatosValores;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Busca el recordatorio correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="listaRenovacion">Lista de tipos de recordatorio</param>        
        /// <param name="listaDatosValorBuscado">Tipo de recordatorio buscado</param>
        /// <returns>El recordatorio correspondiente</returns>
        public ListaDatosValores BuscarRecordatorio(IList<ListaDatosValores> listaRecordatorio, ListaDatosValores listaDatosValorBuscado)
        {
            ListaDatosValores retorno = null;

            if (listaDatosValorBuscado != null)
                foreach (ListaDatosValores listaDatosValores in listaRecordatorio)
                {
                    if (listaDatosValores.Valor == listaDatosValorBuscado.Valor)
                    {
                        retorno = listaDatosValores;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Busca el tipode persona correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="tipoPersona">Inicial del tipo de persona</param>
        /// <returns>El tipo de persona correspondiente</returns>
        public ListaDatosDominio BuscarTipoPersona(char tipoBuscado, IList<ListaDatosDominio> tipoPersonas)
        {
            ListaDatosDominio retorno = new ListaDatosDominio();

            foreach (ListaDatosDominio dato in tipoPersonas)
            {
                if (dato.Id[0] == tipoBuscado)
                    retorno = dato;
            }

            return retorno;
        }

        /// <summary>
        /// Busca el tipode persona correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="tipoPersona">Inicial del tipo de persona</param>
        /// <returns>El tipo de persona correspondiente</returns>
        public ListaDatosDominio BuscarTipoBusqueda(char? tipoBuscado, IList<ListaDatosDominio> tipoBusqueda)
        {
            ListaDatosDominio retorno = new ListaDatosDominio();

            foreach (ListaDatosDominio dato in tipoBusqueda)
            {
                if (dato.Id[0] == tipoBuscado)
                    retorno = dato;
            }

            return retorno;
        }

        /// <summary>
        /// Busca el tomo de entre una lista de valores
        /// </summary>
        /// <param name="tomoBuscado">Tomo buscado</param>
        /// <returns>El tomo correspondiente</returns>
        public ListaDatosDominio BuscarTomos(IList<ListaDatosDominio> tomos, string tomoBuscado)
        {
            ListaDatosDominio retorno = new ListaDatosDominio();

            foreach (ListaDatosDominio dato in tomos)
            {
                if (dato.Id.Equals(tomoBuscado))
                    retorno = dato;
            }

            return retorno;
        }

        /// <summary>
        /// Busca el tipo de destinatario correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="tipoDestinatario">Inicial del tipo de Destinatario</param>
        /// <returns>El tipo de destinatario correspondiente</returns>
        public string BuscarTipoDestinatario(char tipoDestinatario)
        {
            string retorno;

            if (Recursos.Etiquetas.cbiPersona[0] == tipoDestinatario)
                retorno = Recursos.Etiquetas.cbiPersona;
            else if (Recursos.Etiquetas.cbiDepartamento[0] == tipoDestinatario)
                retorno = Recursos.Etiquetas.cbiDepartamento;
            else
                retorno = Recursos.Etiquetas.cbiNinguno;

            return retorno;
        }

        /// <summary>
        /// Busca el estado civil correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="estadoCivil">Inicial del estado civil</param>
        /// <returns>El estado civil correspondiente</returns>
        public ListaDatosDominio BuscarEstadoCivil(IList<ListaDatosDominio> listaDatosDominio, ListaDatosDominio listaDatosDominioBuscado)
        {
            ListaDatosDominio retorno = null;

            if (listaDatosDominioBuscado != null)
                foreach (ListaDatosDominio listaDatosDominios in listaDatosDominio)
                {
                    if (listaDatosDominios.Id == listaDatosDominioBuscado.Id)
                    {
                        retorno = listaDatosDominios;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Busca el tipo de remitente correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="tipoRemitente">Inicial del tipo del remintente</param>
        /// <returns>El tipo de remitente correspondiente</returns>
        public string BuscarTipoRemitente(char tipoRemitente)
        {
            string retorno;

            if (Recursos.Etiquetas.cbiProveedor[0].Equals(tipoRemitente))
                retorno = Recursos.Etiquetas.cbiProveedor;
            else if (Recursos.Etiquetas.cbiOtro[0].Equals(tipoRemitente))
                retorno = Recursos.Etiquetas.cbiOtro;
            else
                retorno = Recursos.Etiquetas.cbiNinguno;

            return retorno;
        }

        /// <summary>
        /// Método que busca un Pais dentro de una lista de paises
        /// </summary>
        /// <param name="paises">Lista de paises</param>
        /// <param name="paisBuscado">pais a buscar</param>
        /// <returns>pais dentro de la lista</returns>
        public Pais BuscarPais(IList<Pais> paises, Pais paisBuscado)
        {
            Pais retorno = null;

            if (paisBuscado != null)
                foreach (Pais pais in paises)
                {
                    if (pais.Id == paisBuscado.Id)
                    {
                        retorno = pais;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de estados
        /// </summary>
        /// <param name="estados">Lista de estados</param>
        /// <param name="estadoBuscado">estado a buscar</param>
        /// <returns>Estado dentro de la lista</returns>
        public Estado BuscarEstado(IList<Estado> estados, Estado estadoBuscado)
        {
            Estado retorno = null;

            if (estadoBuscado != null)
                foreach (Estado estado in estados)
                {
                    if (estado.Id.Equals(estadoBuscado.Id))
                    {
                        retorno = estado;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estadoMarca dentro de una lista de estadosMarca
        /// </summary>
        /// <param name="estados">Lista de estadoMarcas</param>
        /// <param name="estadoBuscado">estadoMarca a buscar</param>
        /// <returns>Estado dentro de la lista</returns>
        public EstadoMarca BuscarEstadoMarca(IList<EstadoMarca> estadosMarca, EstadoMarca estadoMarcaBuscado)
        {
            EstadoMarca retorno = null;

            if (estadoMarcaBuscado != null)
                foreach (EstadoMarca estadoMarca in estadosMarca)
                {
                    if (estadoMarca.Id == estadoMarcaBuscado.Id)
                    {
                        retorno = estadoMarca;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un TipoBase dentro de una lista de tipoBase
        /// </summary>
        /// <param name="estados">Lista de tipoBase</param>
        /// <param name="estadoBuscado">tipoBase a buscar</param>
        /// <returns>tipoBase dentro de la lista</returns>
        public TipoBase BuscarTipoBase(IList<TipoBase> tiposBase, TipoBase tipoBaseBuscado)
        {
            TipoBase retorno = null;

            if (tipoBaseBuscado != null)
                foreach (TipoBase tipoBase in tiposBase)
                {
                    if (tipoBase.Id == tipoBaseBuscado.Id)
                    {
                        retorno = tipoBase;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de Idiomas
        /// </summary>
        /// <param name="monedas">Lista de idiomas</param>
        /// <param name="idiomaBuscado">Idioma a buscar</param>
        /// <returns>Idioma dentro de la lista</returns>
        public Idioma BuscarIdioma(IList<Idioma> idiomas, Idioma idiomaBuscado)
        {
            Idioma retorno = null;

            if (idiomaBuscado != null)
                foreach (Idioma idioma in idiomas)
                {
                    if (idioma.Id.Equals(idiomaBuscado.Id))
                    {
                        retorno = idioma;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de remitentes
        /// </summary>
        /// <param name="medios">Lista de Medios</param>
        /// <param name="medioBuscado">Medio a buscar</param>
        /// <returns>Medio dentro de la lista</returns>
        public Medio BuscarMedios(IList<Medio> medios, Medio medioBuscado)
        {
            Medio retorno = null;

            if (medioBuscado != null)
                foreach (Medio remitente in medios)
                {
                    if (remitente.Id.Equals(medioBuscado.Id))
                    {
                        retorno = remitente;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de remitentes
        /// </summary>
        /// <param name="medios">Lista de Medios</param>
        /// <param name="medioBuscado">Medio a buscar</param>
        /// <returns>Medio dentro de la lista</returns>
        public Medio BuscarMedio(IList<Medio> medios, Medio medioBuscado)
        {
            Medio retorno = null;

            if (medioBuscado != null)
                foreach (Medio medio in medios)
                {
                    if (medio.Id.Equals(medioBuscado.Id))
                    {
                        retorno = medio;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Receptor dentro de una lista de receptores
        /// </summary>
        /// <param name="receptores">Lista de receptores</param>
        /// <param name="receptorBuscado">Receptor a buscar</param>
        /// <returns>Receptor dentro de la lista</returns>
        public Usuario BuscarReceptor(IList<Usuario> receptores, string receptorBuscado)
        {
            Usuario retorno = null;

            if (receptorBuscado != null)
                foreach (Usuario receptor in receptores)
                {
                    if (receptor.Iniciales.Equals(receptorBuscado))
                    {
                        retorno = receptor;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Contacto dentro de una lista de contactos
        /// </summary>
        /// <param name="contactos">Lista de contactos</param>
        /// <param name="contactoBuscado">contacto a buscar</param>
        /// <returns>Contacto dentro de la lista</returns>
        public Contacto BuscarContacto(IList<Contacto> contactos, string contactoBuscado)
        {
            Contacto retorno = null;

            if (contactoBuscado != null)
                foreach (Contacto contacto in contactos)
                {
                    if (contacto.Nombre.Equals(contactoBuscado))
                    {
                        retorno = contacto;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Resumen dentro de una lista de Resumenes
        /// </summary>
        /// <param name="resumenes">Lista de Resumenes</param>
        /// <param name="resumenBuscado">Resumen a buscar</param>
        /// <returns>Resumen dentro de la lista</returns>
        public Resumen BuscarResumen(IList<Resumen> resumenes, string resumenBuscado)
        {
            Resumen retorno = null;

            if (resumenBuscado != null)
                foreach (Resumen resumen in resumenes)
                {
                    if (resumen.Id.Equals(resumenBuscado))
                    {
                        retorno = resumen;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un remitente dentro de una lista de remitentes
        /// </summary>
        /// <param name="remitentes">Lista de remitentes</param>
        /// <param name="remitenteBuscado">Remitente a buscar</param>
        /// <returns>Remitente dentro de la lista</returns>
        public Remitente BuscarRemitente(IList<Remitente> remitentes, Remitente remitenteBuscado)
        {
            Remitente retorno = null;

            if (remitenteBuscado != null)
                foreach (Remitente remitente in remitentes)
                {
                    if (remitente.Id.Equals(remitenteBuscado.Id))
                    {
                        retorno = remitente;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de categorias
        /// </summary>
        /// <param name="categorias">Lista de categorias</param>
        /// <param name="categoriaBuscado">Categoria a buscar</param>
        /// <returns>Categoria dentro de la lista</returns>
        public Categoria BuscarCategoria(IList<Categoria> categorias, Categoria categoriaBuscado)
        {
            Categoria retorno = null;

            if (categoriaBuscado != null)
                foreach (Categoria categoria in categorias)
                {
                    if (categoria.Id.Equals(categoriaBuscado.Id))
                    {
                        retorno = categoria;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de Monedas
        /// </summary>
        /// <param name="monedas">Lista de idiomas</param>
        /// <param name="monedaBuscado">Moneda a buscar</param>
        /// <returns>Moneda dentro de la lista</returns>
        public Moneda BuscarMoneda(IList<Moneda> monedas, Moneda monedaBuscado)
        {
            Moneda retorno = null;

            if (monedaBuscado != null)
                foreach (Moneda moneda in monedas)
                {
                    if (moneda.Id.Equals(monedaBuscado.Id))
                    {
                        retorno = moneda;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Metodo que carga 1 comboBox de Minutos y 1 ComboBox de horas
        /// </summary>
        public void CargarComboBoxTiempo(object horas, object minutos)
        {
            ((ComboBox)horas).Items.Add("");
            ((ComboBox)minutos).Items.Add("");

            for (int i = 1; i < 25; i++)
            {
                ((ComboBox)horas).Items.Add(i.ToString());
            }

            for (int i = 0; i < 60; i++)
            {
                ((ComboBox)minutos).Items.Add(i.ToString());
            }
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de TipoClientes
        /// </summary>
        /// <param name="tipoClientes">Lista de TipoClientes</param>
        /// <param name="tipoClienteBuscado">TipoCliente a buscar</param>
        /// <returns>TipoCliente dentro de la lista</returns>
        public TipoCliente BuscarTipoCliente(IList<TipoCliente> tipoClientes, TipoCliente tipoClienteBuscado)
        {
            TipoCliente retorno = null;

            if (tipoClienteBuscado != null)
                foreach (TipoCliente tipoCliente in tipoClientes)
                {
                    if (tipoCliente.Id.Equals(tipoClienteBuscado.Id))
                    {
                        retorno = tipoCliente;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de TipoInfoboles
        /// </summary>
        /// <param name="tipoInfoboles">Lista de TipoInfoboles</param>
        /// <param name="tipoInfobolBuscado">TipoInfobol a buscar</param>
        /// <returns>TipoInfobol dentro de la lista</returns>
        public TipoInfobol BuscarTipoInfobol(IList<TipoInfobol> tipoInfoboles, TipoInfobol tipoInfobolBuscado)
        {
            TipoInfobol retorno = null;

            if (tipoInfobolBuscado != null)
                foreach (TipoInfobol tipoInfobol in tipoInfoboles)
                {
                    if (tipoInfobol.Id.Equals(tipoInfobolBuscado.Id.Trim()))
                    {
                        retorno = tipoInfobol;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de Tarifas
        /// </summary>
        /// <param name="tarifas">Lista de Tarifas</param>
        /// <param name="tarifaBuscado">Tarifa a buscar</param>
        /// <returns>Tarifa dentro de la lista</returns>
        public Tarifa BuscarTarifa(IList<Tarifa> tarifas, Tarifa tarifaBuscado)
        {
            Tarifa retorno = null;

            if (tarifaBuscado != null)
                foreach (Tarifa tarifa in tarifas)
                {
                    if (tarifa.Id.Equals(tarifaBuscado.Id))
                    {
                        retorno = tarifa;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de Etiquetas
        /// </summary>
        /// <param name="etiquetas">Lista de Etiquetas</param>
        /// <param name="etiquetaBuscado">Etiqueta a buscar</param>
        /// <returns>Etiqueta dentro de la lista</returns>
        public Etiqueta BuscarEtiqueta(IList<Etiqueta> etiquetas, Etiqueta etiquetaBuscado)
        {
            Etiqueta retorno = null;

            if (etiquetaBuscado != null)
                foreach (Etiqueta tarifa in etiquetas)
                {
                    if (tarifa.Id.Equals(etiquetaBuscado.Id))
                    {
                        retorno = tarifa;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un estado dentro de una lista de DetallesPagos
        /// </summary>
        /// <param name="detallesPagos">Lista de DetallesPagos</param>
        /// <param name="detallePagoBuscado">DetallePago a buscar</param>
        /// <returns>DetallePago dentro de la lista</returns>
        public DetallePago BuscarDetallePago(IList<DetallePago> detallesPagos, DetallePago detallePagoBuscado)
        {
            DetallePago retorno = null;

            if (detallePagoBuscado != null)
                foreach (DetallePago detallePago in detallesPagos)
                {
                    if (detallePago.Id.Equals(detallePagoBuscado.Id))
                    {
                        retorno = detallePago;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un detalle estado dentro de una lista de Detalles
        /// </summary>
        /// <param name="detalles">Lista de Detalles</param>
        /// <param name="detalleBuscado">Detalle a buscar</param>
        /// <returns>Detalle dentro de la lista</returns>
        public TipoEstado BuscarDetalle(IList<TipoEstado> listaTipoEstados, TipoEstado tipoEstadoBuscado)
        {
            TipoEstado retorno = null;

            if (tipoEstadoBuscado != null)

                foreach (TipoEstado detalle in listaTipoEstados)
                {
                    if (detalle.Id.Equals(tipoEstadoBuscado.Id))
                    {
                        retorno = tipoEstadoBuscado;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Metodo que deshabilita las fechas en un calendario desde el dia 1 hasta el dia pasado por parametro
        /// </summary>
        /// <param name="calendario">Calendario a modificar</param>
        /// <param name="dia">Dia hasta que se deshabilitaran las fechas</param>
        public void DeshabilitarDias(DatePicker calendario, DateTime dia)
        {
            if (calendario.SelectedDate == null || calendario.SelectedDate <= dia)
            {
                calendario.SelectedDate = dia.AddDays(1);
                calendario.Text = string.Empty;
            }

            calendario.BlackoutDates.Add(new CalendarDateRange(new DateTime(0001, 1, 1), dia));
        }

        /// <summary>
        /// Método que busca un concepto dentro de una lista de conceptos
        /// </summary>
        /// <param name="estados">Lista de conceptos</param>
        /// <param name="estadoBuscado">concepto a buscar</param>
        /// <returns>Concepto dentro de la lista</returns>
        public Concepto BuscarConcepto(IList<Concepto> conceptos, Concepto conceptoBuscado)
        {
            Concepto retorno = null;

            if (conceptoBuscado != null)
                foreach (Concepto concepto in conceptos)
                {
                    if (concepto.Id.Equals(conceptoBuscado.Id))
                    {
                        retorno = concepto;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Sector dentro de una lista de Sectores
        /// </summary>
        /// <param name="estados">Lista de Sectores</param>
        /// <param name="estadoBuscado">Sector a buscar</param>
        /// <returns>Sector dentro de la lista</returns>
        public ListaDatosDominio BuscarSector(IList<ListaDatosDominio> sectores, string sectorBuscado)
        {
            ListaDatosDominio retorno = null;

            if (sectorBuscado != null)
                foreach (ListaDatosDominio sector in sectores)
                {
                    if (sector.Id.Equals(sectorBuscado))
                    {
                        retorno = sector;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un tipo de marca dentro de una lista de tipos de marca
        /// </summary>
        /// <param name="tipoMarcas">Lista de tipos de marcas</param>
        /// <param name="tipoMarcaBuscado">Tipo de marca a buscar</param>
        /// <returns>Tipo de marca dentro de la lista</returns>
        public ListaDatosDominio BuscarTipoMarca(IList<ListaDatosDominio> tipoMarcas, string tipoMarcaBuscado)
        {
            ListaDatosDominio retorno = null;

            if (tipoMarcaBuscado != null)
                foreach (ListaDatosDominio tipoMarca in tipoMarcas)
                {
                    if (tipoMarca.Id.Equals(tipoMarcaBuscado))
                    {
                        retorno = tipoMarca;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca Tipo de Reproduccion dentro de una lista de Reproducciones
        /// </summary>
        /// <param name="estados">Lista de Reproducciones</param>
        /// <param name="estadoBuscado">reproduccion a buscar</param>
        /// <returns>Reproduccion dentro de la lista</returns>
        public ListaDatosDominio BuscarTipoReproduccion(IList<ListaDatosDominio> tipoReproducciones, string tipoReproduccionBuscado)
        {
            ListaDatosDominio retorno = null;

            if (tipoReproduccionBuscado != null)
                foreach (ListaDatosDominio tipoReproduccion in tipoReproducciones)
                {
                    if (tipoReproduccion.Id.Equals(tipoReproduccionBuscado))
                    {
                        retorno = tipoReproduccion;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un id en el texto que va en el Combobox
        /// </summary>
        public string BuscarDepartamentoContacto(string departamentoBuscado)
        {
            string retorno = "NGN";
            switch (departamentoBuscado)
            {
                case "LEG":
                    retorno = Recursos.Etiquetas.cbiLegal;
                    break;
                case "MAR":
                    retorno = Recursos.Etiquetas.cbiMarcas;
                    break;
                case "PAT":
                    retorno = Recursos.Etiquetas.cbiPatentes;
                    break;
                case "ADM":
                    retorno = Recursos.Etiquetas.cbiAdministracion;
                    break;
                default:
                    break;
            }

            return retorno;
        }

        /// <summary>
        /// Método que busca un id en el texto que va en el Combobox
        /// </summary>
        public string BuscarFuncionContacto(string funcionBuscada)
        {
            string retorno = "NGN";
            switch (funcionBuscada)
            {
                case "A":
                    retorno = Recursos.Etiquetas.cbiSoloAdministracion;
                    break;
                case "EDO":
                    retorno = Recursos.Etiquetas.cbiSoloEstadoDeCuenta;
                    break;
                case "M":
                    retorno = Recursos.Etiquetas.cbiSoloMarca;
                    break;
                case "P":
                    retorno = Recursos.Etiquetas.cbiSoloPatente;
                    break;
                case "AM":
                    retorno = Recursos.Etiquetas.cbiAdministracionYPatentes;
                    break;
                case "AP":
                    retorno = Recursos.Etiquetas.cbiAdministracionYPatentes;
                    break;
                case "MP":
                    retorno = Recursos.Etiquetas.cbiMarcasYPatentes;
                    break;
                case "AMP":
                    retorno = Recursos.Etiquetas.cbiAdministracionMarcasPatentes;
                    break;
                default:
                    break;
            }

            return retorno;
        }

        /// <summary>
        /// Método que busca un id en el texto que va en el Combobox
        /// </summary>
        public string transformarDepartamento(string departamento)
        {
            string retorno = "";
            switch (departamento)
            {
                case "Legal":
                    retorno = "LEG";
                    break;
                case "Marcas":
                    retorno = "MAR";
                    break;
                case "Patentes":
                    retorno = "PAT";
                    break;
                case "Administración":
                    retorno = "ADM";
                    break;
                default:
                    break;
            }
            return retorno;
        }

        /// <summary>
        /// Método que busca un id en el texto que va en el Combobox
        /// </summary>
        public string transformarFuncion(string funcion)
        {
            string retorno = "";
            switch (funcion)
            {
                case "Sólo Administración":
                    retorno = "A";
                    break;
                case "Sólo Estado De Cuenta":
                    retorno = "EDO";
                    break;
                case "Sólo Marcas":
                    retorno = "M";
                    break;
                case "Sólo Patentes":
                    retorno = "P";
                    break;
                case "Administración y Marcas":
                    retorno = "AM";
                    break;
                case "Administración y Patentes":
                    retorno = "AP";
                    break;
                case "Marcas y Patentes":
                    retorno = "MP";
                    break;
                case "Administración, Marcas y Patentes":
                    retorno = "AMP";
                    break;
                default:
                    break;
            }
            return retorno;
        }

        /// <summary>
        /// Método que verifica el formato
        /// </summary>
        public bool verificarFormato(string formato, string tracking)
        {
            bool trackingCorrecto = true;
            if (formato.Length == tracking.Length)
            {
                for (int i = 0; i < formato.Length; i++)
                {
                    if (formato[i] == '9')
                    {
                        if (!Char.IsNumber(tracking[i]))
                            trackingCorrecto = false;
                    }

                    if (formato[i] == '-')
                    {
                        if (tracking[i] != '-')
                            trackingCorrecto = false;
                    }
                }
            }
            else
            {
                trackingCorrecto = false;
            }

            return trackingCorrecto;
        }

        /// <summary>
        /// Método que busca un Agente dentro de una lista de Agentes
        /// </summary>
        /// <param name="agentes">Lista de Agentes</param>
        /// <param name="agenteBuscado">Agente a buscar</param>
        /// <returns>Agente dentro de la lista</returns>
        public Agente BuscarAgente(IList<Agente> agentes, Agente agenteBuscado)
        {
            Agente retorno = null;

            if (agenteBuscado != null)
                foreach (Agente agente in agentes)
                {
                    if (agente.Id != null)
                    {
                        if (agente.Id.Equals(agenteBuscado.Id))
                        {
                            retorno = agente;
                            break;
                        }
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Poder dentro de una lista de Poderes
        /// </summary>
        /// <param name="poderes">Lista de Poderes</param>
        /// <param name="poderBuscado">Poder a buscar</param>
        /// <returns>Poder dentro de la lista</returns>
        public Poder BuscarPoder(IList<Poder> poderes, Poder poderBuscado)
        {
            Poder retorno = null;

            if (poderBuscado != null)
                foreach (Poder poder in poderes)
                {
                    if (poder.Id.Equals(poderBuscado.Id))
                    {
                        retorno = poder;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Método que busca un Servicio dentro de una lista de Boletines
        /// </summary>
        /// <param name="servicios">Lista de Servicios</param>
        /// <param name="boletinBuscado">Servicio a buscar</param>
        /// <returns>Servicio dentro de la lista</returns>
        public Servicio BuscarServicio(IList<Servicio> servicios, Servicio servicioBuscado)
        {
            Servicio retorno = null;

            if (servicioBuscado != null)
                foreach (Servicio servicio in servicios)
                {
                    if (servicio.Id == servicioBuscado.Id)
                    {
                        retorno = servicio;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Metodo que abre una ventana del explorador predeterminado a una URL determinada
        /// </summary>
        /// <param name="URL">URL dirigida</param>
        public void IrURL(string URL)
        {
            Process.Start(URL);
        }

        /// <summary>
        /// Método que busca un StatusWeb dentro de una lista de StatusWebs
        /// </summary>
        /// <param name="statuses">Lista de StatusWebs</param>
        /// <param name="statusBuscado">StatusWeb a buscar</param>
        /// <returns>StatusWeb dentro de la lista</returns>
        public StatusWeb BuscarStatusWeb(IList<StatusWeb> statuses, StatusWeb statusBuscado)
        {
            StatusWeb retorno = null;

            if (statusBuscado != null)
                foreach (StatusWeb status in statuses)
                {
                    if (status.Id.Equals(statusBuscado.Id))
                    {
                        retorno = status;
                        break;
                    }
                }

            return retorno;
        }

        /// <summary>
        /// Metodo que recibe el nombre del archivo .bat a ejecutar
        /// </summary>
        /// <param name="nombreArchivo">nombre del archivo a ejecutar</param>
        //public void EjecutarArchivoBAT(string nombreArchivoBat, string nombreArchivoLectura)
        //{

        //    try
        //    {
        //        System.Diagnostics.Process proc = new System.Diagnostics.Process(); // Declare New Process
        //        proc.StartInfo.FileName = nombreArchivoBat;
        //        proc.StartInfo.RedirectStandardError = true;
        //        proc.StartInfo.RedirectStandardOutput = true;
        //        proc.StartInfo.UseShellExecute = false;
        //        proc.StartInfo.Arguments = nombreArchivoLectura;

        //        proc.Start();

        //        proc.WaitForExit(2000);

        //        string errorMessage = proc.StandardError.ReadToEnd();
        //        proc.WaitForExit();

        //        string outputMessage = proc.StandardOutput.ReadToEnd();
        //        proc.WaitForExit();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException();
        //    }
        //}

        /// <summary>
        /// Metodo que recibe el nombre del archivo .bat a ejecutar
        /// </summary>
        /// <param name="nombreArchivo">nombre del archivo a ejecutar</param>
        public void EjecutarArchivoBAT(string nombreArchivoBat, string parametroA)
        {

            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process(); // Declare New Process
                proc.StartInfo.FileName = nombreArchivoBat;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.Arguments = parametroA;

                proc.Start();

                proc.WaitForExit(2000);

                string errorMessage = proc.StandardError.ReadToEnd();
                proc.WaitForExit();

                string outputMessage = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                throw new ApplicationException();
            }
        }

        public void LlamarProcedimientoDeBaseDeDatos(ParametroProcedimiento parametro, string tituloVentana)
        {
            try
            {
                Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
                if (planilla != null)
                {
                    Impresion _ventana =
                        new Impresion("Imprimir " + tituloVentana, planilla.Folio.Replace("\n", Environment.NewLine));

                    _ventana.ShowDialog();

                    //Llamado al archivo .bat 
                    //if (_ventana.ClickImprimir)
                    //    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["batPrint"], 
                    //ConfigurationManager.AppSettings["txtPrint"]);

                    parametro.Via = 0;
                    planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Método que arma el string en el formato necesario para llamar a los .BAT de los escritos
        /// </summary>
        /// <param name="listaMarcas">Lista de marcas a traducir</param>
        /// <returns>el string en el formato correcto</returns>
        public string ArmarStringParametroMarcas(IList<Marca> listaMarcas)
        {
            string retorno = "";

            try
            {
                foreach (Marca marca in listaMarcas)
                {
                    retorno = retorno + marca.Id + "_";
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException();
            }
            return retorno.Substring(0, retorno.Length - 1);
        }

        /// <summary>
        /// Método que arma el string en el formato necesario para llamar a los .BAT de los escritos
        /// </summary>
        /// <param name="listaInteresados">Lista de interesados a traducir</param>
        /// <returns>el string en el formato correcto</returns>
        public string ArmarStringParametroInteresados(IList<Interesado> listaInteresados)
        {
            string retorno = "";

            try
            {
                foreach (Interesado interesado in listaInteresados)
                {
                    retorno = retorno + interesado.Id + "_";
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException();
            }
            return retorno.Substring(0, retorno.Length - 1);
        }

        /// <summary>
        /// Método que valida que un agente este en el poder de la lista de marcas
        /// </summary>
        /// <param name="agente">Agente a buscar</param>
        /// <param name="marcas">Marcas con poder</param>
        /// <returns>true en caso de que las marcas posean a ese agente como apoderado, false en caso contrario</returns>
        public bool ValidarAgenteApoderadoDeMarcas(Agente agente, IList<Marca> marcas)
        {
            bool retorno = true;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (Marca marca in marcas)
                {
                    bool validador = false;
                    Marca marcaAux = this._marcaServicios.ConsultarMarcaConTodo(marca);
                    if (marcaAux.Poder != null)
                    {
                        IList<Agente> agentes = this._agenteServicios.ObtenerAgentesDeUnPoder(marcaAux.Poder);

                        foreach (Agente agenteEnPoder in agentes)
                        {
                            if (agenteEnPoder.Id == agente.Id)
                                validador = true;
                        }
                        retorno = retorno && validador;
                    }
                    else
                        return false;
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
            return retorno;
        }

        /// <summary>
        /// Método que valida que un agente tenga poderes relacionados con la lista de interesados
        /// </summary>
        /// <param name="agente">Agente a buscar</param>
        /// <param name="interesados">Interesados con poder</param>
        /// <returns>true en caso de que los interesados posean poderes relacionados con ese agente, false en caso contrario</returns>
        public bool ValidarPoderesAgenteConPoderesInteresados(Agente agente, IList<Interesado> interesados)
        {
            bool retorno = true;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (Interesado interesado in interesados)
                {
                   
                    
                    bool validador = false;                    
                    Interesado interesadosAux = this._interesadoServicios.ConsultarInteresadoConTodo(interesado);

                    interesadosAux.Poderes = this._poderServicios.ConsultarPoderesPorInteresado(interesado);

                    if (interesadosAux.Poderes != null)

                        foreach (Poder poder in interesadosAux.Poderes)
                        {
                            if (poder != null)
                            {
                                IList<Agente> agentes = this._agenteServicios.ObtenerAgentesDeUnPoder(poder);

                                foreach (Agente agenteEnPoder in agentes)
                                {
                                    if (agenteEnPoder.Id == agente.Id)
                                        validador = true;
                                }
                                retorno = retorno && validador;
                            }
                        }                                        
                    else
                        return false;
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
            return retorno;
        }

        /// <summary>
        /// Ejecuta un comando en consola sin abrirla
        /// </summary>
        /// <param name="comando">Comando que se quiere ejecutar en consola</param>
        public void EjecutarComandoDeConsola(string comando, string accion)
        {
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", comando);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            // Do not create the black window.
            procStartInfo.CreateNoWindow = true;
            // Now we create a process, assign its ProcessStartInfo and start it
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            // Get the output into a string
            string result = proc.StandardOutput.ReadToEnd();

            #region Debug
            logger.Debug("Resultado del comando de consola '" + accion + "': " + result);
            #endregion
        }



        /// <summary>
        /// Método que busca Tipo de Clase Nacional dentro de una lista de tipo de clases nacionales
        /// </summary>
        /// <param name="estados">Lista de Tipos de clases nacionales</param>
        /// <param name="estadoBuscado">clase nacional a buscar</param>
        /// <returns>clase nacional dentro de la lista</returns>
        public ListaDatosDominio BuscarClaseNacional(IList<ListaDatosDominio> tiposClaseNacional, string tipoClaseNacionalBuscado)
        {
            ListaDatosDominio retorno = null;

            if (tipoClaseNacionalBuscado != null)
                foreach (ListaDatosDominio tipoReproduccion in tiposClaseNacional)
                {
                    if (tipoReproduccion.Id.Equals(tipoClaseNacionalBuscado))
                    {
                        retorno = tipoReproduccion;
                        break;
                    }
                }

            return retorno;
        }
    }
}