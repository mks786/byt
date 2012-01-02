using System;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosAnaqua
{
    public class ComandoConsultarAnaquaPorID : ComandoBase<Anaqua>
    {
        Anaqua _anaqua;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="anaqua">InfoAdicional a consultar</param>
        public ComandoConsultarAnaquaPorID(Anaqua anaqua)
        {
            this._anaqua = anaqua;
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            IDaoAnaqua dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoAnaqua();
            this.Receptor = new Receptor<Anaqua>(dao.ObtenerPorId(this._anaqua.IdMarca));
        }
    }
}
