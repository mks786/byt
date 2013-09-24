using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class OrdenReporte
    {
        #region Atributos

        private int _id;
        private Reporte _reporte;
        private CamposReporte _camposReporte;
        private string _tipoOrden;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public OrdenReporte() 
        { 
        }


        public OrdenReporte(int id)
        {
            this.Reporte.Id = id;
        }

        
        #endregion

        #region Propiedades

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as OrdenReporte;
            if (t == null)
                return false;
            if ((Reporte.Id == (t.Reporte.Id)) && (Campo.Id == (t.Campo.Id)))
                return true;
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
        /// Propiedad que asigna u obtiene el Id para la entidad OrdenReporte
        /// </summary>
        public virtual int Id
        {
            get { return this._reporte.Id; }
            set { this._reporte.Id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Reporte al que pertence el Orden de los campos
        /// </summary>
        public virtual Reporte Reporte
        {
            get { return _reporte; }
            set { _reporte = value; }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene cada uno de los Campos del Reporte a ordenar 
        /// </summary>
        public virtual CamposReporte Campo
        {
            get { return _camposReporte; }
            set { _camposReporte = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo de Ordenamiento usado para los campos filtros del Reporte
        /// </summary>
        public virtual string TipoOrdenamiento
        {
            get { return this._tipoOrden; }
            set { this._tipoOrden = value; }
        }

        
        #endregion
    }
}
