using System;
using System.IO;
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
using Trascend.Bolet.Cliente.Contratos.Recordatorios;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Recordatorios;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Recordatorios
{
    class PresentadorConsultarRecordatorios : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IList<ListaDatosValores> _listaRecordatorios;
        private IConsultarRecordatorios _ventana;
        private IMarcaServicios _marcaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IList<Marca> _marcas;
        private IList<RecordatorioVista> _recordatorios;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarRecordatorios(IConsultarRecordatorios ventana)
        {
            try
            {
                this._ventana = ventana;
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarRecordatorios,
                Recursos.Ids.ConsultarRecordatorios);
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

                CargarTipoRecordatorio();

                //Marca MarcaAuxiliar = new Marca();
                //ListaDatosValores listaAuxiliar = this.BuscarRecordatorio(_listaRecordatorios, (ListaDatosValores)this._ventana.Recordatorio);                
                //MarcaAuxiliar.Recordatorio = int.Parse(listaAuxiliar.Valor);

                //this._marcas = this._marcaServicios.ObtenerMarcasFiltro(MarcaAuxiliar);

                this._ventana.TotalHits = "0";
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
        /// Método que carga los datos iniciales de TipoRecordatorio a mostrar en la página
        /// </summary>
        private void CargarTipoRecordatorio()
        {


            ListaDatosValores filtro = new ListaDatosValores(Recursos.Etiquetas.cbiRecordatorio);
            this._listaRecordatorios =
                this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(filtro);
            this._ventana.Recordatorios = this._listaRecordatorios;
            this._ventana.Recordatorio = this.BuscarRecordatorio(this._listaRecordatorios, this._listaRecordatorios.ElementAt(0));
        }

        public void ActualizarMarcasRecordatorio()
        {
            this._ventana.LimpiarFiltros();

            Marca MarcaAuxiliar = new Marca();
            ListaDatosValores listaAuxiliar = this.BuscarRecordatorio(this._listaRecordatorios, (ListaDatosValores)this._ventana.Recordatorio);
            MarcaAuxiliar.Recordatorio = int.Parse(listaAuxiliar.Valor);

            DateTime[] fechas = this.ObtenerFechaFinRecordatorio();
            RecordatorioVista recordatorio = new RecordatorioVista();

            if (this._ventana.AutomaticoFiltro.Value)
            {
                this._recordatorios = this._marcaServicios.ConsultarRecordatoriosVistaMarca(recordatorio, fechas);
            }
            else
            {
                recordatorio.Marca = MarcaAuxiliar;
                this._recordatorios = this._marcaServicios.ConsultarRecordatoriosVistaMarca(recordatorio, fechas);
            }

            IEnumerable<RecordatorioVista> recordatorios = this._recordatorios;

            if (this._ventana.AutomaticoFiltro.Value)
            {
                recordatorios = from m in recordatorios
                                    where fechas[0] < recordatorio.Marca.FechaRenovacion && recordatorio.Marca.FechaRenovacion <= fechas[1] &&
                                    recordatorio.Marca.Recordatorio != MarcaAuxiliar.Recordatorio
                                    select m;
                //marcasDesinfladas = from m in marcasDesinfladas
                //                    select m;
            }

            this._ventana.GestionarEnableChecksFiltro(false);

            this._ventana.Resultados = _recordatorios;
            this._ventana.TotalHits = _recordatorios.ToList().Count.ToString();
        }

        /// <summary>
        /// Método que devuelve la fecha fin dependiendo de la configuración
        /// </summary>
        /// <returns>DateTime fechaFin a filtrar</returns>
        private DateTime[] ObtenerFechaFinRecordatorio()
        {
            int tiempo = 0;
            DateTime[] fechas = new DateTime[2];

            if (((ListaDatosValores)this._ventana.Recordatorio).Valor == "0")
            {
                tiempo = int.Parse(ConfigurationManager.AppSettings["Recordatorio"]);
            }
            else if (((ListaDatosValores)this._ventana.Recordatorio).Valor == "1")
            {
                tiempo = int.Parse(ConfigurationManager.AppSettings["PrimerRecordatorio"]);
            }
            else if (((ListaDatosValores)this._ventana.Recordatorio).Valor == "2")
            {
                tiempo = int.Parse(ConfigurationManager.AppSettings["SegundoRecordatorio"]);
            }
            else if (((ListaDatosValores)this._ventana.Recordatorio).Valor == "3")
            {
                tiempo = int.Parse(ConfigurationManager.AppSettings["UltimoRecordatorio"]);
            }

            fechas[0] = System.DateTime.Now.AddMonths(tiempo - 1);
            fechas[1] = System.DateTime.Now.AddMonths(tiempo);


            return fechas;
        }

        /// <summary>
        /// Método que realiza una consulta al servicio, con el fin de filtrar los datos que se muestran 
        /// por pantalla
        /// </summary>
        public void Consultar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                int filtroValido = 0;//Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                //dos filtros sean utilizados

                ListaDatosValores filtro = new ListaDatosValores(Recursos.Etiquetas.cbiRecordatorio);

                IEnumerable<Marca> marcasDesinfladas = this._marcas;

                IEnumerable<RecordatorioVista> recordatoriossDesinflados = this._recordatorios;



                if (this._ventana.TodosFiltro.Value)
                {
                    recordatoriossDesinflados = from m in recordatoriossDesinflados
                                        select m;
                }
                else
                {
                    if (this._ventana.EmailFiltro.Value)
                    {
                        //marcasDesinfladas = from m in marcasDesinfladas
                        //                    where m.Asociado.Email != null &&
                        //                    m.Asociado.Fax1 == null
                        //                    select m;

                        recordatoriossDesinflados = from m in recordatoriossDesinflados
                                            where m.Asociado.Email != null
                                            select m;
                        filtroValido = 1;
                    }

                    if (this._ventana.FaxFiltro.Value)
                    {
                        recordatoriossDesinflados = from m in recordatoriossDesinflados
                                            where m.Asociado.Fax1 != null &&
                                            m.Asociado.Email == null
                                            select m;

                        filtroValido = 1;
                    }

                    if ((!this._ventana.FaxFiltro.Value) && (!this._ventana.EmailFiltro.Value))
                    {
                        recordatoriossDesinflados = from m in recordatoriossDesinflados
                                            where m.Asociado.Fax1 == null &&
                                            m.Asociado.Email == null
                                            select m;
                        filtroValido = 1;
                    }
                }

                if (!this._ventana.AnoFiltro.Equals(""))
                {
                    recordatoriossDesinflados = from m in recordatoriossDesinflados
                                        where m.FechaRenovacion1.Value.Year == int.Parse(this._ventana.AnoFiltro)
                                        select m;
                    filtroValido = 1;

                }

                if (!this._ventana.MesFiltro.Equals(""))
                {
                    recordatoriossDesinflados = from m in recordatoriossDesinflados
                                        where m.FechaRenovacion1.Value.Month == int.Parse(this._ventana.MesFiltro)
                                        select m;
                    filtroValido = 1;
                }

                if (this._ventana.FechaDesdeFiltro.HasValue)
                {
                    recordatoriossDesinflados = from m in recordatoriossDesinflados
                                        where m.FechaRenovacion1.Value >= this._ventana.FechaDesdeFiltro
                                        select m;
                    filtroValido = 1;
                }

                if (this._ventana.FechaHastaFiltro.HasValue)
                {
                    recordatoriossDesinflados = from m in recordatoriossDesinflados
                                        where m.FechaRenovacion1.Value <= this._ventana.FechaHastaFiltro
                                        select m;
                    filtroValido = 1;
                }


                this._ventana.Resultados = recordatoriossDesinflados;
                this._ventana.TotalHits = recordatoriossDesinflados.ToList<RecordatorioVista>().Count.ToString();


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
        /// Método que se invoca al GenerarInformacio, crea el archivo renmarca.txt
        /// </summary>
        public void GenerarInformacion()
        {
            string rutaArchivo = ConfigurationManager.AppSettings["TxtPath"];

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (File.Exists(rutaArchivo))
                    File.Delete(rutaArchivo);

                File.WriteAllText(rutaArchivo, this.GenerarCadena());

                string comando = ConfigurationManager.AppSettings["ComandoRecordatorio"];

                //this.EjecutarComandoDeConsola(comando, "Generar Recordatorios con plantilla de word");

                this.AbrirArchivoPorConsola(comando, "Generar Recordatorios con plantilla de word");

                this.ActualizarNRecordatorio();

                this.ActualizarMarcasRecordatorio();

                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ExitoGenerandoInformacionRecordatorio, rutaArchivo), 2);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (IOException ex)
            {
                logger.Debug(ex.Message);
                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorGenerandoInformacionRecordatorio, rutaArchivo), 0);
            }
        }

        /// <summary>
        /// Método que se encarga de actualizar el nrecordatorio luego de que se generara la información
        /// </summary>
        private void ActualizarNRecordatorio()
        {
            for (int i = 0; i < this._recordatorios.Count; i++)
			{
                Marca marcaAux = new Marca();

                marcaAux = this._recordatorios[i].Marca;
                marcaAux.Operacion = "MODIFY";
                
                marcaAux.Recordatorio = this._recordatorios[i].Marca.Recordatorio.Equals((int?)null) ? 0 : this._recordatorios[i].Marca.Recordatorio;
                marcaAux.Recordatorio = marcaAux.Recordatorio + 1;

                this._marcaServicios.InsertarOModificar(marcaAux, UsuarioLogeado.Hash);
			 
			}
            
            //foreach (Marca marcaRecordatorio in this._marcas)
            //{
            //    marcaRecordatorio.Operacion = "MODIFY";

            //    marcaRecordatorio.Recordatorio = marcaRecordatorio.Recordatorio.Equals((int?)null) ? 0 : marcaRecordatorio.Recordatorio;
            //    marcaRecordatorio.Recordatorio = marcaRecordatorio.Recordatorio + 1;

            //    this._marcaServicios.InsertarOModificar(marcaRecordatorio, UsuarioLogeado.Hash);
            //}

        }

        /// <summary>
        /// Método que crea la cadena para generar información
        /// </summary>
        /// <returns>Cadena lista para generar el archivo txt</returns>
        private string GenerarCadena()
        {
            string cabecera = "";
            string cadena = "";

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                string fax = "";
                string recordatorioAux = "";

                cabecera = "Asociado|Nombre|Fax|E_mail|Marca|Interesado|Freno|Recordatorio|Pais|Idioma|Dir|cregistro|clase|Fgracia|" +
                                  "frenovacion_in|fechagra_in|cmarca" + "\r\n";

                string[] tipoRecordatorio = this.ValidarTipoRecordatorio();

                if (((IList<object>)this._ventana.Resultado).Count != 0)
                {
                    this._recordatorios = new List<RecordatorioVista>();

                    IList<object> resultadoAux = (IList<object>)this._ventana.Resultado;

                    foreach (RecordatorioVista recordatorio in resultadoAux)
                    {
                        this._recordatorios.Add(recordatorio);
                    }

                }
                //else
                //  marcaAux = this._marcas;

                foreach (RecordatorioVista recordatorio in this._recordatorios)
                {

                    recordatorioAux = recordatorio.Asociado.Idioma.Id.Equals("IN") ? tipoRecordatorio[1] : tipoRecordatorio[0];

                    string claseAux = "";
                    if(recordatorio.Marca != null)
                        claseAux = recordatorio.Marca.Nacional != null ? recordatorio.Marca.Nacional.Descripcion : string.Empty;

                    if (recordatorio.Asociado != null)
                    {
                        if (recordatorio.Asociado.Pais != null)
                        {
                            if (!recordatorio.Asociado.Pais.NombreEspanol.Equals("VENEZUELA"))
                                fax = "00" + recordatorio.Asociado.Fax1;
                        }
                    }

                    cadena = cadena + recordatorio.Asociado.Id + "|" + recordatorio.Asociado.Nombre + "|" + fax + "|" +
                             recordatorio.Asociado.Email + "|" + recordatorio.Marca.Descripcion + "|" + recordatorio.NombreInteresado + "|" +
                             recordatorio.FechaRenovacion + "|" + recordatorioAux + "|" + recordatorio.Pais + "|" +
                             recordatorio.Idioma + "|" + recordatorio.Direccion + "|" + recordatorio.Marca.CodigoRegistro +
                             "|" + recordatorio.Clase + "|" + recordatorio.FechaGracia + "|" +
                             recordatorio.FechaRenovacionIn + "|" + recordatorio.FechaGraciaIn + "|" + recordatorio.Id + "\r\n";
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (IOException ex)
            {
                logger.Debug(ex.Message);
            }

            return cabecera + cadena;
        }

        /// <summary>
        /// Método que guarda los valores en español e ingles del tiporecordatorio
        /// </summary>
        /// <returns>arreglo con el recordatorio en ingles[1] y español[0]</returns>
        private string[] ValidarTipoRecordatorio()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            string[] tipoRecordatorio = new string[2];

            if (((ListaDatosValores)this._ventana.Recordatorio).Valor == "0")
            {
                tipoRecordatorio[0] = "AVISO DE RENOVACION";
                tipoRecordatorio[1] = "RENEWAL NOTICE";
            }
            else if (((ListaDatosValores)this._ventana.Recordatorio).Valor == "1")
            {
                tipoRecordatorio[0] = "PRIMER RECORDATORIO";
                tipoRecordatorio[1] = "FIRST REIMNDER";
            }
            else if (((ListaDatosValores)this._ventana.Recordatorio).Valor == "2")
            {
                tipoRecordatorio[0] = "SEGUNDO RECORDATORIO";
                tipoRecordatorio[1] = "SECOND REMINDER";
            }
            else if (((ListaDatosValores)this._ventana.Recordatorio).Valor == "3")
            {
                tipoRecordatorio[0] = "URGENTE - ULTIMO RECORDATORIO";
                tipoRecordatorio[1] = "URGENT - LAST REMINDER";
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return tipoRecordatorio;
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

            this._ventana.AnoFiltro = null;
            this._ventana.MesFiltro = null;
            this._ventana.FechaDesdeFiltro = DateTime.MinValue;
            this._ventana.FechaHastaFiltro = DateTime.MinValue;
            this._ventana.Recordatorio = this._listaRecordatorios[0];
            this._ventana.AutomaticoFiltro = true;
            this._ventana.Resultado = null;

            this._ventana.GestionarEnableChecksFiltro(false);

            this.ActualizarMarcasRecordatorio();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
