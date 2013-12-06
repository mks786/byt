using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Data;
using Oracle.DataAccess.Client;


namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CartaOutServicios : MarshalByRefObject, ICartaOutServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que consulta todos los elementos de una entidad
        /// </summary>
        /// <returns>Lista de Entidades</returns>
        public IList<CartaOut> ConsultarTodos()
        {
            IList<CartaOut> cartaOut;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                cartaOut = ControladorCartaOut.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ": " + ex.Message);
            }
            return cartaOut;
        }


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public CartaOut ConsultarPorId(CartaOut carta)
        {
            throw new NotImplementedException();
        }


        //-------------------------------------------------------------
        /// <summary>
        /// Servicio que consulta por un campo determinado y 
        /// ordena en forma Ascendente o Descendente
        /// </summary>
        /// <param name="campo">Campo a filtrar</param>
        /// <param name="tipoOrdenamiento">Si el ordenamiento es Ascende o Descendente</param>
        /// <returns>Lista de Entidades</returns>
        public IList<CartaOut> ConsultarPorOtroCampo(String campo, String tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }
        //-------------------------------------------------------------


        /// <summary>
        /// Servicio que inserta o modifica a una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <param name="hash">Hash del usuario que inserta</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool InsertarOModificar(CartaOut carta, int hash)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que elimina a una entidad
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <param name="hash">hash del usuario que realiza la acción</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool Eliminar(CartaOut carta, int hash)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        public bool VerificarExistencia(CartaOut carta)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que se encarga de consultar las cartaOuts segun el filtro
        /// </summary>
        /// <param name="carta">CartaOut a buscar</param>
        /// <returns>Lista de Cartas Out que cumplan con el filtro</returns>
        public IList<CartaOut> ObtenerCartasOutsFiltro(CartaOut carta)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<CartaOut> cartas;

                cartas = ControladorCartaOut.ConsultarCartasOutsFiltro(carta);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return cartas;

            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ": " + ex.Message);
            }
        }


        /// <summary>
        /// Servicio que consulta la correspondencia de la tabla COR_MAIL_OUT mediante el uso de un DataSet
        /// ESTE SERVICIO SE CONECTA A LA BASE DE DATOS USANDO ADO.NET 
        /// </summary>
        /// <param name="carta">Correspondencia utilizada como filtro</param>
        /// <returns>Lista de Cartas Out que cumplan con el filtro</returns>
        public IList<CartaOut> ObtenerCartasOutsFiltroAdo(CartaOut carta)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<CartaOut> cartas = new List<CartaOut>();
                String query = String.Empty;
                DataTable cartasAdo = new DataTable();

                logger.Trace("***************** Iniciando proceso de Recuperacion de Registros COR_MAIL_OUT *****************");
                OracleConnection con = GenerarConexionBD();
                query = GenerarQuery(carta);
                con.Open();

                if (con.State.ToString().Equals("Open"))
                {
                    OracleDataAdapter oracleAdapter = new OracleDataAdapter(query, con);
                    oracleAdapter.Fill(cartasAdo);
                    logger.Trace("***************** Correos recuperados *****************");
                    oracleAdapter.Dispose();
                    con.Close();
                    if (cartasAdo.Rows.Count != 0)
                    {
                        logger.Trace("***************** Creando lista de correos a transferir *****************");
                        cartas = GenerarListaCartasOut(cartasAdo);
                        logger.Trace("***************** Lista de correos a transferir creada *****************");
                    }
                }

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return cartas;

            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ": " + ex.Message);
            }
        }

        

        

        /// <summary>
        /// Servicio que se encarga de realizar la transferencia de plantilla de una tabla (COR_MAIL_OUT) a otra (ENTRADA)
        /// </summary>
        /// <param name="cartas">Cartas a transferir</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public bool TransferirPlantilla(IList<CartaOut> cartas, int hash)
        {
            try
            {
                bool retorno = ControladorCartaOut.TransferirPlantilla(cartas, hash);
                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }


        #region Metodos Privados

        /// <summary>
        /// Metodo que se configura un objeto conexion a base de datos para luego ejecutar un query sobre la misma
        /// </summary>
        /// <returns></returns>
        private OracleConnection GenerarConexionBD()
        {

            OracleConnection connection = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String dataSource = ConfigurationManager.AppSettings["DataSourceADO"].ToString();
                String userAdoDB = ConfigurationManager.AppSettings["UserAdoDB"].ToString();
                String passwordAdoDB = ConfigurationManager.AppSettings["PasswordAdoDB"].ToString();
                String connectionString = "Data Source=" + dataSource + " User Id=" + userAdoDB + ";Password=" + passwordAdoDB + ";";
                String query = String.Empty;

                connection = new OracleConnection(connectionString);


                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor + ": " + ex.Message);
            }

            return connection;
        }

        /// <summary>
        /// Metodo que parsea los campos filtros de la CartaOut filtro y genera la cadena con el query a aplicar sobre la tabla COR_MAIL_OUT
        /// </summary>
        /// <param name="carta">CartaOut filtro</param>
        /// <returns>Cadena que define el query a ejecutar sobre la tabla COR_MAIL_OUT</returns>
        private string GenerarQuery(CartaOut carta)
        {

            String cadenaQuery = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String cabeceraQueryCorMailOut = "SELECT IDX as \"Id\", XFROM as \"From\", XCC \"ConCopia\", XCCO \"ConCopiaOculta\", SUBJECT \"Asunto\", FECHA_COR \"FechaCorreo\", FECHA_ING \"FechaIngreso\", FECHA \"Fecha\", CUERPO \"Cuerpo\", STATUS \"Status\", NRELACION \"NRelacion\", TIPO_EMAIL \"TipoEmail\", CASOCIADO \"Asociado\", XCLAVE \"Clave\", XATTLIS \"Attlis\", NSEC \"NSec\", XFROM_ORG \"FromOrganizacion\", XTO_ORG \"ToOrganizacion\", XCC_ORG \"ConCopiaOrganizacion\", SUBJECT_ORG \"SubjectOrganizacion\", CODDPTO \"Departamento\", RESPONSABLE \"Iniciales\", PERSONA \"Persona\", XFROM_DET \"FromDetalle\" FROM COR_MAIL_OUT WHERE ";
                String filtro = String.Empty;
                
                bool variosFiltros = false;

                if ((null != carta) && (carta.Status != null))
                {
                    //filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaOutStatus, carta.Status);
                    filtro = " STATUS ='" + carta.Status + "' ";
                    variosFiltros = true;
                }
                if ((null != carta) && (null != carta.Id) && (!carta.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    //filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaOutId, carta.Id);
                    filtro += "IDX ='" + carta.Id + "' ";
                    variosFiltros = true;
                }
                if (carta.Asociado != 0)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    //filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaIdAsociado, carta.Asociado);
                    filtro += "ASOCIADO =" + carta.Asociado;
                    variosFiltros = true;
                }
                if ((null != carta.Fecha) && (!carta.Fecha.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", carta.Fecha);
                    DateTime fechaPrueba = Convert.ToDateTime(fecha);
                    string fecha2 = String.Format("{0:dd/MM/yy}", fechaPrueba.AddDays(1));
                    //filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaOutFecha, fecha, fecha2);
                    filtro += "FECHA_ING BETWEEN ('" + fecha + "' AND '" + fecha2 + "')";
                }

                cadenaQuery = cabeceraQueryCorMailOut + filtro;


                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return cadenaQuery;
        }

        /// <summary>
        /// Metodo que genera una lista de CartasOut a partir del contenido de un DataTable 
        /// </summary>
        /// <param name="cartasAdo"></param>
        /// <returns></returns>
        private IList<CartaOut> GenerarListaCartasOut(DataTable cartasAdo)
        {

            IList<CartaOut> cartasOut = new List<CartaOut>();
            

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                String prueba = String.Empty;

                foreach (DataRow registro in cartasAdo.Rows)
                {
                    logger.Trace("Correo de IDX: " + registro["Id"].ToString() + " a procesar");
                    CartaOut cartaOutAux = new CartaOut();
                    cartaOutAux.Id= registro["Id"].ToString();
                    cartaOutAux.From = registro["From"].ToString();
                    cartaOutAux.ConCopia = registro["ConCopia"].ToString();
                    cartaOutAux.ConCopiaOCulta = registro["ConCopiaOculta"].ToString();
                    cartaOutAux.Asunto = registro["Asunto"].ToString();
                    cartaOutAux.FechaCorreo = DateTime.Parse(registro["FechaCorreo"].ToString());
                    cartaOutAux.FechaIngreso = DateTime.Parse(registro["FechaIngreso"].ToString());
                    cartaOutAux.Fecha = registro["Fecha"].ToString();
                    cartaOutAux.Cuerpo = registro["Cuerpo"].ToString();
                    cartaOutAux.NRelacion = Int32.Parse(registro["NRelacion"].ToString());
                    cartaOutAux.TipoEmail = Convert.ToChar(registro["TipoEmail"].ToString());
                    cartaOutAux.Asociado = !registro["Asociado"].ToString().Equals("") ? Int32.Parse(registro["Asociado"].ToString()) : 0;
                    cartaOutAux.Clave  = registro["Clave"].ToString();
                    cartaOutAux.AttLis = registro["AttLis"].ToString();
                    cartaOutAux.NSec = !registro["NSec"].ToString().Equals("") ? Int32.Parse(registro["NSec"].ToString()) : 0;
                    cartaOutAux.FromOrganizacion = registro["FromOrganizacion"].ToString();
                    cartaOutAux.ToOrganizacion = registro["ToOrganizacion"].ToString();
                    cartaOutAux.ConCopiaOrganizacion = registro["ConCopiaOrganizacion"].ToString();
                    cartaOutAux.SubjectOrganizacion = registro["SubjectOrganizacion"].ToString();
                    cartaOutAux.Departamento = registro["Departamento"].ToString();
                    cartaOutAux.Iniciales = registro["Iniciales"].ToString();
                    cartaOutAux.Persona = registro["Persona"].ToString();
                    cartaOutAux.FromDetalle = registro["FromDetalle"].ToString();

                    cartasOut.Add(cartaOutAux);

                    logger.Trace("Correo de IDX: " + registro["Id"].ToString() + " procesado");

                }

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return cartasOut;
        }

        #endregion


    }
}
