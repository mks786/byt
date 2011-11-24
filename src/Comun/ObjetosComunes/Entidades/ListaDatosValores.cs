using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class ListaDatosValores
    {
        #region Atributos

        private string _id;
        private string _valor;
        private string _descripcion;

        #endregion

        #region Constructores


        public override bool Equals(object obj)
        {
            if ((this.Id == ((ListaDatosValores)obj).Id) && (this.Valor == ((ListaDatosValores)obj).Valor))
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

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ListaDatosValores() { }

        /// <summary>
        /// Constructor que inicializa el condigo de los datos de transferencia
        /// 
        /// </summary>
        /// <param name="codigo">Codigo del status</param>
        public ListaDatosValores(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el que represetna 
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Valor
        /// </summary>
        public virtual string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Descripcion
        /// </summary>
        public virtual string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        #endregion
    }
}
