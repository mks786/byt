using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class DetallePlantilla
    {
        #region Atributos

        private string _nombreArchivo;
        private string _rutaArchivo;
        private Plantilla _plantilla;
        private IList<FiltroPlantilla> _filtrosDetalle;
        private BatPlantilla _batPlantilla;


        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public DetallePlantilla() { }

        public DetallePlantilla(String nombreArchivo, String rutaArchivo)
        {
            this._nombreArchivo = nombreArchivo;
            this._rutaArchivo = rutaArchivo;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Nombre del archivo BAT de la plantilla
        /// </summary>
        public virtual string NombreDetalle
        {
            get { return this._nombreArchivo; }
            set { this._nombreArchivo = value; }
        }


        /// <summary>
        /// Propiedad que asigna o recupera la ruta del archivo BAT de la plantilla
        /// </summary>
        public virtual string RutaDetalle
        {
            get { return this._rutaArchivo; }
            set { this._rutaArchivo = value; }
        }


        /// <summary>
        /// Propiedad que asigna o recupera la plantilla a la que pertenece el detalle seleccionado
        /// </summary>
        public virtual Plantilla Plantilla
        {
            get { return this._plantilla; }
            set { this._plantilla = value; }
        }


        /// <summary>
        /// Propiedad que asigna o recupera las variables filtro de detalle de la plantilla seleccionada
        /// </summary>
        public virtual IList<FiltroPlantilla> VariablesDetalle
        {
            get { return this._filtrosDetalle; }
            set { this._filtrosDetalle = value; }
        }

        /// <summary>
        /// Propiedad que asigna o recupera la variable que apunta al archivo BAT del Detalle
        /// </summary>
        public virtual BatPlantilla BatPlantilla
        {
            get { return this._batPlantilla; }
            set { this._batPlantilla = value; }
        }


        #endregion
    }
}

