using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacOperacionDetaTmProforma
    {
        #region "Atributos"

            private  string _id;
            private  Usuario _usuario;
            private  int? _detalle;
            private FacFacturaProforma _factura;
            private int? _codigo;
            private  FacServicio _servicio;
            private bool _seleccion;

        #endregion

        #region "Constructores"
        public FacOperacionDetaTmProforma()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacOperacionDetaTmProforma
        /// </summary>
        /// <param name="id">Id de FacOperacionDetaTmProforma</param>
        public FacOperacionDetaTmProforma(string id, Usuario usuario, int? detalle, FacServicio servicio, int? codigo)
        {
            this._id = id;
            this._usuario = usuario;
            this._detalle = detalle;
            this._servicio = servicio; 
            this._codigo = codigo;                
        }
        #endregion

        #region "Propiedades"
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            // var t = ((ChequeRecido)obj);
            var t = obj as FacOperacionDetaTmProforma;
            if (t == null)
            {
                return false;
            }
            if ((Id == (t.Id)) && (Usuario.Id == t.Usuario.Id) && (Detalle == t.Detalle) && (Servicio.Id == t.Servicio.Id) && (Codigo == t.Codigo))
            {
                //If (Doc_servicio = t.Doc_servicio) Then
                return true;
            }
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
        /// Propiedad que asigna u obtiene el id de la FacOperacionDetaTmProforma
        /// </summary>
        public virtual string Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public virtual Usuario Usuario
        {
            get
            {
                return this._usuario;
            }
            set
            {
                this._usuario = value;
            }
        }
        // ''' <summary>
        // ''' Propiedad que asigna u obtiene xbanco
        // ''' </summary>
        public virtual int? Detalle
        {
            get
            {
                return this._detalle;
            }
            set
            {
                this._detalle = value;
            }
        }

        public virtual FacFacturaProforma Factura
        {
            get
            {
                return this._factura;
            }
            set
            {
                this._factura = value;
            }
        }

        public virtual int? Codigo
        {
            get
            {
                return this._codigo;
            }
            set
            {
                this._codigo = value;
            }
        }

        public virtual FacServicio Servicio 
        {
            get
            {
                return this._servicio;
            }
            set
            {
                this._servicio = value;
            }
        }

        public virtual bool Seleccion
        {
            get
            {
                return this._seleccion;
            }
            set
            {
                this._seleccion = value;
            }
        }

        #endregion

    }
}