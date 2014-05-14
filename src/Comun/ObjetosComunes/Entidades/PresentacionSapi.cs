using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class PresentacionSapi
    {
        #region Atributos

        private int _id;
        private DateTime? _fecha;
        private string _iniciales;
        private int _cantDocumentos;
        private Departamento _departamento;
        private IList<PresentacionSapiDetalle> _detalleSolicitudPresentacion;
        private string _operacion;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public PresentacionSapi()
        {
        }

        /// <summary>
        /// Constructor que inicializa el Id del Departamento
        /// </summary>
        /// <param name="id">Id del departamento</param>
        public PresentacionSapi(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id de la Presentacion Sapi
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de Creacion de la Presentacion Sapi
        /// </summary>
        public virtual DateTime? Fecha
        {
            get { return this._fecha; }
            set { this._fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene las Iniciales del usuario que genera la solicitud de Presentacion Sapi
        /// </summary>
        public virtual string Iniciales
        {
            get { return this._iniciales; }
            set { this._iniciales = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Departamento del usuario que genera la solicitud de Presentacion Sapi
        /// </summary>
        public virtual Departamento Departamento
        {
            get { return this._departamento; }
            set { this._departamento = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Cantidad de Documentos que forman solicitud de Presentacion Sapi actual
        /// </summary>
        public virtual int CantDocumentos
        {
            get { return this._cantDocumentos; }
            set { this._cantDocumentos = value; } 
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el detalle de una solicitud de Presentacion Sapi
        /// </summary>
        public virtual IList<PresentacionSapiDetalle> DetalleSolicitudPresentacion
        {
            get { return this._detalleSolicitudPresentacion; }
            set { this._detalleSolicitudPresentacion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el tipo de operacion que se va a realizar con la entidad
        /// </summary>
        public virtual string Operacion
        {
            get { return this._operacion; }
            set { this._operacion = value; }
        }

        #endregion
    }
}
