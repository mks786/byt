using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacCredito
    {
        #region "Atributos"

           private int? _id;
           private DateTime? _fechacredito;
           private double _bcredito;
           private Asociado _asociado;
           private Idioma _idioma;
           private Moneda _moneda;
           private string _concepto;
           private double _bcreditobf;
           private DateTime? _timestamp;
           private FacBanco _banco;                      
           private int? _creditosent;
           private string _operacion;

           private int? _accion;
        #endregion

        #region "Constructores"
        public FacCredito()
        {
        }
        // ''' <summary>
        // ''' Constructor que inicializa el Id de la FacCredito
        // ''' </summary>
        // ''' <param name="id">Id de la tasa</param>
        public FacCredito(int? id)
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

        public virtual DateTime? FechaCredito
        {
            get
            {
                return this._fechacredito;
            }
            set
            {
                this._fechacredito = value;
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

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene
        // ''' </summary>
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
        
        public virtual DateTime? Timestamp
        {
            get
            {
                return this._timestamp;
            }
            set
            {
                this._timestamp = value;
            }
        }

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene
        // ''' </summary>
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

        public virtual double BCredito
        {
            get
            {
                return this._bcredito;
            }
            set
            {
                this._bcredito = value;
            }
        }

        public virtual double BCreditoBf
        {
            get
            {
                return this._bcreditobf;
            }
            set
            {
                this._bcreditobf = value;
            }
        }

        public virtual string Concepto
        {
            get
            {
                return this._concepto;
            }
            set
            {
                this._concepto = value;
            }
        }

        public virtual int? CreditoSent
        {
            get
            {
                return this._creditosent;
            }
            set
            {
                this._creditosent = value;
            }
        }

        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
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