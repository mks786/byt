using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class MaterialSapi
    {
        #region Atributos

        private string _id;
        private string _descripcion;
        private string _tipo;
        private string _tipoDescripcion;
        private double? _precio;
        private DateTime? _fechaVigencia;
        private int _existencia;
        private Departamento _departamento;
        private string _tablaReferencia;
        private string _campoReferencia;
        private char? _tipoFacturacion;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public MaterialSapi() { }

        /// <summary>
        /// Constructor que inicializa el Id del Departamento
        /// </summary>
        /// <param name="id">Id del departamento</param>
        public MaterialSapi(string id)
        {
            this._id = id;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del Material
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción del Material
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo de Material
        /// </summary>
        public virtual string Tipo
        {
            get { return this._tipo; }
            set { this._tipo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Descripcion del Tipo de Material
        /// </summary>
        public virtual string TipoDescripcion
        {
            get
            {
                switch (this._tipo)
                {
                    case "P":
                        this._tipoDescripcion = "Planilla";
                        break;
                    case "E":
                        this._tipoDescripcion = "Escrito";
                        break;
                    case "B":
                        this._tipoDescripcion = "Busqueda";
                        break;
                    case "T":
                        this._tipoDescripcion = "Timbres";
                        break;
                    case "O":
                        this._tipoDescripcion = "Otros";
                        break;
                    
                }

                return this._tipoDescripcion;
            }

            set { this._tipoDescripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Precio del Material
        /// </summary>
        public virtual double? Precio
        {
            get { return this._precio; }
            set { this._precio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de Vigencia del Material
        /// </summary>
        public virtual DateTime? FechaVigencia
        {
            get { return this._fechaVigencia; }
            set { this._fechaVigencia = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Existencia actual del Material
        /// </summary>
        public virtual int Existencia
        {
            get { return this._existencia; }
            set { this._existencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Departamento al que pertenece el Material
        /// </summary>
        public virtual Departamento Departamento
        {
            get { return this._departamento; }
            set { this._departamento = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Tabla a la que hace referencia el Material
        /// </summary>
        public virtual string TablaReferencia
        {
            get { return this._tablaReferencia; }
            set { this._tablaReferencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el campo de la Tabla a la que hace referencia el Material
        /// </summary>
        public virtual string CampoReferencia
        {
            get { return this._campoReferencia; }
            set { this._campoReferencia = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo de Facturacion del Material 
        /// </summary>
        public virtual char? TipoFacturacion
        {
            get { return this._tipoFacturacion; }
            set { this._tipoFacturacion = value; }
        }


        #endregion
    }
}
