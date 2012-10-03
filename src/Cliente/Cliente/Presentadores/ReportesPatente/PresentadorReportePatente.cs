using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.ReportesPatente;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using DataTable = System.Data.DataTable;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Diagnostics;

namespace Trascend.Bolet.Cliente.Presentadores.ReportesPatente
{
    class PresentadorReportePatente : PresentadorBase
    {

        private IReportePatente _ventana;
        private IReportePatenteServicios _reportePatenteServicios;
        private IPatenteServicios _patenteServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private Patente _patente;


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorReportePatente(IReportePatente ventana, object patente)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                _patente = (Patente)patente;
                this._reportePatenteServicios = (IReportePatenteServicios)Activator.GetObject(typeof(IReportePatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ReportePatenteServicios"]);
                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                CargarPagina();
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarAnexo, "");

                _patente = this._patenteServicios.ConsultarPatenteConTodo(_patente);

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
        /// Método que se encarga de llamar al formato de impresión FM02
        /// </summary>
        /// <param name="modo">Láser en caso de ser Láser, o normal en caso de ser normal</param>
        public void ImprimirSolicitudVan()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            string paqueteProcedimiento = "rp_pregi";
            string procedimiento = "PLANILLA1";
            ParametroProcedimiento parametro =
                new ParametroProcedimiento(_patente.Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

            ReportePatente reportePatente = this._reportePatenteServicios.ImprimirProcedimiento(parametro);

            if (reportePatente != null)
            {
                GenerarReporte(reportePatente, "SolicitudVan");
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se encarga de llamar al formato de impresión FM02
        /// </summary>
        /// <param name="modo">Láser en caso de ser Láser, o normal en caso de ser normal</param>
        public void ImprimirSolicitudVienen()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            string paqueteProcedimiento = "rp_pregi";
            string procedimiento = "PLANILLA1";
            ParametroProcedimiento parametro =
                new ParametroProcedimiento(_patente.Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

            ReportePatente reportePatente = this._reportePatenteServicios.ImprimirProcedimiento(parametro);

            if (reportePatente != null)
            {
                GenerarReporte(reportePatente, "SolicitudVienen");
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se encarga de llamar al formato de impresión FM02
        /// </summary>
        /// <param name="modo">Láser en caso de ser Láser, o normal en caso de ser normal</param>
        public void ImprimirDatosVan()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            string paqueteProcedimiento = "rp_pregi";
            string procedimiento = "PLANILLA2";
            ParametroProcedimiento parametro =
                new ParametroProcedimiento(_patente.Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

            ReportePatente reportePatente = this._reportePatenteServicios.ImprimirProcedimiento(parametro);

            if (reportePatente != null)
            {
                GenerarReporte(reportePatente, "DatosVan");
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se encarga de llamar al formato de impresión FM02
        /// </summary>
        /// <param name="modo">Láser en caso de ser Láser, o normal en caso de ser normal</param>
        public void ImprimirDatosVienen()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            string paqueteProcedimiento = "rp_pregi";
            string procedimiento = "PLANILLA2";
            ParametroProcedimiento parametro =
                new ParametroProcedimiento(_patente.Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

            ReportePatente reportePatente = this._reportePatenteServicios.ImprimirProcedimiento(parametro);

            if (reportePatente != null)
            {
                GenerarReporte(reportePatente, "DatosVienen");
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se encarga de llamar al formato de impresión FM02
        /// </summary>
        /// <param name="modo">Láser en caso de ser Láser, o normal en caso de ser normal</param>
        public void ImprimirPlanilla()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            GenerarReporte(null, "Planilla");

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que realiza toda la lógica para agregar al País dentro de la base de datos
        /// </summary>
        public void GenerarReporte(ReportePatente reportePatente, string tipo)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ReportDocument reporte = new ReportDocument();
                //reporte.Load();

                reporte.Load(GetRutaReporte(tipo));

                DataTable datos = new DataTable("DataTable1");
                datos.Columns.Add("Sitio");
                datos.Columns.Add("Inventores1");
                datos.Columns.Add("Inventores2");
                datos.Columns.Add("Patente1");
                datos.Columns.Add("Patente2");
                datos.Columns.Add("Omision1");
                datos.Columns.Add("Omision2");
                datos.Columns.Add("Resumen1");
                datos.Columns.Add("Resumen2");
                datos.Columns.Add("Agente");
                datos.Columns.Add("CodigoPropiedad");
                datos.Columns.Add("CodigoFomento");
                datos.Columns.Add("CodigoRegistro");
                datos.Columns.Add("Anexo");
                datos.Columns.Add("Pais");
                datos.Columns.Add("FechaPrioridad");
                datos.Columns.Add("Telefono");
                datos.Columns.Add("Domicilio");
                datos.Columns.Add("Observacion");
                datos.Columns.Add("IdPatente");
                datos.Columns.Add("FechaInscripcion");
                datos.Columns.Add("Interesado");
                datos.Columns.Add("CamposVienen");

                StructReportePatente estructura = ObtenerEstructuraReportePatente(reportePatente, tipo);
                datos = ArmarReporte(datos, estructura);
                //reporte.PrintOptions.PrinterName = ConfigurationManager.AppSettings["ImpresoraReportes"];


                DataSet ds = new DataSet();
                ds.Tables.Add(datos);
                reporte.SetDataSource(datos);
                //reporte.PrintToPrinter(1, false, 1, 0);

                if (BorrarArchivosEnDirectorio(ConfigurationManager.AppSettings["rutaReportePatentes"], ".doc"))
                {
                    this._ventana.MensajeExito(Recursos.MensajesConElUsuario.ExitosoReporte);
                    string ruta = ConfigurationManager.AppSettings["rutaReportePatentes"] + GetNombreArchivo(tipo);
                    reporte.ExportToDisk(ExportFormatType.WordForWindows, ruta);
                    Process.Start(ConfigurationManager.AppSettings["rutaReportePatentes"] + GetNombreArchivo(tipo));

                    reporte.Dispose();
                    reporte.Close();
                }
                else
                {
                    this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.ErrorReporte);
                }

                Mouse.OverrideCursor = null;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (CrystalReportsException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
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


        private string GetNombreArchivo(string tipo)
        {
            string retorno = "";
            switch (tipo)
            {
                case "SolicitudVan":
                    retorno = "SolicitudVan.doc";
                    break;
                case "SolicitudVienen":
                    retorno = "SolicitudVienen.doc";
                    break;
                case "DatosVan":
                    retorno = "DatosVan.doc";
                    break;
                case "DatosVienen":
                    retorno = "DatosVienen.doc";
                    break;
                case "Planilla":
                    retorno = "Planilla.doc";
                    break;
                default:
                    break;
            }

            return "\\" + retorno;

        }


        /// <summary>
        /// Método que se encarga de devolver la ruta del reporte
        /// </summary>
        /// <returns></returns>
        private string GetRutaReporte(string tipo)
        {
            string retorno = "";
            switch (tipo)
            {
                case "SolicitudVan":
                    retorno = Environment.CurrentDirectory + ConfigurationManager.AppSettings["rutaReporteSolicitudVan"];
                    break;
                case "SolicitudVienen":
                    retorno = Environment.CurrentDirectory + ConfigurationManager.AppSettings["rutaReporteSolicitudVienen"];
                    break;
                case "DatosVan":
                    retorno = Environment.CurrentDirectory + ConfigurationManager.AppSettings["rutaReporteDatosVan"];
                    break;
                case "DatosVienen":
                    retorno = Environment.CurrentDirectory + ConfigurationManager.AppSettings["rutaReporteDatosVienen"];
                    break;
                case "Planilla":
                    retorno = Environment.CurrentDirectory + ConfigurationManager.AppSettings["rutaReportePlanillas"];
                    break;
                default:
                    break;
            }

            return retorno;

        }


        /// <summary>
        /// Método qeu se encarga de devolver la estructura determinada para el reporte
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="estructuraDeDatos"></param>
        /// <returns></returns>
        private DataTable ArmarReporte(DataTable datos, StructReportePatente estructurasDeDatos)
        {
            DataRow filaDatos = datos.NewRow();

            filaDatos["Sitio"] = estructurasDeDatos.Sitio;
            filaDatos["Inventores1"] = estructurasDeDatos.Inventores1;
            filaDatos["Inventores2"] = estructurasDeDatos.Inventores2;
            filaDatos["Patente1"] = estructurasDeDatos.Patente1;
            filaDatos["Patente2"] = estructurasDeDatos.Patente2;
            filaDatos["Omision1"] = estructurasDeDatos.Omision1;
            filaDatos["Omision2"] = estructurasDeDatos.Omision2;
            filaDatos["Resumen1"] = estructurasDeDatos.Resumen1;
            filaDatos["Resumen2"] = estructurasDeDatos.Resumen2;
            filaDatos["Agente"] = estructurasDeDatos.Agente;
            filaDatos["CodigoPropiedad"] = estructurasDeDatos.CodigoPropiedad;
            filaDatos["CodigoFomento"] = estructurasDeDatos.CodigoFomento;
            filaDatos["CodigoRegistro"] = estructurasDeDatos.CodigoRegistro;
            filaDatos["Anexo"] = estructurasDeDatos.Anexo;
            filaDatos["Pais"] = estructurasDeDatos.Pais;
            filaDatos["FechaPrioridad"] = estructurasDeDatos.FechaPrioridad;
            filaDatos["Telefono"] = estructurasDeDatos.Telefono;
            filaDatos["Domicilio"] = estructurasDeDatos.Domicilio;
            filaDatos["Observacion"] = estructurasDeDatos.Observacion;
            filaDatos["IdPatente"] = estructurasDeDatos.IdPatente;
            filaDatos["FechaInscripcion"] = estructurasDeDatos.FechaInscripcion;
            filaDatos["Interesado"] = estructurasDeDatos.Interesado;
            filaDatos["CamposVienen"] = estructurasDeDatos.CamposVienen;

            datos.Rows.Add(filaDatos);

            return datos;

        }


        /// <summary>
        /// Método que devuelve la estructura determinada para el deporte con sus atributos
        /// </summary>
        /// <returns></returns>
        private StructReportePatente ObtenerEstructuraReportePatente(ReportePatente reportePatente, string tipo)
        {
            StructReportePatente retorno = new StructReportePatente();

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.Inventores1)))
            {
                retorno.Inventores1 = reportePatente.Inventores1;
            }

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.Inventores2)))
            {
                retorno.Inventores2 = reportePatente.Inventores2;
            }

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.NombrePatente1)))
            {
                retorno.Patente1 = reportePatente.NombrePatente1;
            }

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.NombrePatente2)))
            {
                retorno.Patente2 = reportePatente.NombrePatente2;
            }

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.Omision1)))
            {
                retorno.Omision1 = reportePatente.Omision1;
            }
            else if ((tipo.Equals("Planilla")) && (!string.IsNullOrEmpty(_patente.Omision)))
            {
                retorno.Omision1 = _patente.Omision;
            }

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.Omision2)))
            {
                retorno.Omision2 = reportePatente.Omision2;
            }

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.Resumen1)))
            {
                retorno.Resumen1 = reportePatente.Resumen1;
            }

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.Resumen2)))
            {
                retorno.Resumen2 = reportePatente.Resumen2;
            }

            retorno.Sitio = ArmarSitio();

            //Anexo
            //if (!string.IsNullOrEmpty(_patente.an))
            //{

            //}

            //Telefono
            if ((_patente.Agente != null) && (!string.IsNullOrEmpty(_patente.Agente.Telefono)))
            {
                retorno.Telefono = _patente.Agente.Telefono;
            }

            //Domicilio
            if ((_patente.Agente != null) && (!string.IsNullOrEmpty(_patente.Agente.Domicilio)))
            {
                retorno.Domicilio = _patente.Agente.Domicilio;
            }

            if (!string.IsNullOrEmpty(_patente.CodigoRegistro))
            {
                retorno.CodigoRegistro = _patente.CodigoRegistro;
            }

            //CodigoFomento
            //if (!string.IsNullOrEmpty(_patente.CodigoFomento))
            //{

            //}


            //CodigoPropiedad
            //if (!string.IsNullOrEmpty(_patente.CodigoPropiedad))
            //{

            //}

            if ((null != _patente.Pais) && !string.IsNullOrEmpty(_patente.Pais.NombreEspanol))
            {
                retorno.Pais = _patente.Pais.NombreEspanol;
            }

            if (!string.IsNullOrEmpty(_patente.FechaPrioridad.ToString()))
            {
                retorno.FechaPrioridad = _patente.FechaPrioridad.Value.ToShortDateString();
            }

            if (!string.IsNullOrEmpty(_patente.Observacion1))
            {
                retorno.Observacion = _patente.Observacion1;
            }

            if ((null != _patente.Interesado) && !string.IsNullOrEmpty(_patente.Interesado.Nombre))
            {
                retorno.Interesado = _patente.Interesado.Nombre;
            }

            if ((null != _patente.Agente) && !string.IsNullOrEmpty(_patente.Agente.Nombre))
            {
                retorno.Agente = _patente.Agente.Nombre;
                if (tipo.Equals("SolicitudVan"))
                    retorno.Agente += " " + ConfigurationManager.AppSettings["detalleAgente"];

                retorno.CodigoPropiedad = _patente.Agente.NumeroPropiedad;
            }

            retorno.IdPatente = _patente.Id.ToString();

            retorno.FechaInscripcion = _patente.FechaInscripcion.Value.Day + "          " + _patente.FechaInscripcion.Value.ToString("MMMM") + "           " + _patente.FechaInscripcion.Value.Year.ToString().Substring(2, 2);

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.Inventores2)))
                retorno.CamposVienen += reportePatente.Inventores2;

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.NombrePatente2)))
                retorno.CamposVienen += Environment.NewLine + Environment.NewLine + reportePatente.NombrePatente2;

            if ((null != reportePatente) && (!string.IsNullOrEmpty(reportePatente.Resumen2)))
                retorno.CamposVienen += Environment.NewLine + Environment.NewLine + reportePatente.Resumen2;


            return retorno;
        }


        /// <summary>
        /// Método que se encarga de armar el string de Sitio, sumando distintos campos de los interesados
        /// </summary>
        /// <returns></returns>
        private string ArmarSitio()
        {
            string retorno = string.Empty;

            try
            {
                if (_patente.Interesado != null)
                {
                    retorno += _patente.Interesado.Nombre + Environment.NewLine;

                    if (_patente.Interesado.Nacionalidad != null)
                    {
                        retorno += _patente.Interesado.Nacionalidad.Nacionalidad + Environment.NewLine;
                    }

                    if (!string.IsNullOrEmpty(_patente.Interesado.Ciudad))
                    {
                        retorno += _patente.Interesado.Ciudad;
                    }

                    if (!string.IsNullOrEmpty(_patente.Interesado.Estado))
                    {
                        retorno += ", " + _patente.Interesado.Estado;
                    }

                    if ((null != _patente.Interesado.Pais) && (!string.IsNullOrEmpty(_patente.Interesado.Pais.NombreEspanol)))
                    {
                        retorno += ", " + _patente.Interesado.Pais.NombreEspanol;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return retorno;
        }


        #region Estructuras

        struct StructReportePatente
        {
            private string _sitio;

            private string _idPatente;
            private string _fechaInscripcion;
            private string _inventores1;
            private string _inventores2;
            private string _patente1;
            private string _patente2;
            private string _omision1;
            private string _omision2;
            private string _resumen1;
            private string _resumen2;
            private string _agente;
            private string _codigoPropiedad;
            private string _codigoFomento;
            private string _codigoRegistro;
            private string _anexo;
            private string _pais;
            private string _fechaPrioridad;
            private string _telefono;
            private string _domicilio;
            private string _observacion;
            private string _interesado;
            private string _camposVienen;



            public string Sitio
            {
                get { return _sitio; }
                set { _sitio = value; }
            }

            public string Inventores1
            {
                get { return _inventores1; }
                set { _inventores1 = value; }
            }

            public string Inventores2
            {
                get { return _inventores2; }
                set { _inventores2 = value; }
            }

            public string Patente1
            {
                get { return _patente1; }
                set { _patente1 = value; }
            }

            public string Patente2
            {
                get { return _patente2; }
                set { _patente2 = value; }
            }

            public string Omision1
            {
                get { return _omision1; }
                set { _omision1 = value; }
            }

            public string Omision2
            {
                get { return _omision2; }
                set { _omision2 = value; }
            }

            public string Resumen1
            {
                get { return _resumen1; }
                set { _resumen1 = value; }
            }

            public string Resumen2
            {
                get { return _resumen2; }
                set { _resumen2 = value; }
            }

            public string Agente
            {
                get { return _agente; }
                set { _agente = value; }
            }

            public string CodigoPropiedad
            {
                get { return _codigoPropiedad; }
                set { _codigoPropiedad = value; }
            }

            public string CodigoFomento
            {
                get { return _codigoFomento; }
                set { _codigoFomento = value; }
            }

            public string CodigoRegistro
            {
                get { return _codigoRegistro; }
                set { _codigoRegistro = value; }
            }

            public string Anexo
            {
                get { return _anexo; }
                set { _anexo = value; }
            }

            public string Pais
            {
                get { return _pais; }
                set { _pais = value; }
            }

            public string FechaPrioridad
            {
                get { return _fechaPrioridad; }
                set { _fechaPrioridad = value; }
            }

            public string Telefono
            {
                get { return _telefono; }
                set { _telefono = value; }
            }

            public string Domicilio
            {
                get { return _domicilio; }
                set { _domicilio = value; }
            }

            public string Observacion
            {
                get { return _observacion; }
                set { _observacion = value; }
            }

            public string Interesado
            {
                get { return _interesado; }
                set { _interesado = value; }
            }

            public string IdPatente
            {
                get { return _idPatente; }
                set { _idPatente = value; }
            }

            public string FechaInscripcion
            {
                get { return _fechaInscripcion; }
                set { _fechaInscripcion = value; }
            }

            public string CamposVienen
            {
                get { return _camposVienen; }
                set { _camposVienen = value; }
            }
        }

        #endregion
    }
}
