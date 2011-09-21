using System;

namespace Trascend.Bolet.ObjetosComunes.Excepciones
{
    public abstract class ExcepcionBase : Exception
    {
        #region Atributos
        /// <summary>
        /// Mensaje que indica el error que generó la excepción
        /// </summary>
        private string _mensaje;

        /// <summary>
        /// Excepción original que genera a su vez una exepción 
        /// específica
        /// </summary>
        private Exception _excepcionInterna;
        #endregion

        #region Propiedades
        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        public Exception ExcepcionInterna
        {
            get { return _excepcionInterna; }
            set { _excepcionInterna = value; }
        }
        #endregion

        public ExcepcionBase()
        { }

        public ExcepcionBase(string mensaje, Exception excepcionInterna)
            : base(mensaje, excepcionInterna)
        { }
    }
}