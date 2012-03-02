using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Cesion
    {
        #region Atributos

        private int _id;
        private char _registro;
        private char _otrosC1;
        private char _otrosC2;
        private char _otrosC3;
        private char _semejanteno;
        private char _semejantesi;
        private char _cesion;
        private char _poder;
        private char _rif;
        private char _acta;
        private char _timbre;
        private string _certificada;
        private string _expediente;
        private string _referencia;
        private string _anexo;
        private string _otrosS1;
        private string _otrosS2;
        private string _otrosS3;
        private string _ubicacion;
        private string _comentario;
        private string _observacion;
        private DateTime? _fechaPublicacion;
        private DateTime? _fechaCesion;
        private Asociado _asociado;
        private Poder _poderCesionario;
        private Poder _poderCedente;
        private Marca _marca;
        private Interesado _interesadoCedente;
        private Interesado _interesadoCesionario;
        private Boletin _boletinPublicacion;       
        private Agente _agenteCedente;
        private Agente _agenteCesionario;


        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Cesion() { }

        /// <summary>
        /// Constructor que inicializa el Id del Agente
        /// </summary>
        /// <param name="id">Id del Agente</param>
        public Cesion(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id de la cesion
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Registro de la cesion
        /// </summary>
        public virtual char Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC1 de la cesion
        /// </summary>
        public virtual char OtrosC1
        {
            get { return _otrosC1; }
            set { _otrosC1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC2 de la cesion
        /// </summary>
        public virtual char OtrosC2
        {
            get { return _otrosC2; }
            set { _otrosC2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC3 de la cesion
        /// </summary>
        public virtual char OtrosC3
        {
            get { return _otrosC3; }
            set { _otrosC3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Semejanteno de la cesion
        /// </summary>
        public virtual char Semejanteno
        {
            get { return _semejanteno; }
            set { _semejanteno = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Semejantesi de la cesion
        /// </summary>
        public virtual char Semejantesi
        {
            get { return _semejantesi; }
            set { _semejantesi = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Cesion
        /// </summary>
        public virtual char Cesion1
        {
            get { return _cesion; }
            set { _cesion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Poder de la cesion
        /// </summary>
        public virtual char Poder
        {
            get { return _poder; }
            set { _poder = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Rif de la cesion
        /// </summary>
        public virtual char Rif
        {
            get { return _rif; }
            set { _rif = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Acta de la cesion
        /// </summary>
        public virtual char Acta
        {
            get { return _acta; }
            set { _acta = value; }
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

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Timbre de la cesion
        /// </summary>
        public virtual char Timbre
        {
            get { return _timbre; }
            set { _timbre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Certificada de la cesion
        /// </summary>
        public virtual string Certificada
        {
            get { return _certificada; }
            set { _certificada = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo CodigoExpediente de la cesion
        /// </summary>
        public virtual string Expediente
        {
            get { return _expediente; }
            set { _expediente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Referencia de la cesion
        /// </summary>
        public virtual string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Anexo de la cesion
        /// </summary>
        public virtual string Anexo
        {
            get { return _anexo; }
            set { _anexo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS1 de la cesion
        /// </summary>
        public virtual string OtrosS1
        {
            get { return _otrosS1; }
            set { _otrosS1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS2 de la cesion
        /// </summary>
        public virtual string OtrosS2
        {
            get { return _otrosS2; }
            set { _otrosS2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS3 de la cesion
        /// </summary>
        public virtual string OtrosS3
        {
            get { return _otrosS3; }
            set { _otrosS3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Ubicacion de la cesion
        /// </summary>
        public virtual string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Comentario de la cesion
        /// </summary>
        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Observacion de la cesion
        /// </summary>
        public virtual string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Asociado de la cesion
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo PoderCesionario de la cesion
        /// </summary>
        public virtual Poder PoderCesionario
        {
            get { return _poderCesionario; }
            set { _poderCesionario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo PoderCedente de la cesion
        /// </summary>
        public virtual Poder PoderCedente
        {
            get { return _poderCedente; }
            set { _poderCedente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Marca de la cesion
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Cedente de la cesion
        /// </summary>
        public virtual Interesado Cedente
        {
            get { return _interesadoCedente; }
            set { _interesadoCedente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Cesionario de la cesion
        /// </summary>
        public virtual Interesado Cesionario
        {
            get { return _interesadoCesionario; }
            set { _interesadoCesionario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo BoletinPublicacion de la cesion
        /// </summary>
        public virtual Boletin BoletinPublicacion
        {
            get { return _boletinPublicacion; }
            set { _boletinPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo FechaPublicacion de la cesion
        /// </summary>
        public virtual DateTime? FechaPublicacion
        {
            get { return _fechaPublicacion; }
            set { _fechaPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo FechaCesion de la cesion
        /// </summary>
        public virtual DateTime? FechaCesion
        {
            get { return _fechaCesion; }
            set { _fechaCesion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo AgenteCedente de la cesion
        /// </summary>
        public virtual Agente AgenteCedente
        {
            get { return _agenteCedente; }
            set { _agenteCedente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo AgenteCesionario de la cesion
        /// </summary>
        public virtual Agente AgenteCesionario
        {
            get { return _agenteCesionario; }
            set { _agenteCesionario = value; }
        }

        #endregion
    }
}
