using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Remitentes;
//using Trascend.Bolet.Cliente.Ventanas.Remitentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Remitentes;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Remitentes
{
    class PresentadorConsultarRemitentes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarRemitentes _ventana;
        private IPaisServicios _paisServicios;
        private IRemitenteServicios _remitenteServicios;
        private IList<Remitente> _remitentes;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarRemitentes(IConsultarRemitentes ventana)
        {
            try
            {
                this._ventana = ventana;
                this._remitenteServicios = (IRemitenteServicios)Activator.GetObject(typeof(IRemitenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RemitenteServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarRemitentes,
            Recursos.Ids.ConsultarRemitente);
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

                this.ActualizarTitulo();

                this._ventana.RemitenteFiltrar = new Remitente();

                this._remitentes = this._remitenteServicios.ConsultarTodos();
                this._ventana.Resultados = this._remitentes;
                this._ventana.FocoPredeterminado();

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                Pais primerPais = new Pais();
                primerPais.Id = int.MinValue;
                paises.Insert(0, primerPais);
                this._ventana.Paises = paises;

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
        }

        /// <summary>
        /// Método que realiza una consulta al servicio, con el fin de filtrar los datos que se muestran 
        /// por pantalla
        /// </summary>
        public void Consultar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IEnumerable<Remitente> remitentesFiltrados = this._remitentes;

                if (!string.IsNullOrEmpty(((Remitente)this._ventana.RemitenteFiltrar).Id))
                {
                    remitentesFiltrados = from r in remitentesFiltrados
                                          where r.Id != null &&
                                          r.Id.ToLower().Contains(((Remitente)this._ventana.RemitenteFiltrar).Id.ToLower())
                                          select r;
                }

                if (!string.IsNullOrEmpty(((Remitente)this._ventana.RemitenteFiltrar).Descripcion))
                {
                    remitentesFiltrados = from r in remitentesFiltrados
                                          where r.Descripcion != null &&
                                          r.Descripcion.ToLower().Contains(((Remitente)this._ventana.RemitenteFiltrar).Descripcion.ToLower())
                                          select r;
                }

                if (this._ventana.TipoRemitente != ' ')
                {
                    remitentesFiltrados = from r in remitentesFiltrados
                                          where r.TipoRemitente == this._ventana.TipoRemitente
                                          select r;
                }

                if (!string.IsNullOrEmpty(((Remitente)this._ventana.RemitenteFiltrar).Direccion))
                {
                    remitentesFiltrados = from r in remitentesFiltrados
                                          where r.Direccion != null &&
                                          r.Direccion.ToLower().Contains(((Remitente)this._ventana.RemitenteFiltrar).Direccion.ToLower())
                                          select r;
                }

                if (!string.IsNullOrEmpty(((Remitente)this._ventana.RemitenteFiltrar).Ciudad))
                {
                    remitentesFiltrados = from r in remitentesFiltrados
                                          where r.Ciudad != null &&
                                          r.Ciudad.ToLower().Contains(((Remitente)this._ventana.RemitenteFiltrar).Ciudad.ToLower())
                                          select r;
                }

                if (!string.IsNullOrEmpty(((Remitente)this._ventana.RemitenteFiltrar).Estado))
                {
                    remitentesFiltrados = from r in remitentesFiltrados
                                          where r.Estado != null &&
                                          r.Estado.ToLower().Contains(((Remitente)this._ventana.RemitenteFiltrar).Estado.ToLower())
                                          select r;
                }

                if (this._ventana.Pais != null && !((Pais)this._ventana.Pais).Id.Equals(int.MinValue))
                {
                    Pais pais = (Pais)this._ventana.Pais;
                    remitentesFiltrados = from r in remitentesFiltrados
                                           where r.Pais != null &&
                                           r.Pais.Id.ToString().ToLower().Contains(pais.Id.ToString().ToLower())
                                           select r;
                }

                if (!string.IsNullOrEmpty(((Remitente)this._ventana.RemitenteFiltrar).Telefono))
                {
                    remitentesFiltrados = from r in remitentesFiltrados
                                          where r.Telefono != null &&
                                          r.Telefono.ToLower().Contains(((Remitente)this._ventana.RemitenteFiltrar).Telefono.ToLower())
                                          select r;
                }

                if (!string.IsNullOrEmpty(((Remitente)this._ventana.RemitenteFiltrar).Fax))
                {
                    remitentesFiltrados = from r in remitentesFiltrados
                                          where r.Fax != null &&
                                          r.Fax.ToLower().Contains(((Remitente)this._ventana.RemitenteFiltrar).Fax.ToLower())
                                          select r;
                }


                this._ventana.Resultados = remitentesFiltrados.ToList<Remitente>();

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
        /// Método que invoca una nueva página "ConsultarRemitente" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarRemitente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.RemitenteSeleccionado != null)
                this.Navegar(new ConsultarRemitente(this._ventana.RemitenteSeleccionado));

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
    }
}
