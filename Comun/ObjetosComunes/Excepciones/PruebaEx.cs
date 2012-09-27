using System;
using System.Runtime.Serialization;

namespace Trascend.Bolet.ObjetosComunes.Excepciones
{
    public class PruebaEx : ApplicationException
    {
        #region Atributos
        /// <summary>
        /// Excepción original que genera a su vez una exepción 
        /// específica
        /// </summary>
        private string _mensaje;
        #endregion

        #region Propiedades
        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }
        #endregion

        public PruebaEx()
            : base()
        { }

        public PruebaEx(string msj, string mensaje)
            : base(msj)
        {
            this._mensaje = mensaje;
        }

        public PruebaEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this._mensaje = info.GetString("mensaje");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("mensaje", this._mensaje);
        }
    }
}


