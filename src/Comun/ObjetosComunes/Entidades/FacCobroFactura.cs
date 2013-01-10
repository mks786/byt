using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacCobroFactura
    {
        #region "Atributos"

           private FacCobro _id;
           private int? _factura;
           private double _bono;
           private double _bonobf;
      
        #endregion

        #region "Constructores"
        public FacCobroFactura()
        {
        }
        // ''' <summary>
        // ''' Constructor que inicializa el Id de la FacCobroFactura
        // ''' </summary>
        // ''' <param name="id">Id de la tasa</param>
        public FacCobroFactura(FacCobro id,int? factura)
        {
            this._id = id;
            this._factura = factura;
        }

        // para llave compuesta
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            // var t = ((FacCobroFactura)obj);
            var t = obj as FacCobroFactura;
            if (t == null)
            {
                return false;
            }
            if ((Id.Id == (t.Id.Id)) && (Factura== t.Factura))
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



        public virtual FacCobro Id
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

        public virtual int? Factura
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

        public virtual double Bono
        {
            get
            {
                return this._bono;
            }
            set
            {
                this._bono = value;
            }
        }

        public virtual double BonoBf
        {
            get
            {
                return this._bonobf;
            }
            set
            {
                this._bonobf = value;
            }
        }
        #endregion

    }
}