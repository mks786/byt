using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class InfoBolMarcaTer
    {
        #region Atributos

        private string _id;
        private MarcaTercero _marca;
        private TipoInfobol _tipoInfobol;
        private string _tomo;
        private string _pagina;
        private string _resolucion;
        private string _comentario;
        private DateTime? _fecha;
        private DateTime? _timeStamp;
        private int _cambio;
        private Boletin _boletin;
        private Usuario _usuario;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public InfoBolMarcaTer()
        {
        }

        #region Constructor original comentado
        /// <summary>
        /// Constructor que inicializa el condigo de la infoBol
        /// </summary>
        /// <param name="id">Codigo de la infoBol</param>
        //public InfoBolMarcaTer(MarcaTercero marca)
        //{
        //    this._id = marca.Id;
        //} 
        #endregion

        /// <summary>
        /// Constructor predeterminado que recibe el string de la Marca a Terceros
        /// </summary>
        /// <param name="id"></param>
        public InfoBolMarcaTer(string id)
        {
            this.TipoInfobol.Id = id;
        }


        #endregion

        #region Propiedades

        public override bool Equals(object obj)
        {
            #region Codigo original comentado
            //if ((this.Id == ((InfoBolMarcaTer)obj).Id)
            //        && (this.TipoInfobol.Id == ((InfoBolMarcaTer)obj).TipoInfobol.Id))
            //    return true;
            //return false; 
            #endregion

            if (obj == null)
                return false;
            var t = obj as InfoBolMarcaTer;
            if (t == null)
                return false;
            if ((TipoInfobol.Id == (t.TipoInfobol.Id)) && (Marca.Id == (t.Marca.Id)))
                return true;
            return false;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el código
        /// </summary>
        public virtual string Id
        {
            get { return this._marca.Id; }
            set { this._marca.Id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Marca
        /// </summary>
        public virtual MarcaTercero Marca
        {
            #region Codigo original comentado
            //get { return _marca; }
            //set
            //{
            //    _marca = value;
            //    this.Id = _marca.Id;
            //} 
            #endregion

            get { return _marca; }
            set { _marca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el TipoInfobol
        /// </summary>
        public virtual TipoInfobol TipoInfobol
        {
            get { return _tipoInfobol; }
            set { _tipoInfobol = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Tomo
        /// </summary>
        public virtual string Tomo
        {
            get { return this._tomo; }
            set { this._tomo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Pagina
        /// </summary>
        public virtual string Pagina
        {
            get { return _pagina; }
            set { _pagina = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Resolucion
        /// </summary>
        public virtual string Resolucion
        {
            get { return _resolucion; }
            set { _resolucion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Comentario
        /// </summary>
        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
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
        /// Propiedad que asigna u obtiene el TimeStamp
        /// </summary>
        public virtual DateTime? TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Cambio
        /// </summary>
        public virtual int Cambio
        {
            get { return _cambio; }
            set { _cambio = value; }
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
        /// Propiedad que asigna u obtiene el Usuario
        /// </summary>
        public virtual Usuario Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        #endregion
    }
}
