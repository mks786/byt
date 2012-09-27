using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class InstruccionDeRenovacion
    {
        #region Atributos

        private Marca _marca;
        private Carta _carta;
        private DateTime _fecha;

        #endregion

        #region Override

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            bool retorno = false;
            try
            {

                if ((((InstruccionDeRenovacion)obj)._carta.Id == this._carta.Id) && (((InstruccionDeRenovacion)obj)._marca.Id == this._marca.Id))
                {
                    retorno = true;
                }

            }
            catch(Exception ex)
            {
                string a = ex.Message;
            }
            return retorno;
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public InstruccionDeRenovacion()
        {
        }

        #endregion

        #region Propiedades


        /// <summary>
        /// Propiedad que asigna u obtiene el IdMarca de la busqueda
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Fecha de la Instruccion de renovacion
        /// </summary>
        public virtual DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Carta de la Instruccion de renovacion
        /// </summary>
        public virtual Carta Carta
        {
            get { return _carta; }
            set { _carta = value; }
        }


        #endregion
    }
}
