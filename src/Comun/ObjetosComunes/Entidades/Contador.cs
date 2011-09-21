using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Contador
    {
        #region Atributos

        private string _id;
        private string _comentario;
        private int _proximoValor;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Contador() { }

        /// <summary>
        /// Constructor que inicializa el id del Contador
        /// </summary>
        /// <param name="id">Id del Nacional</param>
        public Contador(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el dominio de el contador
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Comentario
        /// </summary>
        public virtual string Comentario
        {
            get { return this._comentario; }
            set { this._comentario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el proximo valor
        /// </summary>
        public virtual int ProximoValor
        {
            get { return _proximoValor; }
            set { _proximoValor = value; }
        }

        #endregion
    }
}
