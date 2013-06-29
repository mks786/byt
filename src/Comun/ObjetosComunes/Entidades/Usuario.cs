using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Usuario
    {

        #region Atributos

        private string _id;
        private string _nombreCompleto;
        private string _password;
        private string _iniciales;
        private Rol _rol;
        private Departamento _departamento;
        private string _email;
        private IList<Asignacion> _asignaciones;
        private char _autorizar;
        private int _hash;
        private char _archivo;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Usuario() { }

        /// <summary>
        /// Constructor que inicializa el id del Usuario
        /// </summary>
        /// <param name="id">Id del Usuario</param>
        public Usuario(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades


        /// <summary>
        /// Propiedad que asigna u obtiene el Id del Usuario
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el nombre completo del Usuario
        /// </summary>
        public virtual string NombreCompleto
        {
            get { return this._nombreCompleto; }
            set { this._nombreCompleto = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la contraseña del Usuario
        /// </summary>
        public virtual string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene las Iniciales del Usuario
        /// </summary>
        public virtual string Iniciales
        {
            get { return this._iniciales; }
            set { this._iniciales = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Rol del Usuario
        /// </summary>
        public virtual Rol Rol
        {
            get { return this._rol; }
            set { this._rol = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Departamento del Usuario
        /// </summary>
        public virtual Departamento Departamento
        {
            get { return this._departamento; }
            set { this._departamento = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el email del Usuario
        /// </summary>
        public virtual string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        /*
        /// <summary>
        /// Propiedad que asigna u obtiene el EmailEdo del Usuario
        /// </summary>
        public virtual string EmailEdo
        {
            get { return this._emailEdo; }
            set { this._emailEdo = value; }
        }
        */

        /// <summary>
        /// Propiedad que asigna u obtiene el carácter de autorizar el Usuario
        /// </summary>
        public virtual char Autorizar
        {
            get { return this._autorizar; }
            set { this._autorizar = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de autorizar el Usuario
        /// </summary>
        public virtual bool BAutorizar
        {
            get 
            {
                if (this.Autorizar.Equals('1'))
                    return true;
                else
                    return false;
            }
            set 
            {
                if (value)
                    this.Autorizar = '1';
                else
                    this.Autorizar = '0';
            }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el carácter de archivo del Usuario
        /// </summary>
        public virtual char ModificarArchivo
        {
            get { return this._archivo; }
            set { this._archivo = value; }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de modificar el archivo del Usuario
        /// </summary>
        public virtual bool BArchivo
        {
            get
            {
                if (this.ModificarArchivo.Equals('T'))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.ModificarArchivo = '1';
                else
                    this.ModificarArchivo = '0';
            }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene el Hash del Usuario
        /// </summary>
        public virtual int Hash
        {
            get { return this._hash; }
            set { this._hash = value; }
        }


        public virtual IList<Asignacion> Asignaciones
        {
            get { return _asignaciones; }
            set { _asignaciones = value; }
        }

        #endregion
    }
}
