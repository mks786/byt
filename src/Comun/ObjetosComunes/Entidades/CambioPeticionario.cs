using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CambioPeticionario
    {
        #region Atributos

        private int _id;
        private DateTime? _fechaPeticionario;
        private DateTime? _fechaPublicacion;
        private char _acta;
        private string _certificada;
        private string _expediente;
        private string _referencia;
        private string _comentario;
        private string _anexo;
        private string _ubicacion;
        private string _observacion;
        private Poder _poderActual;
        private Poder _poderAnterior;
        private Interesado _interesadoActual;
        private Interesado _interesadoAnterior;
        private Boletin _boletinPublicacion;
        private Asociado _asociado;
        private Agente _agenteActual;
        private Agente _agenteAnterior;
        private Marca _marca;                                      

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public CambioPeticionario() { }

        /// <summary>
        /// Constructor que inicializa el Id del Agente
        /// </summary>
        /// <param name="id">Id del Agente</param>
        public CambioPeticionario(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del CambioPeticionario
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene FechaPeticionario del CambioPeticionario
        /// </summary>
        public virtual DateTime? FechaPeticionario
        {
            get { return _fechaPeticionario; }
            set { _fechaPeticionario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene FechaPublicacion del CambioPeticionario
        /// </summary>
        public virtual DateTime? FechaPublicacion
        {
            get { return _fechaPublicacion; }
            set { _fechaPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Acta del CambioPeticionario
        /// </summary>
        public virtual char Acta
        {
            get { return _acta; }
            set { _acta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Certificada del CambioPeticionario
        /// </summary>
        public virtual string Certificada
        {
            get { return _certificada; }
            set { _certificada = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Expedientedel CambioPeticionario
        /// </summary>
        public virtual string Expediente
        {
            get { return _expediente; }
            set { _expediente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Referencia del CambioPeticionario
        /// </summary>
        public virtual string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Comentario del CambioPeticionario
        /// </summary>
        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Anexo del CambioPeticionario
        /// </summary>
        public virtual string Anexo
        {
            get { return _anexo; }
            set { _anexo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Ubicacion del CambioPeticionario
        /// </summary>
        public virtual string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Observacion del CambioPeticionario
        /// </summary>
        public virtual string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene PoderActual del CambioPeticionario
        /// </summary>
        public virtual Poder PoderActual
        {
            get { return _poderActual; }
            set { _poderActual = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene PoderAnterior del CambioPeticionario
        /// </summary>
        public virtual Poder PoderAnterior
        {
            get { return _poderAnterior; }
            set { _poderAnterior = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene InteresadoActual del CambioPeticionario
        /// </summary>
        public virtual Interesado InteresadoActual
        {
            get { return _interesadoActual; }
            set { _interesadoActual = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene InteresadoAnterior del CambioPeticionario
        /// </summary>
        public virtual Interesado InteresadoAnterior
        {
            get { return _interesadoAnterior; }
            set { _interesadoAnterior = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene BoletinPublicacion del CambioPeticionario
        /// </summary>
        public virtual Boletin BoletinPublicacion
        {
            get { return _boletinPublicacion; }
            set { _boletinPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Asociado del CambioPeticionario
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene AgenteActual del CambioPeticionario
        /// </summary>
        public virtual Agente AgenteActual
        {
            get { return _agenteActual; }
            set { _agenteActual = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene AgenteAnterior del CambioPeticionario
        /// </summary>
        public virtual Agente AgenteAnterior
        {
            get { return _agenteAnterior; }
            set { _agenteAnterior = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene Marca del CambioPeticionario
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
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
