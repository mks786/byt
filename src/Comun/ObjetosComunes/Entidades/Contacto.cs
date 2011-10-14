using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Contacto
    {
        #region Atributos

        private int _id;

        private Asociado _asociado;
        
        private string _nombre;

        private string _telefono;

        private string _fax;

        private string _email;

        private string _funcion;

        private string _cargo;

        private string _correspondencia;
        
        private string _departamento;


        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Contacto() { }

        /// <summary>
        /// Constructor que inicializa el Id del Agente
        /// </summary>
        /// <param name="id">Id del Agente</param>
        public Contacto(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades


        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public virtual string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }
        public virtual string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        public virtual string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public virtual string Funcion
        {
            get { return _funcion; }
            set { _funcion = value; }
        }
        public virtual string Cargo
        {
            get { return _cargo; }
            set { _cargo = value; }
        }
        public virtual string Correspondencia
        {
            get { return _correspondencia; }
            set { _correspondencia = value; }
        }

        #endregion
    }
}
