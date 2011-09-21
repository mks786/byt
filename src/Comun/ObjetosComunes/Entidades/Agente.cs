using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Agente
    {
        #region Atributos

        private string _id;
        private string _nombre;
        private string _domicilio;
        private string _telefono;
        private char _estadoCivil;
        private char _sexo;
        private string _numeroAbogado;
        private string _numeroImpresoAbogado;
        private string _numeroPropiedad;
        private string _cci;
        private IList<Poder> _poderes;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Agente() { }

        /// <summary>
        /// Constructor que inicializa el Id del Agente
        /// </summary>
        /// <param name="id">Id del Agente</param>
        public Agente(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Agente
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el nombre del Agente
        /// </summary>
        public virtual string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el domicilio del Agente
        /// </summary>
        public virtual string Domicilio
        {
            get { return this._domicilio; }
            set { this._domicilio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el telefono del Agente
        /// </summary>
        public virtual string Telefono
        {
            get { return this._telefono; }
            set { this._telefono = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el estado civil del Agente
        /// </summary>
        public virtual char EstadoCivil
        {
            get { return this._estadoCivil; }
            set { this._estadoCivil = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el sexo del Agente
        /// </summary>
        public virtual char Sexo
        {
            get { return this._sexo; }
            set { this._sexo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el numero abogado del Agente
        /// </summary>
        public virtual string NumeroAbogado
        {
            get { return this._numeroAbogado; }
            set { this._numeroAbogado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el numero impreso del abogado del Agente
        /// </summary>
        public virtual string NumeroImpresoAbogado
        {
            get { return this._numeroImpresoAbogado; }
            set { this._numeroImpresoAbogado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el numero de propiedad del Agente
        /// </summary>
        public virtual string NumeroPropiedad
        {
            get { return this._numeroPropiedad; }
            set { this._numeroPropiedad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el CCI del Agente
        /// </summary>
        public virtual string CCI
        {
            get { return this._cci; }
            set { this._cci = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene los poderes que poseen el agente
        /// </summary>
        public virtual IList<Poder> Poderes
        {
            get { return this._poderes; }
            set { this._poderes = value; }
        }

        #endregion
    }
}
