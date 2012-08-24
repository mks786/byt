using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class InstruccionDeRenovacion
    {
        #region Atributos

        private int _id;
        private Marca _marca;
        private Carta _correspondencia;
        private DateTime _fecha;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public InstruccionDeRenovacion()
        {
        }

        /// <summary>
        /// Constructor que inicializa el condigo de la infoBol
        /// </summary>
        /// <param name="id">Codigo de la infoBol</param>
        public InstruccionDeRenovacion(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades


        /// <summary>
        /// Propiedad que asigna u obtiene el id de la busqueda
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el IdMarca de la busqueda
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el FechaSolicitudPalabra de la busqueda
        /// </summary>
        public virtual DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }


        #endregion
    }
}
