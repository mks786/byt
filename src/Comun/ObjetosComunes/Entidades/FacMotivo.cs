using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacMotivo
    {
        #region "Atributos"

        private int? _id;
        private string _detalle;

        private bool _seleccion;

        #endregion

        #region "Constructores"
        public FacMotivo()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacFacturaAnulada
        /// </summary>
        /// <param name="id">Id de FacFacturaAnulada</param>
        public FacMotivo(int? id)
        {
            this._id = id;
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
        public virtual int? Id
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
        

        #endregion

    }
}