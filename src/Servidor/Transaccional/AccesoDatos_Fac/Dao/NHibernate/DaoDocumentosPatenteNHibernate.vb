Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoDocumentosPatenteNHibernate
        Inherits DaoBaseNHibernate(Of DocumentosPatente, String)
        Implements IDaoDocumentosPatente
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
    End Class
End Namespace