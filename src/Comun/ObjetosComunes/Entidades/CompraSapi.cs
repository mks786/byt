using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CompraSapi
    {
        #region Atributos

        private int _id;
        private DateTime? _fechaCompra;
        private double _impuesto;
        private double _subtotal;
        private double _subtotalIva;
        private double _total;
        private IList<CompraSapiDetalle> _detalleCompra;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public CompraSapi()
        {
            this._impuesto = 0.00;
            this._subtotal = 0.00;
            this._subtotalIva = 0.00;
            this._total = 0.00;
        }

        /// <summary>
        /// Constructor que inicializa el Id del Departamento
        /// </summary>
        /// <param name="id">Id del departamento</param>
        public CompraSapi(int id)
        {
            this._id = id;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id de la Compra de Material Sapi
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de la Compra de Material Sapi
        /// </summary>
        public virtual DateTime? FechaCompra
        {
            get { return this._fechaCompra; }
            set { this._fechaCompra = value; }
        }

        
        /// <summary>
        /// Propiedad que asigna u obtiene el Impuesto aplicado a la Compra de Material Sapi
        /// </summary>
        public virtual double Impuesto
        {
            get { return this._impuesto; }
            set { this._impuesto = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Subtotal del importe de la Compra de Material Sapi
        /// </summary>
        public virtual double Subtotal
        {
            get { return this._subtotal; }
            set { this._subtotal = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Subtotal con Iva del importe de la Compra de Material Sapi
        /// </summary>
        public virtual double SubtotalIva
        {
            get { return this._subtotalIva; }
            set { this._subtotalIva = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Gran Total de la Compra de Material Sapi
        /// </summary>
        public virtual double Total
        {
            get { return this._total; }
            set { this._total = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene los renglones de Detalle de la Compra de Material Sapi
        /// </summary>
        public virtual IList<CompraSapiDetalle> DetalleCompra
        {
            get { return this._detalleCompra; }
            set { this._detalleCompra = value; }
        }

        #endregion
    }
}
