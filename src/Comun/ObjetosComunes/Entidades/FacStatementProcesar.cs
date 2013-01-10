using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacStatementProcesar
    {
        #region "Atributos"

            private  int? _id;            
            private DateTime? _fechafactura; 
            private  int? _cobro;
            private  DateTime? _fechacobro;            

            private bool _seleccion;
            private int? _status;
            private int? _p_mip;
            private int? _bst;
            private DateTime? _fechadesde;
            private DateTime? _fechahasta;


        #endregion

        #region "Constructores"
        public FacStatementProcesar()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la FacStatementProcesar
        /// </summary>
        /// <param name="id">Id de FacStatementProcesar</param>
        public FacStatementProcesar(int? id)
        {
            this._id = id;
        }
        #endregion

        #region "Propiedades"

        /// <summary>
        /// Propiedad que asigna u obtiene el id de la FacStatementProcesar
        /// </summary>
        public virtual int? Id
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

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene xbanco
        // ''' </summary>
        public virtual DateTime? FechaFactura
        {
            get
            {
                return this._fechafactura;
            }
            set
            {
                this._fechafactura = value;
            }
        }

        public virtual int? Cobro
        {
            get
            {
                return this._cobro;
            }
            set
            {
                this._cobro = value;
            }
        }

        public virtual DateTime? FechaCobro
        {
            get
            {
                return this._fechacobro;
            }
            set
            {
                this._fechacobro = value;
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
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public virtual int? P_mip
        {
            get
            {
                return this._p_mip;
            }
            set
            {
                this._p_mip = value;
            }
        }
        public virtual int? Bst
        {
            get
            {
                return this._bst;
            }
            set
            {
                this._bst = value;
            }
        }

        public virtual DateTime? FechaDesde
        {
            get
            {
                return this._fechadesde;
            }
            set
            {
                this._fechadesde = value;
            }
        }

        public virtual DateTime? FechaHasta
        {
            get
            {
                return this._fechahasta;
            }
            set
            {
                this._fechahasta = value;
            }
        }

        #endregion

    }
}