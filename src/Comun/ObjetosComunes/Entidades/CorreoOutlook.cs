using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CorreoOutlook
    {
        #region Atributos

        private DateTime _fecha;
        private string _remite;
        private string _subject;
        private string _body;
        private string _cc;
        private string _destino;
       
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public CorreoOutlook() { }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna el Expediente del Archivo
        /// </summary>
        public virtual DateTime Fecha
        {
            get { return this._fecha; }
            set { this._fecha = value; }
        }


        /// <summary>
        /// Propiedad que asigna el Aux del Archivo
        /// </summary>
        public virtual string Remite
        {
            get { return this._remite; }
            set { this._remite = value; }
        }


        /// <summary>
        /// Propiedad que asigna o recuperar el Destinatario del CorreoOutlook
        /// </summary>
        public virtual string Destino
        {
            get { return this._destino; }
            set { this._destino = value; }
        }
        

        /// <summary>
        /// Propiedad que asigna o recuperar el Destinatario Con Copia del CorreoOutlook
        /// </summary>
        public virtual string ConCopiaA
        {
            get { return this._cc; }
            set { this._cc = value; }
        }
        

        /// <summary>
        /// Propiedad que asigna el TipoDeDocumento del Archivo
        /// </summary>
        public virtual string Subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }


        /// <summary>
        /// Propiedad que asigna o recuperar el Cuerpo del CorreoOutlook
        /// </summary>
        public virtual string Body
        {
            get { return this._body; }
            set { this._body = value; }
        }
  

        #endregion
    }
}
