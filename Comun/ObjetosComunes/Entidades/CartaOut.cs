using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CartaOut
    {
        #region Atributos

        private string _id;
        private string _from;
        private string _conCopia;
        private string _conCopiaOCulta;
        private string _asunto;
        private DateTime? _fechaCorreo;
        private DateTime? _fechaIngreso;
        private string _fecha;
        private string _cuerpo;
        private char _status;
        private int _nRelacion;
        private char _tipoEmail;
        private int _asociado;
        private string _clave;
        private string _attLis;
        private int _nSec;
        private string _fromOrganizacion;
        private string _toOrganizacion;
        private string _conCopiaOrganizacion;
        private string _subjectOrganizacion;
        private string _departamento;
        private string _iniciales;
        private string _persona;
        private string _fromDetalle;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public CartaOut() { }

        /// <summary>
        /// Constructor que inicializa el Id de la carta
        /// </summary>
        /// <param name="id">Id de la carta</param>
        public CartaOut(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el ID de la CartaOut
        /// </summary>
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el From de la CartaOut
        /// </summary>
        public virtual string From
        {
            get { return _from; }
            set { _from = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el ConCopia de la CartaOut
        /// </summary>
        public virtual string ConCopia
        {
            get { return _conCopia; }
            set { _conCopia = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el ConCopiaOCulta de la CartaOut
        /// </summary>
        public virtual string ConCopiaOCulta
        {
            get { return _conCopiaOCulta; }
            set { _conCopiaOCulta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Asunto de la CartaOut
        /// </summary>
        public virtual string Asunto
        {
            get { return _asunto; }
            set { _asunto = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el FechaCorreo de la CartaOut
        /// </summary>
        public virtual DateTime? FechaCorreo
        {
            get { return _fechaCorreo; }
            set { _fechaCorreo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el FechaIngreso de la CartaOut
        /// </summary>
        public virtual DateTime? FechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Fecha de la CartaOut
        /// </summary>
        public virtual string Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Cuerpo de la CartaOut
        /// </summary>
        public virtual string Cuerpo
        {
            get { return _cuerpo; }
            set { _cuerpo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Status de la CartaOut
        /// </summary>
        public virtual char Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el NRelacion de la CartaOut
        /// </summary>
        public virtual int NRelacion
        {
            get { return _nRelacion; }
            set { _nRelacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el TipoEmail de la CartaOut
        /// </summary>
        public virtual char TipoEmail
        {
            get { return _tipoEmail; }
            set { _tipoEmail = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Asociado de la CartaOut
        /// </summary>
        public virtual int Asociado
        {
            get { return _asociado; }
            set { _asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Clave de la CartaOut
        /// </summary>
        public virtual string Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el AttLis de la CartaOut
        /// </summary>
        public virtual string AttLis
        {
            get { return _attLis; }
            set { _attLis = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el NSec de la CartaOut
        /// </summary>
        public virtual int NSec
        {
            get { return _nSec; }
            set { _nSec = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el FromOrganizacion de la CartaOut
        /// </summary>
        public virtual string FromOrganizacion
        {
            get { return _fromOrganizacion; }
            set { _fromOrganizacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el ToOrganizacion de la CartaOut
        /// </summary>
        public virtual string ToOrganizacion
        {
            get { return _toOrganizacion; }
            set { _toOrganizacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el ConCopiaOrganizacion de la CartaOut
        /// </summary>
        public virtual string ConCopiaOrganizacion
        {
            get { return _conCopiaOrganizacion; }
            set { _conCopiaOrganizacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el SubjectOrganizacion de la CartaOut
        /// </summary>
        public virtual string SubjectOrganizacion
        {
            get { return _subjectOrganizacion; }
            set { _subjectOrganizacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Departamento de la CartaOut
        /// </summary>
        public virtual string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Iniciales de la CartaOut
        /// </summary>
        public virtual string Iniciales
        {
            get { return _iniciales; }
            set { _iniciales = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Persona de la CartaOut
        /// </summary>
        public virtual string Persona
        {
            get { return _persona; }
            set { _persona = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el FromDetalle de la CartaOut
        /// </summary>
        public virtual string FromDetalle
        {
            get { return _fromDetalle; }
            set { _fromDetalle = value; }
        }

        #endregion
    }
}
