using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class SolicitudSapi
    {
        #region Atributos

        private int _id;
        private MaterialSapi _material;
        private DateTime? _fechaSolicitud;
        private DateTime? _fechaEntrega;
        private DateTime? _fechaRecepcion;
        private Departamento _departamento;
        private string _solicitanteInic;
        private int _cantMaterialSol;
        //private string _statusMaterial;
        private string _tipoMovimiento;
        private string _operacion;
        //private IList<ListaDatosValores> _statusDeMateriales;
        private char? _materialSolicitado;
        private char? _materialEntregado;
        private char? _materialRecibido;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public SolicitudSapi() { }


        /// <summary>
        /// Constructor predeterminado que recibe el Id de la Solicitud
        /// </summary>
        /// <param name="id">Id de la Solicitud</param>
        public SolicitudSapi(int id)
        {
            this._id = id;
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
            var t = obj as SolicitudSapi;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (Material.Id == t.Material.Id))
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
        /// Propiedad que asigna u obtiene el Id de la Solicitud de Material Sapi
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene el Material Sapi solicitado
        /// </summary>
        public virtual MaterialSapi Material
        {
            get { return this._material; }
            set { this._material = value; }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene el Tipo de Movimiento de Material Sapi
        /// </summary>
        public virtual string TipoMovimiento
        {
            get { return this._tipoMovimiento; }
            set { this._tipoMovimiento = value; }
        }



        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de Solicitud de Material Sapi
        /// </summary>
        public virtual DateTime? FechaSolicitud
        {
            get { return this._fechaSolicitud; }
            set { this._fechaSolicitud = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de Entrega de Material Sapi (DESPACHO)
        /// </summary>
        public virtual DateTime? FechaEntrega
        {
            get { return this._fechaEntrega; }
            set { this._fechaEntrega = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha de Recepcion del Material Sapi solicitado
        /// </summary>
        public virtual DateTime? FechaRecepcion
        {
            get { return this._fechaRecepcion; }
            set { this._fechaRecepcion = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Departamento del Solicitante de Material Sapi
        /// </summary>
        public virtual Departamento Departamento
        {
            get { return this._departamento; }
            set { this._departamento = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene las Iniciales del Solicitante de Material Sapi
        /// </summary>
        public virtual string SolicitanteInic
        {
            get { return this._solicitanteInic; }
            set { this._solicitanteInic = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Cantidad de Material Sapi solicitada
        /// </summary>
        public virtual int CantMaterialSol
        {
            get { return this._cantMaterialSol; }
            set { this._cantMaterialSol = value; }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene el status SOLICITADO del Material de la Solicitud Sapi 
        /// </summary>
        public virtual char? MaterialSolicitado
        {
            get { return this._materialSolicitado; }
            set { this._materialSolicitado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el status Solicitado del Material en tipologia BOOLEAN
        /// </summary>
        public virtual bool BSolicitado
        {
            get
            {
                if (this.MaterialSolicitado.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.MaterialSolicitado = 'T';
                }
                else
                {
                    this.MaterialSolicitado = 'F';
                }
            }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene el status ENTREGADO del Material de la Solicitud Sapi 
        /// </summary>
        public virtual char? MaterialEntregado
        {
            get { return this._materialEntregado; }
            set { this._materialEntregado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el status Entregado del Material en tipologia BOOLEAN
        /// </summary>
        public virtual bool BEntregado
        {
            get
            {
                if (this.MaterialEntregado.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.MaterialEntregado = 'T';
                }
                else
                {
                    this.MaterialEntregado = 'F';
                }
            }
        }


        /// <summary>
        /// Propiedad que asigna y obtiene el status RECIBIDO del Material de la Solicitud Sapi 
        /// </summary>
        public virtual char? MaterialRecibido
        {
            get { return this._materialRecibido; }
            set { this._materialRecibido = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el status Recibido del Material en tipologia BOOLEAN
        /// </summary>
        public virtual bool BRecibido
        {
            get
            {
                if (this.MaterialRecibido.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.MaterialRecibido = 'T';
                }
                else
                {
                    this.MaterialRecibido = 'F';
                }
            }
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

        
        #endregion
    }
}
