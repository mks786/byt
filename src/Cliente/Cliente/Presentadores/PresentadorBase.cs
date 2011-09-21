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
        /// Busca la el sexo (género) correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="sexo">Inicial del sexo (género)</param>
        /// <returns>El sexo (género) correspondiente</returns>
        public string BuscarSexo(char sexo)
        {
            string retorno;

            if (Recursos.Etiquetas.cbiMasculino[0].Equals(sexo))
                retorno = Recursos.Etiquetas.cbiMasculino;
            else
                retorno = Recursos.Etiquetas.cbiFemenino;

            return retorno;
        }


        /// <summary>
        /// Busca el tipode persona correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="tipoPersona">Inicial del tipo de persona</param>
        /// <returns>El tipo de persona correspondiente</returns>
        public string BuscarTipoPersona(char tipoPersona)
        {
            string retorno;

            if (Recursos.Etiquetas.cbiJuridica[0].Equals(tipoPersona))
                retorno = Recursos.Etiquetas.cbiJuridica;
            else
                retorno = Recursos.Etiquetas.cbiNatural;

            return retorno;
        }

        /// <summary>
        /// Busca el estado civil correspondiente a la inicial que se le esté pasando
        /// </summary>
        /// <param name="estadoCivil">Inicial del estado civil</param>
        /// <returns>El estado civil correspondiente</returns>
        public string BuscarEstadoCivil(char estadoCivil)
        {
            string retorno;

            if (Recursos.Etiquetas.cbiSoltero[0].Equals(estadoCivil))
                retorno = Recursos.Etiquetas.cbiSoltero;
            else if (Recursos.Etiquetas.cbiCasado[0].Equals(estadoCivil))
                retorno = Recursos.Etiquetas.cbiCasado;
            else if (Recursos.Etiquetas.cbiDivorciado[0].Equals(estadoCivil))
                retorno = Recursos.Etiquetas.cbiDivorciado;
            else
                retorno = Recursos.Etiquetas.cbiViudo;

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

    }
}
