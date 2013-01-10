Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace Dao.NHibernate
    Public Class DaoViGestionAsociadoNHibernate
        Inherits DaoBaseNHibernate(Of ViGestionAsociado, Integer)
        Implements IDaoViGestionAsociado
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace