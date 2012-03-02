using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CambioDeNombre
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
        private char _certificada;
        private char _acta;
        private string _expediente;
        private string _referencia;
        private string _anexo;
        private string _otrosS1;
        private string _otrosS2;
        private string _otrosS3;
        private string _ubicacion;
        private string _comentario;
        private string _observacion;
        private Agente _agente;
        private Asociado _asociado;
        private Boletin _boletin;
        private Interesado _interesadoActual;
        private Interesado _interesadoAnterior;
        private Poder _poder;
        private Marca _marca;
        private DateTime? _fechaPublicacion;
        private DateTime? _fecha;


        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public CambioDeNombre()
        {          
        }

        /// <summary>
        /// Constructor que inicializa el Id del Agente
        /// </summary>
        /// <param name="id">Id del Agente</param>
        public CambioDeNombre(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del cambio de nombre
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Registro del cambio de nombre
        /// </summary>
        public virtual char Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC1 del cambio de nombre
        /// </summary>
        public virtual char OtrosC1
        {
            get { return _otrosC1; }
            set { _otrosC1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC2 del cambio de nombre
        /// </summary>
        public virtual char OtrosC2
        {
            get { return _otrosC2; }
            set { _otrosC2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosC3 del cambio de nombre
        /// </summary>
        public virtual char OtrosC3
        {
            get { return _otrosC3; }
            set { _otrosC3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Documento del cambio de nombre
        /// </summary>
        public virtual char Documento
        {
            get { return _documento; }
            set { _documento = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Solvencia del cambio de nombre
        /// </summary>
        public virtual char Solvencia
        {
            get { return _solvencia; }
            set { _solvencia = value; }
        }
        
        /// <summary>
        /// Propiedad que asigna u obtiene el campo Poder del cambio de nombre
        /// </summary>
        public virtual char PoderC
        {
            get { return _poderC; }
            set { _poderC = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Acta del cambio de nombre
        /// </summary>
        public virtual char Acta
        {
            get { return _acta; }
            set { _acta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Timbre del cambio de nombre
        /// </summary>
        public virtual char Timbre
        {
            get { return _timbre; }
            set { _timbre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Certificada del cambio de nombre
        /// </summary>
        public virtual char Certificada
        {
            get { return _certificada; }
            set { _certificada = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Expediente del cambio de nombre
        /// </summary>
        public virtual string Expediente
        {
            get { return _expediente; }
            set { _expediente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Referencia del cambio de nombre
        /// </summary>
        public virtual string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Anexo del cambio de nombre
        /// </summary>
        public virtual string Anexo
        {
            get { return _anexo; }
            set { _anexo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS1 del cambio de nombre
        /// </summary>
        public virtual string OtrosS1
        {
            get { return _otrosS1; }
            set { _otrosS1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS2 del cambio de nombre
        /// </summary>
        public virtual string OtrosS2
        {
            get { return _otrosS2; }
            set { _otrosS2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo OtrosS3 del cambio de nombre
        /// </summary>
        public virtual string OtrosS3
        {
            get { return _otrosS3; }
            set { _otrosS3 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Ubicacion del cambio de nombre
        /// </summary>
        public virtual string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Comentario del cambio de nombre
        /// </summary>
        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Observacion del cambio de nombre
        /// </summary>
        public virtual string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Asociado del cambio de nombre
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Poder del cambio de nombre
        /// </summary>
        public virtual Poder Poder
        {
            get { return _poder; }
            set { _poder = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo Marca del cambio de nombre
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo InteresadoActual del cambio de nombre
        /// </summary>
        public virtual Interesado InteresadoActual
        {
            get { return _interesadoActual; }
            set { _interesadoActual = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo InteresadoAnterior del cambio de nombre
        /// </summary>
        public virtual Interesado InteresadoAnterior
        {
            get { return _interesadoAnterior; }
            set { _interesadoAnterior = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo BoletinPublicacion del cambio de nombre
        /// </summary>
        public virtual Boletin Boletin
        {
            get { return _boletin; }
            set { _boletin = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo FechaPublicacion del cambio de nombre
        /// </summary>
        public virtual DateTime? FechaPublicacion
        {
            get { return _fechaPublicacion; }
            set { _fechaPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo FechaCesion del cambio de nombre
        /// </summary>
        public virtual DateTime? Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo AgenteCedente del cambio de nombre
        /// </summary>
        public virtual Agente Agente
        {
            get { return _agente; }
            set { _agente = value; }
        }
       
        #endregion
    }
}
