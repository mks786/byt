using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacGestion
    {
        #region "Atributos"

            //private  FacFacturaProforma _id;
        private int? _id;        
        private DateTime? _FechaGestion;
        private string _Medio;
        private Asociado _Asociado;
        private string _ConceptoGestion;//cambiar a concepto_gestion
        private string _Observacion;//cambiar a concepto_gestion
        private int? _CodigoResp;
        private TipoCliente _TipoAsociado;
        private DateTime? _FechaIngreso;
        private string _Inicial;
        private int? _Respuesta;//cambiar a respuesta
        private string _ConceptoGestion2;//cambiar a concepto_gestion
        private string _Xdestalleres;
        private string _Ruta;
        private DateTime? _FechaModificacion;

        private bool _seleccion;

        private int? _Status;

        #endregion

        #region "Constructores"
        public FacGestion()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacOperacionProforma
        /// </summary>
        /// <param name="id">Id de FacOperacionProforma</param>
        public FacGestion(int?  id,Asociado asociado)
        {
            this._id = id;
            this._Asociado = asociado;      
        }
        #endregion

        #region "Propiedades"
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var t = obj as FacGestion;
            if (t == null)
            {
                return false;
            }
            if ((Id == (t.Id)) && (Asociado.Id == t.Asociado.Id))
            {
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
        /// Propiedad que asigna u obtiene el id de la FacOperacionProforma
        /// </summary>
        public virtual int?  Id
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


        public virtual DateTime? FechaGestion
        {
            get
            {
                return this._FechaGestion;
            }
            set
            {
                this._FechaGestion = value;
            }
        }

        public virtual String Medio
        {
            get
            {
                return this._Medio;
            }
            set
            {
                this._Medio = value;
            }
        }

        public virtual Asociado Asociado
        {
            get
            {
                return this._Asociado;
            }
            set
            {
                this._Asociado = value;
            }
        }

        public virtual string ConceptoGestion
        {
            get
            {
                return this._ConceptoGestion;
            }
            set
            {
                this._ConceptoGestion = value;
            }
        }
        // ''' <summary>
        // ''' Propiedad que asigna u obtiene xbanco
        // ''' </summary>
        public virtual string Observacion
        {
            get
            {
                return this._Observacion;
            }
            set
            {
                this._Observacion = value;
            }
        }

        public virtual int? CodigoResp
        {
            get
            {
                return this._CodigoResp;
            }
            set
            {
                this._CodigoResp = value;
            }
        }

        public virtual TipoCliente TipoAsociado
        {
            get
            {
                return this._TipoAsociado;
            }
            set
            {
                this._TipoAsociado = value;
            }
        }

        public virtual DateTime? FechaIngreso
        {
            get
            {
                return this._FechaIngreso;
            }
            set
            {
                this._FechaIngreso = value;
            }
        }

        public virtual string Inicial
        {
            get
            {
                return this._Inicial;
            }
            set
            {
                this._Inicial = value;
            }
        }

        public virtual int? Respuesta
        {
            get
            {
                return this._Respuesta;
            }
            set
            {
                this._Respuesta = value;
            }
        }

        public virtual string ConceptoGestion2
        {
            get
            {
                return this._ConceptoGestion2;
            }
            set
            {
                this._ConceptoGestion2 = value;
            }
        }

        public virtual string Xdestalleres
        {
            get
            {
                return this._Xdestalleres;
            }
            set
            {
                this._Xdestalleres = value;
            }
        }

        public virtual string Ruta
        {
            get
            {
                return this._Ruta;
            }
            set
            {
                this._Ruta = value;
            }
        }

        public virtual DateTime? FechaModificacion
        {
            get
            {
                return this._FechaModificacion;
            }
            set
            {
                this._FechaModificacion = value;
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

        public virtual int? Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        #endregion

    }
}