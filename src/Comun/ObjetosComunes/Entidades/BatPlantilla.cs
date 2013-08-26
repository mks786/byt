using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class BatPlantilla
    {
        #region Atributos

        private string _nombreArchivo;
        private string _rutaArchivo;
        

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public BatPlantilla() { }

        public BatPlantilla(String nombreArchivo, String rutaArchivo)
        {
            this._nombreArchivo = nombreArchivo;
            this._rutaArchivo = rutaArchivo;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Nombre del archivo BAT de la plantilla
        /// </summary>
        public virtual string NombreBat
        {
            get { return this._nombreArchivo; }
            set { this._nombreArchivo = value; }
        }


        /// <summary>
        /// Propiedad que asigna o recupera la ruta del archivo BAT de la plantilla
        /// </summary>
        public virtual string RutaBat
        {
            get { return this._rutaArchivo; }
            set { this._rutaArchivo = value; }
        }


        



        #endregion
    }
}

