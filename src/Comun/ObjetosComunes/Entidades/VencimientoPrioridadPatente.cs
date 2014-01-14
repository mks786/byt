using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class VencimientoPrioridadPatente
    {
        #region Atributos

        private int _id;
        private DateTime _fechaSolicitud;
        private string _fechaVencimiento;
        private string _fechaRecordatorio;
        private int _vencimientoDias;
        private Patente _patente;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public VencimientoPrioridadPatente()
        {
            
        }


        /// <summary>
        /// Constructor que inicializa el id de la entidad VencimientoPrioridadPatente
        /// </summary>
        /// <param name="id">Id de la patente</param>
        public VencimientoPrioridadPatente(int id)
            : this()
        {
            this._id = id;

        }

        #endregion

        #region Propiedades

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        public virtual DateTime FechaSolicitud
        {
            get { return _fechaSolicitud; }
            set { _fechaSolicitud = value; }
        }


        public virtual string FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set { _fechaVencimiento = value; }
        }
        

        public virtual string FechaRecordatorio
        {
            get { return _fechaRecordatorio; }
            set { _fechaRecordatorio = value; }
        }


        public virtual int VencimientoDias
        {
            get { return _vencimientoDias; }
            set { _vencimientoDias = value; }
        }


        public virtual Patente Patente
        {
            get { return _patente; }
            set { _patente = value; }
        }
        #endregion
    }
}
