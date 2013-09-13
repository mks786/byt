using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FiltroReporte
    {
        #region Atributos

        private int _id;
        private Reporte _reporte;
        private CamposReporte _camposReporte;
        //private string _tipoReporte;
        private string _operador;
        private string _valor;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public FiltroReporte() 
        { 
        }


        public FiltroReporte(int id)
        {
            this.Reporte.Id = id;
        }

        
        #endregion

        #region Propiedades

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as FiltroReporte;
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
        /// Propiedad que asigna u obtiene el Id para la entidad que relaciona el Reporte con sus Campos
        /// </summary>
        public virtual int Id
        {
            get { return this._reporte.Id; }
            set { this._reporte.Id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Reporte al que pertence el Filtro de Reporte
        /// </summary>
        public virtual Reporte Reporte
        {
            get { return _reporte; }
            set { _reporte = value; }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene el tipo de reporte
        /// </summary>
        //public virtual string TipoReporte
        //{
        //    get { return _tipoReporte; }
        //    set { _tipoReporte = value; }
        //}



        /// <summary>
        /// Propiedad que asigna u obtiene cada uno de los Campos del Reporte definidos en el Filtro del Reporte 
        /// </summary>
        public virtual CamposReporte Campo
        {
            get { return _camposReporte; }
            set { _camposReporte = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Operador usado en el Filtro de Reporte 
        /// </summary>
        public virtual string Operador
        {
            get { return this._operador; }
            set { this._operador = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Valor que se le da al Filtro de Reporte para la ejecucion del reporte
        /// </summary>
        public virtual string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }


        #endregion
    }
}
