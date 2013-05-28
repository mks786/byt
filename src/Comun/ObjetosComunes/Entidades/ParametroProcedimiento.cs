using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    /// <summary>
    /// Esta clase esta dedicada para pasar un parametro al DAO para llamar a distintos
    /// procedimientos situados en distintos packages en la Base de Datos
    /// </summary>
    [Serializable]
    public class ParametroProcedimiento
    {
        #region Atributos

        private int _id;
        private Usuario _usuario;
        private int _via;
        private string _paqueteProcedimiento;//Paquete donde esta situado el procedimiento
        private string _nombreProcedimiento;//Nombre del procedimiento a llamar

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
        public ParametroProcedimiento(int id, Usuario usuario, int via, string paqueteProcedimiento ,string nombreProcedimiento)
        {
            this._id = id;
            this._usuario = usuario;
            this._via = via;
            this._paqueteProcedimiento = paqueteProcedimiento;
            this._nombreProcedimiento = nombreProcedimiento;
        }

        public ParametroProcedimiento(string paqueteProcedimiento, string nombreProcedimiento)
        {
            this._paqueteProcedimiento = paqueteProcedimiento;
            this._nombreProcedimiento = nombreProcedimiento;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene la marca del parametro
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
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
        /// Nombre del paquete del procedimiento
        /// </summary>
        public virtual string PaqueteProcedimiento
        {
            get { return _paqueteProcedimiento; }
            set { _paqueteProcedimiento = value; }
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
