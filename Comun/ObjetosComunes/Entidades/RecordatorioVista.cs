using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class RecordatorioVista
    {

        #region Atributos

        private int _id;
        //private int _codigoRegistro;
        //private int _codigoInscripcion;
        //private string _descripcionMarca;
        //private int _codigoAsociado;
        //private string _nombreAsociado;
        private string _nombreInteresado;
        //private int _condicion;
        private string _fechaRenovacion;
        private string _ano;
        private string _mes;
        private string _fechaGracia;
        private string _anoGracia;
        private string _mesGracia;
        private string _direccion;
        //private string _fax;
        //private string _email;
        private string _pais;
        private string _idioma;
        //private string _tipo;
        private string _fechaRenovacionIn;
        private string _fechaGraciaIn;
        private string _clase;
        private DateTime? _fechaRenovacion1;


        private Marca _marca;
        private Asociado _asociado;
        //private Interesado _interesado;
        //private Pais _pais;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public RecordatorioVista()
        {
            
        }


        /// <summary>
        /// Constructor que inicializa el id de la renovacionVista
        /// </summary>
        /// <param name="id">Id de la renovacionVista</param>
        public RecordatorioVista(int id) : this()
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        
        //public int CodigoRegistro
        //{
        //    get { return _codigoRegistro; }
        //    set { _codigoRegistro = value; }
        //}

        
        //public int CodigoInscripcion
        //{
        //    get { return _codigoInscripcion; }
        //    set { _codigoInscripcion = value; }
        //}

        
        //public string DescripcionMarca
        //{
        //    get { return _descripcionMarca; }
        //    set { _descripcionMarca = value; }
        //}
        

        //public int CodigoAsociado
        //{
        //    get { return _codigoAsociado; }
        //    set { _codigoAsociado = value; }
        //}


        //public string NombreAsociado
        //{
        //    get { return _nombreAsociado; }
        //    set { _nombreAsociado = value; }
        //}


        public virtual string NombreInteresado
        {
            get { return _nombreInteresado; }
            set { _nombreInteresado = value; }
        }


        //public int Condicion
        //{
        //    get { return _condicion; }
        //    set { _condicion = value; }
        //}


        public virtual string FechaRenovacion
        {
            get { return _fechaRenovacion; }
            set { _fechaRenovacion = value; }
        }


        public virtual string Ano
        {
            get { return _ano; }
            set { _ano = value; }
        }


        public virtual string Mes
        {
            get { return _mes; }
            set { _mes = value; }
        }


        public virtual string FechaGracia
        {
            get { return _fechaGracia; }
            set { _fechaGracia = value; }
        }


        public virtual string AnoGracia
        {
            get { return _anoGracia; }
            set { _anoGracia = value; }
        }


        public virtual string MesGracia
        {
            get { return _mesGracia; }
            set { _mesGracia = value; }
        }


        public virtual string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }


        //public string Fax
        //{
        //    get { return _fax; }
        //    set { _fax = value; }
        //}

        
        //public string Email
        //{
        //    get { return _email; }
        //    set { _email = value; }
        //}


        public virtual string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }


        public virtual string Idioma
        {
            get { return _idioma; }
            set { _idioma = value; }
        }


        //public string Tipo
        //{
        //    get { return _tipo; }
        //    set { _tipo = value; }
        //}


        public virtual string FechaRenovacionIn
        {
            get { return _fechaRenovacionIn; }
            set { _fechaRenovacionIn = value; }
        }


        public virtual string FechaGraciaIn
        {
            get { return _fechaGraciaIn; }
            set { _fechaGraciaIn = value; }
        }


        public virtual string Clase
        {
            get { return _clase; }
            set { _clase = value; }
        }


        public virtual DateTime? FechaRenovacion1
        {
            get { return _fechaRenovacion1; }
            set { _fechaRenovacion1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Agente
        /// </summary>
        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Agente
        /// </summary>
        //public virtual Interesado Interesado
        //{
        //    get { return _interesado; }
        //    set { _interesado = value; }
        //}

        /// <summary>
        /// Propiedad que asigna u obtiene el Agente
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Agente
        /// </summary>
        //public virtual Pais Pais
        //{
        //    get { return _pais; }
        //    set { _pais = value; }
        //}

        #endregion

    }
}
