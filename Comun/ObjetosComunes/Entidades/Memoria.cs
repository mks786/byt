using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Memoria
    {
        #region Atributos

        private int _id;
        private Patente _patente;
        private DateTime _fecha;
        private string _ruta;
        private char? _tipoDocumento;
        private int? _tipoMensaje;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Memoria() { }

        /// <summary>
        /// Constructor que inicializa el Id del Memoria
        /// </summary>
        /// <param name="id">Id del Memoria</param>
        public Memoria(int id)
        {
            this._id = id;
        }

        #endregion

        #region Override


        /// <summary>
        /// Sobreescritura del Método Equals debido a que la clase tiene id compuesto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as Memoria;

            if (t == null)
                return false;
            if ((Id == (t.Id)) && (Patente.Id == (t.Patente.Id)))
                return true;
            return false;

        }

        /// <summary>
        /// Sobreescritura del Método GetHashCode debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Sobreescritura del Método ToString debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }


        #endregion


        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id de una memoria
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Patente de una memoria
        /// </summary>
        public virtual Patente Patente
        {
            get { return _patente; }
            set { _patente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de una memoria
        /// </summary>
        public virtual DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Ruta de una memoria
        /// </summary>
        public virtual string Ruta
        {
            get { return _ruta; }
            set { _ruta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el TipoDocumento de una memoria
        /// </summary>
        public virtual char? TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }

        public virtual int? TipoMensaje
        {
            get { return _tipoMensaje; }
            set { _tipoMensaje = value; }
        }
        #endregion
    }
}
