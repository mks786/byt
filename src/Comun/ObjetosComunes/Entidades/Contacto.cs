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

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Contacto() { }

        /// <summary>
        /// Constructor que inicializa el Id del Contacto
        /// </summary>
        /// <param name="id">Id del Agente</param>
        public Contacto(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades


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

        //public override bool Equals(object obj)
        //{
        //    if ((this.Id == ((Contacto)obj).Id) && (this.Asociado.Id == ((Contacto)obj).Asociado.Id))
        //        return true;
        //    return false;
        //}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

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
        public virtual Carta Carta
        {
            get { return _carta; }
            set { _carta = value; }
        }

        #endregion
    }
}
