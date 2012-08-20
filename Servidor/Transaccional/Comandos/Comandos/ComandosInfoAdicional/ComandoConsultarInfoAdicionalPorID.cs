using System;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInfoAdicional
{
    public class ComandoConsultarInfoAdicionalPorID : ComandoBase<InfoAdicional>
    {
        InfoAdicional _infoAdicional;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="infoAdicional">InfoAdicional a consultar</param>
        public ComandoConsultarInfoAdicionalPorID(InfoAdicional infoAdicional)
        {
            this._infoAdicional = infoAdicional;
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            IDaoInfoAdicional dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInfoAdicional();
            this.Receptor = new Receptor<InfoAdicional>(dao.ObtenerPorId(this._infoAdicional.Id));
        }
    }
}
