using System;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Resolucion
    {
        #region Atributos

        private string _id;
        private DateTime _fechaResolucion;
        private Boletin _boletin;
        private string _volumen;
        private string _pagina;

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna el id de la auditoria
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna de la fecha del resolucion
        /// </summary>
        public virtual DateTime FechaResolucion
        {
            get { return this._fechaResolucion; }
            set { this._fechaResolucion = value; }
        }

        /// <summary>
        /// Propiedad que asigna el boletin de la resolución
        /// </summary>
        public virtual Boletin Boletin
        {
            get { return this._boletin; }
            set { this._boletin = value; }
        }

        /// <summary>
        /// Propiedad que asigna el volumen donde está ubicado la resolución
        /// </summary>
        public virtual string Volumen
        {
            get { return this._volumen; }
            set { this._volumen = value; }
        }

        /// <summary>
        /// Propiedad que asigna la pagina donde está ubicado la resolución
        /// </summary>
        public virtual string Pagina
        {
            get { return this._pagina; }
            set { this._pagina = value; }
        }

        #endregion
    }
}
