using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FusionPatenteTercero
    {

        #region Atributos

        private int _id;
        private Estado _estado;
        private FusionPatente _fusion;
        private Pais _pais;
        private Pais _nacionalidad;

        private string _nombre;
        private string _domicilio;

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public FusionPatenteTercero() { }

        /// <summary>
        /// Constructor que inicializa el Id del Anexo
        /// </summary>
        /// <param name="id">Id del Anexo</param>
        public FusionPatenteTercero(int id)
        {
            this._id = id;
        }

        #endregion


        #region Overrides


        /// <summary>
        /// Sobreescritura del Método Equals debido a que la clase tiene id compuesto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as FusionPatenteTercero;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (Fusion.Id == (t.Fusion.Id)))
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


        #endregion


        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el nombre
        /// </summary>
        public virtual string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el domicilio
        /// </summary>
        public virtual string Domicilio
        {
            get { return _domicilio; }
            set { _domicilio = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el estado
        /// </summary>
        public virtual Estado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el estado
        /// </summary>
        public virtual Pais Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el estado
        /// </summary>
        public virtual Pais Nacionalidad
        {
            get { return _nacionalidad; }
            set { _nacionalidad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el estado
        /// </summary>
        public virtual FusionPatente Fusion
        {
            get { return _fusion; }
            set { _fusion = value; }
        }

        #endregion

    }
}
