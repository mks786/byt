using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class EntradaAlterna
    {        
        #region Atributos

        private int _id;
        private DateTime? _fecha;
        private DateTime? _hora;
        private Medio _medio;
        private string _receptor;
        private Remitente _remitente;
        private Categoria _categoria;
        private string _codigoDestinatario;
        private char _tipoDestinatario;
        private string _descripcion;
        private string _descripcionDestinatario;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public EntradaAlterna() { }

        /// <summary>
        /// Constructor que inicializa el id de la Entrada Alterna
        /// </summary>
        /// <param name="id">Id de la Entrada Alterna</param>
        public EntradaAlterna(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna el id de la Entrada Alterna
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna la fecha de la Entrada Alterna
        /// </summary>
        public virtual DateTime? Fecha
        {
            get { return this._fecha; }
            set { this._fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna la hora de la Entrada Alterna
        /// </summary>
        public virtual DateTime? Hora
        {
            get { return this._hora; }
            set { this._hora = value; }
        }

        /// <summary>
        /// Propiedad que asigna el medio de la Entrada Alterna
        /// </summary>
        public virtual Medio Medio
        {
            get { return this._medio; }
            set { this._medio = value; }
        }

        /// <summary>
        /// Propiedad que asigna el receptor de la Entrada Alterna
        /// </summary>
        public virtual string Receptor
        {
            get { return this._receptor; }
            set { this._receptor = value; }
        }

        /// <summary>
        /// Propiedad que asigna el remitente de la Entrada Alterna
        /// </summary>
        public virtual Remitente Remitente
        {
            get { return this._remitente; }
            set { this._remitente = value; }
        }

        /// <summary>
        /// Propiedad que asigna la categoria de la Entrada Alterna
        /// </summary>
        public virtual Categoria Categoria
        {
            get { return this._categoria; }
            set { this._categoria = value; }
        }

        /// <summary>
        /// Propiedad que asigna el departamento de la Entrada Alterna
        /// </summary>
        public virtual string CodigoDestinatartio
        {
            get { return this._codigoDestinatario; }
            set { this._codigoDestinatario = value; }
        }

        /// <summary>
        /// Propiedad que asigna el tipo de destinatario de la Entrada Alterna
        /// </summary>
        public virtual char TipoDestinatario
        {
            get { return this._tipoDestinatario; }
            set { this._tipoDestinatario = value; }
        }

        /// <summary>
        /// Propiedad que asigna la descripcion de la Entrada Alterna
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna la descripcion del departamento de la Entrada Alterna
        /// </summary>
        public virtual string DescripcionDestinatario
        {
            get { return this._descripcionDestinatario; }
            set { this._descripcionDestinatario = value; }
        }

        #endregion
    }
}
