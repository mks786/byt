using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class Busqueda
    {
        #region Atributos

        private int _id;
        private int _idMarca;
        private int _paginaDiseno;
        private int _paginaPalabra;
        private int _reciboDiseno;
        private int _pedidoDiseno;
        private int _reciboPalabra;
        private int _pedidoPalabra;
        private char _tipoBus;
        private string _codVienaDis;
        private DateTime? _fechaBusquedaPalabra;
        private DateTime? _fechaConsigDiseno;
        private DateTime? _fechaBusquedaDiseno;
        private DateTime? _horaBusquedaPalabra;
        private DateTime? _fechaConsigPalabra;
        private DateTime? _fechaSolicitudPalabra;


        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Busqueda()
        {
        }

        /// <summary>
        /// Constructor que inicializa el condigo de la infoBol
        /// </summary>
        /// <param name="id">Codigo de la infoBol</param>
        public Busqueda(int id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades


        /// <summary>
        /// Propiedad que asigna u obtiene el id de la busqueda
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el IdMarca de la busqueda
        /// </summary>
        public virtual int IdMarca
        {
            get { return _idMarca; }
            set { _idMarca = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el PaginaDiseno de la busqueda
        /// </summary>
        public virtual int PaginaDiseno
        {
            get { return _paginaDiseno; }
            set { _paginaDiseno = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el PaginaPalabra de la busqueda
        /// </summary>
        public virtual int PaginaPalabra
        {
            get { return _paginaPalabra; }
            set { _paginaPalabra = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el ReciboDiseno de la busqueda
        /// </summary>
        public virtual int ReciboDiseno
        {
            get { return _reciboDiseno; }
            set { _reciboDiseno = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el PedidoDiseno de la busqueda
        /// </summary>
        public virtual int PedidoDiseno
        {
            get { return _pedidoDiseno; }
            set { _pedidoDiseno = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el ReciboPalabra de la busqueda
        /// </summary>
        public virtual int ReciboPalabra
        {
            get { return _reciboPalabra; }
            set { _reciboPalabra = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el PedidoPalabra de la busqueda
        /// </summary>
        public virtual int PedidoPalabra
        {
            get { return _pedidoPalabra; }
            set { _pedidoPalabra = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el TipoBus de la busqueda
        /// </summary>
        public virtual char TipoBus
        {
            get { return _tipoBus; }
            set { _tipoBus = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el CodVienaDis de la busqueda
        /// </summary>
        public virtual string CodVienaDis
        {
            get { return _codVienaDis; }
            set { _codVienaDis = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el FechaBusquedaPalabra de la busqueda
        /// </summary>
        public virtual DateTime? FechaBusquedaPalabra
        {
            get { return _fechaBusquedaPalabra; }
            set { _fechaBusquedaPalabra = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el FechaConsigDiseno de la busqueda
        /// </summary>
        public virtual DateTime? FechaConsigDiseno
        {
            get { return _fechaConsigDiseno; }
            set { _fechaConsigDiseno = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el FechaBusquedaDiseno de la busqueda
        /// </summary>
        public virtual DateTime? FechaBusquedaDiseno
        {
          
        /// <summary>
        /// Propiedad que asigna u obtiene el IdMarca de la busqueda
        /// </summary>  get { return _fechaBusquedaDiseno; }
            set { _fechaBusquedaDiseno = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el HoraBusquedaPalabra de la busqueda
        /// </summary>
        public virtual DateTime? HoraBusquedaPalabra
        {
            get { return _horaBusquedaPalabra; }
            set { _horaBusquedaPalabra = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el FechaConsigPalabra de la busqueda
        /// </summary>
        public virtual DateTime? FechaConsigPalabra
        {
            get { return _fechaConsigPalabra; }
            set { _fechaConsigPalabra = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el FechaSolicitudPalabra de la busqueda
        /// </summary>
        public virtual DateTime? FechaSolicitudPalabra
        {
            get { return _fechaSolicitudPalabra; }
            set { _fechaSolicitudPalabra = value; }
        }

        #endregion
    }
}
