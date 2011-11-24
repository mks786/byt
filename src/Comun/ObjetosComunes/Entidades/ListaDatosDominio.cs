using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class ListaDatosDominio
    {
        #region Atributos

        private string _id;
        private string _filtro;
        private string _descripcion;

        #endregion

        #region Sobreescritura de Métodos

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ListaDatosDominio;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (Id == (t.Id)))
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

        #region Constructores

        /// <summary>
        /// Constructor con filtro
        /// </summary>
        public ListaDatosDominio()
        {
            
        }

        /// <summary>
        /// Constructor con filtro
        /// </summary>
        public ListaDatosDominio(string filtro) 
        {
            this._filtro = filtro;
        }

        #endregion

        #region Propiedades

        /// <summary>
        ///Propiedad que asigna u obtiene el Id de la ListaDatosDominio 
        /// </summary>
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        ///Propiedad que asigna u obtiene el filtro de la ListaDatosDominio 
        /// </summary>
        public virtual string Filtro
        {
            get { return _filtro; }
            set { _filtro = value; }
        }

        /// <summary>
        ///Propiedad que asigna u obtiene la descripción de la ListaDatosDominio 
        /// </summary>
        public virtual string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        #endregion
    }
}
