using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//using Trascend.Bolet.ObjetosComunes.Entidades;
namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable()]
    public class FacDesgloseServicio
    {
        #region "Atributos"

        private char _id;
        private FacServicio _servicio;

        private double _pporc;
        #endregion

        #region "Constructores"
        public FacDesgloseServicio()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la DesgloseServicio
        /// </summary>
        /// <param name="id">Id de la DesgloseServicio</param>
        public FacDesgloseServicio(char id, FacServicio servicio)
        {
            this._id = id;
            this._servicio = servicio;
        }
        #endregion

        #region "Propiedades"

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
            if ((Id == (t.Id)) && (Servicio.Id == t.Servicio.Id))
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


        /// <summary>
        /// Propiedad que asigna u obtiene el id de la DesgloseServicio
        /// </summary>
        public virtual char Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        public virtual String BId
        {
            get
            {
                if (this.Id.Equals('G'))
                    return "Gastos";
                else
                    if (this.Id.Equals('H'))
                        return "Honorarios";
                    else
                                return "";
            }
        }


        public virtual FacServicio Servicio
        {
            get { return this._servicio; }
            set { this._servicio = value; }
        }


        public virtual double Pporc
        {
            get { return this._pporc; }
            set { this._pporc = value; }
        }

        #endregion

    }
}