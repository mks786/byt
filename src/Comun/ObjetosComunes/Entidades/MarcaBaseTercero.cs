using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class MarcaBaseTercero
    {
        #region Atributos

        private string _id;
        private Anexo _anexo;
        private int _secuencia;
        private string _origen;
        private string _tipo;
        private Marca _marca;
        private TipoBase _tipoBase;
        private Pais _pais;
        private Internacional _internacional;
        private Nacional _nacional;
        #endregion

        #region Constructores

       public MarcaBaseTercero()
        {
        }

        /// <summary>
        /// Constructor que inicializa el id de la marcaTercero
        /// </summary>
        /// <param name="id">Id de la marcaTercero</param>
        public MarcaBaseTercero(string id): this()
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción
        /// </summary>
        public virtual Anexo Anexo
        {
            get { return this._anexo; }
            set { this._anexo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo
        /// </summary>
        public virtual int Secuencia
        {
            get { return _secuencia; }
            set { _secuencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de Prioridad
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la PrimeraReferencia
        /// </summary>
        public virtual Internacional Internacional
        {
            get { return _internacional; }
            set { _internacional = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de Inscripcion
        /// </summary>
        public virtual Nacional Nacional
        {
            get { return _nacional; }
            set { _nacional = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Codigo de Inscripcion
        /// </summary>
        public virtual string Origen
        {
            get { return _origen; }
            set { _origen = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Codigo de Inscripcion
        /// </summary>
        public virtual string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        /// <summary>
        /// Propiedad que asigna u obtiene el  de AsociadoTercero
        /// </summary>
        public virtual TipoBase TipoDeBase
        {
            get { return _tipoBase; }
            set { _tipoBase = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el  de interesadoTercero
        /// </summary>
        public virtual Pais Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

       
        #endregion
    }
}
