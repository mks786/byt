using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacForma
    {
        #region "Atributos"

            private  int _id;
            private  FacCobro _cobro;
            private double _bforma;
            private  string _xforma; 
            private  FacCredito _credito;
            private double _bformabf;
            private double _tasa;
            private string _tipopago;

        #endregion

        #region "Constructores"
        public FacForma()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacForma
        /// </summary>
        /// <param name="id">Id de FacForma</param>
        public FacForma(int id,FacCobro cobro)
        {
            this._id = id;
            this._cobro = cobro;
        }
        #endregion

        #region "Propiedades"
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            // var t = ((ChequeRecido)obj);
            var t = obj as FacForma;
            if (t == null)
            {
                return false;
            }
            if ((Id == (t.Id)) && (Cobro.Id == t.Cobro.Id))
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
        /// Propiedad que asigna u obtiene el id de la FacForma
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

        public virtual FacCobro Cobro
        {
            get
            {
                return this._cobro;
            }
            set
            {
                this._cobro = value;
            }
        }
        // ''' <summary>
        // ''' Propiedad que asigna u obtiene xbanco
        // ''' </summary>
        public virtual double BForma
        {
            get
            {
                return this._bforma;
            }
            set
            {
                this._bforma = value;
            }
        }

        public virtual double BFormaBf
        {
            get
            {
                return this._bformabf;
            }
            set
            {
                this._bformabf = value;
            }
        }

        public virtual FacCredito Credito
        {
            get
            {
                return this._credito;
            }
            set
            {
                this._credito = value;
            }
        }

        public virtual double Tasa
        {
            get
            {
                return this._tasa;
            }
            set
            {
                this._tasa = value;
            }
        }

        public virtual string XForma
        {
            get
            {
                return this._xforma;
            }
            set
            {
                this._xforma = value;
            }
        }

        public virtual string TipoPago
        {
            get
            {
                return this._tipopago;
            }
            set
            {
                this._tipopago = value;
            }
        }

        #endregion

    }
}