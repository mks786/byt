using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Boletin
    {
        #region Atributos

        private int _id;
        private DateTime _fechaBoletin;
        private DateTime? _fechaBoletinVence;
        //private IList<Resolucion> _resoluciones;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Boletin() { }

        /// <summary>
        /// Constructor que inicializa el id del Boletin
        /// </summary>
        /// <param name="id">Id del Boletin</param>
        public Boletin(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna el id de la auditoria
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna de la fecha del boletin
        /// </summary>
        public virtual DateTime FechaBoletin
        {
            get { return _fechaBoletin; }
            set { _fechaBoletin = value; }
        }

        /// <summary>
        /// Propiedad que asigna de la fecha que vence el boletin
        /// </summary>
        public virtual DateTime? FechaBoletinVence
        {
            get { return _fechaBoletinVence; }
            set { _fechaBoletinVence = value; }
        }

        ///// <summary>
        ///// Propiedad que asigna la lista de resoluciones
        ///// </summary>
        //public virtual IList<Resolucion> Resoluciones
        //{
        //    get { return _resoluciones; }
        //    set { _resoluciones = value; }
        //}

        #endregion
    }
}
