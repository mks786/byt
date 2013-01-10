Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoCorrespondenciaNHibernate
        Inherits DaoBaseNHibernate(Of Correspondencia, Integer)
        Implements IDaoCorrespondencia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace