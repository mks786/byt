﻿using NLog;
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
                if ((null != carta) && (carta.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaId, carta.Id);
                    variosFiltros = true;
                }
                if ((null != carta.Asociado) && (!carta.Asociado.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaIdAsociado, carta.Asociado.Id);
                    variosFiltros = true;
                }
                if ((null != carta.Resumen) && (!carta.Resumen.Descripcion.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaResumen, carta.Resumen.Descripcion);
                }
                if ((null != carta.Fecha) && (!carta.Fecha.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", carta.Fecha);
                    string fecha2 = String.Format("{0:dd/MM/yy}", carta.Fecha.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaFecha, fecha, fecha2);
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
        /// Método que inserta o modifica una entidad
        /// </summary>
        /// <param name="carta">Entidad a modificar</param>
        /// <returns>True si fue éxitoso la inserción o modificación, en caso contrario False</returns>
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
