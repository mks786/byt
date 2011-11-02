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
        private DateTime _fecha;
        private Medio _medio;
        private Usuario _receptor;
        private Remitente _remitente;
        private Categoria _categoria;
        private string _descripcion;


        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public EntradaAlterna() { }

        /// <summary>
        /// Constructor que inicializa el id del Entrada Alterna
        /// </summary>
        /// <param name="id">Id del Entrada Alterna</param>
        public EntradaAlterna(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        ///// <summary>
        ///// Propiedad que asigna u obtiene el Id del Remitente
        ///// </summary>
        //public virtual string Id
        //{
        //    get { return this._id; }
        //    set { this._id = value; }
        //}

        ///// <summary>
        ///// Propiedad que asigna u obtiene el Descripcion
        ///// </summary>
        //public virtual string Descripcion
        //{
        //    get { return _descripcion; }
        //    set { _descripcion = value; }
        //}

        ///// <summary>
        ///// Propiedad que asigna u obtiene el TipoRemitente
        ///// </summary>
        //public virtual char TipoRemitente
        //{
        //    get { return _tipoRemintente; }
        //    set { _tipoRemintente = value; }
        //}

        ///// <summary>
        ///// Propiedad que asigna u obtiene el Direccion
        ///// </summary>
        //public virtual string Direccion
        //{
        //    get { return _direccion; }
        //    set { _direccion = value; }
        //}

        ///// <summary>
        ///// Propiedad que asigna u obtiene la Ciudad
        ///// </summary>
        //public virtual string Ciudad
        //{
        //    get { return _ciudad; }
        //    set { _ciudad = value; }
        //}

        ///// <summary>
        ///// Propiedad que asigna u obtiene el Estado
        ///// </summary>
        //public virtual string Estado
        //{
        //    get { return _estado; }
        //    set { _estado = value; }
        //}

        ///// <summary>
        ///// Propiedad que asigna u obtiene el Pais
        ///// </summary>
        //public virtual Pais Pais
        //{
        //    get { return _pais; }
        //    set { _pais = value; }
        //}

        ///// <summary>
        ///// Propiedad que asigna u obtiene la Telefono
        ///// </summary>
        //public virtual string Telefono
        //{
        //    get { return _telefono; }
        //    set { _telefono = value; }
        //}

        ///// <summary>
        ///// Propiedad que asigna u obtiene el Fax
        ///// </summary>
        //public virtual string Fax
        //{
        //    get { return _fax; }
        //    set { _fax = value; }
        //}

        #endregion
    }
}
