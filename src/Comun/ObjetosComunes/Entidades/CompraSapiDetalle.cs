using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CompraSapiDetalle
    {
        #region Atributos

        private CompraSapi _compra;
        private MaterialSapi _material;
        private int _cantidad;
        private double? _punit;
        private double _total;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public CompraSapiDetalle() { }

        /// <summary>
        /// Constructor que inicializa el id del Concepto
        /// </summary>
        /// <param name="id">Id del Concepto</param>
        public CompraSapiDetalle(int id)
        {
            this.Compra.Id = id;
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as CompraSapiDetalle;
            if (t == null)
                return false;
            if ((Compra.Id == (t.Compra.Id)) && (Material.Id == (t.Material.Id)))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }


        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del Detalle de la Compra Sapi
        /// </summary>
        public virtual int Id
        {
            get { return _compra.Id; }
            set { _compra.Id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Compra Sápi que corresponde al Detalle de la Compra Sapi
        /// </summary>
        public virtual CompraSapi Compra
        {
            get { return _compra; }
            set { _compra = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene los Materiales Sapi que corresponden al Detalle de la Compra Sapi
        /// </summary>
        public virtual MaterialSapi Material
        {
            get { return _material; }
            set { _material = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Cantidad de cada uno de los Materiales del Detalle de la Compra Sapi
        /// </summary>
        public virtual int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Precio Unitario de cada uno de los Materiales del Detalle de la Compra Sapi
        /// </summary>
        public virtual double? PUnit
        {
            get { return _punit; }
            set { _punit = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Total del importe por cada uno de los Materiales del Detalle de la Compra Sapi
        /// </summary>
        public virtual double Total
        {
            get { return _total; }
            set { _total = value; }
        }


        #endregion
    }


    
}
