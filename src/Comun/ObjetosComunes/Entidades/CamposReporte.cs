using System;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.Entidades
{
    [Serializable]
    public class CamposReporte
    {
        #region Atributos

        private string _id;
        private string _nombreCampo;
        private string _tipoCampo;
        private string _clase;
        private string _tipoReporte;
        private string _encabezadoEspanol;
        private string _encabezadoIngles;
        private string _adicionalEspanol;
        private string _adicionalIngles;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        public CamposReporte() { }

        /// <summary>
        /// Constructor que inicializa el id del Campo del Reporte
        /// </summary>
        /// <param name="id">Id del campo</param>
        public CamposReporte(string id)
        {
            this._id = id;
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Sobreescritura del Método Equals debido a que la clase tiene id compuesto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //        return false;
        //    var t = obj as CamposReporte;
        //    if (t == null)
        //        return false;
        //    if ((Id == (t.Id)) && (TipoDeReporte == (t.TipoDeReporte)))
        //        return true;
        //    return false;

        //}

        /// <summary>
        /// Sobreescritura del Método GetHashCode debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        /// <summary>
        /// Sobreescritura del Método ToString debido a que la clase tiene id compuesto
        /// </summary>
        /// <returns></returns>
        //public override string ToString()
        //{
        //    return base.ToString();
        //}



        /// <summary>
        /// Propiedad que asigna u obtiene el Id del Campo para Reporte
        /// </summary>
        public virtual string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene el Nombre del Campo para Reporte
        /// </summary>
        public virtual string NombreCampo
        {
            get { return this._nombreCampo; }
            set { this._nombreCampo = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el TipoDeReporte del Campo de Reporte
        /// </summary>
        public virtual string TipoDeReporte
        {
            get { return this._tipoReporte; }
            set { this._tipoReporte = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el TipoDeCampo para el Campo de Reporte 
        /// </summary>
        public virtual string TipoDeCampo
        {
            get { return this._tipoCampo; }
            set { this._tipoCampo = value; }
        }

        /// <summary>
        /// Propiedad que asigna u obtiene la Clase del Reporte de Marca
        /// </summary>
        public virtual string Clase
        {
            get { return this._clase; }
            set { this._clase = value; }
        }

        

        /// <summary>
        /// Propiedad que asigna u obtiene el Encabezado en Español del Campo de Reporte
        /// </summary>
        public virtual string EncabezadoEspanol
        {
            get { return this._encabezadoEspanol; }
            set { this._encabezadoEspanol = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene el Encabezado en Ingles del Campo de Reporte
        /// </summary>
        public virtual string EncabezadoIngles
        {
            get { return this._encabezadoIngles; }
            set { this._encabezadoIngles = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la informacion Adicional en Español del Campo de Reporte
        /// </summary>
        public virtual string AdicionalEspanol
        {
            get { return this._adicionalEspanol; }
            set { this._adicionalEspanol = value; }
        }


        /// <summary>
        /// Propiedad que asigna u obtiene la informacion Adicional en Ingles del Campo de Reporte
        /// </summary>
        public virtual string AdicionalIngles
        {
            get { return this._adicionalIngles; }
            set { this._adicionalIngles = value; }
        }

               

        #endregion


    }
}
