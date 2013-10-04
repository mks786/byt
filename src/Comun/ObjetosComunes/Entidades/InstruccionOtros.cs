using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class InstruccionOtros
    {
        #region Atributos

        private int _id;
        private int cod_marcaOPatente;
        private string _descripcion;
        private Carta _correspondencia;
        private Marca _marca;
        private Patente _patente;
        #endregion


        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public InstruccionOtros() { }

        
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
        /// Propiedad que asigna u obtiene el Codigo de la Instruccion del tipo Otros
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Codigo de la Marca o la Patente
        /// </summary>
        public virtual int Cod_MarcaOPatente
        {
            get { return this.cod_marcaOPatente; }
            set { this.cod_marcaOPatente = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion de una instruccion no tipificada
        /// </summary>
        public virtual String Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        
        /// <summary>
        /// Propiedad que asigna u obtiene la Correspondencia de una instruccion no tipificada
        /// </summary>
        public virtual Carta Correspondencia
        {
            get { return this._correspondencia; }
            set { this._correspondencia = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Marca para la Instruccion por Envio de Originales
        /// </summary>
        public virtual Marca Marca
        {
            get { return this._marca; }
            set { this._marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Patente para la Instruccion por Envio de Originales
        /// </summary>
        public virtual Patente Patente
        {
            get { return this._patente; }
            set { this._patente = value; }
        }


        


        #endregion
    }
}
