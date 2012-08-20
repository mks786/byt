using System;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInfoBolPatente
{
    public class ComandoConsultarInfoBolPorID : ComandoBase<InfoBolPatente>
    {
        InfoBolPatente _infoBol;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="infoBol">InfoBolPatente a consultar</param>
        public ComandoConsultarInfoBolPorID(InfoBolPatente infoBol)
        {
            this._infoBol = infoBol;
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            IDaoInfoBolPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInfoBolPatente();
            this.Receptor = new Receptor<InfoBolPatente>(dao.ObtenerPorId(this._infoBol.Id));
        }
    }
}
