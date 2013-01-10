using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable()]
    public class FacServicio
    {
        #region "Atributos"

        private string _id;
        private string _cod_cont;
        private string _detalle_esp;
        private string _detalle_ing;
        private char _itipo;
        private char _local;
        private char _itidoc;
        private char _itraduc;
        private char _anual;
        private string _detalles_esp;
        private string _detalles_ing;
        private string _codmult;
        private string _xreferencia;
        private char _imult;
        private char _imodpr;
        private char _recursos;
        private char _material;
        private char _aimpuesto;
        #endregion
        private char _desg;

        #region "Constructores"
        public FacServicio()
        {
            this._itidoc = 'F';
            this._itraduc = 'F';
            this._anual = 'F';
            this._imult = 'F';
            this._imodpr = 'F';
            this._recursos = 'F';
            this.Material = 'F';
            this._aimpuesto = 'F';
            this._desg = 'F';
        }
        /// <summary>
        /// Constructor que inicializa el Id de la Servicio
        /// </summary>
        /// <param name="id">Id de la servicioa</param>
        public FacServicio(string id)
        {
            this._id = id;
        }
        #endregion

        #region "Propiedades"
        /// <summary>
        /// Propiedad que asigna u obtiene el id de la documentacion Marca
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        public virtual string Cod_Cont
        {
            get { return this._cod_cont; }
            set { this._cod_cont = value; }
        }


        public virtual string Detalle_Esp
        {
            get { return this._detalle_esp; }
            set { this._detalle_esp = value; }
        }


        public virtual string Detalle_Ing
        {
            get { return this._detalle_ing; }
            set { this._detalle_ing = value; }
        }

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


        public virtual char Local
        {
            get { return this._local; }
            set { this._local = value; }
        }

        public virtual String BLocal
        {
            get
            {
                if (this.Local.Equals('N'))
                    return "Nacional";
                else
                    if (this.Local.Equals('I'))
                        return "Internacional";
                    else
                                return "";
            }
        }

        public virtual char Itidoc
        {
            get { return this._itidoc; }
            set { this._itidoc = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de Itidoc
        /// </summary>
        public virtual bool BItidoc
        {
            get
            {
                if (this.Itidoc.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.Itidoc = 'T';
                }
                else
                {
                    this.Itidoc = 'F';
                }
            }
        }


        public virtual char Itraduc
        {
            get { return this._itraduc; }
            set { this._itraduc = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de Itraduc
        /// </summary>
        public virtual bool BItraduc
        {
            get
            {
                if (this.Itraduc.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.Itraduc = 'T';
                }
                else
                {
                    this.Itraduc = 'F';
                }
            }
        }


        public virtual char Anual
        {
            get { return this._anual; }
            set { this._anual = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de Anual
        /// </summary>
        public virtual bool BAnual
        {
            get
            {
                if (this.Anual.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.Anual = 'T';
                }
                else
                {
                    this.Anual = 'F';
                }
            }
        }


        public virtual string Detalles_Esp
        {
            get { return this._detalles_esp; }
            set { this._detalles_esp = value; }
        }


        public virtual string Detalles_Ing
        {
            get { return this._detalles_ing; }
            set { this._detalles_ing = value; }
        }


        public virtual string Codmult
        {
            get { return this._codmult; }
            set { this._codmult = value; }
        }


        public virtual string Xreferencia
        {
            get { return this._xreferencia; }
            set { this._xreferencia = value; }
        }


        public virtual char Imult
        {
            get { return this._imult; }
            set { this._imult = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de Imult
        /// </summary>
        public virtual bool BImult
        {
            get
            {
                if (this.Imult.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.Imult = 'T';
                }
                else
                {
                    this.Imult = 'F';
                }
            }
        }

        public virtual char Imodpr
        {
            get { return this._imodpr; }
            set { this._imodpr = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de Imodpr
        /// </summary>
        public virtual bool BImodpr
        {
            get
            {
                if (this.Imodpr.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.Imodpr = 'T';
                }
                else
                {
                    this.Imodpr = 'F';
                }
            }
        }


        public virtual char Recursos
        {
            get { return this._recursos; }
            set { this._recursos = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de Recursos
        /// </summary>
        public virtual bool BRecursos
        {
            get
            {
                if (this.Recursos.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.Recursos = 'T';
                }
                else
                {
                    this.Recursos = 'F';
                }
            }
        }


        public virtual char Material
        {
            get { return this._material; }
            set { this._material = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de Material
        /// </summary>
        public virtual bool BMaterial
        {
            get
            {
                if (this.Material.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.Material = 'T';
                }
                else
                {
                    this.Material = 'F';
                }
            }
        }

        public virtual char Aimpuesto
        {
            get { return this._aimpuesto; }
            set { this._aimpuesto = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de Aimpuesto
        /// </summary>
        public virtual bool BAimpuesto
        {
            get
            {
                if (this.Aimpuesto.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.Aimpuesto = 'T';
                }
                else
                {
                    this.Aimpuesto = 'F';
                }
            }
        }


        public virtual char Desg
        {
            get { return this._desg; }
            set { this._desg = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el booleano de Desg
        /// </summary>
        public virtual bool BDesg
        {
            get
            {
                if (this.Desg.Equals('T'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.Desg = 'T';
                }
                else
                {
                    this.Desg = 'F';
                }
            }
        }

        #endregion

    }
}