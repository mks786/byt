using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class DatosTransferencia
    {
        #region Atributos

        private int _id;
        private Asociado _asociado;
        private string _aba;
        private string _bancoBenef;
        private string _bancoInt;
        private string _beneficiario;
        private string _cuenta;
        private string _direccion;
        private string _swif;
        private string _swiftInt;
        private string _iban;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public DatosTransferencia() { }

        /// <summary>
        /// Constructor que inicializa el condigo de los datos de transferencia
        /// 
        /// </summary>
        /// <param name="codigo">Codigo del status</param>
        public DatosTransferencia(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        public override bool Equals(object obj)
        {
            if ((this.Id == ((DatosTransferencia)obj).Id) && (this.Asociado.Id == ((DatosTransferencia)obj).Asociado.Id))
                return true;
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
        /// Propiedad que asigna u obtiene el código de los datos de transferencia
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el asociado que posee los datos de transferencia
        /// </summary>
        public virtual Asociado Asociado
        {
            get { return this._asociado; }
            set { this._asociado = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el aba
        /// </summary>
        public virtual string Aba
        {
            get { return _aba; }
            set { _aba = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el banco beneficiario
        /// </summary>
        public virtual string BancoBenef
        {
            get { return _bancoBenef; }
            set { _bancoBenef = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el banco intermediario
        /// </summary>
        public virtual string BancoInt
        {
            get { return _bancoInt; }
            set { _bancoInt = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el beneficiario
        /// </summary>
        public virtual string Beneficiario
        {
            get { return _beneficiario; }
            set { _beneficiario = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la cuenta 
        /// </summary>
        public virtual string Cuenta
        {
            get { return _cuenta; }
            set { _cuenta = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la direccion
        /// </summary>
        public virtual string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el swif
        /// </summary>
        public virtual string Swif
        {
            get { return _swif; }
            set { _swif = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el swift del intermediario
        /// </summary>
        public virtual string SwiftInt
        {
            get { return _swiftInt; }
            set { _swiftInt = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Iban del intermediario
        /// </summary>
        public virtual string Iban
        {
            get { return _iban; }
            set { _iban = value; }
        }

        #endregion
    }
}
