using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Operacion
    {
        #region Atributos

        private int _id;
        private DateTime? _fecha;
        private string _descripcion;
        private char _aplicada;
        private int _codigoAplicada;
        private int _interno;
        private Servicio _servicio;
        private Asociado _asociado;
        private Interesado _interesado;
        private Boletin _boletin;
        private Marca _marca;
        private MarcaTercero _marcaTercero;
        private Patente _patente;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Operacion() 
        {
            this._aplicada = 'M';
        }

        /// <summary>
        /// Constructor que inicializa el id del Concepto
        /// </summary>
        /// <param name="id">Id del Concepto</param>
        public Operacion(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Descripcion
        /// </summary>
        public virtual string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene a que se aplica la operacion 
        /// M para marca y P para patente
        /// </summary>
        public virtual char Aplicada
        {
            get { return _aplicada; }
            set { _aplicada = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano que describe si es marca o patente
        /// </summary>
        public virtual bool BAplicada
        {
            get
            {
                if (this.Aplicada == 'M')
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Aplicada = 'M';
                else
                    this.Aplicada = 'P';
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Codigo Aplicada
        /// </summary>
        public virtual int CodigoAplicada
        {
            get { return _codigoAplicada; }
            set { _codigoAplicada = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el numero Interno
        /// </summary>
        public virtual int Interno
        {
            get { return _interno; }
            set { _interno = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha
        /// </summary>
        public virtual DateTime? Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Asociado
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Servicio
        /// </summary>
        public virtual Servicio Servicio
        {
            get { return _servicio; }
            set { _servicio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Interesado
        /// </summary>
        public virtual Interesado Interesado
        {
            get { return _interesado; }
            set { _interesado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Boletin
        /// </summary>
        public virtual Boletin Boletin
        {
            get { return _boletin; }
            set { _boletin = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Marca
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Marca
        /// </summary>
        public virtual MarcaTercero MarcaTercero
        {
            get { return _marcaTercero; }
            set { _marcaTercero = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Patente
        /// </summary>
        public virtual Patente Patente
        {
            get { return _patente; }
            set { _patente = value; }
        }

        #endregion
    }
}
