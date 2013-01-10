Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoMotivoNHibernate
        Inherits DaoBaseNHibernate(Of FacMotivo, Integer)
        Implements IDaoMotivo
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace