using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Auditoria
    {
        #region Atributos

        private int _id;
        private string _usuario;
        private DateTime _fecha;
        private string _operacion;
        private string _tabla;
        private int? _fk;

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna el id de la auditoria
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna el objeto del usuario q realiza la operacion
        /// </summary>
        public virtual string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        /// <summary>
        /// Propiedad que asigna de la fecha del evento
        /// </summary>
        public virtual DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        /// <summary>
        /// Propiedad que asigna la operacion del evento
        /// </summary>
        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        /// <summary>
        /// Propiedad que asigna la tabla donde se realiza la operacion
        /// </summary>
        public virtual string Tabla
        {
            get { return _tabla; }
            set { _tabla = value; }
        }

        /// <summary>
        /// Propiedad que asigna la clave foranea de la tabla que se le realizo la operacion
        /// </summary>
        public virtual int? Fk
        {
            get { return _fk; }
            set { _fk = value; }
        }

        #endregion
    }
}
