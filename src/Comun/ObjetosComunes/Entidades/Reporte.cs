using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Reporte
    {
        #region Atributos

        private int _id;
        private VistaReporte _vistaReporte;
        private string _descripcion;
        private string _usuario;
        private string _tituloEspanol;
        private string _tituloIngles;
        private Idioma _idioma;
        private IList<CamposReporteRelacion> _camposDelReporte;
        private IList<FiltroReporte> _filtrosDelReporte;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Reporte() { }

        /// <summary>
        /// Constructor que inicializa el id del Reporte de Marca
        /// </summary>
        /// <param name="id">Id de la plantilla</param>
        public Reporte(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del Reporte 
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Vista que compete al reporte
        /// </summary>
        public virtual VistaReporte VistaReporte
        {
            get { return this._vistaReporte; }
            set { this._vistaReporte = value; }
        }
        

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del Reporte
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Usuario del Reporte 
        /// </summary>
        public virtual string Usuario
        {
            get { return this._usuario; }
            set { this._usuario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Titulo en Español del Reporte 
        /// </summary>
        public virtual string TituloEspanol
        {
            get { return this._tituloEspanol; }
            set { this._tituloEspanol = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Titulo en Ingles del Reporte 
        /// </summary>
        public virtual string TituloIngles
        {
            get { return this._tituloIngles; }
            set { this._tituloIngles = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Idioma del Reporte 
        /// </summary>
        public virtual Idioma Idioma
        {
            get { return this._idioma; }
            set { this._idioma = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la lista de los Campos configurarados para un Reporte 
        /// Estos son los campos que se van a recuperar en la consulta a la vista previamente creada
        /// </summary>
        public virtual IList<CamposReporteRelacion> CamposDelReporte
        {
            get { return this._camposDelReporte; }
            set { this._camposDelReporte = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la lista de los Filtros de un Reporte 
        /// </summary>
        public virtual IList<FiltroReporte> FiltrosDelReporte
        {
            get { return this._filtrosDelReporte; }
            set { this._filtrosDelReporte = value; }
        }

               

        #endregion
    }
}
