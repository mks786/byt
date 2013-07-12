using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Collections;
using NHibernate.Criterion;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoAsociadoNHibernate : DaoBaseNHibernate<Asociado, int>, IDaoAsociado
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Consulta un Asociado coon toda su informacion
        /// </summary>
        /// <param name="asociado">Asociado con parametros</param>
        /// <returns>Asociado </returns>
        public Asociado ObtenerAsociadoConTodo(Asociado asociado)
        {
            Asociado retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Ejecutar package antes de obtener el Asociado con todo
                /*String nombreProcedimiento = "PCK_TALCOSA";
                ParametroProcedimiento parametro = new ParametroProcedimiento(nombreProcedimiento);
                */
                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerAsociadoConTodo, asociado.Id));
                retorno = query.UniqueResult<Asociado>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAsociadoConTodo);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }


        /// <summary>
        /// Consulta los asociados dado unos parametros determinados
        /// </summary>
        /// <param name="asociado">asociado con parametros</param>
        /// <returns>lista de asociados</returns>
        public IList<Asociado> ObtenerAsociadosFiltro(Asociado asociado)
        {

            IList<Asociado> asociados = null;
            String flag = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAsociado);

                //if ((null != asociado) && (asociado.Id != 0))
                if ((null != asociado) && (asociado.Id != int.MinValue) && (asociado.Id !=0))
                {

                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoId, asociado.Id);
                    variosFiltros = true;
                }
                


                if (!string.IsNullOrEmpty(asociado.Nombre))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoNombre, asociado.Nombre.ToUpper());
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(asociado.Domicilio))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoDomicilio, asociado.Domicilio);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(asociado.Email))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoEmail, asociado.Email);
                    variosFiltros = true;
                }

                if (!asociado.TipoPersona.Equals(char.MinValue))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoTipoPersona, asociado.TipoPersona);
                    variosFiltros = true;
                }

                if ((null != asociado.Pais) && (asociado.Pais.Id != int.MinValue))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoPais, asociado.Pais.Id);
                    variosFiltros = true;
                }

                if ((null != asociado.Idioma) && (!asociado.Idioma.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoIdioma, asociado.Idioma.Id);
                    variosFiltros = true;
                }

                if ((null != asociado.Moneda) && (!asociado.Moneda.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoMoneda, asociado.Moneda.Id);
                    variosFiltros = true;
                }

                if ((null != asociado.TipoCliente) && (!asociado.TipoCliente.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoTipoCliente, asociado.TipoCliente.Id);
                    variosFiltros = true;
                }

                if ((null != asociado.Tarifa) && (!asociado.Tarifa.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoTarifa, asociado.Tarifa.Id);
                    variosFiltros = true;
                }

                if ((null != asociado.Etiqueta) && (!asociado.Etiqueta.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoEtiqueta, asociado.Etiqueta.Id);
                    variosFiltros = true;
                }

                if ((null != asociado.DetallePago) && (!asociado.DetallePago.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoDetallePago, asociado.DetallePago.Id);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(asociado.ValorQuery))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += asociado.ValorQuery;
                    variosFiltros = true;
                }


                //Validacion que se realiza cuando el codigo del Asociado = 0 
                if ((filtro.Equals(String.Empty) || filtro.Equals("")) && (asociado.Id == 0))
                {
                    flag = "el filtro viene vacio";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoId, asociado.Id);
                }

                IQuery query = Session.CreateQuery(cabecera + filtro);
                asociados = query.List<Asociado>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAsociadosFiltro);
            }
            finally
            {
                Session.Close();
            }
            return asociados;
        }


        public bool VerificarCartasDeAsociado(Asociado asociado)
        {
            long retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.VerificarCartasPorAsociado, asociado.Id));
                retorno = query.UniqueResult<long>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAsociadoConTodo);
            }
            finally
            {
                Session.Close();
            }


            return retorno.Equals(string.Empty) ? false : true;
        }


        /// <summary>
        /// Método que consulta los contactos de un asociado a traves de una vista
        /// </summary>
        /// <param name="asociado"></param>
        /// <returns></returns>
        public IList<ContactosDelAsociadoVista> ObtenerContactosDelAsociado(Asociado asociado, bool todos)
        {
            IList<ContactosDelAsociadoVista> recordatorios = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string filtro = "";

                if (!todos)
                    filtro += " and c.Tipo = 'A'";

                string cabecera = string.Format(Recursos.ConsultasHQL.ObtenerContactosPorAsociadoVista, asociado.Id);

                IQuery query = Session.CreateQuery(cabecera + filtro);
                recordatorios = query.List<ContactosDelAsociadoVista>();



                //}

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMarcasPorFechaRenovacion);
            }
            finally
            {
                Session.Close();
            }
            return recordatorios;
        }


        /// <summary>
        /// Método que consulta los contactos de un asociado a traves de una vista
        /// </summary>
        /// <param name="asociado"></param>
        /// <returns></returns>
        public IList<EmailAsociado> ObtenerEmailsDelAsociado(Asociado asociado)
        {
            IList<EmailAsociado> emails = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string filtro = "";

                string cabecera = string.Format(Recursos.ConsultasHQL.ObtenerEmailsPorAsociado, asociado.Id);

                IQuery query = Session.CreateQuery(cabecera + filtro);
                emails = query.List<EmailAsociado>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMarcasPorFechaRenovacion);
            }
            finally
            {
                Session.Close();
            }
            return emails;
        }


        public bool EjecutarProcedimiento(ParametroProcedimiento parametro)
        {
            bool retorno = true;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                /*
                IQuery query = Session.GetNamedQuery(parametro.PaqueteProcedimiento + parametro.NombreProcedimiento);
                query.SetParameter<string>("usr", parametro.Usuario.Iniciales);
                query.SetParameter<int>("way", parametro.Via);
                query.SetParameter<int>("cod", parametro.Id);

                query.UniqueResult();*/

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                retorno = false;
                logger.Error(ex.Message);
            }
            finally
            {
                Session.Close();
            }
            return retorno;
        }

    }
}
