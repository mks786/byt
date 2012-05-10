using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Fecha
    {
        #region Atributos

        private int _id;
        private string _tipo;
        private string _comentario;
        private string _usuario;
        private int _correspondencia;
        private DateTime _fecha;
        private DateTime _timeStamp;
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Fecha() { }

        /// <summary>
        /// Constructor que inicializa el id del Concepto
        /// </summary>
        /// <param name="id">Id del Concepto</param>
        public Fecha(int id)
        {
            this._id = id;
        }


        public override bool Equals(object obj)
        {
            if (this.Id == ((Fecha)obj).Id)
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
        /// Propiedad que asigna u obtiene el Id de una Fecha
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la tipo de una Fecha
        /// </summary>
        public virtual string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la comentario de una Fecha
        /// </summary>
        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario= value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el usuario de una Fecha
        /// </summary>
        public virtual string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la correspondencia de una Fecha
        /// </summary>
        public virtual int Correspondencia
        {
            get { return _correspondencia; }
            set { _correspondencia = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la fecha de una Fecha
        /// </summary>
        public virtual DateTime FechaRegistro
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        // <summary>
        /// Propiedad que asigna u obtiene el timestamp de una Fecha
        /// </summary>
        public virtual DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        #endregion
    }
}
