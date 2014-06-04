using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Caso
    {
        #region Atributos

        private int _id;
        private string _descripcion;
        private DateTime? _fecha;
        private string _origen;
        private string _primeraReferencia;
        private string _observacion;
        private string _tipoCaso;
        private string _tipoAccion;
        private Asociado _asociado;
        private Interesado _interesado;
        private Servicio _servicio;
        private string _interesadoCaso;
        private string _domicilioInteresado;
        private string _rifInteresado;
        private string _contactoInteresado;
        private string _cargoInteresado;
        private string _telefonoInteresado;
        private string _faxInteresado;
        private string _emailInteresado;
        private string _comentarioInteresado;
        private string _asociadoCaso;
        private string _domicilioAsociado;
        private string _rifAsociado;
        private string _contactoAsociado;
        private string _cargoAsociado;
        private string _telefonoAsociado;
        private string _faxAsociado;
        private string _emailAsociado;
        private string _comentarioAsociado;
        private string _operacion;
        private IList<TipoCaso> _tiposCaso;
        private IList<Accion> _acciones;
        private IList<CasoBase> _casosBase;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Caso()
        {
        }

        /// <summary>
        /// Constructor que recibe el Id del Caso
        /// </summary>
        /// <param name="id">Id del caso</param>
        public Caso(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Descripcion del Caso
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la FEcha del Caso
        /// </summary>
        public virtual DateTime? Fecha
        {
            get { return this._fecha; }
            set { this._fecha = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Origen del Caso
        /// </summary>
        public virtual string Origen
        {
            get { return this._origen; }
            set { this._origen = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Primera Referencia del Caso
        /// </summary>
        public virtual string PrimeraReferencia
        {
            get { return this._primeraReferencia; }
            set { this._primeraReferencia = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Observacion del Caso
        /// </summary>
        public virtual string Observacion
        {
            get { return this._observacion; }
            set { this._observacion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo del Caso
        /// </summary>
        public virtual string TipoCaso
        {
            get { return this._tipoCaso; }
            set { this._tipoCaso = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo de Accion del Caso
        /// </summary>
        public virtual string TipoAccion
        {
            get { return this._tipoAccion; }
            set { this._tipoAccion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Asociado del Caso
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return this._asociado; }
            set { this._asociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado del Caso
        /// </summary>
        public virtual Interesado Interesado
        {
            get { return this._interesado; }
            set { this._interesado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Servicio del Caso
        /// </summary>
        public virtual Servicio Servicio
        {
            get { return this._servicio; }
            set { this._servicio = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Nombre del Interesado del Caso
        /// </summary>
        public virtual string InteresadoCaso
        {
            get { return this._interesadoCaso; }
            set { this._interesadoCaso = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Domicilio del Interesado del Caso
        /// </summary>
        public virtual string DomicilioInteresado
        {
            get { return this._domicilioInteresado; }
            set { this._domicilioInteresado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Rif del Interesado del Caso
        /// </summary>
        public virtual string RifInteresado
        {
            get { return this._rifInteresado; }
            set { this._rifInteresado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la persona Contacto del Interesado del Caso
        /// </summary>
        public virtual string ContactoInteresado
        {
            get { return this._contactoInteresado; }
            set { this._contactoInteresado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Cargo del Interesado del Caso
        /// </summary>
        public virtual string CargoInteresado
        {
            get { return this._cargoInteresado; }
            set { this._cargoInteresado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Telefono del Interesado del Caso
        /// </summary>
        public virtual string TelefonoInteresado
        {
            get { return this._telefonoInteresado; }
            set { this._telefonoInteresado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Fax del Interesado del Caso
        /// </summary>
        public virtual string FaxInteresado
        {
            get { return this._faxInteresado; }
            set { this._faxInteresado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Email del Interesado del Caso
        /// </summary>
        public virtual string EmailInteresado
        {
            get { return this._emailInteresado; }
            set { this._emailInteresado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Comentario acerca del Interesado del Caso
        /// </summary>
        public virtual string ComentarioInteresado
        {
            get { return this._comentarioInteresado; }
            set { this._comentarioInteresado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Nombre del Asociado del Caso
        /// </summary>
        public virtual string AsociadoCaso
        {
            get { return this._asociadoCaso; }
            set { this._asociadoCaso = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Domicilio del Asociado del Caso
        /// </summary>
        public virtual string DomicilioAsociado
        {
            get { return this._domicilioAsociado; }
            set { this._domicilioAsociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Rif del Asociado del Caso
        /// </summary>
        public virtual string RifAsociado
        {
            get { return this._rifAsociado; }
            set { this._rifAsociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la persona Contacto del Asociado del Caso
        /// </summary>
        public virtual string ContactoAsociado
        {
            get { return this._contactoAsociado; }
            set { this._contactoAsociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Cargo del Asociado del Caso
        /// </summary>
        public virtual string CargoAsociado
        {
            get { return this._cargoAsociado; }
            set { this._cargoAsociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Telefono del ASociado del Caso
        /// </summary>
        public virtual string TelefonoAsociado
        {
            get { return this._telefonoAsociado; }
            set { this._telefonoAsociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Fax del Asociado del Caso
        /// </summary>
        public virtual string FaxAsociado
        {
            get { return this._faxAsociado; }
            set { this._faxAsociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Email del Asociado del Caso
        /// </summary>
        public virtual string EmailAsociado
        {
            get { return this._emailAsociado; }
            set { this._emailAsociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Comentario acerca del Asociado del Caso
        /// </summary>
        public virtual string ComentarioAsociado
        {
            get { return this._comentarioAsociado; }
            set { this._comentarioAsociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la operacion de base de datos a realizar con la Entidad
        /// </summary>
        public virtual string Operacion
        {
            get { return this._operacion; }
            set { this._operacion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el listado de Tipos de Caso
        /// </summary>
        public virtual IList<TipoCaso> TiposCaso
        {
            get { return this._tiposCaso; }
            set { this._tiposCaso = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el listado de Acciones de un Caso
        /// </summary>
        public virtual IList<Accion> Acciones
        {
            get { return this._acciones; }
            set { this._acciones = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el listado de Casos Base de un Caso
        /// </summary>
        public virtual IList<CasoBase> CasosBase
        {
            get { return this._casosBase; }
            set { this._casosBase = value; }
        }

        #endregion
    }
}
