using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Contactos;
using Trascend.Bolet.Cliente.Ventanas.Justificaciones;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Ventanas.Cartas;

namespace Trascend.Bolet.Cliente.Presentadores.Asociados
{
    class PresentadorListaContactos : PresentadorBase
    {

        private IListaContactos _ventana;
        private Asociado _asociado;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IAsociadoServicios _asociadoServicios;
        private IContactoServicios _contactoServicios;
        private ICartaServicios _cartaServicios;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaContactos(IListaContactos ventana, object asociado, object ventanaPadre)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventanaPadre = ventanaPadre;
            this._ventana = ventana;
            this._asociado = (Asociado)asociado;

            this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);

            this._contactoServicios = (IContactoServicios)Activator.GetObject(typeof(IContactoServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ContactoServicios"]);

            this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaContactos,
                    Recursos.Ids.Contacto);
                IList<ContactosDelAsociadoVista> contactos = this._asociadoServicios.ConsultarContactosDelAsociado(_asociado, true);
                this._ventana.Contactos = contactos;
                this._ventana.TotalHits = contactos.Count.ToString();
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


        /// <summary>
        /// Método que invoca una nueva página "Consultar Justificación" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarContacto()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.ContactoSeleccionado != null)
            {
                //((Contacto)this._ventana.ContactoSeleccionado).Asociado = this._asociado;
                Contacto contactoAConsultar = new Contacto(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).Contacto);
                contactoAConsultar.Asociado = this._asociado;
                Contacto contacto = this._contactoServicios.ConsultarPorId(contactoAConsultar);
                contacto.Asociado = this._asociado;
                this.Navegar(new ConsultarContacto(contacto,this._ventana));
            }
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                this._ventana.ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            this._ventana.ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo que invoca una ventana de Agregar contacto de un Asociado
        /// </summary>
        public void IrAgregarContacto()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //this.Navegar(new AgregarContacto(this._asociado,this._ventanaPadre, false));
            this.Navegar(new AgregarContacto(this._asociado, this._ventana, false));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        public void ConsultarUltimaCorrespondenciaEnviada()
        {
            if ((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado != null)
            {
                if (((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).UltimaCartaSalida != 0)
                {
                    Carta ultimaCorrespondenciaEnviada = this._cartaServicios.ObtenerCartasFiltro(
                        new Carta(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).UltimaCartaSalida))[0];
                    Navegar(new ConsultarCarta(ultimaCorrespondenciaEnviada, this._ventana));
                }
            }
        }


        public void ConsultarCorrespondenciaCreacion()
        {
            if ((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado != null)
            {
                if (((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).CartaCreacion != 0)
                {
                    Carta correspondenciaCreacion = this._cartaServicios.ObtenerCartasFiltro(
                        new Carta(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).CartaCreacion))[0];
                    Navegar(new ConsultarCarta(correspondenciaCreacion, this._ventana));
                }
            }
        }


        public void ConsultarUltimaCorrespondenciaEntrada()
        {
            if ((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado != null)
            {
                if (((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).UltimaCartaEntrada != 0)
                {
                    Carta ultimaCorrespondenciaEntrada = this._cartaServicios.ObtenerCartasFiltro(
                        new Carta(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).UltimaCartaEntrada))[0];
                    Navegar(new ConsultarCarta(ultimaCorrespondenciaEntrada, this._ventana));
                }
            }
        }



        //public void SeleccionarContacto()
        //{
        //    Contacto contactoAConsultar = new Contacto(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).Contacto);
        //    Asociado asociadoAConsultar = new Asociado(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).Asociado);

        //    if (null != ((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado))
        //    {
        //        IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(new Asociado(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).Asociado));
                
        //        //((ConsultarCarta)this._ventanaPadre).SeleccionarContactoYAsociado(asociados[0], this._ventana.ContactoSeleccionado);
        //        ((ConsultarCarta)this._ventanaPadre).SeleccionarContactoYAsociado(asociados[0], contactoAConsultar);
        //        RegresarVentanaPadre();
        //    }
        //}

        /// <summary>
        /// Metodo para registrar el Contacto seleccionado como Contacto CxP
        /// </summary>
        public void IrRegistrarContactoCxP()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.ContactoSeleccionado != null)
                {
                    Contacto contactoARegistrar = new Contacto(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).Contacto);
                    contactoARegistrar.Asociado = this._asociado;
                    Contacto contacto = this._contactoServicios.ConsultarPorId(contactoARegistrar);
                    contacto.Asociado = this._asociado;
                    ContactoCxP contactoCxP = new ContactoCxP();
                    contactoCxP.Asociado = this._asociado;
                    contactoCxP.Id = contacto.Id;

                    //this.Navegar(new ConsultarContacto(contacto, this._ventana));
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }
    }
}
