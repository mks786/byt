using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class EmailAsociado
    {

        #region Atributos

        private int _id;
        private Asociado _asociado;
        private TipoEmailAsociado _tipoEmailAsociado;
        private string _email;
        private string _operacion;

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public EmailAsociado() { }

        /// <summary>
        /// Constructor que inicializa el Id del Anexo
        /// </summary>
        /// <param name="id">Id del Anexo</param>
        public EmailAsociado(int id)
        {
            this._id = id;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Anexo
        /// </summary>
        public virtual int Id
        {
            get
            {
                //if (null != this._tipoEmailAsociado)
                //    return this._asociado.Id.ToString() + "-" + this._tipoEmailAsociado.Id.ToString();
                //else
                //    return string.Empty;

                return this._id;
            }
            set { 
                //this._id = this._asociado.Id.ToString() + "-" + this._tipoEmailAsociado.Id.ToString(); 
                this._id = value;
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Anexo
        /// </summary>
        public virtual string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Anexo
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return this._asociado; }
            set { this._asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Anexo
        /// </summary>
        public virtual TipoEmailAsociado TipoEmailAsociado
        {
            get { return this._tipoEmailAsociado; }
            set { this._tipoEmailAsociado = value; }
        }

        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
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
            var t = obj as Contacto;
            if (t == null)
                return false;
            if ((Id.Equals(t.Id)) && (Asociado.Id == (t.Asociado.Id)))
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
    }
}
