Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoFacBancoNHibernate
        Inherits DaoBaseNHibernate(Of FacBanco, Integer)
        Implements IDaoFacBanco
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace