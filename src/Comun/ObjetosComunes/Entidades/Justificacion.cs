using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Justificacion
    {
        #region Atributos

        private int _id;
        private Concepto _concepto;
        private DateTime? _fecha;
        private Asociado _asociado;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado de la Justificacion
        /// </summary>
        public Justificacion() { }

        /// <summary>
        /// Constructor que inicializa el id de la Justificacion
        /// </summary>
        /// <param name="id">Id del Poder</param>
        public Justificacion(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        public override bool Equals(object obj)
        {

            return base.Equals(obj);

            //bool igual = false;

            //if ((this._id == ((Justificacion)obj).Id) && (this._asociado.Id == ((Justificacion)obj).Asociado.Id))
            //    igual = true;

            //return igual;
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
        /// Propiedad que asigna u obtiene el id de la Justificacion
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el objeto de tipo Concepto de la Justificacion
        /// </summary>
        public virtual Concepto Concepto
        {
            get { return _concepto; }
            set { _concepto = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la fecha de la justificacion
        /// </summary>
        public virtual DateTime? Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        /// <summary>
        /// Propiedad que asigna u obtiene el asociado de una justificacion
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }


        #endregion
    }
}
