using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CasoBase
    {
        #region Atributos

        private string _id;
        private Caso _caso;
        private string _tipoBase;
        private char? _interno;
        private string _descripcion;
        private string _tipoCodigoBase;
        private Internacional _internacional;
        private Nacional _nacional;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public CasoBase() { }

        /// <summary>
        /// Constructor que inicializa el id del Caso Base
        /// </summary>
        /// <param name="id">Id del Caso Base</param>
        public CasoBase(string id)
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
            var t = obj as CasoBase;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (Caso.Id == t.Caso.Id))
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
        /// Propiedad que asigna u obtiene el Id del Codigo del Caso Base
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Caso relacionado con el Caso Base
        /// </summary>
        public virtual Caso Caso
        {
            get { return this._caso; }
            set { this._caso = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo de Caso Base
        /// </summary>
        public virtual string TipoBase
        {
            get { return this._tipoBase; }
            set { this._tipoBase = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la bandera para saber si el Caso Base es interno o no
        /// </summary>
        public virtual char? Interno
        {
            get { return this._interno; }
            set { this._interno = value; }
        }


        public virtual bool BInterno
        {
            get
            {
                if (this.Interno != null)
                {
                    if (this.Interno.Equals('T'))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Interno = 'T';
                else
                    this.Interno = 'F';
            }
            
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Descripcion del Caso Base
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo del Caso Base
        /// </summary>
        public virtual string TipoCodigoBase
        {
            get { return this._tipoCodigoBase; }
            set { this._tipoCodigoBase = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el codigo Internacional del Caso Base
        /// </summary>
        public virtual Internacional Internacional
        {
            get { return this._internacional; }
            set { this._internacional = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el codigo Nacional del Caso Base
        /// </summary>
        public virtual Nacional Nacional
        {
            get { return this._nacional; }
            set { this._nacional = value; }
        }


        #endregion



    }
}
