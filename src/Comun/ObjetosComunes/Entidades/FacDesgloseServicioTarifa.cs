using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable()]
    public class FacDesgloseServicioTarifa
    {
        #region Atributos

        private char _id;
        private FacServicio _servicio;
        private Moneda _moneda;
        private double? _monto;
        private Tarifa _tarifa;

        #endregion

        #region Constructores

        public FacDesgloseServicioTarifa()
        {
        }
        
        public FacDesgloseServicioTarifa(char id, FacServicio servicio, Moneda moneda)
        {
            this._id = id;
            this._servicio = servicio;
            this._moneda = moneda;
        }

        #endregion

        #region Propiedades

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var t = obj as FacDesgloseServicioTarifa;

            if (t == null)
            {
                return false;
            }

            if ((Id == (t.Id)) && (Servicio.Id == t.Servicio.Id) && (Moneda.Id == t.Moneda.Id))
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

        public virtual char Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        public virtual String BId
        {
            get
            {
                if (this.Id.Equals('G'))
                    return "Gastos";
                else
                    if (this.Id.Equals('H'))
                        return "Honorarios";
                    else
                        return "";
            }
        }


        public virtual FacServicio Servicio
        {
            get { return this._servicio; }
            set { this._servicio = value; }
        }

        public virtual Moneda Moneda
        {
            get { return this._moneda; }
            set { this._moneda = value; }
        }

        public virtual double? Monto
        {
            get { return this._monto; }
            set { this._monto = value; }
        }

        public virtual Tarifa Tarifa
        {
            get { return this._tarifa; }
            set { this._tarifa = value; }
        }

        #endregion
    }
}
