using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacFacturaAnulada
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
            private FacMotivo _motivo;
            private int? _control;
            private string _detalle;
            private int? _secanula;
            private DateTime? _fechaanulacion;
            private int? _secanula2;
            private FacMotivo _motivo2;
            private int? _control2;
            private string _detalle2;
           // private string _instruc;
           //private int? _entrada;
           // private string _codigodepartamento;
           // private string _codguia;
           // private int? _codigosocio;
           // private double _msocio;
           // private double _mcia;
           // private string _condfac;
            private double _msubtimpo;
            private double _mdescuento;
            private double _mtbimp;
            private double _mtbexc;
            private double _msubtotal;
            private double _mtimp;
            private double _mttotal;
            private double _msubtimpobf;
            private double _mdescuentobf;
            private double _mtbimpbf;
            private double _mtbexcbf;
            private double _msubtotalbf;
            private double _mtimpbf;
            private double _mttotalbf;
            private string _xasociado_o;

            private bool _seleccion;

            private int _xter;
            private bool _iintere;
        

        #endregion

        #region "Constructores"
        public FacFacturaAnulada()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacFacturaAnulada
        /// </summary>
        /// <param name="id">Id de FacFacturaAnulada</param>
        public FacFacturaAnulada(int? id)
        {
            this._id = id;
        }
        #endregion

        #region "Propiedades"


        /// <summary>
        /// Propiedad que asigna u obtiene el id de la FacFacturaAnulada
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

        public virtual Interesado  InteresadoImp
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

        public virtual FacMotivo Motivo
        {
            get
            {
                return this._motivo;
            }
            set
            {
                this._motivo = value;
            }
        }

        public virtual int? Control
        {
            get
            {
                return this._control;
            }
            set
            {
                this._control = value;
            }
        }

        public virtual string Detalle
        {
            get
            {
                return this._detalle;
            }
            set
            {
                this._detalle = value;
            }
        }

        public virtual int? Secanula
        {
            get
            {
                return this._secanula;
            }
            set
            {
                this._secanula = value;
            }
        }

        public virtual DateTime? FechaAnulacion
        {
            get
            {
                return this._fechaanulacion;
            }
            set
            {
                this._fechaanulacion = value;
            }
        }

        public virtual int? Secanula2
        {
            get
            {
                return this._secanula2;
            }
            set
            {
                this._secanula2 = value;
            }
        }

        public virtual FacMotivo Motivo2
        {
            get
            {
                return this._motivo2;
            }
            set
            {
                this._motivo2 = value;
            }
        }

        public virtual int? Control2
        {
            get
            {
                return this._control2;
            }
            set
            {
                this._control2 = value;
            }
        }

        public virtual string Detalle2
        {
            get
            {
                return this._detalle2;
            }
            set
            {
                this._detalle2 = value;
            }
        }

        //public virtual string Instruc
        //{
        //    get
        //    {
        //        return this._instruc;
        //    }
        //    set
        //    {
        //        this._instruc = value;
        //    }
        //}

        //public virtual int? Entrada
        //{
        //    get
        //    {
        //        return this._entrada;
        //    }
        //    set
        //    {
        //        this._entrada = value;
        //    }
        //}

        //public virtual string CodigoDepartamento
        //{
        //    get
        //    {
        //        return this._codigodepartamento;
        //    }
        //    set
        //    {
        //        this._codigodepartamento = value;
        //    }
        //}

        //public virtual string CodGuia
        //{
        //    get
        //    {
        //        return this._codguia;
        //    }
        //    set
        //    {
        //        this._codguia = value;
        //    }
        //}

        //public virtual int? CodigoSocio
        //{
        //    get
        //    {
        //        return this._codigosocio;
        //    }
        //    set
        //    {
        //        this._codigosocio = value;
        //    }
        //}

        //public virtual double MSocio
        //{
        //    get
        //    {
        //        return this._msocio;
        //    }
        //    set
        //    {
        //        this._msocio = value;
        //    }
        //}

        //public virtual double MCia
        //{
        //    get
        //    {
        //        return this._mcia;
        //    }
        //    set
        //    {
        //        this._mcia = value;
        //    }
        //}

        //public virtual string CondFac
        //{
        //    get
        //    {
        //        return this._condfac;
        //    }
        //    set
        //    {
        //        this._condfac = value;
        //    }
        //}

        public virtual double MSubtimpo
        {
            get
            {
                return this._msubtimpo;
            }
            set
            {
                this._msubtimpo = value;
            }
        }

        public virtual double MDescuento
        {
            get
            {
                return this._mdescuento;
            }
            set
            {
                this._mdescuento = value;
            }
        }

        public virtual double MTbimp
        {
            get
            {
                return this._mtbimp;
            }
            set
            {
                this._mtbimp = value;
            }
        }

        public virtual double Mtbexc
        {
            get
            {
                return this._mtbexc;
            }
            set
            {
                this._mtbexc = value;
            }
        }

        public virtual double MSubtotal
        {
            get
            {
                return this._msubtotal;
            }
            set
            {
                this._msubtotal = value;
            }
        }

        public virtual double Mtimp
        {
            get
            {
                return this._mtimp;
            }
            set
            {
                this._mtimp = value;
            }
        }

        public virtual double Mttotal
        {
            get
            {
                return this._mttotal;
            }
            set
            {
                this._mttotal = value;
            }
        }

        public virtual double MSubtimpoBf
        {
            get
            {
                return this._msubtimpobf;
            }
            set
            {
                this._msubtimpobf = value;
            }
        }

        public virtual double MDescuentoBf
        {
            get
            {
                return this._mdescuentobf;
            }
            set
            {
                this._mdescuentobf = value;
            }
        }

        public virtual double MTbimpBf
        {
            get
            {
                return this._mtbimpbf;
            }
            set
            {
                this._mtbimpbf = value;
            }
        }

        public virtual double MTbexcBf
        {
            get
            {
                return this._mtbexcbf;
            }
            set
            {
                this._mtbexcbf = value;
            }
        }

        public virtual double MSubtotalBf
        {
            get
            {
                return this._msubtotalbf;
            }
            set
            {
                this._msubtotalbf = value;
            }
        }

        public virtual double MTimpBf
        {
            get
            {
                return this._mtimpbf;
            }
            set
            {
                this._mtimpbf = value;
            }
        }

        public virtual double MTtotalBf
        {
            get
            {
                return this._mttotalbf;
            }
            set
            {
                this._mttotalbf = value;
            }
        }

        public virtual string XAsociado_O
        {
            get
            {
                return this._xasociado_o;
            }
            set
            {
                this._xasociado_o = value;
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

        public virtual bool Iintere
        {
            get
            {
                return this._iintere;
            }
            set
            {
                this._iintere = value;
            }
        }

        public virtual int Xter
        {
            get
            {
                return this._xter;
            }
            set
            {
                this._xter = value;
            }
        }

        #endregion

    }
}