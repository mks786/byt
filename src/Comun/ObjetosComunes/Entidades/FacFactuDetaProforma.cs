using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacFactuDetaProforma
    {
        #region "Atributos"

            private  int? _id;
            private FacFacturaProforma _factura;
            private double _bdetalle;
            private string _xdetalle;
            private string _cservicio;
            private  int? _pendiente ;
            private FacServicio _servicio;
            private int? _ncantidad;
            private double _pu;
            private double _descuento;
            private char _bsel;
            private char _tiposervicio;
            private int? _codigo;
            private char _iimp;
            private string _xdetallees;
            private double _bdetallees;
            private double _tasa;
            private char _impuesto;
            private double _mimpuesto;
            private double _mdescuento;
            private double _bdetallebf;
                    private double _pubf;
                    private double _mimpuestobf;
                    private double _mdescuentobf;
                    private char _desglose;
            private bool _seleccion;
            private bool _desactivar_desglose;

        #endregion

        #region "Constructores"
        public FacFactuDetaProforma()
        {
            this._bsel = 'F';
            this._desglose = 'F';
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacFactuDetaProforma
        /// </summary>
        /// <param name="id">Id de FacFactuDetaProforma</param>
        public FacFactuDetaProforma(int? id, FacFacturaProforma factura)
        {
            this._id = id;
            this._factura = factura;
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
            var t = obj as FacFactuDetaProforma;
            if (t == null)
            {
                return false;
            }
            if ((Id == (t.Id)) && (Factura == t.Factura))
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
        /// Propiedad que asigna u obtiene el id de la FacFactuDetaProforma
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

        public virtual FacFacturaProforma Factura
        {
            get
            {
                return this._factura;
            }
            set
            {
                this._factura = value;
            }
        }
        // ''' <summary>
        // ''' Propiedad que asigna u obtiene xbanco
        // ''' </summary>
        public virtual double BDetalle
        {
            get
            {
                return this._bdetalle;
            }
            set
            {
                this._bdetalle = value;
            }
        }

        public virtual string XDetalle
        {
            get
            {
                return this._xdetalle;
            }
            set
            {
                this._xdetalle = value;
            }
        }

        public virtual string CServicio
        {
            get
            {
                return this._cservicio;
            }
            set
            {
                this._cservicio = value;
            }
        }

        public virtual int? Pendiente
        {
            get
            {
                return this._pendiente;
            }
            set
            {
                this._pendiente = value;
            }
        }

        public virtual FacServicio Servicio
        {
            get
            {
                return this._servicio;
            }
            set
            {
                this._servicio = value;
            }
        }

        public virtual int? NCantidad
        {
            get
            {
                return this._ncantidad;
            }
            set
            {
                this._ncantidad = value;
            }
        }

        public virtual double Pu
        {
            get
            {
                return this._pu;
            }
            set
            {
                this._pu = value;
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

        public virtual char Bsel
        {
            get
            {
                return this._bsel;
            }
            set
            {
                this._bsel = value;
            }
        }

        public virtual bool BBsel
        {
            get
            {
                if (this.Bsel.Equals('T'))
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
                    this.Bsel = 'T';
                }
                else
                {
                    this.Bsel = 'F';
                }
            }
        }


        public virtual char TipoServicio
        {
            get
            {
                return this._tiposervicio;
            }
            set
            {
                this._tiposervicio = value;
            }
        }

        public virtual int? Codigo
        {
            get
            {
                return this._codigo;
            }
            set
            {
                this._codigo = value;
            }
        }

        public virtual char Iimp
        {
            get
            {
                return this._iimp;
            }
            set
            {
                this._iimp = value;
            }
        }

        public virtual string XDetalleEs
        {
            get
            {
                return this._xdetallees;
            }
            set
            {
                this._xdetallees = value;
            }
        }

        //public virtual double BDetallleEs
        //{
        //    get
        //    {
        //        return this._bdetallees;
        //    }
        //    set
        //    {
        //        this._bdetallees = value;
        //    }
        //}

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
      
        public virtual char Impuesto
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

        public virtual double MImpuesto
        {
            get
            {
                return this._mimpuesto;
            }
            set
            {
                this._mimpuesto = value;
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

        public virtual double BDetalleBf
        {
            get
            {
                return this._bdetallebf;
            }
            set
            {
                this._bdetallebf = value;
            }
        }

        public virtual double PuBf
        {
            get
            {
                return this._pubf;
            }
            set
            {
                this._pubf = value;
            }
        }

        public virtual double MImpuestoBf
        {
            get
            {
                return this._mimpuestobf;
            }
            set
            {
                this._mimpuestobf = value;
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

        public virtual double BDetalleEs
        {
            get
            {
                return this._bdetallees;
            }
            set
            {
                this._bdetallees = value;
            }
        }

        public virtual char Desglose
        {
            get
            {
                return this._desglose;
            }
            set
            {
                this._desglose = value;
            }
        }

        public virtual bool BDesglose
        {
            get
            {
                if (this.Desglose.Equals('T'))
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
                    this.Desglose = 'T';
                }
                else
                {
                    this.Desglose = 'F';
                }
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

        public virtual bool Desactivar_Desglose
        {
            get
            {
                return this._desactivar_desglose;
            }
            set
            {
                this._desactivar_desglose = value;
            }
        }

        #endregion

    }
}