using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacFacturaProforma
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
            private FacDetalleEnvio _detalleenvio;
            private char _imulmon;
            private Moneda _monedaimp;
            private double _tasacambio;
            private string _codeti;
            private int? _numerocontrol;
            private char _local;
            private char _auto;    
            private string _ourref;
            private string _instruc;
            private DateTime? _fechaecuota;
            private string _xcausaRec;
            private Carta _carta;
            private string _codigodepartamento;            
            private int? _codigosocio;
            private double _msocio;
            private double _mcia;
            private string _condfac;
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
            private bool _selecreg;
            private bool _selecpag;
            private int? _status;
            private int? _accion;
            private DateTime? _fechadesde;
            private DateTime? _fechahasta;
                    
            private int? _p_mip;
            private int? _bst;


        #endregion

        #region "Constructores"
        public FacFacturaProforma()
        {
            this._imulmon = 'F';

        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacFacturaProforma
        /// </summary>
        /// <param name="id">Id de FacFacturaProforma</param>
        public FacFacturaProforma(int? id)
        {
            this._id = id;
        }
        #endregion

        #region "Propiedades"
        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return false;
        //    }
        //    // var t = ((ChequeRecido)obj);
        //    var t = obj as FacFacturaProforma;
        //    if (t == null)
        //    {
        //        return false;
        //    }
        //    if ((Id == (t.Id)) && (CodigoOperacion == t.CodigoOperacion))
        //    {
        //        //If (Doc_servicio = t.Doc_servicio) Then
        //        return true;
        //    }
        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        //public override string ToString()
        //{
        //    return base.ToString();
        //}

        /// <summary>
        /// Propiedad que asigna u obtiene el id de la FacFacturaProforma
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

        public virtual FacDetalleEnvio DetalleEnvio
        {
            get
            {
                return this._detalleenvio;
            }
            set
            {
                this._detalleenvio = value;
            }
        }

        public virtual char IMulmon
        {
            get
            {
                return this._imulmon;
            }
            set
            {
                this._imulmon = value;
            }
        }

        public virtual bool BIMulmon
        {
            get
            {
                if (this.IMulmon.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.IMulmon = 'T';
                }
                else
                {
                    this.IMulmon = 'F';
                }
            }
        }

        public virtual Moneda MonedaImp
        {
            get
            {
                return this._monedaimp;
            }
            set
            {
                this._monedaimp = value;
            }
        }

        public virtual double TasaCambio
        {
            get
            {
                return this._tasacambio;
            }
            set
            {
                this._tasacambio = value;
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

        public virtual char Local
        {
            get
            {
                return this._local;
            }
            set
            {
                this._local = value;
            }
        }

        public virtual char Auto
        {
            get
            {
                return this._auto;
            }
            set
            {
                this._auto = value;
            }
        }

        public virtual String BAuto
        {
            get
            {
                if (this.Auto.Equals('0'))
                    return "Pendiente";
                else
                    if (this.Auto.Equals('1'))
                        return "Autorizado";
                    else
                        if (this.Auto.Equals('2'))
                            return "Facturado";
                        else
                            if (this.Auto.Equals('3'))
                                return "Rechazado";
                            else
                                return "";
            }
        }

        public virtual string Ourref
        {
            get
            {
                return this._ourref;
            }
            set
            {
                this._ourref = value;
            }
        }

        public virtual string Instruc
        {
            get
            {
                return this._instruc;
            }
            set
            {
                this._instruc = value;
            }
        }

        public virtual DateTime? FechaEcuota
        {
            get
            {
                return this._fechaecuota;
            }
            set
            {
                this._fechaecuota = value;
            }
        }

        public virtual string XCausaRec
        {
            get
            {
                return this._xcausaRec;
            }
            set
            {
                this._xcausaRec = value;
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

        public virtual string CodigoDepartamento
        {
            get
            {
                return this._codigodepartamento;
            }
            set
            {
                this._codigodepartamento = value;
            }
        }

        public virtual int? CodigoSocio
        {
            get
            {
                return this._codigosocio;
            }
            set
            {
                this._codigosocio = value;
            }
        }

        public virtual double MSocio
        {
            get
            {
                return this._msocio;
            }
            set
            {
                this._msocio = value;
            }
        }

        public virtual double MCia
        {
            get
            {
                return this._mcia;
            }
            set
            {
                this._mcia = value;
            }
        }

        public virtual string CondFac
        {
            get
            {
                return this._condfac;
            }
            set
            {
                this._condfac = value;
            }
        }

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

        public virtual bool SelecReg
        {
            get
            {
                return this._selecreg;
            }
            set
            {
                this._selecreg = value;
            }
        }

        public virtual bool Selecpag
        {
            get
            {
                return this._selecpag;
            }
            set
            {
                this._selecpag = value;
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

        public virtual int? Accion
        {
            //1 = modificar sin el boton regresar
            //2 = no modificar
            //3= modificar con el boton regresar
            get
            {
                return this._accion;
            }
            set
            {
                this._accion = value;
            }
        }

        public virtual DateTime? FechaDesde
        {
            get
            {
                return this._fechadesde;
            }
            set
            {
                this._fechadesde = value;
            }
        }

        public virtual DateTime? FechaHasta
        {
            get
            {
                return this._fechahasta;
            }
            set
            {
                this._fechahasta = value;
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