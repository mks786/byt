Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoDetallePagoNHibernate
        Inherits DaoBaseNHibernate(Of DetallePago, String)
        Implements IDaoDetallePago
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace