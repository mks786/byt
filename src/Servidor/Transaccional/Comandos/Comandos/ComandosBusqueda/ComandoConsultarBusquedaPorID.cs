using System;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosBusqueda
{
    public class ComandoConsultarBusquedaPorID : ComandoBase<Busqueda>
    {
        Busqueda _busqueda;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="busqueda">busqueda a consultar</param>
        public ComandoConsultarBusquedaPorID(Busqueda busqueda)
        {
            this._busqueda = busqueda;
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            IDaoBusqueda dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoBusqueda();
            this.Receptor = new Receptor<Busqueda>(dao.ObtenerPorId(this._busqueda.Id));
        }
    }
}
