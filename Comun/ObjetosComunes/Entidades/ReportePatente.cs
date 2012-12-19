using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class ReportePatente
    {

        #region Atributos

        private int _id;
        private int _codigoPatente;
        private string _inventores1;
        private string _inventores2;
        private string _nombrePatente1;
        private string _nombrePatente2;
        private string _resumen1;
        private string _resumen2;
        private string _omision1;
        private string _omision2;

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ReportePatente() { }

        /// <summary>
        /// Constructor que inicializa el Id del Anexo
        /// </summary>
        /// <param name="id">Id del Anexo</param>
        public ReportePatente(int id)
        {
            this._id = id;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del Anexo
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public virtual int CodigoPatente
        {
            get { return _codigoPatente; }
            set { _codigoPatente = value; }
        }

        public virtual string Inventores1
        {
            get { return _inventores1; }
            set { _inventores1 = value; }
        }

        public virtual string Inventores2
        {
            get { return _inventores2; }
            set { _inventores2 = value; }
        }

        public virtual string NombrePatente1
        {
            get { return _nombrePatente1; }
            set { _nombrePatente1 = value; }
        }

        public virtual string NombrePatente2
        {
            get { return _nombrePatente2; }
            set { _nombrePatente2 = value; }
        }

        public virtual string Resumen1
        {
            get { return _resumen1; }
            set { _resumen1 = value; }
        }

        public virtual string Resumen2
        {
            get { return _resumen2; }
            set { _resumen2 = value; }
        }

        public virtual string Omision1
        {
            get { return _omision1; }
            set { _omision1 = value; }
        }

        public virtual string Omision2
        {
            get { return _omision2; }
            set { _omision2 = value; }
        }

        #endregion

    }
}
