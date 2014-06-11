using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.SAPI.Materiales;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.SAPI.Materiales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.SAPI.Materiales
{
    class PresentadorConsultarExistenciaMaterial : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarExistenciaMaterial _ventana;
        private IMaterialSapiServicios _materialSapiServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IDepartamentoServicios _departamentoServicios;
        private bool _hayFiltro = false;

        /// <summary>
        /// Constructor predeterminado 
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        public PresentadorConsultarExistenciaMaterial(IConsultarExistenciaMaterial ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                MaterialSapi materialSapi = new MaterialSapi();
                this._ventana.MaterialSapi = materialSapi;

                this._materialSapiServicios = (IMaterialSapiServicios)Activator.GetObject(typeof(IMaterialSapiServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MaterialSapiServicios"]);

                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);

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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + "en el metodo: " + (new System.Diagnostics.StackFrame()).GetMethod().Name + ". Error: " + ex.Message, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarExistenciaMaterial,
                Recursos.Ids.GeneradorReporteMarca);
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

                ListaDatosValores primerTipo = new ListaDatosValores();
                primerTipo.Id = "NGN";
                Departamento primerDpto = new Departamento();
                primerDpto.Id = "NGN";
                MaterialSapi primerMaterial = new MaterialSapi();
                primerMaterial.Id = "";
                primerMaterial.Descripcion = "";

                IList<ListaDatosValores> tiposMaterialSapi = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiSapiTipoMaterial));
                tiposMaterialSapi.Insert(0,primerTipo);
                this._ventana.MaterialTipos = tiposMaterialSapi;

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                departamentos.Insert(0, primerDpto);
                this._ventana.MaterialDepartamentos = departamentos;

                IList<MaterialSapi> materiales = this._materialSapiServicios.ConsultarTodos();
                IList<MaterialSapi> materialesOrdenados = materiales.OrderBy(o => o.Descripcion).ToList();
                materiales = materiales.OrderBy(o => o.Id).ToList();
                materiales.Insert(0, primerMaterial);
                materialesOrdenados.Insert(0, primerMaterial);
                this._ventana.MaterialIds = materiales;
                //this._ventana.MaterialDescripciones = materiales;
                this._ventana.MaterialDescripciones = materialesOrdenados;

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
        /// Metodo que permite exportar el resultado de la pantalla a Excel
        /// </summary>
        public void VerReporteExistenciasExcel()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<MaterialSapi> materialesFiltrados;
                DataTable datosExportar = CrearDataTableExportacion();

                materialesFiltrados = (IList<MaterialSapi>)this._ventana.Resultados;

                if (this._ventana.Resultados != null)
                {
                    if (materialesFiltrados.Count > 0)
                    {
                        datosExportar = LlenarDataTableExportacion(materialesFiltrados, datosExportar);
                        this._ventana.ExportarDatosExcel(datosExportar);
                    }
                    else
                        this._ventana.Mensaje("No existen datos para exportar", 0); 
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que llena el DataTable de exportacion a Excel
        /// </summary>
        /// <param name="materialesFiltrados">Lista de Materiales Sapi filtrados</param>
        /// <param name="datosExportar">DataTable de exportacion a llenar</param>
        /// <returns>DataTable de exportacion lleno</returns>
        private DataTable LlenarDataTableExportacion(IList<MaterialSapi> materialesFiltrados, DataTable datosExportar)
        {
            DataTable datos = datosExportar;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (MaterialSapi item in materialesFiltrados)
                {
                    DataRow filaNueva = datos.NewRow();
                    filaNueva["Codigo"] = item.Id;
                    filaNueva["Nombre Material"] = item.Descripcion;
                    filaNueva["Tipo Material"] = item.TipoDescripcion;
                    filaNueva["Departamento"] = item.Departamento.Descripcion;
                    if (item.FechaVigencia != null)
                    {
                        DateTime fechaVigencia = item.FechaVigencia.Value;
                        filaNueva["Fecha Vigencia"] = fechaVigencia.Date;
                    }
                    filaNueva["Existencia"] = item.Existencia;
                    
                    datos.Rows.Add(filaNueva);

                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return datos;
        }

        /// <summary>
        /// Metodo que crea el DataTable de exportacion para ver los datos filtrados en Excel
        /// </summary>
        /// <returns>DataTable de Exportacion creado</returns>
        private DataTable CrearDataTableExportacion()
        {
            DataTable datos = new DataTable();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                datos.Columns.Add("Codigo", typeof(string));
                datos.Columns.Add("Nombre Material", typeof(string));
                datos.Columns.Add("Tipo Material", typeof(string));
                datos.Columns.Add("Departamento", typeof(string));
                datos.Columns.Add("Fecha Vigencia", typeof(DateTime));
                datos.Columns.Add("Existencia", typeof(int));
                
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return datos;
        }

        /// <summary>
        /// Metodo que obtiene una lista de materiales que cumplan con los filtros seleccionados por el usuario
        /// </summary>
        public void BuscarMateriales()
        {
            
            Mouse.OverrideCursor = Cursors.Wait;
            IList<MaterialSapi> materialesFiltrados;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                MaterialSapi materialSapi = ObtenerMaterialSapiPantalla();

                if (this._hayFiltro)
                {
                    materialesFiltrados = this._materialSapiServicios.ObtenerMaterialSapiFiltro(materialSapi);
                    this._hayFiltro = false;
                }
                else
                {
                    materialesFiltrados=this._materialSapiServicios.ConsultarTodos();
                }

                if (materialesFiltrados.Count > 0)
                {
                    this._ventana.Resultados = materialesFiltrados;
                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorConsultaNoTrajoMateriales, 0);

                    
                

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
        /// Metodo para obtener los filtros necesarios para buscar materiales y asi consultar su existencias
        /// </summary>
        /// <returns></returns>
        private MaterialSapi ObtenerMaterialSapiPantalla()
        {
            MaterialSapi material = new MaterialSapi();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((this._ventana.MaterialId != null) && (!((MaterialSapi)this._ventana.MaterialId).Id.Equals("")))
                {
                    material.Id = ((MaterialSapi)this._ventana.MaterialId).Id;
                    this._hayFiltro = true;
                }
                else
                    material.Id = String.Empty;

                if ((this._ventana.MaterialDescripcion != null) && (!((MaterialSapi)this._ventana.MaterialDescripcion).Descripcion.Equals("")))
                {
                    material.Descripcion = ((MaterialSapi)this._ventana.MaterialDescripcion).Descripcion;
                    this._hayFiltro = true;
                }
                else
                    material.Descripcion = null;

                if ((this._ventana.MaterialTipo != null) && !((ListaDatosValores)this._ventana.MaterialTipo).Id.Equals("NGN"))
                {
                    material.Tipo = ((ListaDatosValores)this._ventana.MaterialTipo).Valor;
                    this._hayFiltro = true;
                }
                else
                    material.Tipo = null;

                if ((this._ventana.MaterialDepartamento != null) && !((Departamento)this._ventana.MaterialDepartamento).Id.Equals("NGN"))
                {
                    material.Departamento = (Departamento)this._ventana.MaterialDepartamento;
                    this._hayFiltro = true;
                }
                else
                    material.Departamento = null;
                

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

            return material;
        }

        /// <summary>
        /// Metodo para limpiar los campos de busqueda de la pantalla y el area de resultados de la misma
        /// </summary>
        public void LimpiarCampos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ConsultarExistenciaMaterial());

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
