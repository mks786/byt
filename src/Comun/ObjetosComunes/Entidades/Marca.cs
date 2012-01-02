using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Marca
    {
        #region Atributos

        private int _id;
        private string _descripcion;
        private string _tipo;
        private DateTime? _fechaPrioridad;
        private string _primeraReferencia;
        private DateTime? _fechaInscripcion;
        private string _codigoInscripcion;
        private DateTime? _fechaPublicacion;
        private DateTime? _fechaConcesion;
        private string _codigoRegistro;
        private DateTime? _fechaRegistro;
        private string _iPoder;
        private string _fascimiles;
        private string _etiqueta;
        private char _traduccion;
        private string _fichas;
        private char _registro;
        private char _acta;
        private string _codigoExpediente;
        private string _ubicacion;
        private string _ubicacionRenovacion;
        private DateTime? _fechaRenovacion;
        private string _etiquetaDescripcion;
        private string _primerOtro;
        private string _representante;
        private string _observacion;
        private string _distingue;
        private int _numeroCondiciones;
        private string _sector;
        private char _tipoRps;
        private string _tipoCnac;
        private TipoEstado _tipoEstado;
        private char _rev;
        private string _codigoTipoEstado;
        private string _codigoBus;
        private char _ter;
        private Corresponsal _corresponsal;
        private Agente _agente;
        private Asociado _asociado;
        private Boletin _boletinConcesion;
        private Boletin _boletinPublicacion;
        private Internacional _internacional;
        private Nacional _nacional;
        private Interesado _interesado;
        private Pais _pais;
        private Servicio _servicio;
        private Poder _poder;
        private string _cPrioridad;
        private string _operacion;
        private IList<Anaqua> _anaquas;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Marca() 
        {
            this._etiqueta = "No";
        }

        /// <summary>
        /// Constructor que inicializa el id de la marca
        /// </summary>
        /// <param name="id">Id de la marca</param>
        public Marca(int id) : this()
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
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripción
        /// </summary>
        public virtual string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo
        /// </summary>
        public virtual string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de Prioridad
        /// </summary>
        public virtual DateTime? FechaPrioridad
        {
            get { return _fechaPrioridad; }
            set { _fechaPrioridad = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la PrimeraReferencia
        /// </summary>
        public virtual string PrimeraReferencia
        {
            get { return _primeraReferencia; }
            set { _primeraReferencia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de Inscripcion
        /// </summary>
        public virtual DateTime? FechaInscripcion
        {
            get { return _fechaInscripcion; }
            set { _fechaInscripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Codigo de Inscripcion
        /// </summary>
        public virtual string CodigoInscripcion
        {
            get { return _codigoInscripcion; }
            set { _codigoInscripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la FechaPublicacion
        /// </summary>
        public virtual DateTime? FechaPublicacion
        {
            get { return _fechaPublicacion; }
            set { _fechaPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la FechaConcesion
        /// </summary>
        public virtual DateTime? FechaConcesion
        {
            get { return _fechaConcesion; }
            set { _fechaConcesion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Codigo de Registro
        /// </summary>
        public virtual string CodigoRegistro
        {
            get { return _codigoRegistro; }
            set { _codigoRegistro = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la FechaRegistro
        /// </summary>
        public virtual DateTime? FechaRegistro
        {
            get { return _fechaRegistro; }
            set { _fechaRegistro = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el IPoder
        /// </summary>
        public virtual string IPoder
        {
            get { return _iPoder; }
            set { _iPoder = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Fascimiles
        /// </summary>
        public virtual string Fascimiles
        {
            get { return _fascimiles; }
            set { _fascimiles = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene si Tiene Etiqueta
        /// </summary>
        public virtual string EtiquetaDescripcion
        {
            get { return _etiquetaDescripcion; }
            set { _etiquetaDescripcion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Traduccion
        /// </summary>
        public virtual char Traduccion
        {
            get { return _traduccion; }
            set { _traduccion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene las Fichas
        /// </summary>
        public virtual string Fichas
        {
            get { return _fichas; }
            set { _fichas = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Registro
        /// </summary>
        public virtual char Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Acta
        /// </summary>
        public virtual char Acta
        {
            get { return _acta; }
            set { _acta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el CodigoExpediente
        /// </summary>
        public virtual string CodigoExpediente
        {
            get { return _codigoExpediente; }
            set { _codigoExpediente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Ubicacion
        /// </summary>
        public virtual string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la UbicacionRenovacion
        /// </summary>
        public virtual string UbicacionRenovacion
        {
            get { return _ubicacionRenovacion; }
            set { _ubicacionRenovacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la FechaRenovacion
        /// </summary>
        public virtual DateTime? FechaRenovacion
        {
            get { return _fechaRenovacion; }
            set { _fechaRenovacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Etiqueta
        /// </summary>
        public virtual string Etiqueta
        {
            get { return _etiqueta; }
            set { _etiqueta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el PrimerOtro
        /// </summary>
        public virtual string PrimerOtro
        {
            get { return _primerOtro; }
            set { _primerOtro = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Representante
        /// </summary>
        public virtual string Representante
        {
            get { return _representante; }
            set { _representante = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Observacion
        /// </summary>
        public virtual string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Distingue
        /// </summary>
        public virtual string Distingue
        {
            get { return _distingue; }
            set { _distingue = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Numero de Condiciones
        /// </summary>
        public virtual int NumeroCondiciones
        {
            get { return _numeroCondiciones; }
            set { _numeroCondiciones = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Sector
        /// </summary>
        public virtual string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el TipoRps
        /// </summary>
        public virtual char TipoRps
        {
            get { return _tipoRps; }
            set { _tipoRps = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el TipoCnac
        /// </summary>
        public virtual string TipoCnac
        {
            get { return _tipoCnac; }
            set { _tipoCnac = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo del Estado
        /// </summary>
        public virtual TipoEstado TipoEstado
        {
            get { return _tipoEstado; }
            set { _tipoEstado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Rev
        /// </summary>
        public virtual char Rev
        {
            get { return _rev; }
            set { _rev = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el CodigoTipoEstado
        /// </summary>
        public virtual string CodigoTipoEstado
        {
            get { return _codigoTipoEstado; }
            set { _codigoTipoEstado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el CodigoBus
        /// </summary>
        public virtual string CodigoBus
        {
            get { return _codigoBus; }
            set { _codigoBus = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Ter
        /// </summary>
        public virtual char Ter
        {
            get { return _ter; }
            set { _ter = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Correspondencia
        /// </summary>
        public virtual Corresponsal Corresponsal
        {
            get { return _corresponsal; }
            set { _corresponsal = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Agente
        /// </summary>
        public virtual Agente Agente
        {
            get { return _agente; }
            set { _agente = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Asocaido
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Boletin de Concesion
        /// </summary>
        public virtual Boletin BoletinConcesion
        {
            get { return _boletinConcesion; }
            set { _boletinConcesion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Boletin de Publicacion
        /// </summary>
        public virtual Boletin BoletinPublicacion
        {
            get { return _boletinPublicacion; }
            set { _boletinPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Internacional
        /// </summary>
        public virtual Internacional Internacional
        {
            get { return _internacional; }
            set { _internacional = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Nacional
        /// </summary>
        public virtual Nacional Nacional
        {
            get { return _nacional; }
            set { _nacional = value; }
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
        /// Propiedad que asigna u obtiene el Pais
        /// </summary>
        public virtual Pais Pais
        {
            get { return _pais; }
            set { _pais = value; }
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
        /// Propiedad que asigna u obtiene el Poder
        /// </summary>
        public virtual Poder Poder
        {
            get { return _poder; }
            set { _poder = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Check de etiqueta
        /// </summary>
        public virtual bool BEtiqueta
        {
            get
            {
                if (this.Etiqueta.Equals("Si"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Etiqueta = "Si";
                else
                    this.Etiqueta = "No";
            }
        }

        /// <summary>
        ///Propiedad que asigna u obtiene el CPrioridad
        /// </summary>
        public virtual string CPrioridad
        {
            get { return _cPrioridad; }
            set { _cPrioridad = value; }
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

        /// <summary>
        /// Propiedad que asigna u obtiene el Anaqua que se va a realizar con este objeto
        /// sea CREATE, MODIFY o DELETE
        /// </summary>
        public virtual IList<Anaqua> Anaquas
        {
            get { return _anaquas; }
            set { _anaquas = value; }
        }

        #endregion
    }
}
