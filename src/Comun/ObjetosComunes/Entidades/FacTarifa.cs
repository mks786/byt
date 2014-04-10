using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacTarifa
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private char _desgMonto; 

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public FacTarifa() 
        {
            this._desgMonto = 'F';
        }

        /// <summary>
        /// Constructor que inicializa el condigo de la tarifa
        /// </summary>
        /// <param name="codigo">Codigo de la tarifa</param>
        public FacTarifa(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el código de la tarifa
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion de la tarifa
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el caracter para activar el Desglose por Monto en la Facturacion  
        /// </summary>
        public virtual char DesgMonto
        {
            get { return this._desgMonto; }
            set { this._desgMonto = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el bit para activar el Desglose por Monto en la Facturacion 
        /// </summary>
        public virtual bool BDesgMonto
        {
            get
            {
                if (this.DesgMonto.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.DesgMonto = 'T';
                }
                else
                {
                    this.DesgMonto = 'F';
                }
            }
        }

        #endregion
    }
}
