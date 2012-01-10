using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Threading;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using System.Collections.Generic;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Documents;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorGestionarInfoBol : PresentadorBase
    {
        private IGestionarInfoBol _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IInfoBolServicios _infoBolServicios;
        private IBoletinServicios _boletinServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private ITipoInfobolServicios _tipoInfobolServicios;
        private IOperacionServicios _operacionServicios;
        private bool _nuevaInfoBol = false;
        private bool _tieneListaCambios = false;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorGestionarInfoBol(IGestionarInfoBol ventana, object infoBol)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.InfoBol = null != (InfoBol)infoBol ? (InfoBol)infoBol : new InfoBol();

                if (((InfoBol)infoBol).Id == int.MinValue)
                    this._nuevaInfoBol = true;

                this._infoBolServicios = (IInfoBolServicios)Activator.GetObject(typeof(IInfoBolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._tipoInfobolServicios = (ITipoInfobolServicios)Activator.GetObject(typeof(ITipoInfobolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoInfobolServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);

                IList<TipoInfobol> infoboles = this._tipoInfobolServicios.ConsultarTodos();
                this._ventana.Tipos = null;
                this._ventana.Tipos = infoboles;
                this._ventana.Tipo = this.BuscarTipoInfobol(infoboles, ((InfoBol)this._ventana.InfoBol).TipoInfobol);

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
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionalInfoBol,
                    Recursos.Ids.AgregarInfoBol);

                if (this._nuevaInfoBol)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                    this._ventana.OculatarControlesAlAgregar();
                }

                IList<ListaDatosDominio> tomos = this._listaDatosDominioServicios.
                ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTomo));
                ListaDatosDominio primerTomo = new ListaDatosDominio();
                primerTomo.Id = "NGN";
                tomos.Insert(0, primerTomo);
                this._ventana.Tomos = null;
                this._ventana.Tomos = tomos;

                if (!this._nuevaInfoBol)
                    this._ventana.Tomo = this.BuscarTomos(tomos, ((InfoBol)this._ventana.InfoBol).Tomo);

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                this._ventana.Boletines = null;
                this._ventana.Boletines = boletines;

                if(!this._nuevaInfoBol)
                    this._ventana.Boletin = this.BuscarBoletin(boletines, ((InfoBol)this._ventana.InfoBol).Boletin);

                this._ventana.BorrarTextoCambio();

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
        public bool Aceptar()
        {
            bool exitoso = false;
            try
            {

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Agregar o modificar datos
                else
                {
                    InfoBol infoBol = (InfoBol)this._ventana.InfoBol;

                    infoBol.Tomo = ((ListaDatosDominio)this._ventana.Tomo).Id;
                    infoBol.Boletin = (Boletin)this._ventana.Boletin;
                    infoBol.TimeStamp = System.DateTime.Now;
                    infoBol.Usuario = UsuarioLogeado;
                    infoBol.Cambio = int.Parse(this._ventana.TextoCambio);

                    if (this._nuevaInfoBol)
                    {
                        infoBol.TipoInfobol = (TipoInfobol)this._ventana.Tipo;
                        ((InfoBol)this._ventana.InfoBol).Marca.InfoBoles.Add(infoBol);
                    }

                    exitoso = this._infoBolServicios.InsertarOModificar(infoBol, UsuarioLogeado.Hash);

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

            return exitoso;
        }

        public bool Eliminar()
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ((InfoBol)this._ventana.InfoBol).Marca.InfoBoles.Remove((InfoBol)this._ventana.InfoBol);

                if (this._infoBolServicios.Eliminar((InfoBol)this._ventana.InfoBol, UsuarioLogeado.Hash))
                {
                    exitoso = true;
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

            return exitoso;
        }

        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column, ListView ListaResultados)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        public void irListaInfoBol()
        {
            this.Navegar(new ListaInfoBoles(((InfoBol)this._ventana.InfoBol).Marca));
        }

        public void CambiarCambio()
        {
            try
            {
                if ((Operacion)this._ventana.Cambio != null)
                {
                    this._ventana.TextoCambio = ((Operacion)this._ventana.Cambio).Interno.ToString();
                }
            }
            catch (ApplicationException e)
            {
                this._ventana.BorrarTextoCambio();
            }
        }

        public bool TieneElementosListaCambio()
        {
            return this._tieneListaCambios;
        }

        public void CargarCambio()
        {
            Operacion operacion = new Operacion();
            operacion.Marca = new Marca(((InfoBol)this._ventana.InfoBol).Marca.Id);

            if (((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.CD") || ((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.CN") ||
                 ((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.FU") || ((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.TP") ||
                 ((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.REN") || ((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.LU"))
            {
                if(((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.TP")){
                    operacion.Servicio = new Servicio("CS");
                }
                else if (((TipoInfobol)this._ventana.Tipo).Id.Equals("RT.REN"))
                {
                    operacion.Servicio = new Servicio("RN");
                }
                else
                {
                    operacion.Servicio = new Servicio(((TipoInfobol)this._ventana.Tipo).Id.Substring(((TipoInfobol)this._ventana.Tipo).Id.Length-2));
                }

                this._ventana.Cambios = null;
                this._ventana.Cambios = this._operacionServicios.ObtenerOperacionPorMarcaYServicio(operacion);

                if (((IList<Operacion>)this._ventana.Cambios).Count > 0)
                    this._tieneListaCambios = true;
                else
                    this._tieneListaCambios = false;

            }
            else
                this._tieneListaCambios = false;
        }
    }
}
