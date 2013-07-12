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


        /// <summary>
        /// Metodo que devuelve el Comando para insertar o modificar un Certificado de Marca
        /// </summary>
        /// <param name="certificado">Certificado de Marca a insertar o modificar</param>
        /// <returns>Comando para insertar o modificar un certificado de marca</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CertificadoMarca certificado)
        {
            return new ComandoInsertarOModificarCertificado(certificado);
        }


        /// <summary>
        /// Método que devuelve el Comando para elimnar un certificado de marca
        /// </summary>
        /// <param name="certificado">certificado que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCertificado(CertificadoMarca certificado)
        {
            return new ComandoEliminarCertificado(certificado);
        }

    }
}
