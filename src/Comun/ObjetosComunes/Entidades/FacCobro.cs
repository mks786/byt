using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacCobro
    {
        #region "Atributos"

           private int? _id;
           private DateTime? _fechacobro;
           private Asociado _asociado;
           private Idioma _idioma;
           private Moneda _moneda;
           private DateTime? _timestamp;
           private FacBanco _banco;
           private int? _estadocuenta;
           private DateTime? _fechab;
           private string _detalle;
           private string _inicial;
           private char _envio; 
           private DateTime? _fechaenvio;

           private int? _accion;

        #endregion

        #region "Constructores"
        public FacCobro()
        {
        }
        // ''' <summary>
        // ''' Constructor que inicializa el Id de la FacCobro
        // ''' </summary>
        // ''' <param name="id">Id de la tasa</param>
        public FacCobro(int? id)
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

        public virtual int? EstadoCuenta
        {
            get
            {
                return this._estadocuenta;
            }
            set
            {
                this._estadocuenta = value;
            }
        }

        public virtual DateTime? FechaB
        {
            get
            {
                return this._fechab;
            }
            set
            {
                this._fechab = value;
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

        public virtual char Envio
        {
            get
            {
                return this._envio;
            }
            set
            {
                this._envio = value;
            }
        }

        public virtual DateTime? FechaEnvio 
        {
            get
            {
                return _fechaenvio;
            }
            set
            {
                _fechaenvio = value;
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