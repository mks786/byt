
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static IList<Usuario> _usuarios = new List<Usuario>();

        private static string _rutaXMLUsuarios = ConfigurationManager.AppSettings["RutaXMLUsuarios"];

        /// <summary>
        /// Método que crea la sesión del cliente
        /// </summary>
        /// <param name="usuario">Usuario autenticado</param>
        /// <returns>Usuario autenticado con el hash</returns>
        public static Usuario CrearSesion(Usuario usuario)
        {
            bool agregar = true;
            usuario.Hash = usuario.GetHashCode();

            foreach (Usuario usu in _usuarios)
            {
                if (usu.Id == usuario.Id)
                {
                    agregar = false;
                    usuario.Hash = usu.Hash;
                }
            }

            if (agregar)
            {
                AgregarUsuarioXML(usuario);
                _usuarios.Add(usuario);
            }

            System.Console.WriteLine("Usuario agregado: " + usuario.Hash);
            return usuario;
        }

        /// <summary>
        /// Método que obtiene el usuario autenticado por su hash
        /// </summary>
        /// <param name="hash">Hash del usuario autenticado</param>
        /// <returns>El usuario autenticado</returns>
        public static Usuario ObtenerUsuarioPorHash(int hash)
        {
            Usuario retorno = null;

            foreach (Usuario usuario in _usuarios)
                if (hash == usuario.Hash)
                {
                    retorno = usuario;
                    break;
                }

            return retorno;
        }

        /// <summary>
        /// Método que cierra la sesion del cliente
        /// </summary>
        /// <param name="hash"></param>
        public static void CerrarSesion(int hash)
        {
            foreach (Usuario usuario in _usuarios)
                if (hash == usuario.Hash)
                {
                    EliminarUsuarioXML(usuario);
                    _usuarios.Remove(usuario);
                    System.Console.WriteLine("Usuario eliminado: " + usuario.Hash);
                    break;
                }
        }

        /// <summary>
        /// Método que devuleve la lista de auditorias que presenta una entidad
        /// </summary>
        /// <param name="auditoria">Auditoria que tiene los parametros para filtrar</param>
        /// <returns>Lista de todas las auditorias que presenta una entidad</returns>
        public static IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria)
        {
            IList<Auditoria> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Auditoria>> comando = FabricaComandosAuditoria.ObtenerComandoAuditoriaPorFkyTabla(auditoria);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            return retorno;
        }

        /// <summary>
        /// Método que se encarga de agregar un usuario en el XML
        /// </summary>
        /// <param name="usuario">usuario a insertar en el XML</param>
        private static void AgregarUsuarioXML(Usuario usuario)
        {
            try
            {
                XDocument doc;

                if (!File.Exists(_rutaXMLUsuarios))
                {
                    XElement xml = new XElement("Usuarios",
                               new XElement("Usuario",
                                   new XAttribute("Id", usuario.Id.ToString()),
                                       new XElement("Hash", usuario.Hash)

                        ));
                    xml.Save(_rutaXMLUsuarios);
                }
                else
                {
                    doc = XDocument.Load(_rutaXMLUsuarios); //load the xml file.

                    IEnumerable<XElement> listaDeUsuarios = doc.Element("Usuarios").Elements("Usuario");
                    var nuevoUsuario = new XElement("Usuario",
                     new XAttribute("Id", usuario.Id.ToString()),
                     new XElement("Hash", usuario.Hash));
                    if (listaDeUsuarios.Count() == 0)
                    {
                        XElement xml = doc.Element("Usuarios");
                        xml.Add(new XElement("Usuario",
                                   new XAttribute("Id", usuario.Id.ToString()),
                                       new XElement("Hash", usuario.Hash)));
                    }
                    else
                        listaDeUsuarios.Last().AddAfterSelf(nuevoUsuario); //add node to the last element.

                    doc.Save(_rutaXMLUsuarios);
                }
            }
            catch (IOException ex)
            {
                logger.Error(ex.Message);
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Método que se encarga de eliminar un usuario en el XML
        /// </summary>
        /// <param name="usuario">usuario a insertar en el XML</param>
        private static void EliminarUsuarioXML(Usuario usuario)
        {
            XElement element = LoadXML();
            if (element != null)
            {
                var xml = (from usuarioBuscado in element.Descendants("Usuario")
                           where
                               usuarioBuscado.Attribute("Id").Value == usuario.Id
                           select usuarioBuscado).SingleOrDefault();
                if (xml != null)
                {
                    xml.Remove();
                    element.Save(_rutaXMLUsuarios);
                }
            }
        }

        private static XElement LoadXML()
        {
            if (System.IO.File.Exists(_rutaXMLUsuarios))
            {
                XElement xml = XElement.Load(_rutaXMLUsuarios);
                return xml;
            }
            return null;
        }
        /// <summary>
        /// Método que se encarga de eliminar un usuario en el XML
        /// </summary>
        /// <param name="usuario">usuario a insertar en el XML</param>
        private static void ConsultarUsuarioXML(Usuario usuario)
        {
            try
            {

            }
            catch (IOException ex)
            {
                logger.Error(ex.Message);
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// Mètodo que se encarga de cargar los usuarios presentes en el XML
        /// </summary>
        public static int CargarUsuariosXML()
        {
            XElement element = LoadXML();
            int retorno = 0;
            if (element != null)
            {
                var query = from usuario in element.Descendants("Usuario")
                            select new
                            {
                                IdUsuario = (string)usuario.Attribute("Id"),
                                HashUsuario = usuario.Element("Hash").Value,
                            };
                retorno = query.Count();
                if (query != null && query.Count() > 0)
                {
                    for (int i = 0; i < query.Count(); i++)
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = query.ElementAt(i).IdUsuario;
                        usuario.Hash = int.Parse(query.ElementAt(i).HashUsuario);
                        _usuarios.Add(usuario);
                    }
                }
            }

            return retorno;
        }
    }
}
