using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Asignacion
    {
        #region Atributos

        private int _id;
        private string _iniciales;
        private Usuario _responsable;
        private Carta _carta;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Asignacion() { }

        /// <summary>
        /// Constructor que inicializa el Id del Agente
        /// </summary>
        /// <param name="id">Id del Agente</param>
        public Asignacion(int id)
        {
            this._id = id;
        }

        public Asignacion(Usuario usuario, Carta carta)
        {
            this._responsable = usuario;
            this._carta = carta;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Agente
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el responsable de una asignacion
        /// </summary>
        public virtual Usuario Responsable
        {
            get { return _responsable; }
            set { _responsable = value; }
        }

        public virtual Carta Carta
        {
            get { return _carta; }
            set { _carta = value; }
        }

        public virtual string Iniciales
        {
            get { return _iniciales; }
            set { _iniciales = value; }
        }
        #endregion
    }
}
