using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class ContactoCxP
    {
        #region Atributos

        private int _id;
        private Asociado _asociado;
        private String _frecuenciaPago;
        private String _modoPago;
        private String _observacion;
        private Contacto _contactoAsociado;

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ContactoCxP() { }

        /// <summary>
        /// Constructor que inicializa el Id del ContactoCxP
        /// </summary>
        /// <param name="id">Id del Contacto del Asociado que sera tambien ContactoCxP</param>
        public ContactoCxP(int id)
        {
            this._id = id;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Sobreescritura del Método Equals debido a que la clase tiene id compuesto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ContactoCxP;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (Asociado.Id == (t.Asociado.Id)))
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

        /// <summary>
        /// Propiedad que asigna y obtiene el asociado dueño del contacto cxp
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el Id del contacto cxp
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene la frecuencia de pago para el contacto cxp
        /// </summary>
        public virtual String FrecuenciaPago
        {
            get { return _frecuenciaPago; }
            set { _frecuenciaPago = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene el modo de pago para el contacto cxp
        /// </summary>
        public virtual String ModoPago
        {
            get { return _modoPago; }
            set { _modoPago = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene un campo de observaciones para el contacto cxp
        /// </summary>
        public virtual String Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene el contacto del asociado que es tambien contacto cxp
        /// </summary>
        public virtual Contacto ContactoAsociado
        {
            get { return _contactoAsociado; }
            set { _contactoAsociado = value; }
        }


        #endregion
    }
}
