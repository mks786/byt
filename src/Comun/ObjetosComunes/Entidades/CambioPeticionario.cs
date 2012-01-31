using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CambioPeticionario
    {
        #region Atributos

        private int _id;
        private DateTime? _fechaPeticionario;
        private DateTime? _fechaPublicacion;
        private char _acta;        
        private string _certificada;
        private string _expediente;
        private string _referencia;
        private string _comentario;
        private string _anexo;
        private string _ubicacion;
        private string _observacion;               
        private Poder _poderActual;
        private Poder _poderAnterior;
        private Interesado _interesadoActual;
        private Interesado _interesadoAnterior;
        private Boletin _boletinPublicacion;
        private Asociado _asociado;
        private Agente _agenteActual;
        private Agente _agenteAnterior;
        private Marca _marca;                                  

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public CambioPeticionario() { }

        /// <summary>
        /// Constructor que inicializa el Id del Agente
        /// </summary>
        /// <param name="id">Id del Agente</param>
        public CambioPeticionario(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad que asigna u obtiene el id del CambioDeDomicilio
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

      
  
        #endregion
    }
}
