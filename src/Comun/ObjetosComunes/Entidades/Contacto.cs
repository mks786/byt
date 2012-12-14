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

        private Carta _carta;

        private string _departamento;

        private string _datosAsociado;

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Contacto() { }

        /// <summary>
        /// Constructor que inicializa el Id del Inventor
        /// </summary>
        /// <param name="id">Id del Agente</param>
        public Contacto(int id)
        {
            this._id = id;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Sobreescritura del Método Equals debido a que la clase tiene id compuesto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Contacto;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (Asociado.Id == (t.Asociado.Id)))
                return true;
            return false;

        }

        /// <summary>
        /// Sobreescritura del Método GetHashCode debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Sobreescritura del Método ToString debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el asociado dueño de el contacto
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el Id del contacto
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el nombre del contacto
        /// </summary>
        public virtual string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el Departamento del contacto
        /// </summary>
        public virtual string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el telefono del contacto
        /// </summary>
        public virtual string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el fax del contacto
        /// </summary>
        public virtual string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el email del contacto
        /// </summary>
        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene la funcion del contacto
        /// </summary>
        public virtual string Funcion
        {
            get { return _funcion; }
            set { _funcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el cargo del contacto
        /// </summary>
        public virtual string Cargo
        {
            get { return _cargo; }
            set { _cargo = value; }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene la carta del contacto
        /// </summary>
        public virtual Carta Carta
        {
            get { return _carta; }
            set { _carta = value; }
        }

        public virtual string DatosAsociado
        {
            get
            {
                _datosAsociado = string.Empty;
                string salto = Environment.NewLine;
                _datosAsociado += Asociado.Id + ", " + Asociado.Nombre + salto;
                _datosAsociado += Asociado.Domicilio + salto;
                _datosAsociado += Asociado.Telefono1 + ", " + Asociado.Telefono2 + "," + Asociado.Telefono3 + salto;
                _datosAsociado += Asociado.Fax1 + ", " + Asociado.Fax2 + "," + Asociado.Fax3 + salto;
                _datosAsociado += Asociado.Web + salto;
                _datosAsociado += Asociado.Email + salto;
                return _datosAsociado;
            }
            set
            {
                string salto = Environment.NewLine;
                _datosAsociado += Asociado.Nombre + salto;
                _datosAsociado += Asociado.Domicilio + salto;
                _datosAsociado += Asociado.Telefono1 + ", " + Asociado.Telefono2 + "," + Asociado.Telefono3 + salto;
                _datosAsociado += Asociado.Fax1 + ", " + Asociado.Fax2 + "," + Asociado.Fax3 + salto;
                _datosAsociado += Asociado.Web + salto;
                _datosAsociado += Asociado.Email + salto;
            }
        }

        #endregion

    }
}
