using System;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosOperacion
{
    public class ComandoConsultarOperacionPorID : ComandoBase<Operacion>
    {
        Operacion _operacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="operacion">Operacion a consultar</param>
        public ComandoConsultarOperacionPorID(Operacion operacion)
        {
            this._operacion = operacion;
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            IDaoOperacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoOperacion();
            this.Receptor = new Receptor<Operacion>(dao.ObtenerPorId(this._operacion.Id));
        }
    }
}
