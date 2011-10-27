using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Dao.NHibernate;

namespace Trascend.Bolet.AccesoDatos.Fabrica
{
    public class FabricaDaoNHibernate: FabricaDaoBase
    {
        private static FabricaDaoNHibernate _instancia;

        /// <summary>
        /// Propiedad que devuelve la instancia de la clase
        /// </summary>
        public static FabricaDaoNHibernate ObtenerInstancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new FabricaDaoNHibernate();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        private FabricaDaoNHibernate()
        {
            /*Singleton*/
        }

        /// <summary>
        /// Método que devuelve el DaoAuditoria
        /// </summary>
        /// <returns>IDaoAuditoria</returns>
        public override IDaoAuditoria ObtenerDaoAuditoria()
        {
            return new DaoAuditoriaNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoAgente
        /// </summary>
        /// <returns>IDaoAgente</returns>
        public override IDaoAgente ObtenerDaoAgente()
        {
            return new DaoAgenteNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoDepartamento
        /// </summary>
        /// <returns>IDaoDepartamento</returns>
        public override IDaoDepartamento ObtenerDaoDepartamento()
        {
            return new DaoDepartamentoNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoEstado
        /// </summary>
        /// <returns>IDaoEstado</returns>
        public override IDaoEstado ObtenerDaoEstado()
        {
            return new DaoEstadoNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoEstatus
        /// </summary>
        /// <returns>IDaoEstatus</returns>
        public override IDaoEstatus ObtenerDaoEstatus()
        {
            return new DaoEstatusNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoInternacional
        /// </summary>
        /// <returns>IDaoInternacional</returns>
        public override IDaoInternacional ObtenerDaoInternacional()
        {
            return new DaoInternacionalNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoNacional
        /// </summary>
        /// <returns>IDaoNacional</returns>
        public override IDaoNacional ObtenerDaoNacional()
        {
            return new DaoNacionalNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoObjeto
        /// </summary>
        /// <returns>IDaoObjeto</returns>
        public override IDaoObjeto ObtenerDaoObjeto()
        {
            return new DaoObjetoNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoPais
        /// </summary>
        /// <returns>IDaoPais</returns>
        public override IDaoPais ObtenerDaoPais()
        {
            return new DaoPaisNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoResolucion
        /// </summary>
        /// <returns>Daoresolucion</returns>
        public override IDaoResolucion ObtenerDaoResolucion()
        {
            return new DaoResolucionNHibernate();
        }
        

        /// <summary>
        /// Método que devuelve el DaoRol
        /// </summary>
        /// <returns>IDaoRol</returns>
        public override IDaoRol ObtenerDaoRol()
        {
            return new DaoRolNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoTipoFecha
        /// </summary>
        /// <returns>IDaoTipoFecha</returns>
        public override IDaoTipoFecha ObtenerDaoTipoFecha()
        {
            return new DaoTipoFechaNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoTipoInfobol
        /// </summary>
        /// <returns>IDaoTipoInfobol</returns>
        public override IDaoTipoInfobol ObtenerDaoTipoInfobol()
        {
            return new DaoTipoInfobolNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoUsuario
        /// </summary>
        /// <returns>IDaoUsuario</returns>
        public override IDaoUsuario ObtenerDaoUsuario()
        {
            return new DaoUsuarioNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoBoletin
        /// </summary>
        /// <returns>IDaoBoletin</returns>
        public override IDaoBoletin ObtenerDaoBoletin()
        {
            return new DaoBoletinNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoPoder
        /// </summary>
        /// <returns>IDaoPoder</returns>
        public override IDaoPoder ObtenerDaoPoder()
        {
            return new DaoPoderNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoContador
        /// </summary>
        /// <returns>IDaoContador</returns>
        public override IDaoContador ObtenerDaoContador()
        {
            return new DaoContadorNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoContadorAuditoria
        /// </summary>
        /// <returns>IDaoContadorAuditoria</returns>
        public override IDaoContadorAuditoria ObtenerDaoContadorAuditoria()
        {
            return new DaoContadorAuditoriaNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoInteresado
        /// </summary>
        /// <returns>IDaoInteresado</returns>
        public override IDaoInteresado ObtenerDaoInteresado()
        {
            return new DaoInteresadoNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoEtiqueta
        /// </summary>
        /// <returns>IDaoEtiqueta</returns>
        public override IDaoEtiqueta ObtenerDaoEtiqueta()
        {
            return new DaoEtiquetaNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoIdioma
        /// </summary>
        /// <returns>IDaoIdioma</returns>
        public override IDaoIdioma ObtenerDaoIdioma()
        {
            return new DaoIdiomaNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoMoneda
        /// </summary>
        /// <returns>IDaoMoneda</returns>
        public override IDaoMoneda ObtenerDaoMoneda()
        {
            return new DaoMonedaNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoTarifa
        /// </summary>
        /// <returns>IDaoTarifa</returns>
        public override IDaoTarifa ObtenerDaoTarifa()
        {
            return new DaoTarifaNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoTipoCliente
        /// </summary>
        /// <returns>IDaoTipoCliente</returns>
        public override IDaoTipoCliente ObtenerDaoTipoCliente()
        {
            return new DaoTipoClienteNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoDetallePago
        /// </summary>
        /// <returns>IDaoDetallePago</returns>
        public override IDaoDetallePago ObtenerDaoDetallePago()
        {
            return new DaoDetallePagoNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoAsociado
        /// </summary>
        /// <returns>IDaoAsociado</returns>
        public override IDaoAsociado ObtenerDaoAsociado()
        {
            return new DaoAsociadoNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoContadorFac
        /// </summary>
        /// <returns>IDaoContadorFac</returns>
        public override IDaoContadorFac ObtenerDaoContadorFac()
        {
            return new DaoContadorFacNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoConcepto
        /// </summary>
        /// <returns>IDaoConcepto</returns>
        public override IDaoConcepto ObtenerDaoConcepto()
        {
            return new DaoConceptoNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoJustificacion
        /// </summary>
        /// <returns>IDaoJustificacion</returns>
        public override IDaoJustificacion ObtenerDaoJustificacion()
        {
            return new DaoJustificacionNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoContacto
        /// </summary>
        /// <returns>IDaoContacto</returns>
        public override IDaoContacto ObtenerDaoContacto()
        {
            return new DaoContactoNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoCarta
        /// </summary>
        /// <returns>IDaoCarta</returns>
        public override IDaoCarta ObtenerDaoCarta()
        {
            return new DaoCartaNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoDatosTransferencia
        /// </summary>
        /// <returns>IDaoDatosTransferencia</returns>
        public override IDaoDatosTransferencia ObtenerDaoDatosTransferencia()
        {
            return new DaoDatosTransferenciaNHibernate();
        }

        /// <summary>
        /// Método que devuelve el DaoResumen
        /// </summary>
        /// <returns>IDaoResumen</returns>
        public override IDaoResumen ObtenerDaoResumen()
        {
            return new DaoResumenNHibernate();
        }
    }
}
