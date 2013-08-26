using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class FiltroPlantilla
    {
        #region Atributos

        private int _id;
        private Plantilla _plantilla;
        private string _nombreCampoFiltro;
        private string _tipoDatoCampoFiltro;
        private string _nombreVariableFiltro;
        private string _tipoDeFiltro;
        private string _operacion;
               
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public FiltroPlantilla() { }

        
        /// <summary>
        /// Constructor predeterminado que recibe el id del filtro, la plantilla al que pertenece y el tipo de filtro 
        /// </summary>
        /// <param name="id">Id del filtro</param>
        /// <param name="plantilla">Plantilla al que pertenece el filtro</param>
        /// <param name="tipoDeFiltro">Si el filtro es de Encabezado o de Detalle</param>
        public FiltroPlantilla(int id, Plantilla plantilla, string tipoDeFiltro)
        {
            this._id = id;
            this._plantilla = plantilla;
            this._tipoDeFiltro = tipoDeFiltro;
        }

        #endregion


        #region Propiedades

        /// <summary>
        /// Sobreescritura del Método Equals debido a que la clase tiene id compuesto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as FiltroPlantilla;
            if (t == null)
                return false;
            if ((Id == (t.Id)) && (Plantilla.Id == t.Plantilla.Id))
                return true;
            return false;

        }

        /// <summary>
        /// Sobreescritura del Método GetHashCode debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Sobreescritura del Método ToString debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }





        /// <summary>
        /// Propiedad que asigna el Id del Filtro de la Plantillla 
        /// </summary>
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        
        /// <summary>
        /// Propiedad que asigna la Plantilla al que pertenece el Filtro
        /// </summary>
        public virtual Plantilla Plantilla
        {
            get { return this._plantilla; }
            set { this._plantilla = value; }
        }



        /// <summary>
        /// Propiedad que asigna el Nombre del Campo que se utilizara como Filtro
        /// </summary>
        public virtual string NombreCampoFiltro
        {
            get { return this._nombreCampoFiltro; }
            set { this._nombreCampoFiltro = value; }
        }



        /// <summary>
        /// Propiedad que asigna el Tipo de Dato del Campo que se usara como Filtro 
        /// </summary>
        public virtual string TipoDatoCampoFiltro
        {
            get { return this._tipoDatoCampoFiltro; }
            set { this._tipoDatoCampoFiltro = value; }
        }


        /// <summary>
        /// Propiedad que asigna el nombre asignado a la variable que se usuara como Filtro en la Plantilla
        /// </summary>
        public virtual string NombreVariableFiltro
        {
            get { return this._nombreVariableFiltro; }
            set { this._nombreVariableFiltro = value; }
        }



        /// <summary>
        /// Propiedad que asigna el Almacen del Archivo
        /// </summary>
        public virtual string TipoDeFiltro
        {
            get { return this._tipoDeFiltro; }
            set { this._tipoDeFiltro = value; }
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
