using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class PresentacionSapiDetalle
    {
        #region Atributos

        private PresentacionSapi _presentacion_Enc;
        private MaterialSapi _material;
        private string _codExpediente;
        private string _statusDocumento;
        private string _receptorMatPresent;
        private DateTime? _fechaRecep_Gestor1;
        private string _presentadorAnteSAPI;
        private DateTime? _fechaPres_Gestor2;
        private string _receptorAnteSAPI;
        private DateTime? _fechaRecep_Gestor3;
        private string _inicDptoReceptor;
        private DateTime? _fechaRecep_Dpto;
        private char? _recibeDocumento;
        private char? _presentadoASapi;
        private char? _recibioDeSapi;
        private char? _recibioDpto;
        
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public PresentacionSapiDetalle() { }

        /// <summary>
        /// Constructor que inicializa el id del Concepto
        /// </summary>
        /// <param name="id">Id del Concepto</param>
        public PresentacionSapiDetalle(int id)
        {
            this.Presentacion_Enc.Id = id;
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as PresentacionSapiDetalle;
            if (t == null)
                return false;
            if ((Presentacion_Enc.Id == (t.Presentacion_Enc.Id)) && (Material.Id == (t.Material.Id)))
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

        #endregion
              

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el Id del Detalle de la Presentacion Sapi
        /// </summary>
        public virtual int Id
        {
            get { return _presentacion_Enc.Id; }
            set { _presentacion_Enc.Id = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Encabezado de la Solicitud de Presentacion Sapi
        /// </summary>
        public virtual PresentacionSapi Presentacion_Enc
        {
            get { return _presentacion_Enc; }
            set { _presentacion_Enc = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Documento (Material Sapi) de la Solicitud de Presentacion Sapi
        /// </summary>
        public virtual MaterialSapi Material
        {
            get { return _material; }
            set { _material = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Codigo del Expediente asociado al Documento (Material Sapi) de la Solicitud de Presentacion Sapi
        /// </summary>
        public virtual string CodExpediente
        {
            get { return _codExpediente; }
            set { _codExpediente = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Status del Documento (Material Sapi) de la Solicitud de Presentacion Sapi
        /// </summary>
        public virtual string StatusDocumento
        {
            get { return _statusDocumento; }
            set { _statusDocumento = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene las iniciales del Gestor que recibe la Solicitud de Presentacion Sapi 
        /// para llevarla a SAPI
        /// </summary>
        public virtual string ReceptorMatPresent
        {
            get { return _receptorMatPresent; }
            set { _receptorMatPresent = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha la cual el Gestor que recibe la Solicitud de Presentacion Sapi 
        /// para llevarla a SAPI
        /// </summary>
        public virtual DateTime? FechaRecep_Gestor1
        {
            get { return _fechaRecep_Gestor1; }
            set { _fechaRecep_Gestor1 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el indicador que señala si el Documento fue recibido por el Gestor para introducir a SAPI
        /// </summary>
        public virtual char? RecibeDocumento
        {
            get { return _recibeDocumento; }
            set { _recibeDocumento = value; }
        }

        public virtual bool BRecibeDocumento
        {
            get
            {
                if (this.RecibeDocumento.Equals('T'))
                    return true;
                else
                    return false;
            }

            set
            {
                if (value)
                {
                    this.RecibeDocumento = 'T';
                }
                else
                {
                    this.RecibeDocumento = 'F';
                }
            }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene las iniciales del Gestor que presenta la Solicitud de Presentacion ante SAPI 
        /// </summary>
        public virtual string PresentadorAnteSAPI
        {
            get { return _presentadorAnteSAPI; }
            set { _presentadorAnteSAPI = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha la cual el Gestor presenta la Solicitud de Presentacion en SAPI
        /// </summary>
        public virtual DateTime? FechaPres_Gestor2
        {
            get { return _fechaPres_Gestor2; }
            set { _fechaPres_Gestor2 = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el indicador que señala si el Documento fue presentado por el Gestor en SAPI
        /// </summary>
        public virtual char? PresentadoASapi
        {
            get { return _presentadoASapi; }
            set { _presentadoASapi = value; }
        }

        public virtual bool BPresentadoASapi
        {
            get
            {
                if (this.PresentadoASapi.Equals('T'))
                    return true;
                else
                    return false;
            }

            set
            {
                if (value)
                {
                    this.PresentadoASapi = 'T';
                }
                else
                {
                    this.PresentadoASapi = 'F';
                }
            }
        }
        


        /// <summary>
        /// Propiedad que asigna u obtiene las iniciales del Gestor recibe la Solicitud de Presentacion de SAPI 
        /// </summary>
        public virtual string ReceptorAnteSAPI
        {
            get { return _receptorAnteSAPI; }
            set { _receptorAnteSAPI = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha la cual el Gestor recibe la Solicitud de Presentacion de SAPI
        /// </summary>
        public virtual DateTime? FechaRecep_Gestor3
        {
            get { return _fechaRecep_Gestor3; }
            set { _fechaRecep_Gestor3 = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el indicador que señala si el Documento fue recibido por el Gestor desde SAPI
        /// </summary>
        public virtual char? RecibioDeSapi
        {
            get { return _recibioDeSapi; }
            set { _recibioDeSapi = value; }
        }

        public virtual bool BRecibioDeSapi
        {
            get
            {
                if (this.RecibioDeSapi.Equals('T'))
                    return true;
                else
                    return false;
            }

            set
            {
                if (value)
                {
                    this.RecibioDeSapi = 'T';
                }
                else
                {
                    this.RecibioDeSapi = 'F';
                }
            }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene las iniciales del Departamento que recibe la Solicitud de Presentacion que viene de SAPI 
        /// </summary>
        public virtual string InicDptoReceptor
        {
            get { return _inicDptoReceptor; }
            set { _inicDptoReceptor = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la Fecha la cual el Departamento Receptor recibe la Solicitud de Presentacion que viene de SAPI 
        /// </summary>
        public virtual DateTime? FechaRecep_Dpto
        {
            get { return _fechaRecep_Dpto; }
            set { _fechaRecep_Dpto = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el indicador que señala si el Documento fue recibido por el Departamento
        /// </summary>
        public virtual char? RecibioDpto
        {
            get { return _recibioDpto; }
            set { _recibioDpto = value; }
        }

        public virtual bool BRecibioDpto
        {
            get
            {
                if (this.RecibioDpto.Equals('T'))
                    return true;
                else
                    return false;
            }

            set
            {
                if (value)
                {
                    this.RecibioDpto = 'T';
                }
                else
                {
                    this.RecibioDpto = 'F';
                }
            }
        }
        #endregion
    }
}
