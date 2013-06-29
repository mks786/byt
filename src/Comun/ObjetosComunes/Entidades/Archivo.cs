using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Archivo
    {
        #region Atributos

        private string _id;
        private string _aux;
        private TipoDocumento _documento;
        private string _tipoDeDocumento;
        private int _tipoDeCaja;
        private int _cajaArchivo;
        private Almacen _almacenArchivo;
        private string _usuarioInic;
        private DateTime _fecha;
        private string _operacion;
        //private Marca _marca;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Archivo() { }

        /// <summary>
        /// Constructor que inicializa el id del archivo
        /// </summary>
        /// <param name="id">Id del archivo que corresponde al id de la Marca o de la Patente segun sea el caso</param>
        public Archivo(string id)
        {
            this._id = id;
            TipoDocumento documentoVacio = new TipoDocumento("NGN");
            this._documento = documentoVacio;
            this._aux = "";
            this._tipoDeDocumento = "";
        }

        /// <summary>
        /// Constructor por defecto que recibe el codigo de la marca y el codigo auxiliar 
        /// Esto se usa para marcas internacionales
        /// </summary>
        /// <param name="id">Codigo internacional de la marca</param>
        /// <param name="aux">Correlativo de Expediente</param>
        public Archivo(string id, string aux)
        {
            this._id = id;
            this._aux = aux;
            TipoDocumento documentoVacio = new TipoDocumento("NGN");
            this._documento = documentoVacio;
            this._tipoDeDocumento = "";
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
            var t = obj as Archivo;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (Aux == (t.Aux)) && (Documento.Id == t.Documento.Id))
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
        /// Propiedad que asigna el Expediente del Archivo
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Propiedad que asigna el Aux del Archivo
        /// </summary>
        public virtual string Aux
        {
            get { return this._aux; }
            set { this._aux = value; }
        }



        /// <summary>
        /// Propiedad que asigna el Documento del Archivo
        /// </summary>
        public virtual TipoDocumento Documento
        {
            get { return this._documento; }
            set { this._documento = value; }
        }



        /// <summary>
        /// Propiedad que asigna el TipoDeDocumento del Archivo
        /// </summary>
        public virtual string TipoDeDocumento
        {
            get { return this._tipoDeDocumento; }
            set { this._tipoDeDocumento = value; }
        }



        /// <summary>
        /// Propiedad que asigna el TipoDeCaja del Archivo
        /// </summary>
        public virtual int TipoDeCaja
        {
            get { return this._tipoDeCaja; }
            set { this._tipoDeCaja = value; }
        }


        /// <summary>
        /// Propiedad que asigna la Caja del Archivo
        /// </summary>
        public virtual int CajaArchivo
        {
            get { return this._cajaArchivo; }
            set { this._cajaArchivo = value; }
        }



        /// <summary>
        /// Propiedad que asigna el Almacen del Archivo
        /// </summary>
        public virtual Almacen AlmacenArchivo
        {
            get { return this._almacenArchivo; }
            set { this._almacenArchivo = value; }
        }



        /// <summary>
        /// Propiedad que asigna el Almacen del Archivo
        /// </summary>
        public virtual string Usuario
        {
            get { return this._usuarioInic; }
            set { this._usuarioInic = value; }
        }



        /// <summary>
        /// Propiedad que asigna de la fecha del Archivo
        /// </summary>
        public virtual DateTime Fecha
        {
            get { return this._fecha; }
            set { this._fecha = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la opreacion que se va a realizar con este objeto
        /// sea CREATE, MODIFY o DELETE
        /// </summary>
        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }


        //public virtual Marca Marca
        //{
        //    get { return _marca; }
        //    set { this._marca = value; }
        //}

            

        #endregion
    }
}
