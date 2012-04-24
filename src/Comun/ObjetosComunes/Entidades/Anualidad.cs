using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Anualidad
    {
        #region Atributos

        private int _id;        
        private DateTime? _fechaAnualidad;
        private int _qAnualidad;
        private string _situacion;
        private int _voucher;
        private DateTime? _fechaVoucher;
        private Asociado _asociado;  
        private Patente _patente;  
        private string _recibo;
        private int _factura;
        private string _iFactura;
  

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Anualidad()
        {            
        }

        /// <summary>
        /// Constructor que inicializa el id de la marca
        /// </summary>
        /// <param name="id">Id de la marca</param>
        public Anualidad(int id)
            : this()
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el qAnualidad
        /// </summary>
        public virtual int QAnualidad
        {
            get { return this._qAnualidad; }
            set { this._qAnualidad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Voucher
        /// </summary>
        public virtual int Voucher
        {
            get { return this._voucher; }
            set { this._voucher = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Factura
        /// </summary>
        public virtual int Factura
        {
            get { return this._factura; }
            set { this._factura = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la situacion
        /// </summary>
        public virtual string Situacion
        {
            get { return this._situacion; }
            set { this._situacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el recibo
        /// </summary>
        public virtual string Recibo
        {
            get { return _recibo; }
            set { _recibo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el _iFactura
        /// </summary>
        public virtual string IFactura
        {
            get { return _iFactura; }
            set { _iFactura = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de FechaAnualidad
        /// </summary>
        public virtual DateTime? FechaAnualidad
        {
            get { return _fechaAnualidad; }
            set { _fechaAnualidad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de FechaVoucher
        /// </summary>
        public virtual DateTime? FechaVoucher
        {
            get { return _fechaVoucher; }
            set { _fechaVoucher = value; }
        }
        /// <summary>
        /// Propiedad que asigna u obtiene el Asocaido
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Patente
        /// </summary>
        public virtual Patente Patente
        {
            get { return _patente; }
            set { _patente = value; }
        }

        #endregion
    }
}
