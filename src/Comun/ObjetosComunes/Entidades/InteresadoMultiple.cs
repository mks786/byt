using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class InteresadoMultiple
    {
        #region Atributos

        private int _id;
        private char _tipo;
        private Interesado _interesado;
        private Interesado _interesado1;
        private Interesado _interesado2;
        private Interesado _interesado3;
        private string _observaciones;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public InteresadoMultiple() { }

        /// <summary>
        /// Constructor que inicializa el id del Concepto
        /// </summary>
        /// <param name="id">Id del Concepto</param>
        public InteresadoMultiple(int id)
        {
            this._id = id;
        }


        public override bool Equals(object obj)
        {
            if ((this.Id == ((InteresadoMultiple)obj).Id) && (this.Tipo == ((InteresadoMultiple)obj).Tipo))
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
        /// Propiedad que asigna u obtiene el Id de un Interesado multiple
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        
        /// <summary>
        /// Propiedad que asigna u obtiene el tipo de Entidad al que pertenece un Interesado multiple
        /// </summary>
        public virtual char Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado principal de la Entidad  
        /// </summary>
        public virtual Interesado Interesado
        {
            get { return _interesado; }
            set { _interesado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado1 multiple para una entidad Marca o Patente
        /// </summary>
        public virtual Interesado Interesado1
        {
            get { return _interesado1; }
            set { _interesado1 = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado2 multiple para una entidad Marca o Patente
        /// </summary>
        public virtual Interesado Interesado2
        {
            get { return _interesado2; }
            set { _interesado2 = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado multiple para una entidad Marca o Patente
        /// </summary>
        public virtual Interesado Interesado3
        {
            get { return _interesado3; }
            set { _interesado3 = value; }
        }

        
        /// <summary>
        /// Propiedad que asigna u obtiene Observaciones de Interesados multiple para una entidad Marca o Patente
        /// </summary>
        public virtual string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        

        #endregion
    }
}
