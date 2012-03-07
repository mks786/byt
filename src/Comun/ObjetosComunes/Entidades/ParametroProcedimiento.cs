using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class ParametroProcedimiento
    {
        #region Atributos

        private Marca _marca;
        private Usuario _usuario;
        private int _via;
        private string _nombreProcedimiento;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ParametroProcedimiento() { }

        /// <summary>
        /// Constructor que inicializa el Id del Pais
        /// </summary>
        /// <param name="id">Id del Pais</param>
        public ParametroProcedimiento(Marca marca, Usuario usuario, int via, string nombreProcedimiento)
        {
            this._marca = marca;
            this._usuario = usuario;
            this._via = via;
            this._nombreProcedimiento = nombreProcedimiento;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene la marca del parametro
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el usuario del parametro
        /// </summary>
        public virtual Usuario Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la via del procedimiento
        /// </summary>
        public virtual int Via
        {
            get { return _via; }
            set { _via = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el nombre del procedimiento a ejecutar
        /// </summary>
        public virtual string NombreProcedimiento
        {
            get { return _nombreProcedimiento; }
            set { _nombreProcedimiento = value; }
        }

        #endregion
    }
}
