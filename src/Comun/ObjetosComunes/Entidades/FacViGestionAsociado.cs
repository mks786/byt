using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class ViGestionAsociado
    {
        #region "Atributos"
        private Asociado _id;
        private double _cantidad;
        private DateTime? _fechaultima;
        private string _moneda;
        private double _saldo;
        #endregion

        #region "Constructores"
        public ViGestionAsociado()
        {
        }

        public ViGestionAsociado(Asociado id, string moneda)
        {
            this._id = id;
            this._moneda = moneda;
        }
        // ''' <summary>
        // ''' Constructor que inicializa el Id de la ViGestionAsociado
        // ''' </summary>
        // ''' <param name="id">Id de la tasa</param>
        //Public Sub New(ByVal id As Integer)
        //    Me._id = id
        //End Sub

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            // var t = ((ChequeRecido)obj);
            var t = obj as ViGestionAsociado;
            if (t == null)
            {
                return false;
            }
            if ((Id.Id == (t.Id.Id)) && (Moneda == t.Moneda))
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

        #endregion

        #region "Propiedades"
        /// <summary>
        /// Propiedad que asigna u obtiene el id de la ViGestionAsociado
        /// </summary>
        public virtual Asociado Id
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
        public virtual double Cantidad
        {
            get
            {
                return this._cantidad;
            }
            set
            {
                this._cantidad = value;
            }
        }

        public virtual DateTime? FechaUltima
        {
            get
            {
                return this._fechaultima;
            }
            set
            {
                this._fechaultima = value;
            }
        }

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene
        // ''' </summary>
        public virtual string Moneda
        {
            get
            {
                return this._moneda;
            }
            set
            {
                this._moneda = value;
            }
        }

        public virtual double Saldo
        {
            get
            {
                return this._saldo;
            }
            set
            {
                this._saldo = value;
            }
        }
        #endregion

    }
}