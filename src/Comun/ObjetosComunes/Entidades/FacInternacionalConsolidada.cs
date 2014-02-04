using System;
using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacInternacionalConsolidada
    {
        #region Atributos

        private int _id;
        private Asociado _asociadoInt;
        private Asociado _asociado;
        //private FacInternacional _facInternacional;
        //private DatosTransferencia _datosTransferenciaAsocInt;


        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public FacInternacionalConsolidada()
        {
        }

        #endregion

        #region Override

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as FacInternacionalConsolidada;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (AsociadoInt.Id == (t.AsociadoInt.Id)))
                return true;
            return false;
        }
        
        #endregion

        /// <summary>
        /// Propiedad que asigna y obtiene el Id de la Factura consolidada
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el Asociado Internacional de la Factura consolidada
        /// </summary>
        public virtual Asociado AsociadoInt
        {
            get { return this._asociadoInt; }
            set { this._asociadoInt = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el Asociado Cliente de la Factura consolidada
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return this._asociado; }
            set { this._asociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene los datos de la Factura Internacional a consolidar
        /// </summary>
        //public virtual FacInternacional FacInternacional
        //{
        //    get { return this._facInternacional; }
        //    set { this._facInternacional = value; }
        //}


        /// <summary>
        /// Propiedad que asigna y obtiene los Datos de Transferencia del Asociado Internacional de las facturas consolidadas
        /// </summary>
        //public virtual DatosTransferencia DatosTransferenciaAsocInt
        //{
        //    get { return this._datosTransferenciaAsocInt; }
        //    set { this._datosTransferenciaAsocInt = value; }
        //}
    }
}
