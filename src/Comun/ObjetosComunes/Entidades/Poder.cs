using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Poder
    {
        #region Atributos

        private int _id;
        private string _numPoder;
        private DateTime? _fecha;
        private Boletin _boletin;
        private Interesado _interesado;
        private string _facultad;
        private string _anexo;
        private string _observaciones;
        private string _operacion;
        private IList<Agente> _agentes;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Poder() { }

        /// <summary>
        /// Constructor que inicializa el id del Poder
        /// </summary>
        /// <param name="id">Id del Poder</param>
        public Poder(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del poder
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el numero
        /// </summary>
        public virtual string NumPoder
        {
            get { return this._numPoder; }
            set { this._numPoder = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la fecha
        /// </summary>
        public virtual DateTime? Fecha
        {
            get { return this._fecha; }
            set { this._fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el boltin asociado a un poder
        /// </summary>
        public virtual Boletin Boletin
        {
            get { return this._boletin; }
            set { this._boletin = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene interesado en un poder
        /// </summary>
        public virtual Interesado Interesado
        {
            get { return this._interesado; }
            set { this._interesado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la facultadad
        /// </summary>
        public virtual string Facultad
        {
            get { return this._facultad; }
            set { this._facultad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el anexo
        /// </summary>
        public virtual string Anexo
        {
            get { return this._anexo; }
            set { this._anexo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene las Observaciones
        /// </summary>
        public virtual string Observaciones
        {
            get { return this._observaciones; }
            set { this._observaciones = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el tipo de operacion, debe ser CREATE o MODIFY
        /// </summary>
        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene los agentes que poseen el poder
        /// </summary>
        public virtual IList<Agente> Agentes
        {
            get { return this._agentes; }
            set { this._agentes = value; }
        }

        #endregion
    }
}
