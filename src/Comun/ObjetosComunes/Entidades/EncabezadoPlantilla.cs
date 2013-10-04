using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class EncabezadoPlantilla
    {
        #region Atributos

        private string _nombreArchivo;
        private string _rutaArchivo;
        private MaestroDePlantilla _maestroDePlantilla;
        private IList<FiltroPlantilla> _filtrosEncabezado;
        private BatPlantilla _batPlantilla;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public EncabezadoPlantilla() { }

        public EncabezadoPlantilla(String nombreArchivo, String rutaArchivo)
        {
            this._nombreArchivo = nombreArchivo;
            this._rutaArchivo = rutaArchivo;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna el Nombre del archivo de Encabezado
        /// </summary>
        public virtual string NombreEncabezado
        {
            get { return this._nombreArchivo; }
            set { this._nombreArchivo = value; }
        }


        /// <summary>
        /// Propiedad que asigna o recuperar la ruta del archivo de Encabezado
        /// </summary>
        public virtual string RutaEncabezado
        {
            get { return this._rutaArchivo; }
            set { this._rutaArchivo = value; }
        }


        /// <summary>
        /// Propiedad que asigna o recupera el maestro de plantilla al que pertenece el encabezado seleccionado
        /// </summary>
        public virtual MaestroDePlantilla MaestroDePlantilla
        {
            get { return this._maestroDePlantilla; }
            set { this._maestroDePlantilla = value; }
        }


        /// <summary>
        /// Propiedad que asigna o recupera las variables en encabezado de la plantilla seleccionada
        /// </summary>
        public virtual IList<FiltroPlantilla> VariablesEncabezado
        {
            get { return this._filtrosEncabezado; }
            set { this._filtrosEncabezado= value; }
        }


        /// <summary>
        /// Propiedad que asigna o recupera la variable que apunta al archivo BAT del Encabezado
        /// </summary>
        public virtual BatPlantilla BatPlantilla
        {
            get { return this._batPlantilla; }
            set { this._batPlantilla = value; }
        }

        #endregion
    }
}
