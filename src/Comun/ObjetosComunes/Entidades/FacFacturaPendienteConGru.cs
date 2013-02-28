using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FacFacturaPendienteConGru
    {
        #region "Atributos"
          
           private int _id;
           private Moneda _Moneda;
           private int? _Dias;
           private double? _Saldo;
           private double? _SaldoBf;


           private int? _accion;

        #endregion

        #region "Constructores"
        public FacFacturaPendienteConGru()
        {
        }
        // ''' <summary>
        // ''' Constructor que inicializa el Id de la FacFacturaPendienteConGru
        // ''' </summary>
        // ''' <param name="id">Id de la tasa</param>
        public FacFacturaPendienteConGru(int id)
        {
            this._id = id;
        }

        public virtual int Id
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
        public virtual Moneda Moneda
        {
            get
            {
                return this._Moneda;
            }
            set
            {
                this._Moneda = value;
            }
        }

        public virtual int? Dias
        {
            get
            {
                return this._Dias;
            }
            set
            {
                this._Dias = value;
            }
        }

        public virtual double? Saldo
        {
            get
            {
                return this._Saldo;
            }
            set
            {
                this._Saldo = value;
            }
        }

        public virtual double? SaldoBf
        {
            get
            {
                return this._SaldoBf;
            }
            set
            {
                this._SaldoBf = value;
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