using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Conectividad
    {
        #region Atributos

        private String _tabla;
        private String _campo;
        private int _cantidad;
        private Asociado _asociado;
        

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado de la Justificacion
        /// </summary>
        public Conectividad() { }        
        
        ///// <summary>
        ///// Constructor donde se setea la clave primaria
        ///// </summary>
        //public Conectividad(Carta carta, Asociado asociado) 
        //{
        //    this._carta = carta;
        //    this._asociado = asociado;
        //}


        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene la tabla de informacion de la conectividad
        /// </summary>
        public virtual String Tabla
        {
            get { return _tabla; }
            set { _tabla = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo de la conectividad
        /// </summary>
        public virtual String Campo
        {
            get { return _campo; }
            set { _campo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la cantidad de la conectividad
        /// </summary>
        public virtual int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Asociado de la conectividad
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        


        #endregion
    }
}
