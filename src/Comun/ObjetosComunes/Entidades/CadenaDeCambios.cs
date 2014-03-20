using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CadenaDeCambios
    {
        #region Atributos

        private int _id;
        private string _tipoCambio;
        private string _tipoCambioDescripcion;
        private int _codigoOperacion;
        private DateTime? _fechaCadenaCambio;
        private String _observaciones;
        private Carta _carta;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public CadenaDeCambios() { }

        /// <summary>
        /// Constructor que inicializa el id del archivo
        /// </summary>
        /// <param name="id">Id del archivo que corresponde al id de la Marca o de la Patente segun sea el caso</param>
        public CadenaDeCambios(int id)
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
            var t = obj as CadenaDeCambios;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (TipoCambio == (t.TipoCambio)) && (CodigoOperacion == t.CodigoOperacion))
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
        /// Propiedad que asigna el Id de la Cadena de Cambios
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna el Tipo de la Cadena de Cambios (M o P)
        /// </summary>
        public virtual string TipoCambio
        {
            get { return this._tipoCambio; }
            set { this._tipoCambio = value; }
        }


        /// <summary>
        /// Propiedad que asigna la descripcion del Tipo de la Cadena de Cambios 
        /// </summary>
        public virtual string TipoCambioDescripcion
        {
            get { return this._tipoCambioDescripcion; }
            set { this._tipoCambioDescripcion = value; }
        }


        /// <summary>
        /// Propiedad que asigna el Codigo de la Operacion de la Cadena de Cambios 
        /// Esto corresponde al Id de la Marca o de la Patente
        /// </summary>
        public virtual int CodigoOperacion
        {
            get { return this._codigoOperacion; }
            set { this._codigoOperacion = value; }
        }


        /// <summary>
        /// Propiedad que asigna la Fecha de la Cadena de Cambios 
        /// </summary>
        public virtual DateTime? FechaCadenaCambio
        {
            get { return this._fechaCadenaCambio; }
            set { this._fechaCadenaCambio = value; }
        }


        /// <summary>
        /// Propiedad que asigna las Observaciones de la Cadena de Cambios 
        /// </summary>
        public virtual String Observaciones
        {
            get { return this._observaciones; }
            set { this._observaciones = value; }
        }


        /// <summary>
        /// Propiedad que asigna la Carta de la Cadena de Cambios 
        /// </summary>
        public virtual Carta Carta
        {
            get { return this._carta; }
            set { this._carta = value; }
        }


        #endregion
    }
}
