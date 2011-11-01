using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Remitente
    {        
        #region Atributos

        private string _id;
        private string _descripcion;
        private char _tipoRemintente;
        private string _direccion;
        private string _ciudad;
        private string _estado;  
        private Pais _pais;
        private string _telefono;
        private string _fax;  

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Remitente() { }

        /// <summary>
        /// Constructor que inicializa el id del Remitente
        /// </summary>
        /// <param name="id">Id del Remitente</param>
        public Remitente(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del Remitente
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Descripcion
        /// </summary>
        public virtual string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el TipoRemitente
        /// </summary>
        public virtual char TipoRemitente
        {
            get { return _tipoRemintente; }
            set { _tipoRemintente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Direccion
        /// </summary>
        public virtual string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Ciudad
        /// </summary>
        public virtual string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Estado
        /// </summary>
        public virtual string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Pais
        /// </summary>
        public virtual Pais Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Telefono
        /// </summary>
        public virtual string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Fax
        /// </summary>
        public virtual string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        #endregion
    }
}
