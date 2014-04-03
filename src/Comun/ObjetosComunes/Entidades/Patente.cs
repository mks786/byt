using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Patente
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
        private char _traduccion;
        private string _codigoExpediente;
        private string _ubicacion;
        private string _observacion;
        private char _rev;
        private Agente _agente;
        private Asociado _asociado;
        private Boletin _boletinConcesion;
        private Boletin _boletinPublicacion;
        private Interesado _interesado;
        private Pais _pais;
        private Servicio _servicio;
        private Poder _poder;
        private string _cPrioridad;
        //private InfoAdicional _infoAdicional;
        private string _orden;
        private DateTime? _fechaOrden;
        private char? _presentacion;
        private char _copia;
        private char _juramento;
        private char _cesion;
        private char _dibujo;
        private TipoEstado _tipoEstado;
        private Boletin _boletinOrdenPublicacion;
        private StatusWeb _statusWeb;
        private DateTime? _fechaBase;
        private DateTime? _fechaTermino;
        private string _observacion1;
        private char _ime;
        private string _resumen;
        private string _omision;
        private string _operacion;
        private InfoAdicional _infoAdicional;
        private IList<Inventor> _inventores;
        private IList<Fecha> _fechas;
        private IList<Anualidad> _anualidades;
        private IList<Operacion> _operaciones;
        private IList<InfoBolPatente> _infoBoles;
        private IList<Memoria> _memorias;
        private int _patenteMadre;
        private InteresadoPatente _interesadosDePatente;
        private string _prioridadPresentada;
        private string _origenPatente;


        #region Internacional

        private string _localidadPatente;
        private Asociado _asociadoInternacional;
        private Pais _paisInternacional;
        private int _codigoPatenteInternacional;
        private int _correlativoExpediente;
        private string _referenciaAsociadoInternacional;
        private string _referenciaInteresadoInternacional;

        #endregion


        #endregion


        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Patente()
        {            
        }

        /// <summary>
        /// Constructor que inicializa el id de la marca
        /// </summary>
        /// <param name="id">Id de la marca</param>
        public Patente(int id) : this()
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
        /// Propiedad que asigna u obtiene la Traduccion
        /// </summary>
        public virtual char Traduccion
        {
            get { return _traduccion; }
            set { _traduccion = value; }
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
        /// Propiedad que asigna u obtiene el Observacion
        /// </summary>
        public virtual string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
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
        /// Propiedad que asigna u obtiene el Check de Rev
        /// </summary>
        public virtual bool BRev
        {
            get
            {
                if (this.Rev.ToString().ToUpper().Equals("T"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Rev = 'T';
                else
                    this.Rev = 'F';
            }
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
        /// Propiedad que asigna u obtiene el Check de instrucciones de renovación
        /// </summary>
        public virtual bool BInstruccionesRenovacion
        {
            get
            {
                if (this.Rev.Equals('T'))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Rev = 'T';
                else
                    this.Rev = 'F';
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

        ///// <summary>
        ///// Propiedad que asigna u obtiene la Informacion Adicional 
        ///// </summary>
        //public virtual InfoAdicional InfoAdicional
        //{
        //    get { return _infoAdicional; }
        //    set { _infoAdicional = value; }
        //}

        /// <summary>
        /// Propiedad que asigna u obtiene la Omisión
        /// </summary>
        public virtual string Omision
        {
            get { return _omision; }
            set { _omision = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el resumen
        /// </summary>
        public virtual string Resumen
        {
            get { return _resumen; }
            set { _resumen = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el ime
        /// </summary>
        public virtual char Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Check de Ime
        /// </summary>
        public virtual bool BIme
        {
            get
            {
                if (this.Ime.ToString().ToUpper().Equals("T"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Ime = 'T';
                else
                    this.Ime = 'F';
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la observacion1
        /// </summary>
        public virtual string Observacion1
        {
            get { return _observacion1; }
            set { _observacion1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la fechatermino
        /// </summary>
        public virtual DateTime? FechaTermino
        {
            get { return _fechaTermino; }
            set { _fechaTermino = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la fechaBase
        /// </summary>
        public virtual DateTime? FechaBase
        {
            get { return _fechaBase; }
            set { _fechaBase = value; }
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
        /// Propiedad que asigna u obtiene el StatusWeb
        /// </summary>
        public virtual StatusWeb StatusWeb
        {
            get { return _statusWeb; }
            set { _statusWeb = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el boletinOrdenPublicacion
        /// </summary>
        public virtual Boletin BoletinOrdenPublicacion
        {
            get { return _boletinOrdenPublicacion; }
            set { _boletinOrdenPublicacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el dibujo
        /// </summary>
        public virtual char Dibujo
        {
            get { return _dibujo; }
            set { _dibujo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Check de etiqueta
        /// </summary>
        public virtual bool BDibujo
        {
            get
            {
                if (this.Dibujo.ToString().ToUpper().Equals("T"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Dibujo = 'T';
                else
                    this.Dibujo= 'F';
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la cesion
        /// </summary>
        public virtual char Cesion
        {
            get { return _cesion; }
            set { _cesion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el juramento
        /// </summary>
        public virtual char Juramento
        {
            get { return _juramento; }
            set { _juramento = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la copia
        /// </summary>
        public virtual char Copia
        {
            get { return _copia; }
            set { _copia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la presentacion
        /// </summary>
        public virtual char? Presentacion
        {
            get { return _presentacion; }
            set { _presentacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Orden
        /// </summary>
        public virtual string Orden
        {
            get { return _orden; }
            set { _orden = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la fechaOrden
        /// </summary>
        public virtual DateTime? FechaOrden
        {
            get { return _fechaOrden; }
            set { _fechaOrden = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la operacion de la patente
        /// </summary>
        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Informacion Adicional 
        /// </summary>
        public virtual InfoAdicional InfoAdicional
        {
            get { return _infoAdicional; }
            set { _infoAdicional = value; }
        }

        public virtual IList<Inventor> Inventores
        {
            get { return _inventores; }
            set { _inventores = value; }
        }

        public virtual IList<Fecha> Fechas
        {
            get { return _fechas; }
            set { _fechas = value; }
        }

        public virtual IList<Anualidad> Anualidades
        {
            get { return _anualidades; }
            set { _anualidades = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la lista de Operaciones
        /// </summary>
        public virtual IList<Operacion> Operaciones
        {
            get { return _operaciones; }
            set { _operaciones = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la lista de infoboles
        /// </summary>
        public virtual IList<InfoBolPatente> InfoBoles
        {
            get { return _infoBoles; }
            set { _infoBoles = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la memoria
        /// </summary>
        public virtual IList<Memoria> Memorias
        {
            get { return _memorias; }
            set { _memorias = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Patente Madre de una Patente
        /// </summary>
        public virtual int PatenteMadre
        {
            get { return _patenteMadre; }
            set { _patenteMadre = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene los Interesados vinculados a la Patente
        /// NOTA: PUEDEN SER MAS DE UNO
        /// </summary>
        public virtual InteresadoPatente InteresadosDePatente
        {
            get { return _interesadosDePatente; }
            set { _interesadosDePatente = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el status de presentacion de la prioridad
        /// </summary>
        public virtual string PrioridadPresentada
        {
            get { return _prioridadPresentada; }
            set { _prioridadPresentada = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Check de Prioridad Presentada
        /// </summary>
        public virtual bool BPrioridadPresentada
        {
            get
            {
                if (this.PrioridadPresentada.Equals("SI"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.PrioridadPresentada = "SI";
                else
                    this.PrioridadPresentada = "NO";
            }
        }



        #region Internacional

        public virtual string LocalidadPatente
        {
            get { return _localidadPatente; }
            set { _localidadPatente = value; }
        }

        public virtual Asociado AsociadoInternacional
        {
            get { return _asociadoInternacional; }
            set { _asociadoInternacional = value; }
        }

        public virtual Pais PaisInternacional
        {
            get { return _paisInternacional; }
            set { _paisInternacional = value; }
        }

        public virtual int CodigoPatenteInternacional
        {
            get { return _codigoPatenteInternacional; }
            set { _codigoPatenteInternacional = value; }
        }

        public virtual int CorrelativoExpediente
        {
            get { return _correlativoExpediente; }
            set { _correlativoExpediente = value; }
        }

        public virtual string ReferenciaAsociadoInternacional
        {
            get { return _referenciaAsociadoInternacional; }
            set { _referenciaAsociadoInternacional = value; }
        }

        public virtual string ReferenciaInteresadoInternacional
        {
            get { return _referenciaInteresadoInternacional; }
            set { _referenciaInteresadoInternacional = value; }
        }

        #endregion


        /// <summary>
        /// Propiedad que asigna u obtiene el Origen de la Patente
        /// </summary>
        public virtual string OrigenPatente
        {
            get { return _origenPatente; }
            set { _origenPatente = value; }
        }


        #endregion

    }
}
