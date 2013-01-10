Imports NHibernate
Imports NHibernate.Cfg

'Namespace Diginsoft.Bolet.AccesoDatos
Public Class NHibernateHelper
    Private Shared ReadOnly _sessionFactory As ISessionFactory

    ''' <summary>
    ''' Constructor estático en donde se inicializa la sesion de NHibernate
    ''' </summary>
    Shared Sub New()
        Try
            Dim cfg As New Configuration()
            _sessionFactory = cfg.Configure().BuildSessionFactory()
        Catch e As Exception
            Throw
        End Try
    End Sub


    ''' <summary>
    ''' Metodo estático para solicitar la sesion de NHibernate
    ''' </summary>
    ''' <returns>retorna la sesion abierta</returns>
    Public Shared Function OpenSession() As ISession
        Return _sessionFactory.OpenSession()
    End Function

    ''' <summary>
    ''' Metodo que retorna la sesion abierta
    ''' </summary>
    ''' <returns>la sesion</returns>
    Public Shared Function GetCurrentSession() As ISession
        Return _sessionFactory.GetCurrentSession()
    End Function
End Class
'End Namespace