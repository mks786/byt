using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacPagoBolivia
    {
        #region "Atributos"
        private Asociado _id;
        private DateTime? _fechabanco;
        private FacBanco _bancorec;
        private char _pagorec;
        private double _montorec;
        private double _montobol;
        private string _descripcionrec;
        private string _ipagado;
        private char _pagopag;               
        private BancoG _bancopag;
        private string _numeropag;
        private DateTime? _fechapago;        
        private DateTime? _fechareg;
        private Carta _carta;
        #endregion

        #region "Constructores"
        public FacPagoBolivia()
        {
        }
        // ''' <summary>
        // ''' Constructor que inicializa el Id de la FacPagoBolivia
        // ''' </summary>
        // ''' <param name="id">Id de la tasa</param>
        public FacPagoBolivia(Asociado id, DateTime? FechaBanco)
        {
            this._id = id;
            this._fechabanco = FechaBanco;
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
            // var t = ((FacPagoBolivia)obj);
            var t = obj as FacPagoBolivia;
            if (t == null)
            {
                return false;
            }
            if ((Id.Id == (t.Id.Id)) && (FechaBanco == t.FechaBanco))
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
        /// Propiedad que asigna u obtiene el id de la FacPagoBolivia
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
        public virtual DateTime? FechaBanco
        {
            get
            {
                return this._fechabanco;
            }
            set
            {
                this._fechabanco = value;
            }
        }

        public virtual FacBanco BancoRec
        {
            get
            {
                return this._bancorec;
            }
            set
            {
                this._bancorec = value;
            }
        }

        public virtual char PagoRec
        {
            get
            {
                return this._pagorec;
            }
            set
            {
                this._pagorec = value;
            }
        }

        public virtual String BPagoRec
        {
            get
            {
                if (this.PagoRec.Equals('D'))
                    return "Deposito";
                else
                    if (this.PagoRec.Equals('T'))
                        return "Transferencia";
                    else
                        return "";
            }
        }


        public virtual double MontoRec
        {
            get
            {
                return this._montorec;
            }
            set
            {
                this._montorec = value;
            }
        }

        public virtual double MontoBol
        {
            get
            {
                return this._montobol;
            }
            set
            {
                this._montobol = value;
            }
        }

        public virtual string DescripcionRec
        {
            get
            {
                return this._descripcionrec;
            }
            set
            {
                this._descripcionrec = value;
            }
        }

        public virtual string IPagado
        {
            get
            {
                return this._ipagado;
            }
            set
            {
                this._ipagado = value;
            }
        }

        public virtual char PagoPag
        {
            get
            {
                return this._pagopag;
            }
            set
            {
                this._pagopag = value;
            }
        }

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene
        // ''' </summary>
        public virtual BancoG BancoPag
        {
            get
            {
                return this._bancopag;
            }
            set
            {
                this._bancopag = value;
            }
        }
        public virtual string NumeroPag
        {
            get
            {
                return this._numeropag;
            }
            set
            {
                this._numeropag = value;
            }
        }

        public virtual DateTime? FechaPago
        {
            get
            {
                return this._fechapago;
            }
            set
            {
                this._fechapago = value;
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

        public virtual Carta Carta
        {
            get
            {
                return this._carta;
            }
            set
            {
                this._carta = value;
            }
        }
        #endregion

    }
}