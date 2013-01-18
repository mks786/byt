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
using Trascend.Bolet.Cliente.Contratos.Cartas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Cartas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Asociados;


namespace Trascend.Bolet.Cliente.Presentadores.Cartas
{
    class PresentadorConsultarContactosPorAsociado : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IConsultarContactosPorAsociado _ventana;
        private IAsociadoServicios _asociadoServicios;
        private IContactoServicios _contactoServicios;
        private IDetallePagoServicios _detallePagoServicios;
        private IEtiquetaServicios _etiquetaServicios;
        private IIdiomaServicios _idiomaServicios;
        private IMonedaServicios _monedaServicios;
        private ITarifaServicios _tarifaServicios;
        private ITipoClienteServicios _tipoClienteServicios;
        private IPaisServicios _paisServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private IDepartamentoServicios _departamentoServicios;


        private Asociado _asociadoPrecargado;


        private Asociado _asociadoAFiltrar;
        private Contacto _contactoAFiltrar;

        private IList<Asociado> _asociados;
        private IList<Contacto> _contactos;


        private int _filtroValido;


        private int _filtroAsociado = 0;
        private int _filtroContacto = 0;
        private bool _contactosBloqueados = false;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarContactosPorAsociado(IConsultarContactosPorAsociado ventana, object ventanaPadre, object asociado)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                _ventanaPadre = ventanaPadre;
                _asociadoPrecargado = (Asociado)asociado;
                this._ventana = ventana;
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._contactoServicios = (IContactoServicios)Activator.GetObject(typeof(IContactoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ContactoServicios"]);
                this._detallePagoServicios = (IDetallePagoServicios)Activator.GetObject(typeof(IDetallePagoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DetallePagoServicios"]);
                this._etiquetaServicios = (IEtiquetaServicios)Activator.GetObject(typeof(IEtiquetaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["EtiquetaServicios"]);
                this._idiomaServicios = (IIdiomaServicios)Activator.GetObject(typeof(IIdiomaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["IdiomaServicios"]);
                this._monedaServicios = (IMonedaServicios)Activator.GetObject(typeof(IMonedaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MonedaServicios"]);
                this._tarifaServicios = (ITarifaServicios)Activator.GetObject(typeof(ITarifaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TarifaServicios"]);
                this._tipoClienteServicios = (ITipoClienteServicios)Activator.GetObject(typeof(ITipoClienteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoClienteServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);

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


        public void ActualizarTitulo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarContactosPorAsociado,
            Recursos.Ids.ConsultarAsociados);

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

                ActualizarTitulo();


                this._ventana.TotalHits = "0";

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                Departamento primerDepartamento = new Departamento();
                primerDepartamento.Id = "NGN";
                departamentos.Insert(0, primerDepartamento);
                this._ventana.Departamentos = departamentos;

                Asociado asociado = new Asociado();
                asociado.Id = int.MinValue;
                _asociados = new List<Asociado>();
                _asociados.Insert(0, asociado);

                this._ventana.Asociados = _asociados;

                this._ventana.FocoPredeterminado();

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
            IList<Contacto> contactos = new List<Contacto>();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._filtroValido = 0;

                CargarDatosFiltro();
                if ((_filtroContacto == 0) && (((Asociado)this._ventana.Asociado == null) || (((Asociado)this._ventana.Asociado).Id == int.MinValue)))
                {
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto, 1);
                }
                else
                {
                    if ((_filtroContacto != 0) && (((Asociado)this._ventana.Asociado == null) || (((Asociado)this._ventana.Asociado).Id == int.MinValue)))
                    {
                        //filtro solo por contacto
                        contactos = this._contactoServicios.ConsultarContactosFiltro(_contactoAFiltrar);
                    }
                    else if ((_filtroContacto == 0) && ((Asociado)this._ventana.Asociado != null) && (((Asociado)this._ventana.Asociado).Id != int.MinValue))
                    {
                        //filtro solo por Asociado
                        contactos = this._contactoServicios.ConsultarContactosPorAsociado((Asociado)this._ventana.Asociado);
                    }
                    else
                    {
                        //filtro por Asociado y contacto
                        _contactoAFiltrar.Asociado = _asociadoAFiltrar;
                        contactos = this._contactoServicios.ConsultarContactosFiltro(_contactoAFiltrar);
                    }

                    this._ventana.Resultados = contactos;
                    this._ventana.TotalHits = contactos.Count.ToString();
                    if (contactos.Count == 0)
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }


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


        private void CargarDatosFiltro()
        {

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            _asociadoAFiltrar = CargarAsociadoDelFiltro();

            _contactoAFiltrar = CargarContactoDelFiltro();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Contacto CargarContactoDelFiltro()
        {

            Contacto contacto = new Contacto();
            _filtroContacto = 0;


            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!string.IsNullOrEmpty(this._ventana.NombreContacto))
                {
                    _filtroContacto = 2;
                    contacto.Nombre = this._ventana.NombreContacto;
                }

                if (!string.IsNullOrEmpty(this._ventana.EmailContacto))
                {
                    _filtroContacto = 2;
                    contacto.Email = this._ventana.EmailContacto;
                }

                if (!string.IsNullOrEmpty(this._ventana.TelefonoContacto))
                {
                    _filtroContacto = 2;
                    contacto.Telefono = this._ventana.TelefonoContacto;
                }

                if (!string.IsNullOrEmpty(this._ventana.FaxContacto))
                {
                    _filtroContacto = 2;
                    contacto.Fax = this._ventana.FaxContacto;
                }

                if (((Departamento)this._ventana.Departamento).Id != "NGN")
                {
                    _filtroContacto = 2;
                    contacto.Departamento = ((Departamento)this._ventana.Departamento).Id;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {

            }

            return contacto;
        }


        private Asociado CargarAsociadoDelFiltro()
        {
            Asociado asociado = new Asociado();
            _filtroAsociado = 0;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!string.IsNullOrEmpty(this._ventana.IdAsociado))
                {
                    _filtroAsociado = 2;
                    asociado.Id = int.Parse(this._ventana.IdAsociado);
                }

                if (!string.IsNullOrEmpty(this._ventana.NombreAsociado))
                {
                    _filtroAsociado = 2;
                    asociado.Nombre = this._ventana.NombreAsociado;
                }

                if (!string.IsNullOrEmpty(this._ventana.WebAsociado))
                {
                    _filtroAsociado = 2;
                    asociado.Web = this._ventana.WebAsociado;
                }

                if (!string.IsNullOrEmpty(this._ventana.DomicilioAsociado))
                {
                    _filtroAsociado = 2;
                    asociado.Domicilio = this._ventana.DomicilioAsociado;
                }

                if (!string.IsNullOrEmpty(this._ventana.EmailAsociado))
                {
                    _filtroAsociado = 2;
                    asociado.Email = this._ventana.EmailAsociado;
                }

                if (!string.IsNullOrEmpty(this._ventana.TelefonoAsociado))
                {
                    _filtroAsociado = 2;
                    asociado.Telefono1 = this._ventana.TelefonoAsociado;
                    asociado.Telefono2 = this._ventana.TelefonoAsociado;
                    asociado.Telefono3 = this._ventana.TelefonoAsociado;
                }

                if (!string.IsNullOrEmpty(this._ventana.FaxAsociado))
                {
                    _filtroAsociado = 2;
                    asociado.Fax1 = this._ventana.FaxAsociado;
                    asociado.Fax2 = this._ventana.FaxAsociado;
                    asociado.Fax3 = this._ventana.FaxAsociado;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {

            }

            return asociado;
        }


        /// <summary>
        /// Método que invoca una nueva página "ConsultarAsociado" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (((Contacto)this._ventana.ContactoSeleccionado) != null)
                if (((Contacto)this._ventana.ContactoSeleccionado).Asociado != null)
                    this.Navegar(new ConsultarAsociado(((Contacto)this._ventana.ContactoSeleccionado).Asociado, this._ventana, true));

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
        /// Método que limpia los campos de búsqueda
        /// </summary>
        public void LimpiarCampos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Navegar(new ConsultarContactosPorAsociado(this._ventanaPadre, this._asociadoPrecargado));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        //public void ConsultarContacto()
        //{

        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
        //            logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //        if (!_contactosBloqueados)
        //        {
        //            CargarDatosFiltro();

        //            if (_filtroContacto == 2)
        //            {
        //                Contacto contacto = new Contacto();
        //                contacto.Id = int.MinValue;
        //                contacto.Asociado = new Asociado();

        //                IList<Contacto> contactosFiltrados = this._contactoServicios.ConsultarContactosFiltro(_contactoAFiltrar);
        //                contactosFiltrados.Insert(0, contacto);

        //                if (contactosFiltrados.Count != 0)
        //                    this._ventana.Contactos = contactosFiltrados.ToList<Contacto>();
        //                else
        //                    this._ventana.Contactos = this._contactos;
        //            }
        //            else
        //                this._ventana.Mensaje(Recursos.MensajesConElUsuario.ContactosBloqueados, 1);
        //        }


        //        #region trace
        //        if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
        //            logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        this.Navegar(ex.Message, true);
        //    }
        //    catch (RemotingException ex)
        //    {
        //        logger.Error(ex.Message);
        //        this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
        //    }
        //    catch (SocketException ex)
        //    {
        //        logger.Error(ex.Message);
        //        this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //        this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
        //    }
        //}


        public void ConsultarAsociado()
        {
            try
            {


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CargarDatosFiltro();

                if (_filtroAsociado == 2)
                {

                    Asociado asociado = new Asociado();
                    asociado.Id = int.MinValue;

                    IList<Asociado> AsociadosFiltrados = this._asociadoServicios.ObtenerAsociadosFiltro(_asociadoAFiltrar);
                    AsociadosFiltrados.Insert(0, asociado);

                    if (AsociadosFiltrados.Count != 0)
                        this._ventana.Asociados = AsociadosFiltrados.ToList<Asociado>();
                    else
                        this._ventana.Asociados = this._asociados;
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
        }


        //public bool CambiarContacto()
        //{
        //    bool retorno = false;

        //    if (this._ventana.Contacto != null)
        //    {
        //        this._ventana.ContactoFiltrar = ((Contacto)this._ventana.Contacto).Nombre;
        //        retorno = true;
        //    }

        //    return retorno;
        //}


        public bool CambiarAsociado()
        {
            bool retorno = false;

            if (this._ventana.Asociado != null)
            {
                this._ventana.AsociadoFiltrar = ((Asociado)this._ventana.Asociado).Nombre;

                retorno = true;
            }

            return retorno;
        }


        public void SeleccionarContacto()
        {
            if (null != ((Contacto)this._ventana.ContactoSeleccionado))
            {
                IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(new Asociado(((Contacto)this._ventana.ContactoSeleccionado).Asociado.Id));
                ((ConsultarCarta)this._ventanaPadre).SeleccionarContactoYAsociado(asociados[0], this._ventana.ContactoSeleccionado);
                RegresarVentanaPadre();
            }
        }
    }
}
