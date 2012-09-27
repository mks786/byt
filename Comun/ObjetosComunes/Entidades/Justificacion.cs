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

        private Concepto _concepto;
        private DateTime? _fecha;
        private Asociado _asociado;
        private Carta _carta;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado de la Justificacion
        /// </summary>
        public Justificacion() { }        
        
        /// <summary>
        /// Constructor donde se setea la clave primaria
        /// </summary>
        public Justificacion(Carta carta, Asociado asociado) 
        {
            this._carta = carta;
            this._asociado = asociado;
        }


        #endregion

        #region Propiedades

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

        /// <summary>
        /// Propiedad que asigna u obtiene la carta de una justificacion
        /// </summary>
        public virtual Carta Carta  
        {
            get { return _carta; }
            set { _carta = value; }
        }


        #endregion
    }
}
