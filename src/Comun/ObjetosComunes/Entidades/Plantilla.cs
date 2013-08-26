using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Plantilla
    {
        #region Atributos

        private int _id;
        private string _descripcion;
        private Departamento _departamento;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Plantilla() { }

        /// <summary>
        /// Constructor que inicializa el id de la plantilla
        /// </summary>
        /// <param name="id">Id de la plantilla</param>
        public Plantilla(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id de la plantilla
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción de la plantilla
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Departamento al que pertenece la plantilla
        /// </summary>
        public virtual Departamento Departamento
        {
            get { return this._departamento; }
            set { this._departamento = value; }
        }

               

        #endregion
    }
}
