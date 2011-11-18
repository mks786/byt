using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Carta
    {
        #region Atributos

        private int _id;
        private DateTime _fecha;
        private string _medio;
        private Asociado _asociado;
        private string _descripcionDepartamento;
        private string _persona;
        private string _referencia;
        private string _receptor;
        private string _descripcionResumen;
        private string _anexoMedio;
        private string _anexo;
        private string _tracking;
        private DateTime? _anexoFecha;
        private string _anexoTracking;
        private char _acuse;
        private char _acuseEnvio;
        private string _medioAcuse;
        private DateTime? _fechaEnvioAcuse;
        private Resumen _resumen;
        private DateTime? _fechaAlt;
        private double? _salida;
        private string _detalleResumen;
        private string _iniciales;
        private Departamento _departamento;
        private DateTime? _fechaL;
        private char _iRev;
        private DateTime? _fechaReal;
        private DateTime? _fechaD;
        private IList<Justificacion> _justificaciones;
        private IList<Contacto> _contactos;
        private IList<Anexo> _anexos;
        private IList<Asignacion> _asignaciones;
        private IList<Anexo> _anexosConfirmacion;
        private string _operacion;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Carta() { }

        /// <summary>
        /// Constructor que inicializa el Id de la carta
        /// </summary>
        /// <param name="id">Id de la carta</param>
        public Carta(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la fecha
        /// </summary>
        public virtual DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el medio
        /// </summary>
        public virtual string Medio
        {
            get { return _medio; }
            set { _medio = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el asociado
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la descripcion del departamento
        /// </summary>
        public virtual string DescripcionDepartamento
        {
            get { return _descripcionDepartamento; }
            set { _descripcionDepartamento = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la persona
        /// </summary>
        public virtual string Persona
        {
            get { return _persona; }
            set { _persona = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la referencia
        /// </summary>
        public virtual string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }

        public virtual string Receptor
        {
            get { return _receptor; }
            set { _receptor = value; }
        }

        public virtual string DescripcionResumen
        {
            get { return _descripcionResumen; }
            set { _descripcionResumen = value; }
        }

        public virtual string Tracking
        {
            get { return _tracking; }
            set { _tracking = value; }
        }

        public virtual string Anexo
        {
            get { return _anexo; }
            set { _anexo = value; }
        }

        public virtual string AnexoMedio
        {
            get { return _anexoMedio; }
            set { _anexoMedio = value; }
        }

        public virtual DateTime? AnexoFecha
        {
            get { return _anexoFecha; }
            set { _anexoFecha = value; }
        }

        public virtual string AnexoTracking
        {
            get { return _anexoTracking; }
            set { _anexoTracking = value; }
        }

        public virtual char Acuse
        {
            get { return _acuse; }
            set { _acuse = value; }
        }

        public virtual char AcuseEnvio
        {
            get { return _acuseEnvio; }
            set { _acuseEnvio = value; }
        }

        public virtual string MedioAcuse
        {
            get { return _medioAcuse; }
            set { _medioAcuse = value; }
        }

        public virtual DateTime? FechaEnvioAcuse
        {
            get { return _fechaEnvioAcuse; }
            set { _fechaEnvioAcuse = value; }
        }

        public virtual Resumen Resumen
        {
            get { return _resumen; }
            set { _resumen = value; }
        }

        public virtual DateTime? FechaAlt
        {
            get { return _fechaAlt; }
            set { _fechaAlt = value; }
        }

        public virtual double? Salida
        {
            get { return _salida; }
            set { _salida = value; }
        }

        public virtual string DetalleResumen
        {
            get { return _detalleResumen; }
            set { _detalleResumen = value; }
        }

        public virtual string Iniciales
        {
            get { return _iniciales; }
            set { _iniciales = value; }
        }

        public virtual Departamento Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }

        public virtual DateTime? FechaL
        {
            get { return _fechaL; }
            set { _fechaL = value; }
        }

        public virtual char IRev
        {
            get { return _iRev; }
            set { _iRev = value; }
        }

        public virtual DateTime? FechaReal
        {
            get { return _fechaReal; }
            set { _fechaReal = value; }
        }

        public virtual DateTime? FechaD
        {
            get { return _fechaD; }
            set { _fechaD = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la lista de justificaciones
        /// de un asociado
        /// </summary>
        public virtual IList<Justificacion> Justificaciones
        {
            get { return _justificaciones; }
            set { _justificaciones = value; }
        }

        public virtual IList<Contacto> Contactos
        {
            get { return _contactos; }
            set { _contactos = value; }
        }

        public virtual IList<Anexo> Anexos
        {
            get { return _anexos; }
            set { _anexos = value; }
        }

        public virtual IList<Asignacion> Asignaciones
        {
            get { return _asignaciones; }
            set { _asignaciones = value; }
        }

        public virtual IList<Anexo> AnexosConfirmacion
        {
            get { return _anexosConfirmacion; }
            set { _anexosConfirmacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la operacion
        /// </summary>
        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }
        #endregion
    }
}
