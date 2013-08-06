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
        //private Marca _marca;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public CorreoOutlook() { }

        #endregion

        #region Propiedades

        /// <summary>
        /// Sobreescritura del Método Equals debido a que la clase tiene id compuesto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /*public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Archivo;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (Aux == (t.Aux)) && (Documento.Id == t.Documento.Id))
                return true;
            return false;

        }*/

        /// <summary>
        /// Sobreescritura del Método GetHashCode debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        /*public override int GetHashCode()
        {
            return base.GetHashCode();
        }*/

        /// <summary>
        /// Sobreescritura del Método ToString debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        /*public override string ToString()
        {
            return base.ToString();
        }*/





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
        /// Propiedad que asigna el TipoDeDocumento del Archivo
        /// </summary>
        public virtual string Subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }



       
            

        #endregion
    }
}
