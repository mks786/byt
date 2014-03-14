using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Cartas;
using Trascend.Bolet.Cliente.Ventanas.Contactos;
using Trascend.Bolet.Cliente.Ventanas.ContactosCxP;
using Trascend.Bolet.Cliente.Ventanas.Justificaciones;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Asociados
{
    class PresentadorListaContactosCxC : PresentadorBase
    {
        private IListaContactosCxC _ventana;
        private Asociado _asociado;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IAsociadoServicios _asociadoServicios;
        private IContactoServicios _contactoServicios;
        private IContactoCxPServicios _contactoCxPServicios;
        //private ICartaServicios _cartaServicios;
        private IList<ContactosDelAsociadoVista> _contactosVistaAsociado;

        public PresentadorListaContactosCxC(IListaContactosCxC ventana, object asociado, object contactosAsociado, object ventanaPadre)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventanaPadre = ventanaPadre;
            this._ventana = ventana;
            this._asociado = (Asociado)asociado;
            this._contactosVistaAsociado = (IList<ContactosDelAsociadoVista>)contactosAsociado;

            this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);

            this._contactoServicios = (IContactoServicios)Activator.GetObject(typeof(IContactoServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ContactoServicios"]);

            this._contactoCxPServicios = (IContactoCxPServicios)Activator.GetObject(typeof(IContactoCxPServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ContactoCxPServicios"]);

            //this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
            //    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaContactosCxP,
                    Recursos.Ids.ContactoCxP);

                IList<ContactoCxP> _contactosCxC = this._asociadoServicios.ConsultarContactosCxPAsociado(this._asociado);
                if (_contactosCxC.Count > 0)
                {
                    foreach (ContactoCxP contacto in _contactosCxC)
                    {
                        String idContactoVista = contacto.Asociado.Id.ToString() + "-" + contacto.Id.ToString();
                        ContactosDelAsociadoVista contactoVista = this.BuscarContactoAsociadoVista(this._contactosVistaAsociado, idContactoVista);
                        contacto.ContactoAsociadoVista = contactoVista;
                        Contacto contact = new Contacto();
                        contact.Asociado = this._asociado;
                        contact.Id = contacto.Id;
                        IList<Contacto> contacts = this._contactoServicios.ConsultarContactosFiltro(contact);
                        contacto.ContactoAsociado = contacts[0];
                        switch (contacto.ModoPago)
                        {
                            case "C":
                                contacto.ModoPagoDescripcion = "Cheque";
                                break;
                            case "T":
                                contacto.ModoPagoDescripcion = "Transferencia";
                                break;
                            case "D":
                                contacto.ModoPagoDescripcion = "Depósito";
                                break;

                        }
                    }


                    this._ventana.ContactosCxC = _contactosCxC;
                    this._ventana.TotalHits = _contactosCxC.Count.ToString();
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
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

        public void VerContactoCxC()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.ContactoCxC != null)
                {
                    ContactoCxP contacto = (ContactoCxP)this._ventana.ContactoCxC;
                    //this.Navegar(new AgregarContactoCxP(contacto,this._ventana,false));
                    this.Navegar(new AgregarContactoCxP(contacto,this._contactosVistaAsociado,this._ventana,this._ventanaPadre,false));
                }
                else
                    this._ventana.Mensaje("Selecccione un Contacto para poder modificar sus datos asociados", 0);

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
