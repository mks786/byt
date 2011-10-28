using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Resumen
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private char _seg;
        private int _dias;
        private IList<Carta> _cartas;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Resumen()
        {
        }

        /// <summary>
        /// Constructor que inicializa el condigo del Resumen
        /// </summary>
        /// <param name="codigo">Codigo del asociado</param>
        public Resumen(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el código del Resumen
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion del Resumen
        /// </summary>
        public virtual string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el ISeg del Resumen
        /// </summary>
        public virtual char Seg
        {
            get { return _seg; }
            set { _seg = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de autorizar el Usuario
        /// </summary>
        public virtual bool BSeg
        {
            get
            {
                if (this.Seg != null && this.Seg.Equals('T'))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Seg = 'T';
                else
                    this.Seg = 'F';
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene los dias del Resumen
        /// </summary>
        public virtual int Dias
        {
            get { return _dias; }
            set { _dias = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene las cartas pertenecientes
        /// a un resumen
        /// </summary>
        public virtual IList<Carta> Cartas
        {
            get { return _cartas; }
            set { _cartas = value; }
        }
        #endregion
    }
}
