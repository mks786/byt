using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCertificadoMarca;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCertificadoMarca
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar un Certificado de Marca por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<CertificadoMarca> ObtenerComandoConsultarPorID(CertificadoMarca certificado)
        {
            return new ComandoConsultarCertificadoMarcaPorID(certificado);
        }
    }
}
