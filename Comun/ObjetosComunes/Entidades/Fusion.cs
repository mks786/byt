using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Fusion
    {
        #region Atributos

        private int _id;
        private string _expediente;
        private string _ubicacion;
        private string _anexo;
        private string _comentario;
        private string _referencia;
        private string _observacion;
        private string _certificada;
        private char _acta;
        private DateTime? _fechaPublicacion;
        private DateTime? _fecha;
        private Boletin _boletin;
        private Poder _poder;
        private Agente _agente;
        private Asociado _asociado;
        private Marca _marca;
        private Interesado _interesadoEntre;
        private Interesado _interesadoSobreviviente;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Fusion() { }

        /// <summary>
        /// Constructor que inicializa el Id de la Fusion
        /// </summary>
        /// <param name="id">Id de la Fusion</param>
        public Fusion(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id de la Fusion
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el CodigoExpediente de la Fusion
        /// </summary>
        public virtual string Expediente
        {
            get { return _expediente; }
            set { _expediente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Ubicacion de la Fusion
        /// </summary>
        public virtual string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Anexo de la Fusion
        /// </summary>
        public virtual string Anexo
        {
            get { return _anexo; }
            set { _anexo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Comentario de la Fusion
        /// </summary>
        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Referencia de la Fusion
        /// </summary>
        public virtual string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Observacion de la Fusion
        /// </summary>
        public virtual string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Certificada de la Fusion
        /// </summary>
        public virtual string Certificada
        {
            get { return _certificada; }
            set { _certificada = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Acta de la Fusion
        /// </summary>
        public virtual char Acta
        {
            get { return _acta; }
            set { _acta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la FechaPublicacion de la Fusion
        /// </summary>
        public virtual DateTime? FechaPublicacion
        {
            get { return _fechaPublicacion; }
            set { _fechaPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Boletin de la Fusion
        /// </summary>
        public virtual Boletin Boletin
        {
            get { return _boletin; }
            set { _boletin = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Poder de la Fusion
        /// </summary>
        public virtual Poder Poder
        {
            get { return _poder; }
            set { _poder = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Agente de la Fusion
        /// </summary>
        public virtual Agente Agente
        {
            get { return _agente; }
            set { _agente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Asociado de la Fusion
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de la Fusion
        /// </summary>
        public virtual DateTime? Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Marca de la Fusion
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el InteresadoEntre de la Fusion
        /// </summary>
        public virtual Interesado InteresadoEntre
        {
            get { return _interesadoEntre; }
            set { _interesadoEntre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el InteresadoSobreviviente de la Fusion
        /// </summary>
        public virtual Interesado InteresadoSobreviviente
        {
            get { return _interesadoSobreviviente; }
            set { _interesadoSobreviviente = value; }
        }

        #endregion
    }
}
