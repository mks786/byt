Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoFacAnualidadNHibernate
        Inherits DaoBaseNHibernate(Of FacAnualidad, String)
        Implements IDaoFacAnualidad
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace