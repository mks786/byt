using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class MarcaBaseTercero
    {
        #region Atributos

        private MarcaTercero _marcaTercero;
        private int _id;
        private string _origen;
        private string _tipo;
        private Marca _marca;
        private string _nombreMarca;
        private TipoBase _tipoBase;
        private string _nombreTipoBase;
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
        public MarcaBaseTercero(int id): this()
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
            var t = obj as MarcaBaseTercero;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (MarcaTercero.Id.Equals(t.MarcaTercero.Id)))
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
        /// Propiedad que asigna u obtiene el Id
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo
        /// </summary>
        public virtual MarcaTercero MarcaTercero
        {
            get { return _marcaTercero; }
            set { _marcaTercero = value; }
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

        /// <summary>
        /// Propiedad que asigna u obtiene el NombreMarca
        /// </summary>
        public virtual string NombreMarca
        {
            get { return this._nombreMarca; }
            set { this._nombreMarca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el NombreMarca
        /// </summary>
        public virtual string NombreTipoBase
        {
            get { return this._nombreTipoBase; }
            set { this._nombreTipoBase = value; }
        }
        #endregion
    }
}
