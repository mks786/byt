using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacFacturaTotal
    {
        #region "Atributos"

            private  int? _id;
            private string _anulada;
            private DateTime? _fechafactura; 
            private  Asociado _asociado;
            private  Idioma _idioma;
            private  Moneda _moneda ;
            private string _caso;
            private string _inicial;
            private double _impuesto;
            private double _descuento;
            private Asociado _asociadoimp;
            private Interesado _interesadoimp;
            private char _terrero;
            private char _email;
            private int? _seniat;
            private DateTime? _fechaseniat;
            private double _pseniat;
            private char _ip;
            private string _xasociado;
            private string _rif;
            private string _xnit;


            private DateTime? _fechaoperacion;
            private Pais _pais;
            private string _codeti;
            private int? _numerocontrol;
            private string _xoperacion;
            private double _monto;
            private double _montobf;
            private int? _dias;
            private string _edoven;
            private string _edonot;


            private bool _seleccion;
            private int? _status;
            private int? _p_mip;
            private int? _bst;

        #endregion

        #region "Constructores"
        public FacFacturaTotal()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacFacturaTotal
        /// </summary>
        /// <param name="id">Id de FacFacturaTotal</param>
        public FacFacturaTotal(int? id)
        {
            this._id = id;
        }
        #endregion

        #region "Propiedades"

        /// <summary>
        /// Propiedad que asigna u obtiene el id de la FacFacturaTotal
        /// </summary>
        public virtual int? Id
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

        public virtual string Anulada
        {
            get
            {
                return this._anulada;
            }
            set
            {
                this._anulada = value;
            }
        }
        // ''' <summary>
        // ''' Propiedad que asigna u obtiene xbanco
        // ''' </summary>
        public virtual DateTime? FechaFactura
        {
            get
            {
                return this._fechafactura;
            }
            set
            {
                this._fechafactura = value;
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

        public virtual string Caso
        {
            get
            {
                return this._caso;
            }
            set
            {
                this._caso = value;
            }
        }

        public virtual string Inicial
        {
            get
            {
                return this._inicial;
            }
            set
            {
                this._inicial = value;
            }
        }

        public virtual double Impuesto
        {
            get
            {
                return this._impuesto;
            }
            set
            {
                this._impuesto = value;
            }
        }

        public virtual double Descuento
        {
            get
            {
                return this._descuento;
            }
            set
            {
                this._descuento = value;
            }
        }

        public virtual Asociado AsociadoImp
        {
            get
            {
                return this._asociadoimp;
            }
            set
            {
                this._asociadoimp = value;
            }
        }

        public virtual Interesado InteresadoImp
        {
            get
            {
                return this._interesadoimp;
            }
            set
            {
                this._interesadoimp = value;
            }
        }

        public virtual char Terrero
        {
            get
            {
                return this._terrero;
            }
            set
            {
                this._terrero = value;
            }
        }

        public virtual char Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public virtual int? Seniat
        {
            get
            {
                return this._seniat;
            }
            set
            {
                this._seniat = value;
            }
        }

        public virtual DateTime? FechaSeniat
        {
            get
            {
                return this._fechaseniat;
            }
            set
            {
                this._fechaseniat = value;
            }
        }
        
        public virtual double PSeniat
        {
            get
            {
                return this._pseniat;
            }
            set
            {
                this._pseniat = value;
            }
        }

        public virtual char IP
        {
            get
            {
                return this._ip;
            }
            set
            {
                this._ip = value;
            }
        }

        public virtual string XAsociado
        {
            get
            {
                return this._xasociado;
            }
            set
            {
                this._xasociado = value;
            }
        }

        public virtual string Rif
        {
            get
            {
                return this._rif;
            }
            set
            {
                this._rif = value;
            }
        }

        public virtual string XNit
        {
            get
            {
                return this._xnit;
            }
            set
            {
                this._xnit = value;
            }
        }

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

        public virtual Pais Pais
        {
            get
            {
                return this._pais;
            }
            set
            {
                this._pais = value;
            }
        }

        public virtual string Codeti
        {
            get
            {
                return this._codeti;
            }
            set
            {
                this._codeti = value;
            }
        }

        public virtual int? NumeroControl
        {
            get
            {
                return this._numerocontrol;
            }
            set
            {
                this._numerocontrol = value;
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

        public virtual int? Dias
        {
            get
            {
                return this._dias;
            }
            set
            {
                this._dias = value;
            }
        }

        public virtual string Edoven
        {
            get
            {
                return this._edoven;
            }
            set
            {
                this._edoven = value;
            }
        }

        public virtual string Edonot
        {
            get
            {
                return this._edonot;
            }
            set
            {
                this._edonot = value;
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

        public virtual int? Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public virtual int? P_mip
        {
            get
            {
                return this._p_mip;
            }
            set
            {
                this._p_mip = value;
            }
        }
        public virtual int? Bst
        {
            get
            {
                return this._bst;
            }
            set
            {
                this._bst = value;
            }
        }        
        #endregion

    }
}