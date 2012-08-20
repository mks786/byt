using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CambioDeDomicilioPatente
    {
        #region Atributos

        private int _id;
        private DateTime? _fechaDomicilio;
        private DateTime? _fechaPublicacion;
        private char _otrosC1;
        private char _otrosC2;
        private char _cambio;
        private char _timbre;
        private char _poderC;
        private char _solvencia;
        private char _acta;
        private string _certificada;
        private string _expediente;
        private string _observacion;
        private string _anexo;
        private string _otrosS1;
        private string _otrosS2;
        private string _comentario;
        private string _referencia;
        private string _ubicacion;
        private Boletin _boletinPublicacion;
        private Interesado _interesadoActual;
        private Interesado _interesadoAnterior;
        private Poder _poder;
        private Patente _patente;
        private Agente _agente;
        private Asociado _asociado;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public CambioDeDomicilioPatente() { }

        /// <summary>
        /// Constructor que inicializa el Id del Agente
        /// </summary>
        /// <param name="id">Id del Agente</param>
        public CambioDeDomicilioPatente(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del CambioDeDomicilio
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene fechaDomicilio de CambioDeDomicilio
        /// </summary>
        public virtual DateTime? FechaDomicilio
        {
            get { return _fechaDomicilio; }
            set { _fechaDomicilio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene fechaPublicacion del CambioDeDomicilio
        /// </summary>
        public virtual DateTime? FechaPublicacion
        {
            get { return _fechaPublicacion; }
            set { _fechaPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene OtrosC1 del CambioDeDomicilio
        /// </summary>
        public virtual char OtrosC1
        {
            get { return _otrosC1; }
            set { _otrosC1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene OtrosC2 del CambioDeDomicilio
        /// </summary>
        public virtual char OtrosC2
        {
            get { return _otrosC2; }
            set { _otrosC2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Cambio del CambioDeDomicilio
        /// </summary>
        public virtual char Cambio
        {
            get { return _cambio; }
            set { _cambio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Timbre del CambioDeDomicilio
        /// </summary>
        public virtual char Timbre
        {
            get { return _timbre; }
            set { _timbre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Poder del CambioDeDomicilio
        /// </summary>
        public virtual char PoderC
        {
            get { return _poderC; }
            set { _poderC = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Solvencia del CambioDeDomicilio
        /// </summary>
        public virtual char Solvencia
        {
            get { return _solvencia; }
            set { _solvencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Acta del CambioDeDomicilio
        /// </summary>
        public virtual char Acta
        {
            get { return _acta; }
            set { _acta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Certificada del CambioDeDomicilio
        /// </summary>
        public virtual string Certificada
        {
            get { return _certificada; }
            set { _certificada = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Expediente del CambioDeDomicilio
        /// </summary>
        public virtual string Expediente
        {
            get { return _expediente; }
            set { _expediente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Observacion del CambioDeDomicilio
        /// </summary>
        public virtual string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Anexo del CambioDeDomicilio
        /// </summary>
        public virtual string Anexo
        {
            get { return _anexo; }
            set { _anexo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene OtrosS1 del CambioDeDomicilio
        /// </summary>
        public virtual string OtrosS1
        {
            get { return _otrosS1; }
            set { _otrosS1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene OtrosS2 del CambioDeDomicilio
        /// </summary>
        public virtual string OtrosS2
        {
            get { return _otrosS2; }
            set { _otrosS2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Comentario del CambioDeDomicilio
        /// </summary>
        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Referencia del CambioDeDomicilio
        /// </summary>
        public virtual string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Ubicacion del CambioDeDomicilio
        /// </summary>
        public virtual string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Agente del CambioDeDomicilio
        /// </summary>
        public virtual Agente Agente
        {
            get { return _agente; }
            set { _agente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Asociado del CambioDeDomicilio
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene BoletinPublicacion del CambioDeDomicilio
        /// </summary>
        public virtual Boletin BoletinPublicacion
        {
            get { return _boletinPublicacion; }
            set { _boletinPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene InteresadoActual del CambioDeDomicilio
        /// </summary>
        public virtual Interesado InteresadoActual
        {
            get { return _interesadoActual; }
            set { _interesadoActual = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene InteresadoAnterior del CambioDeDomicilio
        /// </summary>
        public virtual Interesado InteresadoAnterior
        {
            get { return _interesadoAnterior; }
            set { _interesadoAnterior = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Poder del CambioDeDomicilio
        /// </summary>
        public virtual Poder Poder
        {
            get { return _poder; }
            set { _poder = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Patente del CambioDeDomicilio
        /// </summary>
        public virtual Patente Patente
        {
            get { return _patente; }
            set { _patente = value; }
        }

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
  
        #endregion
    }
}
