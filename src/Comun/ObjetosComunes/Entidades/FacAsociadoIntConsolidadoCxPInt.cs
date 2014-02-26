using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacAsociadoIntConsolidadoCxPInt
    {
        #region Atributos

        private int _id;
        private Asociado _asociadoInt;
        private double _montoConsolidado;
        private string _formaPago;
        private DatosTransferencia _datosTransferencia;
        private string _beneficiario;
        private IList<FacInternacional> _facturasIntConsolidadas;
        private IList<ListaDatosValores> _formasDePago;
        private string _datosBancariosStr;
        private int _numeroSecuenciaTransferencia;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public FacAsociadoIntConsolidadoCxPInt()
        {
        }

        #endregion


        #region Propiedades



        /// <summary>
        /// Propiedad que asigna y obtiene el Id del Registro para el Asociado Internacional Consolidado
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene el Asociado Internacional Consolidado
        /// </summary>
        public virtual Asociado AsociadoInt
        {
            get { return this._asociadoInt; }
            set { this._asociadoInt = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene los datos de transferencia para el pago de facturas internacionales consolidadas
        /// </summary>
        public virtual DatosTransferencia DatosTransferencia
        {
            get { return this._datosTransferencia; }
            set { this._datosTransferencia = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene el Monto consolidado de un grupo de facturas internacionales
        /// </summary>
        public virtual double MontoConsolidado
        {
            get { return this._montoConsolidado; }
            set { this._montoConsolidado = value; }
        }



        /// <summary>
        /// Propiedad que asigna y obtiene la forma de pago para un conjunto de facturas internacionales consolidadas
        /// </summary>
        public virtual string FormaPago
        {
            get { return this._formaPago; }
            set { this._formaPago = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene las formas de pago posibles de un Asociado internacional consolidado 
        /// </summary>
        public virtual IList<ListaDatosValores> FormasDePago
        {
            get { return this._formasDePago; }
            set { this._formasDePago = value; }
        }

                
        /// <summary>
        /// Propiedad que asigna y obtiene el Beneficiario del pago de un conjunto de facturas internacionales consolidadas
        /// </summary>
        public virtual string Beneficiario
        {
            get { return this._beneficiario; }
            set { this._beneficiario = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene las facturas internacionales consolidadas de un Asociado internacional especifico
        /// y que seran consolidadas
        /// </summary>
        public virtual IList<FacInternacional> FacturasIntConsolidadas
        {
            get { return this._facturasIntConsolidadas; }
            set { this._facturasIntConsolidadas = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el String con los datos bancarios de un Asociado Internacional seleccionado para consolidar
        /// </summary>
        public virtual string DatosBancariosStr
        {
            get { return this._datosBancariosStr; }
            set { this._datosBancariosStr = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el numero de secuencia de datos de transferencia del Asociado Internacional
        /// </summary>
        public virtual int NumeroSecuenciaTransferencia
        {
            get { return this._numeroSecuenciaTransferencia; }
            set { this._numeroSecuenciaTransferencia = value; }
        }
        

        #endregion
    }
}
