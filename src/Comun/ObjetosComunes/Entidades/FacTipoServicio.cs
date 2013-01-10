using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable()]
    public class FacTipoServicio
    {
        #region "Atributos"

        private string _id;
        private char _itipo;
        private int _interno;
        #endregion
        

        #region "Constructores"
        public FacTipoServicio()
        {
        }
        /// <summary>
        /// Constructor que inicializa el Id de la DetalleEnvio
        /// </summary>
        /// <param name="id">Id de la tasa</param>
        public FacTipoServicio(string id)
        {
            this._id = id;
        }
        #endregion

        #region "Propiedades"
        /// <summary>
        /// Propiedad que asigna u obtiene el id de la DetalleEnvio
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        // ''' <summary>
        // ''' Propiedad que asigna u obtiene docmuento español
        // ''' </summary>
        public virtual char Itipo
        {
            get { return this._itipo; }
            set { this._itipo = value; }
        }

        public virtual String BItipo
        {
            get
            {
                if (this.Itipo.Equals('M'))
                    return "Marca";
                else
                    if (this.Itipo.Equals('P'))
                        return "Patentes";
                    else
                        if (this.Itipo.Equals('C'))
                            return "Cantidades";
                        else
                            if (this.Itipo.Equals('E'))
                                return "Externa";
                            else
                                return "";
            }
        }

        public virtual int Interno
        {
            get { return this._interno; }
            set { this._interno = value; }
        }

        #endregion

    }
}