Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoImpuestoNHibernate
        Inherits DaoBaseNHibernate(Of Impuesto, datetime)
        Implements IDaoImpuesto
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace