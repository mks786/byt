using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class InstruccionEnvioOriginales
    {
        #region Atributos

        private int _id;
        private string _aplicadaA;
        private string _concepto;
        private string _nombreInstruccion;
        private string _direccionInstruccion;
        private Asociado _asociado;
        private Carta _correspAsociado;
        private Interesado _interesado;
        private Carta _correspInteresado;
        private string _operacion;
        private Marca _marca;
        private Patente _patente;
        private string _alerta;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public InstruccionEnvioOriginales() { }

        /// <summary>
        /// Constructor que recibe el Id de la Instruccion de Envio de Originales
        /// </summary>
        /// <param name="id">Id de la Instruccion</param>
        public InstruccionEnvioOriginales(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Sobreescritura del Método Equals debido a que la clase tiene id compuesto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as InstruccionEnvioOriginales;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (AplicadaA == (t.AplicadaA)) && (Concepto == t.Concepto))
                return true;
            return false;

        }

        /// <summary>
        /// Sobreescritura del Método GetHashCode debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Sobreescritura del Método ToString debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Id de la Instruccion de Envio de Originales
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene para que esta aplicada la Instruccion de Envio de Originales (M de Marcas o P de Patentes)
        /// </summary>
        public virtual String AplicadaA
        {
            get { return this._aplicadaA; }
            set { this._aplicadaA = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Concepto de la Instruccion de Envio de Originales
        /// </summary>
        public virtual String Concepto
        {
            get { return this._concepto; }
            set { this._concepto = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Nombre de la Instruccion de Envio de Originales
        /// </summary>
        public virtual String NombreInstruccion
        {
            get { return this._nombreInstruccion; }
            set { this._nombreInstruccion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Direccion de la Instruccion de Envio de Originales
        /// </summary>
        public virtual String DireccionInstruccion
        {
            get { return this._direccionInstruccion; }
            set { this._direccionInstruccion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Asociado de la Instruccion de Envio de Originales
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return this._asociado; }
            set { this._asociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Correspondencia del Asociado de la Instruccion de Envio de Originales
        /// </summary>
        public virtual Carta CorrespAsociado
        {
            get { return this._correspAsociado; }
            set { this._correspAsociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado de la Instruccion de Envio de Originales
        /// </summary>
        public virtual Interesado Interesado
        {
            get { return this._interesado; }
            set { this._interesado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Correspondencia del Interesado de la Instruccion de Envio de Originales
        /// </summary>
        public virtual Carta CorrespInteresado
        {
            get { return this._correspInteresado; }
            set { this._correspInteresado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la operacion de Base de Datos a realizar con la Entidad
        /// </summary>
        public virtual string Operacion
        {
            get { return this._operacion; }
            set { this._operacion = value; }
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


        /// <summary>
        /// Propiedad que asigna u obtiene la Alerta para la Instruccion por Envio de Originales
        /// </summary>
        public virtual String Alerta
        {
            get { return this._alerta; }
            set { this._alerta = value; }
        }


        #endregion
    }
}
