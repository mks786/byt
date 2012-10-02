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
    class PresentadorCarta16 : PresentadorBase
    {
        private ICarta16 _ventana;

        private IPaisServicios _paisServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IIdiomaServicios _idiomaServicios;
        private IUsuarioServicios _usuarioServicios;
        private IMarcaServicios _marcaServicios;
        private IInteresadoServicios _interesadoServicios;
        private IAsociadoServicios _asociadoServicios;

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IList<Marca> _marcasAgregadas = new List<Marca>();


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorCarta16(ICarta16 ventana)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.MarcaGeneral = new Marca();

                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._idiomaServicios = (IIdiomaServicios)Activator.GetObject(typeof(IIdiomaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["IdiomaServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleCarta16,
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
                    IList<StructReporteCarta16> estructuraDeDatos = new List<StructReporteCarta16>();
                    //reporte.Load();

                    reporte.Load(GetRutaReporte());

                    DataTable datos = new DataTable("DataTable1");
                    datos.Columns.Add("Asociado");
                    datos.Columns.Add("DomicilioAsociado");
                    datos.Columns.Add("FechaCarta");

                    estructuraDeDatos.Add(ObtenerEstructuraCarta1());

                    datos = ArmarReporte(datos, estructuraDeDatos);

                    //reporte.PrintOptions.PrinterName = ConfigurationManager.AppSettings["ImpresoraReportes"];


                    DataSet ds = new DataSet();
                    ds.Tables.Add(datos);
                    reporte.SetDataSource(datos);
                    //reporte.PrintToPrinter(1, false, 1, 0);

                    if (BorrarArchivosEnDirectorio(ConfigurationManager.AppSettings["rutaCartas"], ".doc"))
                    {
                        this._ventana.MensajeExito(Recursos.MensajesConElUsuario.ExitosoReporte);
                        string ruta = ConfigurationManager.AppSettings["rutaCartas"] + "\\reporteCarta16.doc";
                        reporte.ExportToDisk(ExportFormatType.WordForWindows, ruta);
                        Process.Start(ConfigurationManager.AppSettings["rutaCartas"] + "\\reporteCarta16.doc");

                        reporte.Dispose();
                        reporte.Close();
                    }
                    else
                    {
                        this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.ErrorReporte);
                    }
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


        /// <summary>
        /// Método que se encarga de validar que la ventana este correcta para su uso
        /// </summary>
        /// <returns></returns>
        private bool ValidarVentana()
        {
            bool retorno = true;
            //realizar validación de la ventana
            if (((Idioma)this._ventana.Idioma).Id.Equals("NGN"))
            {
                retorno = false;
                this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaCartaSinIdioma);
            }
            else if (this._ventana.Asociado == null)
            {
                retorno = false;
                this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaCartaSinAsociado);
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

                retorno = Environment.CurrentDirectory + ConfigurationManager.AppSettings["rutaCarta16"];

            else if (((Idioma)this._ventana.Idioma).Id.Equals("IN"))

                retorno = Environment.CurrentDirectory + ConfigurationManager.AppSettings["rutaCarta16EN"];

            return retorno;

        }


        /// <summary>
        /// Método qeu se encarga de devolver la estructura determinada para el reporte
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="estructuraDeDatos"></param>
        /// <returns></returns>
        private DataTable ArmarReporte(DataTable datos, IList<StructReporteCarta16> estructurasDeDatos)
        {
            foreach (StructReporteCarta16 structura in estructurasDeDatos)
            {
                DataRow filaDatos = datos.NewRow();

                filaDatos["FechaCarta"] = structura.FechaCarta;
                filaDatos["Asociado"] = structura.Asociado;
                filaDatos["DomicilioAsociado"] = structura.DomicilioAsociado;

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
            Asociado asociadoFiltrar = new Asociado();

            asociadoFiltrar.Id = this._ventana.IdFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdFiltrar);
            asociadoFiltrar.Nombre = this._ventana.NombreFiltrar.Equals("") ? string.Empty : this._ventana.NombreFiltrar;

            IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoFiltrar);
            this._ventana.Asociados = asociados;
            Mouse.OverrideCursor = null;
        }


        /// <summary>
        /// Método que consulta la marca deseada en la ventana
        /// </summary>
        public void ConsultarMarca()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            Marca marcaFiltrar = new Marca();

            marcaFiltrar.Id = this._ventana.IdMarcaFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdMarcaFiltrar);
            marcaFiltrar.Descripcion = this._ventana.NombreMarcaFiltrar.Equals("") ? string.Empty : this._ventana.NombreMarcaFiltrar;

            if ((marcaFiltrar.Id != 0) || (!marcaFiltrar.Descripcion.Equals(string.Empty)))
            {
                IList<Marca> marcas = this._marcaServicios.ObtenerMarcasFiltro(marcaFiltrar);
                this._ventana.Marcas = marcas;
            }
            else
                this._ventana.Marcas = null;

            Mouse.OverrideCursor = null;
        }


        /// <summary>
        /// Método que cambia la marca seleccionada
        /// </summary>
        /// <returns></returns>
        public bool CambiarMarca()
        {
            bool retorno = false;
            if (this._ventana.Marca != null)
            {
                this._ventana.MarcaGeneral = this._ventana.Marca;

                IList<Asociado> asociados = new List<Asociado>();
                asociados.Add(((Marca)this._ventana.Marca).Asociado);

                this._ventana.Asociados = asociados;
                this._ventana.Asociado = ((Marca)this._ventana.Marca).Asociado;

                IList<Interesado> interesados = new List<Interesado>();
                interesados.Add(((Marca)this._ventana.Marca).Interesado);

                this._ventana.Interesados = interesados;
                this._ventana.Interesado = ((Marca)this._ventana.Marca).Interesado;
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
        /// Método que agrega una marca a la lista de marcas agregadas
        /// </summary>
        public void AgregarMarca()
        {
            try
            {
                if (this._ventana.Marca != null)
                {
                    _marcasAgregadas.Insert(0, ((Marca)this._ventana.Marca));
                    this._ventana.MarcasAgregadas = null;
                    this._ventana.MarcasAgregadas = _marcasAgregadas;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }


        /// <summary>
        /// Método que elimina una marca de la lista de marcas agregadas
        /// </summary>
        public void EliminarMarca()
        {
            try
            {
                if (this._ventana.MarcaAgregada != null)
                {
                    _marcasAgregadas.Remove(((Marca)this._ventana.MarcaAgregada));
                    this._ventana.MarcasAgregadas = null;
                    this._ventana.MarcasAgregadas = _marcasAgregadas;
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
        private StructReporteCarta16 ObtenerEstructuraCarta1()
        {
            StructReporteCarta16 retorno = new StructReporteCarta16();

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

            retorno.Asociado = ((Asociado)this._ventana.Asociado) != null ?
                ((Asociado)this._ventana.Asociado).Nombre : string.Empty;

            retorno.DomicilioAsociado = ((Asociado)this._ventana.Asociado) != null ?
                ((Asociado)this._ventana.Asociado).Domicilio : string.Empty;

            return retorno;
        }

        /// <summary>
        /// Método que se encarga de armar la lista de estructuras para hacer el reporte para varias marcas
        /// </summary>
        /// <returns></returns>
        private IList<StructReporteCarta16> ObtenerEstructuraCartas1()
        {
            IList<StructReporteCarta16> retorno = new List<StructReporteCarta16>();

            try
            {
                if (this._marcasAgregadas.Count > 0)
                {
                    foreach (Marca marca in this._marcasAgregadas)
                    {
                        StructReporteCarta16 estructura = new StructReporteCarta16();

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

                        estructura.Asociado = marca.Asociado != null ?
                            marca.Asociado.Nombre : string.Empty;

                        estructura.DomicilioAsociado = marca.Asociado != null ?
                            marca.Asociado.Domicilio : string.Empty;

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

        struct StructReporteCarta16
        {
            private string _fechaCarta;
            private string _domicilioAsociado;
            private string _asociado;



            public string FechaCarta
            {
                get { return _fechaCarta; }
                set { _fechaCarta = value; }
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
