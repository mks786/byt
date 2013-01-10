using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacOperacion
    {
        #region "Atributos"

            private  string _id;
            private  int? _codigooperacion;
            private  DateTime _fechaoperacion; 
            private  Asociado _asociado;
            private  Idioma _idioma;
            private  Moneda _moneda ;
            private  double _monto;
            private  double _saldo;
            private  string _xoperacion;
            private  string _operacionimp;
            private  double _montobf;
            private  double _saldobf;
            private  string _st_sel;
            private  string _st_pro;
            private  int _relacion;
            private DateTime _fechaingreso;
            private bool _seleccion;
            private string _valorquery;

        #endregion

        #region "Constructores"
        public FacOperacion()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacOperacion
        /// </summary>
        /// <param name="id">Id de FacOperacion</param>
        public FacOperacion(string id,int? codigooperacion)
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
            var t = obj as FacOperacion;
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
        /// Propiedad que asigna u obtiene el id de la FacOperacion
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
        public virtual DateTime FechaOperacion
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

        public virtual string OperacionImp
        {
            get
            {
                return this._operacionimp;
            }
            set
            {
                this._operacionimp = value;
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

        public virtual string St_Sel
        {
            get
            {
                return this._st_sel;
            }
            set
            {
                this._st_sel = value;
            }
        }

        public virtual string St_Pro
        {
            get
            {
                return this._st_pro;
            }
            set
            {
                this._st_pro = value;
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

        public virtual DateTime FechaIngreso
        {
            get
            {
                return this._fechaingreso;
            }
            set
            {
                this._fechaingreso = value;
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

        public virtual string ValorQuery
        {
            get
            {
                return this._valorquery;
            }
            set
            {
                this._valorquery = value;
            }
        }

        #endregion

    }
}