using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class ChequeRecido
    {
        #region "Atributos"
        private Asociado _id;
        private string _ncheque;
        private DateTime? _fecha;
        private BancoG _bancog;
        private double _monto;
        private string _deposito;
        private DateTime _fechadeposito;
        private string _ndeposito;
        private FacBanco _banco;
        private DateTime? _fechareg;

        #endregion

        #region "Constructores"
        public ChequeRecido()
        {
        }
        // ''' <summary>
        // ''' Constructor que inicializa el Id de la ChequeRecido
        // ''' </summary>
        // ''' <param name="id">Id de la tasa</param>
        public ChequeRecido(Asociado id, string ncheque)
        {
            this._id = id;
            this._ncheque = ncheque;
        }


        #endregion

        #region "Propiedades"
        // para llave compuesta
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            // var t = ((ChequeRecido)obj);
            var t = obj as ChequeRecido;
            if (t == null)
            {
                return false;
            }
            if ((Id.Id == (t.Id.Id)) && (NCheque == t.NCheque))
            {
                //If (Doc_servicio = t.Doc_servicio) Then
                return true;
            }
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

        /// <summary>
        /// Propiedad que asigna u obtiene el id de la ChequeRecido
        /// </summary>
        public virtual Asociado Id
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
        // ''' Propiedad que asigna u obtiene
        // ''' </summary>
        public virtual string NCheque
        {
            get
            {
                return this._ncheque;
            }
            set
            {
                this._ncheque = value;
            }
        }

        public virtual DateTime? Fecha
        {
            get
            {
                return this._fecha;
            }
            set
            {
                this._fecha = value;
            }
        }

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene
        // ''' </summary>
        public virtual BancoG BancoG
        {
            get
            {
                return this._bancog;
            }
            set
            {
                this._bancog = value;
            }
        }

        public virtual double Monto
        {
            get
            {
                return this._monto;
            }
            set
            {
                this._monto = value;
            }
        }

        public virtual string Deposito
        {
            get
            {
                return this._deposito;
            }
            set
            {
                this._deposito = value;
            }
        }

        public virtual DateTime FechaDeposito
        {
            get
            {
                return this._fechadeposito;
            }
            set
            {
                this._fechadeposito = value;
            }
        }

        public virtual string NDeposito
        {
            get
            {
                return this._ndeposito;
            }
            set
            {
                this._ndeposito = value;
            }
        }

        public virtual FacBanco Banco
        {
            get
            {
                return this._banco;
            }
            set
            {
                this._banco = value;
            }
        }

        public virtual DateTime? FechaReg
        {
            get
            {
                return _fechareg;
            }
            set
            {
                _fechareg = value;
            }
        }
        #endregion

    }
}