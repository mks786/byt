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
        private Usuario _usuario;
        private DateTime? _timeStamp;
        private string _operacion;
        private bool _insertarOModificar;
        private Marca _marca;

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


        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de colores
        /// </summary>
        public virtual bool BColores
        {
            get
            {
                if (this.Colores != null && this.Colores.Equals("T"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.Colores = "T";
                else
                    this.Colores = "F";
            }
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

        public virtual Usuario Usuario
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

        public virtual Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        #endregion
    }
}
