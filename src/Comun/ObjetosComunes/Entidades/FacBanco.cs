using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacBanco
    {
        #region "Atributos"

        private int _id;
        private string _xbanco;
        private string _contacto;
        private double _saldo;
        private DateTime _fechasaldo;
        private string _iw;
        private string _moneda;
        private string _xbancoweb;
        private string _ides;
        private int _relacion;


        #endregion

        #region "Constructores"
        public FacBanco()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacBanco
        /// </summary>
        /// <param name="id">Id de la tasa</param>
        public FacBanco(int id)
        {
            this._id = id;
        }
        #endregion

        #region "Propiedades"
        /// <summary>
        /// Propiedad que asigna u obtiene el id de la FacBanco
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



        public virtual double Saldo
        {
            get
            {
                return this._saldo;
            }
            set
            {
                this._saldo = value;
            }
        }

        public virtual DateTime FechaSaldo
        {
            get
            {
                return this._fechasaldo;
            }
            set
            {
                this._fechasaldo = value;
            }
        }

        public virtual string Iw
        {
            get
            {
                return this._iw;
            }
            set
            {
                this._iw = value;
            }
        }

        public virtual string Moneda
        {
            get
            {
                return this._moneda;
            }
            set
            {
                this._moneda = value;
            }
        }

        public virtual string XbancoWeb
        {
            get
            {
                return this._xbancoweb;
            }
            set
            {
                this._xbancoweb = value;
            }
        }

        public virtual string Ides
        {
            get
            {
                return this._ides;
            }
            set
            {
                this._ides = value;
            }
        }

        public virtual int Relacion
        {
            get
            {
                return this._relacion;
            }
            set
            {
                this._relacion = value;
            }
        }
        #endregion

    }
}