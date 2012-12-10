using NLog;
using System;
using System.Configuration;
using NHibernate;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoCartaNHibernate : DaoBaseNHibernate<Carta, int>, IDaoCarta
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que consulta las cartas dado unos parametros
        /// </summary>
        /// <param name="carta">Carta con parametros solicitados</param>
        /// <returns>lista de cartas que cumplen los parametros</returns>
        public IList<Carta> ObtenerCartasFiltro(Carta carta)
        {
            IList<Carta> cartas = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCarta);

                //Por código
                if ((null != carta) && (carta.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaId, carta.Id);
                    variosFiltros = true;
                }

                //Por medio
                if (!string.IsNullOrEmpty(carta.Medio))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaMedio, carta.Medio);
                    variosFiltros = true;
                }

                //Por asociado
                if ((null != carta.Asociado) && (!carta.Asociado.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaIdAsociado, carta.Asociado.Id);
                    variosFiltros = true;
                }

                //Por contacto
                if ((null != carta.Contactos) && (carta.Contactos.Count != 0))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaNombreContacto, carta.Contactos[0].Nombre);
                    variosFiltros = true;
                }

                //Por referencia
                if ((null != carta.Referencia) && (!carta.Referencia.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaReferencia, carta.Referencia.ToUpper());
                }

                //Por departamento
                if ((null != carta.Departamento) && (!carta.Departamento.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaIdDepartamento, carta.Departamento.Id);
                    variosFiltros = true;
                }

                //Por resumen
                if ((null != carta.Resumen) && (!carta.Resumen.Descripcion.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaResumen, carta.Resumen.Descripcion);
                }

                //Por fecha
                if ((null != carta.Fecha) && (!carta.Fecha.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", carta.Fecha);
                    string fecha2 = String.Format("{0:dd/MM/yy}", carta.Fecha.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaFecha, fecha, fecha2);
                }

                //Por fecha
                if ((null != carta.AnexoFecha) && (carta.AnexoFecha.Value != null) && (!carta.AnexoFecha.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", carta.AnexoFecha);
                    string fecha2 = String.Format("{0:dd/MM/yy}", carta.AnexoFecha.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaFechaAnexo, fecha, fecha2);
                }

                //Por tracking
                if ((null != carta.Tracking) && (!carta.Tracking.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaTracking, carta.Tracking.ToUpper());
                }

                //Por tracking anexo
                if ((null != carta.AnexoTracking) && (!carta.AnexoTracking.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaAnexoTracking, carta.AnexoTracking.ToUpper());
                }

                IQuery query = Session.CreateQuery(cabecera + filtro);
                cartas = query.List<Carta>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodos);
            }
            finally
            {
                Session.Close();
            }
            return cartas;
        }


        /// <summary>
        /// Metodo que inserta una carta en la base de datos
        /// </summary>
        /// <param name="carta">parametro a insertar</param>
        /// <param name="transaccion">objetoITransicion indica si se realizo el Commit</param>
        /// <returns>Bool si se inserto correctamente, de lo contrario false</returns>
        public bool Insertar(Carta carta, ITransaction transaccion)
        {
            bool exitoso;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Session.Save(carta);
                transaccion.Commit();
                exitoso = transaccion.WasCommitted;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExInsertarOModificar);
            }
            finally
            {
                Session.Close();
            }

            return exitoso;
        }
    }
}
