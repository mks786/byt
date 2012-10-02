using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Reportes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;
using CrystalDecisions.CrystalReports.Engine;
using DataTable = System.Data.DataTable;
using System.Data;
using CrystalDecisions.Shared;
using System.Threading;
using System.Globalization;
using System.Diagnostics;

namespace Trascend.Bolet.Cliente.Presentadores.Reportes
{
    class PresentadorCarta16P : PresentadorBase
    {

        private ICarta16P _ventana;


        private IPaisServicios _paisServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IIdiomaServicios _idiomaServicios;
        private IUsuarioServicios _usuarioServicios;
        private IPatenteServicios _PatenteServicios;
        private IInteresadoServicios _interesadoServicios;
        private IAsociadoServicios _asociadoServicios;


        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IList<Patente> _PatentesAgregadas = new List<Patente>();


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorCarta16P(ICarta16P ventana)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.PatenteGeneral = new Patente();

                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._idiomaServicios = (IIdiomaServicios)Activator.GetObject(typeof(IIdiomaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["IdiomaServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._PatenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleCarta1,
                "");

                this._ventana.Fecha = System.DateTime.Now.ToShortDateString();

                this.CargarComboBoxs();

                this._ventana.FocoPredeterminado();

                this._ventana.BorrarCeros();

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
        /// Método que carga las regiones iniciales
        /// </summary>
        private void CargarComboBoxs()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Idioma> idiomas = this._idiomaServicios.ConsultarTodos();
            this._ventana.Idiomas = idiomas;
            this._ventana.Idioma = this.BuscarIdioma(idiomas, new Idioma("ES"));

            IList<Usuario> usuarios = this._usuarioServicios.ConsultarTodos();
            Usuario primerUsuario = new Usuario("NGN");
            usuarios.Insert(0, primerUsuario);
            this._ventana.Usuarios = usuarios;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que realiza toda la lógica para agregar al País dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (ValidarVentana())
                {
                    ReportDocument reporte = new ReportDocument();
                    IList<StructReporteCarta1> estructuraDeDatos = new List<StructReporteCarta1>();
                    //reporte.Load();

                    reporte.Load(GetRutaReporte());

                    DataTable datos = new DataTable("DataTable1");
                    datos.Columns.Add("CodigoSolicitud");
                    datos.Columns.Add("FechaSolicitud");
                    datos.Columns.Add("NuestraReferencia");
                    datos.Columns.Add("Pais");
                    datos.Columns.Add("Asociado");
                    datos.Columns.Add("DomicilioAsociado");
                    datos.Columns.Add("FechaCarta");
                    datos.Columns.Add("Referencia");

                    if (this._ventana.RadioMuchasPatentes())
                    {
                        estructuraDeDatos = ObtenerEstructuraCartas1();
                    }
                    else if (this._ventana.RadioUnicaPatente())
                    {
                        StructReporteCarta1 estructura = ObtenerEstructuraCarta1();
                        estructuraDeDatos.Add(estructura);
                    }


                    datos = ArmarReporte(datos, estructuraDeDatos);

                    //reporte.PrintOptions.PrinterName = ConfigurationManager.AppSettings["ImpresoraReportes"];

                    DataSet ds = new DataSet();
                    ds.Tables.Add(datos);
                    reporte.SetDataSource(datos);
                    //reporte.PrintToPrinter(1, false, 1, 0);

                    if (BorrarArchivosEnDirectorio(ConfigurationManager.AppSettings["rutaCartas"], ".doc"))
                    {
                        this._ventana.MensajeExito(Recursos.MensajesConElUsuario.ExitosoReporte);
                        string ruta = ConfigurationManager.AppSettings["rutaCartas"] + "\\reporteCarta16P.doc";
                        reporte.ExportToDisk(ExportFormatType.WordForWindows, ruta);
                        Process.Start(ConfigurationManager.AppSettings["rutaCartas"] + "\\reporteCarta16P.doc");

                        reporte.Dispose();
                        reporte.Close();
                    }
                    else
                    {
                        this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.ErrorReporte);
                    }
                }
                else
                {
                }

                Mouse.OverrideCursor = null;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Método que se encarga de validar que la ventana este correcta para su uso
        /// </summary>
        /// <returns></returns>
        private bool ValidarVentana()
        {

            //realizar validación de la ventana
            bool retorno = true;


            if (((Idioma)this._ventana.Idioma).Id.Equals("NGN"))
            {
                retorno = false;
                this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaCartaSinIdioma);
            }
            else if ((((Patente)this._ventana.PatenteGeneral).Id == 0) && (this._ventana.RadioUnicaPatente()))
            {
                retorno = false;
                this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaCartaSinPatente);
            }
            else if ((this._PatentesAgregadas.Count == 0) && (this._ventana.RadioMuchasPatentes()))
            {
                retorno = false;
                this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaCartaSinPatentes);
            }
            else if ((this._ventana.RadioConsultarAsociado()) && (this._ventana.Asociado == null)
                && (this._ventana.RadioUnicaPatente()))
            {
                retorno = false;
                this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaCartaSinAsociado);
            }
            else if ((this._ventana.RadioConsultarInteresado()) && (this._ventana.Interesado == null)
                && (this._ventana.RadioUnicaPatente()))
            {
                retorno = false;
                this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaCartaSinInteresado);
            }
            return retorno;

        }


        /// <summary>
        /// Método que se encarga de devolver la ruta del reporte
        /// </summary>
        /// <returns></returns>
        private string GetRutaReporte()
        {
            string retorno = "";
            if (((Idioma)this._ventana.Idioma).Id.Equals("ES"))

                retorno = Environment.CurrentDirectory + ConfigurationManager.AppSettings["rutaCarta16P"];

            else if (((Idioma)this._ventana.Idioma).Id.Equals("IN"))

                retorno = Environment.CurrentDirectory + ConfigurationManager.AppSettings["rutaCarta16PEN"];

            return retorno;

        }


        /// <summary>
        /// Método qeu se encarga de devolver la estructura determinada para el reporte
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="estructuraDeDatos"></param>
        /// <returns></returns>
        private DataTable ArmarReporte(DataTable datos, IList<StructReporteCarta1> estructurasDeDatos)
        {
            foreach (StructReporteCarta1 structura in estructurasDeDatos)
            {
                DataRow filaDatos = datos.NewRow();

                filaDatos["FechaCarta"] = structura.FechaCarta;
                filaDatos["CodigoSolicitud"] = structura.CodigoSolicitud;
                filaDatos["Asociado"] = structura.Asociado;
                filaDatos["DomicilioAsociado"] = structura.DomicilioAsociado;
                filaDatos["NuestraReferencia"] = structura.NuestraReferencia;
                filaDatos["Pais"] = structura.Pais;
                filaDatos["Referencia"] = structura.Referencia;
                filaDatos["FechaSolicitud"] = structura.FechaSolicitud;

                datos.Rows.Add(filaDatos);
            }


            return datos;

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


        /// <summary>
        /// Método que se encarga de hacer la consulta de un asociado o interesado dependiendo del checkbox seleccionado
        /// </summary>
        public void ConsultarInteresadoOAsociado()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            if (this._ventana.RadioConsultarInteresado())
            {
                Interesado interesadoFiltrar = new Interesado();

                interesadoFiltrar.Id = this._ventana.IdFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdFiltrar);
                interesadoFiltrar.Nombre = this._ventana.NombreFiltrar.Equals("") ? string.Empty : this._ventana.NombreFiltrar;

                IList<Interesado> interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoFiltrar);
                this._ventana.Interesados = interesados;

            }
            else if (this._ventana.RadioConsultarAsociado())
            {
                Asociado asociadoFiltrar = new Asociado();

                asociadoFiltrar.Id = this._ventana.IdFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdFiltrar);
                asociadoFiltrar.Nombre = this._ventana.NombreFiltrar.Equals("") ? string.Empty : this._ventana.NombreFiltrar;

                IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoFiltrar);
                this._ventana.Asociados = asociados;
            }

            Mouse.OverrideCursor = null;
        }


        /// <summary>
        /// Método que consulta la Patente deseada en la ventana
        /// </summary>
        public void ConsultarPatente()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            Patente PatenteFiltrar = new Patente();

            PatenteFiltrar.Id = this._ventana.IdPatenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPatenteFiltrar);
            PatenteFiltrar.Descripcion = this._ventana.NombrePatenteFiltrar.Equals("") ? string.Empty : this._ventana.NombrePatenteFiltrar;

            if ((PatenteFiltrar.Id != 0) || (!PatenteFiltrar.Descripcion.Equals(string.Empty)))
            {
                IList<Patente> Patentes = this._PatenteServicios.ObtenerPatentesFiltro(PatenteFiltrar);
                this._ventana.Patentes = Patentes;
            }
            else
                this._ventana.Patentes = null;

            Mouse.OverrideCursor = null;
        }


        /// <summary>
        /// Método que cambia la Patente seleccionada
        /// </summary>
        /// <returns></returns>
        public bool CambiarPatente()
        {
            bool retorno = false;
            if (this._ventana.Patente != null)
            {
                this._ventana.PatenteGeneral = this._ventana.Patente;

                IList<Asociado> asociados = new List<Asociado>();
                asociados.Add(((Patente)this._ventana.Patente).Asociado);

                this._ventana.Asociados = asociados;
                this._ventana.Asociado = ((Patente)this._ventana.Patente).Asociado;

                IList<Interesado> interesados = new List<Interesado>();
                interesados.Add(((Patente)this._ventana.Patente).Interesado);

                this._ventana.Interesados = interesados;
                this._ventana.Interesado = ((Patente)this._ventana.Patente).Interesado;
                retorno = true;
            }

            return retorno;
        }


        /// <summary>
        /// Método que capta el evento al cambiar de usuario en el combo de la ventana
        /// </summary>
        public void CambiarUsuario()
        {
            if (!((Usuario)this._ventana.Usuario).Id.Equals("NGN"))
            {
                this._ventana.Departamento(((Usuario)this._ventana.Usuario).Departamento.Descripcion);
            }
        }


        /// <summary>
        /// Método que agrega una Patente a la lista de Patentes agregadas
        /// </summary>
        public void AgregarPatente()
        {
            try
            {
                if (this._ventana.Patente != null)
                {
                    _PatentesAgregadas.Insert(0, ((Patente)this._ventana.Patente));
                    this._ventana.PatentesAgregadas = null;
                    this._ventana.PatentesAgregadas = _PatentesAgregadas;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }


        /// <summary>
        /// Método que elimina una Patente de la lista de Patentes agregadas
        /// </summary>
        public void EliminarPatente()
        {
            try
            {
                if (this._ventana.PatenteAgregada != null)
                {
                    _PatentesAgregadas.Remove(((Patente)this._ventana.PatenteAgregada));
                    this._ventana.PatentesAgregadas = null;
                    this._ventana.PatentesAgregadas = _PatentesAgregadas;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }


        /// <summary>
        /// Método que devuelve la estructura determinada para el deporte con sus atributos
        /// </summary>
        /// <returns></returns>
        private StructReporteCarta1 ObtenerEstructuraCarta1()
        {
            StructReporteCarta1 retorno = new StructReporteCarta1();

            DateTime fechaAux = new DateTime(int.Parse(this._ventana.Fecha.Substring(6, 4)),
               int.Parse(this._ventana.Fecha.Substring(3, 2)),
                int.Parse(this._ventana.Fecha.Substring(0, 2)));

            if (((Idioma)this._ventana.Idioma).Id.Equals("ES"))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            }
            else if (((Idioma)this._ventana.Idioma).Id.Equals("IN"))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            }


            retorno.FechaCarta = fechaAux.ToLongDateString();

            retorno.CodigoSolicitud = null != ((Patente)this._ventana.Patente).CodigoInscripcion && !((Patente)this._ventana.Patente).CodigoInscripcion.Equals(string.Empty) ?
                ((Patente)this._ventana.Patente).CodigoInscripcion : string.Empty;

            retorno.FechaSolicitud = null != ((Patente)this._ventana.Patente).FechaInscripcion && !((Patente)this._ventana.Patente).FechaInscripcion.Equals(string.Empty) ?
                ((Patente)this._ventana.Patente).FechaInscripcion.Value.ToShortDateString() : string.Empty;

            if (((Idioma)this._ventana.Idioma).Id.Equals("ES"))
                retorno.Pais = ((Asociado)this._ventana.Asociado) != null &&
                        ((Asociado)this._ventana.Asociado).Pais != null &&
                        !((Asociado)this._ventana.Asociado).Pais.NombreEspanol.Equals(string.Empty) ?
                        ((Asociado)this._ventana.Asociado).Pais.NombreEspanol : string.Empty;

            else if (((Idioma)this._ventana.Idioma).Id.Equals("IN"))
                retorno.Pais = ((Asociado)this._ventana.Asociado) != null &&
                        ((Asociado)this._ventana.Asociado).Pais != null &&
                        !((Asociado)this._ventana.Asociado).Pais.NombreIngles.Equals(string.Empty) ?
                        ((Asociado)this._ventana.Asociado).Pais.NombreIngles : string.Empty;

            retorno.Asociado = ((Asociado)this._ventana.Asociado) != null ?
                ((Asociado)this._ventana.Asociado).Nombre : string.Empty;

            retorno.Referencia = ((Patente)this._ventana.Patente).PrimeraReferencia;

            retorno.NuestraReferencia = ((Patente)this._ventana.Patente).Id.ToString();

            retorno.DomicilioAsociado = ((Asociado)this._ventana.Asociado) != null ?
                ((Asociado)this._ventana.Asociado).Domicilio : string.Empty;

            return retorno;
        }


        /// <summary>
        /// Método que se encarga de armar la lista de estructuras para hacer el reporte para varias Patentes
        /// </summary>
        /// <returns></returns>
        private IList<StructReporteCarta1> ObtenerEstructuraCartas1()
        {
            IList<StructReporteCarta1> retorno = new List<StructReporteCarta1>();

            try
            {
                if (this._PatentesAgregadas.Count > 0)
                {
                    foreach (Patente Patente in this._PatentesAgregadas)
                    {
                        StructReporteCarta1 estructura = new StructReporteCarta1();

                        DateTime fechaAux = new DateTime(int.Parse(this._ventana.Fecha.Substring(6, 4)),
                           int.Parse(this._ventana.Fecha.Substring(3, 2)),
                            int.Parse(this._ventana.Fecha.Substring(0, 2)));

                        if (((Idioma)this._ventana.Idioma).Id.Equals("ES"))
                        {
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                        }
                        else if (((Idioma)this._ventana.Idioma).Id.Equals("IN"))
                        {
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                        }


                        estructura.FechaCarta = fechaAux.ToLongDateString();

                        estructura.CodigoSolicitud = null != Patente.CodigoInscripcion && !Patente.CodigoInscripcion.Equals(string.Empty) ?
                            Patente.CodigoInscripcion : string.Empty;


                        estructura.FechaSolicitud = null != Patente.FechaInscripcion && !Patente.FechaInscripcion.Equals(string.Empty) ?
                            Patente.FechaInscripcion.Value.ToShortDateString() : string.Empty;

                        if (((Idioma)this._ventana.Idioma).Id.Equals("ES"))
                            estructura.Pais = Patente.Asociado != null &&
                                    Patente.Asociado.Pais != null &&
                                    !Patente.Asociado.Pais.NombreEspanol.Equals(string.Empty) ?
                                    Patente.Asociado.Pais.NombreEspanol : string.Empty;

                        else if (((Idioma)this._ventana.Idioma).Id.Equals("IN"))
                            estructura.Pais = Patente.Asociado != null &&
                                    Patente.Asociado.Pais != null &&
                                    !Patente.Asociado.Pais.NombreIngles.Equals(string.Empty) ?
                                    Patente.Asociado.Pais.NombreIngles : string.Empty;

                        estructura.Asociado = Patente.Asociado != null ?
                            Patente.Asociado.Nombre : string.Empty;

                        estructura.Referencia = Patente.PrimeraReferencia;

                        estructura.NuestraReferencia = Patente.Id.ToString();

                        estructura.DomicilioAsociado = Patente.Asociado != null ?
                            Patente.Asociado.Domicilio : string.Empty;

                        retorno.Add(estructura);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }


            return retorno;
        }


        #region Estructuras

        struct StructReporteCarta1
        {
            private string _fechaCarta;

            private string _fechaSolicitud;
            private string _codigoSolicitud;
            private string _referencia;
            private string _nuestraReferencia;
            private string _domicilioAsociado;

            private string _asociado;

            private string _pais;



            public string FechaCarta
            {
                get { return _fechaCarta; }
                set { _fechaCarta = value; }
            }

            public string NuestraReferencia
            {
                get { return _nuestraReferencia; }
                set { _nuestraReferencia = value; }
            }

            public string FechaSolicitud
            {
                get { return _fechaSolicitud; }
                set { _fechaSolicitud = value; }
            }

            public string CodigoSolicitud
            {
                get { return _codigoSolicitud; }
                set { _codigoSolicitud = value; }
            }

            public string Referencia
            {
                get { return _referencia; }
                set { _referencia = value; }
            }

            public string Pais
            {
                get { return _pais; }
                set { _pais = value; }
            }

            public string DomicilioAsociado
            {
                get { return _domicilioAsociado; }
                set { _domicilioAsociado = value; }
            }

            public string Asociado
            {
                get { return _asociado; }
                set { _asociado = value; }
            }

        }

        #endregion
    }
}
