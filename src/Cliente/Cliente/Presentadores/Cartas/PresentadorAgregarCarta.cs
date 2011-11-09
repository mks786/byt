using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Cartas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Presentadores.Cartas
{
    class PresentadorAgregarCarta : PresentadorBase
    {
        private IAgregarCarta _ventana;
        private ICartaServicios _cartaServicios;
        private IMedioServicios _medioServicios;
        private IAsociadoServicios _asociadoServicios;
        private IUsuarioServicios _usuarioServicios;
        private IAnexoServicios _anexoServicios;
        private IContactoServicios _contactoServicios;
        private IDepartamentoServicios _departamentoServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarCarta(IAgregarCarta ventana)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Carta = new Carta();
                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
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

            try
            {
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarEntradaAlterna,
                    Recursos.Ids.AgregarEntradaAlterna);

                this._ventana.Medios = this._medioServicios.ConsultarTodos();

                this._ventana.Receptores = this._usuarioServicios.ConsultarTodos();

                IList<Anexo> anexos = this._anexoServicios.ConsultarTodos();
                Anexo primerAnexo = new Anexo();
                primerAnexo.Id = "NGN";
                anexos.Insert(0, primerAnexo);
                this._ventana.Anexos = anexos;
                this._ventana.AnexosConfirmacion = anexos;

                IList<Contacto> contactos = this._contactoServicios.ConsultarTodos();
                Contacto primerContacto = new Contacto();
                primerContacto.Id = int.MinValue;
                contactos.Insert(0, primerContacto);
                this._ventana.Personas = contactos;

                this._ventana.Personas = this._ventana.Receptores;

                this._ventana.Asociados = this._asociadoServicios.ConsultarTodos();

                this._ventana.Departamentos = this._departamentoServicios.ConsultarTodos();

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

        /// <summary>
        /// Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                //EntradaAlterna entradaAlterna = (EntradaAlterna)this._ventana.EntradaAlterna;

                //entradaAlterna.Medio = (Medio)this._ventana.Medio;
                //entradaAlterna.Receptor = ((Usuario)this._ventana.Receptor).Iniciales;
                //entradaAlterna.Remitente = !((Remitente)this._ventana.Remitente).Id.Equals("NGN") ? (Remitente)this._ventana.Remitente : null;
                //entradaAlterna.Categoria = !((Categoria)this._ventana.Categoria).Id.Equals("NGN") ? (Categoria)this._ventana.Categoria : null;
                //entradaAlterna.TipoDestinatario = this._ventana.TipoDestinatario;

                //if (!this._ventana.Hora.Equals(""))
                //    entradaAlterna.Hora = new DateTime(entradaAlterna.Fecha.Value.Year, entradaAlterna.Fecha.Value.Month, entradaAlterna.Fecha.Value.Day,
                //        !this._ventana.Hora.Equals(" ") ? int.Parse((string)this._ventana.Hora) : 0,
                //        !this._ventana.Minuto.Equals(" ") ? int.Parse((string)this._ventana.Minuto) : 0, 0);

                //if (entradaAlterna.TipoDestinatario != ' ')
                //{
                //    entradaAlterna.CodigoDestinatartio = entradaAlterna.TipoDestinatario == 'D' ? ((Departamento)_ventana.Departamento).Id : ((Usuario)_ventana.Persona).Iniciales;
                //    entradaAlterna.DescripcionDestinatario = entradaAlterna.TipoDestinatario == 'D' ? ((Departamento)_ventana.Departamento).Descripcion : ((Usuario)_ventana.Persona).NombreCompleto;
                //}

                //if (!this._cartaServicios.VerificarExistencia(entradaAlterna))
                //{
                //    bool exitoso = this._cartaServicios.InsertarOModificar(entradaAlterna, UsuarioLogeado.Hash);

                //    if (exitoso)
                //        this.Navegar(Recursos.MensajesConElUsuario.EntradaAlternaInsertado, false);
                //}
                //else
                //{
                //    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorAgenteRepetido);
                //}

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
