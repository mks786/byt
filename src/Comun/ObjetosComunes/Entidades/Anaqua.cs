using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Anaqua
    {
        #region Atributos

        private Marca _marca;
        private int _idAnaqua;
        private int _bkId;
        private string _colores;
        private string _comentario;
        private string _distingue;
        private string _usuario;
        private DateTime? _timeStamp;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public Anaqua() { }

        /// <summary>
        /// Constructor que inicializa el id del Concepto
        /// </summary>
        /// <param name="idMarca">Id del Concepto</param>
        public Anaqua(Marca marca)
        {
            this._marca = marca;
        }

        #endregion

        #region Propiedades

        public override bool Equals(object obj)
        {
            if ((this.IdAnaqua == ((Anaqua)obj).IdAnaqua) && (this.Marca.Id == ((Anaqua)obj).Marca.Id))
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

        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        public virtual int IdAnaqua
        {
            get { return _idAnaqua; }
            set { _idAnaqua = value; }
        }

        public virtual int BkId
        {
            get { return _bkId; }
            set { _bkId = value; }
        }

        public virtual string Colores
        {
            get { return _colores; }
            set { _colores = value; }
        }
        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        public virtual string Distingue
        {
            get { return _distingue; }
            set { _distingue = value; }
        }

        public virtual string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public virtual DateTime? TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }
        #endregion
    }
}
