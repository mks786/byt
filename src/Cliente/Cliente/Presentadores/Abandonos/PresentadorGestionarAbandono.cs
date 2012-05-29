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
using Trascend.Bolet.Cliente.Contratos.Abandonos;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Abandonos;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.Abandonos
{
    class PresentadorGestionarAbandono : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public bool _agregar = true;
        private IGestionarAbandono _ventana;

        private IMarcaServicios _marcaServicios;        
        private IAsociadoServicios _asociadoServicios;                
        private IPaisServicios _paisServicios;
        private IInteresadoServicios _interesadoServicios;
        private IServicioServicios _servicioServicios;
        private IOperacionServicios _operacionServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IBoletinServicios _boletinServicios;

        private IList<Asociado> _asociados;        
        private IList<Marca> _marcas;        
        private IList<Interesado> _interesados;               
       
        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarAbandono (IGestionarAbandono ventana, object abandono)
        {
            try
            {

                this._ventana = ventana;

                if (abandono != null)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.Operacion = abandono;
                    _agregar = false;
                }
                else
                {
                    Operacion abandonoAgregar = new Operacion();
                    this._ventana.Operacion = abandonoAgregar;

                    ((Operacion)this._ventana.Operacion).Fecha = DateTime.Now;
                    this._ventana.Marca = null;                            
                    this._ventana.Interesado = null;                                             

                    this._ventana.TextoBotonRegresar = Recursos.Etiquetas.btnCancelar;
                                       
                    this._ventana.ActivarControlesAlAgregar();

                    
                    
                }

                #region Servicios

                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._servicioServicios = (IServicioServicios)Activator.GetObject(typeof(IServicioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ServicioServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);

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
            if (_agregar == true)
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarAbandono,
                Recursos.Ids.GestionarAbandono);
            else
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarAbandono,
                Recursos.Ids.GestionarAbandono);
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

                if (_agregar == false)
                {



                    Operacion operacion = (Operacion)this._ventana.Operacion;

                    if (((Operacion)operacion).Marca != null)
                        this._ventana.Marca = this._marcaServicios.ConsultarMarcaConTodo(((Operacion)operacion).Marca);

                    this._ventana.NombreMarca = ((Marca)this._ventana.Marca).Descripcion;                  

                    CargarMarca();                  

                    CargarInteresado();

                    CargarAsociado();

                    CargaBoletines();

                    this._ventana.Boletin = this.BuscarBoletin((IList<Boletin>)this._ventana.Boletines, operacion.Boletin);
                }
                else
                {
                    CargarMarca();

                    CargarInteresado();

                    CargarAsociado();

                    CargaBoletines();              
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        public bool EsAgregar()
        {
            return _agregar;
        }

        /// <summary>
        /// Método que carga la ventana de Consultar Abandonos
        /// </summary>
        public void IrConsultarAbandonos()
        {
           this.Navegar(new ConsultarAbandonos());
        }

        /// <summary>
        /// Método que carga los datos de Operacion Abandono para agregar en la Base de Datos
        /// </summary>
        public Operacion CargarAbandonoDeLaPantalla()
        {

            Operacion operacion = (Operacion)this._ventana.Operacion;

            operacion.Boletin = (Boletin)this._ventana.Boletin;
            operacion.Aplicada = Char.Parse(this._ventana.Aplicada);

            Servicio servicioAuxiliar = new Servicio("AB");
            operacion.Servicio = servicioAuxiliar;

            if (null != this._ventana.Marca)
            {
                operacion.Marca = ((Marca)this._ventana.Marca).Id != int.MinValue ? (Marca)this._ventana.Marca : null;
                operacion.CodigoAplicada = operacion.Marca.Id;
            }

            if (null != this._ventana.Interesado)
                operacion.Interesado = ((Interesado)this._ventana.Interesado).Id != int.MinValue ? (Interesado)this._ventana.Interesado : null;

            if (null != this._ventana.Asociado)
                operacion.Asociado = ((Asociado)this._ventana.Asociado).Id != int.MinValue ? (Asociado)this._ventana.Asociado : null;

            return operacion;
        }

        /// <summary>
        /// Método que carga los boletines registrados
        /// </summary>
        private void CargaBoletines()
        {
            Boletin primerBoletin = new Boletin(int.MinValue);
            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            boletines.Insert(0, primerBoletin);
            this._ventana.Boletines = boletines;
        }

        /// <summary>
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// Agrega los datos de Operacion Abandono
        /// </summary>
        public void Agregar()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Operacion operacion = CargarAbandonoDeLaPantalla();              
               
                bool exitoso = this._operacionServicios.InsertarOModificar(operacion, UsuarioLogeado.Hash);


                if (exitoso)                               
                    this.Navegar(Recursos.MensajesConElUsuario.AbandonoInsertado, false);                
                else
                    this.Navegar(Recursos.MensajesConElUsuario.AbandonoInsertado, true);
                

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
        /// Metodo que se encarga de eliminar una Abandono
        /// </summary>
        public void Eliminar()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._operacionServicios.Eliminar((Operacion)this._ventana.Operacion, UsuarioLogeado.Hash))
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.AbandonoEliminado;
                    this.Navegar(_paginaPrincipal);
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

        #region Marcas

        /// <summary>
        /// Método que carga los datos iniciales de Marca a mostrar en la página
        /// </summary>
        private void CargarMarca()
        {
            this._marcas = new List<Marca>();
            Marca primeraMarca = new Marca(int.MinValue);
            this._marcas.Add(primeraMarca);

            if ((Marca)this._ventana.Marca != null)
            {
                this._marcas.Add((Marca)this._ventana.Marca);
                this._ventana.MarcasFiltradas = this._marcas;
                this._ventana.MarcaFiltrada = (Marca)this._ventana.Marca;
            }
            else
            {
                this._ventana.MarcasFiltradas = this._marcas;
                this._ventana.MarcaFiltrada = primeraMarca;
            }                           
                 
        }

        /// <summary>
        /// Método que se encarga de consultar una Marca en Base de datos
        /// </summary>
        public void ConsultarMarcas()
        {
 
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Marca primeraMarca = new Marca(int.MinValue);

               
                Marca marca = new Marca();
                IList<Marca> marcasFiltradas;
                marca.Descripcion = this._ventana.NombreMarcaFiltrar.ToUpper();
                marca.Id = this._ventana.IdMarcaFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdMarcaFiltrar);

                if ((!marca.Descripcion.Equals("")) || (marca.Id != 0))
                    marcasFiltradas = this._marcaServicios.ObtenerMarcasFiltro(marca);
                else
                    marcasFiltradas = new List<Marca>();

                if (marcasFiltradas.ToList<Marca>().Count != 0)
                {
                    marcasFiltradas.Insert(0, primeraMarca);
                    this._ventana.MarcasFiltradas = marcasFiltradas.ToList<Marca>();
                    this._ventana.MarcaFiltrada = primeraMarca;
                }
                else
                {
                    marcasFiltradas.Insert(0, primeraMarca);
                    this._ventana.MarcasFiltradas = this._marcas;
                    this._ventana.MarcaFiltrada = primeraMarca;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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
        /// Método que se encarga en cambiar la Marca seleccionada en la lista filtrada
        /// </summary>
        /// <returns>True si se cambió correctamente, false en caso contrario</returns>
        public bool CambiarMarca()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.MarcaFiltrada != null)
                {
                    this._ventana.Marca = this._ventana.MarcaFiltrada;
                    this._ventana.NombreMarca = ((Marca)this._ventana.MarcaFiltrada).Descripcion;
                    this._marcas.RemoveAt(0);
                    this._marcas.Add((Marca)this._ventana.MarcaFiltrada);

                    this._ventana.ConvertirEnteroMinimoABlanco();

                    retorno = true;
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

        #endregion       

        #region Interesado

        /// <summary>
        /// Método que carga los datos iniciales de Interesado a mostrar en la página
        /// </summary>
        private void CargarInteresado()
        {
            Interesado primerInteresado = new Interesado(int.MinValue);

            this._interesados = new List<Interesado>();

            this._interesados.Add(primerInteresado);

            if (((Operacion)this._ventana.Operacion).Interesado != null)
            {
                this._ventana.Interesado = this._interesadoServicios.ConsultarInteresadoConTodo(((Operacion)this._ventana.Operacion).Interesado);
                this._ventana.NombreInteresado = ((Interesado)this._ventana.Interesado).Nombre;

                if ((Interesado)this._ventana.Interesado != null)
                {
                    this._interesados.Add((Interesado)this._ventana.Interesado);
                    this._ventana.InteresadosFiltrados = this._interesados;
                    this._ventana.InteresadoFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.InteresadosFiltrados, (Interesado)this._ventana.Interesado);
                }
            }
            else
            {
                if (primerInteresado.Id != int.MinValue)
                    this._ventana.Interesado = primerInteresado;
                else
                    this._ventana.IdInteresado = "";
                
                this._ventana.InteresadosFiltrados = this._interesados;
                this._ventana.InteresadoFiltrado = primerInteresado;

            }
        }             

        /// <summary>
        /// Método que se encarga de consultar un Intesesado en Base de datos
        /// </summary>
        public void ConsultarInteresados()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado primerInteresado = new Interesado(int.MinValue);
                
                Interesado interesado = new Interesado();
                IList<Interesado> interesadosFiltrados;
                interesado.Nombre = this._ventana.NombreInteresadoFiltrar.ToUpper();
                interesado.Id = this._ventana.IdInteresadoFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdInteresadoFiltrar);

                if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                else
                    interesadosFiltrados = new List<Interesado>();

                if (interesadosFiltrados.Count != 0)
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.InteresadosFiltrados = interesadosFiltrados;
                    this._ventana.InteresadoFiltrado = primerInteresado;
                }
                else
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.InteresadosFiltrados = this._interesados;
                    this._ventana.InteresadoFiltrado = primerInteresado;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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
        /// Método que se encarga en cambiar el Interesado seleccionada en la lista filtrada
        /// </summary>
        /// <returns>True si se cambió correctamente, false en caso contrario</returns>
        public bool CambiarInteresado()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.InteresadoFiltrado != null)
                {
                    this._ventana.Interesado = this._ventana.InteresadoFiltrado;
                    this._ventana.NombreInteresado = ((Interesado)this._ventana.InteresadoFiltrado).Nombre;
                    this._interesados.RemoveAt(0);
                    this._interesados.Add((Interesado)this._ventana.InteresadoFiltrado);

                    this._ventana.ConvertirEnteroMinimoABlanco();

                    retorno = true;
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
        /// Método que se encarga de limpiar las lista de Interesados
        /// </summary>
        public void LimpiarListaInteresado()
        {
            Interesado primerInteresado = new Interesado(int.MinValue);
            IList<Interesado> listaInteresados = new List<Interesado>();
            listaInteresados.Add(primerInteresado);

            this._ventana.InteresadosFiltrados = listaInteresados;
            this._ventana.InteresadoFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
            this._ventana.Interesado = this._ventana.InteresadoFiltrado;
        }

        #endregion       

        #region Asociado

        /// <summary>
        /// Método que se encarga de consultar un Asociado en Base de datos
        /// </summary>
        public void ConsultarAsociados()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Asociado primeraAsociado = new Asociado(int.MinValue);


                Asociado asociado = new Asociado();
                IList<Asociado> asociadosFiltrados;

                asociado.Nombre = this._ventana.NombreAsociadoFiltrar.ToUpper();
                asociado.Id = this._ventana.IdAsociadoFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdAsociadoFiltrar);

                if ((!asociado.Nombre.Equals("")) || (asociado.Id != 0))
                    asociadosFiltrados = this._asociadoServicios.ObtenerAsociadosFiltro(asociado);
                else
                    asociadosFiltrados = new List<Asociado>();

                if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                {
                    asociadosFiltrados.Insert(0, primeraAsociado);
                    this._ventana.AsociadosFiltrados = asociadosFiltrados.ToList<Asociado>();
                    this._ventana.AsociadoFiltrado = primeraAsociado;
                }
                else
                {
                    asociadosFiltrados.Insert(0, primeraAsociado);
                    this._ventana.AsociadosFiltrados = this._asociados;
                    this._ventana.AsociadoFiltrado = primeraAsociado;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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
        /// Método que carga los datos iniciales de Asociado a mostrar en la página
        /// </summary>
        private void CargarAsociado()
        {
            this._asociados = new List<Asociado>();
            Asociado primerAsociado = new Asociado(int.MinValue);
            this._asociados.Add(primerAsociado);

            if ( ((Operacion)this._ventana.Operacion).Asociado != null)
            {
                this._asociados.Add(((Operacion)this._ventana.Operacion).Asociado);
                this._ventana.AsociadosFiltrados = this._asociados;                
                this._ventana.AsociadoFiltrado = ((Operacion)this._ventana.Operacion).Asociado;
                this._ventana.NombreAsociado = ((Operacion)this._ventana.Operacion).Asociado.Nombre;
            }
            else
            {
                this._ventana.AsociadosFiltrados = this._asociados;
                this._ventana.AsociadoFiltrado = primerAsociado;
            }

        }

        /// <summary>
        /// Método que se encarga en cambiar el Asociado seleccionada en la lista filtrada
        /// </summary>
        /// <returns>True si se cambió correctamente, false en caso contrario</returns>
        public bool CambiarAsociado()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.AsociadoFiltrado != null)
                {
                    this._ventana.Asociado = this._ventana.AsociadoFiltrado;
                    this._ventana.NombreAsociado = ((Asociado)this._ventana.AsociadoFiltrado).Nombre;
                    this._asociados.RemoveAt(0);
                    this._asociados.Add((Asociado)this._ventana.AsociadoFiltrado);

                    retorno = true;
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

        #endregion

    }
}
