using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CamposReporteRelacion
    {
        #region Atributos

        private int _id;
        private Reporte _reporte;
        private CamposReporte _camposReporte;
        //private string _tipoDeReporte;
        private int _posicionCampo;
        private string _statusCampo;
        private string _filtro;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public CamposReporteRelacion() { }


        public CamposReporteRelacion(int id)
        {
            this.Reporte.Id = id;
        }

        
        #endregion

        #region Propiedades

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as CamposReporteRelacion;
            if (t == null)
                return false;
            if((Reporte.Id == (t.Reporte.Id)) && (Campo.Id == (t.Campo.Id)))
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
        /// Propiedad que asigna u obtiene el Id para la entidad que relaciona el Reporte  con sus Campos
        /// </summary>
        public virtual int Id
        {
            get { return this._reporte.Id; }
            set { this._reporte.Id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Reporte 
        /// </summary>
        public virtual Reporte Reporte
        {
            get { return _reporte; }
            set { _reporte = value; }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene cada uno de los Campos del Reporte 
        /// </summary>
        public virtual CamposReporte Campo
        {
            get { return _camposReporte; }
            set { _camposReporte = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el tipo de Reporte del campo relacion del reporte
        /// </summary>
        //public virtual string TipoDeReporte
        //{
        //    get { return _tipoDeReporte; }
        //    set { _tipoDeReporte = value; }
        //}

  
        /// <summary>
        /// Propiedad que asigna u obtiene la Posicion del Campo dentro del Reporte 
        /// </summary>
        public virtual int PosicionCampo
        {
            get { return this._posicionCampo; }
            set { this._posicionCampo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Status del Campo del Reporte para indicar si el campo fue eliminado o no 
        /// </summary>
        public virtual string StatusCampo
        {
            get { return _statusCampo; }
            set { _statusCampo = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el status de Filtro del campo del Reporte. Esto sirve a la hora de quitar un campo
        /// del reporte. 
        /// </summary>
        public virtual string CampoFiltro
        {
            get { return _filtro; }
            set { _filtro = value; }
        }


        #endregion
    }


}
