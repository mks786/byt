using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacVistaFacturacionCxpInterna
    {
        #region "Atributos"

           private int? _id;//Proforma           
           private Asociado _asociado;
           private string _xasociado;
           private Asociado _asociado_o;
           private string _xasociado1;
           private string _numerofactura;
           private double? _monto;
           private DateTime? _fecha;
           private int? _codpais;
           private string _pais;
           private string _detalle;
           private DateTime? _fechapago;
           private string _montopago;
           private string _descuentopago;
           private int? _codigobanco;
           private string _banco;
           private string _aniofactura;
           private string _mesfactura;
           private string _facturada;
           private string _cobrada;
           private int? _cobro;
           private DateTime? _fechacobro;
           private string _pagada;
           private int? _factura;
           private DateTime? _fechafactura;
           private int? _diasvencida;
           private string _vencida;
           private DateTime? _fecharecepcion;
           private string _estatus;



           private int? _accion;

        #endregion

        #region "Constructores"
        public FacVistaFacturacionCxpInterna()
        {
        }
        // ''' <summary>
        // ''' Constructor que inicializa el Id de la FacVistaFacturacionCxpInterna
        // ''' </summary>
        // ''' <param name="id">Id de la tasa</param>
        public FacVistaFacturacionCxpInterna(int? id)
        {
            this._id = id;
        }

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

        //public virtual Asociado Asociado
        //{
        //    get
        //    {
        //        return this._asociado;
        //    }
        //    set
        //    {
        //        this._asociado = value;
        //    }
        //}

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene
        // ''' </summary>
        public virtual string Xasociado
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

        public virtual Asociado Asociado_o
        {
            get
            {
                return this._asociado_o;
            }
            set
            {
                this._asociado_o = value;
            }
        }

        public virtual string Xasociado1
        {
            get
            {
                return this._xasociado1;
            }
            set
            {
                this._xasociado1 = value;
            }
        }

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene
        // ''' </summary>
        public virtual string NumeroFactura
        {
            get
            {
                return this._numerofactura;
            }
            set
            {
                this._numerofactura = value;
            }
        }

        public virtual double? Monto
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


        //public virtual DateTime? Fecha
        //{
        //    get
        //    {
        //        return this._fecha;
        //    }
        //    set
        //    {
        //        this._fecha = value;
        //    }
        //}

        public virtual int? CodPais
        {
            get
            {
                return _codpais;
            }
            set
            {
                _codpais = value;
            }
        }

        public virtual string Pais
        {
            get
            {
                return _pais;
            }
            set
            {
                _pais = value;
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

        public virtual string MontoPago
        {
            get
            {
                return this._montopago;
            }
            set
            {
                this._montopago = value;
            }
        }

        public virtual string DescuentoPago
        {
            get
            {
                return this._descuentopago;
            }
            set
            {
                this._descuentopago = value;
            }
        }

        public virtual int? CodigoBanco
        {
            get
            {
                return this._codigobanco;
            }
            set
            {
                this._codigobanco = value;
            }
        }

        public virtual string Banco
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

        public virtual string AnioFactura
        {
            get
            {
                return this._aniofactura;
            }
            set
            {
                this._aniofactura = value;
            }
        }

        public virtual string MesFactura
        {
            get
            {
                return this._mesfactura;
            }
            set
            {
                this._mesfactura = value;
            }
        }

        public virtual string Facturada
        {
            get
            {
                return this._facturada;
            }
            set
            {
                this._facturada = value;
            }
        }

        public virtual string Cobrada
        {
            get
            {
                return this._cobrada;
            }
            set
            {
                this._cobrada = value;
            }
        }

        public virtual int? Cobro
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

        public virtual DateTime? FechaCobro
        {
            get
            {
                return this._fechacobro;
            }
            set
            {
                this._fechacobro = value;
            }
        }

        public virtual string Pagada
        {
            get
            {
                return this._pagada;
            }
            set
            {
                this._pagada = value;
            }
        }

        public virtual int? Factura
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

        public virtual int? DiasVencida
        {
            get
            {
                return this._diasvencida;
            }
            set
            {
                this._diasvencida = value;
            }
        }

        public virtual string Vencida
        {
            get
            {
                return this._vencida;
            }
            set
            {
                this._vencida = value;
            }
        }

        public virtual DateTime? FechaRecepcion
        {
            get
            {
                return this._fecharecepcion;
            }
            set
            {
                this._fecharecepcion = value;
            }
        }

        public virtual string Estatus
        {
            get
            {
                return this._estatus;
            }
            set
            {
                this._estatus = value;
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
        #endregion

    }
}