using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class ContactosDelAsociadoVista
    {

        #region Atributos

        private string _id;
        private int _asociado;
        private int _contacto;
        private string _email;
        private string _funcion;
        private string _nombre;
        private string _fecha;
        private string _fechaMaxEnv;
        private string _fechaMaxEnt;
        private int _cartaCreacion;
        private int _ultimaCartaSalida;
        private int _ultimaCartaEntrada;

        private char _tipo;

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public ContactosDelAsociadoVista()
        {

        }

        #endregion


        //#region Overrides

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        //public override string ToString()
        //{
        //    return base.ToString();
        //}

        //public override bool Equals(object obj)
        //{
        //    bool retorno = false;
        //    try
        //    {

        //        if ((((ContactosDelAsociadoVista)obj)._asociado.Id == this._asociado.Id) && (((ContactosDelAsociadoVista)obj)._contacto.Id == this._contacto.Id))
        //        {
        //            retorno = true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string a = ex.Message;
        //    }
        //    return retorno;
        //}

        //#endregion


        #region Propiedades


        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Agente
        /// </summary>
        public virtual int Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        public virtual int UltimaCartaSalida
        {
            get { return _ultimaCartaSalida; }
            set { _ultimaCartaSalida = value; }
        }

        public virtual int UltimaCartaEntrada
        {
            get { return _ultimaCartaEntrada; }
            set { _ultimaCartaEntrada = value; }
        }

        public virtual int CartaCreacion
        {
            get { return _cartaCreacion; }
            set { _cartaCreacion = value; }
        }

        public virtual int Contacto
        {
            get { return _contacto; }
            set { _contacto = value; }
        }

        public virtual char Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
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

        public virtual string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public virtual string Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public virtual string FechaMaxEnv
        {
            get { return _fechaMaxEnv; }
            set { _fechaMaxEnv = value; }
        }

        public virtual string FechaMaxEnt
        {
            get { return _fechaMaxEnt; }
            set { _fechaMaxEnt = value; }
        }

        #endregion


    }
}
