using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class VistaReporte
    {
        #region Atributos

        private int _id;
        private string _nombreVista;
        private string _nombreVistaBD;
                
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public VistaReporte() { }

        /// <summary>
        /// Constructor que inicializa el id del Reporte de Marca
        /// </summary>
        /// <param name="id">Id de la plantilla</param>
        public VistaReporte(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id de la Vista
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el nombre de la Vista
        /// </summary>
        public virtual string NombreVista
        {
            get { return this._nombreVista; }
            set { this._nombreVista = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el nombre de la vista en base de datos
        /// </summary>
        public virtual string NombreVistaBD
        {
            get { return this._nombreVistaBD; }
            set { this._nombreVistaBD = value; }
        }

        #endregion
    }
}
