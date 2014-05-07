using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.SAPI.Materiales;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.SAPI.Materiales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.SAPI.Materiales
{
    class PresentadorGestionarCompraMaterialesSapi : PresentadorBase 
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IGestionarCompraMaterialesSapi _ventana;
        private IMaterialSapiServicios _materialSapiServicios;
        private ICompraSapiServicios _compraSapiServicios;
        private ICompraSapiDetalleServicios _compraSapiDetalleServicios;
        private CompraSapi _compra;
        private IList<CompraSapiDetalle> _detalleCompra;
        private bool _agregar = false;
        private String _patronArchivoCompraPDF = String.Empty;

        /// <summary>
        /// Constructor predeterminado que recibe una Compra Sapi y una Ventana Padre
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="compraSapi">Compra Sapi</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorGestionarCompraMaterialesSapi(IGestionarCompraMaterialesSapi ventana, object compraSapi, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                if (ventanaPadre != null)
                    this._ventanaPadre = ventanaPadre;

                if (compraSapi == null)
                {
                    this._agregar = true;
                    CompraSapi compra = new CompraSapi();
                    this._ventana.CompraSapi = compra;
                }
                else
                {
                    this._ventana.CompraSapi = (CompraSapi)compraSapi;
                }
                

                this._materialSapiServicios = (IMaterialSapiServicios)Activator.GetObject(typeof(IMaterialSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MaterialSapiServicios"]);
                this._compraSapiServicios = (ICompraSapiServicios)Activator.GetObject(typeof(ICompraSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CompraSapiServicios"]);
                this._compraSapiDetalleServicios = (ICompraSapiDetalleServicios)Activator.GetObject(typeof(ICompraSapiDetalleServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CompraSapiDetalleServicios"]);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarCompraMaterialSapi,
                Recursos.Ids.GestionarCompraMaterialSAPI);
        }


        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();

                LlenarComboMateriales();

                this._ventana.ActivarCampoCantidad(false);
                this._ventana.ActivarBotonesIncluirYBorrar(false);
                
                if (!this._agregar)
                {
                    CompraSapi compra = (CompraSapi)this._ventana.CompraSapi;

                    CompraSapiDetalle detalleCompraAux = new CompraSapiDetalle();
                    detalleCompraAux.Compra = compra;
                    IList<CompraSapiDetalle> renglonesDeDetalle = 
                        this._compraSapiDetalleServicios.ObtenerCompraSapiDetalleFiltro(detalleCompraAux);
                    ((CompraSapi)this._ventana.CompraSapi).DetalleCompra = renglonesDeDetalle;
                    this._ventana.DetallesDeCompraSapi = ((CompraSapi)this._ventana.CompraSapi).DetalleCompra;

                    this._ventana.OcultarBotonesAlConsultar();

                    _patronArchivoCompraPDF = ObtenerCadenaArchivoPDF(compra);

                    String ruta = ConfigurationManager.AppSettings["RutaFacturaCompraSAPI"].ToString() + _patronArchivoCompraPDF + "." + compra.Id.ToString() + ".pdf";
                    if (File.Exists(ruta))
                        this._ventana.PintarBotonVerFacturaSAPI();

                }
                else
                {
                    this._ventana.IdCompraSapi = String.Empty;
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }

        }

        /// <summary>
        /// Metodo que obtiene la cadena a concatenar en el patron de nombre de archivo PDF de las Compras SAPI
        /// </summary>
        /// <param name="compra">Compra de Materiales de la pantalla</param>
        /// <returns>Cadena con el patron para el archivo PDF de la Compra SAPI</returns>
        private string ObtenerCadenaArchivoPDF(CompraSapi compra)
        {
            string fecha = string.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                fecha = String.Format("{0:yyyyMMdd}", compra.FechaCompra);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return fecha;
        }

        /// <summary>
        /// Metodo que llena el combo de materiales sapi de una compra de materiales
        /// </summary>
        private void LlenarComboMateriales()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<MaterialSapi> materiales = this._materialSapiServicios.ConsultarTodos();
                MaterialSapi primerMaterial = new MaterialSapi("NGN");
                materiales.Insert(0, primerMaterial);
                this._ventana.Materiales = materiales;


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo que agregar un nuevo Renglon de Detalle para la Compra de Materiales Sapi
        /// </summary>
        public void AgregarRenglonDeDetalleCompraSapi()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<CompraSapiDetalle> renglonesDetalle = new List<CompraSapiDetalle>();
                CompraSapiDetalle renglonDetalle;
                CompraSapi compraActual = (CompraSapi)this._ventana.CompraSapi;

                if (compraActual.Id != 0)
                {
                    if ((null != (MaterialSapi)this._ventana.Material) && (!((MaterialSapi)this._ventana.Material).Id.Equals("NGN")))
                    {
                        if ((!this._ventana.CantidadMaterial.Equals("")) &&
                            (!this._ventana.CantidadMaterial.Equals("0")) &&
                            (!this._ventana.CantidadMaterial.Equals(String.Empty)))
                        {
                            if (null == ((CompraSapi)this._ventana.CompraSapi).DetalleCompra)
                                renglonesDetalle = new List<CompraSapiDetalle>();
                            else
                                renglonesDetalle = ((CompraSapi)this._ventana.CompraSapi).DetalleCompra;



                            renglonDetalle = new CompraSapiDetalle();
                            renglonDetalle.Compra = (CompraSapi)this._ventana.CompraSapi;
                            renglonDetalle.Material = (MaterialSapi)this._ventana.Material;
                            renglonDetalle.Cantidad = int.Parse(this._ventana.CantidadMaterial);
                            renglonDetalle.PUnit = ((MaterialSapi)this._ventana.Material).Precio;
                            renglonDetalle.Total = renglonDetalle.Cantidad * renglonDetalle.PUnit.Value;

                            if (!this._agregar)
                            {
                                AjustarExistenciaMaterial(renglonDetalle, true);
                                bool exitoso = this._compraSapiDetalleServicios.InsertarOModificar(renglonDetalle, UsuarioLogeado.Hash);
                                this._ventana.Mensaje("Presione el botón Totalizar Compra", 1);
                            }

                            renglonesDetalle.Add(renglonDetalle);
                            ((CompraSapi)this._ventana.CompraSapi).DetalleCompra = renglonesDetalle;
                            this._ventana.DetallesDeCompraSapi = null;
                            this._ventana.DetallesDeCompraSapi = ((CompraSapi)this._ventana.CompraSapi).DetalleCompra;
                            this._ventana.SeleccionarPrimerItem();

                        }
                        else
                        {
                            this._ventana.Mensaje("Indique la cantidad de Material a registra en la Compra", 0);
                            this._ventana.CantidadMaterial = String.Empty;
                        }
                    }
                    else
                    {
                        this._ventana.Mensaje("Debe seleccionar un Material para poder incluir el Detalle", 0);
                    }

                    this._ventana.CantidadMaterial = String.Empty;

                }
                else
                {
                    this._ventana.Mensaje("Agregue el Código de la Compra", 0);
                    this._ventana.CantidadMaterial = String.Empty;
                    this._ventana.ActivarCampoCantidad(false);
                    this._ventana.ActivarBotonesIncluirYBorrar(false);
                }

                

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }


        /// <summary>
        /// Metodo que quita un renglon de detalle de la compra sapi
        /// </summary>
        public void QuitarRenglonDeDetalleCompraSapi()
        {

            IList<CompraSapiDetalle> renglonesDetalle = new List<CompraSapiDetalle>();
            CompraSapi compraActual = (CompraSapi)this._ventana.CompraSapi;
            CompraSapiDetalle detalleQuitar;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.DetalleDeCompraSapi != null)
                {
                    if (null == ((CompraSapi)this._ventana.CompraSapi).DetalleCompra)
                        renglonesDetalle = new List<CompraSapiDetalle>();
                    else
                        renglonesDetalle = ((CompraSapi)this._ventana.CompraSapi).DetalleCompra;

                    //Se actualiza la tabla de detalle y las existencias de los Materiales
                    detalleQuitar = (CompraSapiDetalle)this._ventana.DetalleDeCompraSapi;

                    if (!this._agregar)
                    {
                        AjustarExistenciaMaterial(detalleQuitar, false);
                        bool exitoso = this._compraSapiDetalleServicios.Eliminar(detalleQuitar, UsuarioLogeado.Hash);
                        this._ventana.Mensaje("Presione el botón Totalizar Compra", 1);
                    }

                    renglonesDetalle.Remove((CompraSapiDetalle)this._ventana.DetalleDeCompraSapi);
                    
                    ((CompraSapi)this._ventana.CompraSapi).DetalleCompra = renglonesDetalle;
                    this._ventana.DetallesDeCompraSapi = null;
                    this._ventana.DetallesDeCompraSapi = ((CompraSapi)this._ventana.CompraSapi).DetalleCompra;
                    this._ventana.DetalleDeCompraSapi = null;
                }
                else
                    this._ventana.Mensaje("Debe seleccionar un renglón de Detalle para eliminar", 0);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo que ajusta las existencias de Materiales
        /// </summary>
        /// <param name="detalleQuitar">Detalle de Compra</param>
        /// <param name="sumarCantidad">Bandera para indicar si se incrementa o se decrementa la existencia</param>
        private void AjustarExistenciaMaterial(CompraSapiDetalle detalleQuitar, bool sumarCantidad)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                MaterialSapi materialAjustar = detalleQuitar.Material;
                IList<MaterialSapi> materiales = this._materialSapiServicios.ObtenerMaterialSapiFiltro(materialAjustar);
                MaterialSapi material = materiales[0];
                if (sumarCantidad)
                {
                    material.Existencia += detalleQuitar.Cantidad;
                }
                else
                {
                    material.Existencia -= detalleQuitar.Cantidad;
                }

                exitoso = this._materialSapiServicios.InsertarOModificar(material,UsuarioLogeado.Hash);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }
               

        /// <summary>
        /// Metodo que calcula el total de la Compra
        /// </summary>
        public void CalcularTotalCompra()
        {
            double montoImporte, montoIva, montoTotal;
            montoImporte = 0.00;
            montoIva = 0.00;
            montoTotal = 0.00;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if(((CompraSapi)this._ventana.CompraSapi).DetalleCompra != null)
                {
                    IList<CompraSapiDetalle> renglonesDetalleCompra = ((CompraSapi)this._ventana.CompraSapi).DetalleCompra;
                    if ((!this._ventana.PorcentajeImpuesto.Equals("")) && (!this._ventana.PorcentajeImpuesto.Equals("0,00")))
                    {
                        foreach (CompraSapiDetalle renglonDetalle in renglonesDetalleCompra)
                        {
                            montoImporte += renglonDetalle.Total;
                        }
                        montoIva = montoImporte * (double.Parse(this._ventana.PorcentajeImpuesto) / 100);
                        montoTotal = montoImporte + montoIva;

                        this._ventana.MontoImporte = montoImporte.ToString("N");
                        this._ventana.MontoIva = montoIva.ToString("N");
                        this._ventana.TotalCompraSapi = montoTotal.ToString("N");
                    }
                    else
                        this._ventana.Mensaje("Introduzca el porcentaje de IVA a aplicar", 0);
                }
                else
                    this._ventana.Mensaje("La Compra no tiene detalles, no se puede totalizar",0);
                 

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo que indicar si se va a insertar una nueva compra de materiales o se va a modificar una existente
        /// </summary>
        /// <returns></returns>
        public bool InsertarOModificarCompraSapi()
        {
            bool agregar = false;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._agregar)
                agregar = true;
            
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return agregar;
        }

        public void Aceptar()
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                    this._ventana.ActivarBotonesIncluirYBorrar(true);
                }

                else
                {
                    CompraSapi compra = ObtenerCompraSapiPantalla();

                    if (compra.Total != 0)
                    {
                        if (compra.DetalleCompra != null)
                        {
                            exitoso = this._compraSapiServicios.InsertarOModificar(compra, UsuarioLogeado.Hash);
                            if (exitoso)
                            {
                                GuardarDetalleCompraSapi(compra);

                                if(this._agregar)
                                    this._agregar = false;

                                #region CODIGO ORIGINAL COMENTADO - NO BORRAR
                                //if (this._agregar)
                                //    GuardarDetalleCompraSapi(compra);
                                //else
                                //{
                                //    //Buscar los detalles por el numero de la compra
                                //    foreach (CompraSapiDetalle item in compra.DetalleCompra)
                                //    {
                                //        //Ajustar el inventario de materiales
                                //        AjustarExistenciaMaterial(item, false);
                                //        //Borrar cada uno de los detalles
                                //        bool exitosoEliminarItem = this._compraSapiDetalleServicios.Eliminar(item, UsuarioLogeado.Hash);
                                //    }

                                //    //Volver a agregar los detalles que esten
                                //    GuardarDetalleCompraSapi(compra);
                                //} 
                                #endregion

                                this._ventana.Mensaje(string.Format("La Compra {0} fue registrada", compra.Id.ToString()), 2);
                                this._ventana.HabilitarCampos = false;
                                this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                                this._ventana.ActivarBotonesIncluirYBorrar(false);
                                this._ventana.ActivarCampoCantidad(false);
                            }
                        }
                        else
                            this._ventana.Mensaje("La Compra no tiene detalle, revise", 0);
                    }
                    else
                        this._ventana.Mensaje("La Compra de Materiales no esta Totalizada", 0);
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo para guardar y/o actualizar el detalle de una compra
        /// </summary>
        /// <param name="compra"></param>
        private void GuardarDetalleCompraSapi(CompraSapi compra)
        {
            bool exitosoMaterial = false;
            bool exitosoDetalleCompra = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<CompraSapiDetalle> detalleCompra = compra.DetalleCompra;

                if (this._agregar)
                {
                    foreach (CompraSapiDetalle renglonDetalle in detalleCompra)
                    {
                        renglonDetalle.Compra = compra;
                        MaterialSapi materialAux = new MaterialSapi();
                        materialAux.Id = renglonDetalle.Material.Id;
                        IList<MaterialSapi> materiales = this._materialSapiServicios.ObtenerMaterialSapiFiltro(materialAux);
                        MaterialSapi materialAjustar = materiales[0];
                        materialAjustar.Existencia += renglonDetalle.Cantidad;
                        exitosoMaterial = this._materialSapiServicios.InsertarOModificar(materialAjustar, UsuarioLogeado.Hash);
                        if (exitosoMaterial)
                        {
                            exitosoDetalleCompra = this._compraSapiDetalleServicios.InsertarOModificar(renglonDetalle, UsuarioLogeado.Hash);
                            if (exitosoDetalleCompra)
                            {
                                exitosoMaterial = false;
                                exitosoDetalleCompra = false;
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    foreach (CompraSapiDetalle renglonDetalle in detalleCompra)
                    {
                        exitosoDetalleCompra = this._compraSapiDetalleServicios.InsertarOModificar(renglonDetalle, UsuarioLogeado.Hash);
                        if (exitosoDetalleCompra)
                            continue;
                    }
                }

                #region CODIGO ORIGINAL COMENTADO - NO BORRAR
                //foreach (CompraSapiDetalle renglonDetalle in detalleCompra)
                //{
                //    renglonDetalle.Compra = compra;
                //    MaterialSapi materialAux = new MaterialSapi();
                //    materialAux.Id = renglonDetalle.Material.Id;
                //    IList<MaterialSapi> materiales = this._materialSapiServicios.ObtenerMaterialSapiFiltro(materialAux);
                //    MaterialSapi materialAjustar = materiales[0];
                //    materialAjustar.Existencia += renglonDetalle.Cantidad;
                //    exitosoMaterial = this._materialSapiServicios.InsertarOModificar(materialAjustar, UsuarioLogeado.Hash);
                //    if (exitosoMaterial)
                //    {
                //        exitosoDetalleCompra = this._compraSapiDetalleServicios.InsertarOModificar(renglonDetalle, UsuarioLogeado.Hash);
                //        if (exitosoDetalleCompra)
                //        {
                //            exitosoMaterial = false;
                //            exitosoDetalleCompra = false;
                //            continue;
                //        }
                //    }
                //} 
                #endregion

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }


        private CompraSapi ObtenerCompraSapiPantalla()
        {
            CompraSapi compra = (CompraSapi)this._ventana.CompraSapi;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((!this._ventana.MontoImporte.Equals(String.Empty)) && (double.Parse(this._ventana.MontoImporte) != 0))
                    compra.Subtotal = double.Parse(this._ventana.MontoImporte);
                else
                    compra.Subtotal = 0;

                if ((!this._ventana.PorcentajeImpuesto.Equals(String.Empty)) && (double.Parse(this._ventana.PorcentajeImpuesto) != 0))
                    compra.Impuesto = double.Parse(this._ventana.PorcentajeImpuesto);
                else
                    compra.Impuesto = 0;

                if ((!this._ventana.MontoIva.Equals(String.Empty)) && (double.Parse(this._ventana.MontoIva) != 0))
                    compra.SubtotalIva = double.Parse(this._ventana.MontoIva);
                else
                    compra.SubtotalIva = 0;

                if ((!this._ventana.TotalCompraSapi.Equals(String.Empty)) && (double.Parse(this._ventana.TotalCompraSapi) != 0))
                    compra.Total = double.Parse(this._ventana.TotalCompraSapi);
                else
                    compra.Total = 0;
                
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return compra;
        }

        /// <summary>
        /// Metodo que inicializa la pantalla, ya sea para limpiar todos los campos o para ingresar una nueva compra sapi
        /// </summary>
        public void InicializarPantalla()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new GestionarCompraMaterialesSapi());

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo que sirve para visualizar el archivo PDF correspondiente a la Compra de Material SAPI
        /// </summary>
        public void VerFacturaCompraSAPI()
        {
            String ruta = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ruta = ConfigurationManager.AppSettings["RutaFacturaCompraSAPI"].ToString() + _patronArchivoCompraPDF + "." + ((CompraSapi)this._ventana.CompraSapi).Id.ToString() + ".pdf";
                System.Diagnostics.Process.Start(ruta);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Win32Exception ex)
            {
                logger.Error(ex.Message);
                this._ventana.ArchivoNoEncontrado(string.Format(Recursos.MensajesConElUsuario.ErrorArchivoPDFCompraSAPINoExiste, this._ventana.IdCompraSapi));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }


        


    }
}
