using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//using Trascend.Bolet.ObjetosComunes.Entidades;
namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable()]
    public class FacDepartamentoServicio
    {
        #region "Atributos"

        private Departamento _id;

        private FacServicio _servicio;
        #endregion

        #region "Constructores"
        public FacDepartamentoServicio()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la DepartamentoServicio
        /// </summary>
        /// <param name="id">Id de la tasa</param>
        public FacDepartamentoServicio(Departamento id, FacServicio servicio)
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
            dynamic t = obj as FacDepartamentoServicio;
            if (t == null)
            {
                return false;
            }
            if ((Id.Id == (t.Id.Id)) && (Servicio.Id == t.Servicio.Id))
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
        /// Propiedad que asigna u obtiene el id del Departamento Servicio
        /// </summary>
        public virtual Departamento Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public virtual FacServicio Servicio
        {
            get { return this._servicio; }
            set { this._servicio = value; }
        }

        #endregion

    }
}