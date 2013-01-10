using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacDesgloseCole
    {
        #region "Atributos"

        private char? _id;
        private Idioma _idioma;
        private string _detalle;

        private bool _seleccion;

        #endregion

        #region "Constructores"
        public FacDesgloseCole()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacFacturaAnulada
        /// </summary>
        /// <param name="id">Id de FacFacturaAnulada</param>
        public FacDesgloseCole(char? id,Idioma idioma)
        {
            this._id = id;
            this._idioma = idioma;
        }

        //para llave compuesta
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            dynamic t = obj as FacDesgloseServicio;
            if (t == null)
            {
                return false;
            }
            if ((Id == (t.Id)) && (Idioma.Id == t.Idioma.Id))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        #region "Propiedades"
        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return false;
        //    }
        //    // var t = ((ChequeRecido)obj);
        //    var t = obj as FacFacturaAnulada;
        //    if (t == null)
        //    {
        //        return false;
        //    }
        //    if ((Id == (t.Id)) && (CodigoOperacion == t.CodigoOperacion))
        //    {
        //        //If (Doc_servicio = t.Doc_servicio) Then
        //        return true;
        //    }
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
        /// Propiedad que asigna u obtiene el id de la FacFacturaAnulada
        /// </summary>
        public virtual char? Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public virtual string Detalle
        {
            get
            {
                return this._detalle;
            }
            set
            {
                this._detalle = value;
            }
        }

        public virtual Idioma Idioma
        {
            get
            {
                return this._idioma;
            }
            set
            {
                this._idioma = value;
            }
        }
        #endregion

    }
}