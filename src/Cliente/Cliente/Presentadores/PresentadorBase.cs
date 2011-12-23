using System.Collections.Generic;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Principales;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;

namespace Trascend.Bolet.Cliente.Presentadores
{
    class PresentadorBase
    {
        private static IVentanaPrincipal _ventanaPrincipal = VentanaPrincipal.ObtenerInstancia;
        private static IPaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Usuario _usuarioLogeado;

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
        /// Método que busca un concepto dentro de una lista de conceptos
        /// </summary>
        /// <param name="estados">Lista de conceptos</param>
        /// <param name="estadoBuscado">concepto a buscar</param>
        /// <returns>Concepto dentro de la lista</returns>
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
        /// Método que busca un concepto dentro de una lista de conceptos
        /// </summary>
        /// <param name="estados">Lista de conceptos</param>
        /// <param name="estadoBuscado">concepto a buscar</param>
        /// <returns>Concepto dentro de la lista</returns>
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
        /// Método que busca un concepto dentro de una lista de conceptos
        /// </summary>
        /// <param name="estados">Lista de conceptos</param>
        /// <param name="estadoBuscado">concepto a buscar</param>
        /// <returns>Concepto dentro de la lista</returns>
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
                    if (agente.Id.Equals(agenteBuscado.Id))
                    {
                        retorno = agente;
                        break;
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
    }
}