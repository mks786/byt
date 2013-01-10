Imports System.Collections.Generic
Imports NHibernate
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Dao.NHibernate
    Public Class DaoRolNHibernate
        Inherits DaoBaseNHibernate(Of Rol, String)
        Implements IDaoRol

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObteneRolesYObjetos() As System.Collections.Generic.IList(Of ObjetosComunes.Entidades.Rol) Implements Contrato.IDaoRol.ObteneRolesYObjetos
            Dim roles As IList(Of Rol)

            Try
                Dim query As IQuery = Session.CreateQuery(Recursos.ConsultasHQL.ObtenerRolesYObjetos)
                roles = query.List(Of Rol)()
            Catch ex As Exception
                logger.[Error](ex.Message)
                Throw New ApplicationException(Recursos.Errores.ExConsultarTodos)
            Finally
                Session.Close()
            End Try

            Return roles
        End Function
    End Class
End Namespace