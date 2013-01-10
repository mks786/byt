Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoMediosGestionNHibernate
        Inherits DaoBaseNHibernate(Of MediosGestion, String)
        Implements IDaoMediosGestion
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace