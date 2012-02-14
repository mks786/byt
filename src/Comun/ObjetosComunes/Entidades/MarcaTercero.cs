using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class MarcaTercero
    {
        #region Atributos

        private int _id;
        private Fusion _fusion;
        private string _nombre;
        private TipoEstado _estado;
        private Pais _pais;
        private Pais _nacionalidad;
                
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public MarcaTercero() { }

        /// <summary>
        /// Constructor que inicializa el Id del Anexo
        /// </summary>
        /// <param name="id">Id del Anexo</param>
        public MarcaTercero (int id)
        {
            this._id = id;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if ((this.Id == ((MarcaTercero)obj).Id) && (this.Fusion.Id == ((MarcaTercero)obj).Fusion.Id))
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

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del MarcaTercero
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el id del MarcaTercero
        /// </summary>
        public virtual Fusion Fusion
        {
            get { return this._fusion; }
            set { this._fusion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Nombre del MarcaTercero
        /// </summary>
        public virtual string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el TipoEstado de MarcaTercero
        /// </summary>
        public virtual TipoEstado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Pais de un MarcaTercero
        /// </summary>
        public virtual Pais Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Nacionalidad de un MarcaTercero
        /// </summary>
        public virtual Pais Nacionalidad
        {
            get { return _nacionalidad; }
            set { _nacionalidad = value; }
        }

        #endregion
    }
}
