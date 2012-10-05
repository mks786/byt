using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Corresponsal
    {

        #region Atributos

        private int _id;
        private string _descripcion;
        private string _domicilio;
        private string _contribuyente;
        private string _rif;
        private string _telefono1;
        private string _telefono2;
        private string _telefono3;
        private string _fax1;
        private string _fax2;
        private string _fax3;
        private float _porcentajeDescuento;
        private string _observacion;
        private char _activo;
        private char _persona;
        private string _nit;
        private string _email;
        private char _statement;
        private string _web;
        private char _estado;
        private char _tsf;
        private string _email1;
        private char _tieneAlerta;
        private string _alerta;
        private string _penori;
        private string _email3;
        private char _isp;
        private string _email4;
        private Pais _pais;
        private Idioma _idioma;
        private Moneda _moneda;
        private Tarifa _tarifa;
        private TipoCliente _tipoCliente;
        private Etiqueta _etiqueta;
        private DetallePago _detallePago;
        private string _operacion;

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Corresponsal() { }

        /// <summary>
        /// Constructor que inicializa el condigo del corresponsal
        /// </summary>
        /// <param name="codigo">Codigo de la Corresponsal</param>
        public Corresponsal(int id)
        {
            this._id = id;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el código
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Domicilio
        /// </summary>
        public virtual string Domicilio
        {
            get { return _domicilio; }
            set { _domicilio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Contribuyente
        /// </summary>
        public virtual string Contribuyente
        {
            get { return _contribuyente; }
            set { _contribuyente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Rif
        /// </summary>
        public virtual string Rif
        {
            get { return _rif; }
            set { _rif = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Telefono1
        /// </summary>
        public virtual string Telefono1
        {
            get { return _telefono1; }
            set { _telefono1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Telefono2
        /// </summary>
        public virtual string Telefono2
        {
            get { return _telefono2; }
            set { _telefono2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Telefono3
        /// </summary>
        public virtual string Telefono3
        {
            get { return _telefono3; }
            set { _telefono3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Fax1
        /// </summary>
        public virtual string Fax1
        {
            get { return _fax1; }
            set { _fax1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Fax2
        /// </summary>
        public virtual string Fax2
        {
            get { return _fax2; }
            set { _fax2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Fax3
        /// </summary>
        public virtual string Fax3
        {
            get { return _fax3; }
            set { _fax3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Porcentaje del Descuento
        /// </summary>
        public virtual float PorcentajeDescuento
        {
            get { return _porcentajeDescuento; }
            set { _porcentajeDescuento = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion
        /// </summary>
        public virtual string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Activo
        /// </summary>
        public virtual char Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Persona
        /// </summary>
        public virtual char Persona
        {
            get { return _persona; }
            set { _persona = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Nit
        /// </summary>
        public virtual string Nit
        {
            get { return _nit; }
            set { _nit = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Email
        /// </summary>
        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Statement
        /// </summary>
        public virtual char Statement
        {
            get { return _statement; }
            set { _statement = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Web
        /// </summary>
        public virtual string Web
        {
            get { return _web; }
            set { _web = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Estado
        /// </summary>
        public virtual char Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Tsf
        /// </summary>
        public virtual char Tsf
        {
            get { return _tsf; }
            set { _tsf = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Email1
        /// </summary>
        public virtual string Email1
        {
            get { return _email1; }
            set { _email1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene si tiene alerta
        /// </summary>
        public virtual char TieneAlerta
        {
            get { return _tieneAlerta; }
            set { _tieneAlerta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Alerta
        /// </summary>
        public virtual string Alerta
        {
            get { return _alerta; }
            set { _alerta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Penori
        /// </summary>
        public virtual string Penori
        {
            get { return _penori; }
            set { _penori = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Email3
        /// </summary>
        public virtual string Email3
        {
            get { return _email3; }
            set { _email3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Isp
        /// </summary>
        public virtual char Isp
        {
            get { return _isp; }
            set { _isp = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Email4
        /// </summary>
        public virtual string Email4
        {
            get { return _email4; }
            set { _email4 = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Pais
        /// </summary>
        public virtual Pais Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Idioma
        /// </summary>
        public virtual Idioma Idioma
        {
            get { return _idioma; }
            set { _idioma = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Moneda
        /// </summary>
        public virtual Moneda Moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Tarifa
        /// </summary>
        public virtual Tarifa Tarifa
        {
            get { return _tarifa; }
            set { _tarifa = value; }
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

        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        #endregion

    }
}
