using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class InstruccionCorrespondencia
    {
        #region Atributos

        private int _id;
        private string _aplicadaA;
        private string _concepto;
        private string _tipoInstruccion;
        private Carta _correspondencia;
        private string _nombreEmail;
        private string _paraEmail;
        private string _ccEmail;
        private string _operacion;
        private Marca _marca;
        private Patente _patente;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public InstruccionCorrespondencia() { }

        /// <summary>
        /// Constructor que recibe el Id de la Instruccion de Correspondencia
        /// </summary>
        /// <param name="id">Id de la Instruccion</param>
        public InstruccionCorrespondencia(int id)
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
            var t = obj as InstruccionCorrespondencia;
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
        /// Propiedad que asigna u obtiene el Id de la Instruccion de Correspondencia
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene para que esta aplicada la Instruccion de Correspondencia (M de Marcas o P de Patentes)
        /// </summary>
        public virtual String AplicadaA
        {
            get { return this._aplicadaA; }
            set { this._aplicadaA = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Concepto de la Instruccion de Correspondencia
        /// </summary>
        public virtual String Concepto
        {
            get { return this._concepto; }
            set { this._concepto = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo de Instruccion de Correspondencia
        /// </summary>
        public virtual String TipoInstruccion
        {
            get { return this._tipoInstruccion; }
            set { this._tipoInstruccion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Nombre del Email de la Instruccion de Correspondencia
        /// </summary>
        public virtual String NombreEmail
        {
            get { return this._nombreEmail; }
            set { this._nombreEmail = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Destinatario(s) principal(es) del email de la Instruccion de Correspondencia
        /// </summary>
        public virtual String ParaEmail
        {
            get { return this._paraEmail; }
            set { this._paraEmail = value; }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene el Destinatario(s) secundario(s) del email de la Instruccion de Correspondencia
        /// </summary>
        public virtual String CCEmail
        {
            get { return this._ccEmail; }
            set { this._ccEmail = value; }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene la Correspondencia de la Instruccion de Correspondencia
        /// </summary>
        public virtual Carta Correspondencia
        {
            get { return this._correspondencia; }
            set { this._correspondencia = value; }
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
        /// Propiedad que asigna u obtiene la Marca de la Instruccion de Correspondencia
        /// </summary>
        public virtual Marca Marca
        {
            get { return this._marca; }
            set { this._marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Patente de la Instruccion de Correspondencia
        /// </summary>
        public virtual Patente Patente
        {
            get { return this._patente; }
            set { this._patente = value; }
        }


        #endregion
    }
}
