using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class BancoG
    {
        #region "Atributos"

        private int _id;
        private string _xbanco;
        private string _contacto;
        #endregion

        #region "Constructores"
        public BancoG()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la BancoG
        /// </summary>
        /// <param name="id">Id de la tasa</param>
        public BancoG(int id)
        {
            this._id = id;
        }
        #endregion

        #region "Propiedades"
        /// <summary>
        /// Propiedad que asigna u obtiene el id de la BancoG
        /// </summary>
        public virtual int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene xbanco
        // ''' </summary>
        public virtual string XBanco
        {
            get
            {
                return this._xbanco;
            }
            set
            {
                this._xbanco = value;
            }
        }

        public virtual string Contacto
        {
            get
            {
                return this._contacto;
            }
            set
            {
                this._contacto = value;
            }
        }

        #endregion

    }
}