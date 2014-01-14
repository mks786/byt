using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class InteresadoPatente
    {
        #region Atributos

        private int _id;
        private Interesado _interesado;
        private Interesado _interesado1;
        private Interesado _interesado2;
        private Interesado _interesado3;
        //private Patente _patente;
        private string _observaciones;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public InteresadoPatente() { }

        /// <summary>
        /// Constructor que inicializa el id del Concepto
        /// </summary>
        /// <param name="id">Id del Concepto</param>
        public InteresadoPatente(int id)
        {
            this._id = id;
        }


        public override bool Equals(object obj)
        {
            if ((this.Id == ((InteresadoPatente)obj).Id) && (this.Interesado.Id == ((InteresadoPatente)obj).Interesado.Id))
                return true;
            return false;

            //if ((this.Id == ((InteresadoPatente)obj).Id) && (this.Patente.Id == ((InteresadoPatente)obj).Patente.Id))
            //    return true;
            //return false;
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
        /// Propiedad que asigna u obtiene el Id de un Interesado multiple para una Patente
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado multiple para una Patente
        /// </summary>
        public virtual Interesado Interesado
        {
            get { return _interesado; }
            set { _interesado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado1 multiple para una Patente
        /// </summary>
        public virtual Interesado Interesado1
        {
            get { return _interesado1; }
            set { _interesado1 = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado2 multiple para una Patente
        /// </summary>
        public virtual Interesado Interesado2
        {
            get { return _interesado2; }
            set { _interesado2 = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado multiple para una Patente
        /// </summary>
        public virtual Interesado Interesado3
        {
            get { return _interesado3; }
            set { _interesado3 = value; }
        }

        
        /// <summary>
        /// Propiedad que asigna u obtiene Observaciones para un Interesado multiple de una Patente
        /// </summary>
        public virtual string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        

        #endregion
    }
}
