using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CertificadoMarca
    {
        #region Atributos

        private int _idMarca;
        private string _numeroRecibo;
        private DateTime _fechaRecibo;
        private string _registroBs;
        private string _escrituraBs;
        private string _papelProtocolo;
        private string _totalBs;
        private string _clases;
        private string _comentario;
        private Registrador _registrador;
        private string _operacion;

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public CertificadoMarca() { }

        /// <summary>
        /// Constructor que inicializa el id de la marca
        /// </summary>
        /// <param name="idMarca">Id del Concepto</param>
        public CertificadoMarca(int idMarca)
        {
            this._idMarca = idMarca;
        }

        #endregion

        #region Propiedades

       
        public virtual int IdMarca
        {
            get { return _idMarca; }
            set { _idMarca = value; }
        }

        public virtual string NumeroRecibo
        {
            get { return _numeroRecibo; }
            set { _numeroRecibo = value; }
        }

        public virtual DateTime FechaRecibo
        {
            get { return _fechaRecibo; }
            set { _fechaRecibo = value; }
        }


        public virtual string RegistroBs
        {
            get { return _registroBs; }
            set { _registroBs = value; }
        }


        public virtual string EscrituraBs
        {
            get { return _escrituraBs; }
            set { _escrituraBs = value; }
        }


        public virtual string PapelProtocolo
        {
            get { return _papelProtocolo; }
            set { _papelProtocolo = value; }
        }

        
        public virtual string TotalBs
        {
            get { return _totalBs; }
            set { _totalBs = value; }
        }

        
        public virtual string Clases
        {
            get { return _clases; }
            set { _clases = value; }
        }


        public virtual string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }


        public virtual Registrador CodRegistrador
        {
            get { return _registrador; }
            set { _registrador = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la opreacion que se va a realizar con este objeto
        /// sea CREATE, MODIFY o DELETE
        /// </summary>
        public virtual string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }


        #endregion
    }
}
