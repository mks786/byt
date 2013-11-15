using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FiltroDataCrudaCobranza
    {
        #region Atributos
        
        private string _moneda;
        private string _usuario;
        private string _medioGestion;
        private Asociado _asociado;
        private string _ordenamiento;
        private string _ejeX;
        private string _ejeY;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public FiltroDataCrudaCobranza() { }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el filtro MONEDA 
        /// </summary>
        public virtual String Moneda
        {
            get { return this._moneda; }
            set { this._moneda = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el filtro USUARIO 
        /// </summary>
        public virtual String Usuario
        {
            get { return this._usuario; }
            set { this._usuario = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el filtro MEDIO  
        /// </summary>
        public virtual String MedioGestion
        {
            get { return this._medioGestion; }
            set { this._medioGestion = value; }
        }

        
        /// <summary>
        /// Propiedad que asigna u obtiene el tipo de Ordenamiento para el filtro a utilizar en la consulta para obtener la Data Cruda 
        /// </summary>
        public virtual String Ordenamiento
        {
            get { return this._ordenamiento; }
            set { this._ordenamiento = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Asociado para el filtro a utilizar en la consulta para obtener la Data Cruda 
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return this._asociado; }
            set { this._asociado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Eje X a usar para obtener el detalle 
        /// </summary>
        public virtual String EjeX
        {
            get { return this._ejeX; }
            set { this._ejeX = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Eje Y a usar para obtener el detalle
        /// </summary>
        public virtual String EjeY
        {
            get { return this._ejeY; }
            set { this._ejeY = value; }
        }


        #endregion
    }
}
