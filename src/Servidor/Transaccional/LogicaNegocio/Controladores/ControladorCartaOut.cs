using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using System.Collections;
using System.Transactions;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorCartaOut : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que consulta la lista de todos los boletines
        /// </summary>
        /// <returns>Lista con todos los boletines</returns>
        public static IList<CartaOut> ConsultarTodos()
        {
            IList<CartaOut> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CartaOut>> comando = FabricaComandosCartaOut.ObtenerComandoConsultarTodos();
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

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

            return retorno;
        }


        public static IList<CartaOut> ConsultarCartasOutsFiltro(CartaOut carta)
        {
            IList<CartaOut> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CartaOut>> comando = FabricaComandosCartaOut.ObtenerComandoConsultarCartasOutsFiltro(carta);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

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

            return retorno;
        }
        public static bool TransferirPlantilla(IList<CartaOut> cartasOut)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Carta> cartas = TransformarCartas(cartasOut);
            ComandoBase<bool> comando = FabricaComandosCartaOut.ObtenerComandoTransferirPlantilla(cartas, cartasOut);
            comando.Ejecutar();
            return comando.Receptor.ObjetoAlmacenado;

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion



        }
        private static IList<Carta> TransformarCartas(IList<CartaOut> cartasOut)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Carta> retorno = new List<Carta>();
            foreach (CartaOut cartaOut in cartasOut)
            {
                Carta carta = new Carta();
                Asociado asociado = new Asociado();
                Resumen resumen = new Resumen();
                Asignacion asignacion = new Asignacion();

                Usuario responsable = new Usuario();
                responsable.Iniciales = carta.Iniciales;
                resumen.Id = "REV";

                if (cartaOut.Asociado == 0)
                    asociado.Id = 999999;
                else
                    asociado.Id = cartaOut.Asociado;

                carta.Asociado = asociado;
                carta.Iniciales = cartaOut.Iniciales;
                carta.Persona = cartaOut.Persona;
                //carta.FechaAlt = cartaOut.FechaCorreo
                carta.Fecha = cartaOut.FechaIngreso.Value;

                String fechaAux = TraducirFecha(cartaOut.FechaIngreso);
                carta.FechaAlt = fechaAux;
                carta.FechaReal = fechaAux;

                if (cartaOut.TipoEmail.Equals('I'))
                    carta.Acuse = 'S';
                if (cartaOut.TipoEmail.Equals('C'))
                    carta.Acuse = 'E';
                if (cartaOut.TipoEmail.Equals(""))
                    carta.Acuse = 'S';


                //Hashtable tabla = new Hashtable();
                //tabla["I"] = "S";
                //tabla.H

                carta.Id = cartaOut.NRelacion;
                carta.Medio = "EML";
                carta.Referencia = cartaOut.SubjectOrganizacion;
                carta.Resumen = resumen;
                asignacion.Responsable = responsable;
                carta.Asignaciones = new List<Asignacion>();
                carta.Asignaciones.Add(asignacion);
                carta.Receptor = cartaOut.Iniciales;
                // ENTRADA.asignacion = nrelacion.cor_mail_out
                carta.Departamento = new Departamento();
                carta.Departamento.Id = cartaOut.Departamento;

                retorno.Add(carta);
            }

            return retorno;

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        private static string TraducirFecha(DateTime? fecha)
        {
            string retorno = "";
            if (null != fecha)
            {
                string dia = fecha.Value.Day.ToString();
                if (fecha.Value.Day < 10)
                {
                    dia = "0" + fecha.Value.Day;
                }
                retorno = dia + "/" + fecha.Value.Month + "/" + fecha.Value.Year;
            }
            return retorno;
        }
    }
}

