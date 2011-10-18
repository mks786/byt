using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Asociado
    {
        #region Atributos

        private int _id;
        private string _nombre;
        private char _tipoPersona;
        private string _domicilio;
        private Pais _pais;
        private string _contribuyente;
        private string _rif;
        private string _nit;
        private Idioma _idioma;
        private Moneda _moneda;
        private int _descuento;
        private string _telefono1;
        private string _telefono2;
        private string _telefono3;
        private string _fax1;
        private string _fax2;
        private string _fax3;
        private string _email;
        private string _web;
        private int _diaCredito;
        private TipoCliente _tipoCliente;
        private string _activo;
        private char _edoCuenta;
        private string _edoCuentaDigital;
        private char _pendienteStatement;
        private char _isf;
        private char _alerta;
        private string _alarmaDescripcion;
        private Tarifa _tarifa;
        private Etiqueta _etiqueta;
        private DetallePago _detallePago;
        private string _operacion;
        private IList<Justificacion> _justificaciones;
        private IList<Carta> _cartas;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Asociado()
        {
            this._activo = "NO";
            this._contribuyente = "NO";
            this._edoCuentaDigital = "NO";
            this._edoCuenta = 'F';
            this._isf = 'F';
            this._alerta = 'F';
        }

        /// <summary>
        /// Constructor que inicializa el condigo del asociado
        /// </summary>
        /// <param name="codigo">Codigo del asociado</param>
        public Asociado(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el código del asociado
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el nombre del asociado
        /// </summary>
        public virtual string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el tipo de perosna que es el asociado
        /// </summary>
        public virtual char TipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el domicilio del asociado
        /// </summary>
        public virtual string Domicilio
        {
            get { return _domicilio; }
            set { _domicilio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el pais del asociado
        /// </summary>
        public virtual Pais Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene si un asociado es contribuyente o no
        /// </summary>
        public virtual string Contribuyente
        {
            get { return _contribuyente; }
            set { _contribuyente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de contribuyente
        /// </summary>
        public virtual bool BContribuyente
        {
            get
            {
                if (this.Contribuyente != null && this.Contribuyente.Equals("SI"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Contribuyente = "SI";
                else
                    this.Contribuyente = "NO";
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el rif del asociado
        /// </summary>
        public virtual string Rif
        {
            get { return _rif; }
            set { _rif = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el nit del asociado
        /// </summary>
        public virtual string Nit
        {
            get { return _nit; }
            set { _nit = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el idioma del asociado
        /// </summary>
        public virtual Idioma Idioma
        {
            get { return _idioma; }
            set { _idioma = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el moneda del asociado
        /// </summary>
        public virtual Moneda Moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el descuento del asociado
        /// </summary>
        public virtual int Descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el telefono1 del asociado
        /// </summary>
        public virtual string Telefono1
        {
            get { return _telefono1; }
            set { _telefono1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el telefono2 del asociado
        /// </summary>
        public virtual string Telefono2
        {
            get { return _telefono2; }
            set { _telefono2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el telefono3 del asociado
        /// </summary>
        public virtual string Telefono3
        {
            get { return _telefono3; }
            set { _telefono3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el fax1 del asociado
        /// </summary>
        public virtual string Fax1
        {
            get { return _fax1; }
            set { _fax1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el fax2 del asociado
        /// </summary>
        public virtual string Fax2
        {
            get { return _fax2; }
            set { _fax2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el fax3 del asociado
        /// </summary>
        public virtual string Fax3
        {
            get { return _fax3; }
            set { _fax3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el email del asociado
        /// </summary>
        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el web del asociado
        /// </summary>
        public virtual string Web
        {
            get { return _web; }
            set { _web = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene los dias de creditos del asociado
        /// </summary>
        public virtual int DiaCredito
        {
            get { return _diaCredito; }
            set { _diaCredito = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el tipo del asociado
        /// </summary>
        public virtual TipoCliente TipoCliente
        {
            get { return _tipoCliente; }
            set { _tipoCliente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene si el asociado esta activo
        /// </summary>
        public virtual string Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de activo
        /// </summary>
        public virtual bool BActivo
        {
            get
            {
                if (this.Activo != null && this.Activo.Equals("SI"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Activo = "SI";
                else
                    this.Activo = "NO";
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene si se le manda estados de cuenta
        /// </summary>
        public virtual char EdoCuenta
        {
            get { return _edoCuenta; }
            set { _edoCuenta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano del estado de cuenta
        /// </summary>
        public virtual bool BEdoCuenta
        {
            get
            {
                if (this.EdoCuenta != null && this.EdoCuenta.Equals('T'))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.EdoCuenta = 'T';
                else
                    this.EdoCuenta = 'F';
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene si se le manda estados de cuentas digitales
        /// </summary>
        public virtual string EdoCuentaDigital
        {
            get { return _edoCuentaDigital; }
            set { _edoCuentaDigital = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el booleano del estado de cuenta digital
        /// </summary>
        public virtual bool BEdoCuentaDigital
        {
            get
            {
                if (this.EdoCuentaDigital != null && this.EdoCuentaDigital.Equals("SI"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.EdoCuentaDigital = "SI";
                else
                    this.EdoCuentaDigital = "NO";
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene si esta pendiente con statement
        /// </summary>
        public virtual char PendienteStatement
        {
            get { return _pendienteStatement; }
            set { _pendienteStatement = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano del estado de pendiente con statement
        /// </summary>
        public virtual bool BPendienteStatement
        {
            get
            {
                if (this.PendienteStatement != null && this.PendienteStatement.Equals('T'))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.PendienteStatement = 'T';
                else
                    this.PendienteStatement = 'F';
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene si tiene isf
        /// </summary>
        public virtual char Isf
        {
            get { return _isf; }
            set { _isf = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano del estado de Isf
        /// </summary>
        public virtual bool BIsf
        {
            get
            {
                if (this.Isf != null && this.Isf.Equals('T'))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Isf = 'T';
                else
                    this.Isf = 'F';
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene si tiene alerta
        /// </summary>
        public virtual char Alerta
        {
            get { return _alerta; }
            set { _alerta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano del estado de Alerta
        /// </summary>
        public virtual bool BAlerta
        {
            get
            {
                if (this.Alerta != null && this.Alerta.Equals('T'))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Alerta = 'T';
                else
                    this.Alerta = 'F';
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene descripcion de la alerta
        /// </summary>
        public virtual string AlarmaDescripcion
        {
            get { return _alarmaDescripcion; }
            set { _alarmaDescripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la tarifa del asociado
        /// </summary>
        public virtual Tarifa Tarifa
        {
            get { return _tarifa; }
            set { _tarifa = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la etiqueta del asociado
        /// </summary>
        public virtual Etiqueta Etiqueta
        {
            get { return _etiqueta; }
            set { _etiqueta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el detalle de pago asociado
        /// </summary>
        public virtual DetallePago DetallePago
        {
            get { return _detallePago; }
            set { _detallePago = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la operacion
        /// </summary>
        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la lista de justificaciones
        /// de un asociado
        /// </summary>
        public virtual IList<Justificacion> Justificaciones
        {
            get { return _justificaciones; }
            set { _justificaciones = value; }
        }


        public virtual IList<Carta> Cartas
        {
            get { return _cartas; }
            set { _cartas = value; }
        }

        #endregion
    }
}
