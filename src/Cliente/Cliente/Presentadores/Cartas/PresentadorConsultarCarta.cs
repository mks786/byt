using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using System.Linq;
using Trascend.Bolet.Cliente.Contratos.Cartas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Presentadores.Cartas
{
    class PresentadorConsultarCarta : PresentadorBase
    {
        private IConsultarCarta _ventana;
        private ICartaServicios _cartaServicios;
        private IResumenServicios _resumenServicios;
        private IMedioServicios _medioServicios;
        private IAsociadoServicios _asociadoServicios;
        private IUsuarioServicios _usuarioServicios;
        private IAnexoServicios _anexoServicios;
        private IContactoServicios _contactoServicios;
        private IDepartamentoServicios _departamentoServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IList<Asociado> _asociados;
        private IList<Usuario> _receptores;
        private IList<Medio> _medios;
        private IList<Anexo> _anexos;
        private IList<Anexo> _anexosConfirmacion;
        private IList<Contacto> _personas;
        private IList<Resumen> _resumenes;
        private IList<Departamento> _departamentos;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorConsultarCarta(IConsultarCarta ventana, object carta)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Carta = carta;
                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
                this._resumenServicios = (IResumenServicios)Activator.GetObject(typeof(IResumenServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ResumenServicios"]);
                this._medioServicios = (IMedioServicios)Activator.GetObject(typeof(IMedioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MedioServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._anexoServicios = (IAnexoServicios)Activator.GetObject(typeof(IAnexoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AnexoServicios"]);
                this._contactoServicios = (IContactoServicios)Activator.GetObject(typeof(IContactoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ContactoServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
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
            Carta carta = (Carta)this._ventana.Carta;

            try
            {
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarCarta,
                    Recursos.Ids.AgregarCarta);

                this._medios = this._medioServicios.ConsultarTodos();
                this._ventana.Medios = this._medios;
                Medio medio = new Medio();
                medio.Id = carta.Medio;
                this._ventana.Medio = this.BuscarMedio(this._medios, medio);

                this._receptores = this._usuarioServicios.ConsultarTodos();
                this._ventana.Receptores = this._receptores;
                this._ventana.Receptor = this.BuscarReceptor(this._receptores, carta.Receptor);

                this._anexos = this._anexoServicios.ConsultarTodos();
                Anexo primerAnexo = new Anexo();
                primerAnexo.Id = "NGN";
                this._anexos.Insert(0, primerAnexo);
                this._ventana.Anexos = _anexos;
                this._anexosConfirmacion = this._anexoServicios.ConsultarTodos();
                this._anexosConfirmacion.Insert(0, primerAnexo);
                this._ventana.AnexosConfirmacion = _anexosConfirmacion;

                this._asociados = this._asociadoServicios.ConsultarTodos();
                this._ventana.Asociados = this._asociados;
                this._ventana.Asociado = this.BuscarAsociado(this._asociados, carta.Asociado);
                Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.Asociado);
                asociado.Contactos = this._contactoServicios.ConsultarContactosPorAsociado(asociado);
                this._personas = asociado.Contactos;
                this._ventana.Personas = this._personas;
                this._ventana.Persona = BuscarContacto(this._personas, carta.Persona);
                this._ventana.NombreAsociado = ((Carta)this._ventana.Carta).Asociado.Nombre;

                _departamentos = this._departamentoServicios.ConsultarTodos();
                Departamento primeraTarifa = new Departamento();
                primeraTarifa.Id = "NGN";
                _departamentos.Insert(0, primeraTarifa);
                this._ventana.Departamentos = _departamentos;
                if (null != carta.Departamento)
                    this._ventana.Departamento = this.BuscarDepartamento(this._departamentos, carta.Departamento);

                IList<Medio> mediosTracking = (IList<Medio>)this._ventana.Medios;
                Medio primerosMediosTracking = new Medio();
                primerosMediosTracking.Id = "NGN";
                mediosTracking.Insert(0, primerosMediosTracking);
                this._ventana.MediosTracking = mediosTracking;

                this._ventana.MediosTrackingConfirmacion = mediosTracking;

                this._resumenes = this._resumenServicios.ConsultarTodos();
                Resumen primeraResumen = new Resumen();
                primeraResumen.Id = "NGN";
                _resumenes.Insert(0, primeraResumen);
                this._ventana.Resumenes = this._resumenes;
                this._ventana.Resumen = this.BuscarResumen(this._resumenes, carta.Resumen.Id);

                this._ventana.FocoPredeterminado();
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

        public bool CargarAnexosCarta()
        {
            bool retorno = false;
            Carta carta = (Carta)this._ventana.Carta;
            if ((null != carta.Anexos))
            {
                this._ventana.AnexosCarta = carta.Anexos;
                retorno = true;
            }
            this.LimpiarAnexosCarta(carta);
            return retorno;
        }

        private void LimpiarAnexosCarta(Carta carta)
        {
            IList<int> indices = new List<int>();
            foreach (Anexo anexos in carta.Anexos)
            {
                int index = 0;
                foreach (Anexo anexosTotal in this._anexos)
                {
                    if (anexos.Id == anexosTotal.Id)
                    {
                        indices.Insert(0,index);
                    }
                    index++;
                }
            }

            foreach (int indice in indices)
            {
                this._anexos.RemoveAt(indice);
            }

            this._ventana.Anexos = this._anexos;
        }

        

        public bool CargarAnexosCartaConfirmacion()
        {
            bool retorno = false;
            Carta carta = (Carta)this._ventana.Carta;
            if ((null != carta.AnexosConfirmacion))
            {
                this._ventana.AnexosConfirmacionCarta = carta.AnexosConfirmacion;
                retorno = true;
            }
            this.LimpiarAnexosCartaConfirmacion(carta);
            return retorno;
        }

        private void LimpiarAnexosCartaConfirmacion(Carta carta)
        {
            IList<int> indices = new List<int>();
            foreach (Anexo anexos in carta.AnexosConfirmacion)
            {
                int index = 0;
                foreach (Anexo anexosTotal in this._anexosConfirmacion)
                {
                    if (anexos.Id == anexosTotal.Id)
                    {
                        indices.Insert(0,index);
                    }
                    index++;
                }
            }

            foreach (int indice in indices)
            {
                this._anexosConfirmacion.RemoveAt(indice);
            }

            this._ventana.AnexosConfirmacion = this._anexosConfirmacion;
        }

        public bool verificarFormato()
        {
            bool trackingCorrecto = true;
            if (((Medio)this._ventana.MedioTracking).Formato.Length == ((Carta)this._ventana.Carta).Tracking.Length)
            {
                for (int i = 0; i < ((Medio)this._ventana.MedioTracking).Formato.Length; i++)
                {
                    if (((Medio)this._ventana.MedioTracking).Formato[i] == '9')
                    {
                        if (!Char.IsNumber(((Carta)this._ventana.Carta).Tracking[i]))
                            trackingCorrecto = false;
                    }

                    if (((Medio)this._ventana.MedioTracking).Formato[i] == '-')
                    {
                        if (((Carta)this._ventana.Carta).Tracking[i] != '-')
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
        /// Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        /// </summary>
        public void Modificar()
        {
            if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
            {
                this._ventana.HabilitarCampos = true;
                this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
            }
            else
            {
                try
                {
                    bool tracking = true;

                    if (!String.IsNullOrEmpty(((Carta)this._ventana.Carta).Tracking))
                        tracking = verificarFormato();
                    if (tracking)
                    {
                        Carta carta = (Carta)this._ventana.Carta;
                        if (null != this._ventana.Departamento)
                            carta.Departamento = !((Departamento)this._ventana.Departamento).Id.Equals("NGN") ? (Departamento)this._ventana.Departamento : null;
                        if (null != this._ventana.Asociado)
                            carta.Asociado = !((Asociado)this._ventana.Asociado).Id.Equals("NGN") ? (Asociado)this._ventana.Asociado : null;
                        if (null != this._ventana.Persona)
                            carta.Persona = !((Contacto)this._ventana.Persona).Id.Equals("NGN") ? ((Contacto)this._ventana.Persona).Nombre : null;




                        carta.Medio = ((Medio)this._ventana.Medio).Id;
                        carta.Receptor = ((Usuario)this._ventana.Receptor).Iniciales;

                        carta.Medio = ((Medio)this._ventana.Medio).Id;
                        carta.Receptor = ((Usuario)this._ventana.Receptor).Iniciales;

                        //if (!this._cartaServicios.VerificarExistencia(carta))
                        //{
                        //    bool exitoso = this._cartaServicios.InsertarOModificar(carta, UsuarioLogeado.Hash);

                        //    if (exitoso)
                        //        this.Navegar(Recursos.MensajesConElUsuario.EntradaAlternaInsertado, false);
                        //}
                        //else
                        //{
                        //    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorAgenteRepetido);
                        //}
                    }

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

        public void CambiarAsociado()
        {
            try
            {
                Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.Asociado);
                asociado.Contactos = this._contactoServicios.ConsultarContactosPorAsociado(asociado);
                this._ventana.NombreAsociado = ((Asociado)this._ventana.Asociado).Nombre;
                this._ventana.Personas = asociado.Contactos;
            }
            catch (ApplicationException e)
            {
                this._ventana.Personas = null;
            }
        }

        public void BuscarAsociado()
        {
            IEnumerable<Asociado> asociadosFiltrados = this._asociados;

            if (!string.IsNullOrEmpty(this._ventana.idAsociadoFiltrar))
            {
                asociadosFiltrados = from p in asociadosFiltrados
                                     where p.Id == int.Parse(this._ventana.idAsociadoFiltrar)
                                     select p;
            }

            if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoFiltrar))
            {
                asociadosFiltrados = from p in asociadosFiltrados
                                     where p.Nombre != null &&
                                     p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoFiltrar.ToLower())
                                     select p;
            }

            if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                this._ventana.Asociados = asociadosFiltrados.ToList<Asociado>();
            else
                this._ventana.Asociados = this._asociados;
        }

        public bool AgregarAnexoCarta()
        {
            IList<Anexo> anexosCarta;
            bool retorno = false;
            if ((null != (Anexo)this._ventana.Anexo) && (!((Anexo)this._ventana.Anexo).Id.Equals("NGN")))
            {
                if (null == ((Carta)this._ventana.Carta).Anexos)
                    anexosCarta = new List<Anexo>();
                else
                    anexosCarta = ((Carta)this._ventana.Carta).Anexos;

                anexosCarta.Add((Anexo)this._ventana.Anexo);
                ((Carta)this._ventana.Carta).Anexos = anexosCarta;
                this._ventana.AnexosCarta = anexosCarta.ToList<Anexo>();
                this._anexos.Remove((Anexo)this._ventana.Anexo);
                this._ventana.Anexos = this._anexos.ToList<Anexo>();
                retorno = true;
            }
            return retorno;
        }

        public bool AgregarAnexoCartaConfirmacion()
        {
            IList<Anexo> anexosCarta;
            bool retorno = false;
            if ((null != (Anexo)this._ventana.AnexoConfirmacion) && (!((Anexo)this._ventana.AnexoConfirmacion).Id.Equals("NGN")))
            {
                if (null == ((Carta)this._ventana.Carta).AnexosConfirmacion)
                    anexosCarta = new List<Anexo>();
                else
                    anexosCarta = ((Carta)this._ventana.Carta).AnexosConfirmacion;

                anexosCarta.Add((Anexo)this._ventana.AnexoConfirmacion);
                ((Carta)this._ventana.Carta).AnexosConfirmacion = anexosCarta;
                this._ventana.AnexosConfirmacionCarta = anexosCarta.ToList<Anexo>();
                this._anexosConfirmacion.Remove((Anexo)this._ventana.AnexoConfirmacion);
                this._ventana.AnexosConfirmacion = this._anexosConfirmacion.ToList<Anexo>();
                retorno = true;
            }
            return retorno;
        }

        public bool DeshabilitarAnexosCarta()
        {
            IList<Anexo> anexosCarta;
            bool respuesta = false;

            if (null != ((Anexo)this._ventana.AnexoCarta))
            {
                if (null == ((Carta)this._ventana.Carta).Anexos)
                    anexosCarta = new List<Anexo>();
                else
                    anexosCarta = ((Carta)this._ventana.Carta).Anexos;

                anexosCarta.Remove((Anexo)this._ventana.AnexoCarta);
                ((Carta)this._ventana.Carta).Anexos = anexosCarta;
                this._anexos.Add((Anexo)this._ventana.AnexoCarta);
                this._ventana.AnexosCarta = anexosCarta.ToList<Anexo>();
                this._ventana.Anexos = this._anexos.ToList<Anexo>();

                if (anexosCarta.Count == 0)
                    respuesta = true;

            }
            return respuesta;
        }

        public bool DeshabilitarAnexosCartaConfirmacion()
        {
            IList<Anexo> anexosCarta;
            bool respuesta = false;

            if (null != ((Anexo)this._ventana.AnexoConfirmacionCarta))
            {
                if (null == ((Carta)this._ventana.Carta).AnexosConfirmacion)
                    anexosCarta = new List<Anexo>();
                else
                    anexosCarta = ((Carta)this._ventana.Carta).AnexosConfirmacion;

                anexosCarta.Remove((Anexo)this._ventana.AnexoConfirmacionCarta);
                ((Carta)this._ventana.Carta).AnexosConfirmacion = anexosCarta;
                this._anexosConfirmacion.Add((Anexo)this._ventana.AnexoConfirmacionCarta);
                this._ventana.AnexosConfirmacionCarta = anexosCarta.ToList<Anexo>();
                this._ventana.AnexosConfirmacion = this._anexosConfirmacion.ToList<Anexo>();

                if (anexosCarta.Count == 0)
                    respuesta = true;

            }
            return respuesta;
        }

        public void CarmbiarFormatoTracking()
        {
            this._ventana.FormatoTracking = !((Medio)this._ventana.MedioTracking).Id.Equals("NGN") ? "Formato: " + ((Medio)this._ventana.MedioTracking).Formato : "Formato: ";
        }
    }
}
