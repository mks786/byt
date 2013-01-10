Imports System.Configuration
Imports NHibernate
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Dao.NHibernate
    Public Class DaoUsuarioNHibernate
        Inherits DaoBaseNHibernate(Of Usuario, String)
        Implements IDaoUsuario
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function Autenticar(ByVal usuario As ObjetosComunes.Entidades.Usuario) As ObjetosComunes.Entidades.Usuario Implements Contrato.IDaoUsuario.Autenticar
            Dim retorno As Usuario
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region                
                Dim query As IQuery = Session.CreateQuery(String.Format(Recursos.ConsultasHQL.ObtenerUsuarioPorIdYPassword, usuario.Id, usuario.Password))
                'Dim query As IQuery = Session.CreateQuery(String.Format(Recursos.ConsultasHQL.ObtenerUsuarioPorIdYPassword, "HP", "HUGO"))
                retorno = query.UniqueResult(Of Usuario)()

                '#Region "trace"
                If ConfigurationManager.AppSettings("Ambiente").ToString().Equals("Desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Throw New ApplicationException(Recursos.Errores.ExConsultarTodosUsuariosPorUsuario)
            Finally
                Session.Close()
            End Try

            Return retorno
        End Function
    End Class
End Namespace