using System;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInfoBol
{
    public class ComandoConsultarInfoBolPorID : ComandoBase<InfoBol>
    {
        InfoBol _infoBol;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="infoBol">InfoBol a consultar</param>
        public ComandoConsultarInfoBolPorID(InfoBol infoBol)
        {
            this._infoBol = infoBol;
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            IDaoInfoBol dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInfoBol();
            this.Receptor = new Receptor<InfoBol>(dao.ObtenerPorId(this._infoBol.Id));
        }
    }
}
