using System;
using System.Configuration;
using System.Globalization;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Remoting;
using System.Windows;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Abandonos;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Renovaciones;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosDeDomicilio;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosDeNombre;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosPeticionario;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Cesiones;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Ventanas.Traspasos.Licencias;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
//using Trascend.Bolet.Cliente.Ventanas.Operaciones;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorListaOperaciones : PresentadorBase
    {

        private IListaOperaciones _ventana;
        private Marca _marca;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private ILicenciaServicios _licenciaServicios;
        private ICesionServicios _cesionServicios;
        private IFusionServicios _fusionServicios;
        private ICambioDeDomicilioServicios _cambioDomicilioServicios;
        private IRenovacionServicios _renovacionServicios;
        private ICambioDeNombreServicios _cambioNombreServicios;
        private ICambioPeticionarioServicios _peticionarioServicios;
        private IOperacionServicios _operacionServicios;

        private bool _usarOperaciones;
        private IList<Operacion> _operaciones = new List<Operacion>();

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaOperaciones(IListaOperaciones ventana, object marca)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._marca = (Marca)marca;

            #region Servicios

            this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
            this._licenciaServicios = (ILicenciaServicios)Activator.GetObject(typeof(ILicenciaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["LicenciaServicios"]);
            this._cesionServicios = (ICesionServicios)Activator.GetObject(typeof(ICesionServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CesionServicios"]);
            this._fusionServicios = (IFusionServicios)Activator.GetObject(typeof(IFusionServicios),
                 ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FusionServicios"]);
            this._cambioDomicilioServicios = (ICambioDeDomicilioServicios)Activator.GetObject(typeof(ICambioDeDomicilioServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeDomicilioServicios"]);
            this._renovacionServicios = (IRenovacionServicios)Activator.GetObject(typeof(IRenovacionServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RenovacionServicios"]);
            this._cambioNombreServicios = (ICambioDeNombreServicios)Activator.GetObject(typeof(ICambioDeNombreServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeNombreServicios"]);
            this._peticionarioServicios = (ICambioPeticionarioServicios)Activator.GetObject(typeof(ICambioPeticionarioServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioPeticionarioServicios"]);
            this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);

            #endregion

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }



        public PresentadorListaOperaciones(IListaOperaciones ventana, object marca, object ventanaPadre)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._ventanaPadre = ventanaPadre;
            this._marca = (Marca)marca;

            #region Servicios

            this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
            this._licenciaServicios = (ILicenciaServicios)Activator.GetObject(typeof(ILicenciaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["LicenciaServicios"]);
            this._cesionServicios = (ICesionServicios)Activator.GetObject(typeof(ICesionServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CesionServicios"]);
            this._fusionServicios = (IFusionServicios)Activator.GetObject(typeof(IFusionServicios),
                 ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FusionServicios"]);
            this._cambioDomicilioServicios = (ICambioDeDomicilioServicios)Activator.GetObject(typeof(ICambioDeDomicilioServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeDomicilioServicios"]);
            this._renovacionServicios = (IRenovacionServicios)Activator.GetObject(typeof(IRenovacionServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RenovacionServicios"]);
            this._cambioNombreServicios = (ICambioDeNombreServicios)Activator.GetObject(typeof(ICambioDeNombreServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeNombreServicios"]);
            this._peticionarioServicios = (ICambioPeticionarioServicios)Activator.GetObject(typeof(ICambioPeticionarioServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioPeticionarioServicios"]);
            this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);

            #endregion

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        //
        public PresentadorListaOperaciones(IListaOperaciones ventana, object operaciones, object ventanaPadre, bool usarOperaciones)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._ventanaPadre = ventanaPadre;
            this._usarOperaciones = usarOperaciones;
            this._operaciones = (IList<Operacion>)operaciones;

            #region Servicios

            this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
            this._licenciaServicios = (ILicenciaServicios)Activator.GetObject(typeof(ILicenciaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["LicenciaServicios"]);
            this._cesionServicios = (ICesionServicios)Activator.GetObject(typeof(ICesionServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CesionServicios"]);
            this._fusionServicios = (IFusionServicios)Activator.GetObject(typeof(IFusionServicios),
                 ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FusionServicios"]);
            this._cambioDomicilioServicios = (ICambioDeDomicilioServicios)Activator.GetObject(typeof(ICambioDeDomicilioServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeDomicilioServicios"]);
            this._renovacionServicios = (IRenovacionServicios)Activator.GetObject(typeof(IRenovacionServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RenovacionServicios"]);
            this._cambioNombreServicios = (ICambioDeNombreServicios)Activator.GetObject(typeof(ICambioDeNombreServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeNombreServicios"]);
            this._peticionarioServicios = (ICambioPeticionarioServicios)Activator.GetObject(typeof(ICambioPeticionarioServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioPeticionarioServicios"]);
            this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);

            #endregion

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaOperaciones,
                    Recursos.Ids.Operacion);

                if (!this._usarOperaciones)
                {
                    this._ventana.Operaciones = ((Marca)this._marca).Operaciones;
                    this._ventana.TotalHits = ((Marca)this._marca).Operaciones.Count.ToString();
                }
                else
                {
                    this._ventana.Operaciones = this._operaciones;
                    this._ventana.TotalHits = this._operaciones.Count.ToString();
                }
                //this._ventana.TotalHits = ((Marca)this._marca).Operaciones.Count.ToString();
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
        /// Método que invoca una nueva página "GestionarOperacion" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrGestionarOperacion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion


            if (this._ventana.OperacionSeleccionado != null)
            {
                string idServicio = ((Operacion)this._ventana.OperacionSeleccionado).Servicio.Id;
                //es el id de con el que se consigue el objeto al se enviara
                int idGenerico = ((Operacion)this._ventana.OperacionSeleccionado).Interno;

                #region Comentado codigo con Assembly
                //ListaDatosValores listaValor = new ListaDatosValores();
                //ListaDatosValores listaValoreConseguido = new ListaDatosValores();
                //listaValor.Id = Recursos.Etiquetas.cbiCategoriaServiciosOperaciones;
                //IList<ListaDatosValores> servicios = this._listaDatosValoresServicios.
                //                                      ConsultarListaDatosValoresPorParametro(listaValor);
                //listaValor.Valor = idServicio;
                //listaValoreConseguido = BuscarListaDeDatosValores(servicios,listaValor);
                //VentanaEntidad = listaValoreConseguido.Descripcion.Split(',');
                //Ventana = VentanaEntidad[0];

                //Assembly thisAssembly = Assembly.LoadFrom("Trascend.Bolet.Cliente.Ventanas." 
                //                                            + listaValoreConseguido.SubCodigo + "." +Ventana);
                //Page comando = (Page)thisAssembly.
                //    CreateInstance("Trascend.Bolet.Cliente.Ventanas." + listaValoreConseguido.SubCodigo + "." + 
                //    Ventana, true, BindingFlags.CreateInstance, null, null, null, null);

                //this.Navegar(comando);
                #endregion

                switch (idServicio)
                {
                    //redirecciona a Cesion
                    case "CS":
                        Cesion cesion = new Cesion();
                        cesion.Id = idGenerico;
                        //this.Navegar(new GestionarCesion(this._cesionServicios.
                        //                        ObtenerCesionFiltro(cesion)[0], Visibility.Collapsed));

                        this.Navegar(new GestionarCesion(this._cesionServicios.
                                                ObtenerCesionFiltro(cesion)[0], Visibility.Collapsed, this._ventana));

                        break;

                    //redirecciona a Fusion
                    case "FU":
                        Fusion fusion = new Fusion();
                        fusion.Id = idGenerico;
                        //this.Navegar(new GestionarFusion((this._fusionServicios.
                        //                                ObtenerFusionFiltro(fusion)[0]), Visibility.Collapsed));
                        this.Navegar(new GestionarFusion((this._fusionServicios.
                                                        ObtenerFusionFiltro(fusion)[0]), Visibility.Collapsed, this._ventana));

                        break;

                    //redirecciona a CambioDeDomicilio
                    case "CD":
                        CambioDeDomicilio cambioDeDomicilio = new CambioDeDomicilio();
                        cambioDeDomicilio.Id = idGenerico;
                        //this.Navegar(new GestionarCambioDeDomicilio(this._cambioDomicilioServicios.
                        //                   ObtenerCambioDeDomicilioFiltro(cambioDeDomicilio)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarCambioDeDomicilio(this._cambioDomicilioServicios.
                                           ObtenerCambioDeDomicilioFiltro(cambioDeDomicilio)[0], Visibility.Collapsed, this._ventana));

                        break;

                    //redirecciona a CambioDeNombre
                    case "CN":
                        CambioDeNombre cambioDeNombre = new CambioDeNombre();
                        cambioDeNombre.Id = idGenerico;
                        //this.Navegar(new GestionarCambioDeNombre(this._cambioNombreServicios.
                        //                       ObtenerCambioDeNombreFiltro(cambioDeNombre)[0], Visibility.Collapsed));

                        this.Navegar(new GestionarCambioDeNombre(this._cambioNombreServicios.
                                               ObtenerCambioDeNombreFiltro(cambioDeNombre)[0], Visibility.Collapsed, this._ventana));

                        break;

                    //redirecciona a Renovacion
                    case "RN":
                        Renovacion renovacion = new Renovacion();
                        renovacion.Id = idGenerico;
                        //this.Navegar(new GestionarRenovacion(this._renovacionServicios.
                        //                        ObtenerRenovacionFiltro(renovacion)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarRenovacion(this._renovacionServicios.
                                                ObtenerRenovacionFiltro(renovacion)[0], Visibility.Collapsed, this._ventana));
                        break;

                    //redirecciona a Licencia
                    case "LU":
                        Licencia licencia = new Licencia();
                        licencia.Id = idGenerico;
                        //this.Navegar(new GestionarLicencia(this._licenciaServicios.
                        //                                    ObtenerLicenciaFiltro(licencia)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarLicencia(this._licenciaServicios.
                                                            ObtenerLicenciaFiltro(licencia)[0], Visibility.Collapsed,this._ventana));

                        break;

                    //redirecciona a CambioPeticionario
                    case "CT":
                        CambioPeticionario cambioPeticionario = new CambioPeticionario();
                        cambioPeticionario.Id = idGenerico;
                        //this.Navegar(new GestionarCambioPeticionario(this._peticionarioServicios.
                        //                    ObtenerCambioPeticionarioFiltro(cambioPeticionario)[0], Visibility.Collapsed));

                        this.Navegar(new GestionarCambioPeticionario(this._peticionarioServicios.
                                            ObtenerCambioPeticionarioFiltro(cambioPeticionario)[0], Visibility.Collapsed,this._ventana));

                        break;

                    //redirecciona a Abandono
                    case "AB":

                        Operacion operacion = this._operacionServicios.
                                                        ConsultarPorId((Operacion)this._ventana.OperacionSeleccionado);
                        operacion.Marca = new Marca(((Operacion)this._ventana.OperacionSeleccionado).CodigoAplicada);
                        this.Navegar(new GestionarAbandono(operacion, Visibility.Collapsed));


                        break;

                }




            }

            //this.Navegar(new GestionarOperacion(this._ventana.OperacionSeleccionado));

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
