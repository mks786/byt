using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacAnuladaFisica
    {
        #region "Atributos"

            private int? _id;
            private  DateTime  _fechaanulacion;
            private  int? _control;
            private Boolean _seleccion;
        #endregion

        #region "Constructores"
        public FacAnuladaFisica()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacAnuladaFisica
        /// </summary>
        /// <param name="id">Id de FacAnuladaFisica</param>
        public FacAnuladaFisica(int id, DateTime fechaanulacion)
        {
            this._id = id;
            this._fechaanulacion = fechaanulacion;
        }
        #endregion

        #region "Propiedades"

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            // var t = ((ChequeRecido)obj);
            var t = obj as FacAnuladaFisica;
            if (t == null)
            {
                return false;
            }
            if ((Id == (t.Id)) && (FechaAnulacion == t.FechaAnulacion))
            {
                //If (Doc_servicio = t.Doc_servicio) Then
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
 
        /// <summary>
        /// Propiedad que asigna u obtiene el id de la FacAnuladaFisica
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

        public virtual DateTime FechaAnulacion
        {
            get
            {
                return this._fechaanulacion;
            }
            set
            {
                this._fechaanulacion = value;
            }
        }
        // ''' <summary>
        // ''' Propiedad que asigna u obtiene xbanco
        // ''' </summary>
        public virtual int? Control
        {
            get
            {
                return this._control;
            }
            set
            {
                this._control = value;
            }
        }


        public virtual bool Seleccion
        {
            get
            {
                return this._seleccion;
            }
            set
            {
                this._seleccion = value;
            }
        }

        #endregion

    }
}