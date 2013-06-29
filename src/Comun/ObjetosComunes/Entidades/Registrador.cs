using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Registrador
    {
        #region Atributos

        private string _id;
        private string _nombreCompleto;
        
        #endregion


        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Registrador() { }

        /// <summary>
        /// Constructor que se inicializa el id del Registrador
        /// </summary>
        /// <param name="id">Id del Registrador</param>
        public Registrador(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

       
        /// <summary>
        /// Propiedad que asigna el Id del Registrador
        /// </summary>
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }



        /// <summary>
        /// Propiedad que asigna el Nombre Completo del Registrador
        /// </summary>
        public virtual string NombreCompleto
        {
            get { return _nombreCompleto; }
            set { _nombreCompleto = value; }
        }

        


        #endregion
    }
}
