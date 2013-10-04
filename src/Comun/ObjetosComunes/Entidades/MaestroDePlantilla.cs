using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class MaestroDePlantilla
    {
        #region Atributos

        private int _id;
        private Plantilla _plantilla;
        private string _referido;
        private string _criterio;
        private string _sql_Encabezado;
        private string _sql_Detalle;
        private string _bat_Encabezado;
        private string _bat_Detalle;
        private Idioma _idioma;
        private ListaDatosValores _tipoReferencia;
        private ListaDatosValores _tipoCriterio;

       
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public MaestroDePlantilla()
        {
        }

        
        /// <summary>
        /// Constructor por defecto que recibe un Id de maestro de plantilla
        /// </summary>
        /// <param name="id"></param>
        public MaestroDePlantilla(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //        return false;
        //    var t = obj as MaestroDePlantilla;
        //    if (t == null)
        //        return false;
        //    if ((Plantilla.Id == (t.Plantilla.Id)))
        //        return true;
        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        //public override string ToString()
        //{
        //    return base.ToString();
        //}

        /// <summary>
        /// Propiedad que asigna u obtiene el id del maestro de la plantilla
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Plantilla del Maestro de Plantilla
        /// </summary>
        public virtual Plantilla Plantilla
        {
            get { return _plantilla; }
            set { _plantilla = value; }
           
        }



        /// <summary>
        /// Propiedad que asigna u obtiene el Referido
        /// </summary>
        public virtual string Referido
        {
            get { return this._referido; }
            set { this._referido = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Criterio
        /// </summary>
        public virtual string Criterio
        {
            get { return _criterio; }
            set { _criterio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el SQL del Encabezado
        /// </summary>
        public virtual string SQL_Encabezado
        {
            get { return _sql_Encabezado; }
            set { _sql_Encabezado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el SQL del Detalle
        /// </summary>
        public virtual string SQL_Detalle
        {
            get { return _sql_Detalle; }
            set { _sql_Detalle = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el BAT del Encabezado
        /// </summary>
        public virtual string BAT_Encabezado
        {
            get { return _bat_Encabezado; }
            set { _bat_Encabezado = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el BAT del Detalle
        /// </summary>
        public virtual string BAT_Detalle
        {
            get { return _bat_Detalle; }
            set { _bat_Detalle = value; }
        }

        
        /// <summary>
        /// Propiedad que asigna u obtiene el Idioma
        /// </summary>
        public virtual Idioma Idioma
        {
            get { return _idioma; }
            set { _idioma = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el objeto con el tipo de Referencia de un Maestro de Plantilla
        /// </summary>
        public virtual ListaDatosValores TipoReferencia
        {
            get { return _tipoReferencia; }
            set { _tipoReferencia = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el objeto con el tipo de Criterio de un Maestro de Plantilla
        /// </summary>
        public virtual ListaDatosValores TipoCriterio
        {
            get { return _tipoCriterio; }
            set { _tipoCriterio = value; }
        }
        

        #endregion
    }
}
