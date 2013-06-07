using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Ventanas.AbandonosPatente;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CambiosDeDomicilioPatentes;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CambiosDeNombrePatentes;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CambiosPeticionarioPatentes;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CesionesPatentes;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.FusionesPatentes;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.LicenciasPatentes;
//using Trascend.Bolet.Cliente.Ventanas.Operaciones;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Presentadores.Patentes
{
    class PresentadorListaOperaciones : PresentadorBase
    {

        private IListaOperaciones _ventana;
        private Patente _patente;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private ILicenciaPatenteServicios _licenciaServicios;
        private ICesionPatenteServicios _cesionServicios;
        private IFusionPatenteServicios _fusionServicios;
        private ICambioDeDomicilioPatenteServicios _cambioDomicilioServicios;
        private ICambioDeNombrePatenteServicios _cambioNombreServicios;
        private ICambioPeticionarioPatenteServicios _peticionarioServicios;
        private IOperacionServicios _operacionServicios;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaOperaciones(IListaOperaciones ventana, object patente)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._patente = (Patente)patente;


            #region Servicios

            this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
            this._licenciaServicios = (ILicenciaPatenteServicios)Activator.GetObject(typeof(ILicenciaPatenteServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["LicenciaPatenteServicios"]);
            this._cesionServicios = (ICesionPatenteServicios)Activator.GetObject(typeof(ICesionPatenteServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CesionPatenteServicios"]);
            this._fusionServicios = (IFusionPatenteServicios)Activator.GetObject(typeof(IFusionPatenteServicios),
                 ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FusionPatenteServicios"]);
            this._cambioDomicilioServicios = (ICambioDeDomicilioPatenteServicios)Activator.GetObject(typeof(ICambioDeDomicilioPatenteServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeDomicilioPatenteServicios"]);
            this._cambioNombreServicios = (ICambioDeNombrePatenteServicios)Activator.GetObject(typeof(ICambioDeNombrePatenteServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeNombrePatenteServicios"]);
            this._peticionarioServicios = (ICambioPeticionarioPatenteServicios)Activator.GetObject(typeof(ICambioPeticionarioPatenteServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioPeticionarioPatenteServicios"]);
            this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);

            #endregion

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }



        /// <summary>
        /// Constructor Predeterminado que recibe una ventana padre
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaOperaciones(IListaOperaciones ventana, object patente, object ventanaPadre)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._ventanaPadre = ventanaPadre;
            this._patente = (Patente)patente;


            #region Servicios

            this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
            this._licenciaServicios = (ILicenciaPatenteServicios)Activator.GetObject(typeof(ILicenciaPatenteServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["LicenciaPatenteServicios"]);
            this._cesionServicios = (ICesionPatenteServicios)Activator.GetObject(typeof(ICesionPatenteServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CesionPatenteServicios"]);
            this._fusionServicios = (IFusionPatenteServicios)Activator.GetObject(typeof(IFusionPatenteServicios),
                 ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FusionPatenteServicios"]);
            this._cambioDomicilioServicios = (ICambioDeDomicilioPatenteServicios)Activator.GetObject(typeof(ICambioDeDomicilioPatenteServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeDomicilioPatenteServicios"]);
            this._cambioNombreServicios = (ICambioDeNombrePatenteServicios)Activator.GetObject(typeof(ICambioDeNombrePatenteServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeNombrePatenteServicios"]);
            this._peticionarioServicios = (ICambioPeticionarioPatenteServicios)Activator.GetObject(typeof(ICambioPeticionarioPatenteServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioPeticionarioPatenteServicios"]);
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

                this._ventana.Operaciones = ((Patente)this._patente).Operaciones;
                this._ventana.TotalHits = ((Patente)this._patente).Operaciones.Count.ToString();
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
                        CesionPatente cesion = new CesionPatente();
                        cesion.Id = idGenerico;
                        //this.Navegar(new GestionarCesionPatentes(this._cesionServicios.
                        //                        ObtenerCesionPatenteFiltro(cesion)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarCesionPatentes(this._cesionServicios.
                                                ObtenerCesionPatenteFiltro(cesion)[0], Visibility.Collapsed,this._ventana));

                        break;

                    //redirecciona a Fusion
                    case "FU":
                        FusionPatente fusion = new FusionPatente();
                        fusion.Id = idGenerico;
                        //this.Navegar(new GestionarFusionPatentes((this._fusionServicios.
                        //                                ObtenerFusionPatenteFiltro(fusion)[0]), Visibility.Collapsed));
                        this.Navegar(new GestionarFusionPatentes((this._fusionServicios.
                                                        ObtenerFusionPatenteFiltro(fusion)[0]), Visibility.Collapsed,this._ventana));
                        break;

                    //redirecciona a CambioDeDomicilio
                    case "CD":
                        CambioDeDomicilioPatente cambioDeDomicilio = new CambioDeDomicilioPatente();
                        cambioDeDomicilio.Id = idGenerico;
                        //this.Navegar(new GestionarCambioDeDomicilioPatentes(this._cambioDomicilioServicios.
                        //                   ObtenerCambioDeDomicilioPatenteFiltro(cambioDeDomicilio)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarCambioDeDomicilioPatentes(this._cambioDomicilioServicios.
                                           ObtenerCambioDeDomicilioPatenteFiltro(cambioDeDomicilio)[0], Visibility.Collapsed,this._ventana));
                        break;

                    //redirecciona a CambioDeNombre
                    case "CN":
                        CambioDeNombrePatente cambioDeNombre = new CambioDeNombrePatente();
                        cambioDeNombre.Id = idGenerico;
                        //this.Navegar(new GestionarCambioDeNombrePatentes(this._cambioNombreServicios.
                        //                       ObtenerCambioDeNombrePatenteFiltro(cambioDeNombre)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarCambioDeNombrePatentes(this._cambioNombreServicios.
                                               ObtenerCambioDeNombrePatenteFiltro(cambioDeNombre)[0], Visibility.Collapsed, this._ventana));
                        break;

                    //redirecciona a Licencia
                    case "LU":
                        LicenciaPatente licencia = new LicenciaPatente();
                        licencia.Id = idGenerico;
                        //this.Navegar(new GestionarLicenciaPatentes(this._licenciaServicios.
                        //                                    ObtenerLicenciaFiltro(licencia)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarLicenciaPatentes(this._licenciaServicios.
                                                            ObtenerLicenciaFiltro(licencia)[0], Visibility.Collapsed, this._ventana));
                        break;

                    //redirecciona a CambioPeticionario
                    case "CT":
                        CambioPeticionarioPatente cambioPeticionario = new CambioPeticionarioPatente();
                        cambioPeticionario.Id = idGenerico;
                        //this.Navegar(new GestionarCambioPeticionarioPatentes(this._peticionarioServicios.
                        //                    ObtenerCambioPeticionarioFiltro(cambioPeticionario)[0], Visibility.Collapsed));
                        this.Navegar(new GestionarCambioPeticionarioPatentes(this._peticionarioServicios.
                                            ObtenerCambioPeticionarioFiltro(cambioPeticionario)[0], Visibility.Collapsed,this._ventana));

                        break;

                    //redirecciona a Abandono
                    case "AB":

                        Operacion operacion = this._operacionServicios.
                                                        ConsultarPorId((Operacion)this._ventana.OperacionSeleccionado);
                        operacion.Patente = new Patente(((Operacion)this._ventana.OperacionSeleccionado).CodigoAplicada);
                        this.Navegar(new GestionarAbandonoPatente(operacion, Visibility.Collapsed));


                        break;

                }




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

    }
}
