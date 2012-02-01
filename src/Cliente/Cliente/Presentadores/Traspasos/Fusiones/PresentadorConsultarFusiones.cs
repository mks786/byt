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
using Trascend.Bolet.Cliente.Contratos.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Traspasos.Fusiones
{
    class PresentadorConsultarFusiones : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarFusiones _ventana;
        private IMarcaServicios _marcaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IList<Fusion> _fusiones;
        private IList<Marca> _marcas;
        private IList<Interesado> _interesados;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarFusiones(IConsultarFusiones ventana)
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
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarcas,
                Recursos.Ids.ConsultarMarcas);
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

                //this._marcas = this._marcaServicios.ConsultarTodos();
                //this._ventana.Resultados = this._marcas;

                //IList<Marca> marcas = this._marcaServicios.ConsultarTodos();
                //Marca primerMarca = new Marca();
                //primerMarca.Id = int.MinValue;
                //marcas.Insert(0, primerMarca);
                //this._ventana.Marcas = marcas;
                //this._marcas = marcas;

                //IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
                //Interesado primerInteresado = new Interesado();
                //primerInteresado.Id = int.MinValue;
                //interesados.Insert(0, primerInteresado);
                //this._ventana.Interesados = interesados;
                //this._interesados = interesados;

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
                bool consultaResumen = false;
                int filtroValido = 0;//Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                //dos filtros sean utilizados

                Marca MarcaAuxiliar = new Marca();

                if (!this._ventana.Id.Equals(""))
                {
                    filtroValido = 2;
                    MarcaAuxiliar.Id = int.Parse(this._ventana.Id);
                }

                //if ((null != this._ventana.Asociado) && (((Asociado)this._ventana.Asociado).Id != int.MinValue))
                //{
                //    MarcaAuxiliar.Asociado = (Asociado)this._ventana.Asociado;
                //    filtroValido++;
                //}

                //if ((null != this._ventana.Interesado) && (((Interesado)this._ventana.Interesado).Id != int.MinValue))
                //{
                //    MarcaAuxiliar.Interesado = (Interesado)this._ventana.Interesado;
                //    filtroValido++;
                //}


                //if (!this._ventana.FichasFiltrar.Equals(""))
                //{
                //    MarcaAuxiliar.Fichas = this._ventana.FichasFiltrar.ToUpper();
                //    filtroValido++;
                //}


                //if (!this._ventana.DescripcionFiltrar.Equals(""))
                //{
                //    filtroValido = 2;
                //    MarcaAuxiliar.Descripcion = this._ventana.DescripcionFiltrar.ToUpper();
                //}

                if (!this._ventana.Fecha.Equals(""))
                {
                    DateTime fechaPublicacion = DateTime.Parse(this._ventana.Fecha);
                    filtroValido = 2;
                    MarcaAuxiliar.FechaPublicacion = fechaPublicacion;
                }

                if (filtroValido >= 2)
                {
                    //this._fusiones = this._marcaServicios.ObtenerMarcasFiltro(MarcaAuxiliar);

                    //IList<Marca> marcasDesinfladas = new List<Marca>();

                    //foreach (var marca in this._fusiones)
                    //{
                    //    MarcaAuxiliar = new Marca(marca.Id);
                    //    Asociado asociadoAuxiliar = new Asociado();
                    //    Interesado interesadoAuxiliar = new Interesado();

                    //    MarcaAuxiliar.Descripcion = marca.Descripcion != null ? marca.Descripcion : "";

                    //    if ((marca.Asociado != null) && (!string.IsNullOrEmpty(marca.Asociado.Nombre)))
                    //    {
                    //        asociadoAuxiliar.Nombre = marca.Asociado.Nombre;
                    //        MarcaAuxiliar.Asociado = asociadoAuxiliar;
                    //    }

                    //    if ((marca.Interesado != null) && (!string.IsNullOrEmpty(marca.Interesado.Nombre)))
                    //    {
                    //        interesadoAuxiliar.Nombre = marca.Interesado.Nombre;
                    //        MarcaAuxiliar.Interesado = interesadoAuxiliar;
                    //    }

                    //    MarcaAuxiliar.FechaPublicacion = marca.FechaPublicacion != null ? marca.FechaPublicacion : null;

                    //    marcasDesinfladas.Add(MarcaAuxiliar);

                    //}

                    //this._ventana.Resultados = marcasDesinfladas;
                    //this._ventana.TotalHits = marcasDesinfladas.Count.ToString();
                    //if (marcasDesinfladas.Count == 0)
                    //    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados,1);
                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto,0);

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
        /// Método que invoca una nueva página "ConsultarPoder" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarFusion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.FusionSeleccionada != null)
            {
                Fusion fusionParaNavegar = null;
                bool encontrada = false;
                int cont = 0;
                while (!encontrada)
                {
                    Fusion fusion = this._fusiones[cont];
                    if (fusion.Id == ((Marca)this._ventana.FusionSeleccionada).Id)
                    {
                        fusionParaNavegar = fusion;
                        encontrada = true;
                    }
                    cont++;
                }


                this.Navegar(new ConsultarMarca(fusionParaNavegar));
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

        public void BuscarMarca()
        {
            Marca marca = new Marca();
            marca.Descripcion = this._ventana.NombreMarcaFiltrar.ToUpper();
            marca.Id = this._ventana.IdMarcaFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdMarcaFiltrar);

            IEnumerable<Marca> marcasFiltradas = this._marcaServicios.ObtenerMarcasFiltro(marca);

            if (marcasFiltradas.ToList<Marca>().Count != 0)
                this._ventana.Marcas = marcasFiltradas.ToList<Marca>();
            else
                this._ventana.Marcas = this._marcas;
        }

    }
}
