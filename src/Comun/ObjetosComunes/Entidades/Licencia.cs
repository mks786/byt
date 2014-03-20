using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Licencia
    {
        #region Atributos

        private int _id;
        private char _registro;
        private char _otrosC1;
        private char _otrosC2;
        private char _otrosC3;
        private char _documento;
        private char _solvencia;
        private char _timbre;
        private char _poderC;
        private string _certificada;
        private char _acta;
        private char _contrato;
        private string _expediente;
        private string _referencia;
        private string _anexo;
        private string _otrosS1;
        private string _otrosS2;
        private string _otrosS3;
        private string _ubicacion;
        private string _comentario;
        private string _observacion;
        private string _representante;
        private Agente _agenteLicenciatario;
        private Agente _agenteLicenciante;
        private Asociado _asociado;
        private Boletin _boletin;
        private Interesado _interesadoLicenciatario;
        private Interesado _interesadoLicenciante;
        private Poder _poderLicenciatario;
        private Poder _poderLicenciante;
        private Marca _marca;
        private DateTime? _fechaPublicacion;
        private DateTime? _fecha;
        private int? _cadenaDeCambios;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Licencia() { }

        /// <summary>
        /// Constructor que inicializa el Id de la Agente
        /// </summary>
        /// <param name="id">Id de la Agente</param>
        public Licencia(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id de la Licencia
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Registro de la Licencia
        /// </summary>
        public virtual char Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC1 de la Licencia
        /// </summary>
        public virtual char OtrosC1
        {
            get { return _otrosC1; }
            set { _otrosC1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC2 de la Licencia
        /// </summary>
        public virtual char OtrosC2
        {
            get { return _otrosC2; }
            set { _otrosC2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC3 de la Licencia
        /// </summary>
        public virtual char OtrosC3
        {
            get { return _otrosC3; }
            set { _otrosC3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Documento de la Licencia
        /// </summary>
        public virtual char Documento
        {
            get { return _documento; }
            set { _documento = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Solvencia de la Licencia
        /// </summary>
        public virtual char Solvencia
        {
            get { return _solvencia; }
            set { _solvencia = value; }
        }
        
        /// <summary>
        /// Propiedad que asigna u obtiene el campo Poder de la Licencia
        /// </summary>
        public virtual char PoderC
        {
            get { return _poderC; }
            set { _poderC = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Acta de la Licencia
        /// </summary>
        public virtual char Acta
        {
            get { return _acta; }
            set { _acta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Timbre de la Licencia
        /// </summary>
        public virtual char Timbre
        {
            get { return _timbre; }
            set { _timbre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Certificada de la Licencia
        /// </summary>
        public virtual string Certificada
        {
            get { return _certificada; }
            set { _certificada = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Expediente de la Licencia
        /// </summary>
        public virtual string Expediente
        {
            get { return _expediente; }
            set { _expediente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Referencia de la Licencia
        /// </summary>
        public virtual string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Anexo de la Licencia
        /// </summary>
        public virtual string Anexo
        {
            get { return _anexo; }
            set { _anexo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS1 de la Licencia
        /// </summary>
        public virtual string OtrosS1
        {
            get { return _otrosS1; }
            set { _otrosS1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS2 de la Licencia
        /// </summary>
        public virtual string OtrosS2
        {
            get { return _otrosS2; }
            set { _otrosS2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS3 de la Licencia
        /// </summary>
        public virtual string OtrosS3
        {
            get { return _otrosS3; }
            set { _otrosS3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Ubicacion de la Licencia
        /// </summary>
        public virtual string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Comentario de la Licencia
        /// </summary>
        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Observacion de la Licencia
        /// </summary>
        public virtual string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Asociado de la Licencia
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo PoderLicenciante de la Licencia
        /// </summary>
        public virtual Poder PoderLicenciante
        {
            get { return _poderLicenciante; }
            set { _poderLicenciante = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo PoderLicenciatario de la Licencia
        /// </summary>
        public virtual Poder PoderLicenciatario
        {
            get { return _poderLicenciatario; }
            set { _poderLicenciatario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Marca de la Licencia
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value;}
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo InteresadoLicenciatario de la Licencia
        /// </summary>
        public virtual Interesado InteresadoLicenciatario
        {
            get { return _interesadoLicenciatario; }
            set { _interesadoLicenciatario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo InteresadoLicenciante de la Licencia
        /// </summary>
        public virtual Interesado InteresadoLicenciante
        {
            get { return _interesadoLicenciante; }
            set { _interesadoLicenciante = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo BoletinPublicacion de la Licencia
        /// </summary>
        public virtual Boletin Boletin
        {
            get { return _boletin; }
            set { _boletin = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo FechaPublicacion de la Licencia
        /// </summary>
        public virtual DateTime? FechaPublicacion
        {
            get { return _fechaPublicacion; }
            set { _fechaPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo FechaCesion de la Licencia
        /// </summary>
        public virtual DateTime? Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo AgenteCedente de la Licencia
        /// </summary>
        public virtual Agente AgenteLicenciante
        {
            get { return _agenteLicenciante; }
            set { _agenteLicenciante = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo AgenteCedente de la Licencia
        /// </summary>
        public virtual Agente AgenteLicenciatario
        {
            get { return _agenteLicenciatario; }
            set { _agenteLicenciatario = value; }
        }

        public virtual string Representante
        {
            get { return _representante; }
            set { _representante = value; }
        }

        public virtual char Contrato
        {
            get { return _contrato; }
            set { _contrato = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Check de Acta
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
        /// Propiedad que asigna u obtiene la Cadena de la Licencia
        /// </summary>
        public virtual int? CadenaDeCambios
        {
            get { return _cadenaDeCambios; }
            set { _cadenaDeCambios = value; }
        }

        #endregion
    }
}
