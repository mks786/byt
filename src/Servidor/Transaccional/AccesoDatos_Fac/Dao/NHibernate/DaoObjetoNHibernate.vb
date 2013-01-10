Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Dao.NHibernate
    Public Class DaoObjetoNHibernate
        Inherits DaoBaseNHibernate(Of Objeto, String)
        Implements IDaoObjeto
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace