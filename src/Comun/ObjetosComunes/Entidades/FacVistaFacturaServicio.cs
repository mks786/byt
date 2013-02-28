using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacVistaFacturaServicio
    {
        #region "Atributos"
          
           private int? _id;
           private string _Inicial;
           private string _CodigoServicio;
           private string _Referencia;
           private int? _Factura;
           private DateTime? _FechaFactura;
           private int? _Proforma;
           private string _Tipo;


           private int? _accion;

        #endregion

        #region "Constructores"
        public FacVistaFacturaServicio()
        {
        }
        // ''' <summary>
        // ''' Constructor que inicializa el Id de la FacVistaFacturaServicio
        // ''' </summary>
        // ''' <param name="id">Id de la tasa</param>
        public FacVistaFacturaServicio(int? id)
        {
            this._id = id;
        }

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
        // ''' Propiedad que asigna u obtiene
        // ''' </summary>
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

        public virtual string CodigoServicio
        {
            get
            {
                return this._CodigoServicio;
            }
            set
            {
                this._CodigoServicio = value;
            }
        }

        public virtual string Referencia
        {
            get
            {
                return this._Referencia;
            }
            set
            {
                this._Referencia = value;
            }
        }

        public virtual int? Factura
        {
            get
            {
                return this._Factura;
            }
            set
            {
                this._Factura = value;
            }
        }

        public virtual DateTime? FechaFactura
        {
            get
            {
                return this._FechaFactura;
            }
            set
            {
                this._FechaFactura = value;
            }
        }

        public virtual int? Proforma
        {
            get
            {
                return this._Proforma;
            }
            set
            {
                this._Proforma = value;
            }
        }

        public virtual string Tipo
        {
            get
            {
                return this._Tipo;
            }
            set
            {
                this._Tipo = value;
            }
        }
                
        public virtual int? Accion
        {
            //1 = modificar sin el boton regresar
            //2 = no modificar
            //3= modificar con el boton regresar
            get
            {
                return this._accion;
            }
            set
            {
                this._accion = value;
            }
        }
        #endregion

    }
}