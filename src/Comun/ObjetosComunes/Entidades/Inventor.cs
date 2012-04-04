using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Inventor
    {
        #region Atributos

        private int _id;
        private Pais _nacionalidad;
        private Patente _patente;
        private string _domicilio;
        private string _inventorPatente;
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Inventor() { }

        /// <summary>
        /// Constructor que inicializa el id del Concepto
        /// </summary>
        /// <param name="id">Id del Concepto</param>
        public Inventor(int id)
        {
            this._id = id;
        }


        public override bool Equals(object obj)
        {
            if ((this.Id == ((Inventor)obj).Id) && (this.Patente.Id == ((Inventor)obj).Patente.Id))
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
        /// Propiedad que asigna u obtiene el Id de un Inventor
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Nacionalidad de un Inventor
        /// </summary>
        public virtual Pais Nacionalidad
        {
            get { return _nacionalidad; }
            set { _nacionalidad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Patente de un Inventor
        /// </summary>
        public Patente Patente
        {
            get { return _patente; }
            set { _patente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Domicilio de un Inventor
        /// </summary>
        public virtual string Domicilio
        {
            get { return _domicilio; }
            set { _domicilio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el InventorPatente de un Inventor
        /// </summary>
        public virtual string InventorPatente
        {
            get { return _inventorPatente; }
            set { _inventorPatente = value; }
        }

        #endregion
    }
}
