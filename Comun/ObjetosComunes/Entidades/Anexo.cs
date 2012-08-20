using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Anexo
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private IList<Carta> _cartas;
        private IList<Carta> _cartasConfirmadas;
                
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Anexo() { }

        /// <summary>
        /// Constructor que inicializa el Id del Anexo
        /// </summary>
        /// <param name="id">Id del Anexo</param>
        public Anexo(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Anexo
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Anexo
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene las cartas de un anexo
        /// </summary>
        public virtual IList<Carta> Cartas
        {
            get { return _cartas; }
            set { _cartas = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene las cartas confirmadas de un anexo
        /// </summary>
        public virtual IList<Carta> CartasConfirmadas
        {
            get { return _cartasConfirmadas; }
            set { _cartasConfirmadas = value; }
        }

        #endregion
    }
}
