using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacOperacionProforma
    {
        #region "Atributos"

            private  string _id;
            private  int? _codigooperacion;
            private  DateTime? _fechaoperacion; 
            private  Asociado _asociado;
            private  Idioma _idioma;
            private  Moneda _moneda ;
            private  double _monto;
            private  double _saldo;
            private  string _xoperacion;
            private string _fechaoperacionimp;
            private  double _montobf;
            private  double _saldobf;

            private bool _seleccion;

        #endregion

        #region "Constructores"
        public FacOperacionProforma()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacOperacionProforma
        /// </summary>
        /// <param name="id">Id de FacOperacionProforma</param>
        public FacOperacionProforma(string id,int? codigooperacion)
        {
            this._id = id;
            this._codigooperacion = codigooperacion;
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
            var t = obj as FacOperacionProforma;
            if (t == null)
            {
                return false;
            }
            if ((Id == (t.Id)) && (CodigoOperacion == t.CodigoOperacion))
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
        /// Propiedad que asigna u obtiene el id de la FacOperacionProforma
        /// </summary>
        public virtual string Id
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

        public virtual int? CodigoOperacion
        {
            get
            {
                return this._codigooperacion;
            }
            set
            {
                this._codigooperacion = value;
            }
        }
        // ''' <summary>
        // ''' Propiedad que asigna u obtiene xbanco
        // ''' </summary>
        public virtual DateTime? FechaOperacion
        {
            get
            {
                return this._fechaoperacion;
            }
            set
            {
                this._fechaoperacion = value;
            }
        }

        public virtual Asociado Asociado
        {
            get
            {
                return this._asociado;
            }
            set
            {
                this._asociado = value;
            }
        }

        public virtual Idioma Idioma
        {
            get
            {
                return this._idioma;
            }
            set
            {
                this._idioma = value;
            }
        }

        public virtual Moneda Moneda
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

        public virtual string XOperacion
        {
            get
            {
                return this._xoperacion;
            }
            set
            {
                this._xoperacion = value;
            }
        }

        public virtual string FechaOperacionImp
        {
            get
            {
                return this._fechaoperacionimp;
            }
            set
            {
                this._fechaoperacionimp = value;
            }
        }

        public virtual double MontoBf
        {
            get
            {
                return this._montobf;
            }
            set
            {
                this._montobf = value;
            }
        }

        public virtual double SaldoBf
        {
            get
            {
                return this._saldobf;
            }
            set
            {
                this._saldobf = value;
            }
        }

        public virtual bool Seleccion
        {
            get
            {
                return this._seleccion;
            }
            set
            {
                this._seleccion = value;
            }
        }

        #endregion

    }
}