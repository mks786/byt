Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoDetalleEnvioNHibernate
        Inherits DaoBaseNHibernate(Of FacDetalleEnvio, String)
        Implements IDaoDetalleEnvio
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace