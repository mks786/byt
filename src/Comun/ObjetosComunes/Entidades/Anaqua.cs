using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Anaqua
    {
        #region Atributos

        private int _idMarca;
        private string _idAnaqua;
        private string _bkId;
        private string _colores;
        private string _comentario;
        private string _distingue;
        private string _usuario;
        private DateTime? _timeStamp;
        private string _operacion;
        private bool _insertarOModificar;

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
        public Anaqua(int idMarca)
        {
            this._idMarca = idMarca;
        }

        #endregion

        #region Propiedades

        //public override bool Equals(object obj)
        //{
        //    if ((this.IdAnaqua == ((Anaqua)obj).IdAnaqua) && (this.Marca.Id == ((Anaqua)obj).Marca.Id))
        //        return true;
        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        //public override string ToString()
        //{
        //    return base.ToString();
        //}

        public virtual int IdMarca
        {
            get { return _idMarca; }
            set { _idMarca = value; }
        }

        public virtual string IdAnaqua
        {
            get { return _idAnaqua; }
            set { _idAnaqua = value; }
        }

        public virtual string BkId
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

        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        public virtual bool InsertarOModificar
        {
            get { return _insertarOModificar; }
            set { _insertarOModificar = value; }
        }

        #endregion
    }
}
