using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Renovacion
    {
        #region Atributos

        private int _id;
        private char _rif;
        private char _acta;
        private char _timbre;
        private char _registro;
        private char _otrosC1;
        private char _otrosC2;
        private char _otrosC3;
        private char _otrosC4;
        private char _otrosC5;
        private char _poderC;
        private char _tipoR;
        private string _ubicacion;
        private string _otrosS1;
        private string _otrosS2;
        private string _otrosS3;
        private string _otrosS4;
        private string _otrosS5;
        private string _observacion;
        private string _expediente;
        private DateTime? _fecha;
        private DateTime? _fechaUltima;
        private DateTime? _fechaProxima;
        private Agente _agente;
        private Asociado _asociado;
        private Boletin _boletinPublicacion;
        private Interesado _interesado;
        private Poder _poder;
        private Marca _marca;
        private ListaDatosValores _tipoRenovacion;
        private bool _nueva;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Renovacion() { }

        /// <summary>
        /// Constructor que inicializa el Id de la Renovación
        /// </summary>
        /// <param name="id">Id de la Renovación</param>
        public Renovacion(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id de la Renovacion
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Registro de la Renovacion
        /// </summary>
        public virtual char Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC1 de la Renovacion
        /// </summary>
        public virtual char OtrosC1
        {
            get { return _otrosC1; }
            set { _otrosC1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC2 de la Renovacion
        /// </summary>
        public virtual char OtrosC2
        {
            get { return _otrosC2; }
            set { _otrosC2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC3 de la Renovacion
        /// </summary>
        public virtual char OtrosC3
        {
            get { return _otrosC3; }
            set { _otrosC3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC4 de la Renovacion
        /// </summary>
        public virtual char OtrosC4
        {
            get { return _otrosC4; }
            set { _otrosC4 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC5 de la Renovacion
        /// </summary>
        public virtual char OtrosC5
        {
            get { return _otrosC5; }
            set { _otrosC5 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Poder de la Renovacion
        /// </summary>
        public virtual char PoderC
        {
            get { return _poderC; }
            set { _poderC = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Rif de la Renovacion
        /// </summary>
        public virtual char Rif
        {
            get { return _rif; }
            set { _rif = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Acta de la Renovacion
        /// </summary>
        public virtual char Acta
        {
            get { return _acta; }
            set { _acta = value; }
        }

        /// <summary>
        /// Propiedad que gestiona el valor del campo Acta de la renovación
        /// </summary>
        public virtual bool BActa
        {
            get
            {
                if (this.Acta.ToString().ToUpper().Equals("T"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Acta = 'T';
                else
                    this.Acta = 'F';
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Timbre de la Renovacion
        /// </summary>
        public virtual char Timbre
        {
            get { return _timbre; }
            set { _timbre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo tipoR de la Renovacion
        /// </summary>
        public virtual char TipoR
        {
            get { return _tipoR; }
            set { _tipoR = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Expediente de la Renovacion
        /// </summary>
        public virtual string Expediente
        {
            get { return _expediente; }
            set { _expediente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS1 de la Renovacion
        /// </summary>
        public virtual string OtrosS1
        {
            get { return _otrosS1; }
            set { _otrosS1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS2 de la Renovacion
        /// </summary>
        public virtual string OtrosS2
        {
            get { return _otrosS2; }
            set { _otrosS2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS3 de la Renovacion
        /// </summary>
        public virtual string OtrosS3
        {
            get { return _otrosS3; }
            set { _otrosS3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS4 de la Renovacion
        /// </summary>
        public virtual string OtrosS4
        {
            get { return _otrosS4; }
            set { _otrosS4 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS5 de la Renovacion
        /// </summary>
        public virtual string OtrosS5
        {
            get { return _otrosS5; }
            set { _otrosS5 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Ubicacion de la Renovacion
        /// </summary>
        public virtual string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Observacion de la Renovacion
        /// </summary>
        public virtual string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Asociado de la Renovacion
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo PoderCesionario de la Renovacion
        /// </summary>
        public virtual Poder Poder
        {
            get { return _poder; }
            set { _poder = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Marca de la Renovacion
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Cedente de la Renovacion
        /// </summary>
        public virtual Interesado Interesado
        {
            get { return _interesado; }
            set { _interesado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo BoletinPublicacion de la Renovacion
        /// </summary>
        public virtual Boletin BoletinPublicacion
        {
            get { return _boletinPublicacion; }
            set { _boletinPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo FechaRenovacion de la Renovacion
        /// </summary>
        public virtual DateTime? Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo FechaRenovacionProxima de la Renovacion
        /// </summary>
        public virtual DateTime? FechaProxima
        {
            get { return _fechaProxima; }
            set { _fechaProxima = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo FechaRenovacionUltima de la Renovacion
        /// </summary>
        public virtual DateTime? FechaUltima
        {
            get { return _fechaUltima; }
            set { _fechaUltima = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo AgenteCedente de la Renovacion
        /// </summary>
        public virtual Agente Agente
        {
            get { return _agente; }
            set { _agente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo AgenteCedente de la Renovacion
        /// </summary>
        public virtual ListaDatosValores TipoRenovacion
        {
            get { return _tipoRenovacion; }
            set { _tipoRenovacion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la propiedad NUEVA que se usara para mostrar el boton de NUEVA RENOVACION despues de haber
        /// creado previamente una renovacion
        /// </summary>
        public virtual bool Nueva
        {
            get { return _nueva; }
            set { _nueva = value; }
        }

        #endregion
    }
}
