using System;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInstruccionDeRenovacion
{
    public class ComandoConsultarInstruccionDeRenovacionPorId : ComandoBase<InstruccionDeRenovacion>
    {
        InstruccionDeRenovacion _instruccionDeRenovacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="instruccionDeRenovacion">instruccionDeRenovacion a consultar</param>
        public ComandoConsultarInstruccionDeRenovacionPorId(InstruccionDeRenovacion instruccionDeRenovacion)
        {
            this._instruccionDeRenovacion = instruccionDeRenovacion;
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            //IDaoInstruccionDeRenovacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInstruccionDeRenovacion();
            //this.Receptor = new Receptor<InstruccionDeRenovacion>(dao.ObtenerPorId(this._instruccionDeRenovacion.Id));
        }
    }
}
