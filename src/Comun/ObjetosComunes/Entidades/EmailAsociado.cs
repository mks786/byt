using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class EmailAsociado
    {

        #region Atributos

        private string _id;
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
        public EmailAsociado(string id)
        {
            this._id = id;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Anexo
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
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
    }
}
