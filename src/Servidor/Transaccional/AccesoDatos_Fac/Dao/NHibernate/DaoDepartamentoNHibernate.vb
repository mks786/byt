Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Dao.NHibernate
    Public Class DaoDepartamentoNHibernate
        Inherits DaoBaseNHibernate(Of Departamento, String)
        Implements IDaoDepartamento
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

    End Class
End Namespace
