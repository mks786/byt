using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class InstruccionDescuento
    {

        #region Atributos

        private int _id;
        private int _codOperacion;
        private string _aplicaA;
        private int _descuento;
        private string _observaciones;
        private Servicio _servicio;
        private Carta _correspondencia;
        private Marca _marca;
        private Patente _patente;
        #endregion


        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public InstruccionDescuento() { }

        
        #endregion


        #region Propiedades

        /// <summary>
        /// Sobreescritura del Método Equals debido a que la clase tiene id compuesto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //        return false;
        //    var t = obj as InstruccionEnvioOriginales;
        //    if (t == null)
        //        return false;
        //    if ((Id == (t.Id)) && (AplicadaA == (t.AplicadaA)) && (Concepto == t.Concepto))
        //        return true;
        //    return false;

        //}

        /// <summary>
        /// Sobreescritura del Método GetHashCode debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        /// <summary>
        /// Sobreescritura del Método ToString debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        //public override string ToString()
        //{
        //    return base.ToString();
        //}


        /// <summary>
        /// Propiedad que asigna u obtiene el Id de la Instruccion de Descuento
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Codigo de Operacion de la Instruccion de Descuento de la Marca o la Patente
        /// </summary>
        public virtual int CodigoOperacion
        {
            get { return this._codOperacion; }
            set { this._codOperacion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene a que se aplica la Instruccion de Descuento 
        /// </summary>
        public virtual String AplicaA
        {
            get { return this._aplicaA; }
            set { this._aplicaA = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Descuento de la Instruccion de Descuento para marca o para patente
        /// </summary>
        public virtual int Descuento
        {
            get { return this._descuento; }
            set { this._descuento = value; }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene las Observaciones de la Instruccion de Descuento 
        /// </summary>
        public virtual String Observaciones
        {
            get { return this._observaciones; }
            set { this._observaciones = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Servicio aplicado a una instruccion de descuento
        /// </summary>
        public virtual Servicio Servicio
        {
            get { return this._servicio; }
            set { this._servicio = value; }
        }


        
        /// <summary>
        /// Propiedad que asigna u obtiene la Correspondencia de una instruccion de descuento
        /// </summary>
        public virtual Carta Correspondencia
        {
            get { return this._correspondencia; }
            set { this._correspondencia = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Marca para la Instruccion de Descuento
        /// </summary>
        public virtual Marca Marca
        {
            get { return this._marca; }
            set { this._marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Patente para la Instruccion de Descuento
        /// </summary>
        public virtual Patente Patente
        {
            get { return this._patente; }
            set { this._patente = value; }
        }


        


        #endregion

    }
}
