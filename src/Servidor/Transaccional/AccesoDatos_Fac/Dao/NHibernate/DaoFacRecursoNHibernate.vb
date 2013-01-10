Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoFacRecursoNHibernate
        Inherits DaoBaseNHibernate(Of FacRecurso, String)
        Implements IDaoFacRecurso
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace