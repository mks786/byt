using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Caja
    {
        
        #region Atributos

        private int _id;

        private TipoCaja _tipoCaja;

        private string _descripcion;
               

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Caja() { }

        /// <summary>
        /// Constructor que inicializa el Id de la caja
        /// </summary>
        /// <param name="id">Id de Caja</param>
        public Caja(int id)
        {
            this._id = id;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Sobreescritura del Método Equals debido a que la clase tiene id compuesto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Caja;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (TipoCaja.Id == (t.TipoCaja.Id)))
                return true;
            return false;

        }

        /// <summary>
        /// Sobreescritura del Método GetHashCode debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Sobreescritura del Método ToString debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el tipocaja de la entidad Caja
        /// </summary>
        public virtual TipoCaja TipoCaja
        {
            get { return _tipoCaja; }
            set { _tipoCaja = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el Id de la caja
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene la descripcion de la Caja
        /// </summary>
        public virtual string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        #endregion

    }
}
