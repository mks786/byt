
namespace Trascend.Bolet.AccesoDatos.Contrato
{
    interface IFabricaDaoBase
    {
        IDaoDepartamento ObtenerDaoDepartamento();

        IDaoObjeto ObtenerDaoObjeto();

        IDaoRol ObtenerDaoRol();

        IDaoUsuario ObtenerDaoUsuario();

        IDaoAuditoria ObtenerDaoAuditoria();

        IDaoEstado ObtenerDaoEstado();
    }
}
