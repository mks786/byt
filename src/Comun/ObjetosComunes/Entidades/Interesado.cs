using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Interesado
    {        
        #region Atributos

        private int _id;
        private char _tipoPersona;
        private string nombre;
        private string ciudad;
        private string estado;  
        private Pais _pais;
        private Pais _nacionalidad;
        private Estado _corporacion;
        private string _ci;
        private string _rMercantil;
        private string _regMercantil;
        private string _domicilio;
        private string _alerta;
        private IList<Poder> _poderes;
        private string _operacion;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Interesado() { }

        /// <summary>
        /// Constructor que inicializa el id del Poder
        /// </summary>
        /// <param name="id">Id del Poder</param>
        public Interesado(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del poder
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el TipoPersona
        /// </summary>
        public virtual char TipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Nombre
        /// </summary>
        public virtual string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Ciudad
        /// </summary>
        public virtual string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Estado
        /// </summary>
        public virtual string Estado
        {
            get { return estado; }
            set { estado = value; }
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
        /// Propiedad que asigna u obtiene la Nacionalidad
        /// </summary>
        public virtual Pais Nacionalidad
        {
            get { return _nacionalidad; }
            set { _nacionalidad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Corporacion
        /// </summary>
        public virtual Estado Corporacion
        {
            get { return _corporacion; }
            set { _corporacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Ci
        /// </summary>
        public virtual string Ci
        {
            get { return _ci; }
            set { _ci = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el RMercantil
        /// </summary>
        public virtual string RMercantil
        {
            get { return _rMercantil; }
            set { _rMercantil = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el RegMercantil
        /// </summary>
        public virtual string RegMercantil
        {
            get { return _regMercantil; }
            set { _regMercantil = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Domicilio
        /// </summary>
        public virtual string Domicilio
        {
            get { return _domicilio; }
            set { _domicilio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Alerta
        /// </summary>
        public virtual string Alerta
        {
            get { return _alerta; }
            set { _alerta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene una lista de sus poderes
        /// </summary>
        public virtual IList<Poder> Poderes
        {
            get { return _poderes; }
            set { _poderes = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la operacion
        /// </summary>
        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        #endregion
    }
}
