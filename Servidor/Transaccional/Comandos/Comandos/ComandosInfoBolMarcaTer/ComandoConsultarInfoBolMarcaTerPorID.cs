using System;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInfoBolMarcaTer
{
    public class ComandoConsultarInfoBolMarcaTerPorID : ComandoBase<InfoBolMarcaTer>
    {
        InfoBolMarcaTer _infoBol;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="infoBol">InfoBolMarcaTer a consultar</param>
        public ComandoConsultarInfoBolMarcaTerPorID(InfoBolMarcaTer infoBol)
        {
            this._infoBol = infoBol;
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            //IDaoInfoBolMarcaTer dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInfoBolMarcaTer();
            //this.Receptor = new Receptor<InfoBolMarcaTer>(dao.ObtenerPorId(this._infoBol.Id));
        }
    }
}
